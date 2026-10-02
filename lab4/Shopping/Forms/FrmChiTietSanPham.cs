using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmChiTietSanPham : Form
    {

        private LabContext context;
        private Product product;
        public FrmChiTietSanPham() { InitializeComponent(); }
        public FrmChiTietSanPham(LabContext context, Product product) : this()
        {
            this.context = context; this.product = product;
            detail.Text = product.Name + "\n\nMã: " + product.Code + "\nNhà sản xuất: " + product.Maker + "\n\n" + product.Description + "\n\nThông số: " + product.Specs + "\n\nGiá: " + Ui.Money(product.Price) + "\n" + product.Status;
        }
        private void AddClick(object sender, EventArgs e) { Ui.Attempt(() => { context.Cart.Add(product.Code, (int)quantity.Value); MessageBox.Show("Đã thêm vào giỏ."); }); }
        private void CloseClick(object sender, EventArgs e) { Close(); }

    }
}
