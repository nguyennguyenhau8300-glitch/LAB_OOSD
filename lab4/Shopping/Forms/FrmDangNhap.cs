using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmDangNhap : Form
    {

        private LabContext context;
        public FrmDangNhap() { InitializeComponent(); }
        public FrmDangNhap(LabContext context) : this() { this.context = context; AcceptButton = btnLogin; }
        private void LoginClick(object sender, EventArgs e) { Ui.Attempt(() => { context.Session.Customer = context.Accounts.Login(user.Text, password.Text); DialogResult = DialogResult.OK; Close(); }); }
        private void RegisterClick(object sender, EventArgs e) { using var f = new FrmDangKy(context); if (f.ShowDialog(this) == DialogResult.OK) { user.Text = f.Username; password.Clear(); } }
        private void CloseClick(object sender, EventArgs e) { Close(); }

    }
}
