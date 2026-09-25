<img width="1830" height="641" alt="image" src="https://github.com/user-attachments/assets/df9b2b13-a147-44f3-8935-fb292eeb892e" /># LAB_OOSD

# LAB 3 - HỆ THỐNG QUẢN LÝ KHÁCH SẠN

## 1. Thông tin sinh viên

- Họ và tên: Nguyễn Nguyên Hậu
- MSSV: 1250080049
- Tên bài Lab: LAB 3 - Hệ thống quản lý khách sạn

## 2. Môi trường và phiên bản sử dụng

- Hệ điều hành: Windows
- IDE: Visual Studio
- Ngôn ngữ lập trình: C#
- Framework: .NET 10.0
- Loại ứng dụng: Windows Forms (WinForms)
- Hệ quản trị cơ sở dữ liệu: Microsoft SQL Server
- SQL Server Instance: `.\SQLEXPRESS`
- Database: `QuanLyKhachSan`
- Thư viện kết nối SQL Server: `Microsoft.Data.SqlClient`


## 3. Nội dung đã thực hiện

Trong LAB 3, chương trình được xây dựng dưới dạng ứng dụng Windows Forms nhằm hỗ trợ quản lý các nghiệp vụ cơ bản của khách sạn.

Các chức năng chính của chương trình gồm:

### 3.1. Danh mục

Quản lý các dữ liệu danh mục phục vụ cho hệ thống như:

- Khu vực
- Khách hàng
- Nhân viên
- Dịch vụ

Các chức năng cơ bản:

- Thêm dữ liệu
- Sửa dữ liệu
- Xóa dữ liệu
- Hiển thị danh sách
- Tìm kiếm dữ liệu

### 3.2. Phòng - Tiện nghi

Quản lý thông tin phòng và tiện nghi của khách sạn.

Thông tin phòng gồm:

- Số phòng
- Khu vực
- Số người tối đa
- Đơn giá ngày
- Trạng thái phòng

Trạng thái phòng được quản lý để hỗ trợ quá trình đặt, nhận và trả phòng.

### 3.3. Đặt / Nhận phòng

Thực hiện nghiệp vụ đặt và nhận phòng cho khách hàng.

Chức năng bao gồm:

- Chọn khách hàng
- Chọn nhân viên lễ tân
- Chọn phòng
- Nhập ngày nhận phòng
- Nhập ngày trả dự kiến
- Nhập tiền cọc
- Chọn kênh đặt phòng
- Ghi nhận số người
- Theo dõi trạng thái đặt phòng
- Lưu thông tin người lưu trú

Dữ liệu đặt phòng được liên kết giữa các bảng như `PhieuDatPhong`, `ChiTietDatPhong`, `Phong`, `KhachHang` và `NguoiLuuTru`.

### 3.4. Sử dụng dịch vụ

Quản lý các dịch vụ khách sử dụng trong thời gian lưu trú.

Chức năng bao gồm:

- Chọn phòng/phiếu đặt phòng
- Chọn dịch vụ
- Nhập số lượng
- Ghi nhận ngày sử dụng
- Lưu đơn giá
- Tính tiền dịch vụ

### 3.5. Trả phòng - Thanh toán

Thực hiện nghiệp vụ trả phòng và thanh toán cho khách.

Chức năng bao gồm:

- Xác định phòng cần trả
- Kiểm tra thông tin đặt phòng
- Tổng hợp tiền phòng
- Tổng hợp tiền dịch vụ
- Lập hóa đơn
- Thanh toán
- Cập nhật trạng thái phòng sau khi khách trả phòng

### 3.6. Thống kê

Thống kê và tổng hợp dữ liệu từ hệ thống quản lý khách sạn.

Các thông tin thống kê có thể bao gồm:

- Số lượng phòng
- Trạng thái phòng
- Số lượt đặt phòng
- Thông tin khách lưu trú
- Doanh thu
- Thông tin hóa đơn và thanh toán

---

## 4. Kết quả đạt được

Sau khi thực hiện LAB 3, chương trình đã xây dựng được giao diện quản lý khách sạn bằng Windows Forms và kết nối với cơ sở dữ liệu SQL Server.

Chương trình tổ chức các chức năng chính thành 6 nhóm:

1. Danh mục
2. Phòng - Tiện nghi
3. Đặt / Nhận phòng
4. Sử dụng dịch vụ
5. Trả phòng - Thanh toán
6. Thống kê

Các Form có thể trao đổi dữ liệu với database `QuanLyKhachSan` và sử dụng các khóa chính, khóa ngoại để đảm bảo mối quan hệ giữa các bảng.

---

## 5. Lỗi gặp phải

### 5.1. Lỗi kết nối SQL Server

Lỗi đã gặp:

`Cannot open database "QuanLyKhachSan" requested by the login.`

Nguyên nhân:

- Chương trình sử dụng sai phương thức đăng nhập SQL Server.
- Tài khoản kết nối chưa có quyền truy cập database.
- Connection String chưa đúng.

Cách khắc phục:

- Kiểm tra lại SQL Server Instance.
- Kiểm tra database `QuanLyKhachSan`.
- Kiểm tra tài khoản đăng nhập SQL Server.
- Cấu hình lại Connection String.
- Sử dụng `TrustServerCertificate=True` khi cần thiết.

### 5.2. Lỗi sai tên cột trong database

Một số lỗi gặp phải:

`Invalid column name`

Nguyên nhân:

Tên cột được sử dụng trong câu lệnh SQL không trùng với tên cột thực tế trong database.

Cách khắc phục:

- Kiểm tra cấu trúc bảng trong SQL Server.
- Sử dụng đúng tên bảng và tên cột.
- Đồng bộ câu lệnh SQL trong chương trình với database.

### 5.3. Lỗi Windows Forms Designer

Trong quá trình xây dựng giao diện đã gặp lỗi do khai báo trùng:

- `InitializeComponent()`
- Control
- Biến thành viên của Form

Nguyên nhân:

Code giao diện trong file `.Designer.cs` bị khai báo lại trong file `.cs`.

Cách khắc phục:

Tách rõ hai phần:

- `FrmXXX.cs`: xử lý sự kiện và nghiệp vụ.
- `FrmXXX.Designer.cs`: khai báo và khởi tạo giao diện.

Class của Form được khai báo bằng từ khóa `partial`.

### 5.4. Lỗi thư viện SQL Client

Chương trình cần thư viện SQL Client phù hợp với phiên bản .NET đang sử dụng.

Cách khắc phục:

Cài package:

`Microsoft.Data.SqlClient`

thông qua NuGet Package Manager của Visual Studio.

---

## 6. Hướng dẫn cài đặt và chạy chương trình

### Bước 1: Chuẩn bị SQL Server

Cài đặt Microsoft SQL Server và SQL Server Management Studio hoặc sử dụng SQL Server Object Explorer trong Visual Studio.

SQL Server Instance sử dụng:

`.\SQLEXPRESS`

### Bước 2: Tạo cơ sở dữ liệu

Tạo hoặc khôi phục database:

`QuanLyKhachSan`

Đảm bảo các bảng cần thiết đã được tạo đầy đủ và các quan hệ khóa ngoại hoạt động bình thường.

### Bước 3: Cấu hình kết nối

Mở phần cấu hình kết nối database trong project và kiểm tra Connection String.

Ví dụ:

`Server=.\SQLEXPRESS;Database=QuanLyKhachSan;User Id=sa;Password=<MAT_KHAU>;TrustServerCertificate=True;`

Thay `<MAT_KHAU>` bằng mật khẩu SQL Server tương ứng trên máy chạy chương trình.

### Bước 4: Cài NuGet Package

Trong Visual Studio:

`Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution`

Tìm và cài:

`Microsoft.Data.SqlClient`

### Bước 5: Build chương trình

Mở Solution của LAB 3 trong Visual Studio.

Chọn:

`Build -> Build Solution`

hoặc nhấn:

`Ctrl + Shift + B`

Đảm bảo chương trình không còn lỗi build.

### Bước 6: Chạy chương trình

Nhấn:

`F5`

hoặc chọn:

`Debug -> Start Debugging`

Màn hình chính **Hệ thống quản lý khách sạn** sẽ xuất hiện.

---

## 7. Hướng dẫn kiểm tra chức năng

Giảng viên có thể kiểm tra chương trình theo thứ tự:

1. Mở chương trình.
2. Kiểm tra chức năng **Danh mục**.
3. Kiểm tra **Phòng - Tiện nghi**.
4. Tạo một phiếu **Đặt / Nhận phòng**.
5. Ghi nhận **Sử dụng dịch vụ** cho phòng.
6. Thực hiện **Trả phòng - Thanh toán**.
7. Kiểm tra dữ liệu tại chức năng **Thống kê**.
8. Kiểm tra dữ liệu trực tiếp trong SQL Server để xác nhận dữ liệu đã được lưu.

---

## 8. Cấu trúc chương trình

Cấu trúc project chính:

    QuanLyKhachSan/
    │
    ├── Data/
    │   └── Db.cs
    │
    ├── Forms/
    │   ├── FrmMain.cs
    │   ├── FrmKhuVuc.cs
    │   ├── FrmPhong.cs
    │   ├── FrmKhachHang.cs
    │   ├── FrmNhanVien.cs
    │   ├── FrmDatPhong.cs
    │   ├── FrmNhanPhong.cs
    │   ├── FrmSuDungDichVu.cs
    │   ├── FrmTraPhong.cs
    │   ├── FrmThanhToan.cs
    │   └── FrmThongKe.cs
    │
    ├── Program.cs
    └── QuanLyKhachSan.csproj

---

## 9. Ghi chú

- SQL Server phải được khởi động trước khi chạy chương trình.
- Database `QuanLyKhachSan` phải tồn tại.
- Cần chỉnh lại tài khoản/mật khẩu trong Connection String nếu chạy chương trình trên máy khác.
- Không xóa hoặc đổi tên các bảng/cột trong database nếu chưa cập nhật lại code C# tương ứng.

---

## 10. Kết luận

LAB 3 đã thực hiện việc xây dựng ứng dụng quản lý khách sạn bằng C# Windows Forms kết hợp SQL Server.

Qua bài Lab, các nội dung chính được thực hành gồm thiết kế giao diện Windows Forms, kết nối cơ sở dữ liệu, thực hiện các thao tác thêm/sửa/xóa/tìm kiếm dữ liệu, xử lý quan hệ giữa nhiều bảng và xây dựng các nghiệp vụ cơ bản của hệ thống quản lý khách sạn.
