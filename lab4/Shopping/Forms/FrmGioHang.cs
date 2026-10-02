using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmGioHang : Form
    {

        private LabContext context;
        public FrmGioHang() { InitializeComponent(); }
        public FrmGioHang(LabContext context) : this() { this.context = context; LoadCart(); }
        private CartItem Selected() { return grid.CurrentRow?.DataBoundItem as CartItem; }
        private void LoadCart() { grid.DataSource = null; grid.DataSource = context.Session.Cart.Select(x => x.Copy()).ToList(); Ui.Headers(grid); total.Text = "Tạm tính: " + Ui.Money(context.Session.Cart.Sum(x => x.Total)) + "\nGiá được kiểm tra lại khi đặt hàng."; }
        private void SelectionChanged(object sender, EventArgs e) { var item = Selected(); if (item != null) quantity.Value = Math.Min(quantity.Maximum, item.Quantity); }
        private void UpdateClick(object sender, EventArgs e) { Ui.Attempt(() => { if (Selected() != null) context.Cart.Update(Selected().Code, (int)quantity.Value); LoadCart(); }); }
        private void RemoveClick(object sender, EventArgs e) { Ui.Attempt(() => { if (Selected() != null) context.Cart.Remove(Selected().Code); LoadCart(); }); }
        private void CheckoutClick(object sender, EventArgs e)
        {
            Ui.Attempt(() => {
                if (context.Session.Cart.Count == 0) throw new ArgumentException("Giỏ trống.");
                if (context.Session.Customer == null) { using var login = new FrmDangNhap(context); if (login.ShowDialog(this) != DialogResult.OK) return; }
                using var checkout = new FrmDatHang(context); checkout.ShowDialog(this); LoadCart();
            });
        }
        private void CloseClick(object sender, EventArgs e) { Close(); }

    }
}
