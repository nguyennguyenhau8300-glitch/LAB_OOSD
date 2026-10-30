using System.Data;
using QuanLyCaoOc.Data;
using static QuanLyCaoOc.Services.Rules;

namespace QuanLyCaoOc.Services;

public sealed class ThanhToanService
{
    public string Thu(ThuTienRequest input) => Db.Transaction((c, tx) =>
    {
        var old = Db.Scalar(c, tx, "SELECT MaPT FROM PhieuThu WHERE RequestId=@p0", input.RequestId);
        if (old is string previous) return previous;
        Past(input.NgayThu); Text(input.PhuongThuc, "phương thức thu");
        Precision(input.TongTien);foreach(var line in input.PhanBo)Precision(line.SoTien);
        Require(input.TongTien > 0 && input.PhanBo.Count > 0 && input.PhanBo.All(p => p.SoTien > 0), "Tiền thu và mỗi dòng phân bổ phải dương.");
        Require(input.PhanBo.Select(p => p.MaKhoan).Distinct().Count() == input.PhanBo.Count, "Khoản phân bổ bị lặp.");
        Require(input.PhanBo.Sum(p => p.SoTien) == input.TongTien, "Tổng phân bổ phải bằng tổng phiếu thu.");
        var contracts = new HashSet<string>();
        foreach (var line in input.PhanBo)
        {
            var row = Store.One(Db.Query(c, tx, "SELECT n.*,ct.MaHD,h.MaKH,h.TrangThai,h.NgayKy FROM vwCongNo n JOIN ChiTietHopDong ct ON ct.MaCTHD=n.MaCTHD JOIN HopDong h ON h.MaHD=ct.MaHD WHERE n.MaKhoan=@p0", line.MaKhoan), "Khoản phải thu không tồn tại.");
            Require((string)row["MaKH"] == input.MaKH, "Các khoản thu phải cùng khách hàng.");
            Require((string)row["TrangThai"] is "CHO_TRA_TRUOC" or "HIEU_LUC", "Không thu hợp đồng đã hủy/kết thúc.");
            Require(input.NgayThu.Date >= (DateTime)row["NgayKy"], "Ngày thu phải từ ngày ký hợp đồng.");
            Require(line.SoTien <= (decimal)row["ConNo"], "Tiền phân bổ vượt số dư khoản " + line.MaKhoan);
            contracts.Add((string)row["MaHD"]);
        }
        string ma = Id("PT");
        Db.Exec(c, tx, "INSERT PhieuThu VALUES(@p0,@p1,@p2,@p3,@p4,@p5)", ma, input.MaKH, input.NgayThu.Date, input.TongTien, input.PhuongThuc, input.RequestId);
        foreach (var line in input.PhanBo) Db.Exec(c, tx, "INSERT PhanBoThu(MaPT,MaKhoan,SoTien) VALUES(@p0,@p1,@p2)", ma, line.MaKhoan, line.SoTien);
        foreach (string hd in contracts)
            Db.Exec(c, tx, """
                UPDATE HopDong SET TrangThai='HIEU_LUC' WHERE MaHD=@p0 AND TrangThai='CHO_TRA_TRUOC'
                AND NOT EXISTS(SELECT 1 FROM vwCongNo n JOIN ChiTietHopDong ct ON ct.MaCTHD=n.MaCTHD
                WHERE ct.MaHD=HopDong.MaHD AND n.Loai='THUE' AND n.KyTu<DATEADD(month,6,HopDong.NgayBatDau) AND n.ConNo>0)
                """, hd);
        return ma;
    });

    public IReadOnlyList<PhanBo> TraTruoc(string hd)
    {
        var data = Db.Query("SELECT n.MaKhoan,n.ConNo FROM vwCongNo n JOIN ChiTietHopDong ct ON ct.MaCTHD=n.MaCTHD JOIN HopDong h ON h.MaHD=ct.MaHD WHERE ct.MaHD=@p0 AND n.Loai='THUE' AND n.KyTu<DATEADD(month,6,h.NgayBatDau) AND n.ConNo>0", hd);
        return data.Rows.Cast<DataRow>().Select(r => new PhanBo((long)r["MaKhoan"], (decimal)r["ConNo"])).ToList();
    }
    public string LuuDichVu(long detail, int month, int year, IReadOnlyList<DongDichVu> lines, DateTime date) => Db.Transaction((c, tx) =>
    {
        Past(date); Require(month is >= 1 and <= 12 && year is >= 2000 and <= 2200, "Kỳ dịch vụ không hợp lệ.");
        var ct = Store.One(Db.Query(c, tx, "SELECT ct.*,h.NgayBatDau,h.TrangThai FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE ct.MaCTHD=@p0", detail), "Không có phòng thuê.");
        Require((string)ct["TrangThai"] == "HIEU_LUC", "Chỉ ghi dịch vụ cho hợp đồng đã hiệu lực.");
        var period = new DateTime(year, month, 1);
        Require(period <= date.Date && period < (DateTime)ct["NgayKetThuc"] && period.AddMonths(1) > (DateTime)ct["NgayBatDau"], "Kỳ dịch vụ nằm ngoài thời gian thuê hoặc ở tương lai.");
        Require(lines.All(x => x.SoLuong >= 0 && x.DonGia >= 0) && lines.Select(x => x.MaDV).Distinct().Count() == lines.Count, "Dòng dịch vụ bị lặp hoặc có số âm.");
        foreach(var line in lines){Precision(line.SoLuong);Precision(line.DonGia);}
        var existing = Db.Query(c, tx, "SELECT * FROM HoaDonDichVu WHERE MaCTHD=@p0 AND Thang=@p1 AND Nam=@p2", detail, month, year);
        string ma;
        if (existing.Rows.Count > 0)
        {
            var h = existing.Rows[0]; ma = (string)h["MaHDDV"];
            Require(h["MaKhoan"] is DBNull, "Hóa đơn đã phát hành có tiền; không sửa trực tiếp.");
            Db.Exec(c, tx, "DELETE ChiTietDichVu WHERE MaHDDV=@p0", ma);
            Db.Exec(c, tx, "UPDATE HoaDonDichVu SET TrangThai='NHAP',NgayChot=NULL WHERE MaHDDV=@p0", ma);
        }
        else { ma = Id("DV"); Db.Exec(c, tx, "INSERT HoaDonDichVu VALUES(@p0,@p1,@p2,@p3,@p4,NULL,'NHAP',NULL)", ma, detail, month, year, date.Date); }
        foreach (var line in lines) Db.Exec(c, tx, "INSERT ChiTietDichVu(MaHDDV,MaDV,SoLuong,DonGia) VALUES(@p0,@p1,@p2,@p3)", ma, line.MaDV, line.SoLuong, line.DonGia);
        return ma;
    });
    public void PhatHanh(string invoice, DateTime date) => Db.Transaction((c, tx) =>
    {
        Past(date);
        var h = Store.One(Db.Query(c, tx, "SELECT * FROM HoaDonDichVu WHERE MaHDDV=@p0", invoice), "Không tìm thấy hóa đơn.");
        var period = new DateTime((short)h["Nam"], (byte)h["Thang"], 1);
        Require(date.Date >= period && date.Date >= ((DateTime)h["NgayLap"]).Date, "Ngày chốt phải từ ngày lập và trong/sau kỳ hóa đơn.");
        if (h["MaKhoan"] is not DBNull) return true; // Idempotent publish; never reset paid amount.
        decimal total = decimal.Round(Convert.ToDecimal(Db.Scalar(c, tx, "SELECT COALESCE(SUM(SoLuong*DonGia),0) FROM ChiTietDichVu WHERE MaHDDV=@p0", invoice)),2,MidpointRounding.AwayFromZero);
        Require(total == 0 || date.Date >= MonthEnd(period), "Dịch vụ có tiền chỉ được phát hành vào cuối tháng hoặc sau đó.");
        long? charge = null;
        if (total > 0)
            charge = Convert.ToInt64(Db.Scalar(c, tx, "INSERT KhoanPhaiThu(MaCTHD,Loai,KyTu,KyDen,HanThu,SoTien) OUTPUT INSERTED.MaKhoan VALUES(@p0,'DICHVU',@p1,@p2,@p3,@p4)", h["MaCTHD"], period, period.AddMonths(1), MonthEnd(period), total));
        DateTime through = date.Date <= MonthEnd(period) ? date.Date : MonthEnd(period);
        Db.Exec(c, tx, "UPDATE HoaDonDichVu SET TrangThai='PHAT_HANH',NgayChot=@p1,MaKhoan=@p2 WHERE MaHDDV=@p0", invoice, through, charge);
        return true;
    });
    public void XacNhanKhongPhatSinh(long detail, DateTime through)
    {
        Past(through);
        // One atomic transaction; do not overwrite an existing draft with usage.
        Db.Transaction((c, tx) =>
        {
            var ct = Store.One(Db.Query(c, tx, "SELECT ct.*,h.NgayBatDau,h.TrangThai FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE ct.MaCTHD=@p0", detail), "Không có phòng thuê.");
            Require((string)ct["TrangThai"] == "HIEU_LUC", "Hợp đồng chưa hiệu lực.");
            var limit = through.Date < (DateTime)ct["NgayKetThuc"] ? through.Date : ((DateTime)ct["NgayKetThuc"]).AddDays(-1);
            Require(limit >= (DateTime)ct["NgayBatDau"], "Ngày đối soát trước ngày bắt đầu.");
            for (var m = Month((DateTime)ct["NgayBatDau"]); m <= Month(limit); m = m.AddMonths(1))
            {
                DateTime close = MonthEnd(m) < limit ? MonthEnd(m) : limit;
                var data = Db.Query(c, tx, "SELECT h.*,COALESCE((SELECT SUM(d.SoLuong*d.DonGia) FROM ChiTietDichVu d WHERE d.MaHDDV=h.MaHDDV),0) Tong FROM HoaDonDichVu h WHERE MaCTHD=@p0 AND Thang=@p1 AND Nam=@p2", detail, m.Month, m.Year);
                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    // Existing nonzero usage stays untouched and must be closed/paid separately.
                    if ((decimal)row["Tong"] != 0 || row["MaKhoan"] is not DBNull) continue;
                    Db.Exec(c, tx, "UPDATE HoaDonDichVu SET NgayChot=CASE WHEN NgayChot>@p1 THEN NgayChot ELSE @p1 END,TrangThai='PHAT_HANH' WHERE MaHDDV=@p0", row["MaHDDV"], close);
                }
                else Db.Exec(c, tx, "INSERT HoaDonDichVu VALUES(@p0,@p1,@p2,@p3,@p4,@p5,'PHAT_HANH',NULL)", Id("DV"), detail, m.Month, m.Year, close, close);
            }
            return true;
        });
    }
}
