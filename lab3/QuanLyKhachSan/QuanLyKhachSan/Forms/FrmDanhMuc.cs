using Microsoft.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDanhMuc : Form
    {
        public FrmDanhMuc()
        {
            InitializeComponent();
        }

        // =========================================================
        // LOAD FORM
        // =========================================================

        private void FrmDanhMuc_Load(
            object sender,
            EventArgs e)
        {
            LoadKhuVuc();
            LoadKhachHang();
            LoadNhanVien();
            LoadDichVu();
        }

        // =========================================================
        // KHU VỰC
        // =========================================================

        private void LoadKhuVuc()
        {
            try
            {
                string sql = @"
                    SELECT
                        MaKhuVuc AS [Mã khu vực],
                        TenKhuVuc AS [Tên khu vực]
                    FROM KhuVuc
                    ORDER BY MaKhuVuc";

                dgvKhuVuc.DataSource =
                    Db.Query(sql);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnThemKhuVuc_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhuVuc.Text) ||
                string.IsNullOrWhiteSpace(txtTenKhuVuc.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin.");

                return;
            }

            try
            {
                string sql = @"
                    INSERT INTO KhuVuc
                    (
                        MaKhuVuc,
                        TenKhuVuc
                    )
                    VALUES
                    (
                        @Ma,
                        @Ten
                    )";

                Db.Execute(
                    sql,
                    new SqlParameter(
                        "@Ma",
                        txtMaKhuVuc.Text.Trim()),

                    new SqlParameter(
                        "@Ten",
                        txtTenKhuVuc.Text.Trim())
                );

                MessageBox.Show(
                    "Thêm khu vực thành công.");

                LoadKhuVuc();
                ClearKhuVuc();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnSuaKhuVuc_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string sql = @"
                    UPDATE KhuVuc

                    SET TenKhuVuc = @Ten

                    WHERE MaKhuVuc = @Ma";

                int result = Db.Execute(
                    sql,

                    new SqlParameter(
                        "@Ten",
                        txtTenKhuVuc.Text.Trim()),

                    new SqlParameter(
                        "@Ma",
                        txtMaKhuVuc.Text.Trim())
                );

                if (result > 0)
                {
                    MessageBox.Show(
                        "Cập nhật khu vực thành công.");

                    LoadKhuVuc();
                    ClearKhuVuc();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnXoaKhuVuc_Click(
            object sender,
            EventArgs e)
        {
            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa khu vực này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string sql = @"
                    DELETE FROM KhuVuc
                    WHERE MaKhuVuc = @Ma";

                Db.Execute(
                    sql,
                    new SqlParameter(
                        "@Ma",
                        txtMaKhuVuc.Text.Trim()));

                MessageBox.Show(
                    "Xóa khu vực thành công.");

                LoadKhuVuc();
                ClearKhuVuc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa khu vực.\n\n" +
                    "Khu vực có thể đang được sử dụng bởi phòng.\n\n" +
                    ex.Message);
            }
        }

        private void dgvKhuVuc_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvKhuVuc.Rows[e.RowIndex];

            txtMaKhuVuc.Text =
                row.Cells["Mã khu vực"].Value?.ToString();

            txtTenKhuVuc.Text =
                row.Cells["Tên khu vực"].Value?.ToString();
        }

        private void btnLamMoiKhuVuc_Click(
            object sender,
            EventArgs e)
        {
            ClearKhuVuc();
        }

        private void ClearKhuVuc()
        {
            txtMaKhuVuc.Clear();
            txtTenKhuVuc.Clear();

            txtMaKhuVuc.Focus();
        }

        // =========================================================
        // KHÁCH HÀNG
        // =========================================================

        private void LoadKhachHang()
        {
            try
            {
                string sql = @"
                    SELECT
                        MaKhach AS [Mã khách],
                        HoTen AS [Họ tên],
                        CCCD,
                        SoDienThoai AS [Điện thoại],
                        DiaChi AS [Địa chỉ],
                        QuocTich AS [Quốc tịch]

                    FROM KhachHang

                    ORDER BY MaKhach";

                dgvKhachHang.DataSource =
                    Db.Query(sql);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnThemKhach_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhach.Text) ||
                string.IsNullOrWhiteSpace(txtHoTenKhach.Text) ||
                string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show(
                    "Mã khách, họ tên và CCCD không được để trống.");

                return;
            }

            try
            {
                string sql = @"
                    INSERT INTO KhachHang
                    (
                        MaKhach,
                        HoTen,
                        CCCD,
                        SoDienThoai,
                        DiaChi,
                        QuocTich
                    )

                    VALUES
                    (
                        @Ma,
                        @HoTen,
                        @CCCD,
                        @SDT,
                        @DiaChi,
                        @QuocTich
                    )";

                Db.Execute(
                    sql,

                    new SqlParameter(
                        "@Ma",
                        txtMaKhach.Text.Trim()),

                    new SqlParameter(
                        "@HoTen",
                        txtHoTenKhach.Text.Trim()),

                    new SqlParameter(
                        "@CCCD",
                        txtCCCD.Text.Trim()),

                    new SqlParameter(
                        "@SDT",
                        txtSDTKhach.Text.Trim()),

                    new SqlParameter(
                        "@DiaChi",
                        txtDiaChi.Text.Trim()),

                    new SqlParameter(
                        "@QuocTich",
                        txtQuocTich.Text.Trim())
                );

                MessageBox.Show(
                    "Thêm khách hàng thành công.");

                LoadKhachHang();
                ClearKhachHang();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnSuaKhach_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string sql = @"
                    UPDATE KhachHang

                    SET
                        HoTen = @HoTen,
                        CCCD = @CCCD,
                        SoDienThoai = @SDT,
                        DiaChi = @DiaChi,
                        QuocTich = @QuocTich

                    WHERE MaKhach = @Ma";

                Db.Execute(
                    sql,

                    new SqlParameter(
                        "@HoTen",
                        txtHoTenKhach.Text.Trim()),

                    new SqlParameter(
                        "@CCCD",
                        txtCCCD.Text.Trim()),

                    new SqlParameter(
                        "@SDT",
                        txtSDTKhach.Text.Trim()),

                    new SqlParameter(
                        "@DiaChi",
                        txtDiaChi.Text.Trim()),

                    new SqlParameter(
                        "@QuocTich",
                        txtQuocTich.Text.Trim()),

                    new SqlParameter(
                        "@Ma",
                        txtMaKhach.Text.Trim())
                );

                MessageBox.Show(
                    "Cập nhật khách hàng thành công.");

                LoadKhachHang();
                ClearKhachHang();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnXoaKhach_Click(
            object sender,
            EventArgs e)
        {
            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa khách hàng?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Db.Execute(
                    @"DELETE FROM KhachHang
                      WHERE MaKhach = @Ma",

                    new SqlParameter(
                        "@Ma",
                        txtMaKhach.Text.Trim()));

                MessageBox.Show(
                    "Xóa khách hàng thành công.");

                LoadKhachHang();
                ClearKhachHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa khách hàng.\n" +
                    "Khách hàng có thể đã có phiếu đặt phòng.\n\n" +
                    ex.Message);
            }
        }

        private void dgvKhachHang_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvKhachHang.Rows[e.RowIndex];

            txtMaKhach.Text =
                row.Cells["Mã khách"].Value?.ToString();

            txtHoTenKhach.Text =
                row.Cells["Họ tên"].Value?.ToString();

            txtCCCD.Text =
                row.Cells["CCCD"].Value?.ToString();

            txtSDTKhach.Text =
                row.Cells["Điện thoại"].Value?.ToString();

            txtDiaChi.Text =
                row.Cells["Địa chỉ"].Value?.ToString();

            txtQuocTich.Text =
                row.Cells["Quốc tịch"].Value?.ToString();
        }

        private void btnLamMoiKhach_Click(
            object sender,
            EventArgs e)
        {
            ClearKhachHang();
        }

        private void ClearKhachHang()
        {
            txtMaKhach.Clear();
            txtHoTenKhach.Clear();
            txtCCCD.Clear();
            txtSDTKhach.Clear();
            txtDiaChi.Clear();

            txtQuocTich.Text = "Việt Nam";

            txtMaKhach.Focus();
        }

        // =========================================================
        // NHÂN VIÊN
        // =========================================================

        private void LoadNhanVien()
        {
            try
            {
                dgvNhanVien.DataSource =
                    Db.Query(@"
                        SELECT
                            MaNV AS [Mã NV],
                            HoTen AS [Họ tên],
                            ChucVu AS [Chức vụ],
                            SoDienThoai AS [Điện thoại],
                            TrangThai AS [Trạng thái]

                        FROM NhanVien

                        ORDER BY MaNV");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnThemNV_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                Db.Execute(
                    @"INSERT INTO NhanVien
                    (
                        MaNV,
                        HoTen,
                        ChucVu,
                        SoDienThoai,
                        TrangThai
                    )

                    VALUES
                    (
                        @Ma,
                        @HoTen,
                        @ChucVu,
                        @SDT,
                        @TrangThai
                    )",

                    new SqlParameter(
                        "@Ma",
                        txtMaNV.Text.Trim()),

                    new SqlParameter(
                        "@HoTen",
                        txtHoTenNV.Text.Trim()),

                    new SqlParameter(
                        "@ChucVu",
                        txtChucVu.Text.Trim()),

                    new SqlParameter(
                        "@SDT",
                        txtSDTNV.Text.Trim()),

                    new SqlParameter(
                        "@TrangThai",
                        cboTrangThaiNV.Text)
                );

                MessageBox.Show(
                    "Thêm nhân viên thành công.");

                LoadNhanVien();
                ClearNhanVien();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnSuaNV_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                Db.Execute(
                    @"UPDATE NhanVien

                      SET
                          HoTen = @HoTen,
                          ChucVu = @ChucVu,
                          SoDienThoai = @SDT,
                          TrangThai = @TrangThai

                      WHERE MaNV = @Ma",

                    new SqlParameter(
                        "@HoTen",
                        txtHoTenNV.Text.Trim()),

                    new SqlParameter(
                        "@ChucVu",
                        txtChucVu.Text.Trim()),

                    new SqlParameter(
                        "@SDT",
                        txtSDTNV.Text.Trim()),

                    new SqlParameter(
                        "@TrangThai",
                        cboTrangThaiNV.Text),

                    new SqlParameter(
                        "@Ma",
                        txtMaNV.Text.Trim())
                );

                MessageBox.Show(
                    "Cập nhật nhân viên thành công.");

                LoadNhanVien();
                ClearNhanVien();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnXoaNV_Click(
            object sender,
            EventArgs e)
        {
            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa nhân viên?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Db.Execute(
                    @"DELETE FROM NhanVien
                      WHERE MaNV = @Ma",

                    new SqlParameter(
                        "@Ma",
                        txtMaNV.Text.Trim()));

                MessageBox.Show(
                    "Xóa nhân viên thành công.");

                LoadNhanVien();
                ClearNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa nhân viên vì có thể đang được sử dụng.\n\n"
                    + ex.Message);
            }
        }

        private void dgvNhanVien_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvNhanVien.Rows[e.RowIndex];

            txtMaNV.Text =
                row.Cells["Mã NV"].Value?.ToString();

            txtHoTenNV.Text =
                row.Cells["Họ tên"].Value?.ToString();

            txtChucVu.Text =
                row.Cells["Chức vụ"].Value?.ToString();

            txtSDTNV.Text =
                row.Cells["Điện thoại"].Value?.ToString();

            cboTrangThaiNV.Text =
                row.Cells["Trạng thái"].Value?.ToString();
        }

        private void btnLamMoiNV_Click(
            object sender,
            EventArgs e)
        {
            ClearNhanVien();
        }

        private void ClearNhanVien()
        {
            txtMaNV.Clear();
            txtHoTenNV.Clear();
            txtChucVu.Clear();
            txtSDTNV.Clear();

            cboTrangThaiNV.SelectedIndex = 0;

            txtMaNV.Focus();
        }

        // =========================================================
        // DỊCH VỤ
        // =========================================================

        private void LoadDichVu()
        {
            try
            {
                dgvDichVu.DataSource =
                    Db.Query(@"
                        SELECT
                            MaDV AS [Mã DV],
                            TenDV AS [Tên dịch vụ],
                            DonViTinh AS [Đơn vị tính],
                            DonGia AS [Đơn giá]

                        FROM DichVu

                        ORDER BY MaDV");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnThemDV_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                Db.Execute(
                    @"INSERT INTO DichVu
                    (
                        MaDV,
                        TenDV,
                        DonViTinh,
                        DonGia
                    )

                    VALUES
                    (
                        @Ma,
                        @Ten,
                        @DonVi,
                        @Gia
                    )",

                    new SqlParameter(
                        "@Ma",
                        txtMaDV.Text.Trim()),

                    new SqlParameter(
                        "@Ten",
                        txtTenDV.Text.Trim()),

                    new SqlParameter(
                        "@DonVi",
                        txtDonViTinh.Text.Trim()),

                    new SqlParameter(
                        "@Gia",
                        nudDonGia.Value)
                );

                MessageBox.Show(
                    "Thêm dịch vụ thành công.");

                LoadDichVu();
                ClearDichVu();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnSuaDV_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                Db.Execute(
                    @"UPDATE DichVu

                      SET
                          TenDV = @Ten,
                          DonViTinh = @DonVi,
                          DonGia = @Gia

                      WHERE MaDV = @Ma",

                    new SqlParameter(
                        "@Ten",
                        txtTenDV.Text.Trim()),

                    new SqlParameter(
                        "@DonVi",
                        txtDonViTinh.Text.Trim()),

                    new SqlParameter(
                        "@Gia",
                        nudDonGia.Value),

                    new SqlParameter(
                        "@Ma",
                        txtMaDV.Text.Trim())
                );

                MessageBox.Show(
                    "Cập nhật dịch vụ thành công.");

                LoadDichVu();
                ClearDichVu();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnXoaDV_Click(
            object sender,
            EventArgs e)
        {
            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa dịch vụ?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Db.Execute(
                    @"DELETE FROM DichVu
                      WHERE MaDV = @Ma",

                    new SqlParameter(
                        "@Ma",
                        txtMaDV.Text.Trim()));

                MessageBox.Show(
                    "Xóa dịch vụ thành công.");

                LoadDichVu();
                ClearDichVu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa dịch vụ vì đã phát sinh sử dụng.\n\n"
                    + ex.Message);
            }
        }

        private void dgvDichVu_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvDichVu.Rows[e.RowIndex];

            txtMaDV.Text =
                row.Cells["Mã DV"].Value?.ToString();

            txtTenDV.Text =
                row.Cells["Tên dịch vụ"].Value?.ToString();

            txtDonViTinh.Text =
                row.Cells["Đơn vị tính"].Value?.ToString();

            if (decimal.TryParse(
                    row.Cells["Đơn giá"].Value?.ToString(),
                    out decimal gia))
            {
                nudDonGia.Value = gia;
            }
        }

        private void btnLamMoiDV_Click(
            object sender,
            EventArgs e)
        {
            ClearDichVu();
        }

        private void ClearDichVu()
        {
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtDonViTinh.Clear();

            nudDonGia.Value = 0;

            txtMaDV.Focus();
        }

        // =========================================================
        // CHUNG
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
                "Có lỗi xảy ra:\n\n" + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}