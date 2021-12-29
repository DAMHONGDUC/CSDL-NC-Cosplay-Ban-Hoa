using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CSDLNC_CosplayBanHoa
{
    public partial class DonHang_KH : Form
    {       
        Thread t;
        string MASP;
        string giagiam;
        int soluongton;
        string MAKH;
        string TONGCONG;
        string SLMUA;
        string THANHTIEN;

        public DonHang_KH(string masp, string tensp, string dongia, string slton, string makh)
        {
            InitializeComponent();
            txtbox_tensp_DH_KH.Text = tensp;
            txtbox_dongia_DH_KH.Text = dongia;
            txtbox_slton_DH_KH.Text = slton;
            txtBox_slmua_DH_KH.Text = "1";
            MASP = masp;
            soluongton = Int32.Parse(slton);
            MAKH = makh;            
        }

        // xử lí đóng form đơn hàng và mở form TT người nhận
        public void open_FormTTNguoiNhan(object obj)
        {
            Application.Run(new TTNguoiNhan_KH(MAKH, TONGCONG, MASP, SLMUA, THANHTIEN));
        }
        private void btn_tieptuc_DH_KH_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormTTNguoiNhan);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void Load_Data()
        {
            string sql = "SELECT GIAGIAM " +
                "FROM GIAMGIA " +
                "WHERE MASP = '" + MASP + "'";
            giagiam = Functions.GetFieldValues(sql);
            txtBox_giagiam_DH_KH.Text = giagiam;            
        }

        private void DonHang_KH_Load(object sender, EventArgs e)
        {
            Load_Data();
            Auto_Tong_Tien();
        }

        private void Auto_Tong_Tien()
        {         
            float tongcong = float.Parse(txtbox_dongia_DH_KH.Text.ToString()) * Int32.Parse(txtBox_slmua_DH_KH.Text.Trim().ToString()) - float.Parse(giagiam);
            if (tongcong > 0)
                txtBox_tongcong_DH_KH.Text = tongcong.ToString("0.0000");
            else
                txtBox_tongcong_DH_KH.Text = "";
        }

        private void btn_tangsl_DH_KH_Click(object sender, EventArgs e)
        {
            int slmua = Int32.Parse(txtBox_slmua_DH_KH.Text.Trim().ToString());
            slmua += 1;
            if (slmua > soluongton) slmua = soluongton;
            txtbox_slton_DH_KH.Text = (soluongton - slmua).ToString();
            txtBox_slmua_DH_KH.Text = slmua.ToString();
            Auto_Tong_Tien();
        }

        private void btn_giamsl_DH_KH_Click(object sender, EventArgs e)
        {
            int slmua = Int32.Parse(txtBox_slmua_DH_KH.Text.Trim().ToString());
            slmua -= 1;
            if (slmua < 0) slmua = 0;
            txtbox_slton_DH_KH.Text = (soluongton - slmua).ToString();
            txtBox_slmua_DH_KH.Text = slmua.ToString();
            Auto_Tong_Tien();
        }

        private void DonHang_KH_FormClosing(object sender, FormClosingEventArgs e)
        {
           TONGCONG = txtBox_tongcong_DH_KH.Text.Trim();
           SLMUA = txtBox_slmua_DH_KH.Text.Trim();
           THANHTIEN = (float.Parse(txtbox_dongia_DH_KH.Text.ToString()) 
                * Int32.Parse(txtBox_slmua_DH_KH.Text.Trim().ToString())).ToString();
        }
    }
}
