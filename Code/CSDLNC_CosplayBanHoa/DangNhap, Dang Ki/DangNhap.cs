using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSDLNC_CosplayBanHoa
{
    public partial class DangNhap : Form
    {
        public int user_type = -2;
        string MAACC;
        string LOAIACC;
        string tendangnhap;
        string matkhau;

        Thread t;
        public DangNhap()
        {
            InitializeComponent();
        }

        private void resetvalue_DN()
        {
            txtBox_tendangnhap.Text = "";
            txtBox_matkhau.Text = "";
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            //Mở kết nối
            //Functions.Connect(user_type);
            Functions.Connect(Functions.get_ConnectString(user_type));

            resetvalue_DN();
        }

        private void Run_SP_DangNhap()
        {
            SqlCommand cmd = new SqlCommand("Sp_DangNhap", Functions.Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // set kiểu dữ liệu
            cmd.Parameters.Add("@TENDANGNHAP", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@MATKHAU", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@MAACC", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@LOAIACC", SqlDbType.Int).Direction = ParameterDirection.Output;

            // set giá trị
            cmd.Parameters["@TENDANGNHAP"].Value = tendangnhap;
            cmd.Parameters["@MATKHAU"].Value = matkhau;

            cmd.ExecuteNonQuery();

            MAACC = Convert.ToString(cmd.Parameters["@MAACC"].Value);
            LOAIACC = Convert.ToString(cmd.Parameters["@LOAIACC"].Value);
        }

        private int Run_SP_KTTenDangNhap()
        {
            return 1;
        }

        private int Run_SP_KTMatKhau()
        {
            return 1;
        }

        // xử lí mở form tương ứng từng loại acc      
        public void open_FormMain(object obj)
        {
            switch (user_type)
            {
                case 0:
                    {
                        Application.Run(new FormMain_KH());
                        break;
                    }
                case 1:
                    {
                        Application.Run(new FormMain_NS());
                        break;
                    }
                case 2:
                    {
                        Application.Run(new FormMain_QL());
                        break;
                    }
                case 3:
                    {
                        Application.Run(new FormMain_QT());
                        break;
                    }              
            }
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            tendangnhap = txtBox_tendangnhap.Text.Trim().ToString();
            matkhau = txtBox_matkhau.Text.Trim().ToString();

            // nếu chưa có dữ liệu 
            if (tendangnhap.Length == 0 | matkhau.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra tên đăng nhập           
            if (Run_SP_KTTenDangNhap() == 0)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra mật khẩu ứng với tên đăng nhập
            if (Run_SP_KTMatKhau() == 0)
            {
                MessageBox.Show("Mật khẩu không chính xác !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // chạy SP đăng nhập, lấy MAACC, LOAIACC
            Run_SP_DangNhap();

            // chuyển loại acc sang int
            user_type = Int32.Parse(LOAIACC);

            // nếu acc này bị khóa
            if (user_type == -1)
            {
                MessageBox.Show("Tài khoản này đã bị khóa !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ngắt kết nối vô danh
            Functions.Disconnect();

            // kết nối với database tương ứng với loại acc
            Functions.Connect(Functions.get_ConnectString(user_type));

            // mở giao diện tương ứng từng loại acc                 
            this.Close();
            t = new Thread(open_FormMain);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        public void open_FormDangKi(object obj)
        {
            Application.Run(new DangKi());
        }


        private void btn_dangki_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormDangKi);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void txtBox_tendangnhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtBox_matkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
