# LAB 3 - HỆ THỐNG QUẢN LÝ KHÁCH SẠN

## 1. Thông tin sinh viên

- Họ và tên: Nguyễn Nguyên Hậu
- MSSV: 1250080049
- Tên bài Lab: LAB 3 - Hệ thống quản lý khách sạn
- Môn học: Phương pháp phát triển phần mềm hướng đối tượng

---

## 2. Môi trường và công nghệ sử dụng

- Hệ điều hành: Windows
- IDE: Visual Studio 2026
- Ngôn ngữ lập trình: C#
- Framework: .NET 10.0
- Giao diện: Windows Forms (WinForms)
- Hệ quản trị cơ sở dữ liệu: Microsoft SQL Server
- SQL Server Instance: SQLEXPRESS
- Database: QuanLyKhachSan
- Thư viện kết nối CSDL: Microsoft.Data.SqlClient

---

## 3. Mô tả bài toán

LAB 3 xây dựng chương trình quản lý khách sạn bằng C# WinForms kết hợp
SQL Server.

Hệ thống hỗ trợ quản lý các thông tin cơ bản của khách sạn như phòng,
khách hàng, nhân viên, tiện nghi và dịch vụ.

Ngoài ra, chương trình thực hiện các nghiệp vụ chính gồm đặt phòng,
nhận phòng, sử dụng dịch vụ, trả phòng, lập hóa đơn, thanh toán và
thống kê.

---

## 4. Nội dung đã thực hiện

### 4.1. Thiết kế cơ sở dữ liệu

Đã xây dựng cơ sở dữ liệu `QuanLyKhachSan` trên SQL Server.

Các bảng chính gồm:

- KhuVuc
- Phong
- LoaiTienNghi
- TienNghi
- LapDatTienNghi
- KhachHang
- NhanVien
- PhieuDatPhong
- ChiTietDatPhong
- NguoiLuuTru
- DichVu
- PhieuSuDungDV
- ChiTietSuDungDV
- KiemTraPhong
- ChiTietKiemTra
- PhieuDenBu
- ChiTietDenBu
- HoaDon
- ThanhToan

Các bảng được thiết lập khóa chính, khóa ngoại và các ràng buộc cần
thiết nhằm đảm bảo tính toàn vẹn của dữ liệu.

---

### 4.2. Xây dựng giao diện chương trình

Chương trình được xây dựng bằng Windows Forms và chia thành các chức
năng chính:

1. Danh mục
2. Phòng - Tiện nghi
3. Đặt / Nhận phòng
4. Sử dụng dịch vụ
5. Trả phòng - Thanh toán
6. Thống kê

Form chính `FrmMain` được sử dụng để truy cập các chức năng của hệ thống.

---

### 4.3. Quản lý danh mục

Form `FrmDanhMuc` hỗ trợ quản lý:

- Khu vực
- Khách hàng
- Nhân viên
- Dịch vụ

Các chức năng cơ bản:

- Hiển thị danh sách
- Thêm dữ liệu
- Sửa dữ liệu
- Xóa dữ liệu
- Làm mới dữ liệu

---

### 4.4. Quản lý phòng và tiện nghi

Form `FrmPhongTienNghi` hỗ trợ:

- Quản lý danh sách phòng
- Quản lý loại phòng
- Quản lý trạng thái phòng
- Quản lý tiện nghi
- Theo dõi tiện nghi được lắp đặt tại từng phòng
- Ghi nhận ngày lắp đặt và ngày tháo dỡ tiện nghi

Các trạng thái phòng gồm:

- Trống
- Đã đặt
- Đang ở
- Bảo trì

---

### 4.5. Đặt và nhận phòng

Form `FrmDatNhanPhong` hỗ trợ:

- Tạo phiếu đặt phòng
- Chọn khách hàng
- Chọn nhân viên lễ tân
- Chọn ngày nhận và ngày trả dự kiến
- Ghi nhận tiền cọc
- Chọn kênh đặt phòng
- Thêm phòng vào phiếu đặt
- Ghi nhận số người lưu trú
- Thực hiện nhận phòng
- Cập nhật trạng thái phòng

Hệ thống có kiểm tra:

- Ngày trả phải lớn hơn ngày nhận
- Số người không được vượt quá sức chứa của phòng
- Phòng không được bị trùng lịch với phiếu đặt khác
- Một phòng không được thêm trùng vào cùng một phiếu đặt

---

### 4.6. Sử dụng dịch vụ

Form `FrmSuDungDichVu` hỗ trợ ghi nhận các dịch vụ khách sử dụng trong
thời gian lưu trú.

Các thông tin quản lý gồm:

- Phiếu đặt phòng
- Phòng
- Ngày sử dụng
- Nhân viên thực hiện
- Dịch vụ
- Số lượng
- Đơn giá
- Thành tiền

Khi cùng một dịch vụ được sử dụng nhiều lần trong cùng ngày tại cùng
phòng, hệ thống thực hiện cộng dồn số lượng thay vì tạo dữ liệu trùng.

---

### 4.7. Trả phòng và thanh toán

Form `FrmTraPhongThanhToan` hỗ trợ:

- Xác định phiếu đang lưu trú
- Tính tiền phòng
- Tính tiền dịch vụ
- Tính tiền đền bù
- Trừ tiền cọc
- Lập hóa đơn
- Ghi nhận thanh toán
- Hoàn tất trả phòng

Công thức tổng tiền:

TongThanhToan = TienPhong + TienDichVu + TienDenBu - TienCoc

Hệ thống hỗ trợ các phương thức thanh toán:

- Tiền mặt
- Chuyển khoản
- Thẻ

Một hóa đơn có thể được thanh toán nhiều lần cho đến khi đủ tổng số
tiền cần thanh toán.

Sau khi thanh toán đầy đủ và trả phòng:

- Phiếu đặt phòng chuyển sang trạng thái `Đã trả`.
- Ngày trả thực tế được cập nhật.
- Phòng được chuyển về trạng thái `Trống` nếu phòng không ở trạng thái
  bảo trì.

---

### 4.8. Thống kê

Form `FrmThongKe` hỗ trợ hiển thị một số thông tin tổng hợp:

- Tổng số phòng
- Số phòng trống
- Số phiếu/khách đang ở
- Doanh thu theo khoảng thời gian
- Danh sách hóa đơn
- Trạng thái hóa đơn

Người dùng có thể chọn khoảng thời gian từ ngày - đến ngày để xem dữ
liệu thống kê.

---

### 4.9. Stored Procedure và xử lý nghiệp vụ

Một số Stored Procedure được sử dụng để xử lý các nghiệp vụ quan trọng:

#### sp_KiemTraPhongTrong

Kiểm tra phòng có bị trùng lịch đặt trong khoảng thời gian yêu cầu hay
không.

#### sp_ThemPhongVaoPhieuDat

Thêm phòng vào phiếu đặt và kiểm tra:

- Phiếu đặt có tồn tại hay không
- Phòng có tồn tại hay không
- Sức chứa tối đa của phòng
- Trùng lịch đặt phòng
- Phòng đã tồn tại trong phiếu hay chưa

#### sp_ThemDichVu

Thêm dịch vụ cho khách đang lưu trú.

Nếu dịch vụ đã tồn tại trong cùng ngày thì hệ thống cộng thêm số lượng.

#### sp_ThanhToanHoaDon

Thực hiện thanh toán hóa đơn và kiểm tra:

- Hóa đơn có tồn tại
- Số tiền thanh toán hợp lệ
- Phương thức thanh toán hợp lệ
- Không cho thanh toán vượt quá số tiền còn lại
- Tự động cập nhật trạng thái hóa đơn

---

## 5. Kết quả đạt được

Sau khi thực hiện LAB 3, chương trình đã xây dựng được giao diện quản
lý khách sạn và kết nối với cơ sở dữ liệu SQL Server.

Các chức năng chính của hệ thống được tổ chức thành các module riêng
biệt, giúp việc quản lý và kiểm tra chương trình thuận tiện hơn.

Hệ thống xử lý được các nghiệp vụ chính:

- Quản lý danh mục
- Quản lý phòng và tiện nghi
- Đặt phòng
- Kiểm tra trùng lịch phòng
- Kiểm tra sức chứa phòng
- Nhận phòng
- Quản lý người lưu trú
- Ghi nhận dịch vụ
- Cộng dồn dịch vụ
- Tính chi phí lưu trú
- Lập hóa đơn
- Thanh toán nhiều lần
- Trả phòng
- Thống kê dữ liệu

---

## 6. Một số lỗi gặp phải

### 6.1. Lỗi kết nối SQL Server

Lỗi gặp phải:

`Login failed for user`

hoặc lỗi xảy ra trong quá trình đăng nhập SQL Server.

Nguyên nhân:

- Sai Server/Instance.
- Sai tài khoản hoặc mật khẩu SQL Server.
- SQL Server Authentication chưa được bật.
- Tài khoản chưa có quyền truy cập database.

Cách khắc phục:

- Kiểm tra SQL Server Instance.
- Sử dụng đúng `.\SQLEXPRESS`.
- Kiểm tra tài khoản SQL Server.
- Bật SQL Server and Windows Authentication Mode nếu sử dụng SQL
  Authentication.
- Kiểm tra lại Connection String.

---

### 6.2. Lỗi thiếu Microsoft.Data.SqlClient

Nguyên nhân:

Project chưa cài thư viện kết nối SQL Server phù hợp.

Cách khắc phục:

Cài package:

`Microsoft.Data.SqlClient`

thông qua NuGet Package Manager.

---

### 6.3. Lỗi sai tên bảng hoặc tên cột

Nguyên nhân:

Tên bảng/cột trong code C# không trùng với cấu trúc database.

Ví dụ hệ thống sử dụng:

- SoPhieuDat
- MaKhach
- MaNVLeTan
- SoPhong
- ChiTietSuDungDV

Cách khắc phục:

Kiểm tra lại cấu trúc bảng trong SQL Server và sửa câu lệnh SQL trong
code C# cho đúng với tên bảng và tên cột thực tế.

---

### 6.4. Lỗi trùng dữ liệu

Một số trường được thiết lập PRIMARY KEY hoặc UNIQUE nên không thể thêm
hai dữ liệu giống nhau.

Cách khắc phục:

- Kiểm tra dữ liệu trước khi thêm.
- Không chạy script dữ liệu mẫu nhiều lần.
- Sử dụng mã khác khi tạo dữ liệu mới.

---

### 6.5. Lỗi đặt phòng trùng lịch

Một phòng có thể đã được sử dụng trong khoảng thời gian khách muốn đặt.

Cách khắc phục:

Sử dụng Stored Procedure `sp_ThemPhongVaoPhieuDat` để kiểm tra khoảng
thời gian đặt trước khi thêm phòng vào phiếu.

---

### 6.6. Lỗi ràng buộc khóa ngoại khi xóa

Một số dữ liệu không thể xóa do đã được sử dụng trong bảng khác.

Ví dụ:

- Phòng đã có phiếu đặt.
- Khách hàng đã có phiếu đặt.
- Dịch vụ đã phát sinh trong phiếu sử dụng dịch vụ.
- Tiện nghi đã được lắp đặt hoặc kiểm tra.

Cách khắc phục:

Không xóa trực tiếp dữ liệu đang được tham chiếu. Kiểm tra dữ liệu liên
quan trước khi thực hiện thao tác xóa.

---

## 7. Hướng dẫn chạy chương trình

### Bước 1: Chuẩn bị SQL Server

Cài đặt SQL Server và đảm bảo SQL Server Express đang hoạt động.

Instance sử dụng trong project:

`.\SQLEXPRESS`

---

### Bước 2: Tạo cơ sở dữ liệu

Mở SQL Server Management Studio và chạy file/script SQL của LAB 3 để
tạo database:

`QuanLyKhachSan`

Sau đó tạo các bảng, ràng buộc, Stored Procedure và dữ liệu mẫu.

---

### Bước 3: Kiểm tra Connection String

Mở file:

`Data/Db.cs`

Kiểm tra Connection String.

Ví dụ sử dụng SQL Server Authentication:

Server=.\SQLEXPRESS;
Database=QuanLyKhachSan;
User Id=sa;
Password=MAT_KHAU;
Encrypt=True;
TrustServerCertificate=True;

Thay `MAT_KHAU` bằng mật khẩu SQL Server trên máy chạy chương trình.

---

### Bước 4: Mở project

Mở project bằng Visual Studio 2026.

Đảm bảo project sử dụng:

`.NET 10.0`

Nếu thiếu package SQL Server, cài:

`Microsoft.Data.SqlClient`

---

### Bước 5: Build project

Chọn:

`Build -> Build Solution`

hoặc sử dụng:

`Ctrl + Shift + B`

Đảm bảo project không còn lỗi biên dịch.

---

### Bước 6: Chạy chương trình

Nhấn:

`F5`

hoặc chọn:

`Start`

Chương trình sẽ mở `FrmMain`.

Từ màn hình chính có thể kiểm tra lần lượt:

1. Danh mục
2. Phòng - Tiện nghi
3. Đặt / Nhận phòng
4. Sử dụng dịch vụ
5. Trả phòng - Thanh toán
6. Thống kê

---

## 8. Quy trình kiểm tra nghiệp vụ đề xuất

Giảng viên có thể kiểm tra chương trình theo quy trình:

Tạo/kiểm tra khách hàng
→ Tạo phiếu đặt phòng
→ Thêm phòng vào phiếu
→ Kiểm tra sức chứa và trùng lịch
→ Nhận phòng
→ Thêm người lưu trú
→ Ghi nhận dịch vụ
→ Trả phòng
→ Tính tiền
→ Lập hóa đơn
→ Thanh toán
→ Hoàn tất trả phòng
→ Kiểm tra thống kê

---

## 9. Cấu trúc project

QuanLyKhachSan/
│
├── Data/
│   └── Db.cs
│
├── Forms/
│   ├── FrmMain.cs
│   ├── FrmDanhMuc.cs
│   ├── FrmPhongTienNghi.cs
│   ├── FrmDatNhanPhong.cs
│   ├── FrmSuDungDichVu.cs
│   ├── FrmTraPhongThanhToan.cs
│   └── FrmThongKe.cs
│
├── Program.cs
├── QuanLyKhachSan.csproj
└── README.md

---

## 10. Kết luận

LAB 3 đã xây dựng hệ thống quản lý khách sạn bằng C# WinForms kết hợp
SQL Server.

Thông qua bài Lab, sinh viên thực hành được việc thiết kế cơ sở dữ liệu,
kết nối C# với SQL Server, xây dựng giao diện WinForms, thực hiện các
thao tác CRUD và xử lý các nghiệp vụ có liên quan đến nhiều bảng dữ
liệu.

Hệ thống có thể tiếp tục phát triển thêm các chức năng như tìm kiếm nâng
cao, phân quyền tài khoản, xuất hóa đơn, báo cáo doanh thu và cải thiện
giao diện người dùng.
