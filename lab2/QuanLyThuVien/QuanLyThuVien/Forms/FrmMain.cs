using System;
using System.Windows.Forms;

namespace QuanLyThuVien.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnDanhMuc_Click(
            object sender,
            EventArgs e)
        {
            FrmDanhMuc frm =
                new FrmDanhMuc();

            frm.ShowDialog();
        }

        private void btnSach_Click(
            object sender,
            EventArgs e)
        {
            FrmSach frm =
                new FrmSach();

            frm.ShowDialog();
        }

        private void btnDocGia_Click(
            object sender,
            EventArgs e)
        {
            FrmDocGia frm =
                new FrmDocGia();

            frm.ShowDialog();
        }

        private void btnMuonTra_Click(
            object sender,
            EventArgs e)
        {
            FrmMuonTra frm =
                new FrmMuonTra();

            frm.ShowDialog();
        }

        private void btnThongKe_Click(
            object sender,
            EventArgs e)
        {
            FrmThongKe frm =
                new FrmThongKe();

            frm.ShowDialog();
        }

        private void btnThoat_Click(
            object sender,
            EventArgs e)
        {
            Application.Exit();
        }
    }
}