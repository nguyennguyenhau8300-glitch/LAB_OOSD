namespace QuanLyThuVien.Forms
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvTheLoai;
        private System.Windows.Forms.DataGridView dgvNhaXuatBan;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Button btnDong;

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
            this.dgvTheLoai = new System.Windows.Forms.DataGridView();
            this.dgvNhaXuatBan = new System.Windows.Forms.DataGridView();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTheLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaXuatBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();

            this.SuspendLayout();

            // 
            // dgvTheLoai
            // 
            this.dgvTheLoai.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvTheLoai.Location =
                new System.Drawing.Point(20, 20);

            this.dgvTheLoai.Name =
                "dgvTheLoai";

            this.dgvTheLoai.RowHeadersWidth = 51;

            this.dgvTheLoai.Size =
                new System.Drawing.Size(500, 180);

            this.dgvTheLoai.TabIndex = 0;

            // 
            // dgvNhaXuatBan
            // 
            this.dgvNhaXuatBan.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvNhaXuatBan.Location =
                new System.Drawing.Point(20, 220);

            this.dgvNhaXuatBan.Name =
                "dgvNhaXuatBan";

            this.dgvNhaXuatBan.RowHeadersWidth = 51;

            this.dgvNhaXuatBan.Size =
                new System.Drawing.Size(700, 180);

            this.dgvNhaXuatBan.TabIndex = 1;

            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvNhanVien.Location =
                new System.Drawing.Point(20, 420);

            this.dgvNhanVien.Name =
                "dgvNhanVien";

            this.dgvNhanVien.RowHeadersWidth = 51;

            this.dgvNhanVien.Size =
                new System.Drawing.Size(850, 220);

            this.dgvNhanVien.TabIndex = 2;

            // 
            // btnDong
            // 
            this.btnDong.Location =
                new System.Drawing.Point(780, 20);

            this.btnDong.Name =
                "btnDong";

            this.btnDong.Size =
                new System.Drawing.Size(90, 35);

            this.btnDong.TabIndex = 3;

            this.btnDong.Text =
                "Đóng";

            this.btnDong.UseVisualStyleBackColor = true;

            this.btnDong.Click +=
                new System.EventHandler(this.btnDong_Click);

            // 
            // FrmDanhMuc
            // 
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(8F, 16F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(900, 680);

            this.Controls.Add(this.dgvTheLoai);
            this.Controls.Add(this.dgvNhaXuatBan);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.btnDong);

            this.Name =
                "FrmDanhMuc";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text =
                "Quản lý danh mục";

            this.Load +=
                new System.EventHandler(this.FrmDanhMuc_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTheLoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaXuatBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();

            this.ResumeLayout(false);
        }
    }
}