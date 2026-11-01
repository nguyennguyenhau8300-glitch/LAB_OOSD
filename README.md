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
| Lab 6 | Quản lý hồ sơ bệnh án | Hồ sơ, lệ phí năm, lịch hẹn, khám định kỳ, xét nghiệm, toa thuốc, thanh toán và danh mục | [LAB6](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB6/LAB6) | [README Lab 6](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB6/README.md) |
| Lab 7 | Quản lý nhân sự công ty sơn Đông Á | Hồ sơ, tuyển dụng/thử việc, hợp đồng, chế độ, chấm công, thưởng phạt và lương tháng | [LAB7](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB7/LAB7/QuanLyNhanSu) | [README Lab 7](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB7/README.md) |
| Lab 8 | Quản lý cho thuê văn phòng cao ốc | Tầng/phòng, khách hàng, hợp đồng, thanh toán, dịch vụ, gia hạn, nhân viên công ty thuê, thẻ xe và kiểm tra phòng | [LAB8](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/tree/LAB8/LAB8/QuanLyCaoOc) | [README Lab 8](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB8/README.md) |

## 4. Môi trường và phiên bản

| Bài Lab | Ngôn ngữ / nền tảng | Cơ sở dữ liệu và thư viện |
| --- | --- | --- |
| Lab 1 | Java, chương trình console; cần JDK có `javac` và `java` | Không dùng CSDL; repository chưa ghi phiên bản JDK |
| Lab 2 | C#, WinForms, .NET Framework 4.7.2 | SQL Server LocalDB; `System.Data.SqlClient`; database `QuanLyThuVienDB` |
| Lab 3 | C#, WinForms, .NET 10; Visual Studio 2026 theo README | SQL Server Express; `Microsoft.Data.SqlClient` 7.1.0; database `QuanLyKhachSan` |
| Lab 4 | C#, WinForms, `net10.0-windows`; SDK 10.0.401 | LocalDB 17.0.4025.3; `Microsoft.Data.SqlClient` 7.0.3; database `EShopping_Prototype` |
| Lab 5 | C#, WinForms, `net10.0-windows`; SDK 10.0.401 | LocalDB 17.0.4025.3; `Microsoft.Data.SqlClient` 7.1.1; database `QuanLyCongTyDuLich` |
| Lab 6 | C#, WinForms, `net10.0-windows`; .NET 10 SDK | SQL Server LocalDB; `Microsoft.Data.SqlClient` 7.1.1; database `QuanLyHoSoBenhAnLAB6` |
| Lab 7 | C#, WinForms, `net10.0-windows`; .NET 10 SDK | SQL Server LocalDB; `Microsoft.Data.SqlClient` 7.1.1; database `QuanLyNhanSuLAB7` |
| Lab 8 | C#, WinForms, `net10.0-windows`; SDK kiểm tra 10.0.402; Visual Studio 2026 | SQL Server LocalDB; `Microsoft.Data.SqlClient` 7.1.1; database `QuanLyCaoOcLAB8` |

Các bài WinForms chạy trên Windows. Với Lab 2, cài .NET Framework 4.7.2 Developer Pack và workload **.NET desktop development**. Với Lab 3–8, dùng .NET 10 SDK và Visual Studio hỗ trợ .NET 10. Có thể dùng SSMS để chạy script và kiểm tra dữ liệu SQL Server.

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

### 5.6. Lab 6 – Quản lý hồ sơ bệnh án

- Xây dựng 7 Form theo cấu trúc `Forms`, `Services`, `Data` và `App.config`.
- Đăng ký bệnh nhân, thu/gia hạn lệ phí năm, cấp hồ sơ và tra cứu lịch sử.
- Đặt/hủy lịch hẹn; khám điều trị hoặc định kỳ theo quý; chỉ định và ghi kết quả xét nghiệm.
- Lưu kết luận/toa thuốc, xuất TXT và hoàn tất phiếu sau thanh toán.
- Quản lý khoa, bác sĩ, thuốc; thống kê lượt khám và tiền đã thu.
- Gộp cấu trúc 12 bảng, dữ liệu trình diễn và dọn dữ liệu kiểm thử tùy chọn vào `Data/QuanLyHoSoBenhAn.sql`.
- Có hai chế độ kiểm thử `--verify` và `--verify-catalog`, kèm dọn dữ liệu tạm sau khi chạy.

### 5.7. Lab 7 – Quản lý nhân sự công ty sơn Đông Á

- Tổ chức đúng 6 nhóm nghiệp vụ: hồ sơ/danh mục; tuyển dụng–thử việc–hợp đồng; lương–chức vụ–bảo hiểm; phân công–chấm công–phụ trội; thưởng phạt–thu chi; tính/chốt lương tháng.
- Màn hình chính có 6 nút mở 6 Form riêng, mỗi Form có `.cs`/`.Designer.cs`; dùng chung `FrmChucNang`, `FrmEditor`, `FrmTable` và `FormHelper` theo cấu trúc LAB6.
- Giữ .NET 10; Form gọi `NhanSuService`, Service dùng `Db`; SQL tạo/nạp mẫu nằm trong `Data/QuanLyNhanSu.sql`.
- Thiết kế 17 bảng, 19 khóa ngoại; nạp đủ dữ liệu cho 16 màn hình con của tháng 09 và 10/2026, gồm hai bảng lương nháp và phiếu thu/chi.
- Kiểm tra thử việc, hợp đồng, bảo hiểm, ca/giờ công, phụ trội, quyết định thưởng phạt; tính và chốt lương trong transaction, bảo vệ tháng đã chốt.
- Kèm báo cáo Word 66 trang với 33 sơ đồ UML vector, 9 ảnh giao diện, 5 mẫu phiếu; báo cáo kết thúc ở mục 6.4.
- Có các chế độ `--verify` và `--verify-ui` để kiểm tra nghiệp vụ, điều hướng và dữ liệu hiển thị.

### 5.8. Lab 8 – Quản lý cho thuê văn phòng cao ốc

- Tổ chức 6 nhóm nghiệp vụ: tầng/văn phòng; khách hàng/hợp đồng; thanh toán/dịch vụ; gia hạn; nhân viên công ty thuê/thẻ xe; kiểm tra/xử phạt.
- Xây dựng 9 Form gồm `FrmMain`, sáu Form nghiệp vụ, `FrmEditor` và `FrmTable`; có 15 màn hình con, mỗi Form có `.cs`/`.Designer.cs`.
- Sử dụng .NET 10, cấu trúc `Forms`, `Services`, `Data` và `App.config`, tiếp nối cách tổ chức LAB4–LAB7.
- Thiết kế CSDL 19 bảng; gộp cấu trúc, PK/FK/UNIQUE/CHECK, chỉ mục, view công nợ và dữ liệu mẫu vào `Data/QuanLyCaoOc.sql`.
- Kiểm tra thuê tối thiểu 6 tháng, trả trước đủ 6 tháng đầu của tất cả phòng, thu từ kỳ 7, phát hành dịch vụ cuối tháng và phân bổ tiền không vượt công nợ.
- Gia hạn mỗi lần không quá một nửa thời gian thuê gốc; kiểm tra nợ thuê, dịch vụ chưa chốt/chưa trả, hạn hiệu lực và lịch thuê tiếp theo.
- Quản lý hồ sơ nhân viên công ty thuê, ảnh, nơi làm việc, xe và thẻ; kiểm tra phòng theo chu kỳ 12 tháng, lập phiếu phạt và ghi nhận tiền nộp.
- Nghiệp vụ nhiều bảng dùng transaction; `RequestId` UNIQUE và khóa ghi bảo vệ thao tác lặp, lịch phòng và số dư khi xử lý đồng thời.
- Kèm báo cáo Word 63 trang theo 9 phần, có 25 hình gồm sơ đồ draw.io và giao diện chạy thật; SQL/C# được đóng khung.
- Có hai chế độ `--verify` và `--verify-ui` để kiểm tra nghiệp vụ và giao diện.

## 6. Kết quả

| Bài Lab | Kết quả được mô tả trong mã nguồn / README của bài |
| --- | --- |
| Lab 1 | Có chương trình nhập ba loại hình và in chu vi, diện tích; thao tác `ve()` in thông báo trong console. |
| Lab 2 | Xây dựng CSDL 9 bảng, các form quản lý thư viện và kết nối LocalDB. |
| Lab 3 | Xây dựng các module quản lý khách sạn, kết nối SQL Server và xử lý các nghiệp vụ đặt phòng, dịch vụ, thanh toán. |
| Lab 4 | README ghi build Release 0 lỗi, 0 cảnh báo; `Verification.txt` ghi 18/18 kiểm tra đạt. |
| Lab 5 | README ghi build 0 lỗi, 0 cảnh báo; mở 9 form/17 màn hình hoặc tab; kiểm tra lương mẫu và 5 chỉ số thống kê. |
| Lab 6 | 7 Form, CSDL 12 bảng; bộ dữ liệu 12 bệnh nhân, 18 phiếu khám, 27 xét nghiệm, 12 toa và 10 thanh toán; hai chế độ kiểm thử nghiệp vụ và danh mục. |
| Lab 7 | 6 nhóm chức năng, 16 màn hình con; CSDL 17 bảng/19 FK; 240 phân công, 240 dòng công, 2 phiếu thu/chi, 2 bảng lương nháp/6 chi tiết. Lần kiểm tra 24/10/2026: build 0 lỗi/0 cảnh báo, 36/36 nghiệp vụ và 57/57 giao diện đạt. |
| Lab 8 | 6 nhóm chức năng, 9 Form, 15 màn hình con; CSDL 19 bảng; mẫu 3 tầng, 9 phòng, 3 công ty, 3 hợp đồng và dữ liệu thu tiền, dịch vụ, gia hạn, thẻ xe, kiểm tra/phạt. Lần kiểm tra 30/10/2026: build Release 0 lỗi/0 cảnh báo, 50/50 nghiệp vụ và 44/44 giao diện đạt. |

Kết quả trên tổng hợp từ từng bài. Lab 5 đã kiểm tra giao diện, kết nối, lương và thống kê; chưa thực hiện đầy đủ 24 test case nghiệp vụ trong tài liệu.

## 7. Lỗi gặp phải và cách khắc phục

| Bài / tình huống | Cách khắc phục |
| --- | --- |
| Lab 2: không mở được database | Khởi động LocalDB, chạy script tạo CSDL và kiểm tra kết nối trong `App.config`. |
| Lab 2: thiếu `InitializeComponent`, control hoặc event handler | Kiểm tra cặp file `.cs`/`.Designer.cs`, khai báo control và tên hàm xử lý sự kiện. |
| Lab 3: lỗi đăng nhập SQL Server | Sửa server/instance và thông tin xác thực trong `Data/Db.cs` theo máy chạy; kiểm tra quyền truy cập CSDL. |
| Lab 3–8: thiếu `Microsoft.Data.SqlClient` | Restore NuGet trước khi build, kiểm tra phiên bản gói trong `.csproj`. |
| Lab 3: đặt phòng trùng lịch hoặc xóa dữ liệu có khóa ngoại | Kiểm tra lịch phòng và các bản ghi liên quan trước khi thao tác. |
| Lab 4: script tiếng Việt bị sai ký tự | Mở script UTF-8 trong SSMS hoặc dùng `sqlcmd -f 65001`. |
| Lab 4: không kết nối được LocalDB | Khởi động instance và kiểm tra `Connection.txt`. |
| Lab 5: thiếu form khởi động hoặc lớp hỗ trợ | Dùng `FrmMain`, bổ sung `FormHelper` và các file Designer. |
| Lab 5: filtered index báo lỗi `QUOTED_IDENTIFIER` | Dùng `SET QUOTED_IDENTIFIER ON`, `SET ANSI_NULLS ON`; khi chạy `sqlcmd` thêm `-I`. |
| Lab 6: chưa có CSDL hoặc thiếu dữ liệu | Chạy toàn bộ `LAB6/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn/Data/QuanLyHoSoBenhAn.sql`; kiểm tra `App.config`. |
| Lab 7: Form phân công, thu chi hoặc lương trống | Chạy toàn bộ `LAB7/QuanLyNhanSu/QuanLyNhanSu/Data/QuanLyNhanSu.sql`, chọn tháng 09 hoặc 10/2026 rồi Tải lại; kiểm tra `NhanSuDB` trong `App.config`. |
| Lab 8: không khởi tạo được database | Khởi động LocalDB, kiểm tra chuỗi `CaoOcDB` trong `App.config` và quyền tạo database; xem `startup-error.txt` cạnh chương trình. |
| Lab 8: cột mã trong bảng nhập nhiều phòng chưa khóa sửa | Khởi tạo `BindingContext` trước khi gán `DataSource`, sau đó chỉ cho sửa các cột nằm trong danh sách `editable`. |
| Lab 8: vùng ảnh nhân viên hoặc nút chọn ảnh hiển thị chưa đúng | Điều chỉnh thứ tự Dock của lưới/vùng ảnh và tăng kích thước nút chọn ảnh; chụp lại giao diện để đối chiếu. |
| Lab 8: gia hạn bị từ chối | Kiểm tra nợ thuê, hóa đơn dịch vụ, kỳ chưa đối soát, giới hạn tháng và hạn phòng; dữ liệu trình diễn dùng ngày lập 30/10/2026. |

## 8. Hướng dẫn giảng viên kiểm tra và chạy lại

### 8.1. Lấy đúng bài Lab

Clone repository và chuyển sang nhánh tương ứng. Ví dụ:

```powershell
git clone https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD.git
cd LAB_OOSD
git switch LAB1
```

Thay `LAB1` bằng `LAB2`, `LAB3`, `LAB4`, `LAB5`, `LAB6`, `LAB7` hoặc `LAB8` để xem bài khác. Trước khi chuyển nhánh, lưu các thay đổi đang thực hiện. Có thể chọn nhánh trên GitHub rồi chọn **Code → Download ZIP**.

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

### 8.7. Lab 6

Chuyển sang nhánh `LAB6`, mở PowerShell tại thư mục gốc repository:

```powershell
git switch LAB6
SqlLocalDB start MSSQLLocalDB
sqlcmd -S '(localdb)\MSSQLLocalDB' -E -C -b -f 65001 -i '.\LAB6\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn\Data\QuanLyHoSoBenhAn.sql'
dotnet restore '.\LAB6\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn.slnx'
dotnet build '.\LAB6\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn.slnx' -c Release
dotnet run --project '.\LAB6\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn.csproj' -c Release
```

Script gộp tạo/nâng cấp CSDL và nạp dữ liệu trình diễn, có thể chạy lại mà không thêm trùng bộ dữ liệu. Công tắc `@DonDuLieuKiemThu` mặc định là `0`; chỉ đổi sang `1` khi cần dọn hồ sơ `LAB6_TEST_` cũ cùng dữ liệu phụ thuộc. Nếu dùng instance khác, sửa `Data Source` trong `App.config`.

Kiểm tra luồng đăng ký → hẹn khám → lập phiếu → xét nghiệm → kết luận/toa → thanh toán; thử thêm/sửa/xóa danh mục khoa, bác sĩ và thuốc. Chạy kiểm thử tự động:

```powershell
dotnet run --project '.\LAB6\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn.csproj' -c Release -- --verify
dotnet run --project '.\LAB6\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn\QuanLyHoSoBenhAn.csproj' -c Release -- --verify-catalog
```

Nhật ký `results.txt` và `catalogue-results.txt` nằm trong `bin/Release/net10.0-windows/Verification/` của dự án. Xem [README Lab 6](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB6/README.md) để biết cấu trúc, quy tắc nghiệp vụ và hướng dẫn chi tiết.

### 8.8. Lab 7

Chuyển sang nhánh `LAB7`, mở PowerShell tại thư mục gốc repository:

```powershell
git switch LAB7
SqlLocalDB start MSSQLLocalDB
sqlcmd -S '(localdb)\MSSQLLocalDB' -E -C -b -f 65001 -i '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\Data\QuanLyNhanSu.sql'
dotnet restore '.\LAB7\QuanLyNhanSu\QuanLyNhanSu.slnx'
dotnet build '.\LAB7\QuanLyNhanSu\QuanLyNhanSu.slnx' -c Release
dotnet run --project '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\QuanLyNhanSu.csproj' -c Release
```

Script tạo CSDL và nạp dữ liệu mẫu cho hai tháng 09 và 10/2026. Có thể chạy lại mà không thêm trùng; bảng lương mẫu chỉ được tạo khi tháng chưa tồn tại. Nếu dùng instance khác, sửa chuỗi `NhanSuDB` trong `App.config`.

Từ màn hình chính, mở từng nhóm chức năng; chọn **10/2026** để kiểm tra phân công, công, phụ trội, thưởng phạt, phiếu thu/chi và lương nháp. Với **208 giờ chuẩn**, thực lĩnh tháng 10 của Nguyễn Minh Anh là **8.737.000 đồng**, Trần Quốc Bảo **7.685.000 đồng**, Lê Hoàng Hà **6.000.000 đồng** theo bộ mẫu nguyên trạng. Xem chi tiết và đối soát trước khi chốt; tháng đang diễn ra chưa được chốt.

Chạy kiểm thử tự động:

```powershell
dotnet run --project '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\QuanLyNhanSu.csproj' -c Release -- --verify
dotnet run --project '.\LAB7\QuanLyNhanSu\QuanLyNhanSu\QuanLyNhanSu.csproj' -c Release -- --verify-ui
```

`--verify` tạo và dọn CSDL kiểm thử riêng. `--verify-ui` mở/nạp Form, kiểm tra đủ sáu nhóm và dữ liệu trên 16 màn hình con. Nhật ký `KiemThu_LAB7.txt` và `KiemThu_GiaoDien_LAB7.txt` được tạo trong `LAB7/QuanLyNhanSu/QuanLyNhanSu/bin/Release/net10.0-windows/Verification/`. Xem [README Lab 7](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB7/README.md) và [báo cáo Word](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB7/LAB7/QuanLyNhanSu/NguyenNguyenHau_LAB7.docx) để biết cấu trúc và nghiệp vụ chi tiết.

### 8.9. Lab 8

Chuyển sang nhánh `LAB8`, mở PowerShell tại thư mục gốc repository:

```powershell
git switch LAB8
SqlLocalDB start MSSQLLocalDB
dotnet restore '.\LAB8\QuanLyCaoOc\QuanLyCaoOc.sln'
dotnet build '.\LAB8\QuanLyCaoOc\QuanLyCaoOc.sln' -c Release
dotnet run --project '.\LAB8\QuanLyCaoOc\QuanLyCaoOc\QuanLyCaoOc.csproj' -c Release
```

Lần khởi động đầu, chương trình tự tạo database `QuanLyCaoOcLAB8`, chạy file `LAB8/QuanLyCaoOc/QuanLyCaoOc/Data/QuanLyCaoOc.sql` và nạp dữ liệu mẫu. Những lần sau giữ dữ liệu hiện có. Nếu dùng instance khác, sửa chuỗi `CaoOcDB` trong `App.config` rồi build lại.

Nếu chạy SQL thủ công bằng SSMS, tạo và chọn database `QuanLyCaoOcLAB8`, sau đó thực thi toàn bộ script trong `Data`. Script từ chối chạy trong database hệ thống.

Từ màn hình chính, mở đủ sáu module và kiểm tra:

1. Tầng/phòng: lọc tầng, tra cứu lịch trống và xem lịch sử thuê.
2. Hợp đồng: chọn khách, nhiều phòng và thời hạn tối thiểu 6 tháng.
3. Thanh toán: thu trước 6 tháng; thử thu thiếu, thu đủ và thu vượt số dư.
4. Dịch vụ: xem hóa đơn P101 tháng **09/2026** đã phát hành **1.300.000 đồng**, chưa thu; tháng **10/2026** còn nháp **700.000 đồng**.
5. Gia hạn: chọn An Phát để kiểm tra nhánh từ chối do công nợ; chọn Sao Việt, P301, ngày lập **30/10/2026** và gia hạn **1–3 tháng** để thử nhánh thành công.
6. Nhân viên/thẻ xe và kiểm tra/phạt: tra cứu hồ sơ, cấp/thu hồi thẻ, xem biên bản, ghi tiền nộp và xuất CSV.

Bộ mẫu dùng mốc **30/10/2026**. Nếu dùng ngày khác phải đối soát dịch vụ đến ngày xử lý; khi phòng đã quá hạn thì lập hợp đồng mới.

Chạy kiểm thử tự động:

```powershell
dotnet run --project '.\LAB8\QuanLyCaoOc\QuanLyCaoOc\QuanLyCaoOc.csproj' -c Release -- --verify
dotnet run --project '.\LAB8\QuanLyCaoOc\QuanLyCaoOc\QuanLyCaoOc.csproj' -c Release -- --verify-ui
```

`--verify` tạo database riêng `QuanLyCaoOcLAB8_TEST_<guid>`, kiểm tra trên SQL Server thật rồi dọn database thử. `--verify-ui` mở 9 Form, 15 màn hình con và các hộp nhập; đóng bằng Hủy, không lưu chứng từ thử. Lần chạy đầu vẫn khởi tạo database và dữ liệu mẫu như khởi động bình thường.

Nhật ký `verification.txt` nằm trong `LAB8/QuanLyCaoOc/QuanLyCaoOc/bin/Release/net10.0-windows/`; ảnh và nhật ký giao diện nằm trong thư mục con `Verification/UI/`.

Xem [README Lab 8](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB8/README.md) và [báo cáo Word](https://github.com/nguyennguyenhau8300-glitch/LAB_OOSD/blob/LAB8/LAB8/QuanLyCaoOc/NguyenNguyenHau_LAB8.docx) để biết mô hình, cấu trúc và hướng dẫn từng Form.
