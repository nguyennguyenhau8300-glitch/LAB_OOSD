using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms
{
    public partial class FrmDocGia : Form
    {
        public FrmDocGia()
        {
            InitializeComponent();
        }

        private void FrmDocGia_Load(object sender, EventArgs e)
        {
            LoadDocGia();
        }

        // =========================
        // LOAD ĐỘC GIẢ
        // =========================
        private void LoadDocGia()
        {
            try
            {
                dgvDocGia.DataSource = Db.Query(
                    @"SELECT
                        MaDocGia,
                        Ho,
                        Ten,
                        NgaySinh,
                        Phai,
                        SoDienThoai,
                        DiaChi,
                        Email,
                        Anh3x4
                      FROM DocGia
                      ORDER BY MaDocGia");

                dgvDocGia.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách độc giả: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // THÊM ĐỘC GIẢ
        // =========================
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDocGia.Text) ||
                    string.IsNullOrWhiteSpace(txtHo.Text) ||
                    string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show(
                        "Vui lòng nhập đầy đủ Mã độc giả, Họ và Tên.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string sql = @"
                    INSERT INTO DocGia
                    (
                        MaDocGia,
                        Ho,
                        Ten,
                        NgaySinh,
                        Phai,
                        SoDienThoai,
                        DiaChi,
                        Email,
                        Anh3x4
                    )
                    VALUES
                    (
                        @MaDocGia,
                        @Ho,
                        @Ten,
                        @NgaySinh,
                        @Phai,
                        @SoDienThoai,
                        @DiaChi,
                        @Email,
                        @Anh3x4
                    )";

                Db.Execute(
                    sql,
                    new SqlParameter("@MaDocGia", txtMaDocGia.Text.Trim()),
                    new SqlParameter("@Ho", txtHo.Text.Trim()),
                    new SqlParameter("@Ten", txtTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@Phai", txtPhai.Text.Trim()),
                    new SqlParameter("@SoDienThoai", txtSoDienThoai.Text.Trim()),
                    new SqlParameter("@DiaChi", txtDiaChi.Text.Trim()),
                    new SqlParameter("@Email", txtEmail.Text.Trim()),
                    new SqlParameter("@Anh3x4",
                        string.IsNullOrWhiteSpace(txtAnh3x4.Text)
                            ? (object)DBNull.Value
                            : txtAnh3x4.Text.Trim())
                );

                MessageBox.Show(
                    "Thêm độc giả thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadDocGia();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thêm độc giả: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // SỬA ĐỘC GIẢ
        // =========================
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDocGia.Text))
                {
                    MessageBox.Show(
                        "Vui lòng chọn độc giả cần sửa.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string sql = @"
                    UPDATE DocGia
                    SET
                        Ho = @Ho,
                        Ten = @Ten,
                        NgaySinh = @NgaySinh,
                        Phai = @Phai,
                        SoDienThoai = @SoDienThoai,
                        DiaChi = @DiaChi,
                        Email = @Email,
                        Anh3x4 = @Anh3x4
                    WHERE MaDocGia = @MaDocGia";

                Db.Execute(
                    sql,
                    new SqlParameter("@MaDocGia", txtMaDocGia.Text.Trim()),
                    new SqlParameter("@Ho", txtHo.Text.Trim()),
                    new SqlParameter("@Ten", txtTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@Phai", txtPhai.Text.Trim()),
                    new SqlParameter("@SoDienThoai", txtSoDienThoai.Text.Trim()),
                    new SqlParameter("@DiaChi", txtDiaChi.Text.Trim()),
                    new SqlParameter("@Email", txtEmail.Text.Trim()),
                    new SqlParameter("@Anh3x4",
                        string.IsNullOrWhiteSpace(txtAnh3x4.Text)
                            ? (object)DBNull.Value
                            : txtAnh3x4.Text.Trim())
                );

                MessageBox.Show(
                    "Cập nhật độc giả thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadDocGia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi sửa độc giả: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // XÓA ĐỘC GIẢ
        // =========================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDocGia.Text))
                {
                    MessageBox.Show(
                        "Vui lòng chọn độc giả cần xóa.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa độc giả này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                Db.Execute(
                    "DELETE FROM DocGia WHERE MaDocGia = @MaDocGia",
                    new SqlParameter(
                        "@MaDocGia",
                        txtMaDocGia.Text.Trim())
                );

                MessageBox.Show(
                    "Xóa độc giả thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadDocGia();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa độc giả.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // CHỌN DÒNG
        // =========================
        private void dgvDocGia_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvDocGia.Rows[e.RowIndex];

            txtMaDocGia.Text =
                row.Cells["MaDocGia"].Value?.ToString();

            txtHo.Text =
                row.Cells["Ho"].Value?.ToString();

            txtTen.Text =
                row.Cells["Ten"].Value?.ToString();

            if (row.Cells["NgaySinh"].Value != null &&
                row.Cells["NgaySinh"].Value != DBNull.Value)
            {
                dtpNgaySinh.Value =
                    Convert.ToDateTime(
                        row.Cells["NgaySinh"].Value);
            }

            txtPhai.Text =
                row.Cells["Phai"].Value?.ToString();

            txtSoDienThoai.Text =
                row.Cells["SoDienThoai"].Value?.ToString();

            txtDiaChi.Text =
                row.Cells["DiaChi"].Value?.ToString();

            txtEmail.Text =
                row.Cells["Email"].Value?.ToString();

            txtAnh3x4.Text =
                row.Cells["Anh3x4"].Value?.ToString();
        }

        // =========================
        // XÓA TRẮNG FORM
        // =========================
        private void ClearForm()
        {
            txtMaDocGia.Clear();
            txtHo.Clear();
            txtTen.Clear();
            txtPhai.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtAnh3x4.Clear();

            dtpNgaySinh.Value = DateTime.Today;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}