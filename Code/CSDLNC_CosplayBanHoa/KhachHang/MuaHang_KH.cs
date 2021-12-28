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

        private void MuaHang_KH_Load(object sender, EventArgs e)
        {
          
            handle_menu();
        }

        private void btn_muangay_MH_KH_Click(object sender, EventArgs e)
        {
            DonHang_KH donHang_KH = new DonHang_KH();
            donHang_KH.StartPosition = FormStartPosition.CenterScreen;
            donHang_KH.Show();
        }

        private void handle_menu()
        {
            Load_MenuToolStripMenuItems(hoaSinhNhatToolStripMenuItem, Get_Item_For_Menu1());
            Load_MenuToolStripMenuItems(cheĐeToolStripMenuItem, Get_Item_For_Menu2());
            Load_MenuToolStripMenuItems(hoaTuoiToolStripMenuItem, Get_Item_For_Menu3());
            Load_MenuToolStripMenuItems(mauSacToolStripMenuItem, Get_Item_For_Menu4());
            Load_MenuToolStripMenuItems(hoaĐacBietToolStripMenuItem, Get_Item_For_Menu5());
            Load_MenuToolStripMenuItems(hoaCuoiToolStripMenuItem, Get_Item_For_Menu6());
        }

        private void Load_MenuToolStripMenuItems(ToolStripMenuItem tool_strip_menu_item, List<String> list_items)
        {
            int id = 0;

            foreach(String items in list_items)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(items);
                id++;
                tool_strip_menu_item.DropDownItems.Add(item);
                item.Click += new EventHandler(Item_Click);
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
    
            txtBox_timkiem_MH_KH.Text = item.Text;
            btn_timkiem_MH_KH.PerformClick();
        }

        private List<String> Get_Item_For_Menu1()
        {
            List<String> menu_item = new List<String>();

            menu_item.Add("Hoa sinh nhật Ba Mẹ");
            menu_item.Add("Hoa sinh nhật người yêu");
            menu_item.Add("Hoa sinh nhật bạn bè");

            return menu_item;
        }

        private List<String> Get_Item_For_Menu2()
        {
            List<String> menu_item = new List<String>();

            menu_item.Add("Hoa sinh nhật");
            menu_item.Add("Hoa tặng mẹ");
            menu_item.Add("Hoa tình yêu");
            menu_item.Add("Hoa mừng tốt nghiệp");

            return menu_item;
        }

        private List<String> Get_Item_For_Menu3()
        {
            List<String> menu_item = new List<String>();

            menu_item.Add("Hoa Cúc");
            menu_item.Add("Cẩm Chướng");
            menu_item.Add("Hoa Tulip");
            menu_item.Add("Hoa Ly");
            menu_item.Add("Hoa Hồng");
            menu_item.Add("Lan Hồ Điệp");

            return menu_item;
        }

        private List<String> Get_Item_For_Menu4()
        {
            List<String> menu_item = new List<String>();

            menu_item.Add("Hoa sinh nhật Ba Mẹ");
            menu_item.Add("Hoa sinh nhật người yêu");
            menu_item.Add("Hoa sinh nhật bạn bè");

            return menu_item;
        }

        private List<String> Get_Item_For_Menu5()
        {
            List<String> menu_item = new List<String>();

            menu_item.Add("Hoa sinh nhật Ba Mẹ");
            menu_item.Add("Hoa sinh nhật người yêu");
            menu_item.Add("Hoa sinh nhật bạn bè");

            return menu_item;
        }

        private List<String> Get_Item_For_Menu6()
        {
            List<String> menu_item = new List<String>();

            menu_item.Add("Hoa sinh nhật Ba Mẹ");
            menu_item.Add("Hoa sinh nhật người yêu");
            menu_item.Add("Hoa sinh nhật bạn bè");

            return menu_item;
        }



        private void btn_timkiem_MH_KH_Click(object sender, EventArgs e)
        {
            if (txtBox_timkiem_MH_KH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập từ khóa vào ô tìm kiếm !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
