-- SQL Server: chay toan bo script trong SSMS.
-- CSDL prototype rut gon 3 bang; khong xoa du lieu hien co.
-- Ten CSDL rieng de khong xung dot voi thiet ke 8 bang truoc do.
USE master;
GO
IF DB_ID(N'EShopping_Prototype') IS NULL
    EXEC(N'CREATE DATABASE EShopping_Prototype');
GO
USE EShopping_Prototype;
GO

IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KhachHang (
        MaKhachHang INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_KhachHang PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        NgaySinh DATE NOT NULL,
        SoGiayTo NVARCHAR(30) NOT NULL,
        DiaChi NVARCHAR(300) NOT NULL,
        DienThoai VARCHAR(20) NOT NULL,
        TenDangNhap NVARCHAR(50) NOT NULL
            CONSTRAINT UQ_KhachHang_TenDangNhap UNIQUE,
        MatKhauHash VARBINARY(32) NOT NULL,
        MatKhauSalt VARBINARY(16) NOT NULL,
        SoLanLapHash INT NOT NULL,
        Email NVARCHAR(254) NULL,
        CONSTRAINT CK_KhachHang_Hash CHECK (
            DATALENGTH(MatKhauHash) = 32
            AND DATALENGTH(MatKhauSalt) = 16
            AND SoLanLapHash > 0
        )
    );
END;
GO

IF OBJECT_ID(N'dbo.DonHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DonHang (
        MaDonHang BIGINT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_DonHang PRIMARY KEY,
        MaKhachHang INT NOT NULL,
        ThoiDiemDat DATETIME2(0) NOT NULL
            CONSTRAINT DF_DonHang_ThoiDiem DEFAULT SYSDATETIME(),
        TenNguoiNhan NVARCHAR(100) NOT NULL,
        DiaChiNhan NVARCHAR(300) NOT NULL,
        DienThoaiNhan VARCHAR(20) NOT NULL,
        KhuVucGiao NVARCHAR(100) NOT NULL,
        LoaiGiaoHang VARCHAR(20) NOT NULL,
        TienHang DECIMAL(18,0) NOT NULL,
        PhiGiaoHang DECIMAL(18,0) NOT NULL,
        PhiThe DECIMAL(18,0) NOT NULL,
        TongTien AS (TienHang + PhiGiaoHang + PhiThe) PERSISTED,
        LoaiThe VARCHAR(20) NOT NULL,
        MaYeuCauThanhToan UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT UQ_DonHang_MaYeuCau UNIQUE,
        MaGiaoDichThanhToan NVARCHAR(100) NOT NULL
            CONSTRAINT UQ_DonHang_MaGiaoDich UNIQUE,
        TrangThai VARCHAR(20) NOT NULL
            CONSTRAINT DF_DonHang_TrangThai DEFAULT 'DaXacNhan',
        TrangThaiEmail VARCHAR(20) NOT NULL
            CONSTRAINT DF_DonHang_Email DEFAULT 'KhongCoEmail',
        CONSTRAINT FK_DonHang_KhachHang FOREIGN KEY (MaKhachHang)
            REFERENCES dbo.KhachHang(MaKhachHang),
        CONSTRAINT CK_DonHang_LoaiGiao CHECK (
            LoaiGiaoHang IN ('THUONG','NHANH','TRONGNGAY')
        ),
        CONSTRAINT CK_DonHang_LoaiThe CHECK (
            LoaiThe IN ('VISA','MASTER','DISCOVER','AMEX')
        ),
        CONSTRAINT CK_DonHang_Tien CHECK (
            TienHang > 0 AND PhiGiaoHang >= 0 AND PhiThe >= 0
        ),
        CONSTRAINT CK_DonHang_MienPhi CHECK (
            (LoaiGiaoHang <> 'NHANH'
                OR TienHang < 1000000 OR PhiGiaoHang = 0)
            AND (LoaiGiaoHang <> 'TRONGNGAY'
                OR TienHang < 5000000 OR PhiGiaoHang = 0)
        ),
        CONSTRAINT CK_DonHang_TrangThai CHECK (TrangThai = 'DaXacNhan'),
        CONSTRAINT CK_DonHang_Email CHECK (
            TrangThaiEmail IN ('KhongCoEmail','ChoGui','DaGui','GuiLoi')
        )
    );
    CREATE INDEX IX_DonHang_KhachHang_ThoiDiem
        ON dbo.DonHang(MaKhachHang, ThoiDiemDat);
END;
GO

IF OBJECT_ID(N'dbo.ChiTietDonHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ChiTietDonHang (
        MaDonHang BIGINT NOT NULL,
        MaSanPham NVARCHAR(50) NOT NULL,
        TenSanPhamLucDat NVARCHAR(200) NOT NULL,
        SoLuong INT NOT NULL,
        DonGiaLucDat DECIMAL(18,0) NOT NULL,
        ThanhTien AS (CONVERT(DECIMAL(10,0), SoLuong)
            * DonGiaLucDat) PERSISTED,
        CONSTRAINT PK_ChiTietDonHang PRIMARY KEY (MaDonHang, MaSanPham),
        CONSTRAINT FK_ChiTietDonHang_DonHang FOREIGN KEY (MaDonHang)
            REFERENCES dbo.DonHang(MaDonHang),
        CONSTRAINT CK_ChiTietDonHang_SoLuong CHECK (SoLuong > 0),
        CONSTRAINT CK_ChiTietDonHang_DonGia CHECK (DonGiaLucDat >= 0)
    );
END;
GO

-- Danh sach cac bang da tao.
SELECT name AS TenBang
FROM sys.tables
WHERE schema_id = SCHEMA_ID(N'dbo')
ORDER BY name;
GO

-- Quy tac o Service/Repository:
-- 1. Chi INSERT DonHang sau thanh toan thanh cong.
-- 2. Luu DonHang + ChiTietDonHang trong cung transaction.
-- 3. Don phai co it nhat 1 chi tiet.
-- 4. TienHang phai bang SUM(SoLuong * DonGiaLucDat).
-- 5. Bang phi khu vuc va phi the duoc cau hinh o Service trong ban rut gon.
-- 6. San pham lay tu he thong ngoai; MaSanPham khong co FK noi bo.
-- 7. Khong luu so the day du/CSV. Hash va salt duoc ung dung tao.
-- 8. Ban 3 bang chi luu don thanh cong; can bo sung bang yeu cau
--    neu can luu ben vung cac lan thanh toan that bai/cho doi soat.

-- Danh muc mo phong he thong san pham ngoai cho bai lab.
-- Model/thuong hieu co that; gia va tinh trang la du lieu minh hoa.
-- View khong tao them bang: CSDL van co dung 3 bang nghiep vu.
CREATE OR ALTER VIEW dbo.vwSanPhamLab
AS
SELECT CAST(MaSanPham AS nvarchar(50)) AS MaSanPham,
       CAST(NhomSanPham AS nvarchar(100)) AS NhomSanPham,
       CAST(TenSanPham AS nvarchar(200)) AS TenSanPham,
       CAST(NhaSanXuat AS nvarchar(100)) AS NhaSanXuat,
       CAST(GiaBan AS decimal(18,0)) AS GiaBan,
       CAST(ConHang AS bit) AS ConHang,
       CAST(MoTa AS nvarchar(1000)) AS MoTa,
       CAST(ThongSo AS nvarchar(1000)) AS ThongSo
FROM (VALUES
    (N'CAM01', N'Máy ảnh', N'Canon EOS R50 (Body)', N'Canon', 15990000, 1, N'Máy ảnh mirrorless dùng ống kính rời, phù hợp chụp ảnh và quay video.', N'APS-C CMOS 24.2 MP; DIGIC X; video 4K 30p'),
    (N'CAM02', N'Máy ảnh', N'Canon EOS R100 (Body)', N'Canon', 9990000, 1, N'Máy ảnh mirrorless nhỏ gọn thuộc hệ Canon EOS R.', N'APS-C CMOS 24.1 MP; DIGIC 8; ngàm RF'),
    (N'TOY01', N'Đồ chơi', N'LEGO Creator 31136 Exotic Parrot', N'LEGO', 599000, 1, N'Bộ lắp ráp 3 trong 1: vẹt, cá hoặc ếch.', N'253 mảnh; từ 7 tuổi; Creator 3in1'),
    (N'TOY02', N'Đồ chơi', N'LEGO Creator 31134 Space Shuttle', N'LEGO', 299000, 1, N'Lắp tàu con thoi, phi hành gia hoặc tàu vũ trụ.', N'144 mảnh; từ 6 tuổi; Creator 3in1'),
    (N'HOME01', N'Gia dụng', N'Nồi chiên Philips HD9200/91', N'Philips', 1990000, 1, N'Nồi chiên không dầu Philips dòng 3000 Series.', N'Dung tích 4.1 lít; công nghệ Rapid Air'),
    (N'HOME02', N'Gia dụng', N'Ấm đun Philips HD9350/90', N'Philips', 790000, 0, N'Ấm đun nước Philips có thân kim loại và đèn báo.', N'Nắp lò xo; đèn báo hoạt động'),
    (N'PC01', N'Máy tính', N'Bàn phím Logitech K120', N'Logitech', 179000, 1, N'Bàn phím có dây cho học tập và làm việc.', N'Kết nối USB; cắm và sử dụng'),
    (N'PC02', N'Máy tính', N'Chuột không dây Logitech M185', N'Logitech', 249000, 1, N'Chuột không dây Logitech dành cho máy tính.', N'Kết nối không dây qua đầu thu USB'),
    (N'CAM03', N'Máy ảnh', N'Canon EOS R10 (Body)', N'Canon', 21990000, 1, N'Máy ảnh mirrorless Canon EOS R cho chụp ảnh và video.', N'Cảm biến APS-C; ống kính ngàm RF'),
    (N'CAM04', N'Máy ảnh', N'Canon EOS R7 (Body)', N'Canon', 32990000, 1, N'Máy ảnh mirrorless Canon dành cho người yêu nhiếp ảnh.', N'Cảm biến APS-C; hệ Canon EOS R'),
    (N'CAM05', N'Máy ảnh', N'Canon EOS R8 (Body)', N'Canon', 28990000, 1, N'Máy ảnh mirrorless full-frame nhỏ gọn.', N'Cảm biến full-frame; hệ Canon EOS R'),
    (N'TOY03', N'Đồ chơi', N'LEGO Creator 31140 Magical Unicorn', N'LEGO', 299000, 1, N'Bộ lắp ráp mô hình kỳ lân thuộc dòng Creator.', N'LEGO Creator 3in1; mã bộ 31140'),
    (N'TOY04', N'Đồ chơi', N'LEGO Creator 31147 Retro Camera', N'LEGO', 599000, 1, N'Bộ lắp ráp mô hình máy ảnh phong cách cổ điển.', N'LEGO Creator 3in1; mã bộ 31147'),
    (N'TOY05', N'Đồ chơi', N'LEGO Creator 31149 Flowers in Watering Can', N'LEGO', 799000, 1, N'Bộ lắp ráp bình tưới và hoa để chơi hoặc trưng bày.', N'LEGO Creator 3in1; mã bộ 31149'),
    (N'HOME03', N'Gia dụng', N'Nồi chiên Philips HD9252/91', N'Philips', 2490000, 1, N'Nồi chiên không dầu Philips dùng trong gia đình.', N'Philips 3000 Series Airfryer L'),
    (N'HOME04', N'Gia dụng', N'Ấm đun Philips HD9365/10', N'Philips', 1290000, 1, N'Ấm đun Philips thuộc dòng Eco Conscious Edition.', N'Philips 5000 Series; mã HD9365/10'),
    (N'HOME05', N'Gia dụng', N'Máy nướng bánh Philips HD2637/90', N'Philips', 1190000, 1, N'Máy nướng bánh mì Philips phục vụ bữa sáng.', N'Viva Collection; giá hâm bánh tích hợp'),
    (N'PC03', N'Máy tính', N'Chuột không dây Logitech M170', N'Logitech', 199000, 1, N'Chuột không dây dùng cho máy tính.', N'Model M170; kết nối không dây'),
    (N'PC04', N'Máy tính', N'Bàn phím Bluetooth Logitech K380', N'Logitech', 699000, 1, N'Bàn phím Bluetooth hỗ trợ nhiều thiết bị.', N'Bluetooth; dòng K380 Multi-Device'),
    (N'PC05', N'Máy tính', N'Webcam Logitech C270 HD', N'Logitech', 599000, 1, N'Webcam dành cho học trực tuyến và gọi video.', N'Video HD 720p/30fps; micro tích hợp')
) AS DanhMuc(MaSanPham, NhomSanPham, TenSanPham, NhaSanXuat, GiaBan, ConHang, MoTa, ThongSo);
GO
