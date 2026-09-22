namespace QuanLyThuVien.Forms
{
    partial class FrmDocGia
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvDocGia;

        private System.Windows.Forms.TextBox txtMaDocGia;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.TextBox txtPhai;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAnh3x4;

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.Label lblMaDocGia;
        private System.Windows.Forms.Label lblHo;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblPhai;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAnh3x4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvDocGia = new System.Windows.Forms.DataGridView();

            this.txtMaDocGia = new System.Windows.Forms.TextBox();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtPhai = new System.Windows.Forms.TextBox();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAnh3x4 = new System.Windows.Forms.TextBox();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.lblMaDocGia = new System.Windows.Forms.Label();
            this.lblHo = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblPhai = new System.Windows.Forms.Label();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAnh3x4 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).BeginInit();

            this.SuspendLayout();

            // =========================
            // LABEL
            // =========================

            this.lblMaDocGia.AutoSize = true;
            this.lblMaDocGia.Location = new System.Drawing.Point(20, 20);
            this.lblMaDocGia.Text = "Mã độc giả:";

            this.lblHo.AutoSize = true;
            this.lblHo.Location = new System.Drawing.Point(20, 60);
            this.lblHo.Text = "Họ:";

            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(300, 60);
            this.lblTen.Text = "Tên:";

            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new System.Drawing.Point(20, 100);
            this.lblNgaySinh.Text = "Ngày sinh:";

            this.lblPhai.AutoSize = true;
            this.lblPhai.Location = new System.Drawing.Point(300, 100);
            this.lblPhai.Text = "Phái:";

            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new System.Drawing.Point(20, 140);
            this.lblSoDienThoai.Text = "Số điện thoại:";

            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(20, 180);
            this.lblDiaChi.Text = "Địa chỉ:";

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 220);
            this.lblEmail.Text = "Email:";

            this.lblAnh3x4.AutoSize = true;
            this.lblAnh3x4.Location = new System.Drawing.Point(20, 260);
            this.lblAnh3x4.Text = "Ảnh 3x4:";

            // =========================
            // TEXTBOX
            // =========================

            this.txtMaDocGia.Location = new System.Drawing.Point(110, 17);
            this.txtMaDocGia.Size = new System.Drawing.Size(150, 22);

            this.txtHo.Location = new System.Drawing.Point(110, 57);
            this.txtHo.Size = new System.Drawing.Size(150, 22);

            this.txtTen.Location = new System.Drawing.Point(350, 57);
            this.txtTen.Size = new System.Drawing.Size(150, 22);

            this.dtpNgaySinh.Location = new System.Drawing.Point(110, 97);
            this.dtpNgaySinh.Size = new System.Drawing.Size(150, 22);
            this.dtpNgaySinh.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.txtPhai.Location = new System.Drawing.Point(350, 97);
            this.txtPhai.Size = new System.Drawing.Size(150, 22);

            this.txtSoDienThoai.Location = new System.Drawing.Point(110, 137);
            this.txtSoDienThoai.Size = new System.Drawing.Size(200, 22);

            this.txtDiaChi.Location = new System.Drawing.Point(110, 177);
            this.txtDiaChi.Size = new System.Drawing.Size(390, 22);

            this.txtEmail.Location = new System.Drawing.Point(110, 217);
            this.txtEmail.Size = new System.Drawing.Size(390, 22);

            this.txtAnh3x4.Location = new System.Drawing.Point(110, 257);
            this.txtAnh3x4.Size = new System.Drawing.Size(390, 22);

            // =========================
            // BUTTON
            // =========================

            this.btnThem.Location = new System.Drawing.Point(550, 17);
            this.btnThem.Size = new System.Drawing.Size(90, 35);
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click +=
                new System.EventHandler(this.btnThem_Click);

            this.btnSua.Location = new System.Drawing.Point(650, 17);
            this.btnSua.Size = new System.Drawing.Size(90, 35);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click +=
                new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Location = new System.Drawing.Point(750, 17);
            this.btnXoa.Size = new System.Drawing.Size(90, 35);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click +=
                new System.EventHandler(this.btnXoa_Click);

            this.btnDong.Location = new System.Drawing.Point(850, 17);
            this.btnDong.Size = new System.Drawing.Size(90, 35);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click +=
                new System.EventHandler(this.btnDong_Click);

            // =========================
            // DATAGRIDVIEW
            // =========================

            this.dgvDocGia.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvDocGia.Location =
                new System.Drawing.Point(20, 310);

            this.dgvDocGia.Name = "dgvDocGia";

            this.dgvDocGia.RowHeadersWidth = 51;

            this.dgvDocGia.Size =
                new System.Drawing.Size(920, 300);

            this.dgvDocGia.TabIndex = 20;

            this.dgvDocGia.CellClick +=
                new System.Windows.Forms.DataGridViewCellEventHandler(
                    this.dgvDocGia_CellClick);

            // =========================
            // FORM
            // =========================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(8F, 16F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(970, 640);

            this.Controls.Add(this.lblMaDocGia);
            this.Controls.Add(this.lblHo);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.lblPhai);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblAnh3x4);

            this.Controls.Add(this.txtMaDocGia);
            this.Controls.Add(this.txtHo);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.txtPhai);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtAnh3x4);

            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnDong);

            this.Controls.Add(this.dgvDocGia);

            this.Name = "FrmDocGia";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Quản lý độc giả";

            this.Load +=
                new System.EventHandler(this.FrmDocGia_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}