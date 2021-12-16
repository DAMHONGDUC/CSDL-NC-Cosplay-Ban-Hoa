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
        public DonHang_KH()
        {
            InitializeComponent();
        }

        // xử lí đóng form đơn hàng và mở form TT người nhận
        public void open_FormTTNguoiNhan(object obj)
        {
            Application.Run(new TTNguoiNhan_KH());
        }
        private void btn_tieptuc_DH_KH_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormTTNguoiNhan);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
    }
}
