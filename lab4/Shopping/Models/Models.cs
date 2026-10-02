using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.Models
{
    public class Product
    {
        public string Code { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public string Description { get; set; }
        public string Specs { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string Status { get { return Available ? "Còn hàng" : "Hết hàng"; } }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Identity { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
    public class CartItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public CartItem Copy() { return (CartItem)MemberwiseClone(); }
    }
    public class Session
    {
        public Customer Customer { get; set; }
        public List<CartItem> Cart { get; private set; } = new List<CartItem>();
        public Guid? PendingRequest { get; set; }
    }
    public class CardInput
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public string SecurityCode { get; set; }
        public string Holder { get; set; }
        public DateTime Expiry { get; set; }
    }
    public class Quote
    {
        public List<CartItem> Items { get; set; }
        public decimal Goods { get { return Items.Sum(x => x.Total); } }
        public decimal Shipping { get; set; }
        public decimal CardFee { get; set; }
        public decimal Total { get { return Goods + Shipping + CardFee; } }
        public string Fingerprint { get { return string.Join("|", Items.OrderBy(x => x.Code).Select(x => x.Code + ":" + x.Quantity + ":" + x.Price.ToString(System.Globalization.CultureInfo.InvariantCulture))) + ";" + Shipping + ";" + CardFee; } }
    }
    public class CheckoutInput
    {
        public string Recipient { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; }
        public string Delivery { get; set; }
        public string CardType { get; set; }
        public string AcceptedQuote { get; set; }
    }
    public class OrderSnapshot
    {
        public Customer Buyer { get; set; }
        public CheckoutInput Input { get; set; }
        public Quote Quote { get; set; }
        public Guid RequestId { get; set; }
    }
    public enum PaymentScenario { Success, Declined, Timeout }
    public class PaymentResult
    {
        public string Status { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
    }
    public class CheckoutResult
    {
        public string Status { get; set; }
        public long OrderId { get; set; }
        public string Message { get; set; }
    }
}
