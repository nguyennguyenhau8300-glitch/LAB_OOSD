using System.Data;
using QuanLyCaoOc.Data;
using static QuanLyCaoOc.Services.Rules;

namespace QuanLyCaoOc.Services;

public sealed class DanhMucService
{
    public static readonly Dictionary<string, string[]> Columns = new()
    {
        ["Tang"] = ["MaTang","SoTang","TenTang"],
        ["VanPhong"] = ["MaPhong","MaTang","DienTich","Huong","GiaThamKhao","NgaySuDung","NgungSuDung"],
        ["KhachHang"] = ["MaKH","TenCongTy","MaSoThue","DaiDien","DienThoai"],
        ["DichVu"] = ["MaDV","TenDV","DonViTinh","GiaMacDinh"]
    };
    public DataTable List(string table)
    {
        Require(Columns.ContainsKey(table), "Danh mục không hợp lệ.");
        return Db.Query($"SELECT * FROM [{table}] ORDER BY [{Columns[table][0]}]");
    }
    public DataTable TimPhong(string text, string? tang, DateTime from, DateTime to)
    {
        Require(from.Date < to.Date, "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
        return Db.Query("""
            SELECT p.*,t.SoTang,CAST(CASE WHEN p.NgungSuDung=0 AND p.NgaySuDung<=@p2 AND NOT EXISTS(
            SELECT 1 FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD
            WHERE ct.MaPhong=p.MaPhong AND h.TrangThai IN('CHO_TRA_TRUOC','HIEU_LUC')
            AND h.NgayBatDau<@p3 AND @p2<ct.NgayKetThuc) THEN 1 ELSE 0 END AS bit) ConTrong
            FROM VanPhong p JOIN Tang t ON t.MaTang=p.MaTang
            WHERE (@p1 IS NULL OR p.MaTang=@p1) AND (p.MaPhong LIKE @p0 OR p.Huong LIKE @p0 OR t.TenTang LIKE @p0)
            ORDER BY t.SoTang,p.MaPhong
            """, "%"+text.Trim()+"%", tang, from.Date, to.Date);
    }
    public void Save(string table, IReadOnlyDictionary<string, object?> values, bool edit) => Db.Transaction((c, tx) =>
    {
        Require(Columns.ContainsKey(table), "Danh mục không hợp lệ."); var cols = Columns[table];
        Require(cols.All(values.ContainsKey), "Thiếu trường dữ liệu.");
        foreach (var key in cols)
            if (values[key] is string text) { Text(text,key); Require(text.Length <= (key.StartsWith("Ma") ? 20 : 160), "Nội dung quá dài: " + key); }
        if (table == "VanPhong")
        {
            Precision(Convert.ToDecimal(values["DienTich"]));Precision(Convert.ToDecimal(values["GiaThamKhao"]));
            Require(Convert.ToDecimal(values["DienTich"]) > 0 && Convert.ToDecimal(values["GiaThamKhao"]) > 0, "Diện tích và giá phải dương.");
            var first = Db.Scalar(c, tx, "SELECT MIN(h.NgayBatDau) FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE ct.MaPhong=@p0", values["MaPhong"]);
            if (first is DateTime date) Require(Convert.ToDateTime(values["NgaySuDung"]) <= date, "Ngày sử dụng không được sau lịch sử thuê đầu tiên.");
        }
        if (table == "DichVu") {Precision(Convert.ToDecimal(values["GiaMacDinh"]));Require(Convert.ToDecimal(values["GiaMacDinh"]) >= 0, "Đơn giá không âm.");}
        var args = cols.Select(x => values[x]).ToArray();
        string sql = edit ? $"UPDATE [{table}] SET " + string.Join(",", cols.Skip(1).Select((x,i) => $"[{x}]=@p{i+1}")) + $" WHERE [{cols[0]}]=@p0"
            : $"INSERT [{table}]({string.Join(",",cols.Select(x=>"["+x+"]"))}) VALUES({string.Join(",",cols.Select((_,i)=>"@p"+i))})";
        Require(Db.Exec(c,tx,sql,args)==1,"Bản ghi không còn tồn tại."); return true;
    });
    public void Delete(string table, string id) => Db.Transaction((c,tx)=>
    {
        Require(Columns.ContainsKey(table),"Danh mục không hợp lệ.");
        Require(Db.Exec(c,tx,$"DELETE [{table}] WHERE [{Columns[table][0]}]=@p0",id)==1,"Không tìm thấy bản ghi."); return true;
    });
}
