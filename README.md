# LAB_OOSD – Tổng hợp các bài thực hành hướng đối tượng

## 1. Thông tin sinh viên

- **Họ và tên:** Nguyễn Nguyên Hậu
- **MSSV:** 1250080049
- **Lớp:** 12_ĐH_CNPM1
- **Nội dung:** Tổng hợp các bài thực hành lập trình, phân tích và thiết kế hướng đối tượng.

## 2. Giới thiệu

Repository lưu các bài Lab từ mô hình lớp và kế thừa trong Java đến các ứng dụng quản lý bằng C# Windows Forms kết nối SQL Server. Mỗi bài được lưu trên một nhánh riêng; nhánh `main` chứa README tổng để tra cứu nội dung và hướng dẫn kiểm tra.

## 3. Danh sách các bài Lab

| Bài Lab | Tên bài | Nội dung chính | Mã nguồn | README chi tiết |
| --- | --- | --- | --- | --- |
| Lab 1 | Các lớp hình học | Điểm, tam giác, tứ giác, ellipse; lớp trừu tượng, kế thừa và đa hình | [LAB1](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB1/LAB1/bai1) | [README Lab 1](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB1/README.md) |
| Lab 2 | Quản lý thư viện | Sách, độc giả, danh mục, mượn trả, phiếu phạt và thống kê | [LAB2](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB2/lab2) | [README Lab 2](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB2/README.md) |
| Lab 3 | Quản lý khách sạn | Phòng, tiện nghi, đặt/nhận phòng, dịch vụ, trả phòng và thanh toán | [LAB3](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB3/lab3) | [README Lab 3](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB3/README.md) |
| Lab 4 | Cửa hàng online e-SHOPPING | Sản phẩm, giỏ hàng, đăng nhập, đặt hàng, thanh toán và email giả lập | [LAB4](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB4/lab4) | [README Lab 4](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB4/README.md) |
| Lab 5 | Quản lý công ty du lịch | Tour, hành trình, khách lẻ/đoàn, phân công HDV, khảo sát, lương và thống kê | [LAB5](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB5/LAB5) | [README Lab 5](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB5/README.md) |

## 4. Môi trường và phiên bản

| Bài Lab | Ngôn ngữ / nền tảng | Cơ sở dữ liệu và thư viện |
| --- | --- | --- |
| Lab 1 | Java, chương trình console; cần JDK có `javac` và `java` | Không dùng CSDL; repository chưa ghi phiên bản JDK |
| Lab 2 | C#, WinForms, .NET Framework 4.7.2 | SQL Server LocalDB; `System.Data.SqlClient`; database `QuanLyThuVienDB` |
| Lab 3 | C#, WinForms, .NET 10; Visual Studio 2026 theo README | SQL Server Express; `Microsoft.Data.SqlClient` 7.1.0; database `QuanLyKhachSan` |
| Lab 4 | C#, WinForms, `net10.0-windows`; SDK 10.0.401 | LocalDB 17.0.4025.3; `Microsoft.Data.SqlClient` 7.0.3; database `EShopping_Prototype` |
| Lab 5 | C#, WinForms, `net10.0-windows`; SDK 10.0.401 | LocalDB 17.0.4025.3; `Microsoft.Data.SqlClient` 7.1.1; database `QuanLyCongTyDuLich` |

Các bài WinForms chạy trên Windows. Với Lab 2, cài .NET Framework 4.7.2 Developer Pack và workload **.NET desktop development**. Với Lab 3–5, dùng .NET 10 SDK và Visual Studio hỗ trợ .NET 10. Có thể dùng SSMS để chạy script và kiểm tra dữ liệu SQL Server.

## 5. Nội dung đã thực hiện

### 5.1. Lab 1 – Các lớp hình học

- Xây dựng các lớp `CDiem`, `CHinhVe`, `CTamGiac`, `CTuGiac`, `CEllipse`.
- Sử dụng lớp trừu tượng và kế thừa để định nghĩa các loại hình.
- Nhập tọa độ, bán trục; tính chu vi và diện tích.
- Sử dụng kiểu `CHinhVe` để gọi các phương thức của từng loại hình qua đa hình.
- `Main.java` cung cấp chương trình console nhập và in kết quả của ba hình.

### 5.2. Lab 2 – Quản lý thư viện

- Thiết kế CSDL gồm 9 bảng, khóa chính, khóa ngoại và dữ liệu mẫu.
- Xây dựng các form danh mục, sách, độc giả, mượn trả và thống kê.
- Kết nối SQL Server bằng lớp `Db`, hiển thị dữ liệu trên DataGridView.
- Xử lý thêm, sửa, xóa và kiểm tra dữ liệu nhập; sử dụng tham số SQL.

### 5.3. Lab 3 – Quản lý khách sạn

- Quản lý danh mục, phòng, tiện nghi và người lưu trú.
- Đặt/nhận phòng, kiểm tra trùng lịch và sức chứa.
- Ghi nhận dịch vụ, tính tiền phòng, lập hóa đơn, thanh toán và trả phòng.
- Dùng stored procedure cho các nghiệp vụ đặt phòng, dịch vụ và thanh toán.
- Thống kê doanh thu, hóa đơn và tình trạng phòng.

### 5.4. Lab 4 – Cửa hàng online e-SHOPPING

- Xây dựng 8 màn hình WinForms và danh mục 20 sản phẩm thuộc 4 nhóm.
- Đăng ký, đăng nhập, quản lý giỏ hàng và lập đơn hàng.
- Tính phí giao hàng, thanh toán giả lập, xử lý timeout/đối soát và email giả lập.
- Lưu dữ liệu vào 3 bảng và dùng view cho danh mục sản phẩm.
- Kèm báo cáo, sơ đồ và chức năng kiểm tra `--verify`.

### 5.5. Lab 5 – Quản lý công ty du lịch

- Hoàn thiện 9 form và các file `Designer.cs` theo giao diện tài liệu Lab.
- Quản lý danh mục, tour/hành trình, chuyến khách lẻ và đăng ký khách lẻ/đoàn.
- Phân công HDV, kết thúc tour, khảo sát, tính lương và thống kê.
- Kết nối các form với Service và lớp `Db`; script tạo 16 bảng và dữ liệu mẫu.
- Bổ sung chế độ `--check-forms` để kiểm tra giao diện và kết quả truy vấn.

## 6. Kết quả

| Bài Lab | Kết quả được mô tả trong mã nguồn / README của bài |
| --- | --- |
| Lab 1 | Có chương trình nhập ba loại hình và in chu vi, diện tích; thao tác `ve()` in thông báo trong console. |
| Lab 2 | Xây dựng CSDL 9 bảng, các form quản lý thư viện và kết nối LocalDB. |
| Lab 3 | Xây dựng các module quản lý khách sạn, kết nối SQL Server và xử lý các nghiệp vụ đặt phòng, dịch vụ, thanh toán. |
| Lab 4 | README ghi build Release 0 lỗi, 0 cảnh báo; `Verification.txt` ghi 18/18 kiểm tra đạt. |
| Lab 5 | README ghi build 0 lỗi, 0 cảnh báo; mở 9 form/17 màn hình hoặc tab; kiểm tra lương mẫu và 5 chỉ số thống kê. |

Kết quả trên tổng hợp từ từng bài. Lab 5 đã kiểm tra giao diện, kết nối, lương và thống kê; chưa thực hiện đầy đủ 24 test case nghiệp vụ trong tài liệu.

## 7. Lỗi gặp phải và cách khắc phục

| Bài / tình huống | Cách khắc phục |
| --- | --- |
| Lab 2: không mở được database | Khởi động LocalDB, chạy script tạo CSDL và kiểm tra kết nối trong `App.config`. |
| Lab 2: thiếu `InitializeComponent`, control hoặc event handler | Kiểm tra cặp file `.cs`/`.Designer.cs`, khai báo control và tên hàm xử lý sự kiện. |
| Lab 3: lỗi đăng nhập SQL Server | Sửa server/instance và thông tin xác thực trong `Data/Db.cs` theo máy chạy; kiểm tra quyền truy cập CSDL. |
| Lab 3–5: thiếu `Microsoft.Data.SqlClient` | Restore NuGet trước khi build, kiểm tra phiên bản gói trong `.csproj`. |
| Lab 3: đặt phòng trùng lịch hoặc xóa dữ liệu có khóa ngoại | Kiểm tra lịch phòng và các bản ghi liên quan trước khi thao tác. |
| Lab 4: script tiếng Việt bị sai ký tự | Mở script UTF-8 trong SSMS hoặc dùng `sqlcmd -f 65001`. |
| Lab 4: không kết nối được LocalDB | Khởi động instance và kiểm tra `Connection.txt`. |
| Lab 5: thiếu form khởi động hoặc lớp hỗ trợ | Dùng `FrmMain`, bổ sung `FormHelper` và các file Designer. |
| Lab 5: filtered index báo lỗi `QUOTED_IDENTIFIER` | Dùng `SET QUOTED_IDENTIFIER ON`, `SET ANSI_NULLS ON`; khi chạy `sqlcmd` thêm `-I`. |

## 8. Hướng dẫn giảng viên kiểm tra và chạy lại

### 8.1. Lấy đúng bài Lab

Clone repository và chuyển sang nhánh tương ứng. Ví dụ:

```powershell
git clone https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD.git
cd LAB_OOSD
git switch LAB1
```

Thay `LAB1` bằng `LAB2`, `LAB3`, `LAB4` hoặc `LAB5` để xem bài khác. Trước khi chuyển nhánh, lưu các thay đổi đang thực hiện. Có thể chọn nhánh trên GitHub rồi chọn **Code → Download ZIP**.

### 8.2. Lab 1

Trong nhánh `LAB1`, chạy:

```powershell
cd LAB1/bai1
javac -encoding UTF-8 Main.java
java Main
```

Biên dịch riêng `Main.java` vì file này đã chứa các lớp hỗ trợ cùng tên với các file Java khác. Nhập tọa độ tam giác, tứ giác và bán trục ellipse theo lời nhắc; đối chiếu chu vi, diện tích được in ra.

### 8.3. Lab 2

1. Chuyển sang nhánh `LAB2`.
2. Chạy `lab2/QuanLyThuVienDB.sql` trên SQL Server LocalDB.
3. Kiểm tra chuỗi kết nối trong `lab2/QuanLyThuVien/QuanLyThuVien/App.config`.
4. Mở `lab2/QuanLyThuVien/QuanLyThuVien.slnx`, build rồi nhấn **F5**.
5. Kiểm tra danh mục, sách, độc giả, mượn trả và thống kê theo README Lab 2.

### 8.4. Lab 3

1. Chuyển sang nhánh `LAB3`.
2. Chạy `lab3/QuanLyKhachSan/QuanLyKhachSanDB.sql` trên SQL Server.
3. Sửa chuỗi kết nối trong `lab3/QuanLyKhachSan/QuanLyKhachSan/Data/Db.cs` theo instance và thông tin xác thực của máy kiểm tra.
4. Mở `lab3/QuanLyKhachSan/QuanLyKhachSan.slnx`, restore NuGet, build rồi nhấn **F5**.
5. Thử quy trình đặt phòng → nhận phòng → dùng dịch vụ → trả phòng/thanh toán; kiểm tra nhánh trùng lịch và báo cáo.

### 8.5. Lab 4

1. Chuyển sang nhánh `LAB4`, mở `lab4/Shopping.slnx`.
2. Khởi động LocalDB và kiểm tra `Shopping/Connection.txt` trong thư mục `lab4`.
3. Restore, build Release rồi chạy ứng dụng; chương trình tự khởi tạo CSDL.
4. Đăng nhập `demo` / `Demo@123`, kiểm tra 4 nhóm sản phẩm, giỏ hàng, đặt hàng và các kịch bản thanh toán giả lập.
5. Chạy lại kiểm tra từ thư mục `lab4` sau khi build Release:

```powershell
.\Shopping\bin\Release\net10.0-windows\Shopping.exe --verify verification-moi.txt
```

Lệnh kiểm tra tạo thêm đơn demo. Xem README Lab 4 để đối chiếu kết quả và kiểm tra email trong thư mục `Outbox` cạnh EXE.

### 8.6. Lab 5

Chuyển sang nhánh `LAB5`, mở PowerShell tại `LAB5/QuanLyCongTyDuLich`:

```powershell
SqlLocalDB start MSSQLLocalDB
sqlcmd -S '(localdb)\MSSQLLocalDB' -E -I -b -f 65001 -i '.\QuanLyCongTyDuLich\Data\QuanLyCongTyDuLich.sql'
dotnet restore '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj'
dotnet build '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj' --no-restore
dotnet run --project '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj' --no-build
```

Script Lab 5 tạo lại các bảng và dữ liệu mẫu, nên chỉ chạy khi cần khởi tạo/reset CSDL. Nếu instance chưa có, chạy `SqlLocalDB create MSSQLLocalDB` trước khi khởi động.

Kiểm tra tự động các form:

```powershell
dotnet run --project '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj' --no-build -- --check-forms '.\KiemTraForms'
Get-Content '.\KiemTraForms\forms.log'
```

Đối chiếu đủ 9 form, 17 màn hình/tab và kết quả lương mẫu tháng 9/2026: HDV01 **10.500.000 đồng**, HDV02 **11.500.000 đồng**, HDV03 **10.500.000 đồng**. Tham khảo README Lab 5 để kiểm tra thống kê và các điều kiện ngày của tour.
