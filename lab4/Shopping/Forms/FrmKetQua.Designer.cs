namespace Shopping.Forms
{
    partial class FrmKetQua
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(710, 40);
            this.lblTitle.Text = "Kết quả đặt hàng";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // message
            this.message.Name = "message";
            this.message.Location = new System.Drawing.Point(25, 90);
            this.message.Size = new System.Drawing.Size(700, 240);
            this.Controls.Add(this.message);
            // btnClose
            this.btnClose.Name = "btnClose";
            this.btnClose.Location = new System.Drawing.Point(20, 365);
            this.btnClose.Size = new System.Drawing.Size(135, 38);
            this.btnClose.Text = "Đóng";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnClose.Click += this.CloseClick;
            this.Controls.Add(this.btnClose);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(750, 430);
            this.MinimumSize = new System.Drawing.Size(766, 469);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmKetQua";
            this.Text = "e-SHOPPING - Kết quả đặt hàng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
