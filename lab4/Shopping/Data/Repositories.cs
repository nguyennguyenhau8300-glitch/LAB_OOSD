using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Shopping.Models;

namespace Shopping.Data
{
    public class CustomerRepository
    {
        public DataRow Find(string username)
        {
            var dt=Db.Query("SELECT * FROM dbo.KhachHang WHERE TenDangNhap=@u",Db.P("@u",username));
            return dt.Rows.Count==0 ? null : dt.Rows[0];
        }
        public int Insert(Customer c, byte[] hash, byte[] salt, int iterations)
        {
            const string sql=@"INSERT dbo.KhachHang(HoTen,NgaySinh,SoGiayTo,DiaChi,DienThoai,TenDangNhap,MatKhauHash,MatKhauSalt,SoLanLapHash,Email)
VALUES(@n,@b,@i,@a,@p,@u,@h,@s,@r,@e); SELECT CAST(SCOPE_IDENTITY() AS int);";
            using(var cn=Db.Open()) using(var cmd=new SqlCommand(sql,cn))
            {
                cmd.Parameters.AddRange(new[] {Db.P("@n",c.Name),Db.P("@b",c.Birthday.Date),Db.P("@i",c.Identity),Db.P("@a",c.Address),Db.P("@p",c.Phone),Db.P("@u",c.Username),Db.P("@h",hash),Db.P("@s",salt),Db.P("@r",iterations),Db.P("@e",string.IsNullOrWhiteSpace(c.Email)?null:c.Email)});
                return (int)cmd.ExecuteScalar();
            }
        }
        public static Customer Map(DataRow r) { return new Customer {Id=(int)r["MaKhachHang"],Name=(string)r["HoTen"],Birthday=(DateTime)r["NgaySinh"],Identity=(string)r["SoGiayTo"],Address=(string)r["DiaChi"],Phone=(string)r["DienThoai"],Username=(string)r["TenDangNhap"],Email=r.IsNull("Email")?null:(string)r["Email"]}; }
    }
    public class OrderRepository
    {
        public long Save(OrderSnapshot o, PaymentResult payment)
        {
            if(payment.Status!="ThanhCong" || payment.Amount!=o.Quote.Total || string.IsNullOrWhiteSpace(payment.TransactionId)) throw new InvalidOperationException("Giao dịch thanh toán không khớp đơn.");
            if(o.Quote.Items.Count==0 || o.Quote.Items.Any(x=>x.Quantity<=0 || x.Price<0)) throw new InvalidOperationException("Chi tiết đơn không hợp lệ.");
            using(var c=Db.Open()) using(var tx=c.BeginTransaction(IsolationLevel.Serializable))
            {
                using(var check=new SqlCommand("SELECT MaDonHang,MaKhachHang,TongTien FROM dbo.DonHang WITH (UPDLOCK,HOLDLOCK) WHERE MaYeuCauThanhToan=@r",c,tx))
                {
                    check.Parameters.Add(Db.P("@r",o.RequestId));
                    using(var reader=check.ExecuteReader()) if(reader.Read())
                    {
                        if(reader.GetInt32(1)!=o.Buyer.Id || reader.GetDecimal(2)!=o.Quote.Total) throw new InvalidOperationException("Mã yêu cầu đã dùng cho đơn khác.");
                        long existing=reader.GetInt64(0); reader.Close(); tx.Commit(); return existing;
                    }
                }
                const string sql=@"INSERT dbo.DonHang(MaKhachHang,TenNguoiNhan,DiaChiNhan,DienThoaiNhan,KhuVucGiao,LoaiGiaoHang,TienHang,PhiGiaoHang,PhiThe,LoaiThe,MaYeuCauThanhToan,MaGiaoDichThanhToan,TrangThaiEmail)
VALUES(@kh,@n,@a,@p,@region,@delivery,@goods,@ship,@fee,@card,@req,@tran,@mail); SELECT CAST(SCOPE_IDENTITY() AS bigint);";
                long id;
                using(var cmd=new SqlCommand(sql,c,tx))
                {
                    cmd.Parameters.AddRange(new[] {Db.P("@kh",o.Buyer.Id),Db.P("@n",o.Input.Recipient),Db.P("@a",o.Input.Address),Db.P("@p",o.Input.Phone),Db.P("@region",o.Input.Region),Db.P("@delivery",o.Input.Delivery),Db.P("@goods",o.Quote.Goods),Db.P("@ship",o.Quote.Shipping),Db.P("@fee",o.Quote.CardFee),Db.P("@card",o.Input.CardType),Db.P("@req",o.RequestId),Db.P("@tran",payment.TransactionId),Db.P("@mail",string.IsNullOrWhiteSpace(o.Buyer.Email)?"KhongCoEmail":"ChoGui")});
                    id=(long)cmd.ExecuteScalar();
                }
                foreach(var item in o.Quote.Items)
                    using(var cmd=new SqlCommand("INSERT dbo.ChiTietDonHang(MaDonHang,MaSanPham,TenSanPhamLucDat,SoLuong,DonGiaLucDat) VALUES(@id,@code,@name,@q,@price)",c,tx))
                    { cmd.Parameters.AddRange(new[] {Db.P("@id",id),Db.P("@code",item.Code),Db.P("@name",item.Name),Db.P("@q",item.Quantity),Db.P("@price",item.Price)});cmd.ExecuteNonQuery(); }
                using(var cmd=new SqlCommand("SELECT SUM(ThanhTien) FROM dbo.ChiTietDonHang WHERE MaDonHang=@id",c,tx))
                {cmd.Parameters.Add(Db.P("@id",id)); if(Convert.ToDecimal(cmd.ExecuteScalar())!=o.Quote.Goods) throw new InvalidOperationException("Tổng chi tiết không khớp tiền hàng.");}
                tx.Commit(); return id;
            }
        }
        public void EmailStatus(long id,string status)
        { using(var c=Db.Open()) using(var cmd=new SqlCommand("UPDATE dbo.DonHang SET TrangThaiEmail=@s WHERE MaDonHang=@id",c)) {cmd.Parameters.AddRange(new[]{Db.P("@s",status),Db.P("@id",id)});cmd.ExecuteNonQuery();} }
        public DataTable List(int customerId) { return Db.Query("SELECT MaDonHang,ThoiDiemDat,TenNguoiNhan,LoaiGiaoHang,TienHang,PhiGiaoHang,PhiThe,TongTien,TrangThai,TrangThaiEmail FROM dbo.DonHang WHERE MaKhachHang=@id ORDER BY MaDonHang DESC",Db.P("@id",customerId)); }
        public DataTable Details(long id,int customerId) { return Db.Query("SELECT ct.MaSanPham,ct.TenSanPhamLucDat,ct.SoLuong,ct.DonGiaLucDat,ct.ThanhTien FROM dbo.ChiTietDonHang ct JOIN dbo.DonHang d ON d.MaDonHang=ct.MaDonHang WHERE ct.MaDonHang=@id AND d.MaKhachHang=@kh",Db.P("@id",id),Db.P("@kh",customerId)); }
    }
}
