# Bài Lab 5 Quản lý công ty du lịch

## Thông tin sinh viên

- **Họ và tên:** Nguyễn Nguyên Hậu
- **MSSV:** 1250080049
- **Tên bài Lab:** Bài 5 – Quản lý công ty du lịch Văn Hóa Việt
- **Môn học:** Thực hành Phân tích thiết kế hướng đối tượng

## Môi trường và phiên bản

| Thành phần | Phiên bản hoặc cấu hình |
| --- | --- |
| Hệ điều hành | Windows |
| Ngôn ngữ và giao diện | C#, Windows Forms |
| .NET SDK đã dùng để build | 10.0.401 |
| Target framework | `net10.0-windows` |
| SQL Server LocalDB đã kiểm tra | 17.0.4025.3, instance `MSSQLLocalDB` |
| Thư viện truy cập SQL Server | `Microsoft.Data.SqlClient` 7.1.1 |
| Database | `QuanLyCongTyDuLich` |
| Xác thực | Windows Authentication (`Integrated Security=True`) |

Khi chạy project này cần môi trường hỗ trợ .NET 10. Có thể dùng CLI bên dưới hoặc Visual Studio hỗ trợ .NET 10 với workload **.NET desktop development**. Cần mạng để tải các gói NuGet trong lần restore đầu tiên.

## Nội dung đã thực hiện

- Hoàn thiện 9 form và các file `Designer.cs`, bố trí theo các hình giao diện trong tài liệu Lab.
- Bổ sung control, giới hạn nhập số và sự kiện Load, Click, thay đổi lựa chọn và thay đổi giá trị.
- Chuyển màn hình khởi động từ `Form1` sang `FrmMain`.
- Bổ sung `FormHelper` để nạp ComboBox, lấy giá trị được chọn, lấy dữ liệu dòng và thông báo kết quả.
- Kết nối các form với Service và lớp truy cập CSDL `Db`; form không chứa câu lệnh SQL.
- Dùng script `QuanLyCongTyDuLich/Data/QuanLyCongTyDuLich.sql` để tạo 16 bảng, ràng buộc và dữ liệu mẫu.
- Sử dụng `Microsoft.Data.SqlClient` và cấu hình kết nối LocalDB trong `App.config`.
- Bổ sung chế độ kiểm tra `--check-forms` để mở các form/tab, lưu ảnh giao diện và kiểm tra kết quả lương, thống kê bằng truy vấn đọc dữ liệu.

Các màn hình gồm: danh mục; tour và hành trình; lịch chuyến khách lẻ; đăng ký khách lẻ; đăng ký đoàn; phân công hướng dẫn viên; kết thúc tour và khảo sát; lương và thống kê; màn hình chính.

## Kết quả kiểm tra

- Build thành công: **0 lỗi, 0 cảnh báo**.
- Kết nối thành công đến `QuanLyCongTyDuLich` trên `(localdb)\MSSQLLocalDB`.
- CSDL có **16 bảng**, **3 tour mẫu** cùng các dữ liệu liên quan trong script.
- Mở thành công cả **9 form** và **17 màn hình/tab** trong chế độ kiểm tra.
- Hai nút **Tính lương** và **Thống kê** đã được thực thi trong chế độ kiểm tra.
- Với dữ liệu mẫu tháng 9/2026: HDV01 có tổng lương **10.500.000 đồng**; HDV02 **11.500.000 đồng**; HDV03 **10.500.000 đồng**.
- Báo cáo tổng hợp trả về **5 chỉ số**.

**Phạm vi kiểm tra:** đã kiểm tra build, kết nối, tải giao diện/dữ liệu và kết quả lương, thống kê nêu trên. Chưa thực hiện đầy đủ 24 test case nghiệp vụ trong tài liệu Lab; không xem kết quả trên là xác nhận toàn bộ chức năng ghi dữ liệu đã được kiểm thử.

## Lỗi gặp phải và cách khắc phục

| Lỗi hoặc vấn đề | Cách khắc phục |
| --- | --- |
| Các form gọi `InitializeComponent()` nhưng chưa có giao diện, control và sự kiện | Bổ sung các file `Designer.cs`, khai báo control và gắn sự kiện cho 9 form. |
| Thiếu lớp `FormHelper` | Bổ sung các hàm `Nap`, `Gia`, `O`, `Bao`. |
| `Program.cs` gọi `Form1` không tồn tại | Đổi sang khởi chạy `FrmMain`. |
| Code SQL dùng `System.Data.SqlClient`, không phù hợp với cấu hình thư viện của project .NET 10 | Thêm gói `Microsoft.Data.SqlClient` 7.1.1 và cập nhật các tham chiếu, bao gồm kiểu `SqlParameter`. |
| `App.config` còn cấu hình khởi động .NET Framework 4.7.2 | Bỏ phần `supportedRuntime` cũ và giữ chuỗi kết nối cho project .NET 10. |
| LocalDB chưa có database của bài Lab | Chạy script SQL có sẵn trong project để tạo database và dữ liệu mẫu. |
| Tạo filtered index báo lỗi 1934 vì `QUOTED_IDENTIFIER` | Thêm `SET QUOTED_IDENTIFIER ON`, `SET ANSI_NULLS ON` đầu script; khi dùng `sqlcmd` thêm tùy chọn `-I`. |
| Control và bảng dữ liệu bị tràn do thứ tự khởi tạo kích thước, Anchor và tự co giãn theo font | Khởi tạo kích thước form trước khi thêm control, dùng `AutoScaleMode.Dpi`, chỉnh lại vị trí và nhãn theo mẫu. |

## Hướng dẫn giảng viên chạy lại

Thực hiện trong **PowerShell**, tại thư mục chứa `QuanLyCongTyDuLich.slnx` và file README này. Cần cài .NET SDK 10, SQL Server LocalDB và `sqlcmd` (hoặc dùng SQL Server Management Studio để chạy script).

### 1. Kiểm tra môi trường

```powershell
dotnet --version
SqlLocalDB info
```

Nếu lệnh `SqlLocalDB` hoặc `sqlcmd` không được nhận diện, bổ sung thư mục công cụ vào PATH hoặc gọi bằng đường dẫn tuyệt đối. Trên máy đã kiểm tra, các đường dẫn là:

```text
C:\Program Files\Microsoft SQL Server\170\Tools\Binn\SqlLocalDB.exe
C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE
```

### 2. Khởi động LocalDB và khởi tạo dữ liệu

```powershell
SqlLocalDB start MSSQLLocalDB
sqlcmd -S '(localdb)\MSSQLLocalDB' -E -I -b -i '.\QuanLyCongTyDuLich\Data\QuanLyCongTyDuLich.sql'
```

Nếu instance chưa tồn tại, chạy `SqlLocalDB create MSSQLLocalDB` trước khi khởi động.

**Lưu ý về script:** script có các lệnh `DROP TABLE` để tạo lại dữ liệu của bài Lab. Chạy lại sẽ xóa dữ liệu hiện có trong các bảng của database này. Chỉ chạy khi cần khởi tạo/reset dữ liệu Lab; sao lưu nếu cần giữ dữ liệu đã nhập. Không cần chạy lại script mỗi lần mở ứng dụng.

Kiểm tra sau khi tạo:

```powershell
sqlcmd -S '(localdb)\MSSQLLocalDB' -E -d QuanLyCongTyDuLich -b -Q 'SELECT COUNT(*) AS SoBang FROM sys.tables; SELECT COUNT(*) AS SoTour FROM Tour;'
```

Kết quả với script mẫu: `SoBang = 16`, `SoTour = 3`.

### 3. Kiểm tra cấu hình kết nối

File `QuanLyCongTyDuLich/App.config` chứa kết nối tên `QuanLyCongTyDuLichDB`:

```text
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyCongTyDuLich;Integrated Security=True;Encrypt=True;TrustServerCertificate=True
```

Nếu dùng instance SQL Server khác, sửa `Data Source` và thông tin xác thực cho phù hợp. `TrustServerCertificate=True` được sử dụng cho môi trường LocalDB của bài thực hành.

### 4. Restore, build và chạy

```powershell
dotnet restore '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj'
dotnet build '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj' --no-restore
dotnet run --project '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj' --no-build
```

Ứng dụng mở màn hình chính. Chọn các nút chức năng để mở form tương ứng. Có thể mở `QuanLyCongTyDuLich.slnx` trong Visual Studio hỗ trợ .NET 10, chọn project khởi động rồi nhấn **F5**; xem giao diện bằng **View Designer** trên các file `Forms/Frm*.cs`.

### 5. Chạy kiểm tra form và đối chiếu kết quả

Sau khi build, chạy:

```powershell
dotnet run --project '.\QuanLyCongTyDuLich\QuanLyCongTyDuLich.csproj' --no-build -- --check-forms '.\KiemTraForms'
Get-Content '.\KiemTraForms\forms.log'
```

Chế độ này mở rồi đóng từng form/tab, lưu ảnh PNG và `forms.log` vào thư mục được chỉ định. Kiểm tra log có kết nối CSDL, đủ 9 form và dòng `Salary sample and 5 statistics indicators: passed`. Phép kiểm tra lương dùng dữ liệu mẫu nguyên trạng; nếu đã thêm HDV/phân công hoặc thay đổi lương, cần đối chiếu lại kết quả theo dữ liệu mới.

Để kiểm tra thủ công, mở **Lương - thống kê**, chọn tháng **9**, năm **2026**, nhấn **Tính lương** và đối chiếu ba mức lương ở phần kết quả. Trong tab **Thống kê tổng hợp**, chọn khoảng **01/01/2026–01/10/2026** và nhấn **Thống kê**.

Các chức năng đăng ký, đặt cọc, phân công, hủy đoàn, thanh toán và khảo sát cần kiểm tra tiếp bằng 24 test case trong tài liệu Lab. Dữ liệu mẫu có mốc năm 2026/2027; khi chạy vào thời điểm khác, các điều kiện dùng ngày hiện tại có thể khiến ca kiểm thử trước/sau chuyến cho kết quả khác. Hãy điều chỉnh dữ liệu ngày tương ứng trước khi kiểm tra các ca đó.

## Cấu trúc chính

```text
QuanLyCongTyDuLich.slnx
README.md
QuanLyCongTyDuLich/
  QuanLyCongTyDuLich.csproj
  Program.cs
  App.config
  FormDiagnostics.cs
  Data/
    DB.cs
    QuanLyCongTyDuLich.sql
  Services/
    ... các lớp xử lý nghiệp vụ
  Forms/
    FrmMain.cs và FrmMain.Designer.cs
    ... các form chức năng và Designer.cs
    FormHelper.cs
```
