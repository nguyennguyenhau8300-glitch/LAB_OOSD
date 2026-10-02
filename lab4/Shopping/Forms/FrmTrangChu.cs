using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmTrangChu : Form
    {

        private LabContext context;
        public FrmTrangChu() { InitializeComponent(); }
        public FrmTrangChu(LabContext context) : this()
        {
            this.context = context;
            group.SelectedIndex = 0;
            RefreshProducts();
            RefreshStatus();
            FormClosing += MainClosing;
        }
        private void MainClosing(object sender, FormClosingEventArgs e)
        {
            if (context.Session.PendingRequest.HasValue && MessageBox.Show("Còn giao dịch chờ đối soát. Đóng ứng dụng sẽ mất trạng thái tạm. Vẫn đóng?", "e-SHOPPING", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true;
        }
        private Product Selected() { return products.CurrentRow?.DataBoundItem as Product; }
        private void RefreshProducts()
        {
            if (context == null) return;
            products.DataSource = context.Products.GetAll().Where(p => (group.Text == "Tất cả" || p.Group == group.Text) && p.Name.IndexOf(search.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            foreach (string name in new[] { "Description", "Specs", "Available" })
                if (products.Columns.Contains(name)) products.Columns[name].Visible = false;
            Ui.Headers(products);
        }
        private void RefreshStatus() { status.Text = (context.Session.Customer == null ? "Chưa đăng nhập" : "Xin chào " + context.Session.Customer.Name) + "    |    Giỏ: " + context.Session.Cart.Sum(x => x.Quantity) + " sản phẩm"; }
        private bool CanChangeAccount()
        {
            if (!context.Session.PendingRequest.HasValue) return true;
            MessageBox.Show("Đối soát giao dịch đang chờ trước khi đổi tài khoản."); return false;
        }
        private void FilterChanged(object sender, EventArgs e) { RefreshProducts(); }
        private void DetailClick(object sender, EventArgs e) { Ui.Attempt(() => { var p = Selected(); if (p != null) { using var f = new FrmChiTietSanPham(context, p); f.ShowDialog(this); } RefreshStatus(); }); }
        private void AddClick(object sender, EventArgs e) { Ui.Attempt(() => { var p = Selected(); if (p != null) context.Cart.Add(p.Code, (int)quantity.Value); RefreshStatus(); }); }
        private void CartClick(object sender, EventArgs e) { using var f = new FrmGioHang(context); f.ShowDialog(this); RefreshStatus(); }
        private void LoginClick(object sender, EventArgs e) { if (!CanChangeAccount()) return; using var f = new FrmDangNhap(context); f.ShowDialog(this); RefreshStatus(); }
        private void RegisterClick(object sender, EventArgs e) { using var f = new FrmDangKy(context); f.ShowDialog(this); }
        private void OrdersClick(object sender, EventArgs e) { Ui.Attempt(() => { if (context.Session.Customer == null) throw new ArgumentException("Hãy đăng nhập trước."); using var f = new FrmLichSuDonHang(context); f.ShowDialog(this); }); }
        private void LogoutClick(object sender, EventArgs e) { if (CanChangeAccount()) { context.Session.Customer = null; RefreshStatus(); } }

    }
}
