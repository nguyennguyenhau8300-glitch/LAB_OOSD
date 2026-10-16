using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn;

public static class DanhMucVerification
{
    public static void Run()
    {
        var service=new DanhMucService(); var log=new List<string>();
        string tag="LAB6_DM_TEST_"+Guid.NewGuid().ToString("N")[..8];
        int Id(string table,string key,string field,string name) => Convert.ToInt32(Db.Scalar($"SELECT {key} FROM {table} WHERE {field}=@n",Db.P("@n",name)));
        void Check(bool ok,string name){log.Add((ok?"PASS ":"FAIL ")+name);if(!ok)throw new InvalidOperationException(name);}
        try
        {
            Check(service.Luu(LoaiDanhMuc.Khoa,null,tag).ThanhCong,"Thêm khoa");
            int khoa=Id("Khoa","MaKhoa","TenKhoa",tag);
            Check(!service.Luu(LoaiDanhMuc.Khoa,null,tag).ThanhCong,"Chặn tên khoa trùng");
            Check(!service.Luu(LoaiDanhMuc.Khoa,null," ").ThanhCong,"Chặn tên khoa rỗng");
            Check(service.Luu(LoaiDanhMuc.Khoa,khoa,tag+"_SUA").ThanhCong,"Sửa tên khoa");
            Check(!service.Luu(LoaiDanhMuc.Khoa,null,new string('x',101)).ThanhCong,"Chặn tên khoa quá dài");
            Check(service.Luu(LoaiDanhMuc.BacSi,null,tag+"_BS",maKhoa:khoa).ThanhCong,"Thêm bác sĩ thuộc khoa");
            int bs=Id("BacSi","MaBS","HoTen",tag+"_BS");
            Check(!service.Luu(LoaiDanhMuc.BacSi,null,tag+"_BAD",maKhoa:int.MaxValue).ThanhCong,"Chặn khoa không tồn tại");
            Check(!service.Luu(LoaiDanhMuc.BacSi,null,"",maKhoa:khoa).ThanhCong,"Chặn họ tên bác sĩ rỗng");
            Check(service.Luu(LoaiDanhMuc.BacSi,bs,tag+"_BS_SUA",maKhoa:khoa).ThanhCong,"Sửa bác sĩ");
            Check(!service.Xoa(LoaiDanhMuc.Khoa,khoa).ThanhCong,"Chặn xóa khoa đang có bác sĩ");
            Check(service.Luu(LoaiDanhMuc.Thuoc,null,tag+"_THUOC","Viên").ThanhCong,"Thêm thuốc");
            int thuoc=Id("Thuoc","MaThuoc","TenThuoc",tag+"_THUOC");
            Check(!service.Luu(LoaiDanhMuc.Thuoc,null,tag+"_THUOC","Gói").ThanhCong,"Chặn tên thuốc trùng");
            Check(!service.Luu(LoaiDanhMuc.Thuoc,null,tag+"_BAD","").ThanhCong,"Chặn đơn vị thuốc rỗng");
            Check(!service.Luu(LoaiDanhMuc.Thuoc,null,tag+"_BAD",new string('x',31)).ThanhCong,"Chặn đơn vị thuốc quá dài");
            Check(service.Luu(LoaiDanhMuc.Thuoc,thuoc,tag+"_THUOC_SUA","Gói").ThanhCong,"Sửa tên và đơn vị thuốc");
            Check(service.DanhSach(LoaiDanhMuc.Thuoc,tag).Rows.Count==1,"Tìm thuốc theo từ khóa");
            Check(!service.Luu(LoaiDanhMuc.Thuoc,int.MaxValue,"Không tồn tại","Viên").ThanhCong,"Chặn sửa mã không tồn tại");
            Check(!service.Xoa(LoaiDanhMuc.Thuoc,int.MaxValue).ThanhCong,"Chặn xóa mã không tồn tại");
            int usedThuoc=Convert.ToInt32(Db.Scalar("SELECT TOP(1) MaThuoc FROM ChiTietToa"));
            int usedBs=Convert.ToInt32(Db.Scalar("SELECT TOP(1) MaBS FROM PhieuKham"));
            int usedKhoa=Convert.ToInt32(Db.Scalar("SELECT TOP(1) MaKhoa FROM PhieuKham"));
            Check(usedThuoc>0&&!service.Xoa(LoaiDanhMuc.Thuoc,usedThuoc).ThanhCong,"Bảo vệ thuốc trong toa đã lưu");
            Check(usedBs>0&&!service.Xoa(LoaiDanhMuc.BacSi,usedBs).ThanhCong,"Bảo vệ bác sĩ trong phiếu khám");
            Check(usedKhoa>0&&!service.Xoa(LoaiDanhMuc.Khoa,usedKhoa).ThanhCong,"Bảo vệ khoa điều trị lịch sử");
            Check(service.Xoa(LoaiDanhMuc.Thuoc,thuoc).ThanhCong,"Xóa thuốc thử chưa sử dụng");
            Check(service.Xoa(LoaiDanhMuc.BacSi,bs).ThanhCong,"Xóa bác sĩ thử chưa sử dụng");
            Check(service.Xoa(LoaiDanhMuc.Khoa,khoa).ThanhCong,"Xóa khoa thử hết tham chiếu");
            Check(!service.Luu((LoaiDanhMuc)99,null,tag).ThanhCong,"Chặn loại danh mục không hợp lệ");
            Console.WriteLine($"PASS {log.Count}/{log.Count}");
        }
        finally
        {
            // Remove only records owned by this uniquely named test run.
            Db.Execute("DELETE BacSi WHERE HoTen LIKE @n",Db.P("@n",tag+"%"));
            Db.Execute("DELETE Thuoc WHERE TenThuoc LIKE @n",Db.P("@n",tag+"%"));
            Db.Execute("DELETE Khoa WHERE TenKhoa LIKE @n",Db.P("@n",tag+"%"));
            string dir=Path.Combine(AppContext.BaseDirectory,"Verification");Directory.CreateDirectory(dir);
            File.WriteAllLines(Path.Combine(dir,"catalogue-results.txt"),log);
        }
    }
}
