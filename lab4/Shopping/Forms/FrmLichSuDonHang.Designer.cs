namespace Shopping.Forms
{
    partial class FrmLichSuDonHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridView details;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.details = new System.Windows.Forms.DataGridView();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(1110, 40);
            this.lblTitle.Text = "Đơn hàng của tôi";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // grid
            this.grid.Name = "grid";
            this.grid.Location = new System.Drawing.Point(20, 80);
            this.grid.Size = new System.Drawing.Size(1110, 330);
            this.grid.ReadOnly = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.RowHeadersVisible = false;
            this.grid.MultiSelect = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.SelectionChanged += this.SelectionChanged;
            this.Controls.Add(this.grid);
            // details
            this.details.Name = "details";
            this.details.Location = new System.Drawing.Point(20, 430);
            this.details.Size = new System.Drawing.Size(1110, 245);
            this.details.ReadOnly = true;
            this.details.AllowUserToAddRows = false;
            this.details.AllowUserToDeleteRows = false;
            this.details.RowHeadersVisible = false;
            this.details.MultiSelect = false;
            this.details.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.details.BackgroundColor = System.Drawing.Color.White;
            this.Controls.Add(this.details);
            // btnReload
            this.btnReload.Name = "btnReload";
            this.btnReload.Location = new System.Drawing.Point(20, 695);
            this.btnReload.Size = new System.Drawing.Size(135, 38);
            this.btnReload.Text = "Tải lại";
            this.btnReload.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnReload.Click += this.ReloadClick;
            this.Controls.Add(this.btnReload);
            // btnClose
            this.btnClose.Name = "btnClose";
            this.btnClose.Location = new System.Drawing.Point(165, 695);
            this.btnClose.Size = new System.Drawing.Size(135, 38);
            this.btnClose.Text = "Đóng";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnClose.Click += this.CloseClick;
            this.Controls.Add(this.btnClose);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1150, 760);
            this.MinimumSize = new System.Drawing.Size(1166, 799);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmLichSuDonHang";
            this.Text = "e-SHOPPING - Đơn hàng của tôi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
