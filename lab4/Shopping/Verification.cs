using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Shopping.Adapters;
using Shopping.Data;
using Shopping.Forms;
using Shopping.Models;
using Shopping.Services;

namespace Shopping
{
    internal static class Verification
    {
        public static void Run(LabContext c,string reportPath)
        {
            var log=new StringBuilder();int count=0;
            Action<string,bool> check=(name,ok)=>{if(!ok)throw new Exception(name);log.AppendLine("PASS "+(++count)+" "+name);};
            try
            {
                check("Exactly three tables",Db.Query("SELECT name FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo')").Rows.Count==3);
                c.Session.Customer=c.Accounts.Login("demo","Demo@123");check("Demo login",c.Session.Customer.Id>0);
                bool wrong=false;try{c.Accounts.Login("demo","wrong");}catch(ArgumentException){wrong=true;}check("Wrong password rejected",wrong);
                var fee=new FeeService();check("Express below threshold",fee.Shipping(999999,"Nội thành","NHANH")==40000);check("Express at threshold",fee.Shipping(1000000,"Nội thành","NHANH")==0);
                check("Same day below threshold",fee.Shipping(4999999,"Nội thành","TRONGNGAY")==70000);check("Same day at threshold",fee.Shipping(5000000,"Nội thành","TRONGNGAY")==0);
                c.Cart.Add("TOY01",2);c.Cart.Add("TOY01",2);check("Cart merges product",c.Session.Cart.Count==1&&c.Session.Cart[0].Quantity==4);
                bool unavailable=false;try{c.Cart.Add("HOME02",1);}catch(ArgumentException){unavailable=true;}check("Unavailable product blocked",unavailable);
                var input=new CheckoutInput{Recipient="Người nhận lab",Address="Địa chỉ lab",Phone="0901234567",Region="Nội thành",Delivery="NHANH",CardType="VISA"};
                var quote=c.Checkout.GetQuote(input);input.AcceptedQuote=quote.Fingerprint;
                var card=new CardInput{Type="VISA",Number="4111111111111111",SecurityCode="123",Holder="LAB",Expiry=DateTime.Today.AddYears(2)};
                var refused=c.Checkout.PlaceAsync(input,card,PaymentScenario.Declined,false).GetAwaiter().GetResult();check("Decline retains cart",refused.Status=="TuChoi"&&c.Session.Cart.Count==1);
                var timeout=c.Checkout.PlaceAsync(input,card,PaymentScenario.Timeout,false).GetAwaiter().GetResult();check("Timeout awaits reconciliation",timeout.Status=="ChuaRo"&&c.Session.PendingRequest.HasValue);
                var done=c.Checkout.LookupAsync(false).GetAwaiter().GetResult();check("Reconcile saves successful order",done.OrderId>0&&done.Status=="ThanhCong"&&c.Session.Cart.Count==0);
                check("Order detail is saved",c.Orders.Details(done.OrderId,c.Session.Customer.Id).Rows.Count==1);
                string email=File.ReadAllText(Path.Combine(c.Outbox,"DonHang-"+done.OrderId+".txt"));check("Email contains no card details",!email.Contains(card.Number)&&!email.Contains("CSV"));
                c.Cart.Add("PC02",1);input.Delivery="TRONGNGAY";input.AcceptedQuote=c.Checkout.GetQuote(input).Fingerprint;
                var emailFail=c.Checkout.PlaceAsync(input,card,PaymentScenario.Success,true).GetAwaiter().GetResult();check("Email failure preserves successful order",emailFail.OrderId>0&&c.Orders.List(c.Session.Customer.Id).Select("MaDonHang="+emailFail.OrderId)[0]["TrangThaiEmail"].ToString()=="GuiLoi");
                bool duplicate=false;try{c.Accounts.Register(new Customer{Name="Duplicate",Birthday=new DateTime(2000,1,1),Identity="LAB",Address="Lab",Phone="0901234567",Username="demo"},"Demo@123","Demo@123");}catch(ArgumentException){duplicate=true;}check("Duplicate username rejected",duplicate);
                bool bad=false;try{CardValidator.Validate(new CardInput{Type="AMEX",Number="123456789012345",SecurityCode="123",Holder="LAB",Expiry=DateTime.Today.AddYears(1)});}catch(ArgumentException){bad=true;}check("AMEX CSV length validated",bad);
                // Render actual WinForms controls to images for report illustrations.
                string screens=Path.Combine(Path.GetDirectoryName(Path.GetFullPath(reportPath)),"screens");Directory.CreateDirectory(screens);
                using(var main=new FrmTrangChu(c)){main.CreateControl();main.Show();main.Refresh();using(var image=new Bitmap(main.Width,main.Height)){main.DrawToBitmap(image,new Rectangle(Point.Empty,main.Size));image.Save(Path.Combine(screens,"main.png"));}main.Close();}
                using(var orders=new FrmLichSuDonHang(c)){orders.CreateControl();orders.Show();orders.Refresh();using(var image=new Bitmap(orders.Width,orders.Height)){orders.DrawToBitmap(image,new Rectangle(Point.Empty,orders.Size));image.Save(Path.Combine(screens,"orders.png"));}orders.Close();}
                check("Main and history forms instantiate",true);
                log.AppendLine("TOTAL "+count+" checks passed. Demo orders created for lab verification.");
                File.WriteAllText(reportPath,log.ToString(),Encoding.UTF8);
            }
            catch(Exception ex){File.WriteAllText(reportPath,log+"FAIL "+ex,Encoding.UTF8);throw;}
        }
    }
}
