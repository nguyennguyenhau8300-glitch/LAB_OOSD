using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;
namespace QuanLyHoSoBenhAn.Services;

public class BenhAnService
{
    public DataTable HoSo(string q = "") => Db.Query("SELECT h.MaHS,b.* FROM HoSoBenhAn h JOIN BenhNhan b ON h.MaBN=b.MaBN WHERE b.HoTen LIKE @q OR b.DienThoai LIKE @q OR CONVERT(varchar,h.MaHS)=@id ORDER BY h.MaHS DESC", Db.P("@q", "%" + q + "%"), Db.P("@id", q));
    public DataTable BacSi() => Db.Query("SELECT MaBS,HoTen FROM BacSi");
    public DataTable Thuoc() => Db.Query("SELECT * FROM Thuoc");
    public DataTable Kham(int hs) => Db.Query("SELECT p.MaPK,p.NgayKham,p.LoaiKham,b.HoTen AS BacSi,k.TenKhoa,p.TrangThai,p.TrieuChung,p.ChanDoan,p.TienKham,p.MaHS,p.MaBS,p.MaKhoa,p.MaHen FROM PhieuKham p JOIN BacSi b ON p.MaBS=b.MaBS JOIN Khoa k ON p.MaKhoa=k.MaKhoa WHERE p.MaHS=@h ORDER BY p.NgayKham DESC,p.MaPK DESC", Db.P("@h", hs));
    public DataTable XN(int pk) => Db.Query("SELECT * FROM XetNghiem WHERE MaPK=@p", Db.P("@p", pk));
    public DataTable Toa(int pk) => Db.Query("SELECT c.MaThuoc,t.TenThuoc,c.SoLuong,c.LieuDung FROM ToaThuoc a JOIN ChiTietToa c ON a.MaToa=c.MaToa JOIN Thuoc t ON c.MaThuoc=t.MaThuoc WHERE a.MaPK=@p", Db.P("@p", pk));
    public DataTable Hen(int hs) => Db.Query("SELECT * FROM LichHen WHERE MaHS=@h ORDER BY ThoiGian DESC", Db.P("@h", hs));
    public DataTable Phi(int hs) => Db.Query("SELECT * FROM LePhiNam WHERE MaHS=@h ORDER BY TuNgay DESC", Db.P("@h", hs));
    static void Can(bool ok, string msg) { if (!ok) throw new InvalidOperationException(msg); }
    static object Run(SqlConnection c, SqlTransaction t, string sql, params SqlParameter[] ps) { using var m = new SqlCommand(sql, c, t); m.Parameters.AddRange(ps); return m.ExecuteScalar(); }
    static KetQuaXuLy Tx(Action<SqlConnection, SqlTransaction> a) { try { using var c = Db.OpenConnection(); using var t = c.BeginTransaction(IsolationLevel.Serializable); a(c, t); t.Commit(); return KetQuaXuLy.Ok("Đã lưu thành công."); } catch (Exception e) { return KetQuaXuLy.Fail(e.Message); } }
    public KetQuaXuLy DangKy(string ten, DateTime ns, string dc, string dt, byte[] anh, decimal phi) => Tx((c, t) =>
    {
        Can(!string.IsNullOrWhiteSpace(ten) && !string.IsNullOrWhiteSpace(dc), "Nhập họ tên và địa chỉ."); Can(ns.Date <= DateTime.Today, "Ngày sinh không được ở tương lai."); Can(System.Text.RegularExpressions.Regex.IsMatch(dt, @"^\+?[0-9]{9,15}$"), "Điện thoại cần 9 đến 15 chữ số."); Can(phi > 0, "Lệ phí phải lớn hơn 0.");
        var bn = Run(c, t, "INSERT BenhNhan(HoTen,NgaySinh,DiaChi,DienThoai,Anh) OUTPUT INSERTED.MaBN VALUES(@n,@s,@d,@p,@a)", Db.P("@n", ten), Db.P("@s", ns.Date), Db.P("@d", dc), Db.P("@p", dt), new SqlParameter("@a", SqlDbType.VarBinary, -1) { Value = (object)anh ?? DBNull.Value });
        var hs = Run(c, t, "INSERT HoSoBenhAn(MaBN) OUTPUT INSERTED.MaHS VALUES(@b)", Db.P("@b", bn));
        Run(c, t, "INSERT LePhiNam(MaHS,TuNgay,DenNgay,SoTien) VALUES(@h,@s,@e,@v)", Db.P("@h", hs), Db.P("@s", DateTime.Today), Db.P("@e", DateTime.Today.AddYears(1)), Db.P("@v", phi));
    });
    public KetQuaXuLy GiaHan(int hs, decimal phi) => Tx((c, t) => { Can(phi > 0, "Lệ phí phải dương."); var end = Run(c, t, "SELECT MAX(DenNgay) FROM LePhiNam WHERE MaHS=@h", Db.P("@h", hs)); var start = end == DBNull.Value ? DateTime.Today : (DateTime)end; if (start < DateTime.Today) start = DateTime.Today; Run(c, t, "INSERT LePhiNam(MaHS,TuNgay,DenNgay,SoTien) VALUES(@h,@s,@e,@v)", Db.P("@h", hs), Db.P("@s", start), Db.P("@e", start.AddYears(1)), Db.P("@v", phi)); });
    public KetQuaXuLy DatHen(int hs, int bs, DateTime at) => Tx((c, t) =>
    {
        at = new DateTime(at.Year, at.Month, at.Day, at.Hour, at.Minute, 0); Can(at > DateTime.Now, "Lịch hẹn phải ở tương lai.");
        Can(Convert.ToInt32(Run(c, t, "SELECT COUNT(*) FROM LichHen WHERE MaBS=@b AND TrangThai=N'Đã hẹn' AND ThoiGian>=@d AND ThoiGian<DATEADD(minute,1,@d)", Db.P("@b", bs), Db.P("@d", at))) == 0, "Bác sĩ đã có lịch hẹn trong phút này.");
        Run(c, t, "INSERT LichHen(MaHS,MaBS,ThoiGian) VALUES(@h,@b,@d)", Db.P("@h", hs), Db.P("@b", bs), Db.P("@d", at));
    });
    public KetQuaXuLy HuyHen(int id) => Tx((c, t) => { var n = Run(c, t, "UPDATE LichHen SET TrangThai=N'Đã hủy' WHERE MaHen=@i AND TrangThai=N'Đã hẹn';SELECT @@ROWCOUNT", Db.P("@i", id)); Can(Convert.ToInt32(n) == 1, "Chỉ hủy lịch đang hẹn."); });
    public KetQuaXuLy LapKham(int hs, int bs, bool dinhKy, string tc, decimal tien, int? hen = null) => Tx((c, t) =>
    {
        Can(!string.IsNullOrWhiteSpace(tc), "Nhập triệu chứng hoặc ghi nhận sức khỏe."); Can(tien >= 0, "Tiền khám không âm.");
        if (dinhKy) { Can(Convert.ToInt32(Run(c, t, "SELECT COUNT(*) FROM LePhiNam WHERE MaHS=@h AND TuNgay<=CAST(GETDATE() AS date) AND DenNgay>CAST(GETDATE() AS date)", Db.P("@h", hs))) > 0, "Cần lệ phí còn hiệu lực để khám định kỳ miễn phí."); Can(Convert.ToInt32(Run(c, t, "SELECT COUNT(*) FROM PhieuKham WHERE MaHS=@h AND LoaiKham=N'Định kỳ' AND YEAR(NgayKham)=YEAR(GETDATE()) AND DATEPART(quarter,NgayKham)=DATEPART(quarter,GETDATE())", Db.P("@h", hs))) == 0, "Đã có lần khám định kỳ trong quý này."); tien = 0; }
        if (hen.HasValue) { var n = Run(c, t, "UPDATE LichHen SET TrangThai=N'Đã khám' WHERE MaHen=@i AND MaHS=@h AND MaBS=@b AND TrangThai=N'Đã hẹn' AND CAST(ThoiGian AS date)<=CAST(GETDATE() AS date);SELECT @@ROWCOUNT", Db.P("@i", hen), Db.P("@h", hs), Db.P("@b", bs)); Can(Convert.ToInt32(n) == 1, "Lịch hẹn không thuộc hồ sơ/bác sĩ, đã xử lý hoặc chưa đến ngày khám."); }
        var khoa = Run(c, t, "SELECT MaKhoa FROM BacSi WHERE MaBS=@b", Db.P("@b", bs)); Can(khoa != null, "Chọn bác sĩ hợp lệ.");
        Run(c, t, "INSERT PhieuKham(MaHS,MaBS,MaKhoa,MaHen,NgayKham,LoaiKham,TrieuChung,TienKham) VALUES(@h,@b,@k,@a,SYSDATETIME(),@l,@x,@v)", Db.P("@k", khoa), Db.P("@h", hs), Db.P("@b", bs), Db.P("@a", hen), Db.P("@l", dinhKy ? "Định kỳ" : "Điều trị"), Db.P("@x", tc), Db.P("@v", tien));
    });
    static void Mo(SqlConnection c, SqlTransaction t, int pk) { var s = Run(c, t, "SELECT TrangThai FROM PhieuKham WHERE MaPK=@p", Db.P("@p", pk)); Can(s != null, "Chọn phiếu khám hợp lệ."); Can(Convert.ToString(s) == "Đang khám", "Phiếu đã kết luận hoặc thanh toán, không được sửa."); }
    public KetQuaXuLy ChiDinh(int pk, string ten, decimal phi) => Tx((c, t) => { Mo(c, t, pk); Can(!string.IsNullOrWhiteSpace(ten) && phi >= 0, "Nhập tên xét nghiệm và chi phí không âm."); Run(c, t, "INSERT XetNghiem(MaPK,TenXN,ChiPhi) VALUES(@p,@n,@v)", Db.P("@p", pk), Db.P("@n", ten), Db.P("@v", phi)); });
    public KetQuaXuLy KetQua(int xn, string kq) => Tx((c, t) => { Can(!string.IsNullOrWhiteSpace(kq), "Nhập kết quả xét nghiệm."); var pk = Run(c, t, "SELECT MaPK FROM XetNghiem WHERE MaXN=@x", Db.P("@x", xn)); Can(pk != null, "Chọn xét nghiệm."); Mo(c, t, Convert.ToInt32(pk)); Run(c, t, "UPDATE XetNghiem SET KetQua=@k WHERE MaXN=@x", Db.P("@k", kq), Db.P("@x", xn)); });
    public KetQuaXuLy LapToa(int pk, string cd, List<DongThuoc> ds) => Tx((c, t) =>
    {
        Mo(c, t, pk); Can(!string.IsNullOrWhiteSpace(cd), "Nhập chẩn đoán/kết luận sức khỏe."); Can(ds != null, "Danh sách thuốc không hợp lệ."); Can(ds.All(x => x.SoLuong > 0 && !string.IsNullOrWhiteSpace(x.LieuDung)), "Số lượng phải dương và có liều dùng."); Can(ds.Select(x => x.MaThuoc).Distinct().Count() == ds.Count, "Mỗi thuốc chỉ có một dòng."); Can(Convert.ToInt32(Run(c, t, "SELECT COUNT(*) FROM XetNghiem WHERE MaPK=@p AND NULLIF(LTRIM(RTRIM(KetQua)),N'') IS NULL", Db.P("@p", pk))) == 0, "Chờ đủ kết quả xét nghiệm trước khi kết luận.");
        if (ds.Count > 0) { var toa = Run(c, t, "INSERT ToaThuoc(MaPK) OUTPUT INSERTED.MaToa VALUES(@p)", Db.P("@p", pk)); foreach (var x in ds) Run(c, t, "INSERT ChiTietToa VALUES(@a,@m,@s,@l)", Db.P("@a", toa), Db.P("@m", x.MaThuoc), Db.P("@s", x.SoLuong), Db.P("@l", x.LieuDung)); }
        Run(c, t, "UPDATE PhieuKham SET ChanDoan=@c,TrangThai=N'Chờ thanh toán' WHERE MaPK=@p", Db.P("@c", cd), Db.P("@p", pk));
    });
    public KetQuaXuLy ThanhToan(int pk) => Tx((c, t) => { Can(Convert.ToString(Run(c, t, "SELECT TrangThai FROM PhieuKham WHERE MaPK=@p", Db.P("@p", pk))) == "Chờ thanh toán", "Phải hoàn tất khám/kê toa trước thanh toán."); Run(c, t, "INSERT ThanhToan(MaPK,TongTien) SELECT MaPK,TienKham+COALESCE((SELECT SUM(ChiPhi) FROM XetNghiem WHERE MaPK=@p),0) FROM PhieuKham WHERE MaPK=@p;UPDATE PhieuKham SET TrangThai=N'Đã thanh toán' WHERE MaPK=@p", Db.P("@p", pk)); });
    public string XuatToa(int pk)
    {
        var head = Db.Query("SELECT a.MaToa,a.NgayLap,p.MaPK,p.ChanDoan,h.MaHS,b.HoTen,b.NgaySinh,bs.HoTen AS BacSi,k.TenKhoa FROM ToaThuoc a JOIN PhieuKham p ON a.MaPK=p.MaPK JOIN HoSoBenhAn h ON p.MaHS=h.MaHS JOIN BenhNhan b ON h.MaBN=b.MaBN JOIN BacSi bs ON p.MaBS=bs.MaBS JOIN Khoa k ON p.MaKhoa=k.MaKhoa WHERE p.MaPK=@p", Db.P("@p", pk));
        var details = Toa(pk); Can(head.Rows.Count == 1 && details.Rows.Count > 0, "Phiếu khám chưa có toa thuốc đã lưu.");
        var r = head.Rows[0];
        var lines = new List<string> { "TOA THUỐC MÔ PHỎNG LAB6 - KHÔNG DÙNG ĐIỀU TRỊ", $"Mã toa: {r["MaToa"]}; Phiếu khám: {pk}; Hồ sơ: {r["MaHS"]}", $"Bệnh nhân: {r["HoTen"]}; Ngày sinh: {r["NgaySinh"]:dd/MM/yyyy}", $"Bác sĩ: {r["BacSi"]}; Khoa: {r["TenKhoa"]}", $"Ngày lập: {r["NgayLap"]:dd/MM/yyyy HH:mm}", $"Kết luận: {r["ChanDoan"]}", "THUỐC - SỐ LƯỢNG - LIỀU DÙNG" };
        lines.AddRange(details.Rows.Cast<DataRow>().Select(x => $"{x["TenThuoc"]} - {x["SoLuong"]} - {x["LieuDung"]}"));
        return string.Join(Environment.NewLine, lines);
    }
    public DataTable ThongKe() => Db.Query("SELECT h.MaHS,b.HoTen,COUNT(p.MaPK) SoLanKham,COALESCE(SUM(t.TongTien),0) DaThanhToan FROM HoSoBenhAn h JOIN BenhNhan b ON h.MaBN=b.MaBN LEFT JOIN PhieuKham p ON h.MaHS=p.MaHS LEFT JOIN ThanhToan t ON p.MaPK=t.MaPK GROUP BY h.MaHS,b.HoTen");
}
