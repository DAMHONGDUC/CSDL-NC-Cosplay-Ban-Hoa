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
    public partial class SanPham_QT : Form
    {
        DataTable tbl_QT_SP;
        public SanPham_QT()
        {
            InitializeComponent();
        }

        private void LoadData_TatCaSP() // tải dữ liệu vào DataGridView
        {
            string sql = "SP_QT_TatCaSP";
            tbl_QT_SP = Functions.GetDataToTable(sql);
            dGV_QT_SP.DataSource = tbl_QT_SP;

            // set Font cho tên cột
            dGV_QT_SP.Font = new Font("Time New Roman", 13);
            dGV_QT_SP.Columns[0].HeaderText = "Mã sản phẩm";
            dGV_QT_SP.Columns[1].HeaderText = "Tên sản phẩm";
            dGV_QT_SP.Columns[2].HeaderText = "Chi nhánh";
            dGV_QT_SP.Columns[3].HeaderText = "Thành phần chính";
            dGV_QT_SP.Columns[4].HeaderText = "Mô tả";
            dGV_QT_SP.Columns[5].HeaderText = "Số lượng tồn";
            dGV_QT_SP.Columns[6].HeaderText = "Giá gốc";
            dGV_QT_SP.Columns[7].HeaderText = "Chi tiết sản phẩm";
            dGV_QT_SP.Columns[8].HeaderText = "Khuyễn mãi";
            dGV_QT_SP.Columns[9].HeaderText = "Hình ảnh";

            // set Font cho dữ liệu hiển thị trong cột
            dGV_QT_SP.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_QT_SP.Columns[0].Width = 200;
            dGV_QT_SP.Columns[1].Width = 200;
            dGV_QT_SP.Columns[2].Width = 200;
            dGV_QT_SP.Columns[3].Width = 200;
            dGV_QT_SP.Columns[4].Width = 200;
            dGV_QT_SP.Columns[5].Width = 200;
            dGV_QT_SP.Columns[6].Width = 200;
            dGV_QT_SP.Columns[7].Width = 200;
            dGV_QT_SP.Columns[8].Width = 200;
            dGV_QT_SP.Columns[9].Width = 200;


            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_QT_SP.AllowUserToAddRows = false;

            dGV_QT_SP.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void SanPham_QT_Load(object sender, EventArgs e)
        {
            LoadData_TatCaSP();
        }

        private void dGV_QT_SP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tbl_QT_SP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            // set giá trị cho các mục 
            textBox_QT_SP_MASP.Text = dGV_QT_SP.CurrentRow.Cells["MASP"].Value.ToString();
            txtBox_QT_SP_TenSP.Text = dGV_QT_SP.CurrentRow.Cells["TENSP"].Value.ToString();
            textBox_QT_SP_TenCN.Text = dGV_QT_SP.CurrentRow.Cells["TENCN"].Value.ToString();
            textBox_QT_SP_TPC.Text = dGV_QT_SP.CurrentRow.Cells["THANHPHANCHINH"].Value.ToString();
            textBox_QT_SP_MoTa.Text = dGV_QT_SP.CurrentRow.Cells["MOTA"].Value.ToString();
            txtBox_QT_SP_SL.Text = dGV_QT_SP.CurrentRow.Cells["SOLUONGTON"].Value.ToString();
            txtBox_QT_SP_GiaGoc.Text = dGV_QT_SP.CurrentRow.Cells["GIAGOC"].Value.ToString();
            textBox_QT_SP_CTSP.Text = dGV_QT_SP.CurrentRow.Cells["CHITIETSP"].Value.ToString();
            textBox_QT_SP_KM.Text = dGV_QT_SP.CurrentRow.Cells["KHUYENMAI"].Value.ToString();
            textBox_QT_SP_TenCN.Text = dGV_QT_SP.CurrentRow.Cells["HINHANH"].Value.ToString();
        }

    }
}
