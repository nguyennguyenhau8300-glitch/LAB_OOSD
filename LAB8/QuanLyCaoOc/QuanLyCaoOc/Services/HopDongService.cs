using QuanLyCaoOc.Data;
using static QuanLyCaoOc.Services.Rules;

namespace QuanLyCaoOc.Services;

public sealed class HopDongService
{
    public string Ky(HopDongRequest input) => Db.Transaction((c, tx) =>
    {
        var old = Db.Scalar(c, tx, "SELECT MaHD FROM HopDong WHERE RequestId=@p0", input.RequestId);
        if (old is string id) return id;
        Require(input.SoThang is >= 6 and <= 600, "Thời hạn gốc từ 6 đến 600 tháng.");
        Past(input.NgayKy); Require(input.NgayKy.Date <= input.BatDau.Date, "Ngày ký phải trước hoặc bằng ngày bắt đầu.");
        Require(input.Phong.Count > 0 && input.Phong.Select(x => x.MaPhong).Distinct().Count() == input.Phong.Count, "Chọn ít nhất một phòng; không lặp phòng.");
        Require(input.Phong.All(p => p.Gia > 0), "Đơn giá thuê phải dương.");
        foreach(var line in input.Phong) Precision(line.Gia);
        Text(input.MaKH, "khách hàng"); Store.One(Db.Query(c, tx, "SELECT MaKH FROM KhachHang WHERE MaKH=@p0", input.MaKH), "Khách hàng không tồn tại.");
        var start = input.BatDau.Date; var end = start.AddMonths(input.SoThang);
        Store.LockRooms(c, tx, input.Phong.Select(x => x.MaPhong));
        foreach (var room in input.Phong)
        {
            var data = Store.One(Db.Query(c, tx, "SELECT * FROM VanPhong WHERE MaPhong=@p0", room.MaPhong), "Không có phòng.");
            Require(!(bool)data["NgungSuDung"] && (DateTime)data["NgaySuDung"] <= start, "Phòng ngừng sử dụng hoặc chưa đưa vào sử dụng.");
            Store.NoOverlap(c, tx, room.MaPhong, start, end);
        }
        string ma = Id("HD");
        Db.Exec(c, tx, "INSERT HopDong VALUES(@p0,@p1,@p2,@p3,@p4,'CHO_TRA_TRUOC',@p5)", ma, input.MaKH, input.NgayKy.Date, start, input.SoThang, input.RequestId);
        foreach (var room in input.Phong)
        {
            long detail = Convert.ToInt64(Db.Scalar(c, tx, "INSERT ChiTietHopDong(MaHD,MaPhong,DonGiaThang,NgayKetThuc) OUTPUT INSERTED.MaCTHD VALUES(@p0,@p1,@p2,@p3)", ma, room.MaPhong, room.Gia, end));
            Store.GenerateRent(c, tx, detail, start, 0, input.SoThang, room.Gia);
        }
        return ma;
    });

    public void Huy(string hd) => Db.Transaction((c, tx) =>
    {
        var row = Store.Contract(c, tx, hd);
        Require((string)row["TrangThai"] == "CHO_TRA_TRUOC", "Chỉ hủy hợp đồng đang chờ trả trước.");
        Require(Convert.ToInt32(Db.Scalar(c, tx, "SELECT COUNT(*) FROM PhanBoThu p JOIN KhoanPhaiThu k ON k.MaKhoan=p.MaKhoan JOIN ChiTietHopDong ct ON ct.MaCTHD=k.MaCTHD WHERE ct.MaHD=@p0", hd)) == 0, "Đã có tiền thu; cần đối soát hoàn tiền, không được hủy trực tiếp.");
        Db.Exec(c, tx, "UPDATE HopDong SET TrangThai='HUY' WHERE MaHD=@p0", hd); return true;
    });
    public void KetThuc(string hd, DateTime date) => Db.Transaction((c, tx) =>
    {
        Past(date); var h = Store.Contract(c, tx, hd); Require((string)h["TrangThai"] == "HIEU_LUC", "Hợp đồng chưa hiệu lực.");
        var details = Store.Details(c, tx, hd);
        foreach (System.Data.DataRow ct in details.Rows)
        {
            Require(date.Date >= (DateTime)ct["NgayKetThuc"], "Chưa hết thời hạn thuê.");
            Require(Convert.ToDecimal(Db.Scalar(c, tx, "SELECT COALESCE(SUM(ConNo),0) FROM vwCongNo WHERE MaCTHD=@p0", ct["MaCTHD"])) == 0, "Còn công nợ, chưa thể kết thúc.");
            Store.ServicesClear(c, tx, ct, date);
        }
        Db.Exec(c, tx, "UPDATE HopDong SET TrangThai='KET_THUC' WHERE MaHD=@p0", hd); return true;
    });
}
