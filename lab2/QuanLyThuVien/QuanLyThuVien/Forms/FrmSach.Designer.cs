namespace QuanLyThuVien.Forms
{
    partial class FrmSach
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMaDauSach;
        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.Label lblNamXuatBan;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.Label lblNhaXuatBan;

        private System.Windows.Forms.TextBox txtMaDauSach;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.TextBox txtNamXuatBan;
        private System.Windows.Forms.TextBox txtSoLuong;

        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.ComboBox cboNhaXuatBan;

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.DataGridView dgvSach;

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
            this.lblTieuDe = new System.Windows.Forms.Label();

            this.lblMaDauSach = new System.Windows.Forms.Label();
            this.lblTenSach = new System.Windows.Forms.Label();
            this.lblNamXuatBan = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTheLoai = new System.Windows.Forms.Label();
            this.lblNhaXuatBan = new System.Windows.Forms.Label();

            this.txtMaDauSach = new System.Windows.Forms.TextBox();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.txtNamXuatBan = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();

            this.cboTheLoai = new System.Windows.Forms.ComboBox();
            this.cboNhaXuatBan = new System.Windows.Forms.ComboBox();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.dgvSach = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();

            this.SuspendLayout();

            // =====================================================
            // TIÊU ĐỀ
            // =====================================================

            this.lblTieuDe.AutoSize = true;

            this.lblTieuDe.Font =
                new System.Drawing.Font(
                    "Microsoft Sans Serif",
                    16F,
                    System.Drawing.FontStyle.Bold);

            this.lblTieuDe.Location =
                new System.Drawing.Point(350, 20);

            this.lblTieuDe.Name =
                "lblTieuDe";

            this.lblTieuDe.Size =
                new System.Drawing.Size(170, 26);

            this.lblTieuDe.Text =
                "QUẢN LÝ SÁCH";

            // =====================================================
            // MÃ ĐẦU SÁCH
            // =====================================================

            this.lblMaDauSach.AutoSize = true;

            this.lblMaDauSach.Location =
                new System.Drawing.Point(30, 80);

            this.lblMaDauSach.Name =
                "lblMaDauSach";

            this.lblMaDauSach.Size =
                new System.Drawing.Size(83, 16);

            this.lblMaDauSach.Text =
                "Mã đầu sách:";

            // =====================================================
            // TEXTBOX MÃ SÁCH
            // =====================================================

            this.txtMaDauSach.Location =
                new System.Drawing.Point(140, 75);

            this.txtMaDauSach.Name =
                "txtMaDauSach";

            this.txtMaDauSach.Size =
                new System.Drawing.Size(220, 22);

            // =====================================================
            // TÊN SÁCH
            // =====================================================

            this.lblTenSach.AutoSize = true;

            this.lblTenSach.Location =
                new System.Drawing.Point(400, 80);

            this.lblTenSach.Name =
                "lblTenSach";

            this.lblTenSach.Size =
                new System.Drawing.Size(63, 16);

            this.lblTenSach.Text =
                "Tên sách:";

            // =====================================================
            // TEXTBOX TÊN SÁCH
            // =====================================================

            this.txtTenSach.Location =
                new System.Drawing.Point(480, 75);

            this.txtTenSach.Name =
                "txtTenSach";

            this.txtTenSach.Size =
                new System.Drawing.Size(300, 22);

            // =====================================================
            // NĂM XUẤT BẢN
            // =====================================================

            this.lblNamXuatBan.AutoSize = true;

            this.lblNamXuatBan.Location =
                new System.Drawing.Point(30, 125);

            this.lblNamXuatBan.Name =
                "lblNamXuatBan";

            this.lblNamXuatBan.Size =
                new System.Drawing.Size(91, 16);

            this.lblNamXuatBan.Text =
                "Năm xuất bản:";

            // =====================================================
            // TEXTBOX NĂM
            // =====================================================

            this.txtNamXuatBan.Location =
                new System.Drawing.Point(140, 120);

            this.txtNamXuatBan.Name =
                "txtNamXuatBan";

            this.txtNamXuatBan.Size =
                new System.Drawing.Size(220, 22);

            // =====================================================
            // SỐ LƯỢNG
            // =====================================================

            this.lblSoLuong.AutoSize = true;

            this.lblSoLuong.Location =
                new System.Drawing.Point(400, 125);

            this.lblSoLuong.Name =
                "lblSoLuong";

            this.lblSoLuong.Size =
                new System.Drawing.Size(65, 16);

            this.lblSoLuong.Text =
                "Số lượng:";

            // =====================================================
            // TEXTBOX SỐ LƯỢNG
            // =====================================================

            this.txtSoLuong.Location =
                new System.Drawing.Point(480, 120);

            this.txtSoLuong.Name =
                "txtSoLuong";

            this.txtSoLuong.Size =
                new System.Drawing.Size(300, 22);

            // =====================================================
            // THỂ LOẠI
            // =====================================================

            this.lblTheLoai.AutoSize = true;

            this.lblTheLoai.Location =
                new System.Drawing.Point(30, 170);

            this.lblTheLoai.Name =
                "lblTheLoai";

            this.lblTheLoai.Size =
                new System.Drawing.Size(58, 16);

            this.lblTheLoai.Text =
                "Thể loại:";

            // =====================================================
            // COMBOBOX THỂ LOẠI
            // =====================================================

            this.cboTheLoai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboTheLoai.FormattingEnabled = true;

            this.cboTheLoai.Location =
                new System.Drawing.Point(140, 165);

            this.cboTheLoai.Name =
                "cboTheLoai";

            this.cboTheLoai.Size =
                new System.Drawing.Size(220, 24);

            // =====================================================
            // NHÀ XUẤT BẢN
            // =====================================================

            this.lblNhaXuatBan.AutoSize = true;

            this.lblNhaXuatBan.Location =
                new System.Drawing.Point(400, 170);

            this.lblNhaXuatBan.Name =
                "lblNhaXuatBan";

            this.lblNhaXuatBan.Size =
                new System.Drawing.Size(92, 16);

            this.lblNhaXuatBan.Text =
                "Nhà xuất bản:";

            // =====================================================
            // COMBOBOX NHÀ XUẤT BẢN
            // =====================================================

            this.cboNhaXuatBan.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboNhaXuatBan.FormattingEnabled = true;

            this.cboNhaXuatBan.Location =
                new System.Drawing.Point(480, 165);

            this.cboNhaXuatBan.Name =
                "cboNhaXuatBan";

            this.cboNhaXuatBan.Size =
                new System.Drawing.Size(300, 24);

            // =====================================================
            // NÚT THÊM
            // =====================================================

            this.btnThem.Location =
                new System.Drawing.Point(30, 215);

            this.btnThem.Name =
                "btnThem";

            this.btnThem.Size =
                new System.Drawing.Size(100, 35);

            this.btnThem.Text =
                "Thêm";

            this.btnThem.UseVisualStyleBackColor = true;

            // =====================================================
            // NÚT SỬA
            // =====================================================

            this.btnSua.Location =
                new System.Drawing.Point(140, 215);

            this.btnSua.Name =
                "btnSua";

            this.btnSua.Size =
                new System.Drawing.Size(100, 35);

            this.btnSua.Text =
                "Sửa";

            this.btnSua.UseVisualStyleBackColor = true;

            // =====================================================
            // NÚT XÓA
            // =====================================================

            this.btnXoa.Location =
                new System.Drawing.Point(250, 215);

            this.btnXoa.Name =
                "btnXoa";

            this.btnXoa.Size =
                new System.Drawing.Size(100, 35);

            this.btnXoa.Text =
                "Xóa";

            this.btnXoa.UseVisualStyleBackColor = true;

            // =====================================================
            // DATAGRIDVIEW
            // =====================================================

            this.dgvSach.AllowUserToAddRows = false;
            this.dgvSach.AllowUserToDeleteRows = false;
            this.dgvSach.AllowUserToResizeRows = false;

            this.dgvSach.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvSach.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvSach.Location =
                new System.Drawing.Point(30, 270);

            this.dgvSach.MultiSelect = false;

            this.dgvSach.Name =
                "dgvSach";

            this.dgvSach.ReadOnly = true;

            this.dgvSach.RowHeadersWidth = 51;

            this.dgvSach.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvSach.Size =
                new System.Drawing.Size(750, 280);

            this.dgvSach.TabIndex = 0;

            // =====================================================
            // NÚT ĐÓNG
            // =====================================================

            this.btnDong.Location =
                new System.Drawing.Point(680, 570);

            this.btnDong.Name =
                "btnDong";

            this.btnDong.Size =
                new System.Drawing.Size(100, 35);

            this.btnDong.Text =
                "Đóng";

            this.btnDong.UseVisualStyleBackColor = true;

            // =====================================================
            // FORM
            // =====================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(820, 630);

            this.Controls.Add(this.lblTieuDe);

            this.Controls.Add(this.lblMaDauSach);
            this.Controls.Add(this.lblTenSach);
            this.Controls.Add(this.lblNamXuatBan);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.lblTheLoai);
            this.Controls.Add(this.lblNhaXuatBan);

            this.Controls.Add(this.txtMaDauSach);
            this.Controls.Add(this.txtTenSach);
            this.Controls.Add(this.txtNamXuatBan);
            this.Controls.Add(this.txtSoLuong);

            this.Controls.Add(this.cboTheLoai);
            this.Controls.Add(this.cboNhaXuatBan);

            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnDong);

            this.Controls.Add(this.dgvSach);

            this.Name =
                "FrmSach";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text =
                "Quản lý sách";

            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}