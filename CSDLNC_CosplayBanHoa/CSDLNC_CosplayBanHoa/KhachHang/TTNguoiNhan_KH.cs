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
    public partial class TTNguoiNhan_KH : Form
    {
        Thread t;
        public TTNguoiNhan_KH()
        {
            InitializeComponent();
        }

        // xử lí đóng form TT người nhận và mở form thanh toán
        public void open_FormThanhToan(object obj)
        {
            Application.Run(new ThanhToan_KH());
        }

        private void btn_thanhtoan_TTNN_KH_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormThanhToan);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
    }
}
