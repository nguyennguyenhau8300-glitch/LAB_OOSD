using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmKetQua : Form
    {

        public FrmKetQua() { InitializeComponent(); }
        public FrmKetQua(CheckoutResult result) : this() { message.Text = result.Message; }
        private void CloseClick(object sender, EventArgs e) { Close(); }

    }
}
