using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom_04_QLKS
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }
       

        private void item_DangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất tài khoản không  ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                login a = new login();
                a.ShowDialog();
            }
        }

        private void item_NhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVien a = new NhanVien();
            a.ShowDialog();
            a = null;
            this.Show();
                          
        }

        private void item_Phong_Click(object sender, EventArgs e)
        {
            this.Hide();
            Phong  a = new Phong();
            a.ShowDialog();
            a = null;
            this.Show();
        }

        private void dịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Hide();
            DichVu a = new DichVu();
            a.ShowDialog();
            a = null;
            this.Show();
        }

        private void KháchhangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
           KhachHanng a = new KhachHanng();
            a.ShowDialog();
            a = null;
            this.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            HOADON1 a = new HOADON1();
            a.ShowDialog();
            this.Show();
        }
    }
}
