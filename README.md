# LAB - XÂY DỰNG HỆ THỐNG QUẢN LÝ THƯ VIỆN

## 1. THÔNG TIN SINH VIÊN

- **Họ và tên:** Nguyễn Nguyên Hậu
- **MSSV:** 1250080049
- **Lớp:** 12_ĐH_CNPM1
- **Ngôn ngữ lập trình:** C#
- **Loại ứng dụng:** Windows Forms
- **Cơ sở dữ liệu:** Microsoft SQL Server

---

# 2. GIỚI THIỆU BÀI THỰC HÀNH

Trong bài thực hành này, em xây dựng một ứng dụng **Quản lý thư viện** bằng ngôn ngữ lập trình C# sử dụng Windows Forms và kết nối với cơ sở dữ liệu Microsoft SQL Server.

Ứng dụng được xây dựng nhằm hỗ trợ việc quản lý các hoạt động cơ bản trong thư viện như quản lý sách, thể loại, nhà xuất bản, nhân viên, độc giả, thẻ độc giả, mượn sách, trả sách, xử lý phiếu phạt và thống kê dữ liệu.

Dữ liệu của chương trình không được lưu trực tiếp trong mã nguồn mà được lưu trong cơ sở dữ liệu SQL Server. Chương trình C# thực hiện kết nối đến SQL Server để đọc, thêm, sửa và xóa dữ liệu.

Giao diện chương trình được xây dựng bằng Windows Forms với các thành phần như:

- Form
- Label
- TextBox
- ComboBox
- Button
- DataGridView
- DateTimePicker

Ứng dụng được chia thành nhiều Form và lớp khác nhau để thuận tiện cho việc quản lý mã nguồn và xử lý các chức năng.

---

# 3. MỤC TIÊU BÀI THỰC HÀNH

Mục tiêu của bài thực hành là vận dụng kiến thức lập trình hướng đối tượng và lập trình Windows Forms để xây dựng một ứng dụng quản lý có kết nối cơ sở dữ liệu.

Các mục tiêu cụ thể gồm:

- Xây dựng giao diện ứng dụng bằng Windows Forms.
- Thiết kế cơ sở dữ liệu quản lý thư viện.
- Kết nối ứng dụng C# với SQL Server.
- Hiển thị dữ liệu SQL Server lên DataGridView.
- Thực hiện các thao tác CRUD.
- Quản lý sách.
- Quản lý độc giả.
- Quản lý danh mục.
- Quản lý nhân viên.
- Quản lý mượn và trả sách.
- Thống kê dữ liệu.
- Kiểm tra dữ liệu nhập từ người dùng.
- Xử lý lỗi khi thao tác với cơ sở dữ liệu.
- Tổ chức mã nguồn thành các lớp và Form riêng biệt.
- Làm quen với việc quản lý source code bằng Git và GitHub.

---

# 4. CÔNG NGHỆ VÀ MÔI TRƯỜNG SỬ DỤNG

## 4.1. Phần mềm

Bài thực hành sử dụng các công cụ:

- Microsoft Visual Studio
- Microsoft SQL Server
- SQL Server LocalDB
- Git
- GitHub

## 4.2. Ngôn ngữ và Framework

- Ngôn ngữ: **C#**
- Giao diện: **Windows Forms (WinForms)**
- Framework: **.NET Framework 4.7.2**
- Truy cập dữ liệu: **ADO.NET**
- Namespace truy cập SQL Server:

```csharp
System.Data.SqlClient
```

## 4.3. SQL Server Instance

Ứng dụng sử dụng SQL Server LocalDB:

```text
(LocalDB)\MSSQLLocalDB
```

Tên cơ sở dữ liệu:

```text
QuanLyThuVienDB
```

---

# 5. CẤU TRÚC PROJECT

Project được tổ chức như sau:

```text
QuanLyThuVien
│
├── Properties
│
├── References
│
├── Data
│   ├── Db.cs
│   └── Models.cs
│
├── Forms
│   ├── FrmMain.cs
│   ├── FrmMain.Designer.cs
│   │
│   ├── FrmDanhMuc.cs
│   ├── FrmDanhMuc.Designer.cs
│   │
│   ├── FrmSach.cs
│   ├── FrmSach.Designer.cs
│   │
│   ├── FrmDocGia.cs
│   ├── FrmDocGia.Designer.cs
│   │
│   ├── FrmMuonTra.cs
│   ├── FrmMuonTra.Designer.cs
│   │
│   ├── FrmThongKe.cs
│   └── FrmThongKe.Designer.cs
│
├── Services
│   ├── DocGiaService.cs
│   └── MuonTraService.cs
│
├── App.config
│
└── Program.cs
```

---

# 6. MÔ TẢ CÁC THÀNH PHẦN TRONG PROJECT

## 6.1. Thư mục Data

Thư mục `Data` chứa các lớp liên quan đến dữ liệu.

### Db.cs

`Db.cs` chịu trách nhiệm kết nối ứng dụng với SQL Server.

Lớp cung cấp các phương thức:

```text
OpenConnection()
Query()
Execute()
Scalar()
```

### OpenConnection()

Mở kết nối đến SQL Server dựa trên chuỗi kết nối được khai báo trong `App.config`.

### Query()

Dùng để thực hiện các câu lệnh:

```sql
SELECT
```

Kết quả được trả về dưới dạng:

```csharp
DataTable
```

### Execute()

Dùng để thực hiện:

```sql
INSERT
UPDATE
DELETE
```

Phương thức trả về số dòng bị ảnh hưởng.

### Scalar()

Dùng cho các truy vấn chỉ trả về một giá trị.

Ví dụ:

```sql
SELECT COUNT(*) FROM DauSach
```

---

# 7. CẤU HÌNH KẾT NỐI SQL SERVER

Chuỗi kết nối được đặt trong file:

```text
App.config
```

Nội dung:

```xml
<?xml version="1.0" encoding="utf-8" ?>

<configuration>

    <startup useLegacyV2RuntimeActivationPolicy="true">

        <supportedRuntime
            version="v4.0"
            sku=".NETFramework,Version=v4.7.2" />

    </startup>

    <connectionStrings>

        <add
            name="QuanLyThuVienDb"
            connectionString="Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
            providerName="System.Data.SqlClient" />

    </connectionStrings>

</configuration>
```

Trong `Db.cs`, chương trình lấy chuỗi kết nối bằng:

```csharp
ConfigurationManager
    .ConnectionStrings["QuanLyThuVienDb"]
    .ConnectionString;
```

Tên:

```text
QuanLyThuVienDb
```

phải giống với tên được khai báo trong `App.config`.

---

# 8. THIẾT KẾ CƠ SỞ DỮ LIỆU

Cơ sở dữ liệu của chương trình có tên:

```text
QuanLyThuVienDB
```

Hệ thống gồm các bảng:

1. NhanVien
2. TheLoai
3. NhaXuatBan
4. DauSach
5. DocGia
6. TheDocGia
7. PhieuMuon
8. ChiTietPhieuMuon
9. PhieuPhat

---

# 9. BẢNG NHÂN VIÊN

Tên bảng:

```text
NhanVien
```

Dùng để lưu thông tin nhân viên của thư viện.

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaNhanVien | Mã nhân viên |
| Ho | Họ nhân viên |
| Ten | Tên nhân viên |
| Phai | Giới tính |
| NgaySinh | Ngày sinh |
| ChucVu | Chức vụ |
| SoDienThoai | Số điện thoại |

Khóa chính:

```text
MaNhanVien
```

---

# 10. BẢNG THỂ LOẠI

Tên bảng:

```text
TheLoai
```

Dùng để phân loại sách.

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaTheLoai | Mã thể loại |
| TenTheLoai | Tên thể loại |

Khóa chính:

```text
MaTheLoai
```

Ví dụ dữ liệu:

```text
TL001 - Tin học
TL002 - Tiểu thuyết
TL003 - Anh văn
```

---

# 11. BẢNG NHÀ XUẤT BẢN

Tên bảng:

```text
NhaXuatBan
```

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaNhaXuatBan | Mã nhà xuất bản |
| DiaChi | Địa chỉ |
| SoDienThoai | Số điện thoại |

Khóa chính:

```text
MaNhaXuatBan
```

Một số mã dữ liệu được sử dụng:

```text
NXB001
NXB002
NXB003
NXB004
NXB005
NXB006
```

---

# 12. BẢNG ĐẦU SÁCH

Tên bảng:

```text
DauSach
```

Đây là bảng lưu thông tin sách trong thư viện.

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaDauSach | Mã đầu sách |
| TenSach | Tên sách |
| NamXuatBan | Năm xuất bản |
| SoLuongHienCo | Số lượng hiện có |
| MaTheLoai | Mã thể loại |
| MaNhaXuatBan | Mã nhà xuất bản |

Khóa chính:

```text
MaDauSach
```

Khóa ngoại:

```text
MaTheLoai
MaNhaXuatBan
```

Quan hệ:

```text
DauSach.MaTheLoai
        ↓
TheLoai.MaTheLoai
```

và:

```text
DauSach.MaNhaXuatBan
        ↓
NhaXuatBan.MaNhaXuatBan
```

Số lượng sách không được nhỏ hơn 0.

---

# 13. BẢNG ĐỘC GIẢ

Tên bảng:

```text
DocGia
```

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaDocGia | Mã độc giả |
| Ho | Họ |
| Ten | Tên |
| NgaySinh | Ngày sinh |
| Phai | Giới tính |
| SoDienThoai | Số điện thoại |
| DiaChi | Địa chỉ |
| Email | Email |
| Anh3x4 | Đường dẫn ảnh |

Khóa chính:

```text
MaDocGia
```

---

# 14. BẢNG THẺ ĐỘC GIẢ

Tên bảng:

```text
TheDocGia
```

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaThe | Mã thẻ |
| MaDocGia | Mã độc giả |
| NgayCap | Ngày cấp |
| HanSuDung | Hạn sử dụng |
| DaDongLePhi | Đã đóng lệ phí |
| TrangThai | Trạng thái thẻ |

Khóa chính:

```text
MaThe
```

Khóa ngoại:

```text
MaDocGia
```

liên kết với:

```text
DocGia.MaDocGia
```

Hệ thống có ràng buộc:

```text
HanSuDung >= NgayCap
```

Ngoài ra, một độc giả không được có nhiều hơn một thẻ đang hoạt động.

---

# 15. BẢNG PHIẾU MƯỢN

Tên bảng:

```text
PhieuMuon
```

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaPhieuMuon | Mã phiếu mượn |
| MaDocGia | Người mượn |
| MaNhanVien | Nhân viên lập phiếu |
| NgayMuon | Ngày mượn |
| NgayHenTra | Ngày hẹn trả |

Khóa chính:

```text
MaPhieuMuon
```

Khóa ngoại:

```text
MaDocGia
MaNhanVien
```

Ràng buộc:

```text
NgayHenTra >= NgayMuon
```

---

# 16. BẢNG CHI TIẾT PHIẾU MƯỢN

Tên bảng:

```text
ChiTietPhieuMuon
```

Dùng để lưu từng đầu sách thuộc một phiếu mượn.

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaChiTiet | Mã chi tiết |
| MaPhieuMuon | Mã phiếu mượn |
| MaDauSach | Mã đầu sách |
| NgayTraThucTe | Ngày trả thực tế |
| TinhTrangTra | Tình trạng sách khi trả |

Quan hệ:

```text
ChiTietPhieuMuon
        |
        +---- PhieuMuon
        |
        +---- DauSach
```

---

# 17. BẢNG PHIẾU PHẠT

Tên bảng:

```text
PhieuPhat
```

Các thuộc tính:

| Thuộc tính | Ý nghĩa |
|---|---|
| MaPhieuPhat | Mã phiếu phạt |
| MaChiTiet | Chi tiết phiếu mượn |
| MaNhanVien | Nhân viên lập phiếu |
| NgayPhat | Ngày lập phiếu phạt |
| LyDo | Lý do |
| PhiPhat | Số tiền phạt |

Số tiền phạt phải:

```text
PhiPhat >= 0
```

---

# 18. QUAN HỆ GIỮA CÁC BẢNG

Mô hình quan hệ tổng quát:

```text
TheLoai
   |
   | 1 - n
   |
DauSach
   |
   | 1 - n
   |
ChiTietPhieuMuon
   |
   | n - 1
   |
PhieuMuon
   |
   +------------------+
   |                  |
DocGia             NhanVien
   |
   |
TheDocGia


ChiTietPhieuMuon
       |
       |
   PhieuPhat
       |
       |
    NhanVien


NhaXuatBan
     |
     | 1 - n
     |
   DauSach
```

---

# 19. CÁC FORM CỦA CHƯƠNG TRÌNH

Ứng dụng được chia thành các Form:

```text
FrmMain
FrmDanhMuc
FrmSach
FrmDocGia
FrmMuonTra
FrmThongKe
```

Mỗi Form đảm nhận một chức năng riêng.

---

# 20. FORM CHÍNH - FrmMain

`FrmMain` là giao diện chính của hệ thống.

Từ Form chính người dùng có thể truy cập các chức năng:

```text
Quản lý danh mục
Quản lý sách
Quản lý độc giả
Quản lý mượn trả
Thống kê
```

Mục đích của việc sử dụng Form chính là tập trung các chức năng của hệ thống tại một giao diện duy nhất.

---

# 21. FORM DANH MỤC - FrmDanhMuc

Form:

```text
FrmDanhMuc
```

được sử dụng để hiển thị các dữ liệu danh mục.

Form có 3 DataGridView chính:

```text
dgvTheLoai
dgvNhaXuatBan
dgvNhanVien
```

## 21.1. Danh sách thể loại

Dữ liệu được lấy từ:

```sql
SELECT
    MaTheLoai,
    TenTheLoai
FROM TheLoai
ORDER BY MaTheLoai;
```

## 21.2. Danh sách nhà xuất bản

Dữ liệu được lấy từ bảng:

```text
NhaXuatBan
```

Các thông tin hiển thị:

```text
MaNhaXuatBan
DiaChi
SoDienThoai
```

## 21.3. Danh sách nhân viên

Hiển thị:

```text
MaNhanVien
Ho
Ten
Phai
NgaySinh
ChucVu
SoDienThoai
```

---

# 22. FORM QUẢN LÝ SÁCH - FrmSach

Form:

```text
FrmSach
```

là một trong các Form chính của chương trình.

Các Control chính:

```text
txtMaDauSach
txtTenSach
txtNamXuatBan
txtSoLuong

cboTheLoai
cboNhaXuatBan

btnThem
btnSua
btnXoa
btnDong

dgvSach
```

---

# 23. HIỂN THỊ DANH SÁCH SÁCH

Khi Form được mở, chương trình gọi:

```csharp
FrmSach_Load()
```

Sau đó lần lượt gọi:

```csharp
LoadTheLoai();
LoadNhaXuatBan();
LoadDanhSachSach();
```

Luồng xử lý:

```text
Mở FrmSach
    ↓
FrmSach_Load
    ↓
LoadTheLoai
    ↓
LoadNhaXuatBan
    ↓
LoadDanhSachSach
    ↓
Db.Query()
    ↓
SQL Server
    ↓
DataTable
    ↓
dgvSach
```

Dữ liệu sách được JOIN từ:

```text
DauSach
TheLoai
NhaXuatBan
```

Ví dụ:

```sql
SELECT
    ds.MaDauSach AS [Mã sách],
    ds.TenSach AS [Tên sách],
    ds.NamXuatBan AS [Năm xuất bản],
    ds.SoLuongHienCo AS [Số lượng],
    tl.TenTheLoai AS [Thể loại],
    nxb.MaNhaXuatBan AS [Nhà xuất bản]
FROM dbo.DauSach AS ds
INNER JOIN dbo.TheLoai AS tl
    ON ds.MaTheLoai = tl.MaTheLoai
INNER JOIN dbo.NhaXuatBan AS nxb
    ON ds.MaNhaXuatBan = nxb.MaNhaXuatBan
ORDER BY ds.MaDauSach;
```

---

# 24. CHỨC NĂNG THÊM SÁCH

Người dùng nhập:

```text
Mã đầu sách
Tên sách
Năm xuất bản
Số lượng
Thể loại
Nhà xuất bản
```

Sau đó nhấn:

```text
Thêm
```

Chương trình kiểm tra mã sách đã tồn tại hay chưa bằng:

```sql
SELECT COUNT(*)
FROM DauSach
WHERE MaDauSach = @MaDauSach;
```

Nếu chưa tồn tại, chương trình thực hiện:

```sql
INSERT INTO DauSach
(
    MaDauSach,
    TenSach,
    NamXuatBan,
    SoLuongHienCo,
    MaTheLoai,
    MaNhaXuatBan
)
VALUES
(
    @MaDauSach,
    @TenSach,
    @NamXuatBan,
    @SoLuongHienCo,
    @MaTheLoai,
    @MaNhaXuatBan
);
```

Sau khi thêm thành công, DataGridView được tải lại.

---

# 25. CHỨC NĂNG SỬA SÁCH

Người dùng chọn một sách trên:

```text
dgvSach
```

Thông tin được đưa lên các TextBox và ComboBox.

Sau khi thay đổi thông tin, nhấn:

```text
Sửa
```

Chương trình thực hiện câu lệnh:

```sql
UPDATE DauSach
SET
    TenSach = @TenSach,
    NamXuatBan = @NamXuatBan,
    SoLuongHienCo = @SoLuongHienCo,
    MaTheLoai = @MaTheLoai,
    MaNhaXuatBan = @MaNhaXuatBan
WHERE MaDauSach = @MaDauSach;
```

---

# 26. CHỨC NĂNG XÓA SÁCH

Người dùng chọn sách cần xóa và nhấn:

```text
Xóa
```

Chương trình hiển thị hộp thoại xác nhận.

Nếu người dùng đồng ý:

```sql
DELETE FROM DauSach
WHERE MaDauSach = @MaDauSach;
```

Nếu sách đang được tham chiếu trong `ChiTietPhieuMuon`, SQL Server có thể không cho phép xóa do ràng buộc khóa ngoại.

Chương trình sẽ bắt lỗi và thông báo cho người dùng.

---

# 27. KIỂM TRA DỮ LIỆU SÁCH

Trước khi thêm hoặc sửa, chương trình kiểm tra:

- Mã sách không được để trống.
- Tên sách không được để trống.
- Năm xuất bản phải là số.
- Năm xuất bản phải hợp lệ.
- Số lượng phải là số.
- Số lượng không được nhỏ hơn 0.
- Phải chọn thể loại.
- Phải chọn nhà xuất bản.

Việc kiểm tra giúp hạn chế dữ liệu không hợp lệ được đưa vào cơ sở dữ liệu.

---

# 28. FORM QUẢN LÝ ĐỘC GIẢ - FrmDocGia

Form:

```text
FrmDocGia
```

dùng để quản lý thông tin độc giả.

Các thông tin cần quản lý gồm:

```text
Mã độc giả
Họ
Tên
Ngày sinh
Phái
Số điện thoại
Địa chỉ
Email
Ảnh 3x4
```

Dữ liệu được lưu trong:

```text
DocGia
```

Form cho phép hiển thị danh sách độc giả và thực hiện các thao tác quản lý dữ liệu tương ứng với phần chương trình đã xây dựng.

---

# 29. FORM MƯỢN TRẢ - FrmMuonTra

Form:

```text
FrmMuonTra
```

được sử dụng để quản lý hoạt động mượn và trả sách.

Dữ liệu liên quan đến:

```text
DocGia
NhanVien
DauSach
PhieuMuon
ChiTietPhieuMuon
```

Thông tin phiếu mượn gồm:

```text
Mã phiếu mượn
Mã độc giả
Mã nhân viên
Ngày mượn
Ngày hẹn trả
```

Chi tiết phiếu mượn gồm:

```text
Mã chi tiết
Mã phiếu mượn
Mã đầu sách
Ngày trả thực tế
Tình trạng trả
```

---

# 30. FORM THỐNG KÊ - FrmThongKe

Form:

```text
FrmThongKe
```

được xây dựng để hiển thị các thông tin tổng hợp của hệ thống.

Các dữ liệu thống kê gồm các nội dung chương trình đã xây dựng như:

```text
Số đầu sách
Tổng số sách
Số độc giả
Số nhân viên
Số phiếu mượn
```

Các giá trị tổng hợp có thể được lấy bằng:

```sql
SELECT COUNT(*)
```

hoặc:

```sql
SELECT SUM(...)
```

tùy theo dữ liệu cần thống kê.

---

# 31. CÁCH ỨNG DỤNG KẾT NỐI VỚI SQL SERVER

Luồng hoạt động chung:

```text
Windows Form
      ↓
Sự kiện người dùng
      ↓
FrmSach / FrmDocGia / FrmMuonTra / ...
      ↓
Db.cs
      ↓
SqlConnection
      ↓
SqlCommand
      ↓
SQL Server
      ↓
QuanLyThuVienDB
```

Khi truy vấn dữ liệu:

```text
SQL Server
    ↓
SqlDataAdapter
    ↓
DataTable
    ↓
DataGridView
```

---

# 32. SỬ DỤNG PARAMETER TRONG SQL

Trong chương trình không ghép trực tiếp dữ liệu người dùng vào chuỗi SQL.

Ví dụ:

```csharp
new SqlParameter(
    "@MaDauSach",
    txtMaDauSach.Text.Trim())
```

Câu SQL:

```sql
WHERE MaDauSach = @MaDauSach
```

Cách làm này giúp mã nguồn dễ quản lý hơn và hạn chế lỗi khi truyền dữ liệu vào câu SQL.

---

# 33. KẾT QUẢ ĐẠT ĐƯỢC

Sau quá trình thực hiện, chương trình đã xây dựng được cấu trúc ứng dụng quản lý thư viện gồm nhiều Form và kết nối với SQL Server.

Các kết quả chính:

- Tạo được database `QuanLyThuVienDB`.
- Tạo được 9 bảng dữ liệu.
- Thiết lập khóa chính.
- Thiết lập khóa ngoại.
- Thiết lập các ràng buộc dữ liệu.
- Thêm dữ liệu mẫu.
- Kết nối C# với SQL Server LocalDB.
- Xây dựng lớp `Db`.
- Xây dựng Form chính.
- Xây dựng Form danh mục.
- Xây dựng Form sách.
- Xây dựng Form độc giả.
- Xây dựng Form mượn trả.
- Xây dựng Form thống kê.
- Hiển thị dữ liệu SQL lên DataGridView.
- Đưa dữ liệu SQL lên ComboBox.
- Thực hiện chức năng thêm sách.
- Thực hiện chức năng sửa sách.
- Thực hiện chức năng xóa sách.
- Kiểm tra dữ liệu trước khi lưu.
- Xử lý một số lỗi SQL và WinForms.

---

# 34. CÁC LỖI ĐÃ GẶP TRONG QUÁ TRÌNH THỰC HIỆN

Trong quá trình thực hiện bài, em gặp một số lỗi sau.

---

## 34.1. Không mở được database

Thông báo lỗi:

```text
Cannot open database "QuanLyThuVienDB"
requested by the login.

The login failed.
```

### Nguyên nhân

Ứng dụng kết nối đến:

```text
(LocalDB)\MSSQLLocalDB
```

nhưng database:

```text
QuanLyThuVienDB
```

chưa tồn tại trên SQL Server instance đó.

### Cách khắc phục

Kết nối đúng:

```text
(localdb)\MSSQLLocalDB
```

Sau đó tạo:

```sql
CREATE DATABASE QuanLyThuVienDB;
```

và chạy script tạo bảng.

---

# 35. LỖI InitializeComponent

Thông báo:

```text
CS0103
The name 'InitializeComponent' does not exist in the current context
```

### Nguyên nhân

File:

```text
FrmXXX.Designer.cs
```

bị thiếu hoặc class/namespace không trùng với file chính.

Ví dụ đúng:

```csharp
namespace QuanLyThuVien.Forms
{
    public partial class FrmSach : Form
```

và:

```csharp
namespace QuanLyThuVien.Forms
{
    partial class FrmSach
```

Hai phần sẽ được compiler ghép thành một class thông qua từ khóa:

```text
partial
```

---

# 36. LỖI CS0111 - MEMBER ĐƯỢC KHAI BÁO NHIỀU LẦN

Một số lỗi từng gặp:

```text
Type 'FrmDanhMuc' already defines a member called 'FrmDanhMuc'
```

```text
Type 'FrmSach' already defines a member called 'btnDong_Click'
```

```text
Type 'FrmThongKe' already defines a member with the same parameter types
```

### Nguyên nhân

Code xử lý bị viết cả trong:

```text
FrmXXX.cs
```

và:

```text
FrmXXX.Designer.cs
```

### Cách khắc phục

Phân chia rõ:

```text
FrmSach.cs
```

chứa:

- Constructor
- Xử lý nghiệp vụ
- Sự kiện
- Truy vấn dữ liệu

Còn:

```text
FrmSach.Designer.cs
```

chứa:

- Khai báo Control
- InitializeComponent()
- Thiết kế giao diện

Không viết lại các hàm nghiệp vụ trong Designer.

---

# 37. LỖI CONTROL DOES NOT EXIST IN CURRENT CONTEXT

Ví dụ:

```text
The name 'dgvTheLoai' does not exist in the current context
```

hoặc:

```text
The name 'dgvSach' does not exist in the current context
```

### Nguyên nhân

Control được sử dụng trong file `.cs` nhưng chưa được khai báo trong `.Designer.cs`.

### Cách khắc phục

Ví dụ:

```csharp
private System.Windows.Forms.DataGridView dgvSach;
```

Sau đó tạo trong:

```csharp
InitializeComponent()
```

---

# 38. LỖI EVENT HANDLER KHÔNG TỒN TẠI

Ví dụ:

```text
'FrmMuonTra' does not contain a definition for 'btnTraSach_Click'
```

### Nguyên nhân

Designer có:

```csharp
this.btnTraSach.Click +=
    new System.EventHandler(this.btnTraSach_Click);
```

nhưng file:

```text
FrmMuonTra.cs
```

không còn hàm:

```csharp
btnTraSach_Click
```

### Cách khắc phục

Tạo lại đúng Event Handler hoặc xóa event không còn sử dụng.

---

# 39. LỖI AMBIGUOUS COLUMN NAME

Thông báo:

```text
Ambiguous column name 'MaNhaXuatBan'
```

### Nguyên nhân

`MaNhaXuatBan` xuất hiện trong nhiều bảng của câu truy vấn JOIN.

Ví dụ không nên viết:

```sql
SELECT MaNhaXuatBan
FROM DauSach
INNER JOIN NhaXuatBan
    ON DauSach.MaNhaXuatBan =
       NhaXuatBan.MaNhaXuatBan;
```

### Cách khắc phục

Sử dụng alias:

```sql
SELECT
    nxb.MaNhaXuatBan
FROM DauSach AS ds
INNER JOIN NhaXuatBan AS nxb
    ON ds.MaNhaXuatBan =
       nxb.MaNhaXuatBan;
```

---

# 40. FORM MỞ NHƯNG DATAGRIDVIEW KHÔNG CÓ DỮ LIỆU

Biểu hiện:

- Form mở bình thường.
- DataGridView chỉ hiển thị vùng màu xám.
- Không có dữ liệu.
- ComboBox không có dữ liệu.

### Nguyên nhân

Sự kiện:

```text
FrmSach_Load
```

chưa được gọi.

### Cách khắc phục

Gắn sự kiện:

```csharp
this.Load += FrmSach_Load;
```

Sau đó:

```csharp
private void FrmSach_Load(
    object sender,
    EventArgs e)
{
    LoadTheLoai();
    LoadNhaXuatBan();
    LoadDanhSachSach();
}
```

Sau khi sửa, Form sẽ tự lấy dữ liệu từ SQL Server khi được mở.

---

# 41. LỖI SỰ KIỆN CHẠY HAI LẦN

Trong quá trình sửa code, một Event Handler có thể được gắn trong cả:

```text
FrmSach.cs
```

và:

```text
FrmSach.Designer.cs
```

Điều này có thể khiến một thao tác chạy hai lần.

Ví dụ không nên vừa có:

```csharp
btnThem.Click += btnThem_Click;
```

trong constructor vừa có:

```csharp
this.btnThem.Click +=
    new System.EventHandler(this.btnThem_Click);
```

trong Designer.

### Cách khắc phục

Chỉ gắn sự kiện một lần.

Trong phiên bản hiện tại có thể quản lý các sự kiện ở constructor của Form và không gắn lại trong Designer.

---

# 42. HƯỚNG DẪN CHẠY LẠI BÀI

Để giảng viên hoặc người khác có thể chạy lại project, thực hiện theo các bước sau.

## Bước 1: Clone hoặc tải project

Clone repository:

```bash
git clone <đường-dẫn-repository>
```

Hoặc tải file ZIP từ GitHub và giải nén.

---

## Bước 2: Mở SQL Server

Kết nối tới:

```text
(LocalDB)\MSSQLLocalDB
```

---

## Bước 3: Tạo database

Chạy file SQL của project.

File SQL sẽ tạo database:

```text
QuanLyThuVienDB
```

và các bảng cần thiết.

---

## Bước 4: Kiểm tra database

Kiểm tra SQL Server phải có:

```text
QuanLyThuVienDB
│
└── Tables
    ├── NhanVien
    ├── TheLoai
    ├── NhaXuatBan
    ├── DauSach
    ├── DocGia
    ├── TheDocGia
    ├── PhieuMuon
    ├── ChiTietPhieuMuon
    └── PhieuPhat
```

---

## Bước 5: Kiểm tra App.config

Kiểm tra:

```xml
name="QuanLyThuVienDb"
```

và:

```text
Data Source=(LocalDB)\MSSQLLocalDB
```

Database:

```text
Initial Catalog=QuanLyThuVienDB
```

---

## Bước 6: Mở Solution

Mở file:

```text
QuanLyThuVien.sln
```

bằng Visual Studio.

---

## Bước 7: Kiểm tra References

Project cần sử dụng:

```text
System
System.Data
System.Configuration
System.Windows.Forms
```

Nếu `ConfigurationManager` báo lỗi, cần kiểm tra Reference:

```text
System.Configuration
```

---

## Bước 8: Build project

Trong Visual Studio:

```text
Build
→ Clean Solution
```

sau đó:

```text
Build
→ Rebuild Solution
```

Kiểm tra:

```text
0 Error
```

---

## Bước 9: Chạy chương trình

Nhấn:

```text
F5
```

hoặc:

```text
Start
```

---

# 43. CÁCH KIỂM TRA CÁC CHỨC NĂNG

## Kiểm tra quản lý sách

Mở:

```text
Quản lý sách
```

Kiểm tra:

- Danh sách sách có hiển thị hay không.
- ComboBox thể loại có dữ liệu hay không.
- ComboBox nhà xuất bản có dữ liệu hay không.

Sau đó thử:

```text
Thêm
Sửa
Xóa
```

---

## Kiểm tra danh mục

Mở:

```text
Quản lý danh mục
```

Kiểm tra dữ liệu:

```text
Thể loại
Nhà xuất bản
Nhân viên
```

---

## Kiểm tra độc giả

Mở:

```text
Quản lý độc giả
```

Kiểm tra danh sách độc giả và các chức năng đã triển khai trên Form.

---

## Kiểm tra mượn trả

Mở:

```text
Quản lý mượn trả
```

Kiểm tra dữ liệu:

```text
Độc giả
Sách
Phiếu mượn
Chi tiết phiếu mượn
```

---

## Kiểm tra thống kê

Mở:

```text
Thống kê
```

Kiểm tra các số liệu thống kê được lấy từ cơ sở dữ liệu.

---


# 47. KẾT LUẬN

Qua bài thực hành, em đã xây dựng được một ứng dụng quản lý thư viện sử dụng C# Windows Forms và SQL Server.

Hệ thống được tổ chức thành nhiều thành phần khác nhau gồm lớp truy cập dữ liệu, các Form giao diện và các lớp xử lý nghiệp vụ. Cơ sở dữ liệu được thiết kế với các bảng có quan hệ khóa chính và khóa ngoại để quản lý sách, độc giả, nhân viên, thẻ độc giả, phiếu mượn, chi tiết phiếu mượn và phiếu phạt.

Trong quá trình thực hiện, em đã thực hành được cách kết nối ứng dụng C# với SQL Server bằng ADO.NET, sử dụng `SqlConnection`, `SqlCommand`, `SqlDataAdapter`, `DataTable` và `SqlParameter`.

Em cũng thực hiện được việc đưa dữ liệu từ SQL Server lên `DataGridView` và `ComboBox`, đồng thời xây dựng các chức năng thêm, sửa, xóa và kiểm tra dữ liệu.

Bên cạnh đó, quá trình xây dựng chương trình phát sinh nhiều lỗi liên quan đến `Designer.cs`, `InitializeComponent`, tên Control, Event Handler, kết nối LocalDB và câu truy vấn SQL. Việc xác định nguyên nhân và sửa các lỗi này giúp em hiểu rõ hơn về cấu trúc của ứng dụng Windows Forms cũng như quá trình tương tác giữa chương trình C# và cơ sở dữ liệu SQL Server.

Kết quả của bài thực hành là cơ sở để tiếp tục hoàn thiện các chức năng quản lý thư viện và áp dụng mô hình lập trình hướng đối tượng vào các ứng dụng quản lý thực tế.
