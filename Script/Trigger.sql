USE HYT
GO

--Tạo trigger để tự cập nhật Thành tiền ở bản chi tiết hóa đơn 
--và Tổng tiền ở bảng hóa đơn KHI THÊM
-- drop trigger Insert_CT_DONHANG_ThanhTien_TongTien
 Create trigger Insert_CT_DONHANG_ThanhTien_TongTien
    on CT_DONHANG
    for insert
    as
    begin
       begin
       -- update thanh tien
    	update CT_DONHANG
    	 set THANHTIEN = I.SOLUONG * (SP.GIAGOC - (SP.GIAGOC * SP.KHUYENMAI / 100) - GG.GIAGIAM)
    	from inserted I, 
    		 CT_DONHANG CTHD,
    		 DONHANG HD,
    		 SANPHAM SP,
			 GIAMGIA GG
        --Chi tiết hóa đơn mới thêm vào phải tồn tại sản phẩm, và mã hóa đơn
    	where SP.MASP=I.MASP and SP.MASP=CTHD.MASP and HD.MADH=I.MADH and HD.MADH=CTHD.MADH
       end
       -- update tong tien 
       begin
       update DONHANG
        set TONGTIEN = (SELECT SUM(CT.THANHTIEN)
						FROM CT_DONHANG CT JOIN DONHANG DH ON DH.MADH = CT.MADH
						WHERE DH.MADH = I.MADH)
       from DONHANG DH, inserted I
       where DH.MADH = I.MADH
       end 
    end 
  
GO

--Tạo trigger để tự cập nhật Thành tiền ở bản chi tiết hóa đơn 
--và Tổng tiền ở bảng hóa đơn KHI UPDATE
  --drop trigger update_CT_DONHANG_ThanhTien_TongTien
Create trigger update_CT_DONHANG_ThanhTien_TongTien
    on CT_DONHANG
    for update
    as
    begin
    --update mã hóa đơn ở chi tiết hóa đơn
       --update số lượng ở chi tiết hóa đơn
    if UPDATE(SOLUONG)
       begin
       update CT_DONHANG
      set THANHTIEN = I.SOLUONG * (SP.GIAGOC - (SP.GIAGOC * SP.KHUYENMAI / 100) - GG.GIAGIAM)
    	from inserted I, 
    		 CT_DONHANG CTHD,
    		 DONHANG HD,
    		 SANPHAM SP,
			 GIAMGIA GG

    	where SP.MASP = I.MASP and SP.MASP = CTHD.MASP and HD.MADH = I.MADH and HD.MADH = CTHD.MADH
		AND GG.MASP = I.MASP
       
       update DONHANG
       set TONGTIEN = (SELECT SUM(CT.THANHTIEN)
						FROM CT_DONHANG CT JOIN DONHANG DH ON DH.MADH = CT.MADH
						WHERE DH.MADH = DH1.MADH)
       from DONHANG DH1 join deleted D on DH1.MADH= D.MADH
       end
    end 
  
GO

-- TẠO TRIGGER cập nhật lại tổng tiền khi xóa CT hóa đơn
  --drop trigger delete_CT_DONHANG_TongTien
Create trigger delete_CT_DONHANG_TongTien
    on CT_DONHANG
    for delete
    as
    begin
       -- cập nhật lại tổng tiền khi xóa CT hóa đơn
       update DONHANG
       set TONGTIEN = (SELECT SUM(CT.THANHTIEN)
						FROM CT_DONHANG CT JOIN DONHANG DH ON DH.MADH = CT.MADH
						WHERE DH.MADH = DH1.MADH)
       from DONHANG DH1 join deleted D on DH1.MADH = D.MADH
    end 
  
GO
-------------------------------------------------

--Tạo trigger để tự cập nhật số lượng tồn của sản phẩm khi thêm chi tiết hoá đơn
-- drop trigger Insert_SANPHAM_SOLUONGTON
 Create trigger Insert_SANPHAM_SOLUONGTON
    on CT_DONHANG
    for insert
    as
    begin
       -- update SO LUONG TON
    	update SANPHAM
    	 set SOLUONGTON = SOLUONGTON - I.SOLUONG
    	from inserted I, 
    		 CT_DONHANG CTHD,
    		 SANPHAM SP

        --Chi tiết hóa đơn mới thêm vào phải tồn tại sản phẩm, và mã hóa đơn
    	where SP.MASP=I.MASP and SP.MASP=CTHD.MASP 
    end 
  
GO

--Tạo trigger để tự cập nhật SỐ LƯỢNG TỒN Ở SẢN PHẨM KHI THAY chi tiết hóa đơn 

  --drop trigger update_CT_DONHANG_ThanhTien_TongTien
Create trigger update_SOLUONGTON_SANPHAM
    on CT_DONHANG
    for update
    as
    begin
       --update số lượng ở chi tiết hóa đơn
    if UPDATE(SOLUONG)
       begin
       update SANPHAM
		set SOLUONGTON = SOLUONGTON - I.SOLUONG + d.SOLUONG
    	from inserted I, 
			deleted d,
    		 CT_DONHANG CTHD,
    		 SANPHAM SP

    	where SP.MASP = I.MASP and SP.MASP = CTHD.MASP and d.masp = sp.masp
		end
	end
GO

-- TẠO TRIGGER cập nhật số lượng tồn khi xóa CT hóa đơn
  --drop trigger delete_CT_DONHANG_TongTien
Create trigger delete_SANPHAM_SOLUONGTON
    on CT_DONHANG
    for delete
    as
    begin
       -- cập nhật lại tổng tiền khi xóa CT hóa đơn
       update SANPHAM
       set SOLUONGTON = SOLUONGTON + D.SOLUONG
       from SANPHAM SP join deleted D on SP.MASP = D.MASP
    end 
  
GO