﻿use HYT
GO

-- ADMIN 
EXEC sp_addlogin 'HYT_ADMIN', '12345', 'HYT';  
GO

EXEC sp_addsrvrolemember 'HYT_ADMIN', 'sysadmin';  
GO

-- VODANH
EXEC sp_addlogin 'HYT_VODANH', '12345', 'HYT';  

CREATE USER VODANH FOR LOGIN HYT_VODANH

GRANT SELECT, INSERT ON dbo.TAIKHOAN TO VODANH 

GRANT EXECUTE ON OBJECT::SP_KTTenDangNhap TO VODANH
GRANT EXECUTE ON OBJECT::SP_KTMatKhau TO VODANH
GRANT EXECUTE ON OBJECT::Sp_DangNhap TO VODANH
GO

--GRANT EXECUTE ON OBJECT::Sp_DangNhap TO VODANH
GO

-- QUANTRI
EXEC sp_addlogin 'HYT_QT', '12345', 'HYT';  

CREATE USER QUANTRI FOR LOGIN HYT_QT


GRANT SELECT, INSERT, UPDATE, DELETE ON SANPHAM TO QUANTRI
GRANT SELECT, INSERT, UPDATE, DELETE ON LUUVETGIA TO QUANTRI
GRANT SELECT ON LICHSUNHAP TO QUANTRI
GRANT SELECT ON DONHANG TO QUANTRI
GRANT SELECT ON CT_DONHANG TO QUANTRI
GO

-- KHACHHANG
EXEC sp_addlogin 'HYT_KHACHHANG', '12345', 'HYT';  

CREATE USER KHACHHANG FOR LOGIN HYT_KHACHHANG

GRANT SELECT, INSERT, UPDATE ON KHACHHANG TO KHACHHANG
GO

-- QUANLY
EXEC sp_addlogin 'HYT_QL', '12345', 'HYT';  

CREATE USER QUANLY FOR LOGIN HYT_QL

--GRANT SELECT, INSERT ON  TO QUANLY
GO

-- NHANVIEN
EXEC sp_addlogin 'HYT_NHANVIEN', '12345', 'HYT';  

CREATE USER NHANVIEN FOR LOGIN HYT_NHANVIEN

GRANT SELECT, INSERT ON NHANVIEN TO NHANVIEN 
GO

--NHÂN SỰ
EXEC sp_addlogin 'HYT_NHANSU', '12345', 'HYT';  

CREATE USER NHANSU FOR LOGIN HYT_NHANSU
GO

--TAIXE
EXEC sp_addlogin 'HYT_TAIXE', '12345', 'HYT';  

CREATE USER TAIXE FOR LOGIN HYT_TAIXE
GO

-- TẠO ROLE
EXEC SP_ADDROLE 'ROLE_USERS'

-- ADD THÊM USER VÀO ROLE NÀY
EXEC sp_addrolemember 'ROLE_USERS', 'QUANTRI'
EXEC sp_addrolemember 'ROLE_USERS', 'KHACHHANG'
EXEC sp_addrolemember 'ROLE_USERS', 'QUANLY'
EXEC sp_addrolemember 'ROLE_USERS', 'NHANVIEN'
EXEC sp_addrolemember 'ROLE_USERS', 'NHANSU'
EXEC sp_addrolemember 'ROLE_USERS', 'TAIXE'
-- CẤP QUYỀN CHO ROLE
GRANT SELECT, UPDATE ON TAIKHOAN(MATKHAU) TO ROLE_USERS