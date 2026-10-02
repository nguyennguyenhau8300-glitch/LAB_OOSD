# Bài lab e-SHOPPING

## Thông tin sinh viên và bài Lab

Họ tên: Nguyễn Nguyên Hậu 
MSSV: 1250080049 
Lớp: 12_ĐH_CNPM1 
Tên bài Lab: Hệ thống phần mềm Cửa hàng online e-SHOPPING, dự án tại `lab4` 

## Môi trường và phiên bản

- Hệ điều hành: Windows; ứng dụng WinForms chỉ chạy trên Windows.
- Dự án C# `net10.0-windows`, Windows Forms; máy thực hiện dùng .NET SDK `10.0.401`. Khi chạy bằng Visual Studio, dùng phiên bản hỗ trợ .NET 10 và cài workload **.NET desktop development**.
- Thư viện NuGet: `Microsoft.Data.SqlClient` phiên bản `7.0.3`.
- Cơ sở dữ liệu: **Microsoft SQL Server LocalDB**, instance `(localdb)\MSSQLLocalDB`; máy thực hiện có LocalDB `17.0.4025.3`. Có thể thay bằng SQL Server Express qua `Connection.txt`.
- Có thể dùng SQL Server Management Studio (SSMS) để xem dữ liệu hoặc chạy script thủ công. Không cần cài SSMS nếu để ứng dụng tự khởi tạo cơ sở dữ liệu.
- Biểu đồ nguồn mở bằng draw.io trong `Docs/Drawio`.

## Cài đặt môi trường

1. Trên Windows, cài **.NET 10 SDK** và Visual Studio hỗ trợ .NET 10 với workload **.NET desktop development**.
2. Cài **SQL Server Express LocalDB**. Kiểm tra trong PowerShell bằng `sqllocaldb info MSSQLLocalDB`. Nếu instance chưa có, dùng `sqllocaldb create MSSQLLocalDB`; có thể khởi động bằng `sqllocaldb start MSSQLLocalDB`.
3. Mở `Shopping.slnx` trong Visual Studio; cho phép NuGet restore gói `Microsoft.Data.SqlClient`. Nếu dùng bản đã build, giữ nguyên thư mục `Shopping/bin/Release/net10.0-windows`, nhất là DLL, `Connection.txt` và `Database/CuaHangOnlineDB.sql`.
4. Kiểm tra `Shopping/Connection.txt` khi chạy từ mã nguồn, hoặc `Shopping/bin/Release/net10.0-windows/Connection.txt` khi chạy EXE. Chuỗi mặc định trỏ tới `(localdb)\MSSQLLocalDB`, database `EShopping_Prototype`, Windows Authentication. Nếu máy dùng SQL Server Express, đổi `Data Source` thành tên instance trên máy; giữ `Initial Catalog=EShopping_Prototype`.
5. Chạy chương trình bằng **F5** trong Visual Studio hoặc mở `Shopping/bin/Release/net10.0-windows/Shopping.exe`. Lần chạy đầu, chương trình thực thi script `CuaHangOnlineDB.sql`, tạo 3 bảng và view danh mục 20 sản phẩm. Script cũng có thể chạy thủ công trong SSMS.

## Nội dung đã thực hiện

- Phân tích nghiệp vụ, UML và ERD trong báo cáo Word và các file draw.io.
- 8 màn hình WinForms tách `FrmX.cs` và `FrmX.Designer.cs` theo cấu trúc bài QuanLyThuVien.
- Xem, lọc 20 sản phẩm thật theo 4 nhóm; mỗi nhóm 5 sản phẩm. Tên/model là sản phẩm có thật, giá và trạng thái còn hàng là số liệu minh họa.
- Đăng ký, đăng nhập, giỏ hàng, báo giá, đặt hàng, thanh toán giả lập, đối soát timeout, email giả lập và xem lịch sử đơn.
- SQL Server có đúng 3 bảng `KhachHang`, `DonHang`, `ChiTietDonHang`; view `vwSanPhamLab` chứa danh mục cho lab.

## Kết quả

- Dự án đã build ở cấu hình Release với **0 lỗi, 0 cảnh báo**.
- `Verification.txt` ghi **18/18 kiểm tra đạt**, gồm đăng nhập, giỏ hàng, ngưỡng miễn phí giao hàng, từ chối thanh toán, đối soát, lưu đơn và email.
- Script đã được kiểm tra trên SQL Server: **3 bảng** và **5 sản phẩm trong từng nhóm**. Kiểm tra tích hợp tạo thêm đơn demo, không xóa dữ liệu hiện có.

## Lỗi đã gặp và cách khắc phục

| Lỗi hoặc tình huống | Cách khắc phục |
|---|---|
| LocalDB chưa khởi động hoặc báo không kết nối được | Kiểm tra `Connection.txt`; chạy `sqllocaldb info MSSQLLocalDB`, sau đó `sqllocaldb start MSSQLLocalDB`. Mở lại ứng dụng khi instance đã sẵn sàng. |
| Chạy script có tiếng Việt bằng `sqlcmd` hiển thị sai ký tự | Chạy file UTF-8 với tùy chọn `-f 65001`, hoặc mở script bằng SSMS ở mã hóa UTF-8. |
| Thiếu thư viện `Microsoft.Data.SqlClient` khi build | Cho Visual Studio restore NuGet, kiểm tra kết nối tới nguồn gói trong `NuGet.Config`, rồi build lại. |
| Đổi sang SQL Server Express nhưng không kết nối được | Đổi `Data Source` trong `Connection.txt` sang đúng instance SQL Server và xác nhận Windows Authentication có quyền tạo/sử dụng database. |

## Hướng dẫn để giảng viên kiểm tra và chạy lại

1. Mở `Shopping.slnx`, build Release hoặc bấm **F5**. Dùng tài khoản `demo` / `Demo@123`, hoặc đăng ký tài khoản mới.
2. Trên màn hình chính, chọn lần lượt **Máy ảnh**, **Đồ chơi**, **Gia dụng**, **Máy tính**; mỗi nhóm hiển thị **5 sản phẩm**. Thử xem chi tiết, thêm giỏ, sửa số lượng và xóa sản phẩm.
3. Chọn **Tính tiền**, nhập thông tin người nhận, chọn khu vực và loại giao. Dùng thẻ thử VISA `4111111111111111`, CSV `123`, tên bất kỳ và hạn dùng trong tương lai. Chọn kịch bản thành công, từ chối hoặc timeout; với timeout chọn **Đối soát**.
4. Xem **Đơn đã mua** và file email TXT trong `Shopping/bin/Release/net10.0-windows/Outbox`. Email không chứa số thẻ hay CSV.
5. Trong SSMS, kết nối `(localdb)\MSSQLLocalDB`, chọn database `EShopping_Prototype`, chạy `SELECT NhomSanPham, COUNT(*) AS SoSanPham FROM dbo.vwSanPhamLab GROUP BY NhomSanPham;` để thấy mỗi nhóm 5 sản phẩm. Kiểm tra ba bảng `dbo.KhachHang`, `dbo.DonHang`, `dbo.ChiTietDonHang`.
6. Xem kết quả kiểm tra đã chạy trong `Verification.txt`. Muốn chạy lại từ thư mục `lab4`, dùng `./Shopping/bin/Release/net10.0-windows/Shopping.exe --verify verification-moi.txt`. Lệnh này tạo thêm đơn demo.

## Chạy bằng Visual Studio

1. Mở `Shopping.slnx` (Visual Studio hiện tại trên máy).
2. Chọn project Shopping làm Startup Project nếu cần.
3. Bấm F5. Cần .NET 10 Desktop SDK và SQL Server LocalDB.
4. Chương trình tự tạo CSDL `EShopping_Prototype` với đúng **3 bảng**:
   `KhachHang`, `DonHang`, `ChiTietDonHang`.
5. Đăng nhập **demo / Demo@123**. Tài khoản được tạo nếu chưa tồn tại.

## Chạy bản đã build

Mở `Shopping/bin/Release/net10.0-windows/Shopping.exe`.
Giữ các DLL, Connection.txt và thư mục Database bên cạnh EXE.

## Trình diễn

- Chọn nhóm sản phẩm, xem chi tiết, thêm/cập nhật/xóa giỏ.
- Chọn Tính tiền; đăng nhập hoặc đăng ký.
- Nhập người nhận riêng; chọn khu vực và hình thức giao.
- VISA lab: `4111111111111111`, CSV `123`, tên chủ thẻ bất kỳ,
  hạn dùng ở tương lai. Không sử dụng thẻ thật.
- Chọn kịch bản **Thành công**, **Từ chối** hoặc **Timeout / đối soát**.
- Nếu timeout, bấm Đối soát trong cùng ứng dụng; không tạo yêu cầu thu tiền mới.
- Email giả lập là file TXT trong `Outbox` cạnh EXE.
- Xem lịch sử đơn: danh sách đơn phía trên, chi tiết sản phẩm phía dưới.

## Kiến trúc

Forms → Services → Adapters / Repositories → SQL Server.

Danh mục mẫu dùng model sản phẩm có thật, đọc từ SQL Server qua SqlLabProductAdapter và view dbo.vwSanPhamLab. Biểu phí là cấu hình trong
FeeService. Dịch vụ thanh toán và email là Adapter giả lập. Không có bảng
sản phẩm, bảng phí hoặc bảng thanh toán riêng. Đơn lưu mã giao dịch thành công,
loại thẻ và tiền; không lưu số thẻ đầy đủ hay CSV. Mật khẩu dùng PBKDF2 SHA256,
salt riêng và 100.000 lần lặp.

Mức phí chỉ là số liệu minh họa: nội thành thường/nhanh/trong ngày là
20.000/40.000/70.000 đ; ngoại thành 30.000/60.000/100.000 đ. AMEX có phí
5.000 đ, thẻ khác 0 đ. Ngưỡng miễn phí xét tiền hàng trước các khoản phí.

## SQL

Chạy `Shopping/Database/CuaHangOnlineDB.sql` bằng SSMS nếu muốn tạo CSDL thủ công.
`Connection.txt` mặc định kết nối `(localdb)\MSSQLLocalDB` bằng Windows.
Nếu dùng SQL Express, thay Data Source bằng tên instance của máy. CSDL vẫn
giữ tên EShopping_Prototype. Script không xóa dữ liệu hiện có.

## Kiểm tra đã chạy

`Verification.txt` ghi kết quả kiểm tra tích hợp thực tế. Lệnh chạy lại:

```powershell
.\Shopping\bin\Release\net10.0-windows\Shopping.exe --verify verification.txt
```

Kiểm tra tạo thêm đơn demo. Có thể xem các đơn này trong tài khoản demo.
Các file `.drawio` nằm trong `Docs/Drawio`; mở bằng draw.io để chỉnh hoặc
xuất PNG. Báo cáo giữ ảnh draw.io từ Word gốc; không có ảnh sơ đồ tự vẽ.

## Giới hạn của prototype

Yêu cầu thanh toán chờ đối soát và bản chụp checkout nằm trong bộ nhớ để
giữ giới hạn 3 bảng. Cần hoàn tất đối soát trước khi thoát; muốn phục hồi
sau khi đóng ứng dụng phải bổ sung lưu trữ bền vững. Không có thanh toán,
gửi email hay đặt hàng thật. Không triển khai quản lý giao/hoàn/hủy đơn.

## Cấu trúc Form 

Thư mục `Shopping/Forms` chứa 8 màn hình: FrmTrangChu, FrmChiTietSanPham,
FrmGioHang, FrmDangNhap, FrmDangKy, FrmDatHang, FrmKetQua và FrmLichSuDonHang.
Mỗi màn hình gồm hai file:

- `FrmX.cs`: constructor và các hàm xử lý sự kiện.
- `FrmX.Designer.cs`: khai báo control, Dispose và InitializeComponent.

Mở Solution Explorer → Shopping → Forms. Chọn Form rồi nhấn Shift+F7
để mở Designer, hoặc F7 để xem code. Các Form đều có constructor không
tham số để Designer khởi tạo mà không kết nối cơ sở dữ liệu.

`Infrastructure` chứa LabContext và Ui dùng chung. Các thư mục Services,
Adapters, Models, Data và Database giữ các phần nghiệp vụ và SQL Server.

## Danh mục sản phẩm có thật

Chạy lại ứng dụng để cập nhật view `dbo.vwSanPhamLab` từ `Shopping/Database/CuaHangOnlineDB.sql`. Có thể xem danh mục trong SSMS bằng `SELECT * FROM dbo.vwSanPhamLab;`. Giá bán và tồn hàng là số liệu minh họa cho lab, không phải giá hoặc tồn kho thị trường hiện tại. Đơn cũ giữ nguyên tên và đơn giá lúc đặt.

- [Canon EOS R50 (Body)](https://cam.start.canon/en/C011/manual/html/UG-11_Reference_0090.html)
- [Canon EOS R100 (Body)](https://asia.canon/en/consumer/eos-r100/body/specification)
- [LEGO Creator 31136 Exotic Parrot](https://www.lego.com/en-us/product/exotic-parrot-31136)
- [LEGO Creator 31134 Space Shuttle](https://www.lego.com/en-us/product/space-shuttle-31134)
- [Nồi chiên Philips HD9200/91](https://www.usa.philips.com/c-p/HD9200_91/essential-airfryer)
- [Ấm đun Philips HD9350/90](https://www.philips.com.ar/c-p/HD9350_90/daily-collection-kettle)
- [Bàn phím Logitech K120](https://www.logitech.com/assets/48822/2/logitech-keyboard-k120-quickstart-guide.pdf)
- [Chuột không dây Logitech M185](https://www.logitech.com/en-us/products/mice/m185-wireless-mouse.910-002225.html)

Danh mục gồm 20 sản phẩm: mỗi nhóm Máy ảnh, Đồ chơi, Gia dụng, Máy tính có 5 sản phẩm. Giá và tồn hàng tiếp tục là dữ liệu minh họa.

- [Canon EOS R10 (Body)](https://www.usa.canon.com/shop/p/eos-r10)
- [Canon EOS R7 (Body)](https://www.canon.com.au/get-inspired/eos-r7-and-eos-r10-feature-comparison)
- [Canon EOS R8 (Body)](https://www.canon.com.au/products/full-frame-mirrorless-cameras-eos-r-system)
- [LEGO Creator 31140 Magical Unicorn](https://www.lego.com/en-us/service/building-instructions/search-results?searchString=3114)
- [LEGO Creator 31147 Retro Camera](https://www.lego.com/en-us/service/building-instructions/search-results?searchString=3114)
- [LEGO Creator 31149 Flowers in Watering Can](https://www.lego.com/en-us/product/flowers-in-watering-can-31149)
- [Nồi chiên Philips HD9252/91](https://www.usa.philips.com/c-p/HD9252_91/essential-airfryer)
- [Ấm đun Philips HD9365/10](https://www.philips.com/c-p/HD9365_10/eco-conscious-edition-5000-series-kettle)
- [Máy nướng bánh Philips HD2637/90](https://www.philips.com.ph/c-p/HD2637_90/viva-collection-toaster)
- [Chuột không dây Logitech M170](https://www.logitech.com/en-us/shop/p/m170-wireless-mouse)
- [Bàn phím Bluetooth Logitech K380](https://www.logitech.com/content/dam/logitech/vi/business/pdf/k380-multi-device-bluetooth-keyboard.pdf)
- [Webcam Logitech C270 HD](https://www.logitech.com/en-gb/shop/p/c270-hd-webcam.960-000694)
