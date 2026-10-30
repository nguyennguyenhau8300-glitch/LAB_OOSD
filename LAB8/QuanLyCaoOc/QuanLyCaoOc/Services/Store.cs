using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyCaoOc.Data;
using static QuanLyCaoOc.Services.Rules;

namespace QuanLyCaoOc.Services;

internal static class Store
{
    public static DataRow One(DataTable table, string message) { Require(table.Rows.Count == 1, message); return table.Rows[0]; }
    public static DataRow Contract(SqlConnection c, SqlTransaction t, string id) => One(Db.Query(c, t,
        "SELECT * FROM HopDong WITH(UPDLOCK,HOLDLOCK) WHERE MaHD=@p0", id), "Không tìm thấy hợp đồng.");
    public static DataTable Details(SqlConnection c, SqlTransaction t, string id) => Db.Query(c, t,
        "SELECT ct.*,hd.NgayBatDau,hd.SoThangGoc,hd.MaKH,hd.TrangThai FROM ChiTietHopDong ct JOIN HopDong hd ON hd.MaHD=ct.MaHD WHERE ct.MaHD=@p0 ORDER BY ct.MaPhong", id);
    public static void LockRooms(SqlConnection c, SqlTransaction t, IEnumerable<string> rooms)
    {
        foreach (var room in rooms.Distinct().Order())
            One(Db.Query(c, t, "SELECT * FROM VanPhong WITH(UPDLOCK,HOLDLOCK) WHERE MaPhong=@p0", room), "Không tìm thấy phòng " + room);
    }
    public static void NoOverlap(SqlConnection c, SqlTransaction t, string room, DateTime start, DateTime end, long except = 0)
    {
        var count = Convert.ToInt32(Db.Scalar(c, t, """
            SELECT COUNT(*) FROM ChiTietHopDong ct JOIN HopDong h ON ct.MaHD=h.MaHD
            WHERE ct.MaPhong=@p0 AND ct.MaCTHD<>@p3 AND h.TrangThai IN('CHO_TRA_TRUOC','HIEU_LUC')
              AND h.NgayBatDau<@p2 AND @p1<ct.NgayKetThuc
            """, room, start.Date, end.Date, except));
        Require(count == 0, "Phòng " + room + " bị trùng khoảng thuê.");
    }
    public static void GenerateRent(SqlConnection c, SqlTransaction t, long detail, DateTime origin, int first, int count, decimal price)
    {
        for (int i = first; i < first + count; i++)
            Db.Exec(c, t, "INSERT KhoanPhaiThu(MaCTHD,Loai,KyTu,KyDen,HanThu,SoTien) VALUES(@p0,'THUE',@p1,@p2,@p3,@p4)",
                detail, origin.AddMonths(i), origin.AddMonths(i + 1), i < 6 ? origin : origin.AddMonths(i), price);
    }
    public static void ServicesClear(SqlConnection c, SqlTransaction t, DataRow detail, DateTime asOf)
    {
        long id = Convert.ToInt64(detail["MaCTHD"]);
        DateTime end = (DateTime)detail["NgayKetThuc"], start = (DateTime)detail["NgayBatDau"];
        DateTime through = asOf.Date < end ? asOf.Date : end.AddDays(-1);
        if (through < start) return;
        for (DateTime month = Month(start); month <= Month(through); month = month.AddMonths(1))
        {
            DateTime need = MonthEnd(month) < through ? MonthEnd(month) : through;
            var check = Db.Query(c, t, "SELECT TrangThai,NgayChot FROM HoaDonDichVu WHERE MaCTHD=@p0 AND Thang=@p1 AND Nam=@p2", id, month.Month, month.Year);
            Require(check.Rows.Count == 1 && (string)check.Rows[0]["TrangThai"] == "PHAT_HANH" &&
                check.Rows[0]["NgayChot"] is DateTime closed && closed >= need,
                $"Phòng {detail["MaPhong"]}: dịch vụ {month:MM/yyyy} chưa được chốt/đối soát đến {need:dd/MM/yyyy}.");
        }
        decimal debt = Convert.ToDecimal(Db.Scalar(c, t, "SELECT COALESCE(SUM(ConNo),0) FROM vwCongNo WHERE MaCTHD=@p0 AND Loai='DICHVU'", id));
        Require(debt == 0, $"Phòng {detail["MaPhong"]} còn nợ dịch vụ {debt:N0} đồng.");
    }
}

public static class Reports
{
    public static DataTable Contracts() => Db.Query("SELECT h.MaHD,h.MaKH,k.TenCongTy,h.NgayKy,h.NgayBatDau,h.SoThangGoc,h.TrangThai,MAX(ct.NgayKetThuc) NgayKetThuc FROM HopDong h JOIN KhachHang k ON k.MaKH=h.MaKH JOIN ChiTietHopDong ct ON ct.MaHD=h.MaHD GROUP BY h.MaHD,h.MaKH,k.TenCongTy,h.NgayKy,h.NgayBatDau,h.SoThangGoc,h.TrangThai ORDER BY h.NgayBatDau DESC");
    public static DataTable Details(string hd) => Db.Query("SELECT MaCTHD,MaHD,MaPhong,DonGiaThang,NgayKetThuc FROM ChiTietHopDong WHERE MaHD=@p0", hd);
    public static DataTable Charges(string hd) => Db.Query("SELECT n.*,ct.MaPhong FROM vwCongNo n JOIN ChiTietHopDong ct ON ct.MaCTHD=n.MaCTHD WHERE ct.MaHD=@p0 ORDER BY n.HanThu,n.KyTu,ct.MaPhong", hd);
    public static DataTable Receipts() => Db.Query("SELECT p.*,k.TenCongTy FROM PhieuThu p JOIN KhachHang k ON k.MaKH=p.MaKH ORDER BY p.NgayThu DESC");
    public static DataTable Invoices(string hd) => Db.Query("SELECT h.MaHDDV,h.MaCTHD,ct.MaPhong,h.Thang,h.Nam,h.TrangThai,h.NgayChot,CAST(ROUND(COALESCE(SUM(d.SoLuong*d.DonGia),0),2) AS decimal(18,2)) TongTien FROM HoaDonDichVu h JOIN ChiTietHopDong ct ON ct.MaCTHD=h.MaCTHD LEFT JOIN ChiTietDichVu d ON d.MaHDDV=h.MaHDDV WHERE ct.MaHD=@p0 GROUP BY h.MaHDDV,h.MaCTHD,ct.MaPhong,h.Thang,h.Nam,h.TrangThai,h.NgayChot ORDER BY h.Nam DESC,h.Thang DESC", hd);
    public static DataTable Employees() => Db.Query("SELECT n.*,k.TenCongTy,t.SoTang FROM NhanVienCongTy n JOIN KhachHang k ON k.MaKH=n.MaKH JOIN VanPhong p ON p.MaPhong=n.MaPhong JOIN Tang t ON t.MaTang=p.MaTang ORDER BY n.MaNV");
    public static DataTable Cards() => Db.Query("SELECT t.MaThe,t.MaNV,n.HoTen,n.MaKH,n.MaPhong,g.SoTang,x.MaXe,x.BienSo,x.LoaiXe,x.MauXe,t.NgayCap,t.NgayThuHoi FROM TheGiuXe t JOIN NhanVienCongTy n ON n.MaNV=t.MaNV JOIN Xe x ON x.MaXe=t.MaXe JOIN VanPhong p ON p.MaPhong=n.MaPhong JOIN Tang g ON g.MaTang=p.MaTang ORDER BY t.NgayCap DESC");
    public static DataTable Renewals() => Db.Query("SELECT p.MaPGH,p.MaHD,p.NgayLap,p.NguoiLap,d.MaCTHD,ct.MaPhong,d.SoThang,d.HanCu,d.HanMoi,d.DonGiaThang FROM PhieuGiaHan p JOIN ChiTietGiaHan d ON d.MaPGH=p.MaPGH JOIN ChiTietHopDong ct ON ct.MaCTHD=d.MaCTHD ORDER BY p.NgayLap DESC");
    public static DataTable Checks() => Db.Query("SELECT d.MaCTKT,p.MaPKT,p.NgayKiemTra,p.NguoiKiemTra,d.MaPhong,d.TinhTrang,d.ThayDoiKetCau,d.DeXuat,p.GhiChu FROM PhieuKiemTra p JOIN ChiTietKiemTra d ON d.MaPKT=p.MaPKT ORDER BY p.NgayKiemTra DESC");
    public static DataTable Fines() => Db.Query("SELECT p.*,ct.MaPhong,h.MaKH FROM PhieuPhat p JOIN ChiTietHopDong ct ON ct.MaCTHD=p.MaCTHD JOIN HopDong h ON h.MaHD=ct.MaHD ORDER BY p.HanNop DESC");
    public static DataTable DueChecks(DateTime date) => Db.Query("SELECT p.MaPhong,p.MaTang,COALESCE(MAX(k.NgayKiemTra),p.NgaySuDung) LanGanNhat,DATEADD(month,12,COALESCE(MAX(k.NgayKiemTra),p.NgaySuDung)) HanKiemTra FROM VanPhong p LEFT JOIN ChiTietKiemTra d ON d.MaPhong=p.MaPhong LEFT JOIN PhieuKiemTra k ON k.MaPKT=d.MaPKT AND k.NgayKiemTra<=@p0 WHERE p.NgaySuDung<=@p0 AND p.NgungSuDung=0 GROUP BY p.MaPhong,p.MaTang,p.NgaySuDung HAVING DATEADD(month,12,COALESCE(MAX(k.NgayKiemTra),p.NgaySuDung))<=@p0", date.Date);
}

