using Microsoft.Data.SqlClient;
using QuanLyKhachSan.Data;
using System.Data;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmTraPhongThanhToan : Form
    {
        private string? maHoaDon;

        private decimal tienPhong;
        private decimal tienDichVu;
        private decimal tienDenBu;
        private decimal tienCoc;
        private decimal tongThanhToan;

        public FrmTraPhongThanhToan()
        {
            InitializeComponent();
        }

        private void FrmTraPhongThanhToan_Load(
            object sender, EventArgs e)
        {
            dtpNgayTra.Value = DateTime.Now;

            LoadPhieuDangO();
            LoadNhanVien();
        }

        private void LoadPhieuDangO()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    P.SoPhieuDat,
                    P.SoPhieuDat + N' - ' + K.HoTen AS HienThi
                FROM PhieuDatPhong P
                INNER JOIN KhachHang K
                    ON P.MaKhach = K.MaKhach
                WHERE P.TrangThai = N'Đang ở'
                ORDER BY P.SoPhieuDat");

            cboPhieuDat.DataSource = dt;
            cboPhieuDat.DisplayMember = "HienThi";
            cboPhieuDat.ValueMember = "SoPhieuDat";
            cboPhieuDat.SelectedIndex = -1;
        }

        private void LoadNhanVien()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    MaNV,
                    MaNV + N' - ' + HoTen AS HienThi
                FROM NhanVien
                WHERE TrangThai = N'Đang làm'
                ORDER BY MaNV");

            cboNhanVien.DataSource = dt;
            cboNhanVien.DisplayMember = "HienThi";
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.SelectedIndex = -1;
        }

        private string? SoPhieu()
        {
            if (cboPhieuDat.SelectedValue == null ||
                cboPhieuDat.SelectedValue is DataRowView)
                return null;

            return cboPhieuDat.SelectedValue.ToString();
        }

        private void cboPhieuDat_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (SoPhieu() == null)
                return;

            LoadChiTiet();
            TinhTien();
        }

        private void LoadChiTiet()
        {
            string soPhieu = SoPhieu()!;

            dgvPhong.DataSource = Db.Query(@"
                SELECT
                    CT.SoPhong AS [Phòng],
                    P.LoaiPhong AS [Loại phòng],
                    P.DonGiaNgay AS [Đơn giá/ngày]
                FROM ChiTietDatPhong CT
                INNER JOIN Phong P
                    ON CT.SoPhong = P.SoPhong
                WHERE CT.SoPhieuDat = @SoPhieu",
                new SqlParameter("@SoPhieu", soPhieu));

            dgvDichVu.DataSource = Db.Query(@"
                SELECT
                    SD.NgaySuDung AS [Ngày],
                    DV.TenDV AS [Dịch vụ],
                    CT.SoLuong AS [Số lượng],
                    CT.DonGia AS [Đơn giá],
                    CT.ThanhTien AS [Thành tiền]
                FROM PhieuSuDungDV SD
                INNER JOIN ChiTietSuDungDV CT
                    ON SD.SoPhieuSDDV = CT.SoPhieuSDDV
                INNER JOIN DichVu DV
                    ON CT.MaDV = DV.MaDV
                WHERE SD.SoPhieuDat = @SoPhieu
                ORDER BY SD.NgaySuDung",
                new SqlParameter("@SoPhieu", soPhieu));

            dgvDenBu.DataSource = Db.Query(@"
                SELECT
                    DB.SoPhieuDenBu AS [Phiếu],
                    DB.SoPhong AS [Phòng],
                    TN.TenTienNghi AS [Tiện nghi],
                    CT.LyDo AS [Lý do],
                    CT.SoLuong AS [SL],
                    CT.DonGiaDenBu AS [Đơn giá],
                    CT.ThanhTien AS [Thành tiền]
                FROM PhieuDenBu DB
                INNER JOIN ChiTietDenBu CT
                    ON DB.SoPhieuDenBu = CT.SoPhieuDenBu
                INNER JOIN TienNghi TN
                    ON CT.MaTienNghi = TN.MaTienNghi
                WHERE DB.SoPhieuDat = @SoPhieu",
                new SqlParameter("@SoPhieu", soPhieu));

            LoadHoaDon();
        }

        private void btnTinhTien_Click(
            object sender, EventArgs e)
        {
            TinhTien();
        }

        private void TinhTien()
        {
            string? soPhieu = SoPhieu();

            if (soPhieu == null)
                return;

            DataTable dt = Db.Query(@"
                SELECT
                    NgayNhan,
                    NgayNhanThucTe,
                    TienCoc
                FROM PhieuDatPhong
                WHERE SoPhieuDat = @SoPhieu",
                new SqlParameter("@SoPhieu", soPhieu));

            if (dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];

            DateTime ngayNhan =
                row["NgayNhanThucTe"] == DBNull.Value
                ? Convert.ToDateTime(row["NgayNhan"])
                : Convert.ToDateTime(row["NgayNhanThucTe"]);

            DateTime ngayTra = dtpNgayTra.Value;

            int soNgay =
                Math.Max(1,
                    (int)Math.Ceiling(
                        (ngayTra - ngayNhan).TotalDays));

            object? phong = Db.Scalar(@"
                SELECT ISNULL(SUM(P.DonGiaNgay), 0)
                FROM ChiTietDatPhong CT
                INNER JOIN Phong P
                    ON CT.SoPhong = P.SoPhong
                WHERE CT.SoPhieuDat = @SoPhieu",
                new SqlParameter("@SoPhieu", soPhieu));

            tienPhong =
                Convert.ToDecimal(phong) * soNgay;

            tienDichVu =
                Convert.ToDecimal(
                    Db.Scalar(@"
                        SELECT ISNULL(SUM(CT.ThanhTien), 0)
                        FROM PhieuSuDungDV SD
                        INNER JOIN ChiTietSuDungDV CT
                            ON SD.SoPhieuSDDV = CT.SoPhieuSDDV
                        WHERE SD.SoPhieuDat = @SoPhieu",
                        new SqlParameter("@SoPhieu", soPhieu)));

            tienDenBu =
                Convert.ToDecimal(
                    Db.Scalar(@"
                        SELECT ISNULL(SUM(CT.ThanhTien), 0)
                        FROM PhieuDenBu DB
                        INNER JOIN ChiTietDenBu CT
                            ON DB.SoPhieuDenBu = CT.SoPhieuDenBu
                        WHERE DB.SoPhieuDat = @SoPhieu",
                        new SqlParameter("@SoPhieu", soPhieu)));

            tienCoc =
                Convert.ToDecimal(row["TienCoc"]);

            tongThanhToan =
                tienPhong +
                tienDichVu +
                tienDenBu -
                tienCoc;

            if (tongThanhToan < 0)
                tongThanhToan = 0;

            txtTienPhong.Text =
                tienPhong.ToString("N0");

            txtTienDichVu.Text =
                tienDichVu.ToString("N0");

            txtTienDenBu.Text =
                tienDenBu.ToString("N0");

            txtTienCoc.Text =
                tienCoc.ToString("N0");

            txtTongThanhToan.Text =
                tongThanhToan.ToString("N0");

            LoadHoaDon();
        }

        // =====================================================
        // LẬP HÓA ĐƠN
        // =====================================================

        private void btnLapHoaDon_Click(
            object sender, EventArgs e)
        {
            string? soPhieu = SoPhieu();

            if (soPhieu == null)
            {
                MessageBox.Show("Chọn phiếu.");
                return;
            }

            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Chọn nhân viên.");
                return;
            }

            TinhTien();

            try
            {
                object? existing = Db.Scalar(@"
                    SELECT MaHoaDon
                    FROM HoaDon
                    WHERE SoPhieuDat = @SoPhieu",
                    new SqlParameter("@SoPhieu", soPhieu));

                if (existing != null &&
                    existing != DBNull.Value)
                {
                    maHoaDon = existing.ToString();

                    MessageBox.Show(
                        $"Phiếu đã có hóa đơn {maHoaDon}.");

                    LoadHoaDon();
                    return;
                }

                maHoaDon =
                    "HD" +
                    DateTime.Now.ToString(
                        "yyyyMMddHHmmssfff");

                Db.Execute(@"
                    INSERT INTO HoaDon
                    (
                        MaHoaDon,
                        SoPhieuDat,
                        MaNV,
                        TienPhong,
                        TienDichVu,
                        TienDenBu,
                        TienCoc,
                        TrangThai
                    )
                    VALUES
                    (
                        @MaHD,
                        @SoPhieu,
                        @NV,
                        @Phong,
                        @DV,
                        @DenBu,
                        @Coc,
                        N'Chưa thanh toán'
                    )",
                    new SqlParameter("@MaHD", maHoaDon),
                    new SqlParameter("@SoPhieu", soPhieu),
                    new SqlParameter(
                        "@NV",
                        cboNhanVien.SelectedValue),
                    new SqlParameter("@Phong", tienPhong),
                    new SqlParameter("@DV", tienDichVu),
                    new SqlParameter("@DenBu", tienDenBu),
                    new SqlParameter("@Coc", tienCoc));

                MessageBox.Show(
                    $"Lập hóa đơn {maHoaDon} thành công.");

                LoadHoaDon();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =====================================================
        // LOAD HÓA ĐƠN / THANH TOÁN
        // =====================================================

        private void LoadHoaDon()
        {
            string? soPhieu = SoPhieu();

            if (soPhieu == null)
                return;

            DataTable dt = Db.Query(@"
                SELECT
                    MaHoaDon,
                    TongThanhToan
                FROM HoaDon
                WHERE SoPhieuDat = @SoPhieu",
                new SqlParameter("@SoPhieu", soPhieu));

            if (dt.Rows.Count == 0)
            {
                maHoaDon = null;
                txtDaThanhToan.Text = "0";
                txtConLai.Text =
                    tongThanhToan.ToString("N0");

                dgvThanhToan.DataSource = null;
                return;
            }

            maHoaDon =
                dt.Rows[0]["MaHoaDon"].ToString();

            decimal tong =
                Convert.ToDecimal(
                    dt.Rows[0]["TongThanhToan"]);

            decimal daTra =
                Convert.ToDecimal(
                    Db.Scalar(@"
                        SELECT ISNULL(SUM(SoTien), 0)
                        FROM ThanhToan
                        WHERE MaHoaDon = @MaHD",
                        new SqlParameter(
                            "@MaHD",
                            maHoaDon)));

            txtTongThanhToan.Text =
                tong.ToString("N0");

            txtDaThanhToan.Text =
                daTra.ToString("N0");

            txtConLai.Text =
                Math.Max(0, tong - daTra)
                    .ToString("N0");

            dgvThanhToan.DataSource =
                Db.Query(@"
                    SELECT
                        NgayThanhToan AS [Ngày],
                        PhuongThuc AS [Phương thức],
                        SoTien AS [Số tiền],
                        MaGiaoDich AS [Mã giao dịch]
                    FROM ThanhToan
                    WHERE MaHoaDon = @MaHD
                    ORDER BY NgayThanhToan",
                    new SqlParameter(
                        "@MaHD",
                        maHoaDon));
        }

        // =====================================================
        // THANH TOÁN
        // =====================================================

        private void btnThanhToan_Click(
            object sender, EventArgs e)
        {
            if (maHoaDon == null)
            {
                MessageBox.Show(
                    "Hãy lập hóa đơn trước.");

                return;
            }

            if (nudSoTien.Value <= 0)
            {
                MessageBox.Show(
                    "Nhập số tiền thanh toán.");

                return;
            }

            try
            {
                using SqlConnection conn =
                    Db.GetConnection();

                conn.Open();

                using SqlCommand cmd =
                    new SqlCommand(
                        "sp_ThanhToanHoaDon",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@MaHoaDon",
                    maHoaDon);

                cmd.Parameters.AddWithValue(
                    "@PhuongThuc",
                    cboPhuongThuc.Text);

                cmd.Parameters.AddWithValue(
                    "@SoTien",
                    nudSoTien.Value);

                cmd.Parameters.AddWithValue(
                    "@MaGiaoDich",
                    string.IsNullOrWhiteSpace(
                        txtMaGiaoDich.Text)
                    ? DBNull.Value
                    : txtMaGiaoDich.Text.Trim());

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "Thanh toán thành công.");

                nudSoTien.Value = 0;
                txtMaGiaoDich.Clear();

                LoadHoaDon();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =====================================================
        // TRẢ PHÒNG
        // =====================================================

        private void btnTraPhong_Click(
            object sender, EventArgs e)
        {
            string? soPhieu = SoPhieu();

            if (soPhieu == null ||
                maHoaDon == null)
            {
                MessageBox.Show(
                    "Chưa có hóa đơn.");

                return;
            }

            decimal conLai =
                Convert.ToDecimal(
                    Db.Scalar(@"
                        SELECT
                            H.TongThanhToan -
                            ISNULL(SUM(T.SoTien), 0)
                        FROM HoaDon H
                        LEFT JOIN ThanhToan T
                            ON H.MaHoaDon = T.MaHoaDon
                        WHERE H.MaHoaDon = @MaHD
                        GROUP BY H.TongThanhToan",
                        new SqlParameter(
                            "@MaHD",
                            maHoaDon)));

            if (conLai > 0)
            {
                MessageBox.Show(
                    $"Hóa đơn còn {conLai:N0} VNĐ.");

                return;
            }

            try
            {
                using SqlConnection conn =
                    Db.GetConnection();

                conn.Open();

                using SqlTransaction tran =
                    conn.BeginTransaction();

                try
                {
                    SqlCommand cmd1 = new SqlCommand(@"
                        UPDATE PhieuDatPhong
                        SET
                            TrangThai = N'Đã trả',
                            NgayTraThucTe = @Ngay
                        WHERE SoPhieuDat = @SoPhieu",
                        conn, tran);

                    cmd1.Parameters.AddWithValue(
                        "@Ngay",
                        dtpNgayTra.Value);

                    cmd1.Parameters.AddWithValue(
                        "@SoPhieu",
                        soPhieu);

                    cmd1.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand(@"
                        UPDATE P
                        SET TrangThai = N'Trống'
                        FROM Phong P
                        INNER JOIN ChiTietDatPhong CT
                            ON P.SoPhong = CT.SoPhong
                        WHERE CT.SoPhieuDat = @SoPhieu
                          AND P.TrangThai <> N'Bảo trì'",
                        conn, tran);

                    cmd2.Parameters.AddWithValue(
                        "@SoPhieu",
                        soPhieu);

                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd3 = new SqlCommand(@"
                        UPDATE HoaDon
                        SET TrangThai = N'Đã thanh toán'
                        WHERE MaHoaDon = @MaHD",
                        conn, tran);

                    cmd3.Parameters.AddWithValue(
                        "@MaHD",
                        maHoaDon);

                    cmd3.ExecuteNonQuery();

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

                MessageBox.Show(
                    "Trả phòng thành công.");

                maHoaDon = null;

                LoadPhieuDangO();
                ClearMoney();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ClearMoney()
        {
            txtTienPhong.Text = "0";
            txtTienDichVu.Text = "0";
            txtTienDenBu.Text = "0";
            txtTienCoc.Text = "0";
            txtTongThanhToan.Text = "0";
            txtDaThanhToan.Text = "0";
            txtConLai.Text = "0";

            dgvPhong.DataSource = null;
            dgvDichVu.DataSource = null;
            dgvDenBu.DataSource = null;
            dgvThanhToan.DataSource = null;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(
                "Có lỗi xảy ra:\n\n" + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}