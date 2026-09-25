namespace QuanLyKhachSan.Forms
{
    partial class FrmSuDungDichVu
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;

        private GroupBox grpThongTin;
        private ComboBox cboPhieuDat;
        private ComboBox cboPhong;
        private DateTimePicker dtpNgaySuDung;
        private ComboBox cboNhanVien;

        private GroupBox grpDichVu;
        private ComboBox cboDichVu;
        private NumericUpDown nudSoLuong;
        private TextBox txtDonGia;
        private TextBox txtThanhTien;

        private Button btnThemDichVu;
        private Button btnLamMoi;
        private Button btnXoaChiTiet;

        private DataGridView dgvChiTiet;

        private Label lblTongTienText;
        private Label lblTongTien;

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

            ClientSize = new Size(1250, 760);
            MinimumSize = new Size(1150, 700);

            BackColor = Color.FromArgb(241, 245, 249);

            Font = new Font("Segoe UI", 10F);

            StartPosition =
                FormStartPosition.CenterScreen;

            Text = "Sử dụng dịch vụ";

            // =====================================================
            // HEADER
            // =====================================================

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 105,

                BackColor =
                    Color.FromArgb(124, 58, 237)
            };

            lblTitle = new Label
            {
                Text = "SỬ DỤNG DỊCH VỤ",

                Font = new Font(
                    "Segoe UI",
                    22F,
                    FontStyle.Bold),

                ForeColor = Color.White,
                AutoSize = true,

                Location = new Point(35, 22)
            };

            lblSubtitle = new Label
            {
                Text =
                    "Quản lý dịch vụ phát sinh trong thời gian lưu trú",

                Font = new Font(
                    "Segoe UI",
                    10F),

                ForeColor =
                    Color.FromArgb(237, 233, 254),

                AutoSize = true,

                Location = new Point(38, 67)
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);

            Controls.Add(pnlHeader);

            // =====================================================
            // THÔNG TIN PHÒNG
            // =====================================================

            grpThongTin = new GroupBox
            {
                Text = "Thông tin lưu trú",

                Location = new Point(25, 125),

                Size = new Size(1200, 130),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            grpThongTin.Controls.Add(
                CreateLabel(
                    "Phiếu đặt:",
                    20,
                    35));

            cboPhieuDat =
                CreateCombo(
                    120,
                    30,
                    310);

            grpThongTin.Controls.Add(
                cboPhieuDat);

            grpThongTin.Controls.Add(
                CreateLabel(
                    "Phòng:",
                    470,
                    35));

            cboPhong =
                CreateCombo(
                    550,
                    30,
                    200);

            grpThongTin.Controls.Add(
                cboPhong);

            grpThongTin.Controls.Add(
                CreateLabel(
                    "Ngày sử dụng:",
                    790,
                    35));

            dtpNgaySuDung =
                new DateTimePicker
                {
                    Location =
                        new Point(915, 30),

                    Size =
                        new Size(220, 30),

                    Format =
                        DateTimePickerFormat.Short
                };

            grpThongTin.Controls.Add(
                dtpNgaySuDung);

            grpThongTin.Controls.Add(
                CreateLabel(
                    "Nhân viên:",
                    20,
                    82));

            cboNhanVien =
                CreateCombo(
                    120,
                    77,
                    310);

            grpThongTin.Controls.Add(
                cboNhanVien);

            Controls.Add(grpThongTin);

            // =====================================================
            // DỊCH VỤ
            // =====================================================

            grpDichVu = new GroupBox
            {
                Text = "Dịch vụ sử dụng",

                Location =
                    new Point(25, 270),

                Size =
                    new Size(1200, 150),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            grpDichVu.Controls.Add(
                CreateLabel(
                    "Dịch vụ:",
                    20,
                    35));

            cboDichVu =
                CreateCombo(
                    110,
                    30,
                    300);

            grpDichVu.Controls.Add(
                cboDichVu);

            grpDichVu.Controls.Add(
                CreateLabel(
                    "Số lượng:",
                    450,
                    35));

            nudSoLuong =
                new NumericUpDown
                {
                    Location =
                        new Point(540, 30),

                    Size =
                        new Size(120, 30),

                    Minimum = 1,
                    Maximum = 10000,
                    Value = 1
                };

            grpDichVu.Controls.Add(
                nudSoLuong);

            grpDichVu.Controls.Add(
                CreateLabel(
                    "Đơn giá:",
                    700,
                    35));

            txtDonGia =
                CreateTextBox(
                    780,
                    30,
                    160);

            txtDonGia.ReadOnly = true;

            txtDonGia.TextAlign =
                HorizontalAlignment.Right;

            grpDichVu.Controls.Add(
                txtDonGia);

            grpDichVu.Controls.Add(
                CreateLabel(
                    "Thành tiền:",
                    20,
                    85));

            txtThanhTien =
                CreateTextBox(
                    130,
                    80,
                    280);

            txtThanhTien.ReadOnly = true;

            txtThanhTien.TextAlign =
                HorizontalAlignment.Right;

            txtThanhTien.Font =
                new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold);

            grpDichVu.Controls.Add(
                txtThanhTien);

            btnThemDichVu =
                CreateButton(
                    "THÊM DỊCH VỤ",
                    450,
                    78,
                    170,
                    Color.FromArgb(5, 150, 105));

            btnLamMoi =
                CreateButton(
                    "LÀM MỚI",
                    635,
                    78,
                    140,
                    Color.FromArgb(71, 85, 105));

            btnXoaChiTiet =
                CreateButton(
                    "XÓA DỊCH VỤ",
                    790,
                    78,
                    160,
                    Color.FromArgb(220, 38, 38));

            grpDichVu.Controls.Add(
                btnThemDichVu);

            grpDichVu.Controls.Add(
                btnLamMoi);

            grpDichVu.Controls.Add(
                btnXoaChiTiet);

            Controls.Add(grpDichVu);

            // =====================================================
            // GRID
            // =====================================================

            dgvChiTiet = CreateGrid();

            dgvChiTiet.Location =
                new Point(25, 440);

            dgvChiTiet.Size =
                new Size(1200, 220);

            dgvChiTiet.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            Controls.Add(dgvChiTiet);

            // =====================================================
            // TỔNG TIỀN
            // =====================================================

            lblTongTienText = new Label
            {
                Text = "TỔNG TIỀN DỊCH VỤ:",

                Font = new Font(
                    "Segoe UI",
                    12F,
                    FontStyle.Bold),

                AutoSize = true,

                Location =
                    new Point(700, 685),

                Anchor =
                    AnchorStyles.Bottom |
                    AnchorStyles.Right
            };

            lblTongTien = new Label
            {
                Text = "0 VNĐ",

                Font = new Font(
                    "Segoe UI",
                    15F,
                    FontStyle.Bold),

                ForeColor =
                    Color.FromArgb(220, 38, 38),

                AutoSize = true,

                Location =
                    new Point(920, 680),

                Anchor =
                    AnchorStyles.Bottom |
                    AnchorStyles.Right
            };

            Controls.Add(lblTongTienText);
            Controls.Add(lblTongTien);

            // =====================================================
            // ĐÓNG
            // =====================================================

            btnDong =
                CreateButton(
                    "ĐÓNG",
                    25,
                    680,
                    140,
                    Color.FromArgb(220, 38, 38));

            btnDong.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Left;

            Controls.Add(btnDong);

            // =====================================================
            // EVENTS
            // =====================================================

            Load +=
                FrmSuDungDichVu_Load;

            cboPhieuDat.SelectedIndexChanged +=
                cboPhieuDat_SelectedIndexChanged;

            cboPhong.SelectedIndexChanged +=
                cboPhong_SelectedIndexChanged;

            dtpNgaySuDung.ValueChanged +=
                dtpNgaySuDung_ValueChanged;

            cboDichVu.SelectedIndexChanged +=
                cboDichVu_SelectedIndexChanged;

            nudSoLuong.ValueChanged +=
                nudSoLuong_ValueChanged;

            btnThemDichVu.Click +=
                btnThemDichVu_Click;

            btnLamMoi.Click +=
                btnLamMoi_Click;

            btnXoaChiTiet.Click +=
                btnXoaChiTiet_Click;

            btnDong.Click +=
                btnDong_Click;

            ResumeLayout(false);
            PerformLayout();
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

                Location =
                    new Point(x, y),

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
                Location =
                    new Point(x, y),

                Size =
                    new Size(width, 30)
            };
        }

        private ComboBox CreateCombo(
            int x,
            int y,
            int width)
        {
            return new ComboBox
            {
                Location =
                    new Point(x, y),

                Size =
                    new Size(width, 30),

                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };
        }

        private Button CreateButton(
            string text,
            int x,
            int y,
            int width,
            Color color)
        {
            Button button = new Button
            {
                Text = text,

                Location =
                    new Point(x, y),

                Size =
                    new Size(width, 42),

                BackColor = color,
                ForeColor = Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),

                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;

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