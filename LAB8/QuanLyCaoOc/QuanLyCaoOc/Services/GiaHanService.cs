using System.Data;
using QuanLyCaoOc.Data;
using static QuanLyCaoOc.Services.Rules;

namespace QuanLyCaoOc.Services;

public sealed class GiaHanService
{
    public string Lap(GiaHanRequest input) => Db.Transaction((c, tx) =>
    {
        var previous = Db.Scalar(c, tx, "SELECT MaPGH FROM PhieuGiaHan WHERE RequestId=@p0", input.RequestId);
        if (previous is string existing) return existing;
        Past(input.NgayLap); Text(input.NguoiLap, "người lập");
        var hd = Store.Contract(c, tx, input.MaHD);
        Require((string)hd["TrangThai"] == "HIEU_LUC", "Chỉ gia hạn hợp đồng đã hiệu lực.");
        Require(input.NgayLap.Date >= (DateTime)hd["NgayBatDau"], "Chưa đến ngày bắt đầu thuê.");
        Require(input.Phong.Count > 0 && input.Phong.Select(p => p.MaCTHD).Distinct().Count() == input.Phong.Count, "Chọn phòng cần gia hạn, không trùng dòng.");
        var details = Store.Details(c, tx, input.MaHD);
        Store.LockRooms(c, tx, details.Rows.Cast<System.Data.DataRow>().Select(r => (string)r["MaPhong"]));
        var start = (DateTime)hd["NgayBatDau"];
        foreach (var line in input.Phong)
        {
            Precision(line.Gia);
            var ct = Store.One(details.AsEnumerable().Where(r => (long)r["MaCTHD"] == line.MaCTHD).CopyToDataTableSafe(), "Phòng không thuộc hợp đồng.");
            Require(line.SoThang > 0 && line.SoThang <= (int)hd["SoThangGoc"] / 2 && line.Gia > 0, "Số tháng gia hạn phải > 0 và ≤ 1/2 thời gian gốc; giá phải dương.");
            DateTime oldEnd = (DateTime)ct["NgayKetThuc"];
            Require(input.NgayLap.Date <= oldEnd, "Phòng đã quá hạn, cần lập đợt thuê mới.");
            decimal rentDebt = Convert.ToDecimal(Db.Scalar(c, tx, "SELECT COALESCE(SUM(ConNo),0) FROM vwCongNo WHERE MaCTHD=@p0 AND Loai='THUE' AND HanThu<=@p1", line.MaCTHD, input.NgayLap.Date));
            Require(rentDebt == 0, $"Phòng {ct["MaPhong"]} còn nợ thuê {rentDebt:N0} đồng.");
            Store.ServicesClear(c, tx, ct, input.NgayLap);
            int index = Months(start, oldEnd);
            Store.NoOverlap(c, tx, (string)ct["MaPhong"], oldEnd, start.AddMonths(index + line.SoThang), line.MaCTHD);
        }
        string ma = Id("GH");
        Db.Exec(c, tx, "INSERT PhieuGiaHan VALUES(@p0,@p1,@p2,@p3,@p4)", ma, input.MaHD, input.NgayLap.Date, input.NguoiLap, input.RequestId);
        foreach (var line in input.Phong)
        {
            var ct = details.AsEnumerable().Single(r => (long)r["MaCTHD"] == line.MaCTHD);
            var oldEnd = (DateTime)ct["NgayKetThuc"]; int first = Months(start, oldEnd); var newEnd = start.AddMonths(first + line.SoThang);
            Db.Exec(c, tx, "INSERT ChiTietGiaHan(MaPGH,MaCTHD,SoThang,HanCu,HanMoi,DonGiaThang) VALUES(@p0,@p1,@p2,@p3,@p4,@p5)", ma, line.MaCTHD, line.SoThang, oldEnd, newEnd, line.Gia);
            Store.GenerateRent(c, tx, line.MaCTHD, start, first, line.SoThang, line.Gia);
            Db.Exec(c, tx, "UPDATE ChiTietHopDong SET NgayKetThuc=@p1 WHERE MaCTHD=@p0", line.MaCTHD, newEnd);
        }
        return ma;
    });
}

internal static class RowExtensions
{
    public static System.Data.DataTable CopyToDataTableSafe(this IEnumerable<System.Data.DataRow> rows)
    {
        var list = rows.ToList(); return list.Count == 0 ? new System.Data.DataTable() : System.Data.DataTableExtensions.CopyToDataTable(list);
    }
}
