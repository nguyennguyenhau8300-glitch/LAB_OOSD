-- LAB8: Chọn database QuanLyCaoOcLAB8 trong SSMS trước khi chạy.
-- Ứng dụng tự tạo database cấu hình và thực thi toàn bộ file khi khởi động.
-- Không DROP bảng; nạp mẫu khi chưa có dữ liệu của từng mã.
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
IF DB_NAME() IN ('master','model','msdb','tempdb')
 THROW 50001,N'Chọn database QuanLyCaoOcLAB8 trước khi chạy script.',1;
IF OBJECT_ID('dbo.Tang') IS NULL
BEGIN
CREATE TABLE dbo.Tang(MaTang varchar(20) PRIMARY KEY, SoTang int NOT NULL UNIQUE, TenTang nvarchar(80) NOT NULL);
CREATE TABLE dbo.VanPhong(MaPhong varchar(20) PRIMARY KEY, MaTang varchar(20) NOT NULL REFERENCES dbo.Tang,
 DienTich decimal(12,2) NOT NULL CHECK(DienTich>0), Huong nvarchar(30) NOT NULL,
 GiaThamKhao decimal(18,2) NOT NULL CHECK(GiaThamKhao>0), NgaySuDung date NOT NULL, NgungSuDung bit NOT NULL DEFAULT 0);
CREATE TABLE dbo.KhachHang(MaKH varchar(20) PRIMARY KEY, TenCongTy nvarchar(160) NOT NULL, MaSoThue varchar(20) NOT NULL UNIQUE,
 DaiDien nvarchar(100) NOT NULL, DienThoai varchar(20) NOT NULL);
CREATE TABLE dbo.HopDong(MaHD varchar(20) PRIMARY KEY, MaKH varchar(20) NOT NULL REFERENCES dbo.KhachHang,
 NgayKy date NOT NULL, NgayBatDau date NOT NULL, SoThangGoc int NOT NULL CHECK(SoThangGoc>=6),
 TrangThai varchar(20) NOT NULL CHECK(TrangThai IN('CHO_TRA_TRUOC','HIEU_LUC','HUY','KET_THUC')),
 RequestId uniqueidentifier NOT NULL UNIQUE, CHECK(NgayKy<=NgayBatDau));
CREATE TABLE dbo.ChiTietHopDong(MaCTHD bigint IDENTITY PRIMARY KEY, MaHD varchar(20) NOT NULL REFERENCES dbo.HopDong,
 MaPhong varchar(20) NOT NULL REFERENCES dbo.VanPhong, DonGiaThang decimal(18,2) NOT NULL CHECK(DonGiaThang>0),
 NgayKetThuc date NOT NULL, UNIQUE(MaHD,MaPhong));
CREATE INDEX IX_CTHD_Phong ON dbo.ChiTietHopDong(MaPhong,NgayKetThuc) INCLUDE(MaHD);
CREATE TABLE dbo.PhieuGiaHan(MaPGH varchar(20) PRIMARY KEY, MaHD varchar(20) NOT NULL REFERENCES dbo.HopDong,
 NgayLap datetime2 NOT NULL, NguoiLap nvarchar(100) NOT NULL, RequestId uniqueidentifier NOT NULL UNIQUE);
CREATE TABLE dbo.ChiTietGiaHan(MaCTGH bigint IDENTITY PRIMARY KEY, MaPGH varchar(20) NOT NULL REFERENCES dbo.PhieuGiaHan,
 MaCTHD bigint NOT NULL REFERENCES dbo.ChiTietHopDong, SoThang int NOT NULL CHECK(SoThang>0), HanCu date NOT NULL, HanMoi date NOT NULL,
 DonGiaThang decimal(18,2) NOT NULL CHECK(DonGiaThang>0), UNIQUE(MaPGH,MaCTHD), CHECK(HanMoi>HanCu));
CREATE TABLE dbo.KhoanPhaiThu(MaKhoan bigint IDENTITY PRIMARY KEY, MaCTHD bigint NOT NULL REFERENCES dbo.ChiTietHopDong,
 Loai varchar(10) NOT NULL CHECK(Loai IN('THUE','DICHVU')), KyTu date NOT NULL, KyDen date NOT NULL,
 HanThu date NOT NULL, SoTien decimal(18,2) NOT NULL CHECK(SoTien>=0), UNIQUE(MaCTHD,Loai,KyTu), CHECK(KyDen>KyTu));
CREATE INDEX IX_Khoan_CT ON dbo.KhoanPhaiThu(MaCTHD,Loai,HanThu);
CREATE TABLE dbo.PhieuThu(MaPT varchar(20) PRIMARY KEY, MaKH varchar(20) NOT NULL REFERENCES dbo.KhachHang,
 NgayThu datetime2 NOT NULL, TongTien decimal(18,2) NOT NULL CHECK(TongTien>0), PhuongThuc nvarchar(30) NOT NULL,
 RequestId uniqueidentifier NOT NULL UNIQUE);
CREATE TABLE dbo.PhanBoThu(MaPhanBo bigint IDENTITY PRIMARY KEY, MaPT varchar(20) NOT NULL REFERENCES dbo.PhieuThu,
 MaKhoan bigint NOT NULL REFERENCES dbo.KhoanPhaiThu, SoTien decimal(18,2) NOT NULL CHECK(SoTien>0), UNIQUE(MaPT,MaKhoan));
CREATE INDEX IX_PhanBo_Khoan ON dbo.PhanBoThu(MaKhoan);
CREATE TABLE dbo.DichVu(MaDV varchar(20) PRIMARY KEY, TenDV nvarchar(100) NOT NULL UNIQUE,
 DonViTinh nvarchar(30) NOT NULL, GiaMacDinh decimal(18,2) NOT NULL CHECK(GiaMacDinh>=0));
CREATE TABLE dbo.HoaDonDichVu(MaHDDV varchar(20) PRIMARY KEY, MaCTHD bigint NOT NULL REFERENCES dbo.ChiTietHopDong,
 Thang tinyint NOT NULL CHECK(Thang BETWEEN 1 AND 12), Nam smallint NOT NULL,
 NgayLap datetime2 NOT NULL, NgayChot date NULL, TrangThai varchar(20) NOT NULL CHECK(TrangThai IN('NHAP','PHAT_HANH')),
 MaKhoan bigint NULL REFERENCES dbo.KhoanPhaiThu, UNIQUE(MaCTHD,Thang,Nam));
CREATE UNIQUE INDEX UX_HDDV_Khoan ON dbo.HoaDonDichVu(MaKhoan) WHERE MaKhoan IS NOT NULL;
CREATE TABLE dbo.ChiTietDichVu(MaCTDV bigint IDENTITY PRIMARY KEY, MaHDDV varchar(20) NOT NULL REFERENCES dbo.HoaDonDichVu,
 MaDV varchar(20) NOT NULL REFERENCES dbo.DichVu, SoLuong decimal(12,2) NOT NULL CHECK(SoLuong>=0),
 DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0), UNIQUE(MaHDDV,MaDV));
CREATE TABLE dbo.NhanVienCongTy(MaNV varchar(20) PRIMARY KEY, MaKH varchar(20) NOT NULL REFERENCES dbo.KhachHang,
 MaPhong varchar(20) NOT NULL REFERENCES dbo.VanPhong, HoTen nvarchar(100) NOT NULL, Anh nvarchar(260) NULL,
 ChucVu nvarchar(80) NOT NULL, NgayVao date NOT NULL, NgayNghi date NULL, CHECK(NgayNghi IS NULL OR NgayNghi>=NgayVao));
CREATE TABLE dbo.Xe(MaXe varchar(20) PRIMARY KEY, BienSo varchar(20) NOT NULL UNIQUE, LoaiXe nvarchar(40) NOT NULL, MauXe nvarchar(30) NOT NULL);
CREATE TABLE dbo.TheGiuXe(MaThe varchar(20) PRIMARY KEY, MaNV varchar(20) NOT NULL REFERENCES dbo.NhanVienCongTy,
 MaXe varchar(20) NOT NULL REFERENCES dbo.Xe, NgayCap date NOT NULL, NgayThuHoi date NULL, CHECK(NgayThuHoi IS NULL OR NgayThuHoi>=NgayCap));
CREATE UNIQUE INDEX UX_TheXe_HieuLuc ON dbo.TheGiuXe(MaXe) WHERE NgayThuHoi IS NULL;
CREATE TABLE dbo.PhieuKiemTra(MaPKT varchar(20) PRIMARY KEY, NgayKiemTra date NOT NULL, NguoiKiemTra nvarchar(100) NOT NULL, GhiChu nvarchar(500) NULL);
CREATE TABLE dbo.ChiTietKiemTra(MaCTKT bigint IDENTITY PRIMARY KEY, MaPKT varchar(20) NOT NULL REFERENCES dbo.PhieuKiemTra,
 MaPhong varchar(20) NOT NULL REFERENCES dbo.VanPhong, TinhTrang nvarchar(500) NOT NULL, ThayDoiKetCau bit NOT NULL,
 DeXuat nvarchar(500) NULL, UNIQUE(MaPKT,MaPhong));
CREATE TABLE dbo.PhieuPhat(MaPhat varchar(20) PRIMARY KEY, MaCTKT bigint NOT NULL UNIQUE REFERENCES dbo.ChiTietKiemTra,
 MaCTHD bigint NOT NULL REFERENCES dbo.ChiTietHopDong, LyDo nvarchar(500) NOT NULL, SoTien decimal(18,2) NOT NULL CHECK(SoTien>0),
 HanNop date NOT NULL, DaNop decimal(18,2) NOT NULL DEFAULT 0, CHECK(DaNop>=0 AND DaNop<=SoTien));
END;
GO
CREATE OR ALTER VIEW dbo.vwCongNo AS
SELECT k.MaKhoan,k.MaCTHD,k.Loai,k.KyTu,k.KyDen,k.HanThu,k.SoTien,
 COALESCE(p.DaThu,0) AS DaThu,k.SoTien-COALESCE(p.DaThu,0) AS ConNo
FROM dbo.KhoanPhaiThu k
OUTER APPLY(SELECT SUM(pb.SoTien) DaThu FROM dbo.PhanBoThu pb WHERE pb.MaKhoan=k.MaKhoan)p;
GO

-- Dữ liệu minh họa LAB8, chụp tại 30/10/2026. Chỉ thêm khóa chưa tồn tại.
SET XACT_ABORT ON;
IF NOT EXISTS(SELECT 1 FROM sys.extended_properties WHERE class=0 AND name=N'LAB8_DemoSeed')
BEGIN
IF NOT EXISTS(SELECT 1 FROM dbo.HopDong)
BEGIN
BEGIN TRANSACTION;
IF NOT EXISTS(SELECT 1 FROM dbo.[Tang] WHERE [MaTang]=N'T1') INSERT dbo.[Tang] ([MaTang],[SoTang],[TenTang]) VALUES(N'T1',1,N'Tầng 1');
IF NOT EXISTS(SELECT 1 FROM dbo.[Tang] WHERE [MaTang]=N'T2') INSERT dbo.[Tang] ([MaTang],[SoTang],[TenTang]) VALUES(N'T2',2,N'Tầng 2');
IF NOT EXISTS(SELECT 1 FROM dbo.[Tang] WHERE [MaTang]=N'T3') INSERT dbo.[Tang] ([MaTang],[SoTang],[TenTang]) VALUES(N'T3',3,N'Tầng 3');
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P101') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P101',N'T1',80.00,N'Đông',10000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P102') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P102',N'T1',95.00,N'Tây',12000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P103') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P103',N'T1',60.00,N'Nam',8000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P201') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P201',N'T2',80.00,N'Đông',11000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P202') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P202',N'T2',100.00,N'Bắc',14000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P203') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P203',N'T2',55.00,N'Nam',8500000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P301') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P301',N'T3',120.00,N'Đông',18000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P302') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P302',N'T3',95.00,N'Tây',15000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[VanPhong] WHERE [MaPhong]=N'P303') INSERT dbo.[VanPhong] ([MaPhong],[MaTang],[DienTich],[Huong],[GiaThamKhao],[NgaySuDung],[NgungSuDung]) VALUES(N'P303',N'T3',60.00,N'Bắc',10000000.00,'2025-01-01T00:00:00.000',0);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhachHang] WHERE [MaKH]=N'CT01') INSERT dbo.[KhachHang] ([MaKH],[TenCongTy],[MaSoThue],[DaiDien],[DienThoai]) VALUES(N'CT01',N'Công ty An Phát',N'0310000001',N'Nguyễn An',N'0901000001');
IF NOT EXISTS(SELECT 1 FROM dbo.[KhachHang] WHERE [MaKH]=N'CT02') INSERT dbo.[KhachHang] ([MaKH],[TenCongTy],[MaSoThue],[DaiDien],[DienThoai]) VALUES(N'CT02',N'Công ty Minh Khang',N'0310000002',N'Trần Khang',N'0901000002');
IF NOT EXISTS(SELECT 1 FROM dbo.[KhachHang] WHERE [MaKH]=N'CT03') INSERT dbo.[KhachHang] ([MaKH],[TenCongTy],[MaSoThue],[DaiDien],[DienThoai]) VALUES(N'CT03',N'Công ty Sao Việt',N'0310000003',N'Lê Việt',N'0901000003');
IF NOT EXISTS(SELECT 1 FROM dbo.[HopDong] WHERE [MaHD]=N'HD25babe0a43434ef8') INSERT dbo.[HopDong] ([MaHD],[MaKH],[NgayKy],[NgayBatDau],[SoThangGoc],[TrangThai],[RequestId]) VALUES(N'HD25babe0a43434ef8',N'CT02','2026-04-01T00:00:00.000','2026-04-01T00:00:00.000',6,N'HIEU_LUC','b8b8b8b8-0000-0000-0000-000000000002');
IF NOT EXISTS(SELECT 1 FROM dbo.[HopDong] WHERE [MaHD]=N'HD3837972ba4d24e7f') INSERT dbo.[HopDong] ([MaHD],[MaKH],[NgayKy],[NgayBatDau],[SoThangGoc],[TrangThai],[RequestId]) VALUES(N'HD3837972ba4d24e7f',N'CT01','2026-01-01T00:00:00.000','2026-01-01T00:00:00.000',12,N'HIEU_LUC','b8b8b8b8-0000-0000-0000-000000000001');
IF NOT EXISTS(SELECT 1 FROM dbo.[HopDong] WHERE [MaHD]=N'HD70d14a9599b5481b') INSERT dbo.[HopDong] ([MaHD],[MaKH],[NgayKy],[NgayBatDau],[SoThangGoc],[TrangThai],[RequestId]) VALUES(N'HD70d14a9599b5481b',N'CT03','2026-05-01T00:00:00.000','2026-05-01T00:00:00.000',6,N'HIEU_LUC','b8b8b8b8-0000-0000-0000-000000000003');
SET IDENTITY_INSERT dbo.[ChiTietHopDong] ON;
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietHopDong] WHERE [MaCTHD]=1) INSERT dbo.[ChiTietHopDong] ([MaCTHD],[MaHD],[MaPhong],[DonGiaThang],[NgayKetThuc]) VALUES(1,N'HD3837972ba4d24e7f',N'P101',10000000.00,'2027-01-01T00:00:00.000');
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietHopDong] WHERE [MaCTHD]=2) INSERT dbo.[ChiTietHopDong] ([MaCTHD],[MaHD],[MaPhong],[DonGiaThang],[NgayKetThuc]) VALUES(2,N'HD3837972ba4d24e7f',N'P102',12000000.00,'2027-01-01T00:00:00.000');
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietHopDong] WHERE [MaCTHD]=3) INSERT dbo.[ChiTietHopDong] ([MaCTHD],[MaHD],[MaPhong],[DonGiaThang],[NgayKetThuc]) VALUES(3,N'HD25babe0a43434ef8',N'P201',11000000.00,'2026-11-01T00:00:00.000');
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietHopDong] WHERE [MaCTHD]=4) INSERT dbo.[ChiTietHopDong] ([MaCTHD],[MaHD],[MaPhong],[DonGiaThang],[NgayKetThuc]) VALUES(4,N'HD70d14a9599b5481b',N'P301',18000000.00,'2026-11-01T00:00:00.000');
SET IDENTITY_INSERT dbo.[ChiTietHopDong] OFF;
IF NOT EXISTS(SELECT 1 FROM dbo.[PhieuGiaHan] WHERE [MaPGH]=N'GHfc8ba0dccedd40bd') INSERT dbo.[PhieuGiaHan] ([MaPGH],[MaHD],[NgayLap],[NguoiLap],[RequestId]) VALUES(N'GHfc8ba0dccedd40bd',N'HD25babe0a43434ef8','2026-09-30T00:00:00.000',N'Nguyễn Văn Quản','b8b8b8b8-0000-0000-0000-000000000201');
SET IDENTITY_INSERT dbo.[ChiTietGiaHan] ON;
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietGiaHan] WHERE [MaCTGH]=1) INSERT dbo.[ChiTietGiaHan] ([MaCTGH],[MaPGH],[MaCTHD],[SoThang],[HanCu],[HanMoi],[DonGiaThang]) VALUES(1,N'GHfc8ba0dccedd40bd',3,1,'2026-10-01T00:00:00.000','2026-11-01T00:00:00.000',11000000.00);
SET IDENTITY_INSERT dbo.[ChiTietGiaHan] OFF;
SET IDENTITY_INSERT dbo.[KhoanPhaiThu] ON;
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=1) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(1,1,N'THUE','2026-01-01T00:00:00.000','2026-02-01T00:00:00.000','2026-01-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=2) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(2,1,N'THUE','2026-02-01T00:00:00.000','2026-03-01T00:00:00.000','2026-01-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=3) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(3,1,N'THUE','2026-03-01T00:00:00.000','2026-04-01T00:00:00.000','2026-01-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=4) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(4,1,N'THUE','2026-04-01T00:00:00.000','2026-05-01T00:00:00.000','2026-01-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=5) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(5,1,N'THUE','2026-05-01T00:00:00.000','2026-06-01T00:00:00.000','2026-01-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=6) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(6,1,N'THUE','2026-06-01T00:00:00.000','2026-07-01T00:00:00.000','2026-01-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=7) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(7,1,N'THUE','2026-07-01T00:00:00.000','2026-08-01T00:00:00.000','2026-07-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=8) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(8,1,N'THUE','2026-08-01T00:00:00.000','2026-09-01T00:00:00.000','2026-08-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=9) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(9,1,N'THUE','2026-09-01T00:00:00.000','2026-10-01T00:00:00.000','2026-09-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=10) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(10,1,N'THUE','2026-10-01T00:00:00.000','2026-11-01T00:00:00.000','2026-10-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=11) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(11,1,N'THUE','2026-11-01T00:00:00.000','2026-12-01T00:00:00.000','2026-11-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=12) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(12,1,N'THUE','2026-12-01T00:00:00.000','2027-01-01T00:00:00.000','2026-12-01T00:00:00.000',10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=13) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(13,2,N'THUE','2026-01-01T00:00:00.000','2026-02-01T00:00:00.000','2026-01-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=14) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(14,2,N'THUE','2026-02-01T00:00:00.000','2026-03-01T00:00:00.000','2026-01-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=15) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(15,2,N'THUE','2026-03-01T00:00:00.000','2026-04-01T00:00:00.000','2026-01-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=16) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(16,2,N'THUE','2026-04-01T00:00:00.000','2026-05-01T00:00:00.000','2026-01-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=17) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(17,2,N'THUE','2026-05-01T00:00:00.000','2026-06-01T00:00:00.000','2026-01-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=18) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(18,2,N'THUE','2026-06-01T00:00:00.000','2026-07-01T00:00:00.000','2026-01-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=19) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(19,2,N'THUE','2026-07-01T00:00:00.000','2026-08-01T00:00:00.000','2026-07-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=20) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(20,2,N'THUE','2026-08-01T00:00:00.000','2026-09-01T00:00:00.000','2026-08-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=21) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(21,2,N'THUE','2026-09-01T00:00:00.000','2026-10-01T00:00:00.000','2026-09-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=22) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(22,2,N'THUE','2026-10-01T00:00:00.000','2026-11-01T00:00:00.000','2026-10-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=23) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(23,2,N'THUE','2026-11-01T00:00:00.000','2026-12-01T00:00:00.000','2026-11-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=24) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(24,2,N'THUE','2026-12-01T00:00:00.000','2027-01-01T00:00:00.000','2026-12-01T00:00:00.000',12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=25) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(25,3,N'THUE','2026-04-01T00:00:00.000','2026-05-01T00:00:00.000','2026-04-01T00:00:00.000',11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=26) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(26,3,N'THUE','2026-05-01T00:00:00.000','2026-06-01T00:00:00.000','2026-04-01T00:00:00.000',11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=27) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(27,3,N'THUE','2026-06-01T00:00:00.000','2026-07-01T00:00:00.000','2026-04-01T00:00:00.000',11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=28) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(28,3,N'THUE','2026-07-01T00:00:00.000','2026-08-01T00:00:00.000','2026-04-01T00:00:00.000',11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=29) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(29,3,N'THUE','2026-08-01T00:00:00.000','2026-09-01T00:00:00.000','2026-04-01T00:00:00.000',11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=30) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(30,3,N'THUE','2026-09-01T00:00:00.000','2026-10-01T00:00:00.000','2026-04-01T00:00:00.000',11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=31) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(31,4,N'THUE','2026-05-01T00:00:00.000','2026-06-01T00:00:00.000','2026-05-01T00:00:00.000',18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=32) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(32,4,N'THUE','2026-06-01T00:00:00.000','2026-07-01T00:00:00.000','2026-05-01T00:00:00.000',18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=33) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(33,4,N'THUE','2026-07-01T00:00:00.000','2026-08-01T00:00:00.000','2026-05-01T00:00:00.000',18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=34) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(34,4,N'THUE','2026-08-01T00:00:00.000','2026-09-01T00:00:00.000','2026-05-01T00:00:00.000',18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=35) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(35,4,N'THUE','2026-09-01T00:00:00.000','2026-10-01T00:00:00.000','2026-05-01T00:00:00.000',18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=36) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(36,4,N'THUE','2026-10-01T00:00:00.000','2026-11-01T00:00:00.000','2026-05-01T00:00:00.000',18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=37) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(37,1,N'DICHVU','2026-09-01T00:00:00.000','2026-10-01T00:00:00.000','2026-09-30T00:00:00.000',1300000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[KhoanPhaiThu] WHERE [MaKhoan]=38) INSERT dbo.[KhoanPhaiThu] ([MaKhoan],[MaCTHD],[Loai],[KyTu],[KyDen],[HanThu],[SoTien]) VALUES(38,3,N'THUE','2026-10-01T00:00:00.000','2026-11-01T00:00:00.000','2026-10-01T00:00:00.000',11000000.00);
SET IDENTITY_INSERT dbo.[KhoanPhaiThu] OFF;
IF NOT EXISTS(SELECT 1 FROM dbo.[PhieuThu] WHERE [MaPT]=N'PTbddf127c39a34f5c') INSERT dbo.[PhieuThu] ([MaPT],[MaKH],[NgayThu],[TongTien],[PhuongThuc],[RequestId]) VALUES(N'PTbddf127c39a34f5c',N'CT02','2026-04-01T00:00:00.000',66000000.00,N'Chuyển khoản','b8b8b8b8-0000-0000-0000-000000000102');
IF NOT EXISTS(SELECT 1 FROM dbo.[PhieuThu] WHERE [MaPT]=N'PTc06b6e15bc8d4575') INSERT dbo.[PhieuThu] ([MaPT],[MaKH],[NgayThu],[TongTien],[PhuongThuc],[RequestId]) VALUES(N'PTc06b6e15bc8d4575',N'CT01','2026-01-01T00:00:00.000',132000000.00,N'Chuyển khoản','b8b8b8b8-0000-0000-0000-000000000101');
IF NOT EXISTS(SELECT 1 FROM dbo.[PhieuThu] WHERE [MaPT]=N'PTec888e97c69a4056') INSERT dbo.[PhieuThu] ([MaPT],[MaKH],[NgayThu],[TongTien],[PhuongThuc],[RequestId]) VALUES(N'PTec888e97c69a4056',N'CT03','2026-05-01T00:00:00.000',108000000.00,N'Chuyển khoản','b8b8b8b8-0000-0000-0000-000000000103');
SET IDENTITY_INSERT dbo.[PhanBoThu] ON;
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=1) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(1,N'PTc06b6e15bc8d4575',1,10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=2) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(2,N'PTc06b6e15bc8d4575',2,10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=3) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(3,N'PTc06b6e15bc8d4575',3,10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=4) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(4,N'PTc06b6e15bc8d4575',4,10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=5) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(5,N'PTc06b6e15bc8d4575',5,10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=6) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(6,N'PTc06b6e15bc8d4575',6,10000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=7) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(7,N'PTc06b6e15bc8d4575',13,12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=8) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(8,N'PTc06b6e15bc8d4575',14,12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=9) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(9,N'PTc06b6e15bc8d4575',15,12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=10) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(10,N'PTc06b6e15bc8d4575',16,12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=11) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(11,N'PTc06b6e15bc8d4575',17,12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=12) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(12,N'PTc06b6e15bc8d4575',18,12000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=13) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(13,N'PTbddf127c39a34f5c',25,11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=14) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(14,N'PTbddf127c39a34f5c',26,11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=15) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(15,N'PTbddf127c39a34f5c',27,11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=16) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(16,N'PTbddf127c39a34f5c',28,11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=17) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(17,N'PTbddf127c39a34f5c',29,11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=18) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(18,N'PTbddf127c39a34f5c',30,11000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=19) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(19,N'PTec888e97c69a4056',31,18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=20) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(20,N'PTec888e97c69a4056',32,18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=21) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(21,N'PTec888e97c69a4056',33,18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=22) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(22,N'PTec888e97c69a4056',34,18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=23) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(23,N'PTec888e97c69a4056',35,18000000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhanBoThu] WHERE [MaPhanBo]=24) INSERT dbo.[PhanBoThu] ([MaPhanBo],[MaPT],[MaKhoan],[SoTien]) VALUES(24,N'PTec888e97c69a4056',36,18000000.00);
SET IDENTITY_INSERT dbo.[PhanBoThu] OFF;
IF NOT EXISTS(SELECT 1 FROM dbo.[DichVu] WHERE [MaDV]=N'DIEN') INSERT dbo.[DichVu] ([MaDV],[TenDV],[DonViTinh],[GiaMacDinh]) VALUES(N'DIEN',N'Điện',N'kWh',3000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[DichVu] WHERE [MaDV]=N'NET') INSERT dbo.[DichVu] ([MaDV],[TenDV],[DonViTinh],[GiaMacDinh]) VALUES(N'NET',N'Internet',N'Tháng',250000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[DichVu] WHERE [MaDV]=N'NUOC') INSERT dbo.[DichVu] ([MaDV],[TenDV],[DonViTinh],[GiaMacDinh]) VALUES(N'NUOC',N'Nước',N'm³',15000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DV00a8bde09bfa461a') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DV00a8bde09bfa461a',4,10,2026,'2026-10-30T00:00:00.000','2026-10-30T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DV070eb9f26e3b417c') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DV070eb9f26e3b417c',4,6,2026,'2026-06-30T00:00:00.000','2026-06-30T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DV0859b5c2eda44c88') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DV0859b5c2eda44c88',4,5,2026,'2026-05-31T00:00:00.000','2026-05-31T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DV6bfcb95896874438') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DV6bfcb95896874438',3,4,2026,'2026-04-30T00:00:00.000','2026-04-30T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DV7c4294349095400e') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DV7c4294349095400e',1,9,2026,'2026-09-30T00:00:00.000','2026-09-30T00:00:00.000',N'PHAT_HANH',37);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DV8908b00a29bc4a32') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DV8908b00a29bc4a32',3,7,2026,'2026-07-31T00:00:00.000','2026-07-31T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVbc4a94aeaaf94e1a') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVbc4a94aeaaf94e1a',1,10,2026,'2026-10-01T00:00:00.000',NULL,N'NHAP',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVd0dafea130784178') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVd0dafea130784178',3,5,2026,'2026-05-31T00:00:00.000','2026-05-31T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVd5bc6d5b2a794542') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVd5bc6d5b2a794542',4,8,2026,'2026-08-31T00:00:00.000','2026-08-31T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVd7bee851a50547a9') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVd7bee851a50547a9',3,9,2026,'2026-09-30T00:00:00.000','2026-09-30T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVdc72e92bf8af46f6') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVdc72e92bf8af46f6',3,8,2026,'2026-08-31T00:00:00.000','2026-08-31T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVdec263214e284044') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVdec263214e284044',4,7,2026,'2026-07-31T00:00:00.000','2026-07-31T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVe01559ea5ae44428') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVe01559ea5ae44428',3,6,2026,'2026-06-30T00:00:00.000','2026-06-30T00:00:00.000',N'PHAT_HANH',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[HoaDonDichVu] WHERE [MaHDDV]=N'DVef1ec25784db46ab') INSERT dbo.[HoaDonDichVu] ([MaHDDV],[MaCTHD],[Thang],[Nam],[NgayLap],[NgayChot],[TrangThai],[MaKhoan]) VALUES(N'DVef1ec25784db46ab',4,9,2026,'2026-09-30T00:00:00.000','2026-09-30T00:00:00.000',N'PHAT_HANH',NULL);
SET IDENTITY_INSERT dbo.[ChiTietDichVu] ON;
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietDichVu] WHERE [MaCTDV]=1) INSERT dbo.[ChiTietDichVu] ([MaCTDV],[MaHDDV],[MaDV],[SoLuong],[DonGia]) VALUES(1,N'DV7c4294349095400e',N'DIEN',300.00,3000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietDichVu] WHERE [MaCTDV]=2) INSERT dbo.[ChiTietDichVu] ([MaCTDV],[MaHDDV],[MaDV],[SoLuong],[DonGia]) VALUES(2,N'DV7c4294349095400e',N'NUOC',10.00,15000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietDichVu] WHERE [MaCTDV]=3) INSERT dbo.[ChiTietDichVu] ([MaCTDV],[MaHDDV],[MaDV],[SoLuong],[DonGia]) VALUES(3,N'DV7c4294349095400e',N'NET',1.00,250000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietDichVu] WHERE [MaCTDV]=4) INSERT dbo.[ChiTietDichVu] ([MaCTDV],[MaHDDV],[MaDV],[SoLuong],[DonGia]) VALUES(4,N'DVbc4a94aeaaf94e1a',N'DIEN',150.00,3000.00);
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietDichVu] WHERE [MaCTDV]=5) INSERT dbo.[ChiTietDichVu] ([MaCTDV],[MaHDDV],[MaDV],[SoLuong],[DonGia]) VALUES(5,N'DVbc4a94aeaaf94e1a',N'NET',1.00,250000.00);
SET IDENTITY_INSERT dbo.[ChiTietDichVu] OFF;
IF NOT EXISTS(SELECT 1 FROM dbo.[NhanVienCongTy] WHERE [MaNV]=N'NV01') INSERT dbo.[NhanVienCongTy] ([MaNV],[MaKH],[MaPhong],[HoTen],[Anh],[ChucVu],[NgayVao],[NgayNghi]) VALUES(N'NV01',N'CT01',N'P101',N'Nguyễn Minh Anh',NULL,N'Trưởng phòng','2026-01-01T00:00:00.000',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[NhanVienCongTy] WHERE [MaNV]=N'NV02') INSERT dbo.[NhanVienCongTy] ([MaNV],[MaKH],[MaPhong],[HoTen],[Anh],[ChucVu],[NgayVao],[NgayNghi]) VALUES(N'NV02',N'CT03',N'P301',N'Trần Quốc Bảo',NULL,N'Nhân viên','2026-05-01T00:00:00.000',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[Xe] WHERE [MaXe]=N'XEe6d6fa0ad70c42f9') INSERT dbo.[Xe] ([MaXe],[BienSo],[LoaiXe],[MauXe]) VALUES(N'XEe6d6fa0ad70c42f9',N'59A1-12345',N'Xe máy',N'Đen');
IF NOT EXISTS(SELECT 1 FROM dbo.[TheGiuXe] WHERE [MaThe]=N'THE001') INSERT dbo.[TheGiuXe] ([MaThe],[MaNV],[MaXe],[NgayCap],[NgayThuHoi]) VALUES(N'THE001',N'NV01',N'XEe6d6fa0ad70c42f9','2026-01-02T00:00:00.000',NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.[PhieuKiemTra] WHERE [MaPKT]=N'KTb579dc9aa5604922') INSERT dbo.[PhieuKiemTra] ([MaPKT],[NgayKiemTra],[NguoiKiemTra],[GhiChu]) VALUES(N'KTb579dc9aa5604922','2026-09-01T00:00:00.000',N'Nguyễn Văn Kỹ',N'Kiểm tra định kỳ');
SET IDENTITY_INSERT dbo.[ChiTietKiemTra] ON;
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietKiemTra] WHERE [MaCTKT]=1) INSERT dbo.[ChiTietKiemTra] ([MaCTKT],[MaPKT],[MaPhong],[TinhTrang],[ThayDoiKetCau],[DeXuat]) VALUES(1,N'KTb579dc9aa5604922',N'P101',N'Phòng sử dụng bình thường',0,N'Kiểm tra máy lạnh');
IF NOT EXISTS(SELECT 1 FROM dbo.[ChiTietKiemTra] WHERE [MaCTKT]=2) INSERT dbo.[ChiTietKiemTra] ([MaCTKT],[MaPKT],[MaPhong],[TinhTrang],[ThayDoiKetCau],[DeXuat]) VALUES(2,N'KTb579dc9aa5604922',N'P201',N'Tự thay đổi vách ngăn',1,N'Khôi phục theo thiết kế');
SET IDENTITY_INSERT dbo.[ChiTietKiemTra] OFF;
IF NOT EXISTS(SELECT 1 FROM dbo.[PhieuPhat] WHERE [MaPhat]=N'PHd2f499e778344010') INSERT dbo.[PhieuPhat] ([MaPhat],[MaCTKT],[MaCTHD],[LyDo],[SoTien],[HanNop],[DaNop]) VALUES(N'PHd2f499e778344010',2,3,N'Thay đổi vách ngăn không được phép',500000.00,'2026-09-15T00:00:00.000',0.00);
COMMIT TRANSACTION;
END;
EXEC sys.sp_addextendedproperty @name=N'LAB8_DemoSeed',@value=N'2026-10-30';
END;
GO

