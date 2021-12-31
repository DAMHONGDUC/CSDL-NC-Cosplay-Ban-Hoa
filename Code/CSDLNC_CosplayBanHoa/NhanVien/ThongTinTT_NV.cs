using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSDLNC_CosplayBanHoa
{
    public partial class ThongTinTT_NV : Form
    {
        string MANV;
        string TENSP, GIAGOC, SLTON, MASP;
        string GIAGIAM;
        DataTable tbl_KH;
        public ThongTinTT_NV(string masp,
            string tensp,
            string giagoc,
            string slton,
            string manv
            )
        {
            InitializeComponent();
            TENSP = tensp;
            MASP = masp;
            GIAGOC = giagoc;
            SLTON = slton;
            MANV = manv;
        }

        private void reset_value()
        {
            txtBox_ktsdt.Text = "";
            txtBox_tenkh.Text = "";
            txtBox_sdt.Text = "";
            txtBox_email.Text = "";
            txtBox_diachi.Text = "";
        }

        private void ThongTinTT_NV_Load(object sender, EventArgs e)
        {
            txtbox_tensp.Text = TENSP;
                txtbox_dongia.Text = GIAGOC;
            txtbox_slton.Text = SLTON;

            string sql = "SELECT GIAGIAM " +
                "FROM GIAMGIA " +
                "WHERE MASP = '" + MASP + "'";
            GIAGIAM = Functions.GetFieldValues(sql);
            txtBox_giagiam.Text = GIAGIAM;

            Auto_Tong_Tien();
        }

        private void Auto_Tong_Tien()
        {
            float tongcong = float.Parse(txtbox_dongia.Text.ToString()) * Int32.Parse(txtBox_slmua.Text.Trim().ToString()) - float.Parse(GIAGIAM);
            if (tongcong > 0)
                txtBox_tongcong_DH_KH.Text = tongcong.ToString("0.0000");
            else
                txtBox_tongcong_DH_KH.Text = "";
        }

        private void btn_giamsl_DH_KH_Click(object sender, EventArgs e)
        {
            int slmua = Int32.Parse(txtBox_slmua.Text.Trim().ToString());
            slmua -= 1;
            if (slmua < 1) slmua = 1;
            txtBox_slmua.Text = slmua.ToString();
            Auto_Tong_Tien();
        }

        private void btn_tangsl_DH_KH_Click(object sender, EventArgs e)
        {
            int slmua = Int32.Parse(txtBox_slmua.Text.Trim().ToString());
            slmua += 1;
            if (slmua >= Int32.Parse(SLTON)) slmua = Int32.Parse(SLTON);
            txtBox_slmua.Text = slmua.ToString();
            Auto_Tong_Tien();
        }

        private void btn_timtk_Click(object sender, EventArgs e)
        {
            if (cBox_KH_cotk.Checked && txtBox_ktsdt.Text.Trim().Length > 0)
            {
                string sql = "SELECT KH.TENKH, TK.SDT, TK.EMAIL, TK.DIACHI " +
                    "FROM KHACHHANG KH, TAIKHOAN TK " +
                    "WHERE TK.SDT = '" + txtBox_ktsdt.Text.Trim() + "' " +
                    "AND TK.ID = KH.ID";
                tbl_KH = Functions.GetDataToTable(sql);

                if (tbl_KH.Rows.Count == 0)
                {
                    MessageBox.Show("Số điện thoại không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                txtBox_tenkh.Text = tbl_KH.Rows[0].Field<String>(0).ToString();
                txtBox_sdt.Text = tbl_KH.Rows[0].Field<String>(1).ToString();
                txtBox_diachi.Text = tbl_KH.Rows[0].Field<String>(2).ToString();
                txtBox_email.Text = tbl_KH.Rows[0].Field<String>(3).ToString();
            }
        }

      
        private void dTP_ngaygiao_Enter(object sender, EventArgs e)
        {
            dTP_ngaygiao.CustomFormat = "yyyy-MM-dd";
        }

        private void cBox_KH_cotk_CheckedChanged(object sender, EventArgs e)
        {
            reset_value();
            if (cBox_KH_cotk.Checked)
            {
                txtBox_ktsdt.Enabled = true;
                txtBox_tenkh.Enabled = false;
                txtBox_sdt.Enabled = false;
                txtBox_email.Enabled = false;
                txtBox_diachi.Enabled = false;
            }
            else
            {
                txtBox_ktsdt.Enabled = false;
                txtBox_tenkh.Enabled = true;
                txtBox_sdt.Enabled = true;
                txtBox_email.Enabled = true;
                txtBox_diachi.Enabled = true;
            }
        }

        private void cBox_nguoinhan_CheckedChanged(object sender, EventArgs e)
        {
            if (cBox_nguoinhan.Checked == true && cBox_KH_cotk.Checked == true)
            {
                txtBox_tennguoinhan.Text = txtBox_tenkh.Text;
                txtBox_sdtnguoinhan.Text = txtBox_sdt.Text;             
                txtBox_diachinguoinhan.Text = txtBox_diachi.Text;
                dTP_ngaymua.Text = DateTime.Today.ToString();
            }
        }

        private void txtBox_diachinguoinhan_TextChanged(object sender, EventArgs e)
        {
            txtBox_phigiaohang.Text = "300000.0000";
        }

        private void btn_themdh_Click(object sender, EventArgs e)
        {

        }
    }
}
