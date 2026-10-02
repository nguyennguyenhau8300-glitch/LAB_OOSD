namespace Shopping.Forms
{
    partial class FrmTrangChu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ComboBox group;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.NumericUpDown quantity;
        private System.Windows.Forms.DataGridView products;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.group = new System.Windows.Forms.ComboBox();
            this.search = new System.Windows.Forms.TextBox();
            this.quantity = new System.Windows.Forms.NumericUpDown();
            this.products = new System.Windows.Forms.DataGridView();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCart = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(1160, 40);
            this.lblTitle.Text = "Cửa hàng ABC — Mua sắm online";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // status
            this.status.Name = "status";
            this.status.Location = new System.Drawing.Point(20, 65);
            this.status.Size = new System.Drawing.Size(1150, 35);
            this.Controls.Add(this.status);
            // group
            this.group.Name = "group";
            this.group.Location = new System.Drawing.Point(20, 110);
            this.group.Size = new System.Drawing.Size(220, 30);
            this.group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group.Items.AddRange(new object[] { "Tất cả", "Máy ảnh", "Đồ chơi", "Gia dụng", "Máy tính" });
            this.group.SelectedIndexChanged += this.FilterChanged;
            this.Controls.Add(this.group);
            // search
            this.search.Name = "search";
            this.search.Location = new System.Drawing.Point(260, 110);
            this.search.Size = new System.Drawing.Size(300, 30);
            this.search.PlaceholderText = "Tìm tên sản phẩm";
            this.search.TextChanged += this.FilterChanged;
            this.Controls.Add(this.search);
            // quantity
            this.quantity.Name = "quantity";
            this.quantity.Location = new System.Drawing.Point(590, 110);
            this.quantity.Size = new System.Drawing.Size(90, 30);
            this.quantity.Minimum = 1;
            this.quantity.Maximum = 10000;
            this.quantity.Value = 1;
            this.Controls.Add(this.quantity);
            // products
            this.products.Name = "products";
            this.products.Location = new System.Drawing.Point(20, 160);
            this.products.Size = new System.Drawing.Size(1160, 515);
            this.products.ReadOnly = true;
            this.products.AllowUserToAddRows = false;
            this.products.AllowUserToDeleteRows = false;
            this.products.RowHeadersVisible = false;
            this.products.MultiSelect = false;
            this.products.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.products.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.products.BackgroundColor = System.Drawing.Color.White;
            this.products.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(this.products);
            // btnDetail
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Location = new System.Drawing.Point(20, 695);
            this.btnDetail.Size = new System.Drawing.Size(135, 38);
            this.btnDetail.Text = "Xem chi tiết";
            this.btnDetail.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnDetail.Click += this.DetailClick;
            this.Controls.Add(this.btnDetail);
            // btnAdd
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Location = new System.Drawing.Point(165, 695);
            this.btnAdd.Size = new System.Drawing.Size(135, 38);
            this.btnAdd.Text = "Thêm vào giỏ";
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.Click += this.AddClick;
            this.Controls.Add(this.btnAdd);
            // btnCart
            this.btnCart.Name = "btnCart";
            this.btnCart.Location = new System.Drawing.Point(310, 695);
            this.btnCart.Size = new System.Drawing.Size(135, 38);
            this.btnCart.Text = "Giỏ hàng";
            this.btnCart.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnCart.Click += this.CartClick;
            this.Controls.Add(this.btnCart);
            // btnLogin
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Location = new System.Drawing.Point(455, 695);
            this.btnLogin.Size = new System.Drawing.Size(135, 38);
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnLogin.Click += this.LoginClick;
            this.Controls.Add(this.btnLogin);
            // btnRegister
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Location = new System.Drawing.Point(600, 695);
            this.btnRegister.Size = new System.Drawing.Size(135, 38);
            this.btnRegister.Text = "Đăng ký";
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRegister.Click += this.RegisterClick;
            this.Controls.Add(this.btnRegister);
            // btnOrders
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Location = new System.Drawing.Point(745, 695);
            this.btnOrders.Size = new System.Drawing.Size(135, 38);
            this.btnOrders.Text = "Đơn đã mua";
            this.btnOrders.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnOrders.Click += this.OrdersClick;
            this.Controls.Add(this.btnOrders);
            // btnLogout
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Location = new System.Drawing.Point(890, 695);
            this.btnLogout.Size = new System.Drawing.Size(135, 38);
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnLogout.Click += this.LogoutClick;
            this.Controls.Add(this.btnLogout);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.MinimumSize = new System.Drawing.Size(1216, 799);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmTrangChu";
            this.Text = "e-SHOPPING - Cửa hàng ABC — Mua sắm online";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
