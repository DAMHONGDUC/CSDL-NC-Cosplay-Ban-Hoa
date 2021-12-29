﻿USE HYT
GO

----SP phần của Đức (ae đừng chỉnh sửa script gì trong đây nha)

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

--------------------------------------------------------------