using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Shopping.Adapters;
using Shopping.Data;
using Shopping.Models;

namespace Shopping.Services
{
    public static class PasswordHasher
    {
        public const int Iterations=100000;
        public static byte[] Salt() { var s=new byte[16];using(var rng=RandomNumberGenerator.Create())rng.GetBytes(s);return s; }
        public static byte[] Hash(string password,byte[] salt,int iterations) {return Rfc2898DeriveBytes.Pbkdf2(password,salt,iterations,HashAlgorithmName.SHA256,32);}
        public static bool Equal(byte[] a,byte[] b) {if(a.Length!=b.Length)return false;int diff=0;for(int i=0;i<a.Length;i++)diff|=a[i]^b[i];return diff==0;}
    }
    public class AccountService
    {
        private readonly CustomerRepository repo=new CustomerRepository();
        public Customer Register(Customer c,string password,string confirm)
        {
            if(new[]{c.Name,c.Identity,c.Address,c.Phone,c.Username}.Any(string.IsNullOrWhiteSpace))throw new ArgumentException("Vui lòng nhập đầy đủ thông tin bắt buộc.");
            if(c.Birthday.Date>DateTime.Today)throw new ArgumentException("Ngày sinh không được ở tương lai.");
            if(string.IsNullOrEmpty(password)||password.Length<6||password!=confirm)throw new ArgumentException("Mật khẩu cần ít nhất 6 ký tự và nhập lại phải khớp.");
            if(!Regex.IsMatch(c.Phone,@"^[0-9+ ()-]{8,20}$"))throw new ArgumentException("Điện thoại không hợp lệ.");
            if(!string.IsNullOrWhiteSpace(c.Email)){try{var address=new MailAddress(c.Email);if(address.Address!=c.Email)throw new FormatException();}catch{throw new ArgumentException("Email không hợp lệ.");}}
            c.Username=c.Username.Trim();
            if(repo.Find(c.Username)!=null)throw new ArgumentException("Tên đăng nhập đã tồn tại.");
            var salt=PasswordHasher.Salt();
            try{c.Id=repo.Insert(c,PasswordHasher.Hash(password,salt,PasswordHasher.Iterations),salt,PasswordHasher.Iterations);}catch(SqlException ex){if(ex.Number==2601||ex.Number==2627)throw new ArgumentException("Tên đăng nhập đã tồn tại.");throw;}
            return c;
        }
        public Customer Login(string user,string password)
        {
            var r=repo.Find((user??"").Trim());
            if(r==null||!PasswordHasher.Equal((byte[])r["MatKhauHash"],PasswordHasher.Hash(password??"",(byte[])r["MatKhauSalt"],(int)r["SoLanLapHash"])))throw new ArgumentException("Tên đăng nhập hoặc mật khẩu không đúng.");
            return CustomerRepository.Map(r);
        }
        public void SeedDemo()
        {
            if(repo.Find("demo")==null)Register(new Customer{Name="Khách hàng demo",Birthday=new DateTime(2000,1,1),Identity="LAB-DEMO",Address="123 Nguyễn Văn Cừ, TP.HCM",Phone="0901234567",Username="demo",Email="demo@example.com"},"Demo@123","Demo@123");
        }
    }
    public class CartService
    {
        private readonly Session session;private readonly IProductAdapter products;
        public CartService(Session session,IProductAdapter products){this.session=session;this.products=products;}
        public void Add(string code,int quantity)
        {
            EnsureEditable();
            if(quantity<=0)throw new ArgumentException("Số lượng phải lớn hơn 0.");
            var p=products.Get(code);if(p==null||!p.Available)throw new ArgumentException("Sản phẩm không còn hàng.");
            var i=session.Cart.SingleOrDefault(x=>x.Code==code);
            if(i==null)session.Cart.Add(new CartItem{Code=code,Name=p.Name,Quantity=quantity,Price=p.Price});
            else{i.Quantity=checked(i.Quantity+quantity);i.Price=p.Price;}
        }
        private void EnsureEditable(){if(session.PendingRequest.HasValue)throw new InvalidOperationException("Giỏ đang chờ đối soát. Hoàn tất giao dịch trước khi sửa giỏ.");}
        public void Update(string code,int quantity){EnsureEditable();if(quantity<=0)throw new ArgumentException("Số lượng phải > 0.");session.Cart.Single(x=>x.Code==code).Quantity=quantity;}
        public void Remove(string code){EnsureEditable();session.Cart.RemoveAll(x=>x.Code==code);}
    }
    public class FeeService
    {
        public static readonly string[] Regions={"Nội thành","Ngoại thành"};
        public static readonly string[] Deliveries={"THUONG","NHANH","TRONGNGAY"};
        public static readonly string[] Cards={"VISA","MASTER","DISCOVER","AMEX"};
        public decimal Shipping(decimal goods,string region,string delivery)
        {
            if(!Regions.Contains(region)||!Deliveries.Contains(delivery))throw new ArgumentException("Khu vực/loại giao không hợp lệ.");
            if((delivery=="NHANH"&&goods>=1000000)||(delivery=="TRONGNGAY"&&goods>=5000000))return 0;
            decimal[] rates=region=="Nội thành"?new decimal[]{20000,40000,70000}:new decimal[]{30000,60000,100000};
            return rates[Array.IndexOf(Deliveries,delivery)];
        }
        public decimal CardFee(string type){if(!Cards.Contains(type))throw new ArgumentException("Loại thẻ không hỗ trợ.");return type=="AMEX"?5000:0;}
    }
    public static class CardValidator
    {
        public static void Validate(CardInput c)
        {
            if(c==null||!FeeService.Cards.Contains(c.Type))throw new ArgumentException("Loại thẻ không hợp lệ.");
            int number=c.Type=="AMEX"?15:16,security=c.Type=="AMEX"?4:3;
            if(!Regex.IsMatch(c.Number??"",@"^[0-9]{"+number+"}$"))throw new ArgumentException("Số thẻ cần "+number+" chữ số.");
            if(!Regex.IsMatch(c.SecurityCode??"",@"^[0-9]{"+security+"}$"))throw new ArgumentException("CSV cần "+security+" chữ số.");
            if(string.IsNullOrWhiteSpace(c.Holder))throw new ArgumentException("Chưa nhập tên chủ thẻ.");
            var current=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);if(new DateTime(c.Expiry.Year,c.Expiry.Month,1)<current)throw new ArgumentException("Thẻ đã hết hạn.");
        }
    }
    public class CheckoutService
    {
        private readonly Session session;private readonly IProductAdapter products;private readonly IPaymentAdapter payments;private readonly IEmailAdapter emails;
        private readonly FeeService fees=new FeeService();private readonly OrderRepository orders=new OrderRepository();
        private readonly Dictionary<Guid,OrderSnapshot> pending=new Dictionary<Guid,OrderSnapshot>();private readonly SemaphoreSlim gate=new SemaphoreSlim(1,1);
        public CheckoutService(Session session,IProductAdapter products,IPaymentAdapter payments,IEmailAdapter emails){this.session=session;this.products=products;this.payments=payments;this.emails=emails;}
        public Quote GetQuote(CheckoutInput input)
        {
            if(session.Cart.Count==0)throw new ArgumentException("Giỏ hàng đang trống.");
            var items=new List<CartItem>();
            foreach(var item in session.Cart){var p=products.Get(item.Code);if(p==null||!p.Available)throw new ArgumentException(item.Name+" đã hết hàng.");items.Add(new CartItem{Code=p.Code,Name=p.Name,Quantity=item.Quantity,Price=p.Price});}
            var q=new Quote{Items=items,CardFee=fees.CardFee(input.CardType)};q.Shipping=fees.Shipping(q.Goods,input.Region,input.Delivery);return q;
        }
        public async Task<CheckoutResult> PlaceAsync(CheckoutInput input,CardInput card,PaymentScenario scenario,bool failEmail)
        {
            await gate.WaitAsync();try
            {
                if(session.PendingRequest.HasValue)throw new InvalidOperationException("Còn giao dịch chưa rõ; hãy bấm Đối soát trước.");
                if(session.Customer==null)throw new ArgumentException("Khách cần đăng nhập.");
                if(new[]{input.Recipient,input.Address,input.Phone}.Any(string.IsNullOrWhiteSpace)||!Regex.IsMatch(input.Phone,@"^[0-9+ ()-]{8,20}$"))throw new ArgumentException("Thông tin người nhận chưa hợp lệ.");
                CardValidator.Validate(card);if(card.Type!=input.CardType)throw new ArgumentException("Loại thẻ không khớp báo giá.");
                var quote=GetQuote(input);if(quote.Fingerprint!=input.AcceptedQuote)throw new ArgumentException("Báo giá đã đổi. Bấm Tính lại và xác nhận trước khi thanh toán.");
                var snapshot=new OrderSnapshot{Buyer=session.Customer,Input=new CheckoutInput{Recipient=input.Recipient.Trim(),Address=input.Address.Trim(),Phone=input.Phone.Trim(),Region=input.Region,Delivery=input.Delivery,CardType=input.CardType},Quote=quote,RequestId=Guid.NewGuid()};
                pending.Add(snapshot.RequestId,snapshot);session.PendingRequest=snapshot.RequestId;
                var result=await payments.PayAsync(snapshot.RequestId,card,quote.Total,scenario);
                return await Resolve(snapshot,result,failEmail);
            }finally{gate.Release();}
        }
        public async Task<CheckoutResult> LookupAsync(bool failEmail)
        {
            await gate.WaitAsync();try
            {
                if(!session.PendingRequest.HasValue)throw new ArgumentException("Không có giao dịch cần đối soát.");
                var snapshot=pending[session.PendingRequest.Value];var result=await payments.LookupAsync(snapshot.RequestId);return await Resolve(snapshot,result,failEmail);
            }finally{gate.Release();}
        }
        private async Task<CheckoutResult> Resolve(OrderSnapshot snapshot,PaymentResult payment,bool failEmail)
        {
            if(payment.Status=="ChuaRo")return new CheckoutResult{Status="ChuaRo",Message="Chưa rõ kết quả. Giữ cửa sổ ứng dụng và bấm Đối soát; không thanh toán lại."};
            if(payment.Status=="TuChoi"){pending.Remove(snapshot.RequestId);session.PendingRequest=null;return new CheckoutResult{Status="TuChoi",Message="Thanh toán bị từ chối. Giỏ hàng được giữ nguyên."};}
            long id;
            try{id=orders.Save(snapshot,payment);}catch{throw new InvalidOperationException("Thanh toán đã thành công nhưng lưu đơn gặp lỗi. Giữ ứng dụng và bấm Đối soát để hoàn tất cùng giao dịch.");}
            session.Cart.Clear();pending.Remove(snapshot.RequestId);session.PendingRequest=null;
            string emailText="Không có email.";
            if(!string.IsNullOrWhiteSpace(snapshot.Buyer.Email))
            {
                try{await emails.SendAsync(snapshot.Buyer.Email,id,snapshot,failEmail);orders.EmailStatus(id,"DaGui");emailText="Đã tạo email xác nhận trong thư mục Outbox.";}
                catch{try{orders.EmailStatus(id,"GuiLoi");}catch{}emailText="Email gặp lỗi; đơn vẫn thành công.";}
            }
            return new CheckoutResult{Status="ThanhCong",OrderId=id,Message="Đặt hàng thành công #"+id+". "+emailText};
        }
    }
}
