namespace QuanLyThuVien.Forms
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblSoDauSach;
        private System.Windows.Forms.Label lblTongSach;
        private System.Windows.Forms.Label lblSoDocGia;
        private System.Windows.Forms.Label lblSoNhanVien;
        private System.Windows.Forms.Label lblSoPhieuMuon;

        private System.Windows.Forms.DataGridView dgvThongKe;

        private System.Windows.Forms.Button btnThongKe;
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
            this.lblSoDauSach = new System.Windows.Forms.Label();
            this.lblTongSach = new System.Windows.Forms.Label();
            this.lblSoDocGia = new System.Windows.Forms.Label();
            this.lblSoNhanVien = new System.Windows.Forms.Label();
            this.lblSoPhieuMuon = new System.Windows.Forms.Label();

            this.dgvThongKe = new System.Windows.Forms.DataGridView();

            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).BeginInit();
            this.SuspendLayout();

            // 
            // lblSoDauSach
            // 
            this.lblSoDauSach.AutoSize = true;
            this.lblSoDauSach.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.lblSoDauSach.Location = new System.Drawing.Point(30, 30);
            this.lblSoDauSach.Name = "lblSoDauSach";
            this.lblSoDauSach.Size = new System.Drawing.Size(120, 17);
            this.lblSoDauSach.TabIndex = 0;
            this.lblSoDauSach.Text = "Số đầu sách: 0";

            // 
            // lblTongSach
            // 
            this.lblTongSach.AutoSize = true;
            this.lblTongSach.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.lblTongSach.Location = new System.Drawing.Point(220, 30);
            this.lblTongSach.Name = "lblTongSach";
            this.lblTongSach.Size = new System.Drawing.Size(110, 17);
            this.lblTongSach.TabIndex = 1;
            this.lblTongSach.Text = "Tổng số sách: 0";

            // 
            // lblSoDocGia
            // 
            this.lblSoDocGia.AutoSize = true;
            this.lblSoDocGia.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.lblSoDocGia.Location = new System.Drawing.Point(410, 30);
            this.lblSoDocGia.Name = "lblSoDocGia";
            this.lblSoDocGia.Size = new System.Drawing.Size(110, 17);
            this.lblSoDocGia.TabIndex = 2;
            this.lblSoDocGia.Text = "Số độc giả: 0";

            // 
            // lblSoNhanVien
            // 
            this.lblSoNhanVien.AutoSize = true;
            this.lblSoNhanVien.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.lblSoNhanVien.Location = new System.Drawing.Point(590, 30);
            this.lblSoNhanVien.Name = "lblSoNhanVien";
            this.lblSoNhanVien.Size = new System.Drawing.Size(120, 17);
            this.lblSoNhanVien.TabIndex = 3;
            this.lblSoNhanVien.Text = "Số nhân viên: 0";

            // 
            // lblSoPhieuMuon
            // 
            this.lblSoPhieuMuon.AutoSize = true;
            this.lblSoPhieuMuon.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.lblSoPhieuMuon.Location = new System.Drawing.Point(30, 65);
            this.lblSoPhieuMuon.Name = "lblSoPhieuMuon";
            this.lblSoPhieuMuon.Size = new System.Drawing.Size(125, 17);
            this.lblSoPhieuMuon.TabIndex = 4;
            this.lblSoPhieuMuon.Text = "Số phiếu mượn: 0";

            // 
            // dgvThongKe
            // 
            this.dgvThongKe.AllowUserToAddRows = false;
            this.dgvThongKe.AllowUserToDeleteRows = false;
            this.dgvThongKe.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvThongKe.Location = new System.Drawing.Point(30, 105);
            this.dgvThongKe.Name = "dgvThongKe";
            this.dgvThongKe.ReadOnly = true;
            this.dgvThongKe.RowHeadersWidth = 51;
            this.dgvThongKe.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThongKe.Size = new System.Drawing.Size(900, 350);
            this.dgvThongKe.TabIndex = 5;

            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(30, 475);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(120, 35);
            this.btnThongKe.TabIndex = 6;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(
                this.btnThongKe_Click
            );

            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(810, 475);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(
                this.btnDong_Click
            );

            // 
            // FrmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(970, 550);

            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.dgvThongKe);

            this.Controls.Add(this.lblSoPhieuMuon);
            this.Controls.Add(this.lblSoNhanVien);
            this.Controls.Add(this.lblSoDocGia);
            this.Controls.Add(this.lblTongSach);
            this.Controls.Add(this.lblSoDauSach);

            this.Name = "FrmThongKe";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê thư viện";

            this.Load += new System.EventHandler(
                this.FrmThongKe_Load
            );

            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}