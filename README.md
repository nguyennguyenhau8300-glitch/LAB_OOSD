# LAB8 – Quản lý cho thuê văn phòng cao ốc

## Thông tin sinh viên

- **Họ và tên:** Nguyễn Nguyên Hậu
- **MSSV:** 1250080049
- **Lớp:** 12_ĐH_CNPM1
- **Bài thực hành:** Phân tích, thiết kế và hiện thực hệ thống quản lý cho thuê văn phòng cao ốc.

## Giới thiệu

Bài làm tổ chức nghiệp vụ thành **6 nhóm chức năng chính**, từ quản lý tầng và văn phòng đến ký hợp đồng, thanh toán, gia hạn, quản lý nhân viên công ty thuê, thẻ giữ xe và kiểm tra phòng.

Chương trình dùng C# WinForms **.NET 10**, kết nối SQL Server và tổ chức mã nguồn theo `Forms → Services → Data`, tiếp nối cách triển khai LAB4–LAB7.

Màn hình chính có sáu nút xếp hai hàng, mỗi hàng ba nút và nút Thoát phía dưới. Mỗi nút mở một Form nghiệp vụ riêng; giao diện dùng nền trắng, chữ đen.

## Chức năng và các Form

| Nhóm | Chức năng chính | Form | Các màn hình con |
| --- | --- | --- | --- |
| CN01 | Tầng và văn phòng | `FrmVanPhong` | Danh mục tầng, văn phòng, tra cứu lịch trống và lịch sử thuê |
| CN02 | Khách hàng và hợp đồng | `FrmHopDong` | Khách hàng, ký hợp đồng nhiều phòng, xem lịch thu, hủy chờ trả trước và kết thúc hợp đồng |
| CN03 | Thanh toán và dịch vụ | `FrmThanhToan` | Khoản phải thu, hóa đơn dịch vụ, phiếu thu, phân bổ thu và danh mục dịch vụ |
| CN04 | Gia hạn hợp đồng | `FrmGiaHan` | Phòng thuê, kiểm tra công nợ, lập phiếu gia hạn và lịch sử gia hạn |
| CN05 | Nhân viên và thẻ giữ xe | `FrmNhanVienTheXe` | Hồ sơ nhân viên công ty thuê, ảnh, phòng làm việc, xe, cấp và thu hồi thẻ |
| CN06 | Kiểm tra và xử phạt | `FrmKiemTra` | Phòng đến hạn 12 tháng, biên bản kiểm tra, phiếu phạt và ghi nhận tiền nộp |

Có **15 màn hình con** trong sáu module. `FrmEditor` hỗ trợ nhập/sửa dữ liệu và các dòng chi tiết; `FrmTable` hiển thị lịch sử hoặc thông tin chỉ đọc.

Tổng cộng **9 Form**, gồm `FrmMain`, sáu Form nghiệp vụ và hai Form dùng chung. Mỗi Form có cặp `.cs` và `.Designer.cs`.

## Môi trường và cấu trúc mã nguồn

| Thành phần | Phiên bản / cấu hình |
| --- | --- |
| Nền tảng | Windows, C# WinForms, `net10.0-windows` |
| Công cụ | .NET 10 SDK; Visual Studio 2026 và workload **.NET desktop development** |
| CSDL | SQL Server / SQL Server LocalDB, database `QuanLyCaoOcLAB8` |
| Thư viện kết nối | `Microsoft.Data.SqlClient` 7.1.1 |
| Cấu hình | Chuỗi kết nối `CaoOcDB` trong `App.config`; mặc định `(localdb)\MSSQLLocalDB`, xác thực Windows |

```text
LAB8/
├── README.md
└── QuanLyCaoOc/
    ├── QuanLyCaoOc.sln
    └── QuanLyCaoOc/
        ├── QuanLyCaoOc.csproj
        ├── Program.cs
        ├── App.config
        ├── Verification.cs
        ├── Forms/
        │   ├── FrmMain.cs / FrmMain.Designer.cs
        │   ├── FrmVanPhong.cs / FrmVanPhong.Designer.cs
        │   ├── FrmHopDong.cs / FrmHopDong.Designer.cs
        │   ├── FrmThanhToan.cs / FrmThanhToan.Designer.cs
        │   ├── FrmGiaHan.cs / FrmGiaHan.Designer.cs
        │   ├── FrmNhanVienTheXe.cs / FrmNhanVienTheXe.Designer.cs
        │   ├── FrmKiemTra.cs / FrmKiemTra.Designer.cs
        │   ├── FrmEditor.cs / FrmEditor.Designer.cs
        │   ├── FrmTable.cs / FrmTable.Designer.cs
        │   ├── CatalogEditor.cs
        │   └── FormHelper.cs
        ├── Services/
        │   ├── Models.cs
        │   ├── Store.cs
        │   ├── DanhMucService.cs
        │   ├── HopDongService.cs
        │   ├── ThanhToanService.cs
        │   ├── GiaHanService.cs
        │   ├── AnNinhService.cs
        │   └── KiemTraService.cs
        └── Data/
            ├── Db.cs
            └── QuanLyCaoOc.sql
```

`FrmMain` mở các Form bằng `ShowDialog`. Các Service kiểm tra nghiệp vụ và thực hiện thao tác ghi; lớp `Db` quản lý kết nối và giao dịch SQL. `Reports` cung cấp dữ liệu danh sách, lịch sử và công nợ. Giá trị nhập được truyền bằng tham số SQL.

Báo cáo Word và tài liệu mô hình đặt ngoài thư mục `LAB8`. File SQL nằm trong `Data/QuanLyCaoOc.sql`.

## Cài đặt và chạy

Cài .NET 10 SDK, SQL Server Express LocalDB và workload **.NET desktop development**.

Tại thư mục `LAB8`, chạy:

```powershell
SqlLocalDB start MSSQLLocalDB

dotnet restore ".\QuanLyCaoOc\QuanLyCaoOc.sln"

dotnet build ".\QuanLyCaoOc\QuanLyCaoOc.sln" -c Release

dotnet run --project ".\QuanLyCaoOc\QuanLyCaoOc\QuanLyCaoOc.csproj" -c Release
```

Có thể mở solution bằng Visual Studio 2026 và nhấn **F5**. Lần khởi động đầu, chương trình tự tạo database theo cấu hình, thực thi SQL và nạp dữ liệu mẫu.

Nếu dùng SQL Server khác, sửa `connectionStrings/CaoOcDB` trong `App.config` trước khi build:

```xml
<connectionStrings>
  <add name="CaoOcDB"
       connectionString="Server=(localdb)\MSSQLLocalDB;Database=QuanLyCaoOcLAB8;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connect Timeout=15" />
</connectionStrings>
```

Để chạy SQL thủ công bằng SSMS, tạo và chọn database `QuanLyCaoOcLAB8`, sau đó thực thi toàn bộ file `Data/QuanLyCaoOc.sql`. Script không chạy trong database hệ thống.

Tệp SQL gồm lược đồ, khóa/ràng buộc, chỉ mục, view công nợ và dữ liệu mẫu. Seed được đánh dấu bằng `LAB8_DemoSeed`, không đặt lại số dư mỗi lần mở chương trình.

## CSDL và dữ liệu trình diễn

CSDL có **19 bảng**, gồm các nhóm danh mục, hợp đồng, gia hạn, công nợ, thu tiền, dịch vụ, nhân viên công ty thuê, xe/thẻ, kiểm tra và xử phạt.

Bộ mẫu ban đầu gồm:

| Dữ liệu | Số lượng |
| --- | ---: |
| Tầng / văn phòng | 3 / 9 |
| Khách hàng công ty | 3 |
| Hợp đồng / chi tiết phòng thuê | 3 / 4 |
| Danh mục dịch vụ | 3 |
| Phiếu thu trả trước | 3 |
| Phiếu gia hạn / chi tiết gia hạn | 1 / 1 |
| Nhân viên công ty / xe / thẻ xe | 2 / 1 / 1 |
| Biên bản kiểm tra / chi tiết phòng | 1 / 2 |
| Phiếu phạt | 1 |

Dữ liệu trình diễn sử dụng mốc **30/10/2026**:

| Công ty | Phòng thuê | Thời gian gốc | Tiền đã trả trước | Tình huống |
| --- | --- | --- | ---: | --- |
| An Phát | P101, P102 | 12 tháng từ 01/01/2026 | 132.000.000 đồng | Còn nợ thuê từ kỳ 7 và dịch vụ |
| Minh Khang | P201 | 6 tháng từ 01/04/2026 | 66.000.000 đồng | Đã gia hạn 1 tháng; kỳ tháng 10 chưa thu |
| Sao Việt | P301 | 6 tháng từ 01/05/2026 | 108.000.000 đồng | Đủ điều kiện thử gia hạn tại ngày 30/10/2026 |

Dịch vụ P101 tháng **09/2026** đã phát hành **1.300.000 đồng**, chưa thu; tháng **10/2026** có hóa đơn nháp **700.000 đồng**.

Các giá trị trên áp dụng cho bộ mẫu nguyên trạng. Mã hợp đồng do chương trình tạo; khi trình diễn chọn theo tên công ty.

### Kịch bản kiểm tra trên giao diện

1. Mở Tầng và văn phòng, lọc tầng và tra cứu phòng trống theo khoảng ngày.
2. Ký hợp đồng mới với thời hạn ít nhất 6 tháng và chọn một hoặc nhiều phòng.
3. Mở Thanh toán và dịch vụ, chọn hợp đồng rồi thu trước 6 tháng; thử thu thiếu và thu đủ để kiểm tra trạng thái.
4. Chọn An Phát, xem các khoản thuê từ kỳ 7 và hóa đơn dịch vụ còn nợ.
5. Mở Gia hạn với An Phát để kiểm tra nhánh từ chối do công nợ.
6. Chọn Sao Việt, lập phiếu ngày **30/10/2026**, gia hạn P301 từ **1–3 tháng**; xem hạn mới và các khoản thuê bổ sung.
7. Mở Nhân viên và thẻ xe, tra cứu hồ sơ, cấp hoặc thu hồi thẻ.
8. Xem biên bản kiểm tra, phiếu phạt và thử xuất CSV.

Nếu chọn ngày gia hạn khác, phải đối soát dịch vụ đến ngày đó. Khi phòng đã quá hạn, cần lập hợp đồng mới.

## Quy tắc nghiệp vụ

- Hợp đồng gốc tối thiểu **6 tháng**; một hợp đồng có thể thuê nhiều phòng. Kiểm tra lịch giao nhau ngay từ trạng thái chờ trả trước.
- Chỉ kích hoạt hợp đồng khi thu đủ **6 tháng đầu của tất cả phòng**. Từ tháng thứ 7, tiền thuê đến hạn vào đầu mỗi kỳ thuê.
- Cho phép thu từng phần nhưng tổng phân bổ phải bằng tổng phiếu; không thu vượt số dư hoặc thu nhầm khách.
- Dịch vụ quản lý theo tháng dương lịch. Hóa đơn có tiền chỉ phát hành từ cuối tháng; phát hành lại không tạo khoản thu thứ hai.
- Gia hạn mỗi lần không quá `floor(SoThangGoc / 2)` tháng; không gia hạn sau khi phòng đã quá hạn hoặc chồng lịch thuê của khách khác.
- Trước gia hạn phải hết nợ thuê đã đến hạn; dịch vụ đã sử dụng phải được chốt và thanh toán đầy đủ. Kỳ không phát sinh cần xác nhận đối soát.
- Giá gia hạn chỉ áp dụng cho kỳ mới. Các mốc tháng tính từ ngày bắt đầu gốc để tránh trôi ngày cuối tháng.
- Nhân viên phải thuộc công ty có quyền thuê phòng tại ngày bố trí. Một xe có tối đa một thẻ hiệu lực; nghỉ việc thu hồi các thẻ đang hiệu lực.
- Phòng đến hạn kiểm tra sau **12 tháng**. Phiếu phạt phải gắn đúng biên bản vi phạm và bên thuê tại thời điểm đó; tiền nộp không vượt mức phạt.
- Nghiệp vụ nhiều bảng dùng transaction; lỗi sẽ rollback toàn bộ. `RequestId` UNIQUE ngăn tạo trùng hợp đồng, phiếu thu và phiếu gia hạn khi gửi lại yêu cầu.

Các thao tác ghi dùng `Serializable` kết hợp `sp_getapplock` để bảo vệ lịch phòng và số dư khi xử lý đồng thời.

## Kiểm thử

Chạy tại thư mục `LAB8`:

```powershell
dotnet run --project ".\QuanLyCaoOc\QuanLyCaoOc\QuanLyCaoOc.csproj" -c Release -- --verify

dotnet run --project ".\QuanLyCaoOc\QuanLyCaoOc\QuanLyCaoOc.csproj" -c Release -- --verify-ui
```

`--verify` tạo CSDL riêng có hậu tố ngẫu nhiên, kiểm tra nghiệp vụ trên SQL Server thật rồi dọn CSDL đó.

`--verify-ui` mở 9 Form, 15 tab và các hộp nhập; kiểm tra binding, lựa chọn khóa ngoại và cột được phép sửa. Các hộp thử được đóng bằng Hủy, không lưu chứng từ thử. Lần chạy đầu vẫn khởi tạo database và dữ liệu mẫu như khởi động bình thường.

Kết quả lần kiểm tra **30/10/2026**: build Release **0 lỗi, 0 cảnh báo**; **50/50** kiểm tra nghiệp vụ và **44/44** kiểm tra giao diện đạt.

Nhật ký được tạo tại:

```text
LAB8/QuanLyCaoOc/QuanLyCaoOc/bin/Release/net10.0-windows/
├── verification.txt
└── Verification/
    └── UI/
        ├── ui-checks.txt
        └── Các ảnh giao diện
```

Các ca kiểm tra gồm thời hạn tối thiểu, trả trước nhiều phòng, thu vượt nợ, dịch vụ chưa chốt, gia hạn quá nửa/chồng lịch, ngày cuối tháng, rollback nhiều dòng, yêu cầu lặp, thẻ xe trùng và hai thao tác thu tiền hoặc ký hợp đồng đồng thời.

## Báo cáo và phạm vi

Báo cáo `NguyenNguyenHau_LAB8.docx` có **63 trang**, gồm 9 phần: giới thiệu, phân tích, thiết kế, CSDL, kiến trúc và hệ thống Form, giao diện, tạo môi trường/project, hướng dẫn code từng Form và kiểm thử tổng thể.

Báo cáo chứa **25 hình** gồm sơ đồ và ảnh giao diện chạy thật. Sơ đồ được vẽ bằng draw.io; SQL/C# được đóng khung. Báo cáo và tài liệu mô hình đặt ngoài thư mục code `LAB8`.

Ứng dụng chưa có đăng nhập/phân quyền, chữ ký điện tử, cổng thanh toán, hoàn tiền hoặc hóa đơn điện tử. Actor trong UML mô tả trách nhiệm nghiệp vụ.

Chỉ hủy trực tiếp hợp đồng đang chờ và chưa nhận tiền; hợp đồng đã có khoản thu cần quy trình đối soát và hoàn tiền riêng.
