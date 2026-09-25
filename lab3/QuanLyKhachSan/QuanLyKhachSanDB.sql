USE master;
GO

IF DB_ID(N'QuanLyKhachSan') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyKhachSan
    SET SINGLE_USER
    WITH ROLLBACK IMMEDIATE;

    DROP DATABASE QuanLyKhachSan;
END
GO

CREATE DATABASE QuanLyKhachSan;
GO

USE QuanLyKhachSan;
GO

/* =========================================================
   1. KHU VỰC
   ========================================================= */
CREATE TABLE KhuVuc
(
    MaKhuVuc VARCHAR(10) PRIMARY KEY,
    TenKhuVuc NVARCHAR(100) NOT NULL UNIQUE
);
GO


/* =========================================================
   2. PHÒNG
   ========================================================= */
CREATE TABLE Phong
(
    SoPhong VARCHAR(10) PRIMARY KEY,
    MaKhuVuc VARCHAR(10) NOT NULL,
    LoaiPhong NVARCHAR(50) NOT NULL,
    SoNguoiToiDa INT NOT NULL,
    DonGiaNgay DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Trống',

    CONSTRAINT FK_Phong_KhuVuc
        FOREIGN KEY (MaKhuVuc)
        REFERENCES KhuVuc(MaKhuVuc),

    CONSTRAINT CK_Phong_SoNguoi
        CHECK (SoNguoiToiDa > 0),

    CONSTRAINT CK_Phong_DonGia
        CHECK (DonGiaNgay >= 0),

    CONSTRAINT CK_Phong_TrangThai
        CHECK (TrangThai IN
        (
            N'Trống',
            N'Đã đặt',
            N'Đang ở',
            N'Bảo trì'
        ))
);
GO


/* =========================================================
   3. LOẠI TIỆN NGHI
   ========================================================= */
CREATE TABLE LoaiTienNghi
(
    MaLoaiTN VARCHAR(10) PRIMARY KEY,
    TenLoaiTN NVARCHAR(100) NOT NULL UNIQUE
);
GO


/* =========================================================
   4. TIỆN NGHI
   ========================================================= */
CREATE TABLE TienNghi
(
    MaTienNghi VARCHAR(10) PRIMARY KEY,
    MaLoaiTN VARCHAR(10) NOT NULL,
    TenTienNghi NVARCHAR(100) NOT NULL,
    TinhTrang NVARCHAR(30) NOT NULL DEFAULT N'Tốt',

    CONSTRAINT FK_TienNghi_Loai
        FOREIGN KEY (MaLoaiTN)
        REFERENCES LoaiTienNghi(MaLoaiTN),

    CONSTRAINT CK_TienNghi_TinhTrang
        CHECK (TinhTrang IN
        (
            N'Tốt',
            N'Hỏng',
            N'Bảo trì'
        ))
);
GO


/* =========================================================
   5. LẮP ĐẶT TIỆN NGHI CHO PHÒNG
   ========================================================= */
CREATE TABLE LapDatTienNghi
(
    MaLapDat INT IDENTITY(1,1) PRIMARY KEY,
    MaTienNghi VARCHAR(10) NOT NULL,
    SoPhong VARCHAR(10) NOT NULL,
    NgayLap DATE NOT NULL,
    NgayThaoDo DATE NULL,

    CONSTRAINT FK_LapDat_TienNghi
        FOREIGN KEY (MaTienNghi)
        REFERENCES TienNghi(MaTienNghi),

    CONSTRAINT FK_LapDat_Phong
        FOREIGN KEY (SoPhong)
        REFERENCES Phong(SoPhong),

    CONSTRAINT CK_LapDat_Ngay
        CHECK
        (
            NgayThaoDo IS NULL
            OR NgayThaoDo >= NgayLap
        ),

    CONSTRAINT UQ_LapDat_ThietBi_Ngay
        UNIQUE (MaTienNghi, NgayLap)
);
GO


/* =========================================================
   6. KHÁCH HÀNG
   ========================================================= */
CREATE TABLE KhachHang
(
    MaKhach VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    CCCD VARCHAR(20) NOT NULL UNIQUE,
    SoDienThoai VARCHAR(20),
    DiaChi NVARCHAR(200),
    QuocTich NVARCHAR(50) DEFAULT N'Việt Nam'
);
GO


/* =========================================================
   7. NHÂN VIÊN
   ========================================================= */
CREATE TABLE NhanVien
(
    MaNV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    ChucVu NVARCHAR(50) NOT NULL,
    SoDienThoai VARCHAR(20),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Đang làm',

    CONSTRAINT CK_NhanVien_TrangThai
        CHECK (TrangThai IN (N'Đang làm', N'Nghỉ'))
);
GO


/* =========================================================
   8. PHIẾU ĐẶT PHÒNG
   ========================================================= */
CREATE TABLE PhieuDatPhong
(
    SoPhieuDat VARCHAR(20) PRIMARY KEY,
    MaKhach VARCHAR(10) NOT NULL,
    MaNVLeTan VARCHAR(10) NOT NULL,

    NgayLap DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    NgayNhan DATE NOT NULL,
    NgayTraDuKien DATE NOT NULL,

    TienCoc DECIMAL(18,2) NOT NULL DEFAULT 0,

    KenhDat NVARCHAR(30) NOT NULL DEFAULT N'Trực tiếp',
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Đã đặt',

    NgayNhanThucTe DATETIME2 NULL,
    NgayTraThucTe DATETIME2 NULL,

    CONSTRAINT FK_PhieuDat_Khach
        FOREIGN KEY (MaKhach)
        REFERENCES KhachHang(MaKhach),

    CONSTRAINT FK_PhieuDat_NhanVien
        FOREIGN KEY (MaNVLeTan)
        REFERENCES NhanVien(MaNV),

    CONSTRAINT CK_PhieuDat_Ngay
        CHECK (NgayTraDuKien > NgayNhan),

    CONSTRAINT CK_PhieuDat_TienCoc
        CHECK (TienCoc >= 0),

    CONSTRAINT CK_PhieuDat_Kenh
        CHECK (KenhDat IN
        (
            N'Trực tiếp',
            N'Điện thoại',
            N'Website'
        )),

    CONSTRAINT CK_PhieuDat_TrangThai
        CHECK (TrangThai IN
        (
            N'Đã đặt',
            N'Đang ở',
            N'Đã trả',
            N'Hủy',
            N'No-show'
        ))
);
GO


/* =========================================================
   9. CHI TIẾT ĐẶT PHÒNG
   Một phiếu có thể có nhiều phòng
   ========================================================= */
CREATE TABLE ChiTietDatPhong
(
    SoPhieuDat VARCHAR(20) NOT NULL,
    SoPhong VARCHAR(10) NOT NULL,
    SoNguoi INT NOT NULL,

    CONSTRAINT PK_ChiTietDatPhong
        PRIMARY KEY (SoPhieuDat, SoPhong),

    CONSTRAINT FK_CTDP_PhieuDat
        FOREIGN KEY (SoPhieuDat)
        REFERENCES PhieuDatPhong(SoPhieuDat),

    CONSTRAINT FK_CTDP_Phong
        FOREIGN KEY (SoPhong)
        REFERENCES Phong(SoPhong),

    CONSTRAINT CK_CTDP_SoNguoi
        CHECK (SoNguoi > 0)
);
GO


/* =========================================================
   10. NGƯỜI LƯU TRÚ
   ========================================================= */
CREATE TABLE NguoiLuuTru
(
    MaNguoiLT INT IDENTITY(1,1) PRIMARY KEY,
    SoPhieuDat VARCHAR(20) NOT NULL,
    SoPhong VARCHAR(10) NOT NULL,

    HoTen NVARCHAR(100) NOT NULL,
    CCCD VARCHAR(20),
    QuocTich NVARCHAR(50) DEFAULT N'Việt Nam',

    CONSTRAINT FK_NguoiLT_CTDP
        FOREIGN KEY (SoPhieuDat, SoPhong)
        REFERENCES ChiTietDatPhong(SoPhieuDat, SoPhong)
);
GO


/* =========================================================
   11. DỊCH VỤ
   ========================================================= */
CREATE TABLE DichVu
(
    MaDV VARCHAR(10) PRIMARY KEY,
    TenDV NVARCHAR(100) NOT NULL UNIQUE,
    DonViTinh NVARCHAR(30) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,

    CONSTRAINT CK_DichVu_DonGia
        CHECK (DonGia >= 0)
);
GO


/* =========================================================
   12. PHIẾU SỬ DỤNG DỊCH VỤ
   ========================================================= */
CREATE TABLE PhieuSuDungDV
(
    SoPhieuSDDV VARCHAR(20) PRIMARY KEY,
    SoPhieuDat VARCHAR(20) NOT NULL,
    SoPhong VARCHAR(10) NOT NULL,
    NgaySuDung DATE NOT NULL,
    MaNV VARCHAR(10) NOT NULL,

    CONSTRAINT FK_PSDDV_CTDP
        FOREIGN KEY (SoPhieuDat, SoPhong)
        REFERENCES ChiTietDatPhong(SoPhieuDat, SoPhong),

    CONSTRAINT FK_PSDDV_NV
        FOREIGN KEY (MaNV)
        REFERENCES NhanVien(MaNV),

    /* Mỗi phòng trong một phiếu chỉ có 1 phiếu DV/ngày */
    CONSTRAINT UQ_PSDDV_Ngay
        UNIQUE (SoPhieuDat, SoPhong, NgaySuDung)
);
GO


/* =========================================================
   13. CHI TIẾT SỬ DỤNG DỊCH VỤ
   ========================================================= */
CREATE TABLE ChiTietSuDungDV
(
    SoPhieuSDDV VARCHAR(20) NOT NULL,
    MaDV VARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,

    ThanhTien AS
    (
        CONVERT(DECIMAL(18,2), SoLuong * DonGia)
    ) PERSISTED,

    CONSTRAINT PK_CTSDDV
        PRIMARY KEY (SoPhieuSDDV, MaDV),

    CONSTRAINT FK_CTSDDV_Phieu
        FOREIGN KEY (SoPhieuSDDV)
        REFERENCES PhieuSuDungDV(SoPhieuSDDV),

    CONSTRAINT FK_CTSDDV_DichVu
        FOREIGN KEY (MaDV)
        REFERENCES DichVu(MaDV),

    CONSTRAINT CK_CTSDDV_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTSDDV_DonGia
        CHECK (DonGia >= 0)
);
GO


/* =========================================================
   14. KIỂM TRA PHÒNG
   ========================================================= */
CREATE TABLE KiemTraPhong
(
    MaKiemTra INT IDENTITY(1,1) PRIMARY KEY,
    SoPhieuDat VARCHAR(20) NOT NULL,
    SoPhong VARCHAR(10) NOT NULL,
    MaNV VARCHAR(10) NOT NULL,

    NgayKiemTra DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    GhiChu NVARCHAR(500),

    CONSTRAINT FK_KiemTra_CTDP
        FOREIGN KEY (SoPhieuDat, SoPhong)
        REFERENCES ChiTietDatPhong(SoPhieuDat, SoPhong),

    CONSTRAINT FK_KiemTra_NV
        FOREIGN KEY (MaNV)
        REFERENCES NhanVien(MaNV)
);
GO


/* =========================================================
   15. CHI TIẾT KIỂM TRA
   ========================================================= */
CREATE TABLE ChiTietKiemTra
(
    MaKiemTra INT NOT NULL,
    MaTienNghi VARCHAR(10) NOT NULL,

    TinhTrang NVARCHAR(100) NOT NULL,
    MucDoHong NVARCHAR(30),
    GhiChu NVARCHAR(500),

    CONSTRAINT PK_ChiTietKiemTra
        PRIMARY KEY (MaKiemTra, MaTienNghi),

    CONSTRAINT FK_CTKiemTra_KiemTra
        FOREIGN KEY (MaKiemTra)
        REFERENCES KiemTraPhong(MaKiemTra),

    CONSTRAINT FK_CTKiemTra_TienNghi
        FOREIGN KEY (MaTienNghi)
        REFERENCES TienNghi(MaTienNghi)
);
GO


/* =========================================================
   16. PHIẾU ĐỀN BÙ
   ========================================================= */
CREATE TABLE PhieuDenBu
(
    SoPhieuDenBu VARCHAR(20) PRIMARY KEY,
    SoPhieuDat VARCHAR(20) NOT NULL,
    SoPhong VARCHAR(10) NOT NULL,
    MaNV VARCHAR(10) NOT NULL,

    NgayLap DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    GhiChu NVARCHAR(500),

    CONSTRAINT FK_PDB_CTDP
        FOREIGN KEY (SoPhieuDat, SoPhong)
        REFERENCES ChiTietDatPhong(SoPhieuDat, SoPhong),

    CONSTRAINT FK_PDB_NV
        FOREIGN KEY (MaNV)
        REFERENCES NhanVien(MaNV)
);
GO


/* =========================================================
   17. CHI TIẾT ĐỀN BÙ
   ========================================================= */
CREATE TABLE ChiTietDenBu
(
    SoPhieuDenBu VARCHAR(20) NOT NULL,
    MaTienNghi VARCHAR(10) NOT NULL,

    LyDo NVARCHAR(200) NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1,
    DonGiaDenBu DECIMAL(18,2) NOT NULL,

    ThanhTien AS
    (
        CONVERT(DECIMAL(18,2), SoLuong * DonGiaDenBu)
    ) PERSISTED,

    CONSTRAINT PK_ChiTietDenBu
        PRIMARY KEY (SoPhieuDenBu, MaTienNghi),

    CONSTRAINT FK_CTDB_Phieu
        FOREIGN KEY (SoPhieuDenBu)
        REFERENCES PhieuDenBu(SoPhieuDenBu),

    CONSTRAINT FK_CTDB_TienNghi
        FOREIGN KEY (MaTienNghi)
        REFERENCES TienNghi(MaTienNghi),

    CONSTRAINT CK_CTDB_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTDB_DonGia
        CHECK (DonGiaDenBu >= 0)
);
GO


/* =========================================================
   18. HÓA ĐƠN
   ========================================================= */
CREATE TABLE HoaDon
(
    MaHoaDon VARCHAR(20) PRIMARY KEY,
    SoPhieuDat VARCHAR(20) NOT NULL UNIQUE,
    MaNV VARCHAR(10) NOT NULL,

    NgayLap DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    TienPhong DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienDichVu DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienDenBu DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienCoc DECIMAL(18,2) NOT NULL DEFAULT 0,

    TongThanhToan AS
    (
        CONVERT
        (
            DECIMAL(18,2),
            TienPhong + TienDichVu + TienDenBu - TienCoc
        )
    ) PERSISTED,

    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chưa thanh toán',

    CONSTRAINT FK_HoaDon_PhieuDat
        FOREIGN KEY (SoPhieuDat)
        REFERENCES PhieuDatPhong(SoPhieuDat),

    CONSTRAINT FK_HoaDon_NV
        FOREIGN KEY (MaNV)
        REFERENCES NhanVien(MaNV),

    CONSTRAINT CK_HoaDon_Tien
        CHECK
        (
            TienPhong >= 0
            AND TienDichVu >= 0
            AND TienDenBu >= 0
            AND TienCoc >= 0
        ),

    CONSTRAINT CK_HoaDon_TrangThai
        CHECK (TrangThai IN
        (
            N'Chưa thanh toán',
            N'Thanh toán một phần',
            N'Đã thanh toán'
        ))
);
GO


/* =========================================================
   19. THANH TOÁN
   Một hóa đơn có thể thanh toán nhiều lần/nhiều phương thức
   ========================================================= */
CREATE TABLE ThanhToan
(
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon VARCHAR(20) NOT NULL,

    NgayThanhToan DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    PhuongThuc NVARCHAR(30) NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,

    MaGiaoDich VARCHAR(100),
    GhiChu NVARCHAR(300),

    CONSTRAINT FK_ThanhToan_HoaDon
        FOREIGN KEY (MaHoaDon)
        REFERENCES HoaDon(MaHoaDon),

    CONSTRAINT CK_ThanhToan_SoTien
        CHECK (SoTien > 0),

    CONSTRAINT CK_ThanhToan_PhuongThuc
        CHECK (PhuongThuc IN
        (
            N'Tiền mặt',
            N'Chuyển khoản',
            N'Thẻ'
        ))
);
GO

USE QuanLyKhachSan;
GO

/* ============================================================
   1. KHU VỰC
   ============================================================ */

INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc)
VALUES
('KV01', N'Tầng 1'),
('KV02', N'Tầng 2'),
('KV03', N'Tầng 3'),
('KV04', N'Tầng 4'),
('KV05', N'Tầng 5');
GO


/* ============================================================
   2. PHÒNG
   ============================================================ */

INSERT INTO Phong
(
    SoPhong,
    MaKhuVuc,
    LoaiPhong,
    SoNguoiToiDa,
    DonGiaNgay,
    TrangThai
)
VALUES
('101', 'KV01', N'Đơn',    2, 350000,  N'Trống'),
('102', 'KV01', N'Đơn',    2, 350000,  N'Trống'),
('103', 'KV01', N'Đôi',    4, 550000,  N'Đang ở'),
('104', 'KV01', N'Đôi',    4, 550000,  N'Bảo trì'),

('201', 'KV02', N'Đơn',    2, 450000,  N'Trống'),
('202', 'KV02', N'Đôi',    4, 650000,  N'Đã đặt'),
('203', 'KV02', N'VIP',    2, 800000,  N'Trống'),
('204', 'KV02', N'VIP',    4, 950000,  N'Trống'),

('301', 'KV03', N'Đôi',    4, 700000,  N'Trống'),
('302', 'KV03', N'VIP',    2, 900000,  N'Trống'),
('303', 'KV03', N'VIP',    4, 1100000, N'Trống'),

('401', 'KV04', N'VIP',    2, 1200000, N'Trống'),
('402', 'KV04', N'VIP',    4, 1500000, N'Trống'),

('501', 'KV05', N'Suite',  4, 2000000, N'Trống'),
('502', 'KV05', N'Suite',  6, 2500000, N'Trống');
GO


/* ============================================================
   3. LOẠI TIỆN NGHI
   ============================================================ */

INSERT INTO LoaiTienNghi
(
    MaLoaiTN,
    TenLoaiTN
)
VALUES
('LTN01', N'Tivi'),
('LTN02', N'Máy lạnh'),
('LTN03', N'Tủ lạnh'),
('LTN04', N'Ấm đun nước'),
('LTN05', N'Máy sấy tóc'),
('LTN06', N'Két sắt'),
('LTN07', N'Bàn làm việc');
GO


/* ============================================================
   4. TIỆN NGHI
   ============================================================ */

INSERT INTO TienNghi
(
    MaTienNghi,
    MaLoaiTN,
    TenTienNghi,
    TinhTrang
)
VALUES
('TN001', 'LTN01', N'Tivi Samsung 43 inch',      N'Tốt'),
('TN002', 'LTN02', N'Máy lạnh Daikin 1HP',       N'Tốt'),
('TN003', 'LTN03', N'Tủ lạnh mini Aqua',          N'Tốt'),
('TN004', 'LTN04', N'Ấm đun nước Philips',        N'Tốt'),
('TN005', 'LTN01', N'Tivi LG 43 inch',            N'Tốt'),
('TN006', 'LTN02', N'Máy lạnh Panasonic 1HP',     N'Tốt'),
('TN007', 'LTN03', N'Tủ lạnh mini Electrolux',    N'Tốt'),
('TN008', 'LTN05', N'Máy sấy tóc Panasonic',      N'Tốt'),
('TN009', 'LTN06', N'Két sắt điện tử',            N'Tốt'),
('TN010', 'LTN07', N'Bàn làm việc gỗ',            N'Tốt'),
('TN011', 'LTN01', N'Tivi Sony 50 inch',          N'Tốt'),
('TN012', 'LTN02', N'Máy lạnh Daikin 1.5HP',      N'Tốt'),
('TN013', 'LTN06', N'Két sắt điện tử Yale',       N'Tốt'),
('TN014', 'LTN05', N'Máy sấy tóc Philips',        N'Bảo trì'),
('TN015', 'LTN03', N'Tủ lạnh Samsung mini',       N'Tốt');
GO


/* ============================================================
   5. LẮP ĐẶT TIỆN NGHI
   ============================================================ */

INSERT INTO LapDatTienNghi
(
    MaTienNghi,
    SoPhong,
    NgayLap,
    NgayThaoDo
)
VALUES
('TN001', '101', '2026-01-05', NULL),
('TN002', '101', '2026-01-05', NULL),
('TN003', '102', '2026-01-06', NULL),
('TN004', '102', '2026-01-06', NULL),

('TN005', '103', '2026-01-07', NULL),
('TN006', '103', '2026-01-07', NULL),
('TN007', '104', '2026-01-08', NULL),

('TN008', '201', '2026-01-09', NULL),
('TN009', '202', '2026-01-10', NULL),
('TN010', '203', '2026-01-11', NULL),

('TN011', '301', '2026-01-12', NULL),
('TN012', '302', '2026-01-13', NULL),
('TN013', '401', '2026-01-14', NULL),
('TN014', '501', '2026-01-15', NULL),
('TN015', '502', '2026-01-16', NULL);
GO


/* ============================================================
   6. KHÁCH HÀNG
   ============================================================ */

INSERT INTO KhachHang
(
    MaKhach,
    HoTen,
    CCCD,
    SoDienThoai,
    DiaChi,
    QuocTich
)
VALUES
(
    'KH001',
    N'Nguyễn Văn An',
    '079201000001',
    '0901000001',
    N'TP. Hồ Chí Minh',
    N'Việt Nam'
),
(
    'KH002',
    N'Trần Thị Bình',
    '079201000002',
    '0901000002',
    N'Đồng Nai',
    N'Việt Nam'
),
(
    'KH003',
    N'Lê Minh Hoàng',
    '079201000003',
    '0901000003',
    N'Tây Ninh',
    N'Việt Nam'
),
(
    'KH004',
    N'Phạm Thu Trang',
    '079201000004',
    '0901000004',
    N'Bình Dương',
    N'Việt Nam'
),
(
    'KH005',
    N'Võ Quốc Huy',
    '079201000005',
    '0901000005',
    N'Long An',
    N'Việt Nam'
),
(
    'KH006',
    N'Đặng Ngọc Mai',
    '079201000006',
    '0901000006',
    N'TP. Hồ Chí Minh',
    N'Việt Nam'
),
(
    'KH007',
    N'Bùi Thanh Tùng',
    '079201000007',
    '0901000007',
    N'Cần Thơ',
    N'Việt Nam'
),
(
    'KH008',
    N'Hoàng Minh Đức',
    '079201000008',
    '0901000008',
    N'Đà Nẵng',
    N'Việt Nam'
);
GO


/* ============================================================
   7. NHÂN VIÊN
   ============================================================ */

INSERT INTO NhanVien
(
    MaNV,
    HoTen,
    ChucVu,
    SoDienThoai,
    TrangThai
)
VALUES
('NV001', N'Nguyễn Minh Anh', N'Lễ tân',             '0911000001', N'Đang làm'),
('NV002', N'Trần Hoàng Nam',  N'Quản lý',            '0911000002', N'Đang làm'),
('NV003', N'Lê Thanh Hà',     N'Nhân viên dịch vụ', '0911000003', N'Đang làm'),
('NV004', N'Phạm Quốc Dũng',  N'Lễ tân',             '0911000004', N'Đang làm'),
('NV005', N'Võ Thị Lan',      N'Nhân viên dịch vụ', '0911000005', N'Đang làm'),
('NV006', N'Nguyễn Quốc Bảo', N'Kỹ thuật',           '0911000006', N'Đang làm');
GO


/* ============================================================
   8. DỊCH VỤ
   ============================================================ */

INSERT INTO DichVu
(
    MaDV,
    TenDV,
    DonViTinh,
    DonGia
)
VALUES
('DV001', N'Nước suối',          N'Chai',  15000),
('DV002', N'Nước ngọt',          N'Lon',   25000),
('DV003', N'Giặt ủi',            N'Kg',    50000),
('DV004', N'Ăn sáng',            N'Suất',  80000),
('DV005', N'Đưa đón sân bay',    N'Lượt', 300000),
('DV006', N'Phục vụ phòng',      N'Lượt', 100000),
('DV007', N'Thuê xe máy',        N'Ngày',  200000),
('DV008', N'Buffet tối',         N'Suất',  250000),
('DV009', N'Cà phê',             N'Ly',     40000),
('DV010', N'Nước ép trái cây',   N'Ly',     60000);
GO


/* ============================================================
   9. PHIẾU ĐẶT PHÒNG

   Tạo:
   - 2 phiếu đã trả để test hóa đơn/thanh toán
   - 1 phiếu đang ở để test dịch vụ/trả phòng
   - 1 phiếu đã đặt để test nhận phòng
   ============================================================ */

INSERT INTO PhieuDatPhong
(
    SoPhieuDat,
    MaKhach,
    MaNVLeTan,
    NgayLap,
    NgayNhan,
    NgayTraDuKien,
    TienCoc,
    KenhDat,
    TrangThai,
    NgayNhanThucTe,
    NgayTraThucTe
)
VALUES

/* Đã trả */
(
    'DP001',
    'KH001',
    'NV001',
    '2026-08-28 09:00:00',
    '2026-09-01',
    '2026-09-04',
    300000,
    N'Trực tiếp',
    N'Đã trả',
    '2026-09-01 14:00:00',
    '2026-09-04 10:00:00'
),

/* Đã trả */
(
    'DP002',
    'KH002',
    'NV004',
    '2026-09-03 10:30:00',
    '2026-09-05',
    '2026-09-07',
    500000,
    N'Website',
    N'Đã trả',
    '2026-09-05 13:30:00',
    '2026-09-07 09:30:00'
),

/* Đang ở */
(
    'DP003',
    'KH003',
    'NV001',
    '2026-09-20 08:30:00',
    '2026-09-24',
    '2026-09-28',
    500000,
    N'Điện thoại',
    N'Đang ở',
    '2026-09-24 14:00:00',
    NULL
),

/* Đã đặt */
(
    'DP004',
    'KH004',
    'NV004',
    '2026-09-23 11:00:00',
    '2026-09-27',
    '2026-09-30',
    500000,
    N'Website',
    N'Đã đặt',
    NULL,
    NULL
);
GO


/* ============================================================
   10. CHI TIẾT ĐẶT PHÒNG
   ============================================================ */

INSERT INTO ChiTietDatPhong
(
    SoPhieuDat,
    SoPhong,
    SoNguoi
)
VALUES
('DP001', '101', 2),
('DP002', '201', 2),
('DP003', '103', 3),
('DP004', '202', 4);
GO


/* ============================================================
   11. NGƯỜI LƯU TRÚ
   ============================================================ */

INSERT INTO NguoiLuuTru
(
    SoPhieuDat,
    SoPhong,
    HoTen,
    CCCD,
    QuocTich
)
VALUES
('DP001', '101', N'Nguyễn Văn An',    '079201000001', N'Việt Nam'),
('DP001', '101', N'Nguyễn Thị Hoa',   '079201000101', N'Việt Nam'),

('DP002', '201', N'Trần Thị Bình',    '079201000002', N'Việt Nam'),
('DP002', '201', N'Trần Văn Hùng',    '079201000102', N'Việt Nam'),

('DP003', '103', N'Lê Minh Hoàng',    '079201000003', N'Việt Nam'),
('DP003', '103', N'Lê Thanh Mai',     '079201000103', N'Việt Nam'),
('DP003', '103', N'Nguyễn Đức Anh',   '079201000104', N'Việt Nam');
GO


/* ============================================================
   12. PHIẾU SỬ DỤNG DỊCH VỤ
   ============================================================ */

INSERT INTO PhieuSuDungDV
(
    SoPhieuSDDV,
    SoPhieuDat,
    SoPhong,
    NgaySuDung,
    MaNV
)
VALUES
('SD001', 'DP001', '101', '2026-09-02', 'NV003'),
('SD002', 'DP002', '201', '2026-09-06', 'NV005'),
('SD003', 'DP003', '103', '2026-09-24', 'NV003'),
('SD004', 'DP003', '103', '2026-09-25', 'NV005');
GO


/* ============================================================
   13. CHI TIẾT DỊCH VỤ
   ============================================================ */

INSERT INTO ChiTietSuDungDV
(
    SoPhieuSDDV,
    MaDV,
    SoLuong,
    DonGia
)
VALUES

/* DP001 */
('SD001', 'DV001', 4, 15000),
('SD001', 'DV004', 2, 80000),
('SD001', 'DV009', 2, 40000),

/* DP002 */
('SD002', 'DV003', 3, 50000),
('SD002', 'DV004', 2, 80000),

/* DP003 ngày 24 */
('SD003', 'DV001', 5, 15000),
('SD003', 'DV002', 3, 25000),
('SD003', 'DV006', 1, 100000),

/* DP003 ngày 25 */
('SD004', 'DV004', 3, 80000),
('SD004', 'DV010', 3, 60000);
GO


/* ============================================================
   14. KIỂM TRA PHÒNG
   DP001 có một thiết bị cần đền bù
   ============================================================ */

INSERT INTO KiemTraPhong
(
    SoPhieuDat,
    SoPhong,
    MaNV,
    NgayKiemTra,
    GhiChu
)
VALUES
(
    'DP001',
    '101',
    'NV006',
    '2026-09-04 09:30:00',
    N'Kiểm tra phòng trước khi khách trả'
);
GO


/* MaKiemTra đầu tiên = 1 nếu database mới hoàn toàn */

INSERT INTO ChiTietKiemTra
(
    MaKiemTra,
    MaTienNghi,
    TinhTrang,
    MucDoHong,
    GhiChu
)
VALUES
(
    1,
    'TN001',
    N'Remote tivi bị hỏng',
    N'Nhẹ',
    N'Khách làm hỏng remote tivi'
),
(
    1,
    'TN002',
    N'Hoạt động bình thường',
    NULL,
    N'Không phát hiện hư hỏng'
);
GO


/* ============================================================
   15. PHIẾU ĐỀN BÙ
   ============================================================ */

INSERT INTO PhieuDenBu
(
    SoPhieuDenBu,
    SoPhieuDat,
    SoPhong,
    MaNV,
    NgayLap,
    GhiChu
)
VALUES
(
    'DB001',
    'DP001',
    '101',
    'NV001',
    '2026-09-04 09:45:00',
    N'Đền bù hư hỏng thiết bị phòng'
);
GO


INSERT INTO ChiTietDenBu
(
    SoPhieuDenBu,
    MaTienNghi,
    LyDo,
    SoLuong,
    DonGiaDenBu
)
VALUES
(
    'DB001',
    'TN001',
    N'Làm hỏng remote tivi',
    1,
    100000
);
GO


/* ============================================================
   16. HÓA ĐƠN

   DP001:
   Phòng: 3 x 350.000 = 1.050.000
   DV: 300.000
   Đền bù: 100.000
   Cọc: 300.000
   => Tổng: 1.150.000

   DP002:
   Phòng: 2 x 450.000 = 900.000
   DV: 310.000
   Đền bù: 0
   Cọc: 500.000
   => Tổng: 710.000
   ============================================================ */

INSERT INTO HoaDon
(
    MaHoaDon,
    SoPhieuDat,
    MaNV,
    NgayLap,
    TienPhong,
    TienDichVu,
    TienDenBu,
    TienCoc,
    TrangThai
)
VALUES
(
    'HD001',
    'DP001',
    'NV001',
    '2026-09-04 10:00:00',
    1050000,
    300000,
    100000,
    300000,
    N'Đã thanh toán'
),
(
    'HD002',
    'DP002',
    'NV004',
    '2026-09-07 09:30:00',
    900000,
    310000,
    0,
    500000,
    N'Đã thanh toán'
);
GO


/* ============================================================
   17. THANH TOÁN

   HD001 = 1.150.000
   Thanh toán bằng 2 phương thức.

   HD002 = 710.000
   Thanh toán một lần.
   ============================================================ */

INSERT INTO ThanhToan
(
    MaHoaDon,
    NgayThanhToan,
    PhuongThuc,
    SoTien,
    MaGiaoDich,
    GhiChu
)
VALUES
(
    'HD001',
    '2026-09-04 10:05:00',
    N'Tiền mặt',
    500000,
    NULL,
    N'Khách thanh toán một phần bằng tiền mặt'
),
(
    'HD001',
    '2026-09-04 10:07:00',
    N'Chuyển khoản',
    650000,
    'GD-HD001-01',
    N'Thanh toán phần còn lại'
),
(
    'HD002',
    '2026-09-07 09:35:00',
    N'Thẻ',
    710000,
    'GD-HD002-01',
    N'Thanh toán toàn bộ hóa đơn'
);
GO