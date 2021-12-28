USE HYT
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

--------------------------------------------------------------