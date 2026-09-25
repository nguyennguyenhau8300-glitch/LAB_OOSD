namespace QuanLyKhachSan.Forms
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;

        private TabControl tabDanhMuc;

        private TabPage tabKhuVuc;
        private TabPage tabKhachHang;
        private TabPage tabNhanVien;
        private TabPage tabDichVu;

        // ================= KHU VỰC =================

        private DataGridView dgvKhuVuc;
        private TextBox txtMaKhuVuc;
        private TextBox txtTenKhuVuc;

        private Label lblMaKhuVuc;
        private Label lblTenKhuVuc;

        private Button btnThemKhuVuc;
        private Button btnSuaKhuVuc;
        private Button btnXoaKhuVuc;
        private Button btnLamMoiKhuVuc;

        // ================= KHÁCH HÀNG =================

        private DataGridView dgvKhachHang;

        private TextBox txtMaKhach;
        private TextBox txtHoTenKhach;
        private TextBox txtCCCD;
        private TextBox txtSDTKhach;
        private TextBox txtDiaChi;
        private TextBox txtQuocTich;

        private Button btnThemKhach;
        private Button btnSuaKhach;
        private Button btnXoaKhach;
        private Button btnLamMoiKhach;

        // ================= NHÂN VIÊN =================

        private DataGridView dgvNhanVien;

        private TextBox txtMaNV;
        private TextBox txtHoTenNV;
        private TextBox txtChucVu;
        private TextBox txtSDTNV;

        private ComboBox cboTrangThaiNV;

        private Button btnThemNV;
        private Button btnSuaNV;
        private Button btnXoaNV;
        private Button btnLamMoiNV;

        // ================= DỊCH VỤ =================

        private DataGridView dgvDichVu;

        private TextBox txtMaDV;
        private TextBox txtTenDV;
        private TextBox txtDonViTinh;
        private NumericUpDown nudDonGia;

        private Button btnThemDV;
        private Button btnSuaDV;
        private Button btnXoaDV;
        private Button btnLamMoiDV;

        private Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();

            tabDanhMuc = new TabControl();

            tabKhuVuc = new TabPage();
            tabKhachHang = new TabPage();
            tabNhanVien = new TabPage();
            tabDichVu = new TabPage();

            // =====================================================
            // FORM
            // =====================================================

            SuspendLayout();

            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý danh mục";
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1100, 650);

            // =====================================================
            // HEADER
            // =====================================================

            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 90;
            pnlHeader.BackColor = Color.FromArgb(30, 64, 175);

            lblTitle.Text = "QUẢN LÝ DANH MỤC";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font(
                "Segoe UI",
                22F,
                FontStyle.Bold);

            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(35, 25);

            pnlHeader.Controls.Add(lblTitle);

            Controls.Add(pnlHeader);

            // =====================================================
            // TAB CONTROL
            // =====================================================

            tabDanhMuc.Location = new Point(25, 110);
            tabDanhMuc.Size = new Size(1150, 540);

            tabDanhMuc.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabDanhMuc.Font =
                new Font("Segoe UI", 11F);

            tabKhuVuc.Text = "Khu vực";
            tabKhachHang.Text = "Khách hàng";
            tabNhanVien.Text = "Nhân viên";
            tabDichVu.Text = "Dịch vụ";

            tabDanhMuc.TabPages.Add(tabKhuVuc);
            tabDanhMuc.TabPages.Add(tabKhachHang);
            tabDanhMuc.TabPages.Add(tabNhanVien);
            tabDanhMuc.TabPages.Add(tabDichVu);

            Controls.Add(tabDanhMuc);

            // =====================================================
            // KHU VỰC
            // =====================================================

            lblMaKhuVuc = CreateLabel(
                "Mã khu vực:",
                25,
                30);

            txtMaKhuVuc = CreateTextBox(
                150,
                25,
                220);

            lblTenKhuVuc = CreateLabel(
                "Tên khu vực:",
                25,
                80);

            txtTenKhuVuc = CreateTextBox(
                150,
                75,
                220);

            btnThemKhuVuc = CreateButton(
                "Thêm",
                25,
                140,
                Color.FromArgb(5, 150, 105));

            btnSuaKhuVuc = CreateButton(
                "Sửa",
                145,
                140,
                Color.FromArgb(37, 99, 235));

            btnXoaKhuVuc = CreateButton(
                "Xóa",
                265,
                140,
                Color.FromArgb(220, 38, 38));

            btnLamMoiKhuVuc = CreateButton(
                "Làm mới",
                385,
                140,
                Color.FromArgb(71, 85, 105));

            dgvKhuVuc = CreateGrid();

            dgvKhuVuc.Location =
                new Point(25, 210);

            dgvKhuVuc.Size =
                new Size(1075, 270);

            dgvKhuVuc.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabKhuVuc.Controls.AddRange(
                new Control[]
                {
                    lblMaKhuVuc,
                    txtMaKhuVuc,
                    lblTenKhuVuc,
                    txtTenKhuVuc,
                    btnThemKhuVuc,
                    btnSuaKhuVuc,
                    btnXoaKhuVuc,
                    btnLamMoiKhuVuc,
                    dgvKhuVuc
                });

            // =====================================================
            // KHÁCH HÀNG
            // =====================================================

            Label lblMaKhach =
                CreateLabel("Mã khách:", 25, 25);

            txtMaKhach =
                CreateTextBox(140, 20, 220);

            Label lblHoTenKhach =
                CreateLabel("Họ tên:", 25, 70);

            txtHoTenKhach =
                CreateTextBox(140, 65, 220);

            Label lblCCCD =
                CreateLabel("CCCD:", 25, 115);

            txtCCCD =
                CreateTextBox(140, 110, 220);

            Label lblSDTKhach =
                CreateLabel("Điện thoại:", 410, 25);

            txtSDTKhach =
                CreateTextBox(530, 20, 220);

            Label lblDiaChi =
                CreateLabel("Địa chỉ:", 410, 70);

            txtDiaChi =
                CreateTextBox(530, 65, 300);

            Label lblQuocTich =
                CreateLabel("Quốc tịch:", 410, 115);

            txtQuocTich =
                CreateTextBox(530, 110, 220);

            txtQuocTich.Text = "Việt Nam";

            btnThemKhach = CreateButton(
                "Thêm",
                25,
                165,
                Color.FromArgb(5, 150, 105));

            btnSuaKhach = CreateButton(
                "Sửa",
                145,
                165,
                Color.FromArgb(37, 99, 235));

            btnXoaKhach = CreateButton(
                "Xóa",
                265,
                165,
                Color.FromArgb(220, 38, 38));

            btnLamMoiKhach = CreateButton(
                "Làm mới",
                385,
                165,
                Color.FromArgb(71, 85, 105));

            dgvKhachHang = CreateGrid();

            dgvKhachHang.Location =
                new Point(25, 225);

            dgvKhachHang.Size =
                new Size(1075, 255);

            dgvKhachHang.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabKhachHang.Controls.AddRange(
                new Control[]
                {
                    lblMaKhach,
                    txtMaKhach,
                    lblHoTenKhach,
                    txtHoTenKhach,
                    lblCCCD,
                    txtCCCD,
                    lblSDTKhach,
                    txtSDTKhach,
                    lblDiaChi,
                    txtDiaChi,
                    lblQuocTich,
                    txtQuocTich,

                    btnThemKhach,
                    btnSuaKhach,
                    btnXoaKhach,
                    btnLamMoiKhach,

                    dgvKhachHang
                });

            // =====================================================
            // NHÂN VIÊN
            // =====================================================

            Label lblMaNV =
                CreateLabel("Mã NV:", 25, 25);

            txtMaNV =
                CreateTextBox(140, 20, 220);

            Label lblHoTenNV =
                CreateLabel("Họ tên:", 25, 70);

            txtHoTenNV =
                CreateTextBox(140, 65, 220);

            Label lblChucVu =
                CreateLabel("Chức vụ:", 25, 115);

            txtChucVu =
                CreateTextBox(140, 110, 220);

            Label lblSDTNV =
                CreateLabel("Điện thoại:", 410, 25);

            txtSDTNV =
                CreateTextBox(530, 20, 220);

            Label lblTrangThaiNV =
                CreateLabel("Trạng thái:", 410, 70);

            cboTrangThaiNV = new ComboBox();

            cboTrangThaiNV.Location =
                new Point(530, 65);

            cboTrangThaiNV.Size =
                new Size(220, 30);

            cboTrangThaiNV.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboTrangThaiNV.Items.AddRange(
                new object[]
                {
                    "Đang làm",
                    "Nghỉ"
                });

            cboTrangThaiNV.SelectedIndex = 0;

            btnThemNV = CreateButton(
                "Thêm",
                25,
                165,
                Color.FromArgb(5, 150, 105));

            btnSuaNV = CreateButton(
                "Sửa",
                145,
                165,
                Color.FromArgb(37, 99, 235));

            btnXoaNV = CreateButton(
                "Xóa",
                265,
                165,
                Color.FromArgb(220, 38, 38));

            btnLamMoiNV = CreateButton(
                "Làm mới",
                385,
                165,
                Color.FromArgb(71, 85, 105));

            dgvNhanVien = CreateGrid();

            dgvNhanVien.Location =
                new Point(25, 225);

            dgvNhanVien.Size =
                new Size(1075, 255);

            dgvNhanVien.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabNhanVien.Controls.AddRange(
                new Control[]
                {
                    lblMaNV,
                    txtMaNV,
                    lblHoTenNV,
                    txtHoTenNV,
                    lblChucVu,
                    txtChucVu,
                    lblSDTNV,
                    txtSDTNV,
                    lblTrangThaiNV,
                    cboTrangThaiNV,

                    btnThemNV,
                    btnSuaNV,
                    btnXoaNV,
                    btnLamMoiNV,

                    dgvNhanVien
                });

            // =====================================================
            // DỊCH VỤ
            // =====================================================

            Label lblMaDV =
                CreateLabel("Mã dịch vụ:", 25, 25);

            txtMaDV =
                CreateTextBox(150, 20, 220);

            Label lblTenDV =
                CreateLabel("Tên dịch vụ:", 25, 70);

            txtTenDV =
                CreateTextBox(150, 65, 220);

            Label lblDonVi =
                CreateLabel("Đơn vị tính:", 420, 25);

            txtDonViTinh =
                CreateTextBox(540, 20, 220);

            Label lblDonGia =
                CreateLabel("Đơn giá:", 420, 70);

            nudDonGia = new NumericUpDown();

            nudDonGia.Location =
                new Point(540, 65);

            nudDonGia.Size =
                new Size(220, 30);

            nudDonGia.Maximum = 100000000;
            nudDonGia.ThousandsSeparator = true;

            btnThemDV = CreateButton(
                "Thêm",
                25,
                140,
                Color.FromArgb(5, 150, 105));

            btnSuaDV = CreateButton(
                "Sửa",
                145,
                140,
                Color.FromArgb(37, 99, 235));

            btnXoaDV = CreateButton(
                "Xóa",
                265,
                140,
                Color.FromArgb(220, 38, 38));

            btnLamMoiDV = CreateButton(
                "Làm mới",
                385,
                140,
                Color.FromArgb(71, 85, 105));

            dgvDichVu = CreateGrid();

            dgvDichVu.Location =
                new Point(25, 210);

            dgvDichVu.Size =
                new Size(1075, 270);

            dgvDichVu.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            tabDichVu.Controls.AddRange(
                new Control[]
                {
                    lblMaDV,
                    txtMaDV,
                    lblTenDV,
                    txtTenDV,
                    lblDonVi,
                    txtDonViTinh,
                    lblDonGia,
                    nudDonGia,

                    btnThemDV,
                    btnSuaDV,
                    btnXoaDV,
                    btnLamMoiDV,

                    dgvDichVu
                });

            // =====================================================
            // ĐÓNG
            // =====================================================

            btnDong = CreateButton(
                "Đóng",
                1025,
                665,
                Color.FromArgb(220, 38, 38));

            btnDong.Size = new Size(150, 40);

            btnDong.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            Controls.Add(btnDong);

            // =====================================================
            // EVENTS
            // =====================================================

            Load += FrmDanhMuc_Load;

            btnThemKhuVuc.Click += btnThemKhuVuc_Click;
            btnSuaKhuVuc.Click += btnSuaKhuVuc_Click;
            btnXoaKhuVuc.Click += btnXoaKhuVuc_Click;
            btnLamMoiKhuVuc.Click += btnLamMoiKhuVuc_Click;

            dgvKhuVuc.CellClick += dgvKhuVuc_CellClick;

            btnThemKhach.Click += btnThemKhach_Click;
            btnSuaKhach.Click += btnSuaKhach_Click;
            btnXoaKhach.Click += btnXoaKhach_Click;
            btnLamMoiKhach.Click += btnLamMoiKhach_Click;

            dgvKhachHang.CellClick += dgvKhachHang_CellClick;

            btnThemNV.Click += btnThemNV_Click;
            btnSuaNV.Click += btnSuaNV_Click;
            btnXoaNV.Click += btnXoaNV_Click;
            btnLamMoiNV.Click += btnLamMoiNV_Click;

            dgvNhanVien.CellClick += dgvNhanVien_CellClick;

            btnThemDV.Click += btnThemDV_Click;
            btnSuaDV.Click += btnSuaDV_Click;
            btnXoaDV.Click += btnXoaDV_Click;
            btnLamMoiDV.Click += btnLamMoiDV_Click;

            dgvDichVu.CellClick += dgvDichVu_CellClick;

            btnDong.Click += btnDong_Click;

            ResumeLayout(false);
        }

        // =========================================================
        // HÀM TẠO CONTROL
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

        private Button CreateButton(
            string text,
            int x,
            int y,
            Color color)
        {
            Button button = new Button();

            button.Text = text;

            button.Location =
                new Point(x, y);

            button.Size =
                new Size(105, 40);

            button.BackColor = color;
            button.ForeColor = Color.White;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize = 0;

            button.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            button.Cursor = Cursors.Hand;

            return button;
        }

        private DataGridView CreateGrid()
        {
            DataGridView grid =
                new DataGridView();

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;

            grid.ReadOnly = true;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.MultiSelect = false;

            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.BackgroundColor = Color.White;

            grid.BorderStyle =
                BorderStyle.Fixed3D;

            grid.RowHeadersVisible = false;

            return grid;
        }
    }
}