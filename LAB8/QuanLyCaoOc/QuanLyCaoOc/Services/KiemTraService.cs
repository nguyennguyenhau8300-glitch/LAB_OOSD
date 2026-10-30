using QuanLyCaoOc.Data;
using static QuanLyCaoOc.Services.Rules;

namespace QuanLyCaoOc.Services;

public sealed class KiemTraService
{
    public string LapBienBan(DateTime date,string person,string note,IReadOnlyList<KiemTraPhong> rooms)=>Db.Transaction((c,tx)=>
    {
        Past(date);Text(person,"người kiểm tra");
        Require(rooms.Count>0&&rooms.Select(r=>r.MaPhong).Distinct().Count()==rooms.Count,"Chọn phòng kiểm tra, không lặp phòng.");
        string id=Id("KT");Db.Exec(c,tx,"INSERT PhieuKiemTra VALUES(@p0,@p1,@p2,@p3)",id,date.Date,person,note);
        foreach(var room in rooms)
        {
            Text(room.TinhTrang,"hiện trạng");
            var available=Db.Scalar(c,tx,"SELECT NgaySuDung FROM VanPhong WHERE MaPhong=@p0",room.MaPhong);
            Require(available is DateTime start&&start<=date.Date,"Phòng chưa đưa vào sử dụng tại ngày kiểm tra.");
            Db.Exec(c,tx,"INSERT ChiTietKiemTra(MaPKT,MaPhong,TinhTrang,ThayDoiKetCau,DeXuat) VALUES(@p0,@p1,@p2,@p3,@p4)",id,room.MaPhong,room.TinhTrang,room.ThayDoiKetCau,room.DeXuat);
        }
        return id;
    });
    public string LapPhat(long check,long detail,string reason,decimal money,DateTime due)=>Db.Transaction((c,tx)=>
    {
        Text(reason,"lý do và căn cứ phạt");Require(money>0,"Tiền phạt phải dương.");
        Precision(money);
        var row=Store.One(Db.Query(c,tx,"SELECT d.*,k.NgayKiemTra FROM ChiTietKiemTra d JOIN PhieuKiemTra k ON k.MaPKT=d.MaPKT WHERE d.MaCTKT=@p0",check),"Không có chi tiết kiểm tra.");
        Require((bool)row["ThayDoiKetCau"],"Biên bản chưa ghi nhận thay đổi kết cấu.");
        var ct=Store.One(Db.Query(c,tx,"SELECT ct.*,h.NgayBatDau,h.TrangThai FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE ct.MaCTHD=@p0",detail),"Không có chi tiết hợp đồng.");
        var date=(DateTime)row["NgayKiemTra"];
        Require((string)ct["MaPhong"]==(string)row["MaPhong"] && (string)ct["TrangThai"] is "HIEU_LUC" or "KET_THUC" && (DateTime)ct["NgayBatDau"]<=date && date<(DateTime)ct["NgayKetThuc"],"Chi tiết thuê không đúng phòng hoặc bên thuê tại ngày vi phạm.");
        Require(due.Date>=date,"Hạn nộp không được trước ngày kiểm tra.");
        string id=Id("PH");Db.Exec(c,tx,"INSERT PhieuPhat VALUES(@p0,@p1,@p2,@p3,@p4,@p5,0)",id,check,detail,reason,money,due.Date);return id;
    });
    public void NopPhat(string id,decimal money)=>Db.Transaction((c,tx)=>
    {
        Require(money>0,"Tiền nộp phải dương.");
        Precision(money);
        Require(Db.Exec(c,tx,"UPDATE PhieuPhat SET DaNop=DaNop+@p1 WHERE MaPhat=@p0 AND DaNop+@p1<=SoTien",id,money)==1,"Không có phiếu hoặc số tiền vượt còn phải nộp.");return true;
    });
}
