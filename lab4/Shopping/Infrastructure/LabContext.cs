using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Adapters;
using Shopping.Data;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public class LabContext
    {
        public Session Session { get; private set; }=new Session();
        public IProductAdapter Products { get; private set; }=new SqlLabProductAdapter();
        public AccountService Accounts { get; private set; }=new AccountService();
        public OrderRepository Orders { get; private set; }=new OrderRepository();
        public CartService Cart { get; private set; }
        public CheckoutService Checkout { get; private set; }
        public string Outbox { get; private set; }
        public LabContext()
        {
            Outbox=Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Outbox");
            Cart=new CartService(Session,Products);
            Checkout=new CheckoutService(Session,Products,new MockPaymentAdapter(),new FileEmailAdapter(Outbox));
        }
    }
}
