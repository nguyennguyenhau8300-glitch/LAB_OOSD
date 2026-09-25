namespace QuanLyKhachSan.Forms
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTongPhong;
        private Label lblPhongTrong;
        private Label lblDangO;
        private Label lblDoanhThu;

        private DateTimePicker dtpTuNgay;
        private DateTimePicker dtpDenNgay;

        private Button btnThongKe;
        private Button btnDong;

        private DataGridView dgvHoaDon;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ClientSize = new Size(1200, 720);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 10F);
            Text = "Thống kê";

            Label title = new Label
            {
                Text = "THỐNG KÊ KHÁCH SẠN",
                Font = new Font(
                    "Segoe UI",
                    22F,
                    FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Controls.Add(title);

            lblTongPhong =
                Card("TỔNG PHÒNG", 30, 90);

            lblPhongTrong =
                Card("PHÒNG TRỐNG", 310, 90);

            lblDangO =
                Card("ĐANG Ở", 590, 90);

            lblDoanhThu =
                Card("DOANH THU", 870, 90);

            Controls.Add(lblTongPhong);
            Controls.Add(lblPhongTrong);
            Controls.Add(lblDangO);
            Controls.Add(lblDoanhThu);

            Controls.Add(
                new Label
                {
                    Text = "Từ ngày:",
                    AutoSize = true,
                    Location = new Point(30, 225)
                });

            dtpTuNgay = new DateTimePicker
            {
                Location = new Point(105, 220),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Short
            };

            Controls.Add(dtpTuNgay);

            Controls.Add(
                new Label
                {
                    Text = "Đến ngày:",
                    AutoSize = true,
                    Location = new Point(340, 225)
                });

            dtpDenNgay = new DateTimePicker
            {
                Location = new Point(425, 220),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Short
            };

            Controls.Add(dtpDenNgay);

            btnThongKe = new Button
            {
                Text = "THỐNG KÊ",
                Location = new Point(660, 215),
                Size = new Size(150, 40)
            };

            Controls.Add(btnThongKe);

            dgvHoaDon = new DataGridView
            {
                Location = new Point(30, 280),
                Size = new Size(1140, 350),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            Controls.Add(dgvHoaDon);

            btnDong = new Button
            {
                Text = "ĐÓNG",
                Location = new Point(30, 650),
                Size = new Size(140, 40),
                Anchor =
                    AnchorStyles.Bottom |
                    AnchorStyles.Left
            };

            Controls.Add(btnDong);

            Load += FrmThongKe_Load;
            btnThongKe.Click += btnThongKe_Click;
            btnDong.Click += (s, e) => Close();
        }

        private Label Card(
            string title,
            int x,
            int y)
        {
            return new Label
            {
                Text = title + "\n0",
                Location = new Point(x, y),
                Size = new Size(250, 100),

                BackColor =
                    Color.FromArgb(255, 255, 255),

                BorderStyle =
                    BorderStyle.FixedSingle,

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI",
                    13F,
                    FontStyle.Bold)
            };
        }
    }
}