namespace QuanLyKhachSan.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;

        private Panel pnlMenu;

        private Button btnDanhMuc;
        private Button btnPhongTienNghi;
        private Button btnDatNhanPhong;
        private Button btnDichVu;
        private Button btnTraPhong;
        private Button btnThongKe;
        private Button btnThoat;

        private Label lblFooter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();

            pnlMenu = new Panel();

            btnDanhMuc = new Button();
            btnPhongTienNghi = new Button();
            btnDatNhanPhong = new Button();
            btnDichVu = new Button();
            btnTraPhong = new Button();
            btnThongKe = new Button();
            btnThoat = new Button();

            lblFooter = new Label();

            pnlHeader.SuspendLayout();
            pnlMenu.SuspendLayout();

            SuspendLayout();

            // =====================================================
            // pnlHeader
            // =====================================================

            pnlHeader.BackColor = Color.FromArgb(30, 64, 175);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1100, 150);
            pnlHeader.TabIndex = 0;

            // =====================================================
            // lblTitle
            // =====================================================

            lblTitle.Anchor = AnchorStyles.Top;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font(
                "Segoe UI",
                25F,
                FontStyle.Bold
            );

            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(306, 32);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(488, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN";

            // =====================================================
            // lblSubtitle
            // =====================================================

            lblSubtitle.Anchor = AnchorStyles.Top;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font(
                "Segoe UI",
                11F
            );

            lblSubtitle.ForeColor = Color.FromArgb(
                219,
                234,
                254
            );

            lblSubtitle.Location = new Point(391, 91);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(318, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text =
                "Hệ thống quản lý hoạt động khách sạn";

            // =====================================================
            // pnlMenu
            // =====================================================

            pnlMenu.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            pnlMenu.BackColor = Color.White;

            pnlMenu.Controls.Add(btnDanhMuc);
            pnlMenu.Controls.Add(btnPhongTienNghi);
            pnlMenu.Controls.Add(btnDatNhanPhong);
            pnlMenu.Controls.Add(btnDichVu);
            pnlMenu.Controls.Add(btnTraPhong);
            pnlMenu.Controls.Add(btnThongKe);
            pnlMenu.Controls.Add(btnThoat);

            pnlMenu.Location = new Point(70, 190);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(960, 430);
            pnlMenu.TabIndex = 1;

            // =====================================================
            // btnDanhMuc
            // =====================================================

            btnDanhMuc.BackColor = Color.FromArgb(37, 99, 235);
            btnDanhMuc.Cursor = Cursors.Hand;

            btnDanhMuc.FlatAppearance.BorderSize = 0;
            btnDanhMuc.FlatStyle = FlatStyle.Flat;

            btnDanhMuc.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold
            );

            btnDanhMuc.ForeColor = Color.White;

            btnDanhMuc.Location = new Point(70, 45);
            btnDanhMuc.Name = "btnDanhMuc";
            btnDanhMuc.Size = new Size(370, 80);
            btnDanhMuc.TabIndex = 0;
            btnDanhMuc.Text = "DANH MỤC";
            btnDanhMuc.UseVisualStyleBackColor = false;

            btnDanhMuc.Click += btnDanhMuc_Click;

            // =====================================================
            // btnPhongTienNghi
            // =====================================================

            btnPhongTienNghi.BackColor =
                Color.FromArgb(14, 116, 144);

            btnPhongTienNghi.Cursor = Cursors.Hand;

            btnPhongTienNghi.FlatAppearance.BorderSize = 0;
            btnPhongTienNghi.FlatStyle = FlatStyle.Flat;

            btnPhongTienNghi.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold
            );

            btnPhongTienNghi.ForeColor = Color.White;

            btnPhongTienNghi.Location =
                new Point(520, 45);

            btnPhongTienNghi.Name =
                "btnPhongTienNghi";

            btnPhongTienNghi.Size =
                new Size(370, 80);

            btnPhongTienNghi.TabIndex = 1;

            btnPhongTienNghi.Text =
                "PHÒNG - TIỆN NGHI";

            btnPhongTienNghi.UseVisualStyleBackColor =
                false;

            btnPhongTienNghi.Click +=
                btnPhongTienNghi_Click;

            // =====================================================
            // btnDatNhanPhong
            // =====================================================

            btnDatNhanPhong.BackColor =
                Color.FromArgb(5, 150, 105);

            btnDatNhanPhong.Cursor = Cursors.Hand;

            btnDatNhanPhong.FlatAppearance.BorderSize = 0;
            btnDatNhanPhong.FlatStyle = FlatStyle.Flat;

            btnDatNhanPhong.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold
            );

            btnDatNhanPhong.ForeColor = Color.White;

            btnDatNhanPhong.Location =
                new Point(70, 155);

            btnDatNhanPhong.Name =
                "btnDatNhanPhong";

            btnDatNhanPhong.Size =
                new Size(370, 80);

            btnDatNhanPhong.TabIndex = 2;

            btnDatNhanPhong.Text =
                "ĐẶT / NHẬN PHÒNG";

            btnDatNhanPhong.UseVisualStyleBackColor =
                false;

            btnDatNhanPhong.Click +=
                btnDatNhanPhong_Click;

            // =====================================================
            // btnDichVu
            // =====================================================

            btnDichVu.BackColor =
                Color.FromArgb(124, 58, 237);

            btnDichVu.Cursor = Cursors.Hand;

            btnDichVu.FlatAppearance.BorderSize = 0;
            btnDichVu.FlatStyle = FlatStyle.Flat;

            btnDichVu.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold
            );

            btnDichVu.ForeColor = Color.White;

            btnDichVu.Location =
                new Point(520, 155);

            btnDichVu.Name = "btnDichVu";
            btnDichVu.Size = new Size(370, 80);
            btnDichVu.TabIndex = 3;

            btnDichVu.Text =
                "SỬ DỤNG DỊCH VỤ";

            btnDichVu.UseVisualStyleBackColor =
                false;

            btnDichVu.Click += btnDichVu_Click;

            // =====================================================
            // btnTraPhong
            // =====================================================

            btnTraPhong.BackColor =
                Color.FromArgb(234, 88, 12);

            btnTraPhong.Cursor = Cursors.Hand;

            btnTraPhong.FlatAppearance.BorderSize = 0;
            btnTraPhong.FlatStyle = FlatStyle.Flat;

            btnTraPhong.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold
            );

            btnTraPhong.ForeColor = Color.White;

            btnTraPhong.Location =
                new Point(70, 265);

            btnTraPhong.Name = "btnTraPhong";
            btnTraPhong.Size = new Size(370, 80);
            btnTraPhong.TabIndex = 4;

            btnTraPhong.Text =
                "TRẢ PHÒNG - THANH TOÁN";

            btnTraPhong.UseVisualStyleBackColor =
                false;

            btnTraPhong.Click += btnTraPhong_Click;

            // =====================================================
            // btnThongKe
            // =====================================================

            btnThongKe.BackColor =
                Color.FromArgb(71, 85, 105);

            btnThongKe.Cursor = Cursors.Hand;

            btnThongKe.FlatAppearance.BorderSize = 0;
            btnThongKe.FlatStyle = FlatStyle.Flat;

            btnThongKe.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold
            );

            btnThongKe.ForeColor = Color.White;

            btnThongKe.Location =
                new Point(520, 265);

            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(370, 80);
            btnThongKe.TabIndex = 5;

            btnThongKe.Text = "THỐNG KÊ";

            btnThongKe.UseVisualStyleBackColor =
                false;

            btnThongKe.Click += btnThongKe_Click;

            // =====================================================
            // btnThoat
            // =====================================================

            btnThoat.Anchor = AnchorStyles.Bottom;

            btnThoat.BackColor =
                Color.FromArgb(220, 38, 38);

            btnThoat.Cursor = Cursors.Hand;

            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.FlatStyle = FlatStyle.Flat;

            btnThoat.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );

            btnThoat.ForeColor = Color.White;

            btnThoat.Location =
                new Point(380, 370);

            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(200, 45);
            btnThoat.TabIndex = 6;

            btnThoat.Text = "THOÁT";

            btnThoat.UseVisualStyleBackColor =
                false;

            btnThoat.Click += btnThoat_Click;

            // =====================================================
            // lblFooter
            // =====================================================

            lblFooter.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            lblFooter.Font =
                new Font("Segoe UI", 9F);

            lblFooter.ForeColor =
                Color.FromArgb(100, 116, 139);

            lblFooter.Location =
                new Point(0, 650);

            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(1100, 30);
            lblFooter.TabIndex = 2;

            lblFooter.Text =
                "Hệ thống quản lý khách sạn - LAB3";

            lblFooter.TextAlign =
                ContentAlignment.MiddleCenter;

            // =====================================================
            // FrmMain
            // =====================================================

            AutoScaleDimensions =
                new SizeF(7F, 15F);

            AutoScaleMode =
                AutoScaleMode.Font;

            BackColor =
                Color.FromArgb(241, 245, 249);

            ClientSize =
                new Size(1100, 690);

            Controls.Add(lblFooter);
            Controls.Add(pnlMenu);
            Controls.Add(pnlHeader);

            Font =
                new Font("Segoe UI", 9F);

            FormBorderStyle =
                FormBorderStyle.FixedSingle;

            MaximizeBox = false;

            Name = "FrmMain";

            StartPosition =
                FormStartPosition.CenterScreen;

            Text =
                "Hệ thống quản lý khách sạn";

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();

            pnlMenu.ResumeLayout(false);

            ResumeLayout(false);
        }
    }
}