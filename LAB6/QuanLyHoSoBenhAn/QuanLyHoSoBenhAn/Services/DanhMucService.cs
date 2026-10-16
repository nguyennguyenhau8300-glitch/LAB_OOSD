using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;
namespace QuanLyHoSoBenhAn.Services;

public enum LoaiDanhMuc { Khoa, BacSi, Thuoc }

public class DanhMucService
{
    static (string Table, string Id, string Ten) Meta(LoaiDanhMuc loai) => loai switch
    {
        LoaiDanhMuc.Khoa => ("Khoa", "MaKhoa", "TenKhoa"),
        LoaiDanhMuc.BacSi => ("BacSi", "MaBS", "HoTen"),
        LoaiDanhMuc.Thuoc => ("Thuoc", "MaThuoc", "TenThuoc"),
        _ => throw new ArgumentOutOfRangeException(nameof(loai))
    };

    public DataTable DanhSach(LoaiDanhMuc loai, string tuKhoa = "")
    {
        var m = Meta(loai);
        string select = loai == LoaiDanhMuc.BacSi
            ? "SELECT d.MaBS,d.HoTen,d.MaKhoa,k.TenKhoa FROM BacSi d JOIN Khoa k ON k.MaKhoa=d.MaKhoa"
            : $"SELECT d.* FROM {m.Table} d";
        return Db.Query(select + $" WHERE d.{m.Ten} LIKE @q OR CONVERT(varchar,d.{m.Id})=@id ORDER BY d.{m.Id} DESC",
            Db.P("@q", "%" + tuKhoa.Trim() + "%"), Db.P("@id", tuKhoa));
    }

    static object Run(SqlConnection c, SqlTransaction t, string sql, params SqlParameter[] ps)
    {
        using var cmd = new SqlCommand(sql, c, t); cmd.Parameters.AddRange(ps); return cmd.ExecuteScalar();
    }
    static void Can(bool value, string message) { if (!value) throw new InvalidOperationException(message); }
    static KetQuaXuLy Tx(Action<SqlConnection, SqlTransaction> work, string message)
    {
        try
        {
            using var c = Db.OpenConnection(); using var t = c.BeginTransaction(IsolationLevel.Serializable);
            work(c, t); t.Commit(); return KetQuaXuLy.Ok(message);
        }
        catch (SqlException e) when (e.Number is 2601 or 2627) { return KetQuaXuLy.Fail("Tên danh mục đã tồn tại. Nhập tên khác."); }
        catch (SqlException e) when (e.Number == 547) { return KetQuaXuLy.Fail("Khóa tham chiếu không hợp lệ hoặc danh mục đang được sử dụng."); }
        catch (Exception e) { return KetQuaXuLy.Fail(e.Message); }
    }

    public KetQuaXuLy Luu(LoaiDanhMuc loai, int? id, string ten, string donVi = "", int? maKhoa = null) => Tx((c, t) =>
    {
        var m = Meta(loai); ten = (ten ?? "").Trim(); donVi = (donVi ?? "").Trim();
        Can(ten.Length > 0, "Nhập tên danh mục hoặc họ tên bác sĩ.");
        Can(ten.Length <= (loai == LoaiDanhMuc.Thuoc ? 150 : 100), "Tên vượt quá độ dài cho phép.");
        if (id.HasValue) Can(id > 0, "Chọn mã danh mục hợp lệ.");
        if (loai == LoaiDanhMuc.Thuoc) Can(donVi.Length is > 0 and <= 30, "Đơn vị thuốc phải có từ 1 đến 30 ký tự.");
        if (loai == LoaiDanhMuc.BacSi)
            Can(maKhoa.HasValue && Convert.ToInt32(Run(c,t,"SELECT COUNT(*) FROM Khoa WHERE MaKhoa=@k",Db.P("@k",maKhoa))) == 1, "Chọn khoa tồn tại cho bác sĩ.");
        var ps = new List<SqlParameter> { Db.P("@n", ten) };
        string extraColumn = "", extraValue = "", extraSet = "";
        if (loai == LoaiDanhMuc.Thuoc) { extraColumn=",DonVi"; extraValue=",@v"; extraSet=",DonVi=@v"; ps.Add(Db.P("@v",donVi)); }
        if (loai == LoaiDanhMuc.BacSi) { extraColumn=",MaKhoa"; extraValue=",@k"; extraSet=",MaKhoa=@k"; ps.Add(Db.P("@k",maKhoa)); }
        if (id.HasValue)
        {
            ps.Add(Db.P("@i",id));
            var n = Run(c,t,$"UPDATE {m.Table} SET {m.Ten}=@n{extraSet} WHERE {m.Id}=@i;SELECT @@ROWCOUNT",ps.ToArray());
            Can(Convert.ToInt32(n)==1, "Danh mục đã bị xóa hoặc mã không tồn tại. Hãy tải lại.");
        }
        else Run(c,t,$"INSERT {m.Table}({m.Ten}{extraColumn}) VALUES(@n{extraValue});SELECT SCOPE_IDENTITY()",ps.ToArray());
    }, id.HasValue ? "Đã cập nhật danh mục." : "Đã thêm danh mục.");

    public KetQuaXuLy Xoa(LoaiDanhMuc loai, int id) => Tx((c,t) =>
    {
        var m=Meta(loai); Can(id>0,"Chọn danh mục cần xóa.");
        string refs = loai switch
        {
            LoaiDanhMuc.Khoa => "SELECT (SELECT COUNT(*) FROM BacSi WHERE MaKhoa=@i)+(SELECT COUNT(*) FROM PhieuKham WHERE MaKhoa=@i)",
            LoaiDanhMuc.BacSi => "SELECT (SELECT COUNT(*) FROM PhieuKham WHERE MaBS=@i)+(SELECT COUNT(*) FROM LichHen WHERE MaBS=@i)",
            LoaiDanhMuc.Thuoc => "SELECT COUNT(*) FROM ChiTietToa WHERE MaThuoc=@i",
            _ => throw new ArgumentOutOfRangeException(nameof(loai))
        };
        Can(Convert.ToInt32(Run(c,t,refs,Db.P("@i",id)))==0,"Danh mục đang được tham chiếu trong dữ liệu khác; không được xóa.");
        var count=Run(c,t,$"DELETE {m.Table} WHERE {m.Id}=@i;SELECT @@ROWCOUNT",Db.P("@i",id));
        Can(Convert.ToInt32(count)==1,"Mã danh mục không tồn tại hoặc đã bị xóa.");
    }, "Đã xóa danh mục chưa được sử dụng.");
}
