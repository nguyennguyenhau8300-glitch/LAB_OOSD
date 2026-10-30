using QuanLyCaoOc.Data;
using static QuanLyCaoOc.Services.Rules;

namespace QuanLyCaoOc.Services;

public sealed class AnNinhService
{
    public void LuuNhanVien(string ma, string kh, string room, string name, string? image, string role, DateTime from, bool edit) => Db.Transaction((c,tx)=>
    {
        Text(ma,"mã nhân viên");Text(name,"họ tên");Text(role,"chức vụ");Past(from);
        Require(ma.Length<=20,"Mã nhân viên tối đa 20 ký tự.");
        Require(Convert.ToInt32(Db.Scalar(c,tx,"SELECT COUNT(*) FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE h.MaKH=@p0 AND ct.MaPhong=@p1 AND h.TrangThai IN('HIEU_LUC','KET_THUC') AND h.NgayBatDau<=@p2 AND @p2<ct.NgayKetThuc",kh,room,from.Date))>0,"Công ty không có quyền thuê phòng tại ngày bố trí.");
        if(edit)
        {
            var old=Store.One(Db.Query(c,tx,"SELECT * FROM NhanVienCongTy WHERE MaNV=@p0",ma),"Nhân viên không tồn tại.");
            Require(old["NgayNghi"] is DBNull,"Nhân viên đã nghỉ; không sửa hồ sơ công tác cũ.");
            Require((string)old["MaKH"]==kh,"Không chuyển công ty bằng cách sửa hồ sơ; hãy lập hồ sơ mới.");
            Require(Convert.ToInt32(Db.Scalar(c,tx,"SELECT COUNT(*) FROM TheGiuXe WHERE MaNV=@p0 AND NgayCap<@p1",ma,from.Date))==0,"Ngày vào không được sau lịch sử cấp thẻ.");
            Db.Exec(c,tx,"UPDATE NhanVienCongTy SET MaPhong=@p1,HoTen=@p2,Anh=@p3,ChucVu=@p4,NgayVao=@p5 WHERE MaNV=@p0",ma,room,name,image,role,from.Date);
        }
        else Db.Exec(c,tx,"INSERT NhanVienCongTy VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,NULL)",ma,kh,room,name,image,role,from.Date);
        return true;
    });
    public void NghiViec(string ma,DateTime date)=>Db.Transaction((c,tx)=>
    {
        Past(date);var row=Store.One(Db.Query(c,tx,"SELECT * FROM NhanVienCongTy WHERE MaNV=@p0",ma),"Không có nhân viên.");
        Require(row["NgayNghi"] is DBNull && date.Date>=(DateTime)row["NgayVao"],"Ngày nghỉ hoặc trạng thái không hợp lệ.");
        Require(Convert.ToInt32(Db.Scalar(c,tx,"SELECT COUNT(*) FROM TheGiuXe WHERE MaNV=@p0 AND NgayCap>@p1",ma,date.Date))==0,"Ngày nghỉ trước ngày cấp thẻ trong lịch sử.");
        Db.Exec(c,tx,"UPDATE NhanVienCongTy SET NgayNghi=@p1 WHERE MaNV=@p0",ma,date.Date);
        Db.Exec(c,tx,"UPDATE TheGiuXe SET NgayThuHoi=@p1 WHERE MaNV=@p0 AND NgayThuHoi IS NULL",ma,date.Date);return true;
    });
    public string CapThe(string card,string nv,string plate,string type,string color,DateTime date)=>Db.Transaction((c,tx)=>
    {
        Text(card,"mã thẻ");Text(plate,"biển số");Text(type,"loại xe");Text(color,"màu xe");Past(date);
        plate=new string(plate.Where(ch=>!char.IsWhiteSpace(ch)).ToArray()).ToUpperInvariant();
        Require(card.Length<=20&&plate.Length<=20,"Mã thẻ và biển số tối đa 20 ký tự.");
        var row=Store.One(Db.Query(c,tx,"SELECT * FROM NhanVienCongTy WHERE MaNV=@p0",nv),"Không có nhân viên.");
        Require(date.Date>=(DateTime)row["NgayVao"] && (row["NgayNghi"] is DBNull || date.Date<(DateTime)row["NgayNghi"]),"Nhân viên không làm việc tại ngày cấp.");
        Require(Convert.ToInt32(Db.Scalar(c,tx,"SELECT COUNT(*) FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE h.MaKH=@p0 AND ct.MaPhong=@p1 AND h.TrangThai='HIEU_LUC' AND h.NgayBatDau<=@p2 AND @p2<ct.NgayKetThuc",row["MaKH"],row["MaPhong"],date.Date))>0,"Phòng làm việc không còn hợp đồng hiệu lực.");
        var value=Db.Scalar(c,tx,"SELECT MaXe FROM Xe WHERE BienSo=@p0",plate);
        string xe=value is string old?old:Id("XE");
        Require(Convert.ToInt32(Db.Scalar(c,tx,"SELECT COUNT(*) FROM TheGiuXe WHERE MaXe=@p0 AND (NgayThuHoi IS NULL OR NgayThuHoi>@p1)",xe,date.Date))==0,"Xe đã có thẻ hiệu lực trong khoảng này.");
        if(value is not string)Db.Exec(c,tx,"INSERT Xe VALUES(@p0,@p1,@p2,@p3)",xe,plate,type,color);
        Db.Exec(c,tx,"INSERT TheGiuXe VALUES(@p0,@p1,@p2,@p3,NULL)",card,nv,xe,date.Date);return card;
    });
    public void ThuHoi(string card,DateTime date)=>Db.Transaction((c,tx)=>
    {
        Past(date);Require(Db.Exec(c,tx,"UPDATE TheGiuXe SET NgayThuHoi=@p1 WHERE MaThe=@p0 AND NgayThuHoi IS NULL AND NgayCap<=@p1",card,date.Date)==1,"Thẻ đã thu hồi hoặc ngày không hợp lệ.");return true;
    });
}
