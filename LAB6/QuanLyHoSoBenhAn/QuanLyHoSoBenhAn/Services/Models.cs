namespace QuanLyHoSoBenhAn.Services;

public record KetQuaXuLy(bool ThanhCong, string ThongBao)
{
    public static KetQuaXuLy Ok(string s) => new(true, s);
    public static KetQuaXuLy Fail(string s) => new(false, s);
}
public record DongThuoc(int MaThuoc, int SoLuong, string LieuDung);
