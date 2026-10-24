SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
GO
IF DB_ID(N'QuanLyNhanSuLAB7') IS NULL CREATE DATABASE QuanLyNhanSuLAB7;
GO
USE QuanLyNhanSuLAB7;
GO
IF OBJECT_ID('dbo.PhongBan') IS NULL CREATE TABLE PhongBan(Id int IDENTITY PRIMARY KEY, Ten nvarchar(120) NOT NULL UNIQUE);
IF OBJECT_ID('dbo.ChucVu') IS NULL CREATE TABLE ChucVu(Id int IDENTITY PRIMARY KEY, Ten nvarchar(120) NOT NULL UNIQUE);
IF OBJECT_ID('dbo.CaLam') IS NULL CREATE TABLE CaLam(Id int IDENTITY PRIMARY KEY, Ten nvarchar(120) NOT NULL UNIQUE, GioBatDau time NOT NULL, GioKetThuc time NOT NULL, CHECK(GioBatDau<>GioKetThuc));
IF OBJECT_ID('dbo.NhanVien') IS NULL CREATE TABLE NhanVien(Id int IDENTITY PRIMARY KEY, HoTen nvarchar(120) NOT NULL, NgaySinh date NOT NULL, DienThoai nvarchar(25) NOT NULL, DiaChi nvarchar(250) NOT NULL, PhongBanId int NOT NULL REFERENCES PhongBan(Id), TrangThai nvarchar(30) NOT NULL DEFAULT N'Chờ thử việc' CHECK(TrangThai IN(N'Chờ thử việc',N'Đang thử việc',N'Đạt thử việc',N'Không đạt',N'Chính thức')));
IF OBJECT_ID('dbo.TuyenDung') IS NULL CREATE TABLE TuyenDung(Id int IDENTITY PRIMARY KEY, NhanVienId int NOT NULL UNIQUE REFERENCES NhanVien(Id), SoQD nvarchar(40) NOT NULL UNIQUE, TuNgay date NOT NULL, DenNgay date NOT NULL, LuongThuViec decimal(18,2) NOT NULL CHECK(LuongThuViec>0), DieuKhoan nvarchar(1000) NOT NULL, KetQua nvarchar(30) NOT NULL DEFAULT N'Chưa đánh giá' CHECK(KetQua IN(N'Chưa đánh giá',N'Đạt',N'Không đạt')), CHECK(DenNgay>=TuNgay));
IF OBJECT_ID('dbo.HopDong') IS NULL CREATE TABLE HopDong(Id int IDENTITY PRIMARY KEY, NhanVienId int NOT NULL REFERENCES NhanVien(Id), SoHD nvarchar(40) NOT NULL UNIQUE, Loai nvarchar(30) NOT NULL CHECK(Loai IN(N'Dài hạn',N'Ngắn hạn')), NgayKy date NOT NULL, TuNgay date NOT NULL, DenNgay date NULL, DieuKhoan nvarchar(1000) NOT NULL, TrangThai nvarchar(20) NOT NULL DEFAULT N'Chờ ký' CHECK(TrangThai IN(N'Chờ ký',N'Đã ký')), CHECK(DenNgay IS NULL OR DenNgay>=TuNgay), CHECK(Loai<>N'Ngắn hạn' OR DenNgay IS NOT NULL));
IF OBJECT_ID('dbo.QDLuong') IS NULL CREATE TABLE QDLuong(Id int IDENTITY PRIMARY KEY, NhanVienId int NOT NULL REFERENCES NhanVien(Id), SoQD nvarchar(40) NOT NULL UNIQUE, NgayHieuLuc date NOT NULL, LuongCoBan decimal(18,2) NOT NULL CHECK(LuongCoBan>0), PhuCapLuong decimal(18,2) NOT NULL CHECK(PhuCapLuong>=0), PhuCapDocHai decimal(18,2) NOT NULL CHECK(PhuCapDocHai>=0), NoiDung nvarchar(1000) NOT NULL, UNIQUE(NhanVienId,NgayHieuLuc));
IF OBJECT_ID('dbo.QDChucVu') IS NULL CREATE TABLE QDChucVu(Id int IDENTITY PRIMARY KEY, NhanVienId int NOT NULL REFERENCES NhanVien(Id), ChucVuId int NOT NULL REFERENCES ChucVu(Id), SoQD nvarchar(40) NOT NULL UNIQUE, NgayHieuLuc date NOT NULL, PhuCap decimal(18,2) NOT NULL CHECK(PhuCap>=0), NoiDung nvarchar(1000) NOT NULL, UNIQUE(NhanVienId,NgayHieuLuc));
IF OBJECT_ID('dbo.BaoHiem') IS NULL CREATE TABLE BaoHiem(Id int IDENTITY PRIMARY KEY, NhanVienId int NOT NULL REFERENCES NhanVien(Id), Loai nvarchar(10) NOT NULL CHECK(Loai IN(N'BHYT',N'BHXH')), SoBH nvarchar(50) NOT NULL UNIQUE, TuNgay date NOT NULL, DenNgay date NULL, TyLe decimal(9,4) NOT NULL CHECK(TyLe>=0 AND TyLe<=100), CHECK(DenNgay IS NULL OR DenNgay>=TuNgay));
IF OBJECT_ID('dbo.PhanCong') IS NULL CREATE TABLE PhanCong(Id int IDENTITY PRIMARY KEY, NhanVienId int NOT NULL REFERENCES NhanVien(Id), CaLamId int NOT NULL REFERENCES CaLam(Id), NgayLam date NOT NULL, GioPhuTroiDuKien decimal(5,2) NOT NULL CHECK(GioPhuTroiDuKien>=0 AND GioPhuTroiDuKien<=16), UNIQUE(NhanVienId,NgayLam,CaLamId));
IF OBJECT_ID('dbo.ChamCong') IS NULL CREATE TABLE ChamCong(Id int IDENTITY PRIMARY KEY, PhanCongId int NOT NULL UNIQUE REFERENCES PhanCong(Id), SoGio decimal(5,2) NOT NULL CHECK(SoGio>=0 AND SoGio<=24), GhiChu nvarchar(300) NOT NULL);
IF OBJECT_ID('dbo.PhieuPhuTroi') IS NULL CREATE TABLE PhieuPhuTroi(Id int IDENTITY PRIMARY KEY, SoPhieu nvarchar(40) NOT NULL UNIQUE, Ngay date NOT NULL, HinhThuc nvarchar(30) NOT NULL CHECK(HinhThuc IN(N'Tăng ca',N'Ngày lễ',N'Ngày nghỉ')), SoGio decimal(5,2) NOT NULL CHECK(SoGio>0 AND SoGio<=24));
IF OBJECT_ID('dbo.ChiTietPhuTroi') IS NULL CREATE TABLE ChiTietPhuTroi(Id int IDENTITY PRIMARY KEY, PhieuId int NOT NULL REFERENCES PhieuPhuTroi(Id), NhanVienId int NOT NULL REFERENCES NhanVien(Id), SoGio decimal(5,2) NOT NULL CHECK(SoGio>0 AND SoGio<=24), UNIQUE(PhieuId,NhanVienId));
IF OBJECT_ID('dbo.BangLuong') IS NULL CREATE TABLE BangLuong(Id int IDENTITY PRIMARY KEY, Thang int NOT NULL CHECK(Thang BETWEEN 1 AND 12), Nam int NOT NULL CHECK(Nam BETWEEN 2000 AND 2100), GioChuan decimal(8,2) NOT NULL CHECK(GioChuan>0), TrangThai nvarchar(20) NOT NULL DEFAULT N'Nháp' CHECK(TrangThai IN(N'Nháp',N'Đã chốt')), NgayLap datetime2 NOT NULL DEFAULT SYSDATETIME(), UNIQUE(Thang,Nam));
IF OBJECT_ID('dbo.ThuongPhat') IS NULL CREATE TABLE ThuongPhat(Id int IDENTITY PRIMARY KEY, SoQD nvarchar(40) NOT NULL UNIQUE, Loai nvarchar(20) NOT NULL CHECK(Loai IN(N'Khen thưởng',N'Kỷ luật')), NhanVienId int NULL REFERENCES NhanVien(Id), PhongBanId int NULL REFERENCES PhongBan(Id), Ngay date NOT NULL, LyDo nvarchar(1000) NOT NULL, SoTien decimal(18,2) NOT NULL CHECK(SoTien>=0), PhuongThuc nvarchar(20) NOT NULL CHECK(PhuongThuc IN(N'Trực tiếp',N'Qua lương')), TrangThai nvarchar(20) NOT NULL DEFAULT N'Chờ ký' CHECK(TrangThai IN(N'Chờ ký',N'Đã ký')), BangLuongId int NULL REFERENCES BangLuong(Id), CHECK((NhanVienId IS NOT NULL AND PhongBanId IS NULL) OR (NhanVienId IS NULL AND PhongBanId IS NOT NULL)), CHECK(PhongBanId IS NULL OR PhuongThuc=N'Trực tiếp'));
IF OBJECT_ID('dbo.ThuChi') IS NULL CREATE TABLE ThuChi(Id int IDENTITY PRIMARY KEY, QuyetDinhId int NOT NULL UNIQUE REFERENCES ThuongPhat(Id), Ngay date NOT NULL, SoTien decimal(18,2) NOT NULL CHECK(SoTien>0), Loai nvarchar(10) NOT NULL CHECK(Loai IN(N'Thu',N'Chi')), BangLuongId int NULL REFERENCES BangLuong(Id));
IF OBJECT_ID('dbo.ChiTietLuong') IS NULL CREATE TABLE ChiTietLuong(Id int IDENTITY PRIMARY KEY, BangLuongId int NOT NULL REFERENCES BangLuong(Id), NhanVienId int NOT NULL REFERENCES NhanVien(Id), GioCong decimal(10,2) NOT NULL, GioPhuTroi decimal(10,2) NOT NULL, LuongTheoCong decimal(18,2) NOT NULL, PhuCap decimal(18,2) NOT NULL, TienPhuTroi decimal(18,2) NOT NULL, BaoHiem decimal(18,2) NOT NULL, Thuong decimal(18,2) NOT NULL, Phat decimal(18,2) NOT NULL, ThucLinh AS (LuongTheoCong+PhuCap+TienPhuTroi-BaoHiem+Thuong-Phat) PERSISTED, UNIQUE(BangLuongId,NhanVienId));
GO
IF NOT EXISTS(SELECT 1 FROM PhongBan)
BEGIN
INSERT PhongBan(Ten) VALUES(N'Tài chính kế toán'),(N'Kỹ thuật'),(N'Sản xuất');
INSERT ChucVu(Ten) VALUES(N'Nhân viên'),(N'Trưởng phòng'),(N'Tổ trưởng');
INSERT CaLam(Ten,GioBatDau,GioKetThuc) VALUES(N'Ca sáng','08:00','12:00'),(N'Ca chiều','13:00','17:00');
END;
GO
-- Dữ liệu trình diễn tháng 09/2026. Không thêm trùng khi chạy lại.
IF NOT EXISTS(SELECT 1 FROM TuyenDung WHERE SoQD=N'TD-2026-001')
BEGIN
BEGIN TRANSACTION;
DECLARE @pb int=(SELECT MIN(Id) FROM PhongBan),@cv int=(SELECT MIN(Id) FROM ChucVu),@ca1 int=(SELECT MIN(Id) FROM CaLam),@ca2 int=(SELECT MAX(Id) FROM CaLam);
INSERT NhanVien(HoTen,NgaySinh,DienThoai,DiaChi,PhongBanId,TrangThai) VALUES(N'Nguyễn Minh Anh','19980512',N'0901234567',N'TP.HCM',@pb,N'Chính thức');
DECLARE @nv1 int=SCOPE_IDENTITY();
INSERT NhanVien(HoTen,NgaySinh,DienThoai,DiaChi,PhongBanId,TrangThai) VALUES(N'Trần Quốc Bảo','19961020',N'0912345678',N'TP.HCM',@pb,N'Chính thức');
DECLARE @nv2 int=SCOPE_IDENTITY();
INSERT NhanVien(HoTen,NgaySinh,DienThoai,DiaChi,PhongBanId,TrangThai) VALUES(N'Lê Hoàng Hà','20010315',N'0923456789',N'Đồng Nai',@pb,N'Đang thử việc');
DECLARE @nv3 int=SCOPE_IDENTITY();
INSERT TuyenDung(NhanVienId,SoQD,TuNgay,DenNgay,LuongThuViec,DieuKhoan,KetQua) VALUES
(@nv1,N'TD-2026-001','20260701','20260731',7800000,N'Thử việc tại phòng ban tiếp nhận',N'Đạt'),
(@nv2,N'TD-2026-002','20260701','20260731',7500000,N'Thử việc tại phòng ban tiếp nhận',N'Đạt'),
(@nv3,N'TD-2026-003','20260901','20261031',7800000,N'Thử việc hai tháng, đánh giá sau khi hết hạn',N'Chưa đánh giá');
INSERT HopDong(NhanVienId,SoHD,Loai,NgayKy,TuNgay,DenNgay,DieuKhoan,TrangThai) VALUES
(@nv1,N'HD-2026-001',N'Dài hạn','20260801','20260801',NULL,N'Hợp đồng dài hạn, có BHYT và BHXH',N'Đã ký'),
(@nv2,N'HD-2026-002',N'Ngắn hạn','20260801','20260801','20261231',N'Hợp đồng ngắn hạn theo đề bài',N'Đã ký');
INSERT QDLuong(NhanVienId,SoQD,NgayHieuLuc,LuongCoBan,PhuCapLuong,PhuCapDocHai,NoiDung) VALUES
(@nv1,N'LG-2026-001','20260801',10400000,1040000,0,N'Lương chính thức'),
(@nv2,N'LG-2026-002','20260801',9360000,520000,0,N'Lương chính thức');
INSERT QDChucVu(NhanVienId,ChucVuId,SoQD,NgayHieuLuc,PhuCap,NoiDung) VALUES(@nv1,@cv,N'CV-2026-001','20260801',520000,N'Phụ cấp chức vụ');
INSERT BaoHiem(NhanVienId,Loai,SoBH,TuNgay,DenNgay,TyLe) VALUES(@nv1,N'BHYT',N'YT-2026-001','20260801',NULL,1.5),(@nv1,N'BHXH',N'XH-2026-001','20260801',NULL,8);
DECLARE @i int=0,@day date,@pc int;
WHILE @i<20
BEGIN
SET @day=DATEADD(day,@i,'20260901');
INSERT PhanCong(NhanVienId,CaLamId,NgayLam,GioPhuTroiDuKien) VALUES(@nv1,@ca1,@day,0),(@nv1,@ca2,@day,0),(@nv2,@ca1,@day,0),(@nv2,@ca2,@day,0),(@nv3,@ca1,@day,0),(@nv3,@ca2,@day,0);
INSERT ChamCong(PhanCongId,SoGio,GhiChu) SELECT Id,4,N'' FROM PhanCong WHERE NgayLam=@day AND NhanVienId IN(@nv1,@nv2,@nv3);
SET @i=@i+1;
END;
INSERT PhieuPhuTroi(SoPhieu,Ngay,HinhThuc,SoGio) VALUES(N'PT-2026-001','20260910',N'Tăng ca',2);
DECLARE @pt int=SCOPE_IDENTITY();
INSERT ChiTietPhuTroi(PhieuId,NhanVienId,SoGio) VALUES(@pt,@nv1,2),(@pt,@nv2,2);
INSERT ThuongPhat(SoQD,Loai,NhanVienId,PhongBanId,Ngay,LyDo,SoTien,PhuongThuc,TrangThai) VALUES
(N'KT-2026-001',N'Khen thưởng',@nv1,NULL,'20260920',N'Hoàn thành tốt công việc',500000,N'Qua lương',N'Đã ký'),
(N'KL-2026-001',N'Kỷ luật',@nv2,NULL,'20260920',N'Khoản xử phạt theo tình huống bài lab',100000,N'Qua lương',N'Đã ký'),
(N'KT-2026-002',N'Khen thưởng',NULL,@pb,'20260920',N'Tập thể hoàn thành kế hoạch',2000000,N'Trực tiếp',N'Chờ ký');
COMMIT;
END;
GO


-- Bổ sung dữ liệu tháng 10/2026; chỉ thêm các chứng từ mẫu chưa có.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
DECLARE @oct1 int=(SELECT NhanVienId FROM TuyenDung WHERE SoQD=N'TD-2026-001'),
        @oct2 int=(SELECT NhanVienId FROM TuyenDung WHERE SoQD=N'TD-2026-002'),
        @oct3 int=(SELECT NhanVienId FROM TuyenDung WHERE SoQD=N'TD-2026-003');
IF @oct1 IS NOT NULL AND @oct2 IS NOT NULL AND @oct3 IS NOT NULL
BEGIN
    ;WITH Days AS (
        SELECT CAST('20261001' AS date) AS Ngay
        UNION ALL SELECT DATEADD(day,1,Ngay) FROM Days WHERE Ngay<'20261020'
    ), Employees AS (
        SELECT @oct1 Id UNION ALL SELECT @oct2 UNION ALL SELECT @oct3
    )
    INSERT PhanCong(NhanVienId,CaLamId,NgayLam,GioPhuTroiDuKien)
    SELECT n.Id,c.Id,d.Ngay,0 FROM Employees n CROSS JOIN Days d CROSS JOIN CaLam c
    WHERE c.Ten IN(N'Ca sáng',N'Ca chiều')
      AND NOT EXISTS(SELECT 1 FROM PhanCong p WHERE p.NhanVienId=n.Id AND p.CaLamId=c.Id AND p.NgayLam=d.Ngay);
    INSERT ChamCong(PhanCongId,SoGio,GhiChu)
    SELECT p.Id,4,N'Dữ liệu mẫu tháng 10/2026' FROM PhanCong p JOIN CaLam c ON c.Id=p.CaLamId
    WHERE p.NhanVienId IN(@oct1,@oct2,@oct3) AND p.NgayLam BETWEEN '20261001' AND '20261020'
      AND c.Ten IN(N'Ca sáng',N'Ca chiều') AND NOT EXISTS(SELECT 1 FROM ChamCong cc WHERE cc.PhanCongId=p.Id);
    IF NOT EXISTS(SELECT 1 FROM PhieuPhuTroi WHERE SoPhieu=N'PT-2026-010')
        INSERT PhieuPhuTroi(SoPhieu,Ngay,HinhThuc,SoGio) VALUES(N'PT-2026-010','20261010',N'Tăng ca',3);
    DECLARE @octpt int=(SELECT Id FROM PhieuPhuTroi WHERE SoPhieu=N'PT-2026-010');
    IF NOT EXISTS(SELECT 1 FROM ChiTietPhuTroi WHERE PhieuId=@octpt AND NhanVienId=@oct1)
        INSERT ChiTietPhuTroi(PhieuId,NhanVienId,SoGio) VALUES(@octpt,@oct1,3);
    IF NOT EXISTS(SELECT 1 FROM ChiTietPhuTroi WHERE PhieuId=@octpt AND NhanVienId=@oct2)
        INSERT ChiTietPhuTroi(PhieuId,NhanVienId,SoGio) VALUES(@octpt,@oct2,2);
    DECLARE @octpb int=(SELECT PhongBanId FROM NhanVien WHERE Id=@oct1);
    IF NOT EXISTS(SELECT 1 FROM ThuongPhat WHERE SoQD=N'KT-2026-010')
        INSERT ThuongPhat(SoQD,Loai,NhanVienId,PhongBanId,Ngay,LyDo,SoTien,PhuongThuc,TrangThai)
        VALUES(N'KT-2026-010',N'Khen thưởng',@oct1,NULL,'20261020',N'Hoàn thành công việc tháng 10',300000,N'Qua lương',N'Đã ký');
    IF NOT EXISTS(SELECT 1 FROM ThuongPhat WHERE SoQD=N'KL-2026-010')
        INSERT ThuongPhat(SoQD,Loai,NhanVienId,PhongBanId,Ngay,LyDo,SoTien,PhuongThuc,TrangThai)
        VALUES(N'KL-2026-010',N'Kỷ luật',@oct2,NULL,'20261020',N'Khoản kỷ luật mẫu tháng 10',50000,N'Qua lương',N'Đã ký');
    IF NOT EXISTS(SELECT 1 FROM ThuongPhat WHERE SoQD=N'KT-2026-011')
        INSERT ThuongPhat(SoQD,Loai,NhanVienId,PhongBanId,Ngay,LyDo,SoTien,PhuongThuc,TrangThai)
        VALUES(N'KT-2026-011',N'Khen thưởng',NULL,@octpb,'20261021',N'Tập thể hoàn thành kế hoạch tháng 10',1000000,N'Trực tiếp',N'Đã ký');
    IF NOT EXISTS(SELECT 1 FROM ThuongPhat WHERE SoQD=N'KL-2026-011')
        INSERT ThuongPhat(SoQD,Loai,NhanVienId,PhongBanId,Ngay,LyDo,SoTien,PhuongThuc,TrangThai)
        VALUES(N'KL-2026-011',N'Kỷ luật',@oct2,NULL,'20261021',N'Phiếu thu trực tiếp minh họa',150000,N'Trực tiếp',N'Đã ký');
    INSERT ThuChi(QuyetDinhId,Ngay,SoTien,Loai,BangLuongId)
    SELECT q.Id,q.Ngay,q.SoTien,CASE WHEN q.Loai=N'Khen thưởng' THEN N'Chi' ELSE N'Thu' END,NULL
    FROM ThuongPhat q WHERE q.SoQD IN(N'KT-2026-011',N'KL-2026-011')
      AND NOT EXISTS(SELECT 1 FROM ThuChi t WHERE t.QuyetDinhId=q.Id);
END;
COMMIT;
GO

-- Bảng lương nháp mẫu 09 và 10/2026; số liệu đối chiếu từ PayrollService.
-- Chỉ tạo tháng chưa có; không ghi đè tháng người dùng đã tính hoặc chốt.
BEGIN TRANSACTION;
DECLARE @seedPayroll int;
IF NOT EXISTS(SELECT 1 FROM BangLuong WHERE Thang=9 AND Nam=2026)
BEGIN
    INSERT BangLuong(Thang,Nam,GioChuan) VALUES(9,2026,208);
    SET @seedPayroll=SCOPE_IDENTITY();
    INSERT ChiTietLuong(BangLuongId,NhanVienId,GioCong,GioPhuTroi,LuongTheoCong,PhuCap,TienPhuTroi,BaoHiem,Thuong,Phat)
    SELECT @seedPayroll,NhanVienId,160.0,2.0,8000000.0,1200000.0,150000.0,988000.0,500000.0,0.0 FROM TuyenDung WHERE SoQD=N'TD-2026-001';
    INSERT ChiTietLuong(BangLuongId,NhanVienId,GioCong,GioPhuTroi,LuongTheoCong,PhuCap,TienPhuTroi,BaoHiem,Thuong,Phat)
    SELECT @seedPayroll,NhanVienId,160.0,2.0,7200000.0,400000.0,135000.0,0.0,0.0,100000.0 FROM TuyenDung WHERE SoQD=N'TD-2026-002';
    INSERT ChiTietLuong(BangLuongId,NhanVienId,GioCong,GioPhuTroi,LuongTheoCong,PhuCap,TienPhuTroi,BaoHiem,Thuong,Phat)
    SELECT @seedPayroll,NhanVienId,160.0,0.0,6000000.0,0.0,0.0,0.0,0.0,0.0 FROM TuyenDung WHERE SoQD=N'TD-2026-003';
END;
IF NOT EXISTS(SELECT 1 FROM BangLuong WHERE Thang=10 AND Nam=2026)
BEGIN
    INSERT BangLuong(Thang,Nam,GioChuan) VALUES(10,2026,208);
    SET @seedPayroll=SCOPE_IDENTITY();
    INSERT ChiTietLuong(BangLuongId,NhanVienId,GioCong,GioPhuTroi,LuongTheoCong,PhuCap,TienPhuTroi,BaoHiem,Thuong,Phat)
    SELECT @seedPayroll,NhanVienId,160.0,3.0,8000000.0,1200000.0,225000.0,988000.0,300000.0,0.0 FROM TuyenDung WHERE SoQD=N'TD-2026-001';
    INSERT ChiTietLuong(BangLuongId,NhanVienId,GioCong,GioPhuTroi,LuongTheoCong,PhuCap,TienPhuTroi,BaoHiem,Thuong,Phat)
    SELECT @seedPayroll,NhanVienId,160.0,2.0,7200000.0,400000.0,135000.0,0.0,0.0,50000.0 FROM TuyenDung WHERE SoQD=N'TD-2026-002';
    INSERT ChiTietLuong(BangLuongId,NhanVienId,GioCong,GioPhuTroi,LuongTheoCong,PhuCap,TienPhuTroi,BaoHiem,Thuong,Phat)
    SELECT @seedPayroll,NhanVienId,160.0,0.0,6000000.0,0.0,0.0,0.0,0.0,0.0 FROM TuyenDung WHERE SoQD=N'TD-2026-003';
END;
COMMIT;
GO
