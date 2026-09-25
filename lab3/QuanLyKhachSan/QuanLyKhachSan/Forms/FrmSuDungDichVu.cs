using Microsoft.Data.SqlClient;
using QuanLyKhachSan.Data;
using System.Data;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmSuDungDichVu : Form
    {
        private decimal donGiaHienTai = 0;

        public FrmSuDungDichVu()
        {
            InitializeComponent();
        }

        // =========================================================
        // LOAD FORM
        // =========================================================

        private void FrmSuDungDichVu_Load(
            object sender,
            EventArgs e)
        {
            dtpNgaySuDung.Value =
                DateTime.Today;

            LoadPhieuDangO();
            LoadNhanVien();
            LoadDichVu();

            cboPhong.DataSource = null;

            dgvChiTiet.DataSource = null;

            TinhThanhTien();
        }

        // =========================================================
        // PHIẾU ĐANG Ở
        // =========================================================

        private void LoadPhieuDangO()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    P.SoPhieuDat,

                    P.SoPhieuDat
                    + N' - '
                    + K.HoTen AS HienThi

                FROM PhieuDatPhong P

                INNER JOIN KhachHang K
                    ON P.MaKhach = K.MaKhach

                WHERE P.TrangThai = N'Đang ở'

                ORDER BY P.SoPhieuDat");

            cboPhieuDat.DataSource = dt;

            cboPhieuDat.DisplayMember =
                "HienThi";

            cboPhieuDat.ValueMember =
                "SoPhieuDat";

            cboPhieuDat.SelectedIndex = -1;
        }

        // =========================================================
        // NHÂN VIÊN
        // =========================================================

        private void LoadNhanVien()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    MaNV,

                    MaNV
                    + N' - '
                    + HoTen AS HienThi

                FROM NhanVien

                WHERE TrangThai = N'Đang làm'

                ORDER BY MaNV");

            cboNhanVien.DataSource = dt;

            cboNhanVien.DisplayMember =
                "HienThi";

            cboNhanVien.ValueMember =
                "MaNV";

            cboNhanVien.SelectedIndex = -1;
        }

        // =========================================================
        // DỊCH VỤ
        // =========================================================

        private void LoadDichVu()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    MaDV,
                    TenDV,
                    DonViTinh,
                    DonGia,

                    MaDV
                    + N' - '
                    + TenDV
                    + N' ('
                    + DonViTinh
                    + N')' AS HienThi

                FROM DichVu

                ORDER BY MaDV");

            cboDichVu.DataSource = dt;

            cboDichVu.DisplayMember =
                "HienThi";

            cboDichVu.ValueMember =
                "MaDV";

            cboDichVu.SelectedIndex = -1;
        }

        // =========================================================
        // CHỌN PHIẾU
        // =========================================================

        private void cboPhieuDat_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cboPhieuDat.SelectedValue == null ||
                cboPhieuDat.SelectedValue is DataRowView)
            {
                cboPhong.DataSource = null;
                dgvChiTiet.DataSource = null;

                return;
            }

            LoadPhongTheoPhieu();
        }

        private void LoadPhongTheoPhieu()
        {
            string soPhieu =
                cboPhieuDat.SelectedValue!.ToString()!;

            DataTable dt = Db.Query(@"
                SELECT
                    CT.SoPhong,

                    CT.SoPhong
                    + N' - '
                    + P.LoaiPhong AS HienThi

                FROM ChiTietDatPhong CT

                INNER JOIN Phong P
                    ON CT.SoPhong = P.SoPhong

                WHERE CT.SoPhieuDat = @SoPhieu

                ORDER BY CT.SoPhong",

                new SqlParameter(
                    "@SoPhieu",
                    soPhieu));

            cboPhong.DataSource = dt;

            cboPhong.DisplayMember =
                "HienThi";

            cboPhong.ValueMember =
                "SoPhong";

            cboPhong.SelectedIndex = -1;

            dgvChiTiet.DataSource = null;

            CapNhatTongTien();
        }

        // =========================================================
        // CHỌN PHÒNG
        // =========================================================

        private void cboPhong_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            LoadChiTietDichVu();
        }

        private void dtpNgaySuDung_ValueChanged(
            object sender,
            EventArgs e)
        {
            LoadChiTietDichVu();
        }

        // =========================================================
        // CHỌN DỊCH VỤ
        // =========================================================

        private void cboDichVu_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            donGiaHienTai = 0;

            if (cboDichVu.SelectedItem is DataRowView row)
            {
                donGiaHienTai =
                    Convert.ToDecimal(
                        row["DonGia"]);
            }

            txtDonGia.Text =
                donGiaHienTai.ToString("N0")
                + " VNĐ";

            TinhThanhTien();
        }

        private void nudSoLuong_ValueChanged(
            object sender,
            EventArgs e)
        {
            TinhThanhTien();
        }

        private void TinhThanhTien()
        {
            decimal thanhTien =
                donGiaHienTai *
                nudSoLuong.Value;

            txtThanhTien.Text =
                thanhTien.ToString("N0")
                + " VNĐ";
        }

        // =========================================================
        // THÊM DỊCH VỤ
        // =========================================================

        private void btnThemDichVu_Click(
            object sender,
            EventArgs e)
        {
            if (cboPhieuDat.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn phiếu đặt.");

                return;
            }

            if (cboPhong.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn phòng.");

                return;
            }

            if (cboDichVu.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn dịch vụ.");

                return;
            }

            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên.");

                return;
            }

            try
            {
                string soPhieu =
                    cboPhieuDat.SelectedValue
                        .ToString()!;

                string soPhong =
                    cboPhong.SelectedValue
                        .ToString()!;

                // Kiểm tra ngày sử dụng có nằm trong
                // thời gian lưu trú hay không.

                DataTable dt = Db.Query(@"
                    SELECT
                        NgayNhan,
                        NgayTraDuKien,
                        NgayNhanThucTe

                    FROM PhieuDatPhong

                    WHERE SoPhieuDat = @SoPhieu",

                    new SqlParameter(
                        "@SoPhieu",
                        soPhieu));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy phiếu đặt.");

                    return;
                }

                DataRow booking = dt.Rows[0];

                DateTime ngayBatDau;

                if (booking["NgayNhanThucTe"] != DBNull.Value)
                {
                    ngayBatDau =
                        Convert.ToDateTime(
                            booking["NgayNhanThucTe"])
                        .Date;
                }
                else
                {
                    ngayBatDau =
                        Convert.ToDateTime(
                            booking["NgayNhan"])
                        .Date;
                }

                DateTime ngayKetThuc =
                    Convert.ToDateTime(
                        booking["NgayTraDuKien"])
                    .Date;

                DateTime ngaySuDung =
                    dtpNgaySuDung.Value.Date;

                if (ngaySuDung < ngayBatDau ||
                    ngaySuDung > ngayKetThuc)
                {
                    MessageBox.Show(
                        "Ngày sử dụng dịch vụ không nằm trong thời gian lưu trú.");

                    return;
                }

                // Gọi Stored Procedure

                using SqlConnection conn =
                    Db.GetConnection();

                conn.Open();

                using SqlCommand cmd =
                    new SqlCommand(
                        "sp_ThemDichVu",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@SoPhieuDat",
                    soPhieu);

                cmd.Parameters.AddWithValue(
                    "@SoPhong",
                    soPhong);

                cmd.Parameters.AddWithValue(
                    "@NgaySuDung",
                    ngaySuDung);

                cmd.Parameters.AddWithValue(
                    "@MaDV",
                    cboDichVu.SelectedValue);

                cmd.Parameters.AddWithValue(
                    "@SoLuong",
                    Convert.ToInt32(
                        nudSoLuong.Value));

                cmd.Parameters.AddWithValue(
                    "@MaNV",
                    cboNhanVien.SelectedValue);

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "Thêm dịch vụ thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadChiTietDichVu();

                cboDichVu.SelectedIndex = -1;

                nudSoLuong.Value = 1;

                donGiaHienTai = 0;

                txtDonGia.Clear();
                txtThanhTien.Clear();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =========================================================
        // LOAD DỊCH VỤ ĐÃ DÙNG
        // =========================================================

        private void LoadChiTietDichVu()
        {
            if (cboPhieuDat.SelectedValue == null ||
                cboPhong.SelectedValue == null ||
                cboPhieuDat.SelectedValue is DataRowView ||
                cboPhong.SelectedValue is DataRowView)
            {
                dgvChiTiet.DataSource = null;

                CapNhatTongTien();

                return;
            }

            try
            {
                dgvChiTiet.DataSource =
                    Db.Query(@"
                        SELECT
                            SD.SoPhieuSDDV
                                AS [Số phiếu SD],

                            SD.NgaySuDung
                                AS [Ngày sử dụng],

                            CT.MaDV
                                AS [Mã DV],

                            DV.TenDV
                                AS [Dịch vụ],

                            DV.DonViTinh
                                AS [Đơn vị],

                            CT.SoLuong
                                AS [Số lượng],

                            CT.DonGia
                                AS [Đơn giá],

                            CT.ThanhTien
                                AS [Thành tiền],

                            NV.HoTen
                                AS [Nhân viên]

                        FROM PhieuSuDungDV SD

                        INNER JOIN ChiTietSuDungDV CT
                            ON SD.SoPhieuSDDV =
                               CT.SoPhieuSDDV

                        INNER JOIN DichVu DV
                            ON CT.MaDV = DV.MaDV

                        INNER JOIN NhanVien NV
                            ON SD.MaNV = NV.MaNV

                        WHERE SD.SoPhieuDat =
                              @SoPhieu

                          AND SD.SoPhong =
                              @SoPhong

                        ORDER BY
                            SD.NgaySuDung DESC,
                            DV.TenDV",

                        new SqlParameter(
                            "@SoPhieu",
                            cboPhieuDat.SelectedValue),

                        new SqlParameter(
                            "@SoPhong",
                            cboPhong.SelectedValue)
                    );

                CapNhatTongTien();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =========================================================
        // TỔNG TIỀN
        // =========================================================

        private void CapNhatTongTien()
        {
            if (cboPhieuDat.SelectedValue == null ||
                cboPhong.SelectedValue == null ||
                cboPhieuDat.SelectedValue is DataRowView ||
                cboPhong.SelectedValue is DataRowView)
            {
                lblTongTien.Text = "0 VNĐ";
                return;
            }

            try
            {
                object? result = Db.Scalar(@"
                    SELECT
                        ISNULL(
                            SUM(CT.ThanhTien),
                            0
                        )

                    FROM PhieuSuDungDV SD

                    INNER JOIN ChiTietSuDungDV CT
                        ON SD.SoPhieuSDDV =
                           CT.SoPhieuSDDV

                    WHERE SD.SoPhieuDat = @SoPhieu
                      AND SD.SoPhong = @SoPhong",

                    new SqlParameter(
                        "@SoPhieu",
                        cboPhieuDat.SelectedValue),

                    new SqlParameter(
                        "@SoPhong",
                        cboPhong.SelectedValue));

                decimal tong =
                    Convert.ToDecimal(result);

                lblTongTien.Text =
                    tong.ToString("N0")
                    + " VNĐ";
            }
            catch
            {
                lblTongTien.Text = "0 VNĐ";
            }
        }

        // =========================================================
        // XÓA CHI TIẾT DỊCH VỤ
        // =========================================================

        private void btnXoaChiTiet_Click(
            object sender,
            EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null)
            {
                MessageBox.Show(
                    "Chọn dịch vụ cần xóa.");

                return;
            }

            string soPhieuSD =
                dgvChiTiet.CurrentRow
                    .Cells["Số phiếu SD"]
                    .Value
                    .ToString()!;

            string maDV =
                dgvChiTiet.CurrentRow
                    .Cells["Mã DV"]
                    .Value
                    .ToString()!;

            string tenDV =
                dgvChiTiet.CurrentRow
                    .Cells["Dịch vụ"]
                    .Value
                    .ToString()!;

            DialogResult confirm =
                MessageBox.Show(
                    $"Xóa dịch vụ \"{tenDV}\"?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using SqlConnection conn =
                    Db.GetConnection();

                conn.Open();

                using SqlTransaction tran =
                    conn.BeginTransaction();

                try
                {
                    using SqlCommand cmdDelete =
                        new SqlCommand(@"
                            DELETE FROM ChiTietSuDungDV

                            WHERE SoPhieuSDDV =
                                  @SoPhieuSD

                              AND MaDV =
                                  @MaDV",
                            conn,
                            tran);

                    cmdDelete.Parameters.AddWithValue(
                        "@SoPhieuSD",
                        soPhieuSD);

                    cmdDelete.Parameters.AddWithValue(
                        "@MaDV",
                        maDV);

                    cmdDelete.ExecuteNonQuery();

                    // Nếu phiếu không còn dịch vụ nào
                    // thì xóa luôn phiếu rỗng.

                    using SqlCommand cmdCount =
                        new SqlCommand(@"
                            SELECT COUNT(*)

                            FROM ChiTietSuDungDV

                            WHERE SoPhieuSDDV =
                                  @SoPhieuSD",
                            conn,
                            tran);

                    cmdCount.Parameters.AddWithValue(
                        "@SoPhieuSD",
                        soPhieuSD);

                    int count =
                        Convert.ToInt32(
                            cmdCount.ExecuteScalar());

                    if (count == 0)
                    {
                        using SqlCommand cmdDeletePhieu =
                            new SqlCommand(@"
                                DELETE FROM PhieuSuDungDV

                                WHERE SoPhieuSDDV =
                                      @SoPhieuSD",
                                conn,
                                tran);

                        cmdDeletePhieu.Parameters.AddWithValue(
                            "@SoPhieuSD",
                            soPhieuSD);

                        cmdDeletePhieu.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

                MessageBox.Show(
                    "Xóa dịch vụ thành công.");

                LoadChiTietDichVu();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =========================================================
        // LÀM MỚI
        // =========================================================

        private void btnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            cboDichVu.SelectedIndex = -1;

            nudSoLuong.Value = 1;

            donGiaHienTai = 0;

            txtDonGia.Clear();
            txtThanhTien.Clear();

            dtpNgaySuDung.Value =
                DateTime.Today;
        }

        // =========================================================
        // ĐÓNG
        // =========================================================

        private void btnDong_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(
                "Có lỗi xảy ra:\n\n"
                + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}