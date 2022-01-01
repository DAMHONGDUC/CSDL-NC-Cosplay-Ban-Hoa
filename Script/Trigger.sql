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
        set TONGTIEN = TONGTIEN + I.THANHTIEN
		--+ (select CTHD.THANHTIEN  
  --                                from inserted I,CT_DONHANG CTHD
  --                                where DH.MADH=CTHD.MADH and CTHD.MADH=I.MADH and I.MASP=CTHD.MASP)
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