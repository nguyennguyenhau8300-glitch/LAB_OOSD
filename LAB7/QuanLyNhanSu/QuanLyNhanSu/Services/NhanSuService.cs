using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyNhanSu.Data;
namespace QuanLyNhanSu.Services;
public static partial class NhanSuService
{
    static void Require(bool condition,string message){if(!condition)throw new InvalidOperationException(message);}
    static int Number(object? x)=>Convert.ToInt32(x);
    static decimal Money(object? x)=>Convert.ToDecimal(x);
    static bool Exists(SqlConnection c,SqlTransaction t,string sql,params object?[] v)=>Number(Db.Scalar(c,t,sql,v))>0;
    static void Unlocked(SqlConnection c,SqlTransaction t,DateTime day)=>Require(!Exists(c,t,"SELECT COUNT(*) FROM BangLuong WHERE TrangThai=N'Đã chốt' AND Nam=YEAR(@p0) AND Thang=MONTH(@p0)",day),"Tháng này đã chốt lương, không thể sửa dữ liệu nguồn.");
    static void HistoryUnlocked(SqlConnection c,SqlTransaction t,DateTime from)=>Require(!Exists(c,t,"SELECT COUNT(*) FROM BangLuong WHERE TrangThai=N'Đã chốt' AND EOMONTH(DATEFROMPARTS(Nam,Thang,1))>=@p0",from),"Thay đổi này ảnh hưởng lịch sử lương đã chốt.");
    public static DataTable List(Module m,string search="")
    {
        // Identifiers come exclusively from the fixed module catalog.
        var cols=m.Fields.Where(f=>f.Kind is "text" or "long").Select(f=>$"[{f.Name}] LIKE @p0").ToArray();
        return Db.Query($"SELECT * FROM [{m.Table}]"+(cols.Length>0?" WHERE "+string.Join(" OR ",cols):"")+" ORDER BY Id DESC","%"+search+"%");
    }
    public static DataTable Lookup(string table)
    {
        string sql=table switch {
            "NhanVien"=>"SELECT Id,CONCAT(Id,N' - ',HoTen) Ten FROM NhanVien ORDER BY HoTen",
            "PhanCong"=>"SELECT p.Id,CONCAT(n.HoTen,N' | ',CONVERT(varchar,p.NgayLam,103),N' | ',c.Ten) Ten FROM PhanCong p JOIN NhanVien n ON n.Id=p.NhanVienId JOIN CaLam c ON c.Id=p.CaLamId ORDER BY p.NgayLam DESC",
            "PhieuPhuTroi"=>"SELECT Id,CONCAT(SoPhieu,N' | ',CONVERT(varchar,Ngay,103)) Ten FROM PhieuPhuTroi ORDER BY Ngay DESC",
            "PhongBan" or "ChucVu" or "CaLam"=>$"SELECT Id,Ten FROM [{table}] ORDER BY Ten",
            _=>throw new InvalidOperationException("Danh mục không hợp lệ")};
        return Db.Query(sql);
    }
    static void Validate(SqlConnection c,SqlTransaction t,Module m,int id,Dictionary<string,object?> v)
    {
        foreach(var f in m.Fields){Require(v.ContainsKey(f.Name),"Thiếu trường "+f.Label);if(f.Required)Require(v[f.Name]!=null && (v[f.Name] is not string s || !string.IsNullOrWhiteSpace(s)),"Cần nhập "+f.Label);}
        if(v.ContainsKey("TuNgay") && v.GetValueOrDefault("DenNgay") is DateTime end)Require(end>=Convert.ToDateTime(v["TuNgay"]),"Ngày kết thúc phải từ ngày bắt đầu trở đi.");
        if(m.Table=="NhanVien")Require(Convert.ToDateTime(v["NgaySinh"])<DateTime.Today,"Ngày sinh phải trước hôm nay.");
        if(m.Table=="CaLam"){
            Require((TimeSpan)v["GioBatDau"]! != (TimeSpan)v["GioKetThuc"]!,"Giờ bắt đầu và kết thúc phải khác nhau.");
            if(id>0)Require(!Exists(c,t,"SELECT COUNT(*) FROM PhanCong WHERE CaLamId=@p0",id),"Ca đã được phân công. Tạo ca mới để giữ lịch sử.");
        }
        if(m.Table=="TuyenDung"){
            Require(Money(v["LuongThuViec"])>0,"Lương thử việc phải dương.");
            HistoryUnlocked(c,t,Convert.ToDateTime(v["TuNgay"]));
            Require(!Exists(c,t,"SELECT COUNT(*) FROM HopDong WHERE NhanVienId=@p0 AND TrangThai=N'Đã ký'",v["NhanVienId"]),"Nhân viên đã ký hợp đồng; không sửa tuyển dụng.");
            if(id>0)Require(!Exists(c,t,"SELECT COUNT(*) FROM TuyenDung WHERE Id=@p0 AND KetQua<>N'Chưa đánh giá'",id),"Quyết định đã đánh giá không thể sửa.");
        }
        if(m.Table=="HopDong"){
            Require(Exists(c,t,"SELECT COUNT(*) FROM TuyenDung WHERE NhanVienId=@p0 AND KetQua=N'Đạt' AND DenNgay<@p1",v["NhanVienId"],v["TuNgay"]),"Cần đạt thử việc; hợp đồng bắt đầu sau ngày kết thúc thử việc.");
            Require(Convert.ToDateTime(v["NgayKy"])<=Convert.ToDateTime(v["TuNgay"]),"Ngày ký không sau ngày hiệu lực.");
            if((string)v["Loai"]! == "Ngắn hạn")Require(v["DenNgay"]!=null,"Hợp đồng ngắn hạn cần ngày kết thúc.");
            if(id>0)Require(!Exists(c,t,"SELECT COUNT(*) FROM HopDong WHERE Id=@p0 AND TrangThai=N'Đã ký'",id),"Hợp đồng đã ký không thể sửa.");
        }
        if(m.Table is "QDLuong" or "QDChucVu"){
            HistoryUnlocked(c,t,Convert.ToDateTime(v["NgayHieuLuc"]));
            if(m.Table=="QDLuong")Require(Money(v["LuongCoBan"])>0,"Lương cơ bản phải dương.");
        }
        if(m.Table=="BaoHiem"){
            HistoryUnlocked(c,t,Convert.ToDateTime(v["TuNgay"]));
            Require(Exists(c,t,"SELECT COUNT(*) FROM HopDong WHERE NhanVienId=@p0 AND Loai=N'Dài hạn' AND TrangThai=N'Đã ký' AND TuNgay<=@p1 AND (DenNgay IS NULL OR DenNgay>=@p1)",v["NhanVienId"],v["TuNgay"]),"Bảo hiểm phải thuộc hợp đồng dài hạn đã ký, đang có hiệu lực.");
            Require(!Exists(c,t,"SELECT COUNT(*) FROM BaoHiem WHERE Id<>@p0 AND NhanVienId=@p1 AND Loai=@p2 AND TuNgay<=COALESCE(@p3,CONVERT(date,'21001231')) AND COALESCE(DenNgay,CONVERT(date,'21001231'))>=@p4",id,v["NhanVienId"],v["Loai"],v["DenNgay"],v["TuNgay"]),"Trùng thời gian bảo hiểm cùng loại.");
        }
        if(m.Table=="PhanCong"){
            var day=Convert.ToDateTime(v["NgayLam"]);Unlocked(c,t,day);
            Require(Exists(c,t,"SELECT COUNT(*) FROM NhanVien WHERE Id=@p0 AND TrangThai NOT IN(N'Không đạt',N'Chờ thử việc')",v["NhanVienId"]),"Nhân viên chưa có quyết định thử việc hoặc không đạt.");
            var shifts=Db.Query(c,t,"SELECT GioBatDau,GioKetThuc FROM CaLam WHERE Id=@p0",v["CaLamId"]);
            var start=day+(TimeSpan)shifts.Rows[0][0];var stop=day+(TimeSpan)shifts.Rows[0][1];if(stop<=start)stop=stop.AddDays(1);
            var prior=Db.Query(c,t,"SELECT p.NgayLam,c.GioBatDau,c.GioKetThuc FROM PhanCong p JOIN CaLam c ON c.Id=p.CaLamId WHERE p.NhanVienId=@p0 AND p.Id<>@p1 AND p.NgayLam BETWEEN DATEADD(day,-1,@p2) AND DATEADD(day,1,@p2)",v["NhanVienId"],id,day);
            foreach(DataRow row in prior.Rows){var a=(DateTime)row[0]+(TimeSpan)row[1];var b=(DateTime)row[0]+(TimeSpan)row[2];if(b<=a)b=b.AddDays(1);Require(stop<=a||start>=b,"Ca phân công bị chồng thời gian.");}
            if(id>0)Require(!Exists(c,t,"SELECT COUNT(*) FROM ChamCong WHERE PhanCongId=@p0",id),"Phân công đã chấm công không thể sửa.");
        }
        if(m.Table=="ChamCong"){
            var r=Db.Query(c,t,"SELECT p.NgayLam,p.NhanVienId,c.GioBatDau,c.GioKetThuc FROM PhanCong p JOIN CaLam c ON c.Id=p.CaLamId WHERE p.Id=@p0",v["PhanCongId"]).Rows[0];
            Unlocked(c,t,(DateTime)r[0]);var dur=(TimeSpan)r[3]-(TimeSpan)r[2];if(dur.TotalHours<0)dur+=TimeSpan.FromDays(1);
            Require(Money(v["SoGio"])<=(decimal)dur.TotalHours,"Giờ công vượt độ dài ca.");CheckDaily(c,t,Number(r[1]),(DateTime)r[0],Money(v["SoGio"]),id,false);
        }
        if(m.Table=="PhieuPhuTroi"){
            Unlocked(c,t,Convert.ToDateTime(v["Ngay"]));Require(Money(v["SoGio"])>0,"Số giờ phải dương.");
            if(id>0)Require(!Exists(c,t,"SELECT COUNT(*) FROM ChiTietPhuTroi WHERE PhieuId=@p0",id),"Phiếu đã có nhân viên; không thể sửa thông tin đầu phiếu.");
        }
        if(m.Table=="ChiTietPhuTroi"){
            var r=Db.Query(c,t,"SELECT Ngay,SoGio FROM PhieuPhuTroi WHERE Id=@p0",v["PhieuId"]).Rows[0];Unlocked(c,t,(DateTime)r[0]);
            Require(Money(v["SoGio"])>0 && Money(v["SoGio"])<=Money(r[1]),"Giờ tham gia phải dương và không vượt số giờ của phiếu.");CheckDaily(c,t,Number(v["NhanVienId"]),(DateTime)r[0],Money(v["SoGio"]),id,true);
            Require(Exists(c,t,"SELECT COUNT(*) FROM TuyenDung WHERE NhanVienId=@p0 AND TuNgay<=@p1",v["NhanVienId"],r[0]),"Nhân viên chưa được tiếp nhận vào ngày phụ trội.");
        }
        if(m.Table=="ThuongPhat"){
            Require((v["NhanVienId"]==null) != (v["PhongBanId"]==null),"Chọn đúng một đối tượng: cá nhân hoặc tập thể.");
            Require(v["PhongBanId"]==null || (string)v["PhuongThuc"]! == "Trực tiếp","Khen thưởng/kỷ luật tập thể được thu chi trực tiếp; không tự chia đều vào lương.");
            if((string)v["PhuongThuc"]! == "Qua lương")Unlocked(c,t,Convert.ToDateTime(v["Ngay"]));
            if(id>0)Require(!Exists(c,t,"SELECT COUNT(*) FROM ThuongPhat WHERE Id=@p0 AND TrangThai=N'Đã ký'",id),"Quyết định đã ký không thể sửa.");
        }
    }
    static void CheckDaily(SqlConnection c,SqlTransaction t,int nv,DateTime day,decimal hours,int id,bool ot)
    {
        var normal=Money(Db.Scalar(c,t,"SELECT COALESCE(SUM(cc.SoGio),0) FROM ChamCong cc JOIN PhanCong p ON p.Id=cc.PhanCongId WHERE p.NhanVienId=@p0 AND p.NgayLam=@p1 AND cc.Id<>@p2",nv,day,ot?-1:id));
        var extra=Money(Db.Scalar(c,t,"SELECT COALESCE(SUM(ct.SoGio),0) FROM ChiTietPhuTroi ct JOIN PhieuPhuTroi p ON p.Id=ct.PhieuId WHERE ct.NhanVienId=@p0 AND p.Ngay=@p1 AND ct.Id<>@p2",nv,day,ot?id:-1));
        Require(normal+extra+hours<=24,"Tổng giờ công và phụ trội trong ngày vượt 24 giờ.");
    }
    public static int Save(Module m,int id,Dictionary<string,object?> v)=>Db.Transaction((c,t)=>{
        Require(m.Editable,"Không sửa trực tiếp chứng từ hệ thống.");
        if(id>0){var old=Db.Query(c,t,$"SELECT * FROM [{m.Table}] WHERE Id=@p0",id);Require(old.Rows.Count==1,"Bản ghi đã bị xóa.");Validate(c,t,m,id,m.Fields.ToDictionary(f=>f.Name,f=>old.Rows[0][f.Name]==DBNull.Value?null:old.Rows[0][f.Name]));
            if(v.ContainsKey("NhanVienId"))Require(Equals(v["NhanVienId"],old.Rows[0]["NhanVienId"]),"Không đổi nhân viên trên chứng từ; lập bản ghi mới.");}
        Validate(c,t,m,id,v);
        var values=m.Fields.Select(f=>v[f.Name]).ToList();string sql;
        if(id==0)sql=$"INSERT [{m.Table}] ({string.Join(",",m.Fields.Select(f=>'['+f.Name+']'))}) OUTPUT INSERTED.Id VALUES ({string.Join(",",m.Fields.Select((f,i)=>"@p"+i))})";
        else{sql=$"UPDATE [{m.Table}] SET {string.Join(",",m.Fields.Select((f,i)=>$"[{f.Name}]=@p{i}"))} WHERE Id=@p{values.Count}; SELECT @p{values.Count}";values.Add(id);}
        int result=Number(Db.Scalar(c,t,sql,values.ToArray()));
        if(m.Table=="TuyenDung")Db.Exec(c,t,"UPDATE NhanVien SET TrangThai=N'Đang thử việc' WHERE Id=@p0",v["NhanVienId"]);
        return result;
    });
    public static void Delete(Module m,int id)=>Db.Transaction((c,t)=>{
        Require(m.Editable,"Không xóa chứng từ hệ thống.");var rows=Db.Query(c,t,$"SELECT * FROM [{m.Table}] WHERE Id=@p0",id);Require(rows.Rows.Count==1,"Không tìm thấy bản ghi.");
        var row=rows.Rows[0];Validate(c,t,m,id,m.Fields.ToDictionary(f=>f.Name,f=>row[f.Name]==DBNull.Value?null:row[f.Name]));
        Db.Exec(c,t,$"DELETE FROM [{m.Table}] WHERE Id=@p0",id);
        if(m.Table=="TuyenDung")Db.Exec(c,t,"UPDATE NhanVien SET TrangThai=N'Chờ thử việc' WHERE Id=@p0",row["NhanVienId"]);
        return true;
    });
    public static void Evaluate(int id,bool pass)=>Db.Transaction((c,t)=>{
        var rows=Db.Query(c,t,"SELECT * FROM TuyenDung WHERE Id=@p0",id);Require(rows.Rows.Count==1,"Chọn quyết định tuyển dụng.");var r=rows.Rows[0];
        Require((string)r["KetQua"]=="Chưa đánh giá","Đã ghi nhận kết quả thử việc.");Require((DateTime)r["DenNgay"]<=DateTime.Today,"Chưa kết thúc thời gian thử việc.");
        Db.Exec(c,t,"UPDATE TuyenDung SET KetQua=@p1 WHERE Id=@p0; UPDATE NhanVien SET TrangThai=@p3 WHERE Id=@p2",id,pass?"Đạt":"Không đạt",r["NhanVienId"],pass?"Đạt thử việc":"Không đạt");return true;
    });
    public static void SignContract(int id)=>Db.Transaction((c,t)=>{
        var rows=Db.Query(c,t,"SELECT * FROM HopDong WHERE Id=@p0",id);Require(rows.Rows.Count==1,"Chọn hợp đồng.");var r=rows.Rows[0];
        Require((string)r["TrangThai"]=="Chờ ký","Hợp đồng đã ký.");Require((DateTime)r["NgayKy"]<=DateTime.Today,"Chưa đến ngày ký.");
        Require(Exists(c,t,"SELECT COUNT(*) FROM TuyenDung WHERE NhanVienId=@p0 AND KetQua=N'Đạt'",r["NhanVienId"]),"Nhân viên chưa đạt thử việc.");
        Require(!Exists(c,t,"SELECT COUNT(*) FROM HopDong WHERE NhanVienId=@p0 AND Id<>@p1 AND TrangThai=N'Đã ký' AND TuNgay<=COALESCE(@p2,CONVERT(date,'21001231')) AND COALESCE(DenNgay,CONVERT(date,'21001231'))>=@p3",r["NhanVienId"],id,r["DenNgay"]==DBNull.Value?null:r["DenNgay"],r["TuNgay"]),"Thời gian hợp đồng trùng hợp đồng đã ký.");
        Db.Exec(c,t,"UPDATE HopDong SET TrangThai=N'Đã ký' WHERE Id=@p0; UPDATE NhanVien SET TrangThai=N'Chính thức' WHERE Id=@p1",id,r["NhanVienId"]);return true;
    });
    public static void Approve(int id)=>Db.Transaction((c,t)=>{
        var rows=Db.Query(c,t,"SELECT * FROM ThuongPhat WHERE Id=@p0",id);Require(rows.Rows.Count==1,"Chọn quyết định.");var r=rows.Rows[0];Require((string)r["TrangThai"]=="Chờ ký","Quyết định đã ký.");
        Require((DateTime)r["Ngay"]<=DateTime.Today,"Chưa đến ngày quyết định.");if((string)r["PhuongThuc"]=="Qua lương")Unlocked(c,t,(DateTime)r["Ngay"]);
        Db.Exec(c,t,"UPDATE ThuongPhat SET TrangThai=N'Đã ký' WHERE Id=@p0",id);return true;
    });
    public static void PayDirect(int id)=>Db.Transaction((c,t)=>{
        var rows=Db.Query(c,t,"SELECT * FROM ThuongPhat WHERE Id=@p0",id);Require(rows.Rows.Count==1,"Chọn quyết định.");var r=rows.Rows[0];
        Require((string)r["TrangThai"]=="Đã ký" && (string)r["PhuongThuc"]=="Trực tiếp","Cần quyết định đã ký, phương thức trực tiếp.");Require(Money(r["SoTien"])>0,"Quyết định không phát sinh tiền.");
        Db.Exec(c,t,"INSERT ThuChi(QuyetDinhId,Ngay,SoTien,Loai) VALUES(@p0,@p1,@p2,@p3)",id,DateTime.Today,r["SoTien"],(string)r["Loai"]=="Khen thưởng"?"Chi":"Thu");return true;
    });
    public static DataTable History(int nv)=>Db.Query("SELECT N'Tuyển dụng' Loai,SoQD SoChungTu,TuNgay Ngay,DieuKhoan NoiDung FROM TuyenDung WHERE NhanVienId=@p0 UNION ALL SELECT N'Hợp đồng',SoHD,TuNgay,DieuKhoan FROM HopDong WHERE NhanVienId=@p0 UNION ALL SELECT N'Lương',SoQD,NgayHieuLuc,NoiDung FROM QDLuong WHERE NhanVienId=@p0 UNION ALL SELECT N'Chức vụ',SoQD,NgayHieuLuc,NoiDung FROM QDChucVu WHERE NhanVienId=@p0 UNION ALL SELECT Loai,SoQD,Ngay,LyDo FROM ThuongPhat WHERE NhanVienId=@p0 ORDER BY Ngay DESC",nv);
    public static DataTable Details(int id)=>Db.Query("SELECT n.HoTen,c.* FROM ChiTietLuong c JOIN NhanVien n ON n.Id=c.NhanVienId WHERE c.BangLuongId=@p0 ORDER BY n.HoTen",id);
}
