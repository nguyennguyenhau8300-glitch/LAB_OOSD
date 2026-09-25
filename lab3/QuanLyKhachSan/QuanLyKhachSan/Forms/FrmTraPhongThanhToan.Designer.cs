namespace QuanLyKhachSan.Forms
{
    partial class FrmTraPhongThanhToan
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;

        private ComboBox cboPhieuDat;
        private DateTimePicker dtpNgayTra;
        private ComboBox cboNhanVien;

        private DataGridView dgvPhong;
        private DataGridView dgvDichVu;
        private DataGridView dgvDenBu;
        private DataGridView dgvThanhToan;

        private TextBox txtTienPhong;
        private TextBox txtTienDichVu;
        private TextBox txtTienDenBu;
        private TextBox txtTienCoc;
        private TextBox txtTongThanhToan;
        private TextBox txtDaThanhToan;
        private TextBox txtConLai;

        private ComboBox cboPhuongThuc;
        private NumericUpDown nudSoTien;
        private TextBox txtMaGiaoDich;

        private Button btnTinhTien;
        private Button btnLapHoaDon;
        private Button btnThanhToan;
        private Button btnTraPhong;
        private Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            ClientSize = new Size(1350, 850);
            MinimumSize = new Size(1250, 780);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 10F);
            Text = "Trả phòng - Thanh toán";

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(234, 88, 12)
            };

            lblTitle = new Label
            {
                Text = "TRẢ PHÒNG - THANH TOÁN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(35, 25)
            };

            pnlHeader.Controls.Add(lblTitle);
            Controls.Add(pnlHeader);

            Controls.Add(Label("Phiếu đang ở:", 25, 115));

            cboPhieuDat = Combo(150, 110, 300);
            Controls.Add(cboPhieuDat);

            Controls.Add(Label("Ngày trả:", 480, 115));

            dtpNgayTra = new DateTimePicker
            {
                Location = new Point(570, 110),
                Size = new Size(210, 30),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm"
            };

            Controls.Add(dtpNgayTra);

            Controls.Add(Label("Nhân viên:", 810, 115));

            cboNhanVien = Combo(900, 110, 300);
            Controls.Add(cboNhanVien);

            TabControl tab = new TabControl
            {
                Location = new Point(25, 160),
                Size = new Size(850, 430),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Bottom |
                         AnchorStyles.Left
            };

            TabPage tabPhong = new TabPage("Tiền phòng");
            TabPage tabDV = new TabPage("Dịch vụ");
            TabPage tabDB = new TabPage("Đền bù");
            TabPage tabTT = new TabPage("Lịch sử thanh toán");

            dgvPhong = Grid();
            dgvPhong.Dock = DockStyle.Fill;
            tabPhong.Controls.Add(dgvPhong);

            dgvDichVu = Grid();
            dgvDichVu.Dock = DockStyle.Fill;
            tabDV.Controls.Add(dgvDichVu);

            dgvDenBu = Grid();
            dgvDenBu.Dock = DockStyle.Fill;
            tabDB.Controls.Add(dgvDenBu);

            dgvThanhToan = Grid();
            dgvThanhToan.Dock = DockStyle.Fill;
            tabTT.Controls.Add(dgvThanhToan);

            tab.TabPages.Add(tabPhong);
            tab.TabPages.Add(tabDV);
            tab.TabPages.Add(tabDB);
            tab.TabPages.Add(tabTT);

            Controls.Add(tab);

            GroupBox grpTien = new GroupBox
            {
                Text = "Hóa đơn",
                Location = new Point(900, 160),
                Size = new Size(410, 430),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            AddMoney(grpTien, "Tiền phòng:", out txtTienPhong, 35);
            AddMoney(grpTien, "Tiền dịch vụ:", out txtTienDichVu, 80);
            AddMoney(grpTien, "Tiền đền bù:", out txtTienDenBu, 125);
            AddMoney(grpTien, "Tiền cọc:", out txtTienCoc, 170);
            AddMoney(grpTien, "TỔNG:", out txtTongThanhToan, 230);
            AddMoney(grpTien, "Đã thanh toán:", out txtDaThanhToan, 280);
            AddMoney(grpTien, "Còn lại:", out txtConLai, 325);

            btnTinhTien = Button(
                "TÍNH TIỀN", 20, 375, 170,
                Color.FromArgb(37, 99, 235));

            btnLapHoaDon = Button(
                "LẬP HÓA ĐƠN", 205, 375, 180,
                Color.FromArgb(5, 150, 105));

            grpTien.Controls.Add(btnTinhTien);
            grpTien.Controls.Add(btnLapHoaDon);

            Controls.Add(grpTien);

            GroupBox grpThanhToan = new GroupBox
            {
                Text = "Thanh toán",
                Location = new Point(25, 610),
                Size = new Size(1285, 140),
                Anchor = AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };

            grpThanhToan.Controls.Add(
                Label("Phương thức:", 20, 35));

            cboPhuongThuc = Combo(130, 30, 200);

            cboPhuongThuc.Items.AddRange(
                new object[]
                {
                    "Tiền mặt",
                    "Chuyển khoản",
                    "Thẻ"
                });

            cboPhuongThuc.SelectedIndex = 0;

            grpThanhToan.Controls.Add(cboPhuongThuc);

            grpThanhToan.Controls.Add(
                Label("Số tiền:", 365, 35));

            nudSoTien = new NumericUpDown
            {
                Location = new Point(440, 30),
                Size = new Size(180, 30),
                Maximum = 1000000000,
                ThousandsSeparator = true
            };

            grpThanhToan.Controls.Add(nudSoTien);

            grpThanhToan.Controls.Add(
                Label("Mã giao dịch:", 650, 35));

            txtMaGiaoDich = new TextBox
            {
                Location = new Point(770, 30),
                Size = new Size(230, 30)
            };

            grpThanhToan.Controls.Add(txtMaGiaoDich);

            btnThanhToan = Button(
                "THANH TOÁN", 1020, 25, 220,
                Color.FromArgb(124, 58, 237));

            btnTraPhong = Button(
                "HOÀN TẤT TRẢ PHÒNG", 770, 80, 230,
                Color.FromArgb(234, 88, 12));

            grpThanhToan.Controls.Add(btnThanhToan);
            grpThanhToan.Controls.Add(btnTraPhong);

            Controls.Add(grpThanhToan);

            btnDong = Button(
                "ĐÓNG", 25, 775, 140,
                Color.FromArgb(220, 38, 38));

            btnDong.Anchor =
                AnchorStyles.Bottom | AnchorStyles.Left;

            Controls.Add(btnDong);

            Load += FrmTraPhongThanhToan_Load;
            cboPhieuDat.SelectedIndexChanged += cboPhieuDat_SelectedIndexChanged;
            btnTinhTien.Click += btnTinhTien_Click;
            btnLapHoaDon.Click += btnLapHoaDon_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            btnTraPhong.Click += btnTraPhong_Click;
            btnDong.Click += (s, e) => Close();

            ResumeLayout(false);
        }

        private Label Label(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
        }

        private ComboBox Combo(int x, int y, int w)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(w, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private Button Button(
            string text, int x, int y, int w, Color color)
        {
            Button b = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 42),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        private DataGridView Grid()
        {
            return new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
        }

        private void AddMoney(
            GroupBox box,
            string label,
            out TextBox txt,
            int y)
        {
            box.Controls.Add(Label(label, 20, y + 5));

            txt = new TextBox
            {
                Location = new Point(175, y),
                Size = new Size(200, 30),
                ReadOnly = true,
                Text = "0",
                TextAlign = HorizontalAlignment.Right
            };

            box.Controls.Add(txt);
        }
    }
}