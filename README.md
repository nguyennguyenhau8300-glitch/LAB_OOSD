# BÀI THỰC HÀNH - HỆ THỐNG QUẢN LÝ THƯ VIỆN

## 1. Thông tin sinh viên

- Họ và tên: ................................................
- MSSV: ......................................................
- Lớp: .......................................................
- Tên bài Lab: Xây dựng ứng dụng Quản lý thư viện
- Môn học: Phân tích và thiết kế hệ thống hướng đối tượng

---

## 2. Mục tiêu bài thực hành

Xây dựng ứng dụng **Quản lý thư viện** bằng C# Windows Forms, kết nối với cơ sở dữ liệu SQL Server.

Ứng dụng hỗ trợ quản lý các thông tin cơ bản của thư viện như:

- Quản lý đầu sách.
- Quản lý thể loại.
- Quản lý nhà xuất bản.
- Quản lý độc giả.
- Quản lý nhân viên.
- Quản lý mượn và trả sách.
- Theo dõi phiếu mượn.
- Quản lý phiếu phạt.
- Thống kê dữ liệu thư viện.

---

## 3. Môi trường phát triển

- Hệ điều hành: Windows
- IDE: Microsoft Visual Studio
- Ngôn ngữ: C#
- Giao diện: Windows Forms (WinForms)
- Framework: .NET Framework 4.7.2
- Hệ quản trị cơ sở dữ liệu: Microsoft SQL Server
- SQL Server LocalDB: `(LocalDB)\MSSQLLocalDB`
- Thư viện truy cập dữ liệu: `System.Data.SqlClient`
- Quản lý mã nguồn: Git và GitHub

---

## 4. Cấu trúc chương trình

Project được tổ chức thành các thư mục chính:

```text
QuanLyThuVien
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
└── Program.cs
