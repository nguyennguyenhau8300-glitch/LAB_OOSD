using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms
{
    public partial class FrmDanhMuc : Form
    {
        public FrmDanhMuc()
        {
            InitializeComponent();
        }

        // =====================================================
        // FORM LOAD
        // =====================================================
        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            LoadTheLoai();
            LoadNhaXuatBan();
            LoadNhanVien();
        }

        // =====================================================
        // THỂ LOẠI
        // =====================================================
        private void LoadTheLoai()
        {
            try
            {
                string sql = @"
                    SELECT
                        tl.MaTheLoai,
                        tl.TenTheLoai
                    FROM dbo.TheLoai AS tl
                    ORDER BY tl.MaTheLoai";

                DataTable dt = Db.Query(sql);

                dgvTheLoai.DataSource = dt;

                dgvTheLoai.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
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
        // NHÀ XUẤT BẢN
        // =====================================================
        private void LoadNhaXuatBan()
        {
            try
            {
                string sql = @"
                    SELECT
                        nxb.MaNhaXuatBan,
                        nxb.DiaChi,
                        nxb.SoDienThoai
                    FROM dbo.NhaXuatBan AS nxb
                    ORDER BY nxb.MaNhaXuatBan";

                DataTable dt = Db.Query(sql);

                dgvNhaXuatBan.DataSource = dt;

                dgvNhaXuatBan.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
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
        // NHÂN VIÊN
        // =====================================================
        private void LoadNhanVien()
        {
            try
            {
                string sql = @"
                    SELECT
                        nv.MaNhanVien,
                        nv.Ho,
                        nv.Ten,
                        nv.Phai,
                        nv.NgaySinh,
                        nv.ChucVu,
                        nv.SoDienThoai
                    FROM dbo.NhanVien AS nv
                    ORDER BY nv.MaNhanVien";

                DataTable dt = Db.Query(sql);

                dgvNhanVien.DataSource = dt;

                dgvNhanVien.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải nhân viên:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // ĐÓNG FORM
        // =====================================================
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}