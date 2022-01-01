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
        public ThongKe_QL()
        {
            InitializeComponent();
        }
        private void LoadData_CN()//dữ liệu vào DataGridView
        {
            int thang = Int32.Parse(cbB_Thang.Text.Trim().ToString());
            int nam = Int32.Parse(cbB_Nam.Text.Trim().ToString());

            string sql = "SP_QL_DSBC " + thang + ", " + nam ;
            tbl_QuanLi_MHBC = Functions.GetDataToTable(sql);
            dgv_MHBC.DataSource = tbl_QuanLi_MHBC;

            // set Font cho tên cột
            dgv_MHBC.Font = new Font("Time New Roman", 13);
            dgv_MHBC.Columns[0].HeaderText = "Mã sản phẩm";
            dgv_MHBC.Columns[1].HeaderText = "Tên sản phẩm";
            dgv_MHBC.Columns[2].HeaderText = "Số lượng tồn";

            // set Font cho dữ liệu hiển thị trong cột
            dgv_MHBC.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dgv_MHBC.Columns[0].Width = 100;
            dgv_MHBC.Columns[1].Width = 350;
            dgv_MHBC.Columns[2].Width = 100;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dgv_MHBC.AllowUserToAddRows = false;
            dgv_MHBC.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void LoadData_SL()//dữ liệu vào DataGridView
        {
            int thang = Int32.Parse(cbB_Thang.Text.Trim().ToString());
            int nam = Int32.Parse(cbB_Nam.Text.Trim().ToString());

            string sql = "SP_QL_DSBC_NUM " + thang + ", " + nam;
            tbl_QuanLi_MHBC = Functions.GetDataToTable(sql);
            dgv_MHBC.DataSource = tbl_QuanLi_MHBC;

            // set Font cho tên cột
            dgv_MHBC.Font = new Font("Time New Roman", 13);

            dgv_MHBC.Columns[3].HeaderText = "Số lượng đã bán";
            dgv_MHBC.Columns[3].HeaderText = "Doanh thu";

            // set Font cho dữ liệu hiển thị trong cột
            dgv_MHBC.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột

            dgv_MHBC.Columns[4].Width = 150;
            dgv_MHBC.Columns[3].Width = 150;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dgv_MHBC.AllowUserToAddRows = false;
            dgv_MHBC.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void ThongKe_QL_Load(object sender, EventArgs e)
        {
           
        }

        private void btn_Tim_BanChay_Click(object sender, EventArgs e)
        {
            LoadData_CN();
            LoadData_SL();
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
        }
    }
}
