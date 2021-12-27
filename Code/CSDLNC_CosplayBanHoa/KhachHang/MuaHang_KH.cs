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
    public partial class MuaHang_KH : Form
    {
        public MuaHang_KH()
        {
            InitializeComponent();
        }

        private void btn_muangay_MH_KH_Click(object sender, EventArgs e)
        {
            DonHang_KH donHang_KH = new DonHang_KH();
            donHang_KH.StartPosition = FormStartPosition.CenterScreen;
            donHang_KH.Show();
        }
    }
}
