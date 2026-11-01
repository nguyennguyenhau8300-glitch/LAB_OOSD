# LAB7 – Quản lý nhân sự công ty sơn Đông Á 

[README tổng các bài Lab](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/main/README.md) · [Mã nguồn LAB7](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB7/LAB7/QuanLyNhanSu)

## Thông tin sinh viên

- **Họ và tên:** Nguyễn Nguyên Hậu
- **MSSV:** 1250080049
- **Lớp:** 12_ĐH_CNPM1
- **Bài thực hành:** Phân tích, thiết kế và hiện thực hệ thống quản lý nhân sự công ty sơn Đông Á.

## Giới thiệu

Bài làm tổ chức nghiệp vụ thành **6 nhóm chức năng chính**, từ tuyển dụng và quản lý hồ sơ đến chấm công, thưởng phạt và tính lương. Chương trình dùng C# WinForms **.NET 10**, kết nối SQL Server và tổ chức mã nguồn theo `Forms → Services → Data`, tương tự LAB6.

Màn hình chính có sáu nút xếp hai hàng, mỗi hàng ba nút và nút Thoát phía dưới. Mỗi nút mở một Form nghiệp vụ riêng; giao diện dùng nền trắng, chữ đen và không thêm icon cho các chức năng.

## Chức năng và các Form

| Nhóm | Chức năng chính | Form | Các màn hình con |
| --- | --- | --- | --- |
| CN01 | Hồ sơ nhân viên và danh mục | `FrmHoSoNhanVien` | Nhân viên, phòng ban, chức vụ, ca làm; tra cứu lịch sử công tác |
| CN02 | Tuyển dụng – thử việc – hợp đồng | `FrmTuyenDung` | Quyết định tuyển dụng, đánh giá thử việc, lập và ghi nhận ký hợp đồng |
| CN03 | Quyết định lương – chức vụ – bảo hiểm | `FrmCheDo` | Quyết định lương, bổ nhiệm chức vụ, hồ sơ BHYT/BHXH |
| CN04 | Phân công tuần – chấm công – phụ trội | `FrmChamCong` | Phân công, ghi công thực tế, phiếu phụ trội, nhân viên tham gia |
| CN05 | Khen thưởng – kỷ luật – thu chi | `FrmThuongPhat` | Quyết định thưởng/phạt, ghi nhận đã ký, phiếu thu/chi trực tiếp |
| CN06 | Tính và chốt lương tháng | `FrmBangLuong` | Tính lại bản nháp, xem chi tiết, chốt tháng, xuất CSV |

Sáu Form nghiệp vụ kế thừa `FrmChucNang`, dùng chung bộ lọc, lưới dữ liệu và thao tác. Có **16 màn hình con**; `FrmEditor` hỗ trợ nhập/sửa và `FrmTable` hiển thị lịch sử hoặc chi tiết lương. Mỗi Form có cặp `.cs` và `.Designer.cs`.

## Môi trường và cấu trúc mã nguồn

| Thành phần | Phiên bản / cấu hình |
| --- | --- |
| Nền tảng | Windows, C# WinForms, `net10.0-windows` |
| Công cụ | .NET 10 SDK; Visual Studio hỗ trợ .NET 10 và workload **.NET desktop development** |
| CSDL | SQL Server / SQL Server LocalDB, database `QuanLyNhanSuLAB7` |
| Thư viện kết nối | `Microsoft.Data.SqlClient` 7.1.1 |
| Cấu hình | Chuỗi kết nối `NhanSuDB` trong `App.config`; mặc định `(localdb)\MSSQLLocalDB`, xác thực Windows |

```text
LAB7/
└── QuanLyNhanSu/
    ├── NguyenNguyenHau_LAB7.docx
    ├── QuanLyNhanSu.slnx
    └── QuanLyNhanSu/
        ├── QuanLyNhanSu.csproj
        ├── Program.cs
        ├── App.config
        ├── Verification.cs
        ├── Forms/
        │   ├── FrmMain.cs / FrmMain.Designer.cs
        │   ├── FrmHoSoNhanVien.cs / FrmHoSoNhanVien.Designer.cs
        │   ├── FrmTuyenDung.cs / FrmTuyenDung.Designer.cs
        │   ├── FrmCheDo.cs / FrmCheDo.Designer.cs
        │   ├── FrmChamCong.cs / FrmChamCong.Designer.cs
        │   ├── FrmThuongPhat.cs / FrmThuongPhat.Designer.cs
        │   ├── FrmBangLuong.cs / FrmBangLuong.Designer.cs
        │   ├── FrmChucNang.cs / FrmChucNang.Designer.cs
        │   ├── FrmEditor.cs / FrmEditor.Designer.cs
        │   ├── FrmTable.cs / FrmTable.Designer.cs
        │   └── FormHelper.cs
        ├── Services/
        │   ├── Models.cs
        │   ├── NhanSuService.cs
        │   └── PayrollService.cs
        └── Data/
            ├── DB.cs
            └── QuanLyNhanSu.sql
```

`FrmMain` mở các Form bằng `ShowDialog`. Form gọi `NhanSuService`; lớp `Db` quản lý kết nối và giao dịch SQL. `PayrollService.cs` là phần khai báo `partial` của `NhanSuService`, chứa xử lý tính/chốt lương. Giá trị nhập được truyền bằng tham số SQL.

## Cài đặt và chạy

Lấy mã nguồn lần đầu:

```powershell
git clone https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD.git
cd LAB_OOSD
git switch LAB7
```

Tại thư mục gốc repository, chạy:

```powershell
SqlLocalDB start MSSQLLocalDB
sqlcmd -S '(localdb)\MSSQLLocalDB' -E -C -b -f 65001 -i '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\Data\QuanLyNhanSu.sql'
dotnet restore '.\LAB7\QuanLyNhanSu\QuanLyNhanSu.slnx'
dotnet build '.\LAB7\QuanLyNhanSu\QuanLyNhanSu.slnx' -c Release
dotnet run --project '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\QuanLyNhanSu.csproj' -c Release
```

Có thể mở và chạy toàn bộ SQL bằng SSMS, sau đó mở solution bằng Visual Studio và nhấn **F5**. Nếu dùng SQL Server khác, sửa `connectionStrings/NhanSuDB` trong `App.config` trước khi build.

Tệp SQL trong `Data` gồm lược đồ, khóa/ràng buộc và dữ liệu mẫu. Script kiểm tra bản ghi đã có để tránh thêm trùng; bảng lương mẫu chỉ được tạo khi tháng đó chưa tồn tại, không ghi đè tháng đã tính hoặc chốt.

## CSDL và dữ liệu trình diễn

CSDL có **17 bảng, 19 khóa ngoại**. Bộ mẫu ban đầu gồm:

| Dữ liệu | Số lượng |
| --- | ---: |
| Nhân viên / quyết định tuyển dụng | 3 / 3 |
| Phòng ban / chức vụ / ca làm | 3 / 3 / 2 |
| Hợp đồng / quyết định lương / quyết định chức vụ | 2 / 2 / 1 |
| Hồ sơ bảo hiểm | 2 |
| Phân công / chấm công | 240 / 240 |
| Phiếu phụ trội / chi tiết tham gia | 2 / 4 |
| Quyết định thưởng phạt / phiếu thu chi | 7 / 2 |
| Bảng lương nháp / chi tiết lương | 2 / 6 |

Dữ liệu công thuộc **tháng 09 và 10/2026**; mỗi tháng có 120 phân công và 120 dòng chấm công. Khi trình diễn, chọn đúng tháng/năm trong Form. Đối chiếu lương với **208 giờ chuẩn**:

| Nhân viên | Tháng 09/2026 | Tháng 10/2026 |
| --- | ---: | ---: |
| Nguyễn Minh Anh | 8.862.000 đồng | 8.737.000 đồng |
| Trần Quốc Bảo | 7.635.000 đồng | 7.685.000 đồng |
| Lê Hoàng Hà | 6.000.000 đồng | 6.000.000 đồng |

Các giá trị trên áp dụng cho bộ mẫu nguyên trạng. Nếu đã sửa dữ liệu nguồn, nhấn **Tính lại bản nháp** và đối chiếu chi tiết trước khi chốt.

### Kịch bản kiểm tra trên giao diện

1. Mở Hồ sơ nhân viên, chọn Nguyễn Minh Anh và xem lịch sử công tác.
2. Xem thử việc, hợp đồng, quyết định lương/chức vụ và bảo hiểm của nhân viên.
3. Mở Phân công tuần – chấm công – phụ trội, chọn **10/2026**; kiểm tra công và nhân viên tham gia phiếu `PT-2026-010`.
4. Mở Khen thưởng – kỷ luật – thu chi; xem bốn quyết định tháng 10 và hai phiếu thu/chi trực tiếp.
5. Mở Tính và chốt lương tháng, chọn **10/2026**, giờ chuẩn **208**; xem bản nháp, tính lại và đối chiếu ba mức lương mẫu.
6. Thử xuất CSV. Chỉ chốt khi đã đối soát và tháng đã kết thúc; chốt lương sẽ khóa dữ liệu ảnh hưởng tháng đó.

## Quy tắc nghiệp vụ

- Đánh giá thử việc sau khi hết hạn; chỉ lập/ký hợp đồng sau khi đã đạt. Kiểm tra hợp đồng chồng thời gian.
- Quyết định lương, chức vụ và bảo hiểm được áp dụng theo ngày hiệu lực. Hợp đồng dài hạn cần đủ BHYT và BHXH để tính lương.
- Kiểm tra ca chồng nhau, giờ công vượt ca, giờ phụ trội vượt phiếu và tổng giờ trong ngày.
- Quyết định thưởng/phạt chọn đúng một đối tượng cá nhân hoặc tập thể. Tập thể chỉ thanh toán trực tiếp; quyết định phải được ghi nhận đã ký trước khi thu/chi.
- Lương theo công và phụ cấp được phân bổ theo giờ thực tế/giờ chuẩn; phụ trội áp dụng hệ số **1,5** theo bài triển khai.
- Thực lĩnh = lương theo công + phụ cấp + phụ trội + thưởng − phạt − bảo hiểm. Khoản đã thu/chi trực tiếp không tính thêm vào lương.
- Khi chốt, tính lại trong cùng giao dịch và khóa tháng; lỗi sẽ rollback. Chưa có chức năng mở lại tháng đã chốt.

208 giờ và các tỷ lệ bảo hiểm là tham số/dữ liệu minh họa của bài lab.

## Kiểm thử

Chạy tại thư mục gốc repository sau khi khởi tạo dữ liệu:

```powershell
dotnet run --project '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\QuanLyNhanSu.csproj' -c Release -- --verify
dotnet run --project '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\QuanLyNhanSu.csproj' -c Release -- --verify-ui
```

`--verify` tạo CSDL riêng có hậu tố ngẫu nhiên, kiểm tra nghiệp vụ rồi dọn CSDL đó. `--verify-ui` mở các Form, kiểm tra điều hướng, dữ liệu hiển thị và nạp bản ghi; không lưu thay đổi vào CSDL làm việc.

Kết quả lần kiểm tra **24/10/2026**: build Release **0 lỗi, 0 cảnh báo**; **36/36** kiểm tra nghiệp vụ và **57/57** kiểm tra giao diện đạt. Nhật ký `KiemThu_LAB7.txt`, `KiemThu_GiaoDien_LAB7.txt` được tạo tại `LAB7/QuanLyNhanSu/QuanLyNhanSu/bin/Release/net10.0-windows/Verification/`.

## Báo cáo và phạm vi

[Báo cáo Word LAB7](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB7/LAB7/QuanLyNhanSu/NguyenNguyenHau_LAB7.docx) có **66 trang**, gồm phân tích, thiết kế, CSDL và giao diện. Báo cáo chứa **33 sơ đồ UML dạng vector**, 9 ảnh giao diện và 5 mẫu phiếu: chấm công, phụ trội, lương nhân viên, chi khen thưởng, thu nộp phạt. Tên hình được căn giữa; SQL/C# được đóng khung. Báo cáo kết thúc ở mục 6.4.

Ứng dụng chưa có đăng nhập/phân quyền, chữ ký điện tử hoặc in trực tiếp. Nút **Ghi nhận đã ký** xác nhận văn bản đã ký ngoài phần mềm; các phiếu in là mẫu trong báo cáo. Vai trò trong UML mô tả trách nhiệm nghiệp vụ.
