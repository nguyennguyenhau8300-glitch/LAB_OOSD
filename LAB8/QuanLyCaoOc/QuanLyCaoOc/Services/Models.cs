namespace QuanLyCaoOc.Services;

public record PhongThue(string MaPhong, decimal Gia);
public record HopDongRequest(string MaKH, DateTime NgayKy, DateTime BatDau, int SoThang, IReadOnlyList<PhongThue> Phong, Guid RequestId);
public record PhanBo(long MaKhoan, decimal SoTien);
public record ThuTienRequest(string MaKH, DateTime NgayThu, decimal TongTien, string PhuongThuc, IReadOnlyList<PhanBo> PhanBo, Guid RequestId);
public record PhongGiaHan(long MaCTHD, int SoThang, decimal Gia);
public record GiaHanRequest(string MaHD, DateTime NgayLap, string NguoiLap, IReadOnlyList<PhongGiaHan> Phong, Guid RequestId);
public record DongDichVu(string MaDV, decimal SoLuong, decimal DonGia);
public record KiemTraPhong(string MaPhong, string TinhTrang, bool ThayDoiKetCau, string DeXuat);

public static class Rules
{
    public static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
    public static void Text(string value, string name) => Require(!string.IsNullOrWhiteSpace(value), "Vui lòng nhập " + name + ".");
    public static void Past(DateTime date) => Require(date.Date <= DateTime.Today, "Ngày ghi nhận không được ở tương lai.");
    public static void Precision(decimal value) => Require(value == decimal.Round(value,2), "Số tiền, đơn giá và số lượng chỉ nhận tối đa 2 chữ số thập phân.");
    public static DateTime Month(DateTime value) => new(value.Year, value.Month, 1);
    public static DateTime MonthEnd(DateTime value) => Month(value).AddMonths(1).AddDays(-1);
    public static string Id(string prefix) => prefix + Guid.NewGuid().ToString("N")[..16];
    public static int Months(DateTime origin, DateTime end) => (end.Year - origin.Year) * 12 + end.Month - origin.Month;
}
