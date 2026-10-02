namespace Shopping.Forms
{
    partial class FrmGioHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.NumericUpDown quantity;
        private System.Windows.Forms.Label total;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnCheckout;
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
            this.quantity = new System.Windows.Forms.NumericUpDown();
            this.total = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(960, 40);
            this.lblTitle.Text = "Giỏ hàng";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // grid
            this.grid.Name = "grid";
            this.grid.Location = new System.Drawing.Point(20, 80);
            this.grid.Size = new System.Drawing.Size(960, 410);
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
            // quantity
            this.quantity.Name = "quantity";
            this.quantity.Location = new System.Drawing.Point(20, 510);
            this.quantity.Size = new System.Drawing.Size(120, 30);
            this.quantity.Minimum = 1;
            this.quantity.Maximum = 10000;
            this.quantity.Value = 1;
            this.Controls.Add(this.quantity);
            // total
            this.total.Name = "total";
            this.total.Location = new System.Drawing.Point(160, 510);
            this.total.Size = new System.Drawing.Size(800, 70);
            this.Controls.Add(this.total);
            // btnUpdate
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Location = new System.Drawing.Point(20, 615);
            this.btnUpdate.Size = new System.Drawing.Size(135, 38);
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnUpdate.Click += this.UpdateClick;
            this.Controls.Add(this.btnUpdate);
            // btnRemove
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Location = new System.Drawing.Point(165, 615);
            this.btnRemove.Size = new System.Drawing.Size(135, 38);
            this.btnRemove.Text = "Xóa sản phẩm";
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRemove.Click += this.RemoveClick;
            this.Controls.Add(this.btnRemove);
            // btnCheckout
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Location = new System.Drawing.Point(310, 615);
            this.btnCheckout.Size = new System.Drawing.Size(135, 38);
            this.btnCheckout.Text = "Tính tiền";
            this.btnCheckout.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnCheckout.Click += this.CheckoutClick;
            this.Controls.Add(this.btnCheckout);
            // btnClose
            this.btnClose.Name = "btnClose";
            this.btnClose.Location = new System.Drawing.Point(455, 615);
            this.btnClose.Size = new System.Drawing.Size(135, 38);
            this.btnClose.Text = "Tiếp tục mua";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnClose.Click += this.CloseClick;
            this.Controls.Add(this.btnClose);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.MinimumSize = new System.Drawing.Size(1016, 719);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmGioHang";
            this.Text = "e-SHOPPING - Giỏ hàng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
