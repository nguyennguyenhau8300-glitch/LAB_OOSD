namespace Shopping.Forms
{
    partial class FrmDangKy
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label lblbirthday;
        private System.Windows.Forms.DateTimePicker birthday;
        private System.Windows.Forms.Label lblidentity;
        private System.Windows.Forms.TextBox identity;
        private System.Windows.Forms.Label lbladdress;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label lblphone;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label lbluser;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.Label lblpassword;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label lblconfirm;
        private System.Windows.Forms.TextBox confirm;
        private System.Windows.Forms.Label lblemail;
        private System.Windows.Forms.TextBox email;
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
            this.lblname = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.lblbirthday = new System.Windows.Forms.Label();
            this.birthday = new System.Windows.Forms.DateTimePicker();
            this.lblidentity = new System.Windows.Forms.Label();
            this.identity = new System.Windows.Forms.TextBox();
            this.lbladdress = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.lblphone = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.TextBox();
            this.lbluser = new System.Windows.Forms.Label();
            this.user = new System.Windows.Forms.TextBox();
            this.lblpassword = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.lblconfirm = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.TextBox();
            this.lblemail = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(720, 40);
            this.lblTitle.Text = "Đăng ký khách hàng";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // lblname
            this.lblname.Name = "lblname";
            this.lblname.Location = new System.Drawing.Point(25, 85);
            this.lblname.Size = new System.Drawing.Size(180, 28);
            this.lblname.Text = "Họ tên *";
            this.Controls.Add(this.lblname);
            // name
            this.name.Name = "name";
            this.name.Location = new System.Drawing.Point(210, 85);
            this.name.Size = new System.Drawing.Size(410, 28);
            this.Controls.Add(this.name);
            // lblbirthday
            this.lblbirthday.Name = "lblbirthday";
            this.lblbirthday.Location = new System.Drawing.Point(25, 130);
            this.lblbirthday.Size = new System.Drawing.Size(180, 28);
            this.lblbirthday.Text = "Ngày sinh *";
            this.Controls.Add(this.lblbirthday);
            // birthday
            this.birthday.Name = "birthday";
            this.birthday.Location = new System.Drawing.Point(210, 130);
            this.birthday.Size = new System.Drawing.Size(410, 28);
            this.birthday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Controls.Add(this.birthday);
            // lblidentity
            this.lblidentity.Name = "lblidentity";
            this.lblidentity.Location = new System.Drawing.Point(25, 175);
            this.lblidentity.Size = new System.Drawing.Size(180, 28);
            this.lblidentity.Text = "CMND / Passport *";
            this.Controls.Add(this.lblidentity);
            // identity
            this.identity.Name = "identity";
            this.identity.Location = new System.Drawing.Point(210, 175);
            this.identity.Size = new System.Drawing.Size(410, 28);
            this.Controls.Add(this.identity);
            // lbladdress
            this.lbladdress.Name = "lbladdress";
            this.lbladdress.Location = new System.Drawing.Point(25, 220);
            this.lbladdress.Size = new System.Drawing.Size(180, 28);
            this.lbladdress.Text = "Địa chỉ *";
            this.Controls.Add(this.lbladdress);
            // address
            this.address.Name = "address";
            this.address.Location = new System.Drawing.Point(210, 220);
            this.address.Size = new System.Drawing.Size(410, 28);
            this.Controls.Add(this.address);
            // lblphone
            this.lblphone.Name = "lblphone";
            this.lblphone.Location = new System.Drawing.Point(25, 265);
            this.lblphone.Size = new System.Drawing.Size(180, 28);
            this.lblphone.Text = "Điện thoại *";
            this.Controls.Add(this.lblphone);
            // phone
            this.phone.Name = "phone";
            this.phone.Location = new System.Drawing.Point(210, 265);
            this.phone.Size = new System.Drawing.Size(410, 28);
            this.Controls.Add(this.phone);
            // lbluser
            this.lbluser.Name = "lbluser";
            this.lbluser.Location = new System.Drawing.Point(25, 310);
            this.lbluser.Size = new System.Drawing.Size(180, 28);
            this.lbluser.Text = "Tên đăng nhập *";
            this.Controls.Add(this.lbluser);
            // user
            this.user.Name = "user";
            this.user.Location = new System.Drawing.Point(210, 310);
            this.user.Size = new System.Drawing.Size(410, 28);
            this.Controls.Add(this.user);
            // lblpassword
            this.lblpassword.Name = "lblpassword";
            this.lblpassword.Location = new System.Drawing.Point(25, 355);
            this.lblpassword.Size = new System.Drawing.Size(180, 28);
            this.lblpassword.Text = "Mật khẩu *";
            this.Controls.Add(this.lblpassword);
            // password
            this.password.Name = "password";
            this.password.Location = new System.Drawing.Point(210, 355);
            this.password.Size = new System.Drawing.Size(410, 28);
            this.password.UseSystemPasswordChar = true;
            this.Controls.Add(this.password);
            // lblconfirm
            this.lblconfirm.Name = "lblconfirm";
            this.lblconfirm.Location = new System.Drawing.Point(25, 400);
            this.lblconfirm.Size = new System.Drawing.Size(180, 28);
            this.lblconfirm.Text = "Nhập lại mật khẩu *";
            this.Controls.Add(this.lblconfirm);
            // confirm
            this.confirm.Name = "confirm";
            this.confirm.Location = new System.Drawing.Point(210, 400);
            this.confirm.Size = new System.Drawing.Size(410, 28);
            this.confirm.UseSystemPasswordChar = true;
            this.Controls.Add(this.confirm);
            // lblemail
            this.lblemail.Name = "lblemail";
            this.lblemail.Location = new System.Drawing.Point(25, 445);
            this.lblemail.Size = new System.Drawing.Size(180, 28);
            this.lblemail.Text = "Email (tùy chọn)";
            this.Controls.Add(this.lblemail);
            // email
            this.email.Name = "email";
            this.email.Location = new System.Drawing.Point(210, 445);
            this.email.Size = new System.Drawing.Size(410, 28);
            this.Controls.Add(this.email);
            // btnRegister
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Location = new System.Drawing.Point(20, 585);
            this.btnRegister.Size = new System.Drawing.Size(135, 38);
            this.btnRegister.Text = "Đăng ký";
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRegister.Click += this.RegisterClick;
            this.Controls.Add(this.btnRegister);
            // btnCancel
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Location = new System.Drawing.Point(165, 585);
            this.btnCancel.Size = new System.Drawing.Size(135, 38);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.Click += this.CloseClick;
            this.Controls.Add(this.btnCancel);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(760, 650);
            this.MinimumSize = new System.Drawing.Size(776, 689);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmDangKy";
            this.Text = "e-SHOPPING - Đăng ký khách hàng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
