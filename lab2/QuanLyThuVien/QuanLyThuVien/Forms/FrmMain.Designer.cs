namespace QuanLyThuVien.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnSach;
        private System.Windows.Forms.Button btnDocGia;
        private System.Windows.Forms.Button btnMuonTra;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnSach = new System.Windows.Forms.Button();
            this.btnDocGia = new System.Windows.Forms.Button();
            this.btnMuonTra = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 20F,
                System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(250, 50);
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN";

            // btnDanhMuc
            this.btnDanhMuc.Location = new System.Drawing.Point(250, 130);
            this.btnDanhMuc.Size = new System.Drawing.Size(300, 45);
            this.btnDanhMuc.Text = "Danh mục và nhân viên";
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);

            // btnSach
            this.btnSach.Location = new System.Drawing.Point(250, 190);
            this.btnSach.Size = new System.Drawing.Size(300, 45);
            this.btnSach.Text = "Quản lý đầu sách";
            this.btnSach.Click += new System.EventHandler(this.btnSach_Click);

            // btnDocGia
            this.btnDocGia.Location = new System.Drawing.Point(250, 250);
            this.btnDocGia.Size = new System.Drawing.Size(300, 45);
            this.btnDocGia.Text = "Độc giả và thẻ";
            this.btnDocGia.Click += new System.EventHandler(this.btnDocGia_Click);

            // btnMuonTra
            this.btnMuonTra.Location = new System.Drawing.Point(250, 310);
            this.btnMuonTra.Size = new System.Drawing.Size(300, 45);
            this.btnMuonTra.Text = "Mượn - Trả sách";
            this.btnMuonTra.Click += new System.EventHandler(this.btnMuonTra_Click);

            // btnThongKe
            this.btnThongKe.Location = new System.Drawing.Point(250, 370);
            this.btnThongKe.Size = new System.Drawing.Size(300, 45);
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);

            // btnThoat
            this.btnThoat.Location = new System.Drawing.Point(250, 430);
            this.btnThoat.Size = new System.Drawing.Size(300, 45);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            // FrmMain
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.btnSach);
            this.Controls.Add(this.btnDocGia);
            this.Controls.Add(this.btnMuonTra);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnThoat);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thư viện";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}