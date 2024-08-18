CREATE DATABASE QL_KHACHSAN2;
GO
USE QL_KHACHSAN2;
GO


-- Create the TAIKHOAN table
CREATE TABLE TAIKHOAN (
    TENTK NVARCHAR(50) PRIMARY KEY,
    MATKHAU NVARCHAR(50),
    PHANQUYEN NVARCHAR(50)
);

-- Create the NHANVIEN table
CREATE TABLE NHANVIEN (
    MANV NVARCHAR(50) PRIMARY KEY,
    TENNV NVARCHAR(50),
    SDT NVARCHAR(15),
    CCCD NVARCHAR(20),
    TUOI INT,
    TENTK NVARCHAR(50) REFERENCES TAIKHOAN(TENTK)
);

-- Create the PHONG table
CREATE TABLE PHONG (
    MAPHONG NVARCHAR(50) PRIMARY KEY,
    LOAIPHONG NVARCHAR(50),
    GIAPHONG varchar(30),
    TINHTRANG NVARCHAR(50)
);

-- Create the DICHVU table
CREATE TABLE DICHVU (
    MADV NVARCHAR(50) PRIMARY KEY,
    TENDV NVARCHAR(50),
    GIADV varchar(30)
);

-- Create the KHACHHANG table
CREATE TABLE KHACHHANG (
    MAKH NVARCHAR(50) PRIMARY KEY,
    TENKH NVARCHAR(50),
    CCCD NVARCHAR(20),
    GIOTINH NVARCHAR(5),
    SDT NVARCHAR(15),
    MAPHONG NVARCHAR(50) REFERENCES PHONG(MAPHONG)
);

-- Create the HOADON table
CREATE TABLE HOADON (
    MAHD NVARCHAR(50) PRIMARY KEY,
    MANV NVARCHAR(50) REFERENCES NHANVIEN(MANV),
    MAKH NVARCHAR(50) REFERENCES KHACHHANG(MAKH),
    NGAY DATE,
    
);

-- Create the CTHOADON table
CREATE TABLE CTHOADON (
    MAHD NVARCHAR(50) REFERENCES HOADON(MAHD),
    MADV NVARCHAR(50) REFERENCES DICHVU(MADV),
    MAPHONG NVARCHAR(50) REFERENCES PHONG(MAPHONG),
    SOLUONG INT,
    GIADV varchar(30),
	THANHTIEN FLOAT
);

-- Nhập dữ liệu cho bảng TAIKHOAN
INSERT INTO TAIKHOAN 
VALUES
    ('admin', '12345', 'admin'),
    ('trikhoi', '09876', 'user'),
    ('loan', '12345', 'user'),
    ('phuonganh', '12345', 'user');

-- Nhập dữ liệu cho bảng NHANVIEN
INSERT INTO NHANVIEN (MANV, TENNV, SDT, CCCD, TUOI, TENTK)
VALUES
    ('NV001', N'Nguyen Tri Khoi', N'123456789', 'CCCD001', 18, 'admin'),
    ('NV002', N'Tran Thi Loan', N'987654321', 'CCCD002', 19, 'trikhoi'),
    ('NV003', N'Le Van Phuong Em', N'555555555', 'CCCD003', 20, 'loan'),
    ('NV004', N'Pham Thi D', N'333333333', 'CCCD004', 23, 'phuonganh');

-- Nhập dữ liệu cho bảng PHONG
INSERT INTO PHONG (MAPHONG, LOAIPHONG, GIAPHONG, TINHTRANG)
VALUES
    ('P001', N'Phòng đơn', 1000000, N'Trống'),
    ('P002', N'Phòng đôi', 1500000, N'Đang được sử dụng'),
    ('P003', N'Vip', 2000000, N'Trống'),
    ('P004', N'Phòng đơn', 1000000, N'Đang được sử dụng');

-- Nhập dữ liệu cho bảng DICHVU
INSERT INTO DICHVU (MADV, TENDV, GIADV)
VALUES
    ('DV001', N'Trái Cây', 50000),
    ('DV002', N'Nước', 100000),
    ('DV003', N'Khăn', 80000),
    ('DV004', N'Dọn lại phòng', 70000);

-- Nhập dữ liệu cho bảng KHACHHANG
INSERT INTO KHACHHANG (MAKH, TENKH, CCCD, GIOTINH, SDT, MAPHONG)
VALUES
    ('KH001', 'Tran Van X', 'CCCD005', 'nam', '111111111', 'P001'),
    ('KH002', 'Le Thi Y', 'CCCD006', 'nữ', '222222222', 'P002'),
    ('KH003', 'Nguyen Van Z', 'CCCD007', 'nam', '333333333', 'P003'),
    ('KH004', 'Pham Thi W', 'CCCD008', 'nữ', '444444444', 'P004');

-- Nhập dữ liệu cho bảng HOADON
INSERT INTO HOADON (MAHD, MANV, MAKH, NGAY)
VALUES
    ('HD001', 'NV001', 'KH001', '2023-01-01'),
    ('HD002', 'NV002', 'KH002', '2023-02-01'),
    ('HD003', 'NV003', 'KH003', '2023-03-01'),
    ('HD004', 'NV004', 'KH004', '2023-04-01');

-- Nhập dữ liệu cho bảng CTHOADON
INSERT INTO CTHOADON (MAHD, MADV, MAPHONG, SOLUONG, GIADV, THANHTIEN)
VALUES
    ('HD001', 'DV001', 'P001', 2, 50000, null),
    ('HD002', 'DV002', 'P002', 3, 100000, null),
    ('HD003', 'DV001', 'P003', 1, 50000, null),
    ('HD004', 'DV002', 'P004', 2, 100000, null),


-----------------------------TẠO CÁC RÀNG BUỘC--------------------------------
--------THÊM, CẬP NHẬT TUỔI CỦA NHÂN VIÊN
CREATE TRIGGER CheckAgeTrigger
ON NHANVIEN
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    -- Kiểm tra tuổi khi chèn hoặc cập nhật
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE DATEDIFF(YEAR, TUOI, GETDATE()) < 18
    )
    BEGIN
        PRINT 'TUỔI NHÂN VIÊN PHẢI TỪ 18 TUỔI TRỞ LÊN'
        ROLLBACK; -- Hủy bỏ thao tác chèn hoặc cập nhật
    END
    ELSE
    BEGIN
        -- Nếu không có vấn đề, thực hiện thao tác chèn hoặc cập nhật
        INSERT INTO NHANVIEN (MANV, TENNV, SDT, CCCD, TUOI, TENTK)
        SELECT MANV, TENNV, SDT, CCCD, TUOI, TENTK
        FROM inserted;
    END
END;
---------------------------------------------------------------------------------------
DROP TABLE CTHOADON
DROP TABLE DICHVU
DROP TABLE HOADON
DROP TABLE KHACHHANG
DROP TABLE NHANVIEN
DROP TABLE PHONG	
DROP TABLE TAIKHOAN

---------------------------------------------------------------------------------------

SELECT*FROM TAIKHOAN
SELECT*FROM HOADON
SELECT*FROM CTHOADON
SELECT*FROM DICHVU