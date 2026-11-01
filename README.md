# LAB6 – Quản lý hồ sơ bệnh án 

Bài thực hành phân tích, thiết kế hướng đối tượng và xây dựng ứng dụng C# Windows Forms kết nối SQL Server, dựa trên đề **Quản lý hồ sơ bệnh án của bệnh nhân**.

## 1. Thông tin sinh viên

| Nội dung | Thông tin |
|---|---|
| Họ và tên | Nguyễn Nguyên Hậu |
| MSSV | 1250080049 |
| Lớp | 12_ĐH_CNPM1 |
| Bài thực hành | LAB6 |
| Nhánh mã nguồn | `LAB6` |

## 2. Mục tiêu

- Phân tích bệnh nhân, hồ sơ bệnh án, lệ phí năm, lịch hẹn, khám bệnh, xét nghiệm, toa thuốc và thanh toán.
- Xác định lớp, thuộc tính định danh, liên kết, bản số, Actor và Use Case.
- Thiết kế Class Diagram, Use Case, Activity và Sequence Diagram.
- Xây dựng WinForms theo các lớp giao diện, nghiệp vụ và truy cập dữ liệu.
- Áp dụng khóa chính, khóa ngoại, UNIQUE và transaction cho dữ liệu nhiều bảng.

## 3. Chức năng

| Module | Chức năng chính |
|---|---|
| Đăng ký và hồ sơ | Nhập thông tin bệnh nhân, chọn ảnh, thu lệ phí và cấp hồ sơ |
| Lệ phí năm | Gia hạn và xem lịch sử đóng lệ phí |
| Tra cứu | Tìm hồ sơ theo mã, họ tên hoặc điện thoại; xem lịch sử khám |
| Lịch hẹn | Đặt, xem và hủy lịch; kiểm tra bác sĩ trùng lịch theo phút |
| Khám bệnh | Lập phiếu khám điều trị hoặc khám định kỳ theo quý |
| Xét nghiệm | Chỉ định xét nghiệm, ghi nhận và xem kết quả |
| Toa thuốc | Nhập kết luận, soạn dòng thuốc, lưu, xem và xuất toa TXT |
| Thanh toán | Tính tiền khám cộng xét nghiệm và hoàn tất phiếu |
| Danh mục | Tìm kiếm, thêm, sửa, xóa khoa, bác sĩ và thuốc |
| Thống kê | Xem số lượt khám và tiền đã thu theo hồ sơ |

Các quy tắc chính:

- Đăng ký lần đầu ghi bệnh nhân, hồ sơ và lệ phí trong cùng transaction.
- Khám định kỳ miễn tiền khám khi lệ phí còn hiệu lực; mỗi hồ sơ có tối đa một lần định kỳ trong một quý.
- Phiếu khám lưu khoa điều trị tại thời điểm khám, giữ được lịch sử khi bác sĩ chuyển khoa.
- Phải có đủ kết quả xét nghiệm trước khi lưu kết luận/toa thuốc.
- Có thể hoàn tất khám không dùng thuốc; trường hợp này không tạo toa rỗng.
- Phiếu chỉ chuyển sang **Đã thanh toán** sau khi ghi nhận thanh toán thành công.
- Không xóa danh mục đang được lịch hẹn, phiếu khám hoặc toa thuốc tham chiếu.

## 4. Công nghệ

| Thành phần | Công nghệ |
|---|---|
| Ngôn ngữ | C# |
| Giao diện | Windows Forms |
| Nền tảng | .NET 10, `net10.0-windows` |
| Cơ sở dữ liệu | SQL Server / SQL Server LocalDB |
| Thư viện kết nối | `Microsoft.Data.SqlClient` 7.1.1 |
| Cấu hình kết nối | `App.config` |

## 5. Cấu trúc thư mục

```text
LAB_OOSD/
├── README.md
└── LAB6/
    └── QuanLyHoSoBenhAn/
        ├── QuanLyHoSoBenhAn.slnx
        └── QuanLyHoSoBenhAn/
            ├── Forms/                  # Các Form và file Designer.cs
            ├── Services/               # BenhAnService, DanhMucService, Models
            ├── Data/
            │   ├── DB.cs               # Kết nối, truy vấn, tham số SQL
            │   └── QuanLyHoSoBenhAn.sql # SQL tổng hợp của LAB6
            ├── App.config
            ├── Program.cs
            ├── Verification.cs         # Kiểm thử nghiệp vụ
            ├── VerificationCleanup.cs  # Dọn dữ liệu của lần kiểm thử
            ├── DanhMucVerification.cs  # Kiểm thử danh mục
            └── QuanLyHoSoBenhAn.csproj
```

Code nằm trong `LAB6`; SQL tổng hợp nằm trong thư mục `Data` của dự án, README đặt ở thư mục gốc. Các tài liệu Word và draw.io được bàn giao riêng.

## 6. Cài đặt và chạy

### Bước 1 – Lấy đúng nhánh LAB6

```powershell
git clone --branch LAB6 --single-branch https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD.git
cd LAB_OOSD
```

### Bước 2 – Khởi tạo cơ sở dữ liệu

Cần Windows, .NET 10 SDK và SQL Server LocalDB hoặc SQL Server. Dùng SSMS mở **[QuanLyHoSoBenhAn.sql](LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn/Data/QuanLyHoSoBenhAn.sql)** rồi chạy toàn bộ tệp.

Tệp SQL duy nhất gồm:

1. Tạo hoặc nâng cấp CSDL `QuanLyHoSoBenhAnLAB6`, các bảng, khóa và ràng buộc.
2. Nạp dữ liệu trình diễn; có thể chạy lại mà không tạo trùng bộ dữ liệu.
3. Dọn hồ sơ kiểm thử cũ theo công tắc `@DonDuLieuKiemThu`, mặc định `0` là **không dọn**.
4. Hiển thị số lượng bản ghi sau khi khởi tạo.

Nếu có `sqlcmd`, có thể chạy từ thư mục gốc:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -C -b -f 65001 -i "LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn/Data/QuanLyHoSoBenhAn.sql"
```

Khi muốn dọn hồ sơ kiểm thử cũ, đổi `@DonDuLieuKiemThu = 0` thành `1` ở phần 3. Phần này xóa hồ sơ có tên bắt đầu bằng `LAB6_TEST_` và dữ liệu phụ thuộc, không phải chức năng xóa bệnh nhân thông thường.

### Bước 3 – Kiểm tra cấu hình

Mở `LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn/App.config`:

```xml
<add name="QuanLyHoSoBenhAnDB"
     connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyHoSoBenhAnLAB6;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
     providerName="Microsoft.Data.SqlClient" />
```

Nếu dùng SQL Server khác, sửa `Data Source` theo tên instance đang sử dụng. Tài khoản kết nối cần quyền truy cập CSDL.

### Bước 4 – Build và chạy

Mở solution bằng Visual Studio hỗ trợ .NET 10, restore NuGet rồi chạy dự án. Hoặc dùng PowerShell:

```powershell
dotnet restore "LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn.slnx"
dotnet build "LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn.slnx" -c Release
dotnet run --project "LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn.csproj" -c Release
```

## 7. Dữ liệu trình diễn và luồng sử dụng

Trên CSDL mới, script tạo bộ dữ liệu:

| Dữ liệu | Số lượng |
|---|---:|
| Bệnh nhân / hồ sơ | 12 / 12 |
| Phiếu khám | 18 |
| Xét nghiệm | 27 |
| Toa thuốc | 12 |
| Thanh toán | 10 |

Bộ dữ liệu có lệ phí còn hạn/hết hạn, lịch hẹn và phiếu khám ở nhiều trạng thái. Ngày khám trình diễn lấy mốc 16/10/2026; lịch hẹn dựa trên ngày chạy script lần đầu. Số lượng trong CSDL đang sử dụng có thể cao hơn khi thêm dữ liệu mới.

Luồng chính:

```text
Đăng ký và đóng lệ phí → Cấp hồ sơ → Đặt lịch hẹn nếu cần
→ Lập phiếu khám → Chỉ định và cập nhật xét nghiệm nếu cần
→ Lưu kết luận/toa thuốc → Thanh toán → Tra cứu lịch sử
```

Để chỉnh khoa, bác sĩ hoặc thuốc, chọn **Danh mục** ở màn hình chính. Chọn loại danh mục, tìm/chọn dòng để sửa hoặc nhấn **Thêm mới**, sau đó **Lưu**.

## 8. Kiểm thử

Chạy sau khi khởi tạo CSDL và cấu hình kết nối:

```powershell
# Kiểm thử nghiệp vụ
dotnet run --project "LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn.csproj" -c Release -- --verify

# Kiểm thử danh mục
dotnet run --project "LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn.csproj" -c Release -- --verify-catalog
```

Hai chế độ kiểm tra đăng ký/thu phí, khám theo quý, lịch hẹn, xét nghiệm, toa thuốc, thanh toán, rollback và CRUD danh mục. Chúng tạo dữ liệu tạm và có bước dọn dữ liệu kiểm thử sau khi chạy.

Nhật ký nằm trong `bin/Release/net10.0-windows/Verification/` của dự án:

- `results.txt`: kết quả kiểm thử nghiệp vụ.
- `catalogue-results.txt`: kết quả kiểm thử danh mục.

## 9. Phạm vi bài lab

Ứng dụng phục vụ mô phỏng nghiệp vụ và học tập. Chưa triển khai đăng nhập/phân quyền, lịch khám theo khoảng thời lượng, quản lý kho/cấp phát thuốc hoặc phản ứng phụ có cấu trúc. Ảnh bệnh nhân là tùy chọn trong phiên bản này. Thông tin thuốc, liều dùng và mức phí trong dữ liệu trình diễn không dùng cho điều trị thực tế.
