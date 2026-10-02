using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public partial class FrmDatHang : Form
    {

        private LabContext c;
        private string fingerprint;
        private bool working;
        private bool ready;
        public FrmDatHang() { InitializeComponent(); }
        public FrmDatHang(LabContext context) : this()
        {
            c = context;
            recipient.Text = c.Session.Customer.Name; address.Text = c.Session.Customer.Address;
            phone.Text = c.Session.Customer.Phone; holder.Text = c.Session.Customer.Name;
            region.Items.AddRange(FeeService.Regions); delivery.Items.AddRange(FeeService.Deliveries); card.Items.AddRange(FeeService.Cards);
            region.SelectedIndex = delivery.SelectedIndex = card.SelectedIndex = scenario.SelectedIndex = 0;
            expiry.Value = DateTime.Today.AddYears(2);
            ready = true;
            Shown += CheckoutShown;
            FormClosing += CheckoutClosing;
        }
        private void CheckoutShown(object sender, EventArgs e) { Ui.Attempt(Requote); }
        private void CheckoutClosing(object sender, FormClosingEventArgs e) { if (working) e.Cancel = true; }
        private void QuoteChanged(object sender, EventArgs e) { if (ready) Ui.Attempt(Requote); }
        private void RequoteClick(object sender, EventArgs e) { Ui.Attempt(Requote); }
        private async void PlaceClick(object sender, EventArgs e) { await Submit(false); }
        private async void LookupClick(object sender, EventArgs e) { await Submit(true); }
        private void CloseClick(object sender, EventArgs e) { if (!working) Close(); }
        private CheckoutInput Input(){return new CheckoutInput{Recipient=recipient.Text,Address=address.Text,Phone=phone.Text,Region=region.Text,Delivery=delivery.Text,CardType=card.Text,AcceptedQuote=fingerprint};}
        private void Requote()
        {
            var q=c.Checkout.GetQuote(Input());fingerprint=q.Fingerprint;
            quoteLabel.Text="THÔNG TIN ĐƠN\n\n"+string.Join("\n",q.Items.Select(x=>x.Name+" × "+x.Quantity))+"\n\nTiền hàng\n"+Ui.Money(q.Goods)+"\n\nPhí giao\n"+Ui.Money(q.Shipping)+"\n\nPhí thẻ\n"+Ui.Money(q.CardFee)+"\n\nTỔNG THANH TOÁN\n"+Ui.Money(q.Total)+"\n\nThẻ lab VISA: 4111111111111111\nCSV: 123\nKhông nhập thẻ thật.\n\nEmail giả lập được lưu vào Outbox.";
            place.Enabled=!c.Session.PendingRequest.HasValue;lookup.Enabled=c.Session.PendingRequest.HasValue;
        }
        private async Task Submit(bool reconcile)
        {
            if(working)return;working=true;place.Enabled=false;lookup.Enabled=false;
            try
            {
                CheckoutResult result;
                if(reconcile)result=await c.Checkout.LookupAsync(failEmail.Checked);
                else
                {
                    if(MessageBox.Show("Xác nhận đặt hàng với tổng tiền đã hiển thị?","e-SHOPPING",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;
                    result=await c.Checkout.PlaceAsync(Input(),new CardInput{Type=card.Text,Number=number.Text,SecurityCode=csv.Text,Holder=holder.Text,Expiry=expiry.Value},(PaymentScenario)scenario.SelectedIndex,failEmail.Checked);
                }
                new FrmKetQua(result).ShowDialog(this);
                if(result.Status=="ThanhCong"){DialogResult=DialogResult.OK;working=false;Close();}
            }
            catch(Exception ex){MessageBox.Show(ex.Message,"Đặt hàng",MessageBoxButtons.OK,MessageBoxIcon.Information);}
            finally{number.Clear();csv.Clear();working=false;place.Enabled=!c.Session.PendingRequest.HasValue;lookup.Enabled=c.Session.PendingRequest.HasValue;}
        }
    }
}
