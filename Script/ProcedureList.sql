﻿USE HYT
GO

----SP phần của Đức

-- DROP PROC SP_KTTenDangNhap
-- Kiểm tra tên đăng nhập
CREATE PROC SP_KTTenDangNhap
	@TENDN VARCHAR(50)
AS
BEGIN	
	-- kiểm tra	
	IF EXISTS (SELECT TENDN FROM TAIKHOAN WHERE TENDN = @TENDN)
	RETURN 1
	ELSE RETURN 0
END
GO

-- DROP PROC SP_KTMatKhau
-- Kiểm tra mật khẩu
CREATE PROC SP_KTMatKhau
	@TENDN VARCHAR(50),
	@MATKHAU  VARCHAR(50)
AS
BEGIN	
	-- kiểm tra	
	IF EXISTS (SELECT MATKHAU FROM TAIKHOAN WHERE TENDN = @TENDN AND MATKHAU = @MATKHAU)
	RETURN 1
	ELSE RETURN 0
END
GO

-- DROP PROC Sp_DangNhap
-- Xử lí đăng nhập tài khoản
CREATE PROC Sp_DangNhap
	@TENDN VARCHAR(50),
	@MATKHAU VARCHAR(50),
	@ID VARCHAR(15) OUTPUT,
	@LOAITK INT OUTPUT
AS
BEGIN	
	SET @ID = 'NULL'

	-- lấy mã tài khoản		
	SET @ID = (SELECT ID
				FROM TAIKHOAN
				WHERE TENDN = @TENDN
				AND MATKHAU = @MATKHAU)

	-- lấy loại tài khoản		
	SET @LOAITK = (SELECT LOAITK
				FROM TAIKHOAN
				WHERE TENDN = @TENDN
				AND MATKHAU = @MATKHAU)

	-- xử lí đăng nhập
	if (@ID != 'NULL')
	BEGIN
		PRINT N'Đăng nhập thành công'
		RETURN 1
	END
	ELSE RETURN 0	
END
GO

-- DROP PROC Sp_KH_TimKiemSP
-- Xử lí đăng nhập tài khoản
CREATE PROC Sp_KH_TimKiemSP
	@TUKHOA NVARCHAR(50)	
AS
BEGIN	

	SELECT MASP, TENSP, GIAGOC, KHUYENMAI, MOTA, CHITIETSP, HINHANH, SOLUONGTON
	FROM SANPHAM
	WHERE THANHPHANCHINH LIKE '%'+ @TUKHOA + '%' 
	OR TENSP LIKE '%'+ @TUKHOA + '%' 
	OR CHITIETSP LIKE '%'+ @TUKHOA + '%' 
END
GO

-- DROP PROC Sp_KH_ThemDH
-- Xử lí thêm đơn hàng
CREATE PROC Sp_KH_ThemDH
	@MADH VARCHAR(15),
	@MAKH VARCHAR(15),
	@TENNGUOINHAN NVARCHAR(255) ,
	@DIACHI_NGUOINHAN NVARCHAR(255) ,
	@SDT_NGUOINHAN NVARCHAR(255) ,
	@PHIVANCHUYEN DECIMAL(19,4) ,
	@HINHTHUCTHANHTOAN INT ,
	@NGAYMUONGIAO DATETIME ,
	@NGAYLAP DATE ,
	@TINHTRANG INT,
	@TONGTIEN DECIMAL(19,4) 	
AS
BEGIN	
	--Kiểm tra mã DH (KIỂM TRA KHÓA CHÍNH)
	IF EXISTS (SELECT MADH FROM DONHANG WHERE MADH = @MADH)
	BEGIN
		PRINT N'MÃ DH ĐÃ TỒN TẠI'
		RETURN 0
	END

	--Kiểm tra mã KH
	IF NOT EXISTS (SELECT MAKH FROM KHACHHANG WHERE MAKH = @MAKH)
	BEGIN
		PRINT N'MÃ KH KHÔNG TỒN TẠI'
		RETURN -1
	END

	-- XỬ LÍ THÊM DH
	INSERT INTO DONHANG
	VALUES
	(@MADH,@MAKH,N''+@TENNGUOINHAN+'',N''+@DIACHI_NGUOINHAN+'',@SDT_NGUOINHAN,@PHIVANCHUYEN,@HINHTHUCTHANHTOAN
	,@NGAYMUONGIAO,@NGAYLAP,@TINHTRANG,@TONGTIEN)
	RETURN 1
END
GO

-- DROP PROC Sp_KH_ThemCTDH
-- Xử lí thêm chi tiết đơn hàng
CREATE PROC Sp_KH_ThemCTDH
	@MADH VARCHAR(15),
	@MASP VARCHAR(15),
	@SOLUONG INT ,
	@THANHTIEN DECIMAL(19,4)
AS
BEGIN	
	--Kiểm tra mã SP
	IF NOT EXISTS (SELECT MASP FROM SANPHAM WHERE MASP = @MASP)
	BEGIN
		PRINT N'MÃ SP KHÔNG TỒN TẠI'
		RETURN 0
	END

	--Kiểm tra mã DH
	IF NOT EXISTS (SELECT MADH FROM DONHANG WHERE MADH = @MADH)
	BEGIN
		PRINT N'MÃ DH KHÔNG TỒN TẠI'
		RETURN -1
	END

	-- XỬ LÍ THÊM CTDH
	INSERT INTO CT_DONHANG
	VALUES
	(@MADH, @MASP, @SOLUONG, @THANHTIEN )
	RETURN 1
END
GO


-- DROP PROC Sp_KH_LayThongTinDH
-- Xử lí lấy thông tin đơn hàng
CREATE PROC Sp_KH_LayThongTinDH
	@MAKH VARCHAR(15)	
AS
BEGIN	
	--Kiểm tra mã KH
	IF NOT EXISTS (SELECT MAKH FROM KHACHHANG WHERE MAKH = @MAKH)
	BEGIN
		PRINT N'MÃ KH KHÔNG TỒN TẠI'
		RETURN 0
	END
	
	-- lấy thông tin
	SELECT * FROM DONHANG WHERE MAKH = @MAKH
	RETURN 1
END
GO


-- DROP PROC Sp_KH_LayThongTinCTDH
-- Xử lí lấy thông tin chi tiết đơn hàng
CREATE PROC Sp_KH_LayThongTinCTDH
	@MADH VARCHAR(15)	
AS
BEGIN	
	--Kiểm tra mã DH
	IF NOT EXISTS (SELECT MADH FROM DONHANG WHERE MADH = @MADH)
	BEGIN
		PRINT N'MÃ DH KHÔNG TỒN TẠI'
		RETURN 0
	END
	
	-- lấy thông tin
	SELECT CTDH.MADH, CTDH.MASP , SP.TENSP ,SP.GIAGOC, SP.KHUYENMAI, GG.GIAGIAM, CTDH.SOLUONG, CTDH.THANHTIEN
	FROM CT_DONHANG CTDH, SANPHAM SP, GIAMGIA GG
	WHERE CTDH.MADH = @MADH
	AND SP.MASP = CTDH.MASP
	AND GG.MASP = SP.MASP
	RETURN 1
END
GO

--------------------------------------------------------------

--------------------------------------------------------------

----------- Phần SP của Tuấn

-- Thêm nhân viên mới
CREATE PROCEDURE SP_NhanSu_ThemNV
	@MANV VARCHAR(15),
	@ID VARCHAR(15),
	@TENNV NVARCHAR(255),
	@CHINHANHLV VARCHAR(15),
	@LOAINV INT,
	@LUONG DECIMAL(19,4)
AS
BEGIN
	-- Them thông tin vào bảng NHANVIEN
	INSERT dbo.NHANVIEN (MANV, ID, TENNV, CHINHANHLV, LOAINV)
	VALUES( @MANV, @ID, @TENNV, @CHINHANHLV, @LOAINV)
	
	-- Them thông tin về lương của nhân viên mới vào bảng LUONG
	INSERT dbo.LUONG(MANV, NGAY, LUONG)
	VALUES(@MANV, GETDATE(), @LUONG)
END
GO

-- Cập nhật thông tin nhân viên
CREATE PROCEDURE SP_NhanSu_CapNhatNV
	@MANV VARCHAR(15),
	@ID VARCHAR(15),
	@TENNV NVARCHAR(255),
	@CHINHANHLV VARCHAR(15),
	@LOAINV INT,
	@LUONG DECIMAL(19,4)
AS
BEGIN
	-- Cập nhật bảng NHANVIEN
	UPDATE NHANVIEN 
	SET 
		ID = @ID,
		TENNV = @TENNV,
		CHINHANHLV = @CHINHANHLV,
		LOAINV = @LOAINV
	WHERE MANV = @MANV
	
	-- Cập nhật bảng LUONG
	UPDATE dbo.LUONG
	SET
		LUONG = @LUONG,
		NGAY = GETDATE()
	WHERE MANV = @MANV
END
GO

-- Xóa 1 một nhân viên
CREATE PROCEDURE SP_NhanSu_XoaNV
	@MANV VARCHAR(15)
AS
BEGIN
	UPDATE dbo.LICHSUNHAP SET NGUOINHAP=NULL WHERE NGUOINHAP=@MANV
	DELETE dbo.DIEMDANH WHERE MANV = @MANV
	DELETE dbo.LUONG WHERE MANV = @MANV
	DELETE dbo.NHANVIEN WHERE MANV = @MANV
END
GO

-- Tìm danh sách nhân viên theo chi nhánh
CREATE PROCEDURE SP_NhanSu_TimNVtheoCN
	@MACN VARCHAR(15)
AS
BEGIN
	SELECT NV.MANV, NV.ID, NV.TENNV, NV.CHINHANHLV, NV.LOAINV ,L.LUONG
	FROM NHANVIEN NV, LUONG L
	WHERE NV.MANV = L.MANV
	AND NV.CHINHANHLV = @MACN
	GROUP BY NV.MANV, L.MANV, L.NGAY, NV.ID, NV.TENNV, NV.CHINHANHLV, NV.LOAINV, L.LUONG
	HAVING ABS(DATEDIFF(day, GETDATE(), L.NGAY)) = (SELECT MIN(ABS(DATEDIFF(day, GETDATE(), L.NGAY))) FROM LUONG L1 WHERE L1.MANV = L.MANV GROUP BY L1.MANV, L1.NGAY)
END
GO


--------------------------------------------------------------

--------------------------------------------------------------

----------- Phần SP của Huy

--
--  LỊCH SỬ NHẬP
--
-- Lấy tât cả lịch sử nhập của sản phẩm
CREATE PROCEDURE SP_QT_TatCaLSN
AS
BEGIN
	SELECT LSN.MASP,LSN.NGAYNHAP,NV.TENNV,LSN.SOLUONG FROM LICHSUNHAP LSN, NHANVIEN NV
	WHERE LSN.NGUOINHAP = NV.MANV
	ORDER BY LSN.NGAYNHAP DESC, LSN.MASP ASC
END
GO
-- Tìm lịch sử nhập theo mã sản phẩm
CREATE PROCEDURE SP_QT_TimLSNTheoMaSP
	@MASP VARCHAR(15)
AS
BEGIN
	SELECT LSN.MASP,LSN.NGAYNHAP,NV.TENNV,LSN.SOLUONG FROM LICHSUNHAP LSN, NHANVIEN NV
	WHERE LSN.NGUOINHAP = NV.MANV AND LSN.MASP=@MASP 
	ORDER BY LSN.NGAYNHAP DESC, LSN.MASP ASC
END
GO

-- Tìm lịch sử nhập theo theo ngày nhập
CREATE PROCEDURE SP_QT_TimLSNTheoNgayNhap
	@NGAYNHAP DATE
AS
BEGIN
	SELECT LSN.MASP,LSN.NGAYNHAP,NV.TENNV,LSN.SOLUONG FROM LICHSUNHAP LSN, NHANVIEN NV
	WHERE LSN.NGUOINHAP = NV.MANV AND LSN.NGAYNHAP = @NGAYNHAP 
	ORDER BY LSN.NGAYNHAP DESC, LSN.MASP ASC
END
GO
--
--  LỊCH SỬ XUẤT
--
-- Lấy lịch sử xuất của sản phẩm
CREATE PROCEDURE SP_QT_TatCaLSX
AS
BEGIN
	SELECT CTDH.MASP, DH.NGAYLAP, CTDH.SOLUONG FROM DONHANG DH, CT_DONHANG CTDH
WHERE DH.MADH=CTDH.MADH
ORDER BY DH.NGAYLAP DESC, CTDH.MASP ASC
END
GO

-- Lấy lịch sử xuất theo masp
CREATE PROCEDURE SP_QT_TimLSXTheoMaSP
	@MASP VARCHAR(15)
AS
BEGIN
	SELECT CTDH.MASP, DH.NGAYLAP, CTDH.SOLUONG FROM DONHANG DH, CT_DONHANG CTDH
WHERE DH.MADH=CTDH.MADH AND CTDH.MASP=@MASP
ORDER BY DH.NGAYLAP DESC, CTDH.MASP ASC
END
GO

-- Lấy lịch sử xuất của sản phẩm theo ngày
CREATE PROCEDURE SP_QT_TimLSXTheoNgayLap 
	@NGAYNHAP DATE
AS
BEGIN
	SELECT CTDH.MASP, DH.NGAYLAP, CTDH.SOLUONG FROM DONHANG DH, CT_DONHANG CTDH
WHERE DH.MADH=CTDH.MADH AND DH.NGAYLAP = @NGAYNHAP
ORDER BY DH.NGAYLAP DESC, CTDH.MASP ASC
END
GO

--
--  QUẢN TRỊ - TẤT CẢ SP
--
--DROP PROCEDURE SP_QT_TatCaSP
CREATE PROCEDURE SP_QT_TatCaSP 
AS
BEGIN
	SELECT SP.MASP,SP.TENSP, CN.TENCN,SP.THANHPHANCHINH, SP.MOTA,SP.SOLUONGTON,SP.GIAGOC,SP.CHITIETSP,SP.KHUYENMAI,SP.HINHANH FROM SANPHAM SP, CHINHANH CN
	ORDER BY SP.MASP ASC
END
GO

--
--  QUẢN LÝ - TẤT CẢ SP
--
CREATE PROCEDURE SP_QT_TatCaSP 
AS
BEGIN
	SELECT SP.MASP,SP.TENSP, CN.TENCN,SP.THANHPHANCHINH, SP.MOTA,SP.SOLUONGTON,SP.GIAGOC,SP.CHITIETSP,SP.KHUYENMAI,SP.HINHANH FROM SANPHAM SP, CHINHANH CN
	ORDER BY SP.MASP ASC
END