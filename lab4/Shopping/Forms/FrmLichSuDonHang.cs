using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmLichSuDonHang : Form
    {

        private LabContext context;
        public FrmLichSuDonHang() { InitializeComponent(); }
        public FrmLichSuDonHang(LabContext context) : this() { this.context = context; LoadOrders(); }
        private void LoadOrders() { grid.DataSource = context.Orders.List(context.Session.Customer.Id); Ui.Headers(grid); }
        private void SelectionChanged(object sender, EventArgs e)
        {
            if (context == null) return;
            Ui.Attempt(() => { if (grid.CurrentRow != null && grid.CurrentRow.Cells["MaDonHang"].Value != null) { details.DataSource = context.Orders.Details(Convert.ToInt64(grid.CurrentRow.Cells["MaDonHang"].Value), context.Session.Customer.Id); Ui.Headers(details); } });
        }
        private void ReloadClick(object sender, EventArgs e) { Ui.Attempt(LoadOrders); }
        private void CloseClick(object sender, EventArgs e) { Close(); }

    }
}
