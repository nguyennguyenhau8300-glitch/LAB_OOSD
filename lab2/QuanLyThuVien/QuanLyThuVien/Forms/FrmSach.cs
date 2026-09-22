using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms
{
    public partial class FrmSach : Form
    {
        public FrmSach()
        {
            InitializeComponent();

            // Gắn sự kiện tại đây, không gắn lại trong Designer
            this.Load += FrmSach_Load;
            this.dgvSach.CellClick += dgvSach_CellClick;
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;
            this.btnDong.Click += btnDong_Click;
        }

        // =====================================================
        // FORM LOAD
        // =====================================================
        private void FrmSach_Load(object sender, EventArgs e)
        {
            LoadTheLoai();
            LoadNhaXuatBan();
            LoadDanhSachSach();
        }

        // =====================================================
        // LOAD THỂ LOẠI
        // =====================================================
        private void LoadTheLoai()
        {
            try
            {
                string sql = @"
                    SELECT
                        MaTheLoai,
                        TenTheLoai
                    FROM dbo.TheLoai
                    ORDER BY MaTheLoai";

                DataTable dt = Db.Query(sql);

                cboTheLoai.DataSource = null;
                cboTheLoai.DataSource = dt;
                cboTheLoai.DisplayMember = "TenTheLoai";
                cboTheLoai.ValueMember = "MaTheLoai";
                cboTheLoai.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải thể loại:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // LOAD NHÀ XUẤT BẢN
        // =====================================================
        private void LoadNhaXuatBan()
        {
            try
            {
                string sql = @"
                    SELECT
                        MaNhaXuatBan,
                        DiaChi,
                        SoDienThoai
                    FROM dbo.NhaXuatBan
                    ORDER BY MaNhaXuatBan";

                DataTable dt = Db.Query(sql);

                cboNhaXuatBan.DataSource = null;
                cboNhaXuatBan.DataSource = dt;

                // ComboBox chỉ hiển thị mã NXB
                cboNhaXuatBan.DisplayMember = "MaNhaXuatBan";
                cboNhaXuatBan.ValueMember = "MaNhaXuatBan";
                cboNhaXuatBan.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải nhà xuất bản:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // LOAD DANH SÁCH SÁCH
        // =====================================================
        private void LoadDanhSachSach()
        {
            try
            {
                string sql = @"
                    SELECT
                        ds.MaDauSach AS [Mã sách],
                        ds.TenSach AS [Tên sách],
                        ds.NamXuatBan AS [Năm xuất bản],
                        ds.SoLuongHienCo AS [Số lượng],
                        tl.TenTheLoai AS [Thể loại],
                        nxb.MaNhaXuatBan AS [Nhà xuất bản]
                    FROM dbo.DauSach AS ds
                    INNER JOIN dbo.TheLoai AS tl
                        ON ds.MaTheLoai = tl.MaTheLoai
                    INNER JOIN dbo.NhaXuatBan AS nxb
                        ON ds.MaNhaXuatBan = nxb.MaNhaXuatBan
                    ORDER BY ds.MaDauSach";

                DataTable dt = Db.Query(sql);

                dgvSach.DataSource = null;
                dgvSach.DataSource = dt;

                dgvSach.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvSach.ReadOnly = true;
                dgvSach.AllowUserToAddRows = false;
                dgvSach.AllowUserToDeleteRows = false;
                dgvSach.MultiSelect = false;

                dgvSach.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvSach.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách sách:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CLICK VÀO SÁCH
        // =====================================================
        private void dgvSach_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row =
                    dgvSach.Rows[e.RowIndex];

                txtMaDauSach.Text =
                    row.Cells["Mã sách"].Value?.ToString() ?? "";

                txtTenSach.Text =
                    row.Cells["Tên sách"].Value?.ToString() ?? "";

                txtNamXuatBan.Text =
                    row.Cells["Năm xuất bản"].Value?.ToString() ?? "";

                txtSoLuong.Text =
                    row.Cells["Số lượng"].Value?.ToString() ?? "";

                string maSach =
                    txtMaDauSach.Text.Trim();

                if (string.IsNullOrWhiteSpace(maSach))
                    return;

                string sql = @"
                    SELECT
                        MaTheLoai,
                        MaNhaXuatBan
                    FROM dbo.DauSach
                    WHERE MaDauSach = @MaDauSach";

                DataTable dt = Db.Query(
                    sql,
                    new SqlParameter(
                        "@MaDauSach",
                        maSach));

                if (dt.Rows.Count > 0)
                {
                    cboTheLoai.SelectedValue =
                        dt.Rows[0]["MaTheLoai"].ToString();

                    cboNhaXuatBan.SelectedValue =
                        dt.Rows[0]["MaNhaXuatBan"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi chọn sách:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // THÊM SÁCH
        // =====================================================
        private void btnThem_Click(
            object sender,
            EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            try
            {
                string maSach =
                    txtMaDauSach.Text.Trim();

                string sqlCheck = @"
                    SELECT COUNT(*)
                    FROM dbo.DauSach
                    WHERE MaDauSach = @MaDauSach";

                int tonTai =
                    Convert.ToInt32(
                        Db.Scalar(
                            sqlCheck,
                            new SqlParameter(
                                "@MaDauSach",
                                maSach)));

                if (tonTai > 0)
                {
                    MessageBox.Show(
                        "Mã sách đã tồn tại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string sql = @"
                    INSERT INTO dbo.DauSach
                    (
                        MaDauSach,
                        TenSach,
                        NamXuatBan,
                        SoLuongHienCo,
                        MaTheLoai,
                        MaNhaXuatBan
                    )
                    VALUES
                    (
                        @MaDauSach,
                        @TenSach,
                        @NamXuatBan,
                        @SoLuongHienCo,
                        @MaTheLoai,
                        @MaNhaXuatBan
                    )";

                Db.Execute(
                    sql,

                    new SqlParameter(
                        "@MaDauSach",
                        maSach),

                    new SqlParameter(
                        "@TenSach",
                        txtTenSach.Text.Trim()),

                    new SqlParameter(
                        "@NamXuatBan",
                        Convert.ToInt32(
                            txtNamXuatBan.Text.Trim())),

                    new SqlParameter(
                        "@SoLuongHienCo",
                        Convert.ToInt32(
                            txtSoLuong.Text.Trim())),

                    new SqlParameter(
                        "@MaTheLoai",
                        cboTheLoai.SelectedValue.ToString()),

                    new SqlParameter(
                        "@MaNhaXuatBan",
                        cboNhaXuatBan.SelectedValue.ToString())
                );

                MessageBox.Show(
                    "Thêm sách thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadDanhSachSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thêm sách:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // SỬA SÁCH
        // =====================================================
        private void btnSua_Click(
            object sender,
            EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            try
            {
                string sql = @"
                    UPDATE dbo.DauSach
                    SET
                        TenSach = @TenSach,
                        NamXuatBan = @NamXuatBan,
                        SoLuongHienCo = @SoLuongHienCo,
                        MaTheLoai = @MaTheLoai,
                        MaNhaXuatBan = @MaNhaXuatBan
                    WHERE MaDauSach = @MaDauSach";

                int ketQua =
                    Db.Execute(
                        sql,

                        new SqlParameter(
                            "@MaDauSach",
                            txtMaDauSach.Text.Trim()),

                        new SqlParameter(
                            "@TenSach",
                            txtTenSach.Text.Trim()),

                        new SqlParameter(
                            "@NamXuatBan",
                            Convert.ToInt32(
                                txtNamXuatBan.Text.Trim())),

                        new SqlParameter(
                            "@SoLuongHienCo",
                            Convert.ToInt32(
                                txtSoLuong.Text.Trim())),

                        new SqlParameter(
                            "@MaTheLoai",
                            cboTheLoai.SelectedValue.ToString()),

                        new SqlParameter(
                            "@MaNhaXuatBan",
                            cboNhaXuatBan.SelectedValue.ToString())
                    );

                if (ketQua > 0)
                {
                    MessageBox.Show(
                        "Cập nhật sách thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadDanhSachSach();
                    XoaTrang();
                }
                else
                {
                    MessageBox.Show(
                        "Không tìm thấy sách cần sửa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi sửa sách:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // XÓA SÁCH
        // =====================================================
        private void btnXoa_Click(
            object sender,
            EventArgs e)
        {
            string maSach =
                txtMaDauSach.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSach))
            {
                MessageBox.Show(
                    "Vui lòng chọn sách cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc muốn xóa sách " +
                    maSach +
                    " không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                string sql = @"
                    DELETE FROM dbo.DauSach
                    WHERE MaDauSach = @MaDauSach";

                int ketQua =
                    Db.Execute(
                        sql,
                        new SqlParameter(
                            "@MaDauSach",
                            maSach));

                if (ketQua > 0)
                {
                    MessageBox.Show(
                        "Xóa sách thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadDanhSachSach();
                    XoaTrang();
                }
                else
                {
                    MessageBox.Show(
                        "Không tìm thấy sách cần xóa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Không thể xóa sách.\n\n" +
                    "Có thể sách đang được sử dụng trong phiếu mượn.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi xóa sách:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // KIỂM TRA DỮ LIỆU
        // =====================================================
        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(
                txtMaDauSach.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã sách!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMaDauSach.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtTenSach.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sách!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTenSach.Focus();
                return false;
            }

            int namXuatBan;

            if (!int.TryParse(
                txtNamXuatBan.Text.Trim(),
                out namXuatBan))
            {
                MessageBox.Show(
                    "Năm xuất bản phải là số!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNamXuatBan.Focus();
                return false;
            }

            if (namXuatBan <= 0)
            {
                MessageBox.Show(
                    "Năm xuất bản không hợp lệ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNamXuatBan.Focus();
                return false;
            }

            int soLuong;

            if (!int.TryParse(
                txtSoLuong.Text.Trim(),
                out soLuong))
            {
                MessageBox.Show(
                    "Số lượng phải là số!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSoLuong.Focus();
                return false;
            }

            if (soLuong < 0)
            {
                MessageBox.Show(
                    "Số lượng không được nhỏ hơn 0!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSoLuong.Focus();
                return false;
            }

            if (cboTheLoai.SelectedIndex < 0 ||
                cboTheLoai.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn thể loại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboTheLoai.Focus();
                return false;
            }

            if (cboNhaXuatBan.SelectedIndex < 0 ||
                cboNhaXuatBan.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhà xuất bản!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboNhaXuatBan.Focus();
                return false;
            }

            return true;
        }

        // =====================================================
        // XÓA TRẮNG
        // =====================================================
        private void XoaTrang()
        {
            txtMaDauSach.Clear();
            txtTenSach.Clear();
            txtNamXuatBan.Clear();
            txtSoLuong.Clear();

            cboTheLoai.SelectedIndex = -1;
            cboNhaXuatBan.SelectedIndex = -1;

            dgvSach.ClearSelection();

            txtMaDauSach.Focus();
        }

        // =====================================================
        // ĐÓNG
        // =====================================================
        private void btnDong_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}