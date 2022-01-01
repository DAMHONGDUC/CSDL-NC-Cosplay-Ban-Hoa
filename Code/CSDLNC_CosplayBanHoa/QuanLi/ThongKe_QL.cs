using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CSDLNC_CosplayBanHoa
{
    public partial class ThongKe_QL : Form
    {
        DataTable tbl_QuanLi_MHBC;
        DataTable tbl_QuanLi_MHBC2;
        DataTable tbl_QuanLi_BanCham;
        DataTable tbl_QuanLi_BanCham2;
        DataTable tbl_QuanLi_DoanhThu;
        DataTable tbl_QuanLi_DoanhThu2;
        int thang, nam;
        public ThongKe_QL()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            // lấy MASP, TENSP, SLTON vào bảng tbl_QuanLi_MHBC
            thang = Int32.Parse(cbB_Thang.Text.Trim().ToString());
            nam = Int32.Parse(cbB_Nam.Text.Trim().ToString());

            string sql = "SP_QL_DSBC " + thang + ", " + nam;
            tbl_QuanLi_MHBC = Functions.GetDataToTable(sql);

            // lấy so luong da ban   
            sql = "SP_QL_DSBC_NUM " + thang + ", " + nam;
            tbl_QuanLi_MHBC2 = Functions.GetDataToTable(sql);

            // thêm so luong da ban vào bảng 
            int i = 0;
            tbl_QuanLi_MHBC.Columns.Add("SLDB", typeof(System.Int32));
            foreach (DataRow row in tbl_QuanLi_MHBC.Rows)
            {
                row["SLDB"] = tbl_QuanLi_MHBC2.Rows[i].Field<Int32>(0);
                i++;
            }

            // lấy lợi nhuận
            sql = "SP_QL_DOANHTHU_BANCHAY " + thang + ", " + nam;
            tbl_QuanLi_MHBC2 = Functions.GetDataToTable(sql);

            // thêm lợi nhuận vào bảng
            i = 0;
            tbl_QuanLi_MHBC.Columns.Add("DOANHTHU", typeof(System.Decimal));
            foreach (DataRow row in tbl_QuanLi_MHBC.Rows)
            {
                row["DOANHTHU"] = tbl_QuanLi_MHBC2.Rows[i].Field<Decimal>(0);
                i++;
            }
            dgv_MHBC.DataSource = tbl_QuanLi_MHBC;

            // set Font cho tên cột
            dgv_MHBC.Font = new Font("Time New Roman", 13);
            dgv_MHBC.Columns[0].HeaderText = "Mã sản phẩm";
            dgv_MHBC.Columns[1].HeaderText = "Tên sản phẩm";
            dgv_MHBC.Columns[2].HeaderText = "Số lượng tồn";
            dgv_MHBC.Columns[3].HeaderText = "Số lượng đã bán";
            dgv_MHBC.Columns[4].HeaderText = "Lợi nhuận";

            // set Font cho dữ liệu hiển thị trong cột
            dgv_MHBC.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dgv_MHBC.Columns[0].Width = 200;
            dgv_MHBC.Columns[1].Width = 200;
            dgv_MHBC.Columns[2].Width = 200;
            dgv_MHBC.Columns[3].Width = 200;
            dgv_MHBC.Columns[4].Width = 200;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dgv_MHBC.AllowUserToAddRows = false;
            dgv_MHBC.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        
        private void btn_Tim_BanChay_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgv_MHBC_Click(object sender, EventArgs e)
        {
            if (tbl_QuanLi_MHBC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            
            txtBox_MaSP_MHBC.Text = dgv_MHBC.CurrentRow.Cells["MASP"].Value.ToString();
            txtBox_TenSP_MHBC.Text = dgv_MHBC.CurrentRow.Cells["TENSP"].Value.ToString();
            txtBox_SLT_MHBC.Text = dgv_MHBC.CurrentRow.Cells["SOLUONGTON"].Value.ToString();
            txtBox_SLDB_MHBC.Text = dgv_MHBC.CurrentRow.Cells["SLDB"].Value.ToString();
            txtBox_doanhthu_MHBC.Text = dgv_MHBC.CurrentRow.Cells["DOANHTHU"].Value.ToString();
        }

        private void tabPage_mathangbanchay_Enter(object sender, EventArgs e)
        {
            // khi vào tab thì load MHBC của tháng hiện tại lên
            thang = Int32.Parse(DateTime.Now.ToString("MM"));
            nam = Int32.Parse(DateTime.Now.ToString("yyyy"));

            cbB_Thang.Text = thang.ToString();
            cbB_Nam.Text = nam.ToString();
            LoadData();
        }

        private void LoadDataBanCham()
        {
            // lấy MASP, TENSP, SLTON vào bảng tbl_QuanLi_BanCham
            thang = Int32.Parse(cbx_Thang.Text.Trim().ToString());
            nam = Int32.Parse(cbx_Nam.Text.Trim().ToString());

            string sql = "SP_QL_DSBanCham " + thang + ", " + nam;
            tbl_QuanLi_BanCham = Functions.GetDataToTable(sql);

            // lấy so luong da ban   
            sql = "SP_QL_DSBanCham_NUM " + thang + ", " + nam;
            tbl_QuanLi_BanCham2 = Functions.GetDataToTable(sql);

            // thêm so luong da ban vào bảng 
            int i = 0;
            tbl_QuanLi_BanCham.Columns.Add("SLDB", typeof(System.Int32));
            foreach (DataRow row in tbl_QuanLi_BanCham.Rows)
            {
                row["SLDB"] = tbl_QuanLi_BanCham2.Rows[i].Field<Int32>(0);
                i++;
            }

            // lấy lợi nhuận
            sql = "SP_QL_DOANHTHU_BANCHAM " + thang + ", " + nam;
            tbl_QuanLi_BanCham2 = Functions.GetDataToTable(sql);

            // thêm lợi nhuận vào bảng
            i = 0;
            tbl_QuanLi_BanCham.Columns.Add("DOANHTHU", typeof(System.Decimal));
            foreach (DataRow row in tbl_QuanLi_MHBC.Rows)
            {
                row["DOANHTHU"] = tbl_QuanLi_BanCham2.Rows[i].Field<Decimal>(0);
                i++;
            }
            dgv_BanCham.DataSource = tbl_QuanLi_BanCham;

            // set Font cho tên cột
            dgv_BanCham.Font = new Font("Time New Roman", 13);
            dgv_BanCham.Columns[0].HeaderText = "Mã sản phẩm";
            dgv_BanCham.Columns[1].HeaderText = "Tên sản phẩm";
            dgv_BanCham.Columns[2].HeaderText = "Số lượng tồn";
            dgv_BanCham.Columns[3].HeaderText = "Số lượng đã bán";
            dgv_BanCham.Columns[4].HeaderText = "Lợi nhuận";

            // set Font cho dữ liệu hiển thị trong cột
            dgv_MHBC.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dgv_BanCham.Columns[0].Width = 200;
            dgv_BanCham.Columns[1].Width = 200;
            dgv_BanCham.Columns[2].Width = 200;
            dgv_BanCham.Columns[3].Width = 200;
            dgv_BanCham.Columns[4].Width = 200;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dgv_BanCham.AllowUserToAddRows = false;
            dgv_BanCham.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void dgv_BanCham_Click(object sender, EventArgs e)
        {
            if (tbl_QuanLi_BanCham.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            
            txtBox_MaSP_BanCham.Text = dgv_BanCham.CurrentRow.Cells["MASP"].Value.ToString();
            txtBox_TenSP_BanCham.Text = dgv_BanCham.CurrentRow.Cells["TENSP"].Value.ToString();
            txtBox_SLT_BanCham.Text = dgv_BanCham.CurrentRow.Cells["SOLUONGTON"].Value.ToString();
            txtBox_SLDB_BanCham.Text = dgv_BanCham.CurrentRow.Cells["SLDB"].Value.ToString();
            txtBox_LN_BanCham.Text = dgv_BanCham.CurrentRow.Cells["DOANHTHU"].Value.ToString();
        }

        private void btn_Tim_BanCham_Click(object sender, EventArgs e)
        {
            LoadDataBanCham();
        }

        private void tabPage_mathangbancham_Enter(object sender, EventArgs e)
        {
            // khi vào tab thì load MHBC của tháng hiện tại lên
            thang = Int32.Parse(DateTime.Now.ToString("MM"));
            nam = Int32.Parse(DateTime.Now.ToString("yyyy"));

            cbx_Thang.Text = thang.ToString();
            cbx_Nam.Text = nam.ToString();
            LoadDataBanCham();
        }

        private void LoadDataDT()
        {
            // lấy MASP, TENSP, SLTON vào bảng tbl_QuanLi_MHBC
            thang = Int32.Parse(cbo_Thang.Text.Trim().ToString());
            nam = Int32.Parse(cbo_Nam.Text.Trim().ToString());

            string sql = "SP_QL_DSBH " + thang + ", " + nam;
            tbl_QuanLi_DoanhThu = Functions.GetDataToTable(sql);

            // lấy so luong da ban   
            sql = "SP_QL_DSDH " + thang + ", " + nam;
            tbl_QuanLi_DoanhThu2 = Functions.GetDataToTable(sql);

            // thêm so luong da ban vào bảng 
            int i = 0;
            tbl_QuanLi_DoanhThu.Columns.Add("SLDB", typeof(System.Int32));
            foreach (DataRow row in tbl_QuanLi_DoanhThu.Rows)
            {
                row["SLDB"] = tbl_QuanLi_DoanhThu2.Rows[i].Field<Int32>(0);
                i++;
            }

            // lấy doanh thu
            sql = "SP_QL_DOANHTHU_BANDUOC " + thang + ", " + nam;
            tbl_QuanLi_DoanhThu2 = Functions.GetDataToTable(sql);

            // thêm doanh thu vào bảng
            i = 0;
            tbl_QuanLi_DoanhThu.Columns.Add("DOANHTHU", typeof(System.Decimal));
            foreach (DataRow row in tbl_QuanLi_DoanhThu.Rows)
            {
                row["DOANHTHU"] = tbl_QuanLi_DoanhThu2.Rows[i].Field<Decimal>(0);
                i++;
            }
            dgv_DThu.DataSource = tbl_QuanLi_DoanhThu;

            // lấy lợi nhuận
            sql = "SP_QL_LOINHUAN_BANDUOC " + thang + ", " + nam;
            tbl_QuanLi_DoanhThu2 = Functions.GetDataToTable(sql);

            // thêm lợi nhuận vào bảng
            i = 0;
            tbl_QuanLi_DoanhThu.Columns.Add("LOINHUAN", typeof(System.Decimal));
            foreach (DataRow row in tbl_QuanLi_DoanhThu.Rows)
            {
                row["LOINHUAN"] = tbl_QuanLi_DoanhThu2.Rows[i].Field<Decimal>(0);
                i++;
            }
            dgv_DThu.DataSource = tbl_QuanLi_DoanhThu;

            // set Font cho tên cột
            dgv_DThu.Font = new Font("Time New Roman", 13);
            dgv_DThu.Columns[0].HeaderText = "Mã sản phẩm";
            dgv_DThu.Columns[1].HeaderText = "Tên sản phẩm";
            dgv_DThu.Columns[2].HeaderText = "Số lượng tồn";
            dgv_DThu.Columns[3].HeaderText = "Số lượng đã bán";
            dgv_DThu.Columns[4].HeaderText = "Lợi nhuận";

            // set Font cho dữ liệu hiển thị trong cột
            dgv_DThu.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dgv_DThu.Columns[0].Width = 200;
            dgv_DThu.Columns[1].Width = 200;
            dgv_DThu.Columns[2].Width = 200;
            dgv_DThu.Columns[3].Width = 200;
            dgv_DThu.Columns[4].Width = 200;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dgv_DThu.AllowUserToAddRows = false;
            dgv_DThu.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void btn_Tim_DT_Click(object sender, EventArgs e)
        {
            LoadDataDT();
        }

        private void dgv_DThu_Click(object sender, EventArgs e)
        {
            if (tbl_QuanLi_BanCham.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            

            txtBox_DT_SLDB.Text = dgv_DThu.CurrentRow.Cells["SLDB"].Value.ToString();
            txtBox_DT_DoanhThu.Text = dgv_DThu.CurrentRow.Cells["DOANHTHU"].Value.ToString();
            txtBox_DT_LoiNhuan.Text = dgv_DThu.CurrentRow.Cells["LOINHUAN"].Value.ToString();

        }


    }
}
