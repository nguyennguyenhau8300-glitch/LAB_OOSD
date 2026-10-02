namespace Shopping.Forms
{
    partial class FrmChiTietSanPham
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label detail;
        private System.Windows.Forms.NumericUpDown quantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.detail = new System.Windows.Forms.Label();
            this.quantity = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(810, 40);
            this.lblTitle.Text = "Chi tiết sản phẩm";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // detail
            this.detail.Name = "detail";
            this.detail.Location = new System.Drawing.Point(25, 85);
            this.detail.Size = new System.Drawing.Size(790, 365);
            this.Controls.Add(this.detail);
            // quantity
            this.quantity.Name = "quantity";
            this.quantity.Location = new System.Drawing.Point(25, 455);
            this.quantity.Size = new System.Drawing.Size(120, 30);
            this.quantity.Minimum = 1;
            this.quantity.Maximum = 10000;
            this.quantity.Value = 1;
            this.Controls.Add(this.quantity);
            // btnAdd
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Location = new System.Drawing.Point(20, 515);
            this.btnAdd.Size = new System.Drawing.Size(135, 38);
            this.btnAdd.Text = "Thêm vào giỏ";
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.Click += this.AddClick;
            this.Controls.Add(this.btnAdd);
            // btnClose
            this.btnClose.Name = "btnClose";
            this.btnClose.Location = new System.Drawing.Point(165, 515);
            this.btnClose.Size = new System.Drawing.Size(135, 38);
            this.btnClose.Text = "Đóng";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnClose.Click += this.CloseClick;
            this.Controls.Add(this.btnClose);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(850, 580);
            this.MinimumSize = new System.Drawing.Size(866, 619);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmChiTietSanPham";
            this.Text = "e-SHOPPING - Chi tiết sản phẩm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
