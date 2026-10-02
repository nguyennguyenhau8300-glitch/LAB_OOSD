namespace Shopping.Forms
{
    partial class FrmDangNhap
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lbluser;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.Label lblpassword;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label lblDemo;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lbluser = new System.Windows.Forms.Label();
            this.user = new System.Windows.Forms.TextBox();
            this.lblpassword = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.lblDemo = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(660, 40);
            this.lblTitle.Text = "Đăng nhập";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // lbluser
            this.lbluser.Name = "lbluser";
            this.lbluser.Location = new System.Drawing.Point(25, 85);
            this.lbluser.Size = new System.Drawing.Size(180, 28);
            this.lbluser.Text = "Tên đăng nhập";
            this.Controls.Add(this.lbluser);
            // user
            this.user.Name = "user";
            this.user.Location = new System.Drawing.Point(210, 85);
            this.user.Size = new System.Drawing.Size(410, 28);
            this.Controls.Add(this.user);
            // lblpassword
            this.lblpassword.Name = "lblpassword";
            this.lblpassword.Location = new System.Drawing.Point(25, 130);
            this.lblpassword.Size = new System.Drawing.Size(180, 28);
            this.lblpassword.Text = "Mật khẩu";
            this.Controls.Add(this.lblpassword);
            // password
            this.password.Name = "password";
            this.password.Location = new System.Drawing.Point(210, 130);
            this.password.Size = new System.Drawing.Size(410, 28);
            this.password.UseSystemPasswordChar = true;
            this.Controls.Add(this.password);
            // lblDemo
            this.lblDemo.Name = "lblDemo";
            this.lblDemo.Location = new System.Drawing.Point(25, 205);
            this.lblDemo.Size = new System.Drawing.Size(590, 35);
            this.lblDemo.Text = "Tài khoản lab: demo / Demo@123";
            this.Controls.Add(this.lblDemo);
            // btnLogin
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Location = new System.Drawing.Point(20, 365);
            this.btnLogin.Size = new System.Drawing.Size(135, 38);
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnLogin.Click += this.LoginClick;
            this.Controls.Add(this.btnLogin);
            // btnRegister
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Location = new System.Drawing.Point(165, 365);
            this.btnRegister.Size = new System.Drawing.Size(135, 38);
            this.btnRegister.Text = "Đăng ký mới";
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRegister.Click += this.RegisterClick;
            this.Controls.Add(this.btnRegister);
            // btnCancel
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Location = new System.Drawing.Point(310, 365);
            this.btnCancel.Size = new System.Drawing.Size(135, 38);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.Click += this.CloseClick;
            this.Controls.Add(this.btnCancel);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(700, 430);
            this.MinimumSize = new System.Drawing.Size(716, 469);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmDangNhap";
            this.Text = "e-SHOPPING - Đăng nhập";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
