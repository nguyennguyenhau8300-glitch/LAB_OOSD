using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyNhanSu.Data;
namespace QuanLyNhanSu.Services;
public static partial class NhanSuService
{
    public static int Payroll(int month,int year,decimal standard,bool close=false)=>Db.Transaction((c,t)=>Calculate(c,t,month,year,standard,close));
    static int Calculate(SqlConnection c,SqlTransaction t,int month,int year,decimal standard,bool close)
    {
        Require(month is >=1 and <=12 && year is >=2000 and <=2100 && standard>0,"Tháng, năm hoặc giờ chuẩn không hợp lệ.");
        var from=new DateTime(year,month,1);var to=from.AddMonths(1).AddDays(-1);Unlocked(c,t,from);
        Require(!close || to<DateTime.Today,"Chỉ chốt lương sau khi hết tháng.");
        var header=Db.Query(c,t,"SELECT Id FROM BangLuong WHERE Thang=@p0 AND Nam=@p1",month,year);
        int id=header.Rows.Count==0?Number(Db.Scalar(c,t,"INSERT BangLuong(Thang,Nam,GioChuan) OUTPUT INSERTED.Id VALUES(@p0,@p1,@p2)",month,year,standard)):Number(header.Rows[0][0]);
        Db.Exec(c,t,"UPDATE BangLuong SET GioChuan=@p1,NgayLap=SYSDATETIME() WHERE Id=@p0; DELETE FROM ChiTietLuong WHERE BangLuongId=@p0",id,standard);
        var employees=Db.Query(c,t,"SELECT DISTINCT n.Id,n.HoTen FROM NhanVien n WHERE EXISTS(SELECT 1 FROM TuyenDung td WHERE td.NhanVienId=n.Id AND td.TuNgay<=@p1 AND td.DenNgay>=@p0) OR EXISTS(SELECT 1 FROM HopDong hd WHERE hd.NhanVienId=n.Id AND hd.TrangThai=N'Đã ký' AND hd.TuNgay<=@p1 AND (hd.DenNgay IS NULL OR hd.DenNgay>=@p0)) OR EXISTS(SELECT 1 FROM ThuongPhat q WHERE q.NhanVienId=n.Id AND q.Ngay BETWEEN @p0 AND @p1 AND q.PhuongThuc=N'Qua lương' AND q.TrangThai=N'Đã ký')",from,to);
        foreach(DataRow employee in employees.Rows)
        {
            int nv=Number(employee[0]);string name=(string)employee[1];
            var attendance=Db.Query(c,t,"SELECT p.NgayLam,cc.SoGio FROM ChamCong cc JOIN PhanCong p ON p.Id=cc.PhanCongId WHERE p.NhanVienId=@p0 AND p.NgayLam BETWEEN @p1 AND @p2",nv,from,to);
            var ot=Db.Query(c,t,"SELECT p.Ngay,ct.SoGio FROM ChiTietPhuTroi ct JOIN PhieuPhuTroi p ON p.Id=ct.PhieuId WHERE ct.NhanVienId=@p0 AND p.Ngay BETWEEN @p1 AND @p2",nv,from,to);
            var contracts=Db.Query(c,t,"SELECT * FROM HopDong WHERE NhanVienId=@p0 AND TrangThai=N'Đã ký' AND TuNgay<=@p2 AND (DenNgay IS NULL OR DenNgay>=@p1)",nv,from,to);
            var salaries=Db.Query(c,t,"SELECT * FROM QDLuong WHERE NhanVienId=@p0 AND NgayHieuLuc<=@p1 ORDER BY NgayHieuLuc DESC",nv,to);
            var roles=Db.Query(c,t,"SELECT * FROM QDChucVu WHERE NhanVienId=@p0 AND NgayHieuLuc<=@p1 ORDER BY NgayHieuLuc DESC",nv,to);
            var trial=Db.Query(c,t,"SELECT * FROM TuyenDung WHERE NhanVienId=@p0",nv);
            var insurance=Db.Query(c,t,"SELECT * FROM BaoHiem WHERE NhanVienId=@p0 AND TuNgay<=@p2 AND (DenNgay IS NULL OR DenNgay>=@p1)",nv,from,to);
            bool Active(DataRow r,DateTime date,string start="TuNgay",string end="DenNgay")=>(DateTime)r[start]<=date && (r[end]==DBNull.Value||(DateTime)r[end]>=date);
            (decimal basic,decimal allowances) Rate(DateTime day)
            {
                if(contracts.Rows.Cast<DataRow>().Any(r=>Active(r,day)))
                {
                    var salary=salaries.Rows.Cast<DataRow>().FirstOrDefault(r=>(DateTime)r["NgayHieuLuc"]<=day);Require(salary!=null,$"{name}: thiếu quyết định lương có hiệu lực ngày {day:dd/MM/yyyy}.");
                    var role=roles.Rows.Cast<DataRow>().FirstOrDefault(r=>(DateTime)r["NgayHieuLuc"]<=day);
                    return(Money(salary!["LuongCoBan"]),Money(salary["PhuCapLuong"])+Money(salary["PhuCapDocHai"])+(role==null?0:Money(role["PhuCap"])));
                }
                var tr=trial.Rows.Cast<DataRow>().FirstOrDefault(r=>Active(r,day));Require(tr!=null,$"{name}: ngày {day:dd/MM/yyyy} có công nhưng không có hợp đồng hoặc thử việc hiệu lực.");
                return(Money(tr!["LuongThuViec"]),0);
            }
            decimal hours=0,extraHours=0,basePay=0,allowance=0,extraPay=0,bh=0;
            foreach(DataRow row in attendance.Rows){var rate=Rate((DateTime)row[0]);decimal h=Money(row[1]);hours+=h;basePay+=rate.basic*h/standard;allowance+=rate.allowances*h/standard;}
            foreach(DataRow row in ot.Rows){var rate=Rate((DateTime)row[0]);decimal h=Money(row[1]);extraHours+=h;extraPay+=rate.basic*h/standard*1.5m;}
            for(var day=from;day<=to;day=day.AddDays(1))
            {
                if(!contracts.Rows.Cast<DataRow>().Any(r=>(string)r["Loai"]=="Dài hạn" && Active(r,day)))continue;
                decimal basic=Rate(day).basic;
                foreach(var type in new[]{"BHYT","BHXH"})
                {
                    var policy=insurance.Rows.Cast<DataRow>().FirstOrDefault(r=>(string)r["Loai"]==type && Active(r,day));
                    Require(policy!=null,$"{name}: thiếu {type} có hiệu lực ngày {day:dd/MM/yyyy}.");
                    bh+=basic*Money(policy!["TyLe"])/100m/DateTime.DaysInMonth(year,month);
                }
            }
            var adjustments=Db.Query(c,t,"SELECT Loai,COALESCE(SUM(SoTien),0) Tien FROM ThuongPhat q WHERE NhanVienId=@p0 AND Ngay BETWEEN @p1 AND @p2 AND PhuongThuc=N'Qua lương' AND TrangThai=N'Đã ký' AND BangLuongId IS NULL AND NOT EXISTS(SELECT 1 FROM ThuChi x WHERE x.QuyetDinhId=q.Id) GROUP BY Loai",nv,from,to);
            decimal reward=0,fine=0;foreach(DataRow r in adjustments.Rows){if((string)r[0]=="Khen thưởng")reward=Money(r[1]);else fine=Money(r[1]);}
            decimal Round(decimal value)=>Math.Round(value,2,MidpointRounding.AwayFromZero);
            Db.Exec(c,t,"INSERT ChiTietLuong(BangLuongId,NhanVienId,GioCong,GioPhuTroi,LuongTheoCong,PhuCap,TienPhuTroi,BaoHiem,Thuong,Phat) VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",id,nv,hours,extraHours,Round(basePay),Round(allowance),Round(extraPay),Round(bh),reward,fine);
        }
        Require(employees.Rows.Count>0,"Chưa có nhân viên phát sinh trong tháng này.");
        Require(!Exists(c,t,"SELECT COUNT(*) FROM ChamCong cc JOIN PhanCong pc ON pc.Id=cc.PhanCongId WHERE pc.NgayLam BETWEEN @p0 AND @p1 AND NOT EXISTS(SELECT 1 FROM ChiTietLuong ct WHERE ct.BangLuongId=@p2 AND ct.NhanVienId=pc.NhanVienId)",from,to,id),"Có công của nhân viên thiếu thử việc/hợp đồng hiệu lực.");
        Require(!Exists(c,t,"SELECT COUNT(*) FROM ChiTietPhuTroi ct JOIN PhieuPhuTroi p ON p.Id=ct.PhieuId WHERE p.Ngay BETWEEN @p0 AND @p1 AND NOT EXISTS(SELECT 1 FROM ChiTietLuong l WHERE l.BangLuongId=@p2 AND l.NhanVienId=ct.NhanVienId)",from,to,id),"Có phụ trội của nhân viên thiếu thử việc/hợp đồng hiệu lực.");
        if(close)
        {
            Require(!Exists(c,t,"SELECT COUNT(*) FROM PhanCong p WHERE NgayLam BETWEEN @p0 AND @p1 AND NOT EXISTS(SELECT 1 FROM ChamCong cc WHERE cc.PhanCongId=p.Id)",from,to),"Còn phân công chưa chấm công; nhập 0 giờ nếu nghỉ.");
            Require(!Exists(c,t,"SELECT COUNT(*) FROM PhieuPhuTroi p WHERE Ngay BETWEEN @p0 AND @p1 AND NOT EXISTS(SELECT 1 FROM ChiTietPhuTroi ct WHERE ct.PhieuId=p.Id)",from,to),"Còn phiếu phụ trội chưa có nhân viên.");
            Require(!Exists(c,t,"SELECT COUNT(*) FROM ThuongPhat WHERE Ngay BETWEEN @p0 AND @p1 AND PhuongThuc=N'Qua lương' AND TrangThai=N'Chờ ký'",from,to),"Còn quyết định thưởng phạt qua lương chưa ký.");
            Db.Exec(c,t,"UPDATE ThuongPhat SET BangLuongId=@p2 WHERE Ngay BETWEEN @p0 AND @p1 AND PhuongThuc=N'Qua lương' AND TrangThai=N'Đã ký' AND BangLuongId IS NULL; INSERT ThuChi(QuyetDinhId,Ngay,SoTien,Loai,BangLuongId) SELECT Id,@p1,SoTien,CASE WHEN Loai=N'Khen thưởng' THEN N'Chi' ELSE N'Thu' END,@p2 FROM ThuongPhat q WHERE BangLuongId=@p2 AND SoTien>0 AND NOT EXISTS(SELECT 1 FROM ThuChi x WHERE x.QuyetDinhId=q.Id); UPDATE BangLuong SET TrangThai=N'Đã chốt' WHERE Id=@p2",from,to,id);
        }
        return id;
    }
}
