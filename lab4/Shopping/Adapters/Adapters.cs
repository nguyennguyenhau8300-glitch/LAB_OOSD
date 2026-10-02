using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Models;

namespace Shopping.Adapters
{
    public interface IProductAdapter
    {
        List<Product> GetAll();
        Product Get(string code);
    }
    // SQL view mo phong nguon san pham ngoai trong pham vi bai lab.
    public class SqlLabProductAdapter : IProductAdapter
    {
        public List<Product> GetAll()
        {
            var table = Shopping.Data.Db.Query("SELECT * FROM dbo.vwSanPhamLab ORDER BY MaSanPham");
            var result = new List<Product>();
            foreach (System.Data.DataRow row in table.Rows) result.Add(Map(row));
            return result;
        }
        public Product Get(string code)
        {
            var table = Shopping.Data.Db.Query("SELECT * FROM dbo.vwSanPhamLab WHERE MaSanPham=@code", Shopping.Data.Db.P("@code", code));
            return table.Rows.Count == 0 ? null : Map(table.Rows[0]);
        }
        private static Product Map(System.Data.DataRow row)
        {
            return new Product {
                Code = (string)row["MaSanPham"], Group = (string)row["NhomSanPham"],
                Name = (string)row["TenSanPham"], Maker = (string)row["NhaSanXuat"],
                Price = (decimal)row["GiaBan"], Available = (bool)row["ConHang"],
                Description = (string)row["MoTa"], Specs = (string)row["ThongSo"]
            };
        }
    }
    public interface IPaymentAdapter
    {
        Task<PaymentResult> PayAsync(Guid requestId, CardInput card, decimal amount, PaymentScenario scenario);
        Task<PaymentResult> LookupAsync(Guid requestId);
    }
    public class MockPaymentAdapter : IPaymentAdapter
    {
        private readonly Dictionary<Guid, PaymentResult> results = new Dictionary<Guid, PaymentResult>();
        public async Task<PaymentResult> PayAsync(Guid requestId, CardInput card, decimal amount, PaymentScenario scenario)
        {
            await Task.Delay(250);
            lock (results)
            {
                if (results.ContainsKey(requestId)) return results[requestId];
                var result = new PaymentResult { Status=scenario == PaymentScenario.Declined ? "TuChoi" : "ThanhCong", Amount=amount, TransactionId="LAB-" + requestId.ToString("N") };
                results.Add(requestId, result);
                // Timeout mo phong mat phan hoi sau khi cong da chap nhan: doi soat tra ket qua goc.
                if (scenario == PaymentScenario.Timeout) return new PaymentResult { Status="ChuaRo", Amount=amount };
                return result;
            }
        }
        public async Task<PaymentResult> LookupAsync(Guid requestId)
        {
            await Task.Delay(150);
            lock (results) { return results.ContainsKey(requestId) ? results[requestId] : new PaymentResult { Status="ChuaRo" }; }
        }
    }
    public interface IEmailAdapter { Task SendAsync(string email, long orderId, OrderSnapshot order, bool simulateFailure); }
    public class FileEmailAdapter : IEmailAdapter
    {
        public string Outbox { get; private set; }
        public FileEmailAdapter(string outbox) { Outbox = outbox; }
        public Task SendAsync(string email, long orderId, OrderSnapshot order, bool simulateFailure)
        {
            if (simulateFailure) throw new IOException("Lỗi gửi email giả lập.");
            Directory.CreateDirectory(Outbox);
            var text = new StringBuilder();
            text.AppendLine("To: " + email); text.AppendLine("Subject: Xác nhận đơn e-SHOPPING #" + orderId);
            text.AppendLine("Người mua: " + order.Buyer.Name); text.AppendLine("Người nhận: " + order.Input.Recipient);
            text.AppendLine("Địa chỉ: " + order.Input.Address); text.AppendLine("Điện thoại: " + order.Input.Phone);
            text.AppendLine("Giao hàng: " + order.Input.Delivery + " / " + order.Input.Region);
            foreach (var item in order.Quote.Items) text.AppendLine(item.Code + " - " + item.Name + " | SL " + item.Quantity + " | Đơn giá " + item.Price.ToString("N0") + " | Tiền " + item.Total.ToString("N0"));
            text.AppendLine("Tiền hàng: " + order.Quote.Goods.ToString("N0")); text.AppendLine("Phí giao: " + order.Quote.Shipping.ToString("N0"));
            text.AppendLine("Phí thanh toán: " + order.Quote.CardFee.ToString("N0")); text.AppendLine("Tổng tiền: " + order.Quote.Total.ToString("N0") + " đ");
            File.WriteAllText(Path.Combine(Outbox, "DonHang-" + orderId + ".txt"), text.ToString(), Encoding.UTF8);
            return Task.CompletedTask;
        }
    }
}
