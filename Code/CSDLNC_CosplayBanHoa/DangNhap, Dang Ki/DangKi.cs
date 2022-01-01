using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace CSDLNC_CosplayBanHoa
{
    public partial class DangKi : Form
    {
        Thread t;
        public DangKi()
        {
            InitializeComponent();
        }

        public void open_FormDangNhap(object obj)
        {
            Application.Run(new DangNhap());
        }



        private void btn_dangki_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormDangNhap);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormDangNhap);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
    }
}
