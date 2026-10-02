using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmDangKy : Form
    {

        private LabContext context;
        public string Username { get; private set; }
        public FrmDangKy() { InitializeComponent(); }
        public FrmDangKy(LabContext context) : this() { this.context = context; birthday.MaxDate = DateTime.Today; birthday.Value = new DateTime(2000, 1, 1); }
        private void RegisterClick(object sender, EventArgs e)
        {
            Ui.Attempt(() => {
                var customer = new Customer { Name = name.Text.Trim(), Birthday = birthday.Value.Date, Identity = identity.Text.Trim(), Address = address.Text.Trim(), Phone = phone.Text.Trim(), Username = user.Text.Trim(), Email = email.Text.Trim() };
                context.Accounts.Register(customer, password.Text, confirm.Text);
                Username = customer.Username;
                MessageBox.Show("Tạo tài khoản thành công. Hãy đăng nhập.");
                DialogResult = DialogResult.OK; Close();
            });
        }
        private void CloseClick(object sender, EventArgs e) { Close(); }

    }
}
