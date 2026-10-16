using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn;

internal static class Verification
{
    public static void Run()
    {
        var log = new List<string>();
        var logDirectory=Path.Combine(AppContext.BaseDirectory,"Verification"); Directory.CreateDirectory(logDirectory);
        var logFile=Path.Combine(logDirectory,"results.txt"); File.WriteAllText(logFile,"");
        void Check(bool b, string s) { log.Add((b ? "PASS " : "FAIL ") + s); File.WriteAllLines(logFile,log); if (!b) throw new Exception(s); }
        var service = new BenhAnService(); string tag = "LAB6_TEST_" + Guid.NewGuid().ToString("N")[..8];
        try
        {
        Check(!service.DangKy("", DateTime.Today, "X", "0901234567", null, 100).ThanhCong, "Từ chối tên rỗng");
        Check(!service.DangKy(tag, DateTime.Today.AddDays(1), "X", "0901234567", null, 100).ThanhCong, "Từ chối ngày sinh tương lai");
        Check(service.DangKy(tag, DateTime.Today.AddYears(-20), "Địa chỉ demo", "0901234567", null, 200000).ThanhCong, "Đăng ký hồ sơ và lệ phí cùng transaction");
        int h = Convert.ToInt32(service.HoSo(tag).Rows[0]["MaHS"]); int bs = Convert.ToInt32(service.BacSi().Rows[0]["MaBS"]);
        var now = DateTime.Now; var at = new DateTime(now.Year + 2, now.Month, now.Day, now.Hour, now.Minute, 0);
        Check(service.DatHen(h, bs, at).ThanhCong, "Đặt hẹn"); Check(!service.DatHen(h, bs, at).ThanhCong, "Chặn bác sĩ trùng lịch");
        Check(service.LapKham(h, bs, true, "Sức khỏe định kỳ", 150000).ThanhCong, "Khám định kỳ");
        int p = Convert.ToInt32(service.Kham(h).Rows[0]["MaPK"]);
        Check(Convert.ToDecimal(service.Kham(h).Rows[0]["TienKham"]) == 0, "Miễn tiền khám định kỳ");
        Check(!service.LapKham(h, bs, true, "Lặp quý", 0).ThanhCong, "Chặn định kỳ trùng quý");
        Check(service.ChiDinh(p, "Xét nghiệm mô phỏng", 50000).ThanhCong, "Chỉ định xét nghiệm");
        Check(!service.LapToa(p, "Kết luận", new()).ThanhCong, "Chặn kết luận khi thiếu kết quả xét nghiệm");
        int x = Convert.ToInt32(service.XN(p).Rows[0]["MaXN"]); Check(service.KetQua(x, "Bình thường (demo)").ThanhCong, "Ghi kết quả");
        int m = Convert.ToInt32(service.Thuoc().Rows[0]["MaThuoc"]);
        Check(!service.LapToa(p, "Demo", new() { new(m, 0, "Demo") }).ThanhCong, "Chặn số lượng bằng 0");
        Check(service.LapToa(p, "Kết luận mô phỏng", new() { new(m, 2, "Liều dùng mô phỏng") }).ThanhCong, "Lưu toa và chi tiết");
        Check(!service.LapToa(p, "Trùng", new()).ThanhCong, "Chặn kê toa hai lần");
        Check(service.ThanhToan(p).ThanhCong, "Thanh toán và đóng phiếu"); Check(!service.ThanhToan(p).ThanhCong, "Chặn thu tiền hai lần");
        Check(Convert.ToDecimal(Db.Scalar("SELECT TongTien FROM ThanhToan WHERE MaPK=@p", Db.P("@p", p))) == 50000, "Tổng tiền khám và xét nghiệm");
        Check(!service.KetQua(x, "Sửa sau đóng").ThanhCong, "Khóa cập nhật sau hoàn tất");
        Check(service.LapKham(h, bs, false, "Lần điều trị tiếp theo", 100000).ThanhCong, "Nhiều phiếu không có lịch hẹn");
        Check(service.GiaHan(h, 200000).ThanhCong, "Gia hạn lệ phí");

        // Các kiểm tra hồi quy bổ sung sau rà soát toàn bộ.
        bool Throws(Action a) { try { a(); return false; } catch (InvalidOperationException) { return true; } }
        Check(service.HoSo(h.ToString()).Rows.Cast<System.Data.DataRow>().Any(r => Convert.ToInt32(r["MaHS"]) == h), "Tra cứu chính xác mã hồ sơ");
        Check(Throws(() => service.XuatToa(-1)), "Không xuất toa chưa tồn tại");
        var exported = service.XuatToa(p);
        Check(exported.Contains(tag) && exported.Contains("Bác sĩ:") && exported.Contains("Khoa:") && exported.Contains("Kết luận:"), "Toa xuất có thông tin bệnh nhân bác sĩ khoa và kết luận");
        int p2 = Convert.ToInt32(service.Kham(h).Rows[0]["MaPK"]);
        Check(!service.LapToa(p2, "Thử", new() { new(m, 1, "A"), new(m, 2, "B") }).ThanhCong, "Chặn thuốc trùng trong một toa");
        Check(!service.LapToa(p2, "Thử", new() { new(m, 1, " ") }).ThanhCong, "Chặn dòng thuốc thiếu liều dùng");
        Check(!service.ChiDinh(p2, "Xét nghiệm", -1).ThanhCong, "Chặn chi phí xét nghiệm âm");
        Check(!service.ChiDinh(p2, " ", 0).ThanhCong, "Chặn tên xét nghiệm rỗng");
        Check(!service.LapToa(p2, "Thử", null).ThanhCong, "Chặn danh sách thuốc null");
        Check(!service.LapToa(p2, " ", new()).ThanhCong, "Chặn kết luận rỗng");
        Check(!service.LapToa(p2, "Thử rollback", new() { new(m, 1, "Demo"), new(-1, 1, "Demo") }).ThanhCong, "Lỗi khóa ngoại khi lưu dòng thuốc được báo thất bại");
        Check(Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM ToaThuoc WHERE MaPK=@p", Db.P("@p", p2))) == 0 && Convert.ToString(Db.Scalar("SELECT TrangThai FROM PhieuKham WHERE MaPK=@p", Db.P("@p", p2))) == "Đang khám", "Rollback toàn bộ toa khi một dòng lỗi");
        Check(!service.ThanhToan(p2).ThanhCong, "Chặn thanh toán phiếu chưa kết luận");
        Check(service.LapToa(p2, "Không cần thuốc", new()).ThanhCong, "Hoàn tất khám không kê thuốc");
        Check(Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM ToaThuoc WHERE MaPK=@p", Db.P("@p", p2))) == 0, "Không tạo toa rỗng cho lần khám không cần thuốc");
        Check(Convert.ToString(Db.Scalar("SELECT TrangThai FROM PhieuKham WHERE MaPK=@p", Db.P("@p", p2))) == "Chờ thanh toán", "Phiếu không thuốc ở trạng thái chờ thanh toán");
        Check(Throws(() => service.XuatToa(p2)), "Chặn xuất toa đối với lần khám không cần thuốc");
        Check(service.ThanhToan(p2).ThanhCong, "Thanh toán lần khám không thuốc");
        Check(Convert.ToDecimal(Db.Scalar("SELECT TongTien FROM ThanhToan WHERE MaPK=@p", Db.P("@p", p2))) == 100000, "Tổng thanh toán không tự thêm tiền thuốc");
        int hen = Convert.ToInt32(service.Hen(h).Rows[0]["MaHen"]);
        Check(!service.LapKham(h, bs, false, "Sai ngày hẹn", 1, hen).ThanhCong, "Không tiếp nhận lịch hẹn thuộc ngày tương lai");
        Check(Convert.ToString(Db.Scalar("SELECT TrangThai FROM LichHen WHERE MaHen=@h", Db.P("@h", hen))) == "Đã hẹn", "Lịch tương lai giữ trạng thái khi tiếp nhận bị từ chối");
        var savedAt = Convert.ToDateTime(service.Hen(h).Rows[0]["ThoiGian"]);
        Check(!service.DatHen(h, bs, savedAt.AddSeconds(30)).ThanhCong, "Chặn lịch cùng phút dù khác giây");
        Db.Execute("UPDATE LichHen SET ThoiGian=@d WHERE MaHen=@h", Db.P("@d", DateTime.Now.AddMinutes(-10)), Db.P("@h", hen));
        Check(service.LapKham(h, bs, false, "Tiếp nhận đúng lịch", 100000, hen).ThanhCong, "Tiếp nhận lịch đến ngày khám");
        Check(Convert.ToString(Db.Scalar("SELECT TrangThai FROM LichHen WHERE MaHen=@h", Db.P("@h", hen))) == "Đã khám" && Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM PhieuKham WHERE MaHen=@h", Db.P("@h", hen))) == 1, "Lịch hẹn liên kết duy nhất với phiếu khám");
        Check(!service.LapKham(h, bs, false, "Tiếp nhận lặp", 100000, hen).ThanhCong, "Chặn dùng lại lịch hẹn đã xử lý");
        Check(service.DangKy(tag + "_HETHAN", DateTime.Today.AddYears(-30), "Demo", "0901234567", null, 200000).ThanhCong, "Tạo hồ sơ kiểm tra lệ phí hết hạn");
        int expired = Convert.ToInt32(service.HoSo(tag + "_HETHAN").Rows[0]["MaHS"]);
        Db.Execute("UPDATE LePhiNam SET TuNgay=@s,DenNgay=@e WHERE MaHS=@h", Db.P("@s", DateTime.Today.AddYears(-2)), Db.P("@e", DateTime.Today.AddYears(-1)), Db.P("@h", expired));
        Check(!service.LapKham(expired, bs, true, "Định kỳ hết hạn", 0).ThanhCong, "Lệ phí hết hạn không được khám định kỳ miễn phí");
        Check(service.GiaHan(expired, 200000).ThanhCong, "Gia hạn hồ sơ hết hạn bắt đầu từ hôm nay");
        Check(service.LapKham(expired, bs, true, "Định kỳ sau gia hạn", 100000).ThanhCong, "Khám định kỳ sau gia hạn thành công");
        int originalKhoa = Convert.ToInt32(Db.Scalar("SELECT MaKhoa FROM BacSi WHERE MaBS=@b", Db.P("@b", bs)));
        int otherKhoa = Convert.ToInt32(Db.Scalar("SELECT TOP(1) MaKhoa FROM Khoa WHERE MaKhoa<>@k", Db.P("@k", originalKhoa)));
        int auditDoctor = Convert.ToInt32(Db.Scalar("INSERT BacSi(HoTen,MaKhoa) OUTPUT INSERTED.MaBS VALUES(@n,@k)", Db.P("@n", tag + "_BS"), Db.P("@k", originalKhoa)));
        Check(service.LapKham(h, auditDoctor, false, "Lưu khoa tại thời điểm khám", 0).ThanhCong, "Lưu khoa điều trị theo lần khám");
        int history = Convert.ToInt32(service.Kham(h).Rows[0]["MaPK"]);
        Db.Execute("UPDATE BacSi SET MaKhoa=@k WHERE MaBS=@b", Db.P("@k", otherKhoa), Db.P("@b", auditDoctor));
        Check(Convert.ToInt32(Db.Scalar("SELECT MaKhoa FROM PhieuKham WHERE MaPK=@p", Db.P("@p", history))) == originalKhoa, "Đổi khoa bác sĩ không làm đổi khoa lịch sử");
        Check(!service.GiaHan(h, 0).ThanhCong, "Chặn gia hạn có lệ phí bằng 0");
        Check(!service.LapKham(h, -1, false, "Sai bác sĩ", 0).ThanhCong, "Chặn bác sĩ không tồn tại");
        Check(!service.DatHen(h, bs, DateTime.Now.AddDays(-1)).ThanhCong, "Chặn lịch hẹn trong quá khứ");
        Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "Verification")); File.WriteAllLines(Path.Combine(AppContext.BaseDirectory, "Verification", "results.txt"), log); Console.WriteLine($"PASS {log.Count}/{log.Count}");
        }
        finally { VerificationCleanup.Run(tag); }
    }
    public static void Screenshots(string path) { Directory.CreateDirectory(path); foreach (var type in new[] { typeof(Forms.FrmMain), typeof(Forms.FrmHoSo), typeof(Forms.FrmLichHen), typeof(Forms.FrmKham), typeof(Forms.FrmXetNghiem), typeof(Forms.FrmToaThuoc) }) { using var f = (Form)Activator.CreateInstance(type); f.Show(); Application.DoEvents(); using var bitmap = new Bitmap(f.Width, f.Height); f.DrawToBitmap(bitmap, new Rectangle(0, 0, f.Width, f.Height)); bitmap.Save(Path.Combine(path, type.Name + ".png")); f.Close(); } }
}
