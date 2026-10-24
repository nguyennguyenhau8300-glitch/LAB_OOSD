namespace QuanLyNhanSu.Services;

public record Field(string Name, string Label, string Kind="text", bool Required=true, string Source="");
public record Module(string Table, string Title, Field[] Fields, bool Editable=true);
public static class Modules
{
    static Field F(string n,string l,string k="text",bool r=true,string s="")=>new(n,l,k,r,s);
    public static readonly Module[] All = [
        new("NhanVien","Hồ sơ nhân viên",[F("HoTen","Họ tên"),F("NgaySinh","Ngày sinh","date"),F("DienThoai","Điện thoại"),F("DiaChi","Địa chỉ"),F("PhongBanId","Phòng ban","ref",true,"PhongBan")]),
        new("TuyenDung","Tuyển dụng và thử việc",[F("NhanVienId","Nhân viên","ref",true,"NhanVien"),F("SoQD","Số quyết định"),F("TuNgay","Bắt đầu thử việc","date"),F("DenNgay","Kết thúc thử việc","date"),F("LuongThuViec","Lương thử việc / tháng","money"),F("DieuKhoan","Điều khoản","long")]),
        new("HopDong","Hợp đồng",[F("NhanVienId","Nhân viên","ref",true,"NhanVien"),F("SoHD","Số hợp đồng"),F("Loai","Loại","choice",true,"Dài hạn|Ngắn hạn"),F("NgayKy","Ngày ký dự kiến","date"),F("TuNgay","Bắt đầu hiệu lực","date"),F("DenNgay","Kết thúc (bỏ chọn nếu vô hạn)","date",false),F("DieuKhoan","Điều khoản","long")]),
        new("QDLuong","Quyết định lương",[F("NhanVienId","Nhân viên","ref",true,"NhanVien"),F("SoQD","Số quyết định"),F("NgayHieuLuc","Ngày hiệu lực","date"),F("LuongCoBan","Lương cơ bản / tháng","money"),F("PhuCapLuong","Phụ cấp lương / tháng","money"),F("PhuCapDocHai","Phụ cấp độc hại / tháng","money"),F("NoiDung","Nội dung","long")]),
        new("QDChucVu","Quyết định chức vụ",[F("NhanVienId","Nhân viên","ref",true,"NhanVien"),F("ChucVuId","Chức vụ","ref",true,"ChucVu"),F("SoQD","Số quyết định"),F("NgayHieuLuc","Ngày hiệu lực","date"),F("PhuCap","Phụ cấp chức vụ / tháng","money"),F("NoiDung","Nội dung","long")]),
        new("BaoHiem","Bảo hiểm",[F("NhanVienId","Nhân viên","ref",true,"NhanVien"),F("Loai","Loại","choice",true,"BHYT|BHXH"),F("SoBH","Số bảo hiểm"),F("TuNgay","Ngày tham gia","date"),F("DenNgay","Ngày kết thúc (tùy chọn)","date",false),F("TyLe","Tỷ lệ người lao động đóng (%)","percent")]),
        new("PhanCong","Phân công tuần",[F("NhanVienId","Nhân viên","ref",true,"NhanVien"),F("CaLamId","Ca làm","ref",true,"CaLam"),F("NgayLam","Ngày làm","date"),F("GioPhuTroiDuKien","Giờ phụ trội dự kiến","hours")]),
        new("ChamCong","Chấm công",[F("PhanCongId","Phân công","ref",true,"PhanCong"),F("SoGio","Số giờ thực tế","hours"),F("GhiChu","Ghi chú","text",false)]),
        new("PhieuPhuTroi","Phiếu phụ trội",[F("SoPhieu","Số phiếu"),F("Ngay","Ngày phụ trội","date"),F("HinhThuc","Hình thức","choice",true,"Tăng ca|Ngày lễ|Ngày nghỉ"),F("SoGio","Số giờ tối đa mỗi nhân viên","hours")]),
        new("ChiTietPhuTroi","Nhân viên phụ trội",[F("PhieuId","Phiếu phụ trội","ref",true,"PhieuPhuTroi"),F("NhanVienId","Nhân viên","ref",true,"NhanVien"),F("SoGio","Số giờ tham gia","hours")]),
        new("ThuongPhat","Khen thưởng và kỷ luật",[F("SoQD","Số quyết định"),F("Loai","Loại","choice",true,"Khen thưởng|Kỷ luật"),F("NhanVienId","Cá nhân (chọn một đối tượng)","ref",false,"NhanVien"),F("PhongBanId","Tập thể (chọn một đối tượng)","ref",false,"PhongBan"),F("Ngay","Ngày quyết định","date"),F("LyDo","Lý do và hình thức","long"),F("SoTien","Số tiền","money"),F("PhuongThuc","Hình thức thanh toán","choice",true,"Trực tiếp|Qua lương")]),
        new("ThuChi","Phiếu thu chi",[],false),
        new("BangLuong","Bảng lương tháng",[],false),
        new("PhongBan","Danh mục phòng ban",[F("Ten","Tên phòng ban")]),
        new("ChucVu","Danh mục chức vụ",[F("Ten","Tên chức vụ")]),
        new("CaLam","Danh mục ca làm",[F("Ten","Tên ca"),F("GioBatDau","Giờ bắt đầu","time"),F("GioKetThuc","Giờ kết thúc","time")])
    ];
    public static Module Get(string table)=>All.Single(x=>x.Table==table);
}
