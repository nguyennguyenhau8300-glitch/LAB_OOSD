namespace QuanLyThuVien.Forms
{
    partial class FrmMuonTra
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cboDocGia;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.DateTimePicker dtpNgayMuon;
        private System.Windows.Forms.DateTimePicker dtpNgayHenTra;

        private System.Windows.Forms.DataGridView dgvSach;
        private System.Windows.Forms.DataGridView dgvPhieuMuon;

        private System.Windows.Forms.Button btnLapPhieuMuon;
        private System.Windows.Forms.Button btnTraSach;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.Label lblDocGia;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblNgayMuon;
        private System.Windows.Forms.Label lblNgayHenTra;
        private System.Windows.Forms.Label lblSach;
        private System.Windows.Forms.Label lblPhieuMuon;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cboDocGia = new System.Windows.Forms.ComboBox();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();

            this.dtpNgayMuon = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayHenTra = new System.Windows.Forms.DateTimePicker();

            this.dgvSach = new System.Windows.Forms.DataGridView();
            this.dgvPhieuMuon = new System.Windows.Forms.DataGridView();

            this.btnLapPhieuMuon = new System.Windows.Forms.Button();
            this.btnTraSach = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.lblDocGia = new System.Windows.Forms.Label();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblNgayMuon = new System.Windows.Forms.Label();
            this.lblNgayHenTra = new System.Windows.Forms.Label();
            this.lblSach = new System.Windows.Forms.Label();
            this.lblPhieuMuon = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).BeginInit();

            this.SuspendLayout();

            // LABEL ĐỘC GIẢ
            this.lblDocGia.AutoSize = true;
            this.lblDocGia.Location = new System.Drawing.Point(20, 20);
            this.lblDocGia.Text = "Độc giả:";

            // COMBO ĐỘC GIẢ
            this.cboDocGia.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboDocGia.Location =
                new System.Drawing.Point(100, 17);

            this.cboDocGia.Size =
                new System.Drawing.Size(220, 24);

            // LABEL NHÂN VIÊN
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location =
                new System.Drawing.Point(350, 20);

            this.lblNhanVien.Text = "Nhân viên:";

            // COMBO NHÂN VIÊN
            this.cboNhanVien.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboNhanVien.Location =
                new System.Drawing.Point(430, 17);

            this.cboNhanVien.Size =
                new System.Drawing.Size(220, 24);

            // NGÀY MƯỢN
            this.lblNgayMuon.AutoSize = true;
            this.lblNgayMuon.Location =
                new System.Drawing.Point(20, 60);

            this.lblNgayMuon.Text = "Ngày mượn:";

            this.dtpNgayMuon.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtpNgayMuon.Location =
                new System.Drawing.Point(100, 57);

            this.dtpNgayMuon.Size =
                new System.Drawing.Size(150, 22);

            // NGÀY HẸN TRẢ
            this.lblNgayHenTra.AutoSize = true;
            this.lblNgayHenTra.Location =
                new System.Drawing.Point(350, 60);

            this.lblNgayHenTra.Text = "Ngày hẹn trả:";

            this.dtpNgayHenTra.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtpNgayHenTra.Location =
                new System.Drawing.Point(430, 57);

            this.dtpNgayHenTra.Size =
                new System.Drawing.Size(150, 22);

            // NÚT LẬP PHIẾU
            this.btnLapPhieuMuon.Location =
                new System.Drawing.Point(700, 15);

            this.btnLapPhieuMuon.Size =
                new System.Drawing.Size(120, 35);

            this.btnLapPhieuMuon.Text =
                "Lập phiếu mượn";

            this.btnLapPhieuMuon.Click +=
                new System.EventHandler(
                    this.btnLapPhieuMuon_Click);

            // NÚT TRẢ SÁCH
            this.btnTraSach.Location =
                new System.Drawing.Point(830, 15);

            this.btnTraSach.Size =
                new System.Drawing.Size(100, 35);

            this.btnTraSach.Text = "Trả sách";

            this.btnTraSach.Click +=
                new System.EventHandler(
                    this.btnTraSach_Click);

            // NÚT ĐÓNG
            this.btnDong.Location =
                new System.Drawing.Point(940, 15);

            this.btnDong.Size =
                new System.Drawing.Size(80, 35);

            this.btnDong.Text = "Đóng";

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            // SÁCH
            this.lblSach.AutoSize = true;
            this.lblSach.Location =
                new System.Drawing.Point(20, 100);

            this.lblSach.Text = "Danh sách sách:";

            this.dgvSach.Location =
                new System.Drawing.Point(20, 125);

            this.dgvSach.Size =
                new System.Drawing.Size(1000, 220);

            this.dgvSach.Name = "dgvSach";

            this.dgvSach.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvSach.MultiSelect = true;

            this.dgvSach.ReadOnly = true;

            this.dgvSach.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // PHIẾU MƯỢN
            this.lblPhieuMuon.AutoSize = true;
            this.lblPhieuMuon.Location =
                new System.Drawing.Point(20, 365);

            this.lblPhieuMuon.Text = "Danh sách phiếu mượn:";

            this.dgvPhieuMuon.Location =
                new System.Drawing.Point(20, 390);

            this.dgvPhieuMuon.Size =
                new System.Drawing.Size(1000, 220);

            this.dgvPhieuMuon.Name = "dgvPhieuMuon";

            this.dgvPhieuMuon.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvPhieuMuon.MultiSelect = false;

            this.dgvPhieuMuon.ReadOnly = true;

            this.dgvPhieuMuon.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // FORM
            this.ClientSize =
                new System.Drawing.Size(1050, 650);

            this.Controls.Add(this.lblDocGia);
            this.Controls.Add(this.cboDocGia);

            this.Controls.Add(this.lblNhanVien);
            this.Controls.Add(this.cboNhanVien);

            this.Controls.Add(this.lblNgayMuon);
            this.Controls.Add(this.dtpNgayMuon);

            this.Controls.Add(this.lblNgayHenTra);
            this.Controls.Add(this.dtpNgayHenTra);

            this.Controls.Add(this.btnLapPhieuMuon);
            this.Controls.Add(this.btnTraSach);
            this.Controls.Add(this.btnDong);

            this.Controls.Add(this.lblSach);
            this.Controls.Add(this.dgvSach);

            this.Controls.Add(this.lblPhieuMuon);
            this.Controls.Add(this.dgvPhieuMuon);

            this.Name = "FrmMuonTra";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Quản lý mượn trả";

            this.Load +=
                new System.EventHandler(
                    this.FrmMuonTra_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}