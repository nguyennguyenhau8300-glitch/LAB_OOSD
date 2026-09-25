namespace QuanLyKhachSan.Forms
{
    partial class FrmPhongTienNghi
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;

        private TabControl tabMain;
        private TabPage tabPhong;
        private TabPage tabTienNghi;
        private TabPage tabLapDat;

        // PHÒNG
        private TextBox txtSoPhong;
        private ComboBox cboKhuVuc;
        private TextBox txtLoaiPhong;
        private NumericUpDown nudSoNguoi;
        private NumericUpDown nudDonGiaPhong;
        private ComboBox cboTrangThaiPhong;

        private Button btnThemPhong;
        private Button btnSuaPhong;
        private Button btnXoaPhong;
        private Button btnMoiPhong;

        private DataGridView dgvPhong;

        // TIỆN NGHI
        private TextBox txtMaTienNghi;
        private ComboBox cboLoaiTienNghi;
        private TextBox txtTenTienNghi;
        private ComboBox cboTinhTrangTN;

        private Button btnThemTN;
        private Button btnSuaTN;
        private Button btnXoaTN;
        private Button btnMoiTN;

        private DataGridView dgvTienNghi;

        // LẮP ĐẶT
        private ComboBox cboTienNghiLapDat;
        private ComboBox cboPhongLapDat;

        private DateTimePicker dtpNgayLap;
        private DateTimePicker dtpNgayThaoDo;

        private CheckBox chkChuaThaoDo;

        private Button btnLapDat;
        private Button btnThaoDo;
        private Button btnMoiLapDat;

        private DataGridView dgvLapDat;

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

            BackColor = Color.FromArgb(241, 245, 249);

            ClientSize = new Size(1250, 750);

            Font = new Font("Segoe UI", 10F);

            StartPosition =
                FormStartPosition.CenterScreen;

            Text = "Phòng - Tiện nghi";

            MinimumSize =
                new Size(1150, 700);

            // =====================================================
            // HEADER
            // =====================================================

            pnlHeader = new Panel();

            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 90;

            pnlHeader.BackColor =
                Color.FromArgb(14, 116, 144);

            lblTitle = new Label();

            lblTitle.Text =
                "QUẢN LÝ PHÒNG - TIỆN NGHI";

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    22F,
                    FontStyle.Bold);

            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;

            lblTitle.Location =
                new Point(35, 25);

            pnlHeader.Controls.Add(lblTitle);

            Controls.Add(pnlHeader);

            // =====================================================
            // TAB
            // =====================================================

            tabMain = new TabControl();

            tabMain.Location =
                new Point(25, 110);

            tabMain.Size =
                new Size(1200, 570);

            tabMain.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabPhong = new TabPage("Danh sách phòng");
            tabTienNghi = new TabPage("Tiện nghi");
            tabLapDat = new TabPage("Lắp đặt tiện nghi");

            tabMain.TabPages.Add(tabPhong);
            tabMain.TabPages.Add(tabTienNghi);
            tabMain.TabPages.Add(tabLapDat);

            Controls.Add(tabMain);

            // =====================================================
            // TAB PHÒNG
            // =====================================================

            tabPhong.Controls.Add(
                CreateLabel("Số phòng:", 25, 25));

            txtSoPhong =
                CreateTextBox(145, 20, 200);

            tabPhong.Controls.Add(txtSoPhong);

            tabPhong.Controls.Add(
                CreateLabel("Khu vực:", 25, 70));

            cboKhuVuc =
                CreateCombo(145, 65, 200);

            tabPhong.Controls.Add(cboKhuVuc);

            tabPhong.Controls.Add(
                CreateLabel("Loại phòng:", 25, 115));

            txtLoaiPhong =
                CreateTextBox(145, 110, 200);

            tabPhong.Controls.Add(txtLoaiPhong);

            // -----------------------------------------------------

            tabPhong.Controls.Add(
                CreateLabel("Số người tối đa:", 400, 25));

            nudSoNguoi =
                CreateNumber(545, 20, 200);

            nudSoNguoi.Minimum = 1;
            nudSoNguoi.Maximum = 100;

            tabPhong.Controls.Add(nudSoNguoi);

            tabPhong.Controls.Add(
                CreateLabel("Đơn giá/ngày:", 400, 70));

            nudDonGiaPhong =
                CreateNumber(545, 65, 200);

            nudDonGiaPhong.Maximum = 100000000;
            nudDonGiaPhong.ThousandsSeparator = true;

            tabPhong.Controls.Add(nudDonGiaPhong);

            tabPhong.Controls.Add(
                CreateLabel("Trạng thái:", 400, 115));

            cboTrangThaiPhong =
                CreateCombo(545, 110, 200);

            cboTrangThaiPhong.Items.AddRange(
                new object[]
                {
                    "Trống",
                    "Đã đặt",
                    "Đang ở",
                    "Bảo trì"
                });

            cboTrangThaiPhong.SelectedIndex = 0;

            tabPhong.Controls.Add(cboTrangThaiPhong);

            // -----------------------------------------------------

            btnThemPhong =
                CreateButton(
                    "Thêm",
                    25,
                    170,
                    Color.FromArgb(5, 150, 105));

            btnSuaPhong =
                CreateButton(
                    "Sửa",
                    145,
                    170,
                    Color.FromArgb(37, 99, 235));

            btnXoaPhong =
                CreateButton(
                    "Xóa",
                    265,
                    170,
                    Color.FromArgb(220, 38, 38));

            btnMoiPhong =
                CreateButton(
                    "Làm mới",
                    385,
                    170,
                    Color.FromArgb(71, 85, 105));

            tabPhong.Controls.Add(btnThemPhong);
            tabPhong.Controls.Add(btnSuaPhong);
            tabPhong.Controls.Add(btnXoaPhong);
            tabPhong.Controls.Add(btnMoiPhong);

            // -----------------------------------------------------

            dgvPhong = CreateGrid();

            dgvPhong.Location =
                new Point(25, 230);

            dgvPhong.Size =
                new Size(1135, 285);

            dgvPhong.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabPhong.Controls.Add(dgvPhong);

            // =====================================================
            // TAB TIỆN NGHI
            // =====================================================

            tabTienNghi.Controls.Add(
                CreateLabel("Mã tiện nghi:", 25, 25));

            txtMaTienNghi =
                CreateTextBox(155, 20, 210);

            tabTienNghi.Controls.Add(txtMaTienNghi);

            tabTienNghi.Controls.Add(
                CreateLabel("Loại tiện nghi:", 25, 70));

            cboLoaiTienNghi =
                CreateCombo(155, 65, 210);

            tabTienNghi.Controls.Add(cboLoaiTienNghi);

            tabTienNghi.Controls.Add(
                CreateLabel("Tên tiện nghi:", 420, 25));

            txtTenTienNghi =
                CreateTextBox(550, 20, 250);

            tabTienNghi.Controls.Add(txtTenTienNghi);

            tabTienNghi.Controls.Add(
                CreateLabel("Tình trạng:", 420, 70));

            cboTinhTrangTN =
                CreateCombo(550, 65, 250);

            cboTinhTrangTN.Items.AddRange(
                new object[]
                {
                    "Tốt",
                    "Hỏng",
                    "Bảo trì"
                });

            cboTinhTrangTN.SelectedIndex = 0;

            tabTienNghi.Controls.Add(cboTinhTrangTN);

            btnThemTN =
                CreateButton(
                    "Thêm",
                    25,
                    130,
                    Color.FromArgb(5, 150, 105));

            btnSuaTN =
                CreateButton(
                    "Sửa",
                    145,
                    130,
                    Color.FromArgb(37, 99, 235));

            btnXoaTN =
                CreateButton(
                    "Xóa",
                    265,
                    130,
                    Color.FromArgb(220, 38, 38));

            btnMoiTN =
                CreateButton(
                    "Làm mới",
                    385,
                    130,
                    Color.FromArgb(71, 85, 105));

            tabTienNghi.Controls.Add(btnThemTN);
            tabTienNghi.Controls.Add(btnSuaTN);
            tabTienNghi.Controls.Add(btnXoaTN);
            tabTienNghi.Controls.Add(btnMoiTN);

            dgvTienNghi = CreateGrid();

            dgvTienNghi.Location =
                new Point(25, 200);

            dgvTienNghi.Size =
                new Size(1135, 315);

            dgvTienNghi.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabTienNghi.Controls.Add(dgvTienNghi);

            // =====================================================
            // TAB LẮP ĐẶT
            // =====================================================

            tabLapDat.Controls.Add(
                CreateLabel("Tiện nghi:", 25, 25));

            cboTienNghiLapDat =
                CreateCombo(150, 20, 300);

            tabLapDat.Controls.Add(cboTienNghiLapDat);

            tabLapDat.Controls.Add(
                CreateLabel("Phòng:", 25, 70));

            cboPhongLapDat =
                CreateCombo(150, 65, 300);

            tabLapDat.Controls.Add(cboPhongLapDat);

            tabLapDat.Controls.Add(
                CreateLabel("Ngày lắp:", 510, 25));

            dtpNgayLap = new DateTimePicker();

            dtpNgayLap.Location =
                new Point(630, 20);

            dtpNgayLap.Size =
                new Size(250, 30);

            dtpNgayLap.Format =
                DateTimePickerFormat.Short;

            tabLapDat.Controls.Add(dtpNgayLap);

            tabLapDat.Controls.Add(
                CreateLabel("Ngày tháo:", 510, 70));

            dtpNgayThaoDo = new DateTimePicker();

            dtpNgayThaoDo.Location =
                new Point(630, 65);

            dtpNgayThaoDo.Size =
                new Size(250, 30);

            dtpNgayThaoDo.Format =
                DateTimePickerFormat.Short;

            tabLapDat.Controls.Add(dtpNgayThaoDo);

            chkChuaThaoDo = new CheckBox();

            chkChuaThaoDo.Text =
                "Chưa tháo dỡ";

            chkChuaThaoDo.AutoSize = true;

            chkChuaThaoDo.Location =
                new Point(900, 68);

            chkChuaThaoDo.Checked = true;

            tabLapDat.Controls.Add(chkChuaThaoDo);

            btnLapDat =
                CreateButton(
                    "Lắp đặt",
                    25,
                    130,
                    Color.FromArgb(5, 150, 105));

            btnThaoDo =
                CreateButton(
                    "Tháo dỡ",
                    145,
                    130,
                    Color.FromArgb(234, 88, 12));

            btnMoiLapDat =
                CreateButton(
                    "Làm mới",
                    265,
                    130,
                    Color.FromArgb(71, 85, 105));

            tabLapDat.Controls.Add(btnLapDat);
            tabLapDat.Controls.Add(btnThaoDo);
            tabLapDat.Controls.Add(btnMoiLapDat);

            dgvLapDat = CreateGrid();

            dgvLapDat.Location =
                new Point(25, 200);

            dgvLapDat.Size =
                new Size(1135, 315);

            dgvLapDat.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabLapDat.Controls.Add(dgvLapDat);

            // =====================================================
            // ĐÓNG
            // =====================================================

            btnDong =
                CreateButton(
                    "Đóng",
                    1075,
                    695,
                    Color.FromArgb(220, 38, 38));

            btnDong.Size =
                new Size(150, 40);

            btnDong.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            Controls.Add(btnDong);

            // =====================================================
            // EVENTS
            // =====================================================

            Load += FrmPhongTienNghi_Load;

            btnThemPhong.Click += btnThemPhong_Click;
            btnSuaPhong.Click += btnSuaPhong_Click;
            btnXoaPhong.Click += btnXoaPhong_Click;
            btnMoiPhong.Click += btnMoiPhong_Click;

            dgvPhong.CellClick += dgvPhong_CellClick;

            btnThemTN.Click += btnThemTN_Click;
            btnSuaTN.Click += btnSuaTN_Click;
            btnXoaTN.Click += btnXoaTN_Click;
            btnMoiTN.Click += btnMoiTN_Click;

            dgvTienNghi.CellClick += dgvTienNghi_CellClick;

            btnLapDat.Click += btnLapDat_Click;
            btnThaoDo.Click += btnThaoDo_Click;
            btnMoiLapDat.Click += btnMoiLapDat_Click;

            dgvLapDat.CellClick += dgvLapDat_CellClick;

            chkChuaThaoDo.CheckedChanged +=
                chkChuaThaoDo_CheckedChanged;

            btnDong.Click += btnDong_Click;

            ResumeLayout(false);
        }

        // =========================================================
        // CONTROL HELPERS
        // =========================================================

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
            Color color)
        {
            Button button = new Button();

            button.Text = text;
            button.Location = new Point(x, y);

            button.Size =
                new Size(105, 40);

            button.BackColor = color;
            button.ForeColor = Color.White;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize = 0;

            button.Cursor = Cursors.Hand;

            button.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            return button;
        }

        private DataGridView CreateGrid()
        {
            return new DataGridView
            {
                ReadOnly = true,

                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,

                MultiSelect = false,

                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,

                BackgroundColor = Color.White,

                RowHeadersVisible = false
            };
        }
    }
}