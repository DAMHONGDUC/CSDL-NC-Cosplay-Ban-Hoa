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
    public partial class LichSuMuaHang_KH : Form
    {
        public LichSuMuaHang_KH()
        {
            InitializeComponent();
        }

        private void btn_xemchitiet_LSMH_KH_Click(object sender, EventArgs e)
        {
            CT_DonHang ct_DonHang = new CT_DonHang();
            ct_DonHang.StartPosition = FormStartPosition.CenterScreen;
            ct_DonHang.Show();
        }
    }
}
