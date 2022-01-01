﻿USE master
GO
IF DB_ID('HYT') IS NOT NULL 
	DROP DATABASE HYT
GO
CREATE DATABASE HYT
GO
USE HYT
GO

--bảng chi nhánh
CREATE TABLE CHINHANH
(
MACN VARCHAR(15),
TENCN NVARCHAR(255) NOT NULL,
DIACHI NVARCHAR(255) NOT NULL,
TINH NVARCHAR(30) NOT NULL,
HUYEN NVARCHAR(30) NOT NULL

CONSTRAINT PK_CN
PRIMARY KEY (MACN)
)
GO

--bảng nhân viên
CREATE TABLE NHANVIEN
(
MANV VARCHAR(15),
ID VARCHAR(15),
TENNV NVARCHAR(255) NOT NULL,
CHINHANHLV VARCHAR(15) NOT NULL,
LOAINV INT NOT NULL

CONSTRAINT PK_NV
PRIMARY KEY (MANV)
)
GO

--bảng sản phẩm
CREATE TABLE SANPHAM
(
MASP VARCHAR(15),
TENSP NVARCHAR(255) NOT NULL,
THANHPHANCHINH NVARCHAR(255) NOT NULL,
HINHANH VARCHAR(255) NOT NULL,
MOTA NVARCHAR(255) NOT NULL,
GIAGOC DECIMAL(19,4) NOT NULL,
CHITIETSP NVARCHAR(255) NOT NULL,
KHUYENMAI INT NOT NULL,
SOLUONGTON INT NOT NULL,
NGAYNHAP DATE NOT NULL

CONSTRAINT PK_SP
PRIMARY KEY (MASP)
)
GO

--bảng sản phẩm - chi nhánh
CREATE TABLE SANPHAM_CHINHANH
(
MASP VARCHAR(15),
MACN VARCHAR(15)

CONSTRAINT PK_SPCN
PRIMARY KEY (MASP,MACN)
)
GO

--bảng lưu vết giá
CREATE TABLE LUUVETGIA
(
MASP VARCHAR(15),
NGAY DATE NOT NULL,
GIA DECIMAL(19,4) NOT NULL

CONSTRAINT PK_LVG
PRIMARY KEY (MASP)
)
GO

CREATE TABLE LICHSUNHAP
(
MASP VARCHAR(15),
NGAYNHAP DATE,
NGUOINHAP VARCHAR(15),
SOLUONG INT

CONSTRAINT PK_LSN
PRIMARY KEY (MASP,NGAYNHAP)
)
GO

--bảng chủ đề
CREATE TABLE CHUDE
(
MACD VARCHAR(15),
TENCD NVARCHAR(255) NOT NULL

CONSTRAINT PK_CD
PRIMARY KEY (MACD)
)
GO

--bảng chủ đề - sản phẩm
CREATE TABLE CHUDE_SANPHAM
(
MACD VARCHAR(15),
MASP VARCHAR(15)

CONSTRAINT PK_CDSP
PRIMARY KEY (MACD,MASP)
)
GO

--bảng giảm giá
CREATE TABLE GIAMGIA
(
MASP VARCHAR(15),
GIAGIAM DECIMAL(19,4) NOT NULL

CONSTRAINT PK_GG
PRIMARY KEY (MASP)
)
GO

--bảng đơn hàng
CREATE TABLE DONHANG
(
MADH VARCHAR(15),
MAKH VARCHAR(15),
TENNGUOINHAN NVARCHAR(255) NOT NULL,
DIACHI_NGUOINHAN NVARCHAR(255) NOT NULL,
SDT_NGUOINHAN NVARCHAR(255) NOT NULL,
PHIVANCHUYEN DECIMAL(19,4) NOT NULL,
HINHTHUCTHANHTOAN INT NOT NULL,
NGAYMUONGIAO DATETIME NOT NULL,
NGAYLAP DATE NOT NULL,
TINHTRANG INT NOT NULL,
TONGTIEN DECIMAL(19,4) NOT NULL

CONSTRAINT PK_DH
PRIMARY KEY (MADH)
) --ON  DonHangDaGiaoPartition_Schema(TINHTRANG)
GO

--bảng chi tiết đơn hàng
CREATE TABLE CT_DONHANG
(
MADH VARCHAR(15),
MASP VARCHAR(15),
SOLUONG INT NOT NULL,
THANHTIEN DECIMAL(19,4) NOT NULL

CONSTRAINT PK_CTDH
PRIMARY KEY (MADH,MASP)
)

--bảng khách hàng
CREATE TABLE KHACHHANG
(
MAKH VARCHAR(15),
ID VARCHAR(15),
TENKH NVARCHAR(50) NOT NULL,
STK VARCHAR(30) NOT NULL

CONSTRAINT PK_KH
PRIMARY KEY (MAKH)
)
GO

--bảng tài khoản
CREATE TABLE TAIKHOAN
(
ID VARCHAR(15),
TENDN VARCHAR(50) NOT NULL,
MATKHAU VARCHAR(50) NOT NULL,
LOAITK INT NOT NULL,
SDT CHAR(15) NOT NULL,
EMAIL VARCHAR(255) NOT NULL,
DIACHI NVARCHAR(255) NOT NULL

CONSTRAINT PK_ACC
PRIMARY KEY (ID)
)
GO

--bảng điểm danh
CREATE TABLE DIEMDANH
(
MANV VARCHAR(15),
NGAY DATE NOT NULL

CONSTRAINT PK_DD
PRIMARY KEY(MANV)
)
GO

--bảng lương
CREATE TABLE LUONG
(
MANV VARCHAR(15),
NGAY DATE NOT NULL,
LUONG DECIMAL(19,4) NOT NULL

CONSTRAINT PK_L
PRIMARY KEY (MANV)
)
GO

--bảng tài xế
CREATE TABLE TAIXE
(
MATX VARCHAR(15),
ID VARCHAR(15),
TENTX NVARCHAR(255) NOT NULL,
CMND CHAR(50) NOT NULL,
KHUVUC NVARCHAR(255) NOT NULL,
STK CHAR(50) NOT NULL,
BIENSOXE CHAR(50) NOT NULL

CONSTRAINT PK_TX
PRIMARY KEY (MATX)
)
GO

--bảng xử lí đơn hàng
CREATE TABLE XULI_DONHANG
(
MADH VARCHAR(15),
MATX VARCHAR(15),
NGAYKHNHAN DATETIME,
NGAYTXNHANH DATETIME

CONSTRAINT PK_XLDH
PRIMARY KEY (MADH)
)
GO

--Ràng buộc khoá ngoại

ALTER TABLE SANPHAM_CHINHANH
ADD
	CONSTRAINT FK_SPCN_CN
	FOREIGN KEY(MACN)
	REFERENCES CHINHANH,

	CONSTRAINT FK_SPCN_SP
	FOREIGN KEY(MASP)
	REFERENCES SANPHAM
GO

ALTER TABLE LICHSUNHAP
ADD
	CONSTRAINT FK_LSN_SP
	FOREIGN KEY(MASP)
	REFERENCES SANPHAM,

	CONSTRAINT FK_LSN_NV
	FOREIGN KEY(NGUOINHAP)
	REFERENCES NHANVIEN
GO

ALTER TABLE LUUVETGIA
ADD
	CONSTRAINT FK_LVG_SP
	FOREIGN KEY(MASP)
	REFERENCES SANPHAM
GO

ALTER TABLE GIAMGIA
ADD
	CONSTRAINT FK_GG_SP
	FOREIGN KEY(MASP)
	REFERENCES SANPHAM
GO

ALTER TABLE CHUDE_SANPHAM
ADD
	CONSTRAINT FK_CDSP_CD
	FOREIGN KEY(MACD)
	REFERENCES CHUDE,

	CONSTRAINT FK_CDSP_SP
	FOREIGN KEY(MASP)
	REFERENCES SANPHAM
GO

ALTER TABLE CT_DONHANG
ADD
	CONSTRAINT FK_CTDH_DH
	FOREIGN KEY(MADH)
	REFERENCES DONHANG,

	CONSTRAINT FK_CTDH_SP
	FOREIGN KEY(MASP)
	REFERENCES SANPHAM
GO

ALTER TABLE KHACHHANG
ADD
	CONSTRAINT FK_KH_TK
	FOREIGN KEY(ID)
	REFERENCES TAIKHOAN
GO

ALTER TABLE TAIXE
ADD
	CONSTRAINT FK_TX_TK
	FOREIGN KEY(ID)
	REFERENCES TAIKHOAN
GO

ALTER TABLE LUONG
ADD
	CONSTRAINT FK_L_NV
	FOREIGN KEY(MANV)
	REFERENCES NHANVIEN
GO

ALTER TABLE DIEMDANH
ADD
	CONSTRAINT FK_DD_NV
	FOREIGN KEY(MANV)
	REFERENCES NHANVIEN
GO

ALTER TABLE NHANVIEN
ADD
	CONSTRAINT FK_NV_TK
	FOREIGN KEY(ID)
	REFERENCES TAIKHOAN,

	CONSTRAINT FK_NV_CN
	FOREIGN KEY(CHINHANHLV)
	REFERENCES CHINHANH
GO

ALTER TABLE XULI_DONHANG
ADD
	CONSTRAINT FK_XLDH_TX
	FOREIGN KEY(MATX)
	REFERENCES TAIXE,

	CONSTRAINT FK_XLDH_DH
	FOREIGN KEY(MADH)
	REFERENCES DONHANG
GO

ALTER TABLE DONHANG
ADD
	CONSTRAINT FK_DH_KH
	FOREIGN KEY(MAKH)
	REFERENCES KHACHHANG
GO
