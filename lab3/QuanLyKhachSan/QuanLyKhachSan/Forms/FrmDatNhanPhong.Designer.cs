namespace QuanLyKhachSan.Forms
{
    partial class FrmDatNhanPhong
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;

        private TabControl tabMain;
        private TabPage tabDatPhong;
        private TabPage tabChiTiet;
        private TabPage tabNhanPhong;

        // ===== ĐẶT PHÒNG =====
        private TextBox txtSoPhieuDat;
        private ComboBox cboKhachHang;
        private ComboBox cboNhanVien;
        private DateTimePicker dtpNgayNhan;
        private DateTimePicker dtpNgayTra;
        private NumericUpDown nudTienCoc;
        private ComboBox cboKenhDat;
        private ComboBox cboTrangThai;

        private Button btnThemPhieu;
        private Button btnSuaPhieu;
        private Button btnHuyPhieu;
        private Button btnMoiPhieu;

        private DataGridView dgvPhieuDat;

        // ===== CHI TIẾT PHÒNG =====
        private ComboBox cboPhieuChiTiet;
        private ComboBox cboPhong;
        private NumericUpDown nudSoNguoi;

        private Button btnThemPhong;
        private Button btnXoaPhong;
        private Button btnMoiChiTiet;

        private DataGridView dgvChiTiet;

        // ===== NHẬN PHÒNG =====
        private ComboBox cboPhieuNhan;
        private Label lblThongTinNhan;
        private DateTimePicker dtpNgayNhanThucTe;
        private Button btnNhanPhong;

        private TextBox txtHoTenNguoiLT;
        private TextBox txtCCCDNguoiLT;
        private TextBox txtQuocTichNguoiLT;
        private ComboBox cboPhongNguoiLT;
        private Button btnThemNguoiLT;

        private DataGridView dgvNguoiLuuTru;

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

            // =====================================================
            // FORM
            // =====================================================

            ClientSize = new Size(1300, 800);
            MinimumSize = new Size(1200, 720);

            BackColor = Color.FromArgb(241, 245, 249);

            Font = new Font("Segoe UI", 10F);

            StartPosition =
                FormStartPosition.CenterScreen;

            Text = "Đặt / Nhận phòng";

            // =====================================================
            // HEADER
            // =====================================================

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(5, 150, 105)
            };

            lblTitle = new Label
            {
                Text = "ĐẶT / NHẬN PHÒNG",

                Font = new Font(
                    "Segoe UI",
                    22F,
                    FontStyle.Bold),

                ForeColor = Color.White,
                AutoSize = true,

                Location = new Point(35, 25)
            };

            pnlHeader.Controls.Add(lblTitle);

            Controls.Add(pnlHeader);

            // =====================================================
            // TAB
            // =====================================================

            tabMain = new TabControl
            {
                Location = new Point(25, 110),
                Size = new Size(1250, 620),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            tabDatPhong =
                new TabPage("Phiếu đặt phòng");

            tabChiTiet =
                new TabPage("Phòng đã đặt");

            tabNhanPhong =
                new TabPage("Nhận phòng");

            tabMain.TabPages.Add(tabDatPhong);
            tabMain.TabPages.Add(tabChiTiet);
            tabMain.TabPages.Add(tabNhanPhong);

            Controls.Add(tabMain);

            // =====================================================
            // TAB PHIẾU ĐẶT PHÒNG
            // =====================================================

            tabDatPhong.Controls.Add(
                CreateLabel("Số phiếu:", 25, 25));

            txtSoPhieuDat =
                CreateTextBox(150, 20, 220);

            tabDatPhong.Controls.Add(txtSoPhieuDat);

            tabDatPhong.Controls.Add(
                CreateLabel("Khách hàng:", 25, 70));

            cboKhachHang =
                CreateCombo(150, 65, 300);

            tabDatPhong.Controls.Add(cboKhachHang);

            tabDatPhong.Controls.Add(
                CreateLabel("Nhân viên:", 25, 115));

            cboNhanVien =
                CreateCombo(150, 110, 300);

            tabDatPhong.Controls.Add(cboNhanVien);

            // -----------------------------------------------------

            tabDatPhong.Controls.Add(
                CreateLabel("Ngày nhận:", 500, 25));

            dtpNgayNhan =
                CreateDate(620, 20);

            tabDatPhong.Controls.Add(dtpNgayNhan);

            tabDatPhong.Controls.Add(
                CreateLabel("Ngày trả:", 500, 70));

            dtpNgayTra =
                CreateDate(620, 65);

            dtpNgayTra.Value =
                DateTime.Today.AddDays(1);

            tabDatPhong.Controls.Add(dtpNgayTra);

            tabDatPhong.Controls.Add(
                CreateLabel("Tiền cọc:", 500, 115));

            nudTienCoc =
                CreateNumber(620, 110, 230);

            nudTienCoc.Maximum = 100000000;
            nudTienCoc.ThousandsSeparator = true;

            tabDatPhong.Controls.Add(nudTienCoc);

            // -----------------------------------------------------

            tabDatPhong.Controls.Add(
                CreateLabel("Kênh đặt:", 900, 25));

            cboKenhDat =
                CreateCombo(1000, 20, 200);

            cboKenhDat.Items.AddRange(
                new object[]
                {
                    "Trực tiếp",
                    "Điện thoại",
                    "Website"
                });

            cboKenhDat.SelectedIndex = 0;

            tabDatPhong.Controls.Add(cboKenhDat);

            tabDatPhong.Controls.Add(
                CreateLabel("Trạng thái:", 900, 70));

            cboTrangThai =
                CreateCombo(1000, 65, 200);

            cboTrangThai.Items.AddRange(
                new object[]
                {
                    "Đã đặt",
                    "Đang ở",
                    "Đã trả",
                    "Hủy",
                    "No-show"
                });

            cboTrangThai.SelectedIndex = 0;

            tabDatPhong.Controls.Add(cboTrangThai);

            // -----------------------------------------------------

            btnThemPhieu =
                CreateButton(
                    "Thêm phiếu",
                    25,
                    175,
                    130,
                    Color.FromArgb(5, 150, 105));

            btnSuaPhieu =
                CreateButton(
                    "Sửa phiếu",
                    170,
                    175,
                    130,
                    Color.FromArgb(37, 99, 235));

            btnHuyPhieu =
                CreateButton(
                    "Hủy phiếu",
                    315,
                    175,
                    130,
                    Color.FromArgb(220, 38, 38));

            btnMoiPhieu =
                CreateButton(
                    "Làm mới",
                    460,
                    175,
                    130,
                    Color.FromArgb(71, 85, 105));

            tabDatPhong.Controls.Add(btnThemPhieu);
            tabDatPhong.Controls.Add(btnSuaPhieu);
            tabDatPhong.Controls.Add(btnHuyPhieu);
            tabDatPhong.Controls.Add(btnMoiPhieu);

            dgvPhieuDat = CreateGrid();

            dgvPhieuDat.Location =
                new Point(25, 235);

            dgvPhieuDat.Size =
                new Size(1180, 315);

            dgvPhieuDat.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabDatPhong.Controls.Add(dgvPhieuDat);

            // =====================================================
            // TAB CHI TIẾT PHÒNG
            // =====================================================

            tabChiTiet.Controls.Add(
                CreateLabel("Phiếu đặt:", 25, 30));

            cboPhieuChiTiet =
                CreateCombo(150, 25, 300);

            tabChiTiet.Controls.Add(cboPhieuChiTiet);

            tabChiTiet.Controls.Add(
                CreateLabel("Phòng:", 500, 30));

            cboPhong =
                CreateCombo(590, 25, 250);

            tabChiTiet.Controls.Add(cboPhong);

            tabChiTiet.Controls.Add(
                CreateLabel("Số người:", 880, 30));

            nudSoNguoi =
                CreateNumber(980, 25, 150);

            nudSoNguoi.Minimum = 1;
            nudSoNguoi.Maximum = 100;

            tabChiTiet.Controls.Add(nudSoNguoi);

            btnThemPhong =
                CreateButton(
                    "Thêm phòng",
                    25,
                    90,
                    140,
                    Color.FromArgb(5, 150, 105));

            btnXoaPhong =
                CreateButton(
                    "Xóa phòng",
                    180,
                    90,
                    140,
                    Color.FromArgb(220, 38, 38));

            btnMoiChiTiet =
                CreateButton(
                    "Làm mới",
                    335,
                    90,
                    140,
                    Color.FromArgb(71, 85, 105));

            tabChiTiet.Controls.Add(btnThemPhong);
            tabChiTiet.Controls.Add(btnXoaPhong);
            tabChiTiet.Controls.Add(btnMoiChiTiet);

            dgvChiTiet = CreateGrid();

            dgvChiTiet.Location =
                new Point(25, 155);

            dgvChiTiet.Size =
                new Size(1180, 395);

            dgvChiTiet.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabChiTiet.Controls.Add(dgvChiTiet);

            // =====================================================
            // TAB NHẬN PHÒNG
            // =====================================================

            tabNhanPhong.Controls.Add(
                CreateLabel("Phiếu đặt:", 25, 25));

            cboPhieuNhan =
                CreateCombo(150, 20, 300);

            tabNhanPhong.Controls.Add(cboPhieuNhan);

            tabNhanPhong.Controls.Add(
                CreateLabel(
                    "Nhận thực tế:",
                    500,
                    25));

            dtpNgayNhanThucTe =
                new DateTimePicker
                {
                    Location = new Point(630, 20),
                    Size = new Size(250, 30),
                    Format =
                        DateTimePickerFormat.Custom,
                    CustomFormat =
                        "dd/MM/yyyy HH:mm"
                };

            tabNhanPhong.Controls.Add(
                dtpNgayNhanThucTe);

            btnNhanPhong =
                CreateButton(
                    "NHẬN PHÒNG",
                    920,
                    18,
                    180,
                    Color.FromArgb(5, 150, 105));

            tabNhanPhong.Controls.Add(btnNhanPhong);

            lblThongTinNhan = new Label
            {
                Text =
                    "Chọn phiếu đặt để xem thông tin.",

                Location =
                    new Point(25, 75),

                Size =
                    new Size(1100, 30),

                ForeColor =
                    Color.FromArgb(71, 85, 105),

                Font =
                    new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Italic)
            };

            tabNhanPhong.Controls.Add(
                lblThongTinNhan);

            // -----------------------------------------------------
            // Người lưu trú
            // -----------------------------------------------------

            GroupBox grpNguoiLT =
                new GroupBox
                {
                    Text = "Thêm người lưu trú",
                    Location = new Point(25, 115),
                    Size = new Size(1180, 125)
                };

            grpNguoiLT.Controls.Add(
                CreateLabel("Phòng:", 20, 35));

            cboPhongNguoiLT =
                CreateCombo(90, 30, 180);

            grpNguoiLT.Controls.Add(
                cboPhongNguoiLT);

            grpNguoiLT.Controls.Add(
                CreateLabel("Họ tên:", 300, 35));

            txtHoTenNguoiLT =
                CreateTextBox(380, 30, 220);

            grpNguoiLT.Controls.Add(
                txtHoTenNguoiLT);

            grpNguoiLT.Controls.Add(
                CreateLabel("CCCD:", 630, 35));

            txtCCCDNguoiLT =
                CreateTextBox(700, 30, 180);

            grpNguoiLT.Controls.Add(
                txtCCCDNguoiLT);

            grpNguoiLT.Controls.Add(
                CreateLabel("Quốc tịch:", 20, 80));

            txtQuocTichNguoiLT =
                CreateTextBox(110, 75, 160);

            txtQuocTichNguoiLT.Text =
                "Việt Nam";

            grpNguoiLT.Controls.Add(
                txtQuocTichNguoiLT);

            btnThemNguoiLT =
                CreateButton(
                    "Thêm người",
                    920,
                    65,
                    160,
                    Color.FromArgb(37, 99, 235));

            grpNguoiLT.Controls.Add(
                btnThemNguoiLT);

            tabNhanPhong.Controls.Add(
                grpNguoiLT);

            dgvNguoiLuuTru =
                CreateGrid();

            dgvNguoiLuuTru.Location =
                new Point(25, 260);

            dgvNguoiLuuTru.Size =
                new Size(1180, 290);

            dgvNguoiLuuTru.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabNhanPhong.Controls.Add(
                dgvNguoiLuuTru);

            // =====================================================
            // BUTTON ĐÓNG
            // =====================================================

            btnDong =
                CreateButton(
                    "Đóng",
                    1100,
                    745,
                    175,
                    Color.FromArgb(220, 38, 38));

            btnDong.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            Controls.Add(btnDong);

            // =====================================================
            // EVENTS
            // =====================================================

            Load += FrmDatNhanPhong_Load;

            btnThemPhieu.Click +=
                btnThemPhieu_Click;

            btnSuaPhieu.Click +=
                btnSuaPhieu_Click;

            btnHuyPhieu.Click +=
                btnHuyPhieu_Click;

            btnMoiPhieu.Click +=
                btnMoiPhieu_Click;

            dgvPhieuDat.CellClick +=
                dgvPhieuDat_CellClick;

            cboPhieuChiTiet.SelectedIndexChanged +=
                cboPhieuChiTiet_SelectedIndexChanged;

            btnThemPhong.Click +=
                btnThemPhong_Click;

            btnXoaPhong.Click +=
                btnXoaPhong_Click;

            btnMoiChiTiet.Click +=
                btnMoiChiTiet_Click;

            cboPhieuNhan.SelectedIndexChanged +=
                cboPhieuNhan_SelectedIndexChanged;

            btnNhanPhong.Click +=
                btnNhanPhong_Click;

            btnThemNguoiLT.Click +=
                btnThemNguoiLT_Click;

            btnDong.Click +=
                btnDong_Click;

            ResumeLayout(false);
        }

        private Label CreateLabel(
            string text,
            int x,
            int y)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Location = new Point(x, y),

                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold)
            };
        }

        private TextBox CreateTextBox(
            int x,
            int y,
            int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30)
            };
        }

        private ComboBox CreateCombo(
            int x,
            int y,
            int width)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30),

                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };
        }

        private DateTimePicker CreateDate(
            int x,
            int y)
        {
            return new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(230, 30),

                Format =
                    DateTimePickerFormat.Short
            };
        }

        private NumericUpDown CreateNumber(
            int x,
            int y,
            int width)
        {
            return new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(width, 30)
            };
        }

        private Button CreateButton(
            string text,
            int x,
            int y,
            int width,
            Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 42),

                BackColor = color,
                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),

                Cursor = Cursors.Hand
            };
        }

        private DataGridView CreateGrid()
        {
            DataGridView grid =
                new DataGridView();

            grid.ReadOnly = true;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;

            grid.MultiSelect = false;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.BackgroundColor = Color.White;

            grid.RowHeadersVisible = false;

            return grid;
        }
    }
}