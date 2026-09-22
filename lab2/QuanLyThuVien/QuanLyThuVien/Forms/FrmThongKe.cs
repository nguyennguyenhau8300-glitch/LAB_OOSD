using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms
{
    public partial class FrmThongKe : Form
    {
        public FrmThongKe()
        {
            InitializeComponent();
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            try
            {
                // Tổng số đầu sách
                object soDauSach = Db.Scalar(
                    "SELECT COUNT(*) FROM DauSach"
                );

                // Tổng số sách hiện có
                object tongSach = Db.Scalar(
                    "SELECT ISNULL(SUM(SoLuongHienCo), 0) FROM DauSach"
                );

                // Tổng số độc giả
                object soDocGia = Db.Scalar(
                    "SELECT COUNT(*) FROM DocGia"
                );

                // Tổng số nhân viên
                object soNhanVien = Db.Scalar(
                    "SELECT COUNT(*) FROM NhanVien"
                );

                // Tổng số phiếu mượn
                object soPhieuMuon = Db.Scalar(
                    "SELECT COUNT(*) FROM PhieuMuon"
                );

                lblSoDauSach.Text = "Số đầu sách: " + soDauSach;
                lblTongSach.Text = "Tổng số sách: " + tongSach;
                lblSoDocGia.Text = "Số độc giả: " + soDocGia;
                lblSoNhanVien.Text = "Số nhân viên: " + soNhanVien;
                lblSoPhieuMuon.Text = "Số phiếu mượn: " + soPhieuMuon;

                // Danh sách thống kê sách
                string sql = @"
                    SELECT
                        ds.MaDauSach AS [Mã sách],
                        ds.TenSach AS [Tên sách],
                        tl.TenTheLoai AS [Thể loại],
                        ds.NamXuatBan AS [Năm xuất bản],
                        ds.SoLuongHienCo AS [Số lượng hiện có],
                        nxb.MaNhaXuatBan AS [Mã NXB]
                    FROM DauSach ds
                    INNER JOIN TheLoai tl
                        ON ds.MaTheLoai = tl.MaTheLoai
                    INNER JOIN NhaXuatBan nxb
                        ON ds.MaNhaXuatBan = nxb.MaNhaXuatBan
                    ORDER BY ds.MaDauSach
                ";

                DataTable dt = Db.Query(sql);

                dgvThongKe.DataSource = dt;

                dgvThongKe.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvThongKe.ReadOnly = true;
                dgvThongKe.AllowUserToAddRows = false;
                dgvThongKe.AllowUserToDeleteRows = false;
                dgvThongKe.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu thống kê:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}