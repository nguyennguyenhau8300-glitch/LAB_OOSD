using Microsoft.Data.SqlClient;
using QuanLyKhachSan.Data;
using System.Data;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmThongKe : Form
    {
        public FrmThongKe()
        {
            InitializeComponent();
        }

        private void FrmThongKe_Load(
            object sender,
            EventArgs e)
        {
            dtpTuNgay.Value =
                new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    1);

            dtpDenNgay.Value =
                DateTime.Today;

            LoadThongKe();
        }

        private void btnThongKe_Click(
            object sender,
            EventArgs e)
        {
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            try
            {
                int tongPhong =
                    Convert.ToInt32(
                        Db.Scalar(
                            "SELECT COUNT(*) FROM Phong"));

                int phongTrong =
                    Convert.ToInt32(
                        Db.Scalar(@"
                            SELECT COUNT(*)
                            FROM Phong
                            WHERE TrangThai = N'Trống'"));

                int dangO =
                    Convert.ToInt32(
                        Db.Scalar(@"
                            SELECT COUNT(*)
                            FROM PhieuDatPhong
                            WHERE TrangThai = N'Đang ở'"));

                decimal doanhThu =
                    Convert.ToDecimal(
                        Db.Scalar(@"
                            SELECT ISNULL(SUM(SoTien), 0)
                            FROM ThanhToan
                            WHERE NgayThanhToan >= @TuNgay
                              AND NgayThanhToan < DATEADD(
                                    DAY, 1, @DenNgay)",
                            new SqlParameter(
                                "@TuNgay",
                                dtpTuNgay.Value.Date),

                            new SqlParameter(
                                "@DenNgay",
                                dtpDenNgay.Value.Date)));

                lblTongPhong.Text =
                    $"TỔNG PHÒNG\n{tongPhong}";

                lblPhongTrong.Text =
                    $"PHÒNG TRỐNG\n{phongTrong}";

                lblDangO.Text =
                    $"KHÁCH ĐANG Ở\n{dangO}";

                lblDoanhThu.Text =
                    $"DOANH THU\n{doanhThu:N0} VNĐ";

                dgvHoaDon.DataSource =
                    Db.Query(@"
                        SELECT
                            H.MaHoaDon AS [Hóa đơn],
                            H.SoPhieuDat AS [Phiếu đặt],
                            K.HoTen AS [Khách hàng],
                            H.NgayLap AS [Ngày lập],
                            H.TienPhong AS [Tiền phòng],
                            H.TienDichVu AS [Dịch vụ],
                            H.TienDenBu AS [Đền bù],
                            H.TienCoc AS [Tiền cọc],
                            H.TongThanhToan AS [Tổng],
                            H.TrangThai AS [Trạng thái]

                        FROM HoaDon H

                        INNER JOIN PhieuDatPhong P
                            ON H.SoPhieuDat =
                               P.SoPhieuDat

                        INNER JOIN KhachHang K
                            ON P.MaKhach =
                               K.MaKhach

                        WHERE H.NgayLap >= @TuNgay

                          AND H.NgayLap <
                              DATEADD(
                                  DAY,
                                  1,
                                  @DenNgay)

                        ORDER BY H.NgayLap DESC",

                        new SqlParameter(
                            "@TuNgay",
                            dtpTuNgay.Value.Date),

                        new SqlParameter(
                            "@DenNgay",
                            dtpDenNgay.Value.Date));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thống kê:\n\n" +
                    ex.Message);
            }
        }
    }
}