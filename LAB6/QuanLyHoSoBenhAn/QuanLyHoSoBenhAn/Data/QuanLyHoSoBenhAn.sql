-- LAB6 - QUAN LY HO SO BENH AN
-- Chay toan bo tep trong SSMS hoac sqlcmd. Can quyen tao CSDL.
-- Gom: tao/nang cap cau truc, du lieu trinh dien, don du lieu kiem thu tuy chon.
-- Co the chay lai; mac dinh KHONG xoa ho so kiem thu.

-- PHAN 1: TAO VA NANG CAP CSDL
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
USE master;
GO
IF DB_ID(N'QuanLyHoSoBenhAnLAB6') IS NULL CREATE DATABASE QuanLyHoSoBenhAnLAB6;
GO
USE QuanLyHoSoBenhAnLAB6;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
IF OBJECT_ID('BenhNhan') IS NULL CREATE TABLE BenhNhan(MaBN int IDENTITY PRIMARY KEY,HoTen nvarchar(100) NOT NULL,NgaySinh date NOT NULL,DiaChi nvarchar(300) NOT NULL,DienThoai varchar(20) NOT NULL,Anh varbinary(max) NULL);
IF OBJECT_ID('HoSoBenhAn') IS NULL CREATE TABLE HoSoBenhAn(MaHS int IDENTITY PRIMARY KEY,MaBN int NOT NULL UNIQUE REFERENCES BenhNhan,NgayLap date NOT NULL DEFAULT CAST(GETDATE() AS date));
IF OBJECT_ID('LePhiNam') IS NULL CREATE TABLE LePhiNam(MaLP int IDENTITY PRIMARY KEY,MaHS int NOT NULL REFERENCES HoSoBenhAn,TuNgay date NOT NULL,DenNgay date NOT NULL,SoTien decimal(18,0) NOT NULL CHECK(SoTien>0),CHECK(DenNgay>TuNgay),UNIQUE(MaHS,TuNgay));
IF OBJECT_ID('Khoa') IS NULL CREATE TABLE Khoa(MaKhoa int IDENTITY PRIMARY KEY,TenKhoa nvarchar(100) NOT NULL UNIQUE);
IF OBJECT_ID('BacSi') IS NULL CREATE TABLE BacSi(MaBS int IDENTITY PRIMARY KEY,HoTen nvarchar(100) NOT NULL,MaKhoa int NOT NULL REFERENCES Khoa);
IF OBJECT_ID('Thuoc') IS NULL CREATE TABLE Thuoc(MaThuoc int IDENTITY PRIMARY KEY,TenThuoc nvarchar(150) NOT NULL UNIQUE,DonVi nvarchar(30) NOT NULL);
IF OBJECT_ID('LichHen') IS NULL CREATE TABLE LichHen(MaHen int IDENTITY PRIMARY KEY,MaHS int NOT NULL REFERENCES HoSoBenhAn,MaBS int NOT NULL REFERENCES BacSi,ThoiGian datetime2 NOT NULL,TrangThai nvarchar(20) NOT NULL DEFAULT N'Đã hẹn',CHECK(TrangThai IN(N'Đã hẹn',N'Đã khám',N'Đã hủy')));
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name='UX_LichHen') CREATE UNIQUE INDEX UX_LichHen ON LichHen(MaBS,ThoiGian) WHERE TrangThai=N'Đã hẹn';
IF OBJECT_ID('PhieuKham') IS NULL CREATE TABLE PhieuKham(MaPK int IDENTITY PRIMARY KEY,MaHS int NOT NULL REFERENCES HoSoBenhAn,MaBS int NOT NULL REFERENCES BacSi,MaKhoa int NOT NULL REFERENCES Khoa,MaHen int NULL REFERENCES LichHen,NgayKham datetime2 NOT NULL,LoaiKham nvarchar(20) NOT NULL CHECK(LoaiKham IN(N'Điều trị',N'Định kỳ')),TrieuChung nvarchar(1000) NOT NULL,ChanDoan nvarchar(1000) NULL,TrangThai nvarchar(30) NOT NULL DEFAULT N'Đang khám' CHECK(TrangThai IN(N'Đang khám',N'Chờ thanh toán',N'Đã thanh toán')),TienKham decimal(18,0) NOT NULL CHECK(TienKham>=0));
IF OBJECT_ID('XetNghiem') IS NULL CREATE TABLE XetNghiem(MaXN int IDENTITY PRIMARY KEY,MaPK int NOT NULL REFERENCES PhieuKham,TenXN nvarchar(150) NOT NULL,KetQua nvarchar(1000) NULL,ChiPhi decimal(18,0) NOT NULL CHECK(ChiPhi>=0));
IF OBJECT_ID('ToaThuoc') IS NULL CREATE TABLE ToaThuoc(MaToa int IDENTITY PRIMARY KEY,MaPK int NOT NULL UNIQUE REFERENCES PhieuKham,NgayLap datetime2 NOT NULL DEFAULT SYSDATETIME());
IF OBJECT_ID('ChiTietToa') IS NULL CREATE TABLE ChiTietToa(MaToa int NOT NULL REFERENCES ToaThuoc,MaThuoc int NOT NULL REFERENCES Thuoc,SoLuong int NOT NULL CHECK(SoLuong>0),LieuDung nvarchar(300) NOT NULL,PRIMARY KEY(MaToa,MaThuoc));
IF OBJECT_ID('ThanhToan') IS NULL CREATE TABLE ThanhToan(MaTT int IDENTITY PRIMARY KEY,MaPK int NOT NULL UNIQUE REFERENCES PhieuKham,NgayTT datetime2 NOT NULL DEFAULT SYSDATETIME(),TongTien decimal(18,0) NOT NULL CHECK(TongTien>=0));
GO
INSERT Khoa(TenKhoa) SELECT v.Ten FROM (VALUES(N'Nội tổng quát'),(N'Ngoại tổng quát')) v(Ten) WHERE NOT EXISTS(SELECT 1 FROM Khoa k WHERE k.TenKhoa=v.Ten);
UPDATE BacSi SET HoTen=REPLACE(REPLACE(HoTen,N' (Mẫu)',N''),N'Bác sĩ mẫu ',N'Bác sĩ ');
INSERT BacSi(HoTen,MaKhoa) SELECT v.Ten,k.MaKhoa FROM (VALUES(N'Bác sĩ 01',N'Nội tổng quát'),(N'Bác sĩ 02',N'Ngoại tổng quát')) v(Ten,Khoa) JOIN Khoa k ON k.TenKhoa=v.Khoa WHERE NOT EXISTS(SELECT 1 FROM BacSi b WHERE b.HoTen=v.Ten AND b.MaKhoa=k.MaKhoa);
INSERT Thuoc(TenThuoc,DonVi) SELECT v.Ten,v.DonVi FROM (VALUES(N'Thuốc minh họa A',N'Viên'),(N'Thuốc minh họa B',N'Gói')) v(Ten,DonVi) WHERE NOT EXISTS(SELECT 1 FROM Thuoc t WHERE t.TenThuoc=v.Ten);
GO

IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name='UX_PhieuKham_Hen') CREATE UNIQUE INDEX UX_PhieuKham_Hen ON PhieuKham(MaHen) WHERE MaHen IS NOT NULL;
GO

-- Cap nhat tu ban LAB6 cu; giu du lieu hien co.
SET XACT_ABORT ON;
BEGIN TRY
 BEGIN TRANSACTION;
 IF COL_LENGTH('dbo.PhieuKham','MaKhoa') IS NULL
 BEGIN
  ALTER TABLE dbo.PhieuKham ADD MaKhoa int NULL;
  EXEC(N'UPDATE p SET MaKhoa=b.MaKhoa FROM dbo.PhieuKham p JOIN dbo.BacSi b ON p.MaBS=b.MaBS; ALTER TABLE dbo.PhieuKham ALTER COLUMN MaKhoa int NOT NULL; ALTER TABLE dbo.PhieuKham ADD CONSTRAINT FK_PhieuKham_Khoa FOREIGN KEY(MaKhoa) REFERENCES dbo.Khoa(MaKhoa);');
 END;
 IF EXISTS(SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID('dbo.PhieuKham') AND definition LIKE N'%Đã kê toa%')
 BEGIN
  DECLARE @dropChecks nvarchar(max)=N'';
  SELECT @dropChecks=@dropChecks+N'ALTER TABLE dbo.PhieuKham DROP CONSTRAINT '+QUOTENAME(name)+N';'
   FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID('dbo.PhieuKham') AND definition LIKE N'%Đã kê toa%';
  EXEC sp_executesql @dropChecks;
  UPDATE dbo.PhieuKham SET TrangThai=N'Chờ thanh toán' WHERE TrangThai=N'Đã kê toa';
  ALTER TABLE dbo.PhieuKham WITH CHECK ADD CONSTRAINT CK_PhieuKham_TrangThaiV2 CHECK(TrangThai IN(N'Đang khám',N'Chờ thanh toán',N'Đã thanh toán'));
 END;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
GO

-- PHAN 2: NAP DU LIEU TRINH DIEN
-- Du lieu gia lap cho LAB6. Chay sau QuanLyHoSoBenhAn.sql.
-- Co the chay lai: khong xoa du lieu, khong tao trung bo mau.
USE QuanLyHoSoBenhAnLAB6;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;

BEGIN TRY
 BEGIN TRANSACTION;
 UPDATE BenhNhan SET HoTen=REPLACE(HoTen,N' (Mẫu)',N''),DiaChi=REPLACE(DiaChi,N'Địa chỉ mẫu ',N'Địa chỉ ');
 UPDATE BacSi SET HoTen=REPLACE(REPLACE(HoTen,N' (Mẫu)',N''),N'Bác sĩ mẫu ',N'Bác sĩ ');
 DECLARE @NgayGoc date='20261016';
 DECLARE @K TABLE(Ten nvarchar(100));
 INSERT @K VALUES(N'Nội tổng quát'),(N'Ngoại tổng quát'),(N'Nhi'),(N'Tai mũi họng');
 INSERT Khoa(TenKhoa) SELECT Ten FROM @K k WHERE NOT EXISTS(SELECT 1 FROM Khoa t WHERE t.TenKhoa=k.Ten);

 DECLARE @B TABLE(Ten nvarchar(100),Khoa nvarchar(100));
 INSERT @B VALUES(N'BS Nguyễn Minh An',N'Nội tổng quát'),(N'BS Trần Thu Hà',N'Nội tổng quát'),
 (N'BS Lê Quốc Bảo',N'Ngoại tổng quát'),(N'BS Phạm Ngọc Lan',N'Nhi'),
 (N'BS Võ Hoàng Nam',N'Tai mũi họng'),(N'BS Đặng Thảo Vy',N'Ngoại tổng quát');
 INSERT BacSi(HoTen,MaKhoa) SELECT b.Ten,k.MaKhoa FROM @B b JOIN Khoa k ON b.Khoa=k.TenKhoa
 WHERE NOT EXISTS(SELECT 1 FROM BacSi t WHERE t.HoTen=b.Ten AND t.MaKhoa=k.MaKhoa);

 DECLARE @T TABLE(Ten nvarchar(150),DonVi nvarchar(30));
 INSERT @T VALUES(N'Thuốc mô phỏng C',N'Viên'),(N'Thuốc mô phỏng D',N'Gói'),(N'Thuốc mô phỏng E',N'Chai');
 INSERT Thuoc(TenThuoc,DonVi) SELECT Ten,DonVi FROM @T m WHERE NOT EXISTS(SELECT 1 FROM Thuoc t WHERE t.TenThuoc=m.Ten);

 DECLARE @BN TABLE(STT int,Ten nvarchar(100),NgaySinh date,DiaChi nvarchar(300),DT varchar(20));
 INSERT @BN VALUES
 (1,N'Nguyễn Văn An','19920312',N'Địa chỉ 01, TP.HCM','0900000001'),
 (2,N'Trần Thị Bình','19880725',N'Địa chỉ 02, TP.HCM','0900000002'),
 (3,N'Lê Minh Châu','20001109',N'Địa chỉ 03, TP.HCM','0900000003'),
 (4,N'Phạm Ngọc Dung','19950518',N'Địa chỉ 04, TP.HCM','0900000004'),
 (5,N'Võ Quốc Duy','19800204',N'Địa chỉ 05, TP.HCM','0900000005'),
 (6,N'Đặng Thu Hà','20010930',N'Địa chỉ 06, TP.HCM','0900000006'),
 (7,N'Bùi Gia Hân','20180615',N'Địa chỉ 07, TP.HCM','0900000007'),
 (8,N'Đỗ Hoàng Khang','19971221',N'Địa chỉ 08, TP.HCM','0900000008'),
 (9,N'Hồ Thanh Linh','19741103',N'Địa chỉ 09, TP.HCM','0900000009'),
 (10,N'Ngô Đức Minh','19860308',N'Địa chỉ 10, TP.HCM','0900000010'),
 (11,N'Dương Thảo Nhi','20150914',N'Địa chỉ 11, TP.HCM','0900000011'),
 (12,N'Vũ Hải Yến','19990427',N'Địa chỉ 12, TP.HCM','0900000012');
 INSERT BenhNhan(HoTen,NgaySinh,DiaChi,DienThoai)
 SELECT Ten,NgaySinh,DiaChi,DT FROM @BN m WHERE NOT EXISTS(SELECT 1 FROM BenhNhan b WHERE b.HoTen=m.Ten AND b.DienThoai=m.DT);
 INSERT HoSoBenhAn(MaBN,NgayLap)
 SELECT b.MaBN,DATEADD(day,-100,@NgayGoc) FROM @BN m JOIN BenhNhan b ON b.HoTen=m.Ten AND b.DienThoai=m.DT
 WHERE NOT EXISTS(SELECT 1 FROM HoSoBenhAn h WHERE h.MaBN=b.MaBN);

 DECLARE @i int=1,@hs int,@bs int,@from date,@to date,@at datetime2,@j int,@pk int,@toa int,@maThuoc int,
 @tag nvarchar(100),@tc nvarchar(1000),@state nvarchar(30),@type nvarchar(20),@fee decimal(18,0),@ngay datetime2;
 WHILE @i<=12
 BEGIN
  SELECT @hs=h.MaHS FROM @BN m JOIN BenhNhan b ON b.HoTen=m.Ten AND b.DienThoai=m.DT JOIN HoSoBenhAn h ON h.MaBN=b.MaBN WHERE m.STT=@i;
  SELECT @bs=MaBS FROM(SELECT MaBS,ROW_NUMBER() OVER(ORDER BY MaBS) rn FROM BacSi WHERE HoTen IN(SELECT Ten FROM @B)) q WHERE rn=((@i-1)%6)+1;
  -- Ho so 9 va 10 het han de thu chuc nang gia han; cac ho so khac con hieu luc.
  SET @from=CASE WHEN @i IN(9,10) THEN DATEADD(year,-1,DATEADD(day,-30,@NgayGoc)) ELSE DATEADD(day,-100,@NgayGoc) END;
  SET @to=DATEADD(year,1,@from);
  IF NOT EXISTS(SELECT 1 FROM LePhiNam WHERE MaHS=@hs AND TuNgay=@from)
   INSERT LePhiNam(MaHS,TuNgay,DenNgay,SoTien) VALUES(@hs,@from,@to,200000);

  -- Lich hen tuong lai tinh tu ngay chay, giu nguyen lich mau neu chay lai.
  SET @at=DATEADD(hour,8+(@i%4),CAST(DATEADD(day,@i,CAST(GETDATE() AS date)) AS datetime2));
  IF NOT EXISTS(SELECT 1 FROM LichHen WHERE MaHS=@hs)
   INSERT LichHen(MaHS,MaBS,ThoiGian,TrangThai) VALUES(@hs,@bs,@at,CASE WHEN @i IN(5,10) THEN N'Đã hủy' ELSE N'Đã hẹn' END);

  SET @j=1;
  WHILE @j<=CASE WHEN @i<=6 THEN 2 ELSE 1 END
  BEGIN
   SET @tag=N'[DLMAU6-'+RIGHT('0'+CAST(@i AS varchar),2)+N'-'+CAST(@j AS nvarchar)+N']';
   SET @ngay=DATEADD(hour,9+(@i%4),CAST(CASE WHEN @i<=6 AND @j=1 THEN DATEADD(day,-40-@i,@NgayGoc) ELSE @NgayGoc END AS datetime2));
   SET @type=CASE WHEN @i%4=0 AND NOT(@i<=6 AND @j=1) THEN N'Định kỳ' ELSE N'Điều trị' END;
   SET @state=CASE WHEN @i<=6 AND @j=1 THEN N'Đã thanh toán' WHEN @i%3=1 THEN N'Đang khám' WHEN @i%3=2 THEN N'Chờ thanh toán' ELSE N'Đã thanh toán' END;
   SET @fee=CASE WHEN @type=N'Định kỳ' THEN 0 ELSE 150000 END;
   SET @tc=@tag+CASE WHEN @type=N'Định kỳ' THEN N' Khám sức khỏe định kỳ, dữ liệu mô phỏng.' ELSE N' Ghi nhận triệu chứng để thử quy trình khám, dữ liệu mô phỏng.' END;
   SET @pk=NULL;
   SELECT @pk=MaPK FROM PhieuKham WHERE MaHS=@hs AND TrieuChung=@tc;
   IF @pk IS NULL
   BEGIN
    INSERT PhieuKham(MaHS,MaBS,MaKhoa,NgayKham,LoaiKham,TrieuChung,ChanDoan,TrangThai,TienKham)
    VALUES(@hs,@bs,(SELECT MaKhoa FROM BacSi WHERE MaBS=@bs),@ngay,@type,@tc,CASE WHEN @state=N'Đang khám' THEN NULL ELSE N'Kết luận mô phỏng phục vụ bài lab.' END,@state,@fee);
    SET @pk=SCOPE_IDENTITY();
    INSERT XetNghiem(MaPK,TenXN,KetQua,ChiPhi) VALUES(@pk,N'Xét nghiệm tổng quát (mô phỏng)',CASE WHEN @state=N'Đang khám' THEN NULL ELSE N'Đã có kết quả mô phỏng.' END,50000);
    IF @i%2=0 INSERT XetNghiem(MaPK,TenXN,KetQua,ChiPhi) VALUES(@pk,N'Xét nghiệm bổ sung (mô phỏng)',CASE WHEN @state=N'Đang khám' THEN NULL ELSE N'Đã có kết quả mô phỏng.' END,80000);
    IF @state<>N'Đang khám' AND @type<>N'Định kỳ'
    BEGIN
     INSERT ToaThuoc(MaPK,NgayLap) VALUES(@pk,DATEADD(minute,30,@ngay));SET @toa=SCOPE_IDENTITY();
     SELECT @maThuoc=MaThuoc FROM Thuoc WHERE TenThuoc=N'Thuốc minh họa A';
     INSERT ChiTietToa(MaToa,MaThuoc,SoLuong,LieuDung) VALUES(@toa,@maThuoc,10,N'Dòng thuốc giả lập để kiểm thử, không dùng điều trị.');
     SELECT @maThuoc=MaThuoc FROM Thuoc WHERE TenThuoc=N'Thuốc mô phỏng C';
     INSERT ChiTietToa(MaToa,MaThuoc,SoLuong,LieuDung) VALUES(@toa,@maThuoc,5,N'Dòng thuốc giả lập bổ sung, không dùng điều trị.');
    END;
    IF @state=N'Đã thanh toán'
     INSERT ThanhToan(MaPK,NgayTT,TongTien) SELECT @pk,DATEADD(minute,45,@ngay),@fee+SUM(ChiPhi) FROM XetNghiem WHERE MaPK=@pk;
   END;
   SET @j+=1;
  END;
  SET @i+=1;
 END;
 COMMIT;
 PRINT N'Đã nạp dữ liệu mẫu LAB6. Dữ liệu cũ được giữ nguyên.';
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
GO

-- PHAN 3: DON DU LIEU KIEM THU CU (TUY CHON)
USE QuanLyHoSoBenhAnLAB6;
GO
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;

-- Doi 0 thanh 1 neu muon xoa ho so LAB6_TEST va du lieu phu thuoc.
DECLARE @DonDuLieuKiemThu bit = 0;
IF @DonDuLieuKiemThu = 1
BEGIN
BEGIN TRANSACTION;
DECLARE @BN TABLE(ID int PRIMARY KEY);
INSERT @BN SELECT MaBN FROM BenhNhan WHERE HoTen LIKE N'LAB6[_]TEST[_]%';
DECLARE @HS TABLE(ID int PRIMARY KEY);
INSERT @HS SELECT MaHS FROM HoSoBenhAn WHERE MaBN IN (SELECT ID FROM @BN);
DECLARE @PK TABLE(ID int PRIMARY KEY);
INSERT @PK SELECT MaPK FROM PhieuKham WHERE MaHS IN (SELECT ID FROM @HS);
DELETE c FROM ChiTietToa c JOIN ToaThuoc t ON c.MaToa=t.MaToa WHERE t.MaPK IN(SELECT ID FROM @PK);
DELETE FROM ToaThuoc WHERE MaPK IN(SELECT ID FROM @PK);
DELETE FROM ThanhToan WHERE MaPK IN(SELECT ID FROM @PK);
DELETE FROM XetNghiem WHERE MaPK IN(SELECT ID FROM @PK);
DELETE FROM PhieuKham WHERE MaPK IN(SELECT ID FROM @PK);
DELETE FROM LichHen WHERE MaHS IN(SELECT ID FROM @HS);
DELETE FROM LePhiNam WHERE MaHS IN(SELECT ID FROM @HS);
DELETE FROM HoSoBenhAn WHERE MaHS IN(SELECT ID FROM @HS);
DELETE FROM BenhNhan WHERE MaBN IN(SELECT ID FROM @BN);
DELETE b FROM BacSi b WHERE b.HoTen LIKE N'LAB6[_]TEST[_]%' AND NOT EXISTS(SELECT 1 FROM PhieuKham p WHERE p.MaBS=b.MaBS) AND NOT EXISTS(SELECT 1 FROM LichHen h WHERE h.MaBS=b.MaBS);
UPDATE BenhNhan SET HoTen=REPLACE(HoTen,N' (Mẫu)',N''),DiaChi=REPLACE(DiaChi,N'Địa chỉ mẫu ',N'Địa chỉ ');
UPDATE BacSi SET HoTen=REPLACE(REPLACE(HoTen,N' (Mẫu)',N''),N'Bác sĩ mẫu ',N'Bác sĩ ');
COMMIT;
SELECT COUNT(*) AS TestPatientsRemoved FROM @BN;
SELECT COUNT(*) AS PatientsRemaining FROM BenhNhan;
SELECT MaBN,HoTen FROM BenhNhan ORDER BY MaBN;
END;
GO

-- KIEM TRA SO LUONG SAU KHI KHOI TAO
SELECT N'Bệnh nhân' AS Bang, COUNT(*) AS SoLuong FROM BenhNhan
UNION ALL SELECT N'Hồ sơ bệnh án',COUNT(*) FROM HoSoBenhAn
UNION ALL SELECT N'Phiếu khám',COUNT(*) FROM PhieuKham
UNION ALL SELECT N'Xét nghiệm',COUNT(*) FROM XetNghiem
UNION ALL SELECT N'Toa thuốc',COUNT(*) FROM ToaThuoc
UNION ALL SELECT N'Thanh toán',COUNT(*) FROM ThanhToan;
GO
