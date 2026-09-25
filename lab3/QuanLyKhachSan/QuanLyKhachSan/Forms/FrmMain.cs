namespace QuanLyKhachSan.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        // =====================================================
        // 1. DANH MỤC
        // =====================================================
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            using FrmDanhMuc frm = new FrmDanhMuc();
            frm.ShowDialog();
        }

        // =====================================================
        // 2. PHÒNG - TIỆN NGHI
        // =====================================================
        private void btnPhongTienNghi_Click(object sender, EventArgs e)
        {
            using FrmPhongTienNghi frm = new FrmPhongTienNghi();
            frm.ShowDialog();
        }

        // =====================================================
        // 3. ĐẶT / NHẬN PHÒNG
        // =====================================================
        private void btnDatNhanPhong_Click(object sender, EventArgs e)
        {
            using FrmDatNhanPhong frm = new FrmDatNhanPhong();
            frm.ShowDialog();
        }

        // =====================================================
        // 4. SỬ DỤNG DỊCH VỤ
        // =====================================================
        private void btnDichVu_Click(object sender, EventArgs e)
        {
            using FrmSuDungDichVu frm = new FrmSuDungDichVu();
            frm.ShowDialog();
        }

        // =====================================================
        // 5. TRẢ PHÒNG - THANH TOÁN
        // =====================================================
        private void btnTraPhong_Click(object sender, EventArgs e)
        {
            using FrmTraPhongThanhToan frm =
                new FrmTraPhongThanhToan();

            frm.ShowDialog();
        }

        // =====================================================
        // 6. THỐNG KÊ
        // =====================================================
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using FrmThongKe frm = new FrmThongKe();
            frm.ShowDialog();
        }

        // =====================================================
        // THOÁT
        // =====================================================
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn thoát chương trình?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}