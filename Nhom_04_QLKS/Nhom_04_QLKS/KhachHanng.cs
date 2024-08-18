using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom_04_QLKS
{
    public partial class KhachHanng : Form
    {
        DBConnect db = new DBConnect();
        public KhachHanng()
        {
            InitializeComponent();
        }

      
        void load_GridViewKH()
        {
            string chuoitruyvan = "select * from KhachHang ";
            DataTable dt = db.getTable(chuoitruyvan);

            dataGridView1.DataSource = dt;
        }
        bool ktTrungMKH(string ma)
        {
            DBConnect connect = new DBConnect();

            string chuoiTV = "select count(*) from KhachHang where MaKH = '" + ma + "'";

            int kq = (int)connect.getScalar(chuoiTV);
            if (kq != 0)
                return false;
            return true;
        }




     
        private string GetValueFromCell(DataGridViewRow row, int cellIndex)
        {
            if (row.Cells[cellIndex].Value != null && row.Cells[cellIndex].Value != DBNull.Value)
            {
                string value = row.Cells[cellIndex].Value.ToString().Trim(); // Loại bỏ khoảng trắng đầu cuối
                if (!string.IsNullOrWhiteSpace(value)) // Kiểm tra giá trị không phải là khoảng trắng hoặc giá trị không mong muốn khác
                {
                    return value;
                }
            }
            return string.Empty;
        }

      


      

     

        private void KhachHanng_Load(object sender, EventArgs e)
        {
           
                load_GridViewKH();
            
        }

        private void btnTrangMenu_Click(object sender, EventArgs e)
        {
           
                this.Hide();
                menu a = new menu();
                a.ShowDialog();
                a = null;
                this.Show();
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
                string maKhachHang = txtMaKh.Text.Trim(); // Lấy mã khách hàng và loại bỏ khoảng trắng ở đầu và cuối

                if (!maKhachHang.StartsWith("KH"))
                {
                    MessageBox.Show("Mã khách hàng phải bắt đầu bằng 'KH'.", "Thông báo");
                    return;
                }

                if (ktTrungMKH(maKhachHang))
                {
                    DBConnect connect = new DBConnect();
                    string chuoiTV = "Insert into KhachHang values ('" + maKhachHang + "', N'" + txtTenKH.Text + "','" + txtCCCD.Text + "','" + txtGioiTinh.Text + "','" + txtSDT.Text + "','" + txtMaPH.Text + "')";

                    int kq = connect.getNonQuery(chuoiTV);

                    if (kq == 1)
                        MessageBox.Show("Đã thêm", "Thông báo");
                    else
                        MessageBox.Show("Chưa thêm", "Thông báo");
                    load_GridViewKH();
                }
                else
                    MessageBox.Show("Trùng mã khách hàng!!!", "Thông báo");

        }

      

        private void btnXoa_Click(object sender, EventArgs e)
        {
                if (ktTrungMKH(txtMaKh.Text) == false)
                {
                    DBConnect connect = new DBConnect();
                    string chuoiTV = "delete KhachHang where MaKH = '" + txtMaKh.Text + "'";

                    int kq = connect.getNonQuery(chuoiTV);

                    if (kq == 1)
                        MessageBox.Show("Đã Xóa", "Thông báo");
                    else
                        MessageBox.Show("Chưa Xóa", "Thông báo");
                    load_GridViewKH();
                }
                else
                    MessageBox.Show("Không tồn tại!!!", "Thông báo");      
        }

        private void btnRS_Click(object sender, EventArgs e)
        {
       
                load_GridViewKH();
                txtMaKh.Text = "";
                txtTenKH.Text = "";
                txtSDT.Text = "";
                txtGioiTinh.Text = "";
                txtMaPH.Text = "";
                txtTimKiem.Text = "";

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
         
                string chuoiTim = "select * from KhachHang where MaKH = '" + txtTimKiem.Text + "' ";
                DataTable dt = db.getTable(chuoiTim);

                dataGridView1.DataSource = dt;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    txtMaKh.Text = GetValueFromCell(row, 0);
                    txtTenKH.Text = GetValueFromCell(row, 1);
                    txtSDT.Text = GetValueFromCell(row,4);
                    txtCCCD.Text = GetValueFromCell(row, 2);
                    txtGioiTinh.Text = GetValueFromCell(row, 3);
                    txtMaPH.Text = GetValueFromCell(row, 5);
                
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
          
                if (ktTrungMKH(txtMaKh.Text) == false)
                {
                DBConnect connect = new DBConnect();
                string chuoiTV = "update KhachHang set TenKH = N'" + txtTenKH.Text + "', CCCD = '" + txtCCCD.Text + "',GIOTINH ='" + txtGioiTinh.Text + "', SDT = '" + txtSDT.Text + "', MAPHONG = '" + txtMaPH.Text + "' where MaKH = '" + txtMaKh.Text + "'";
                int kq = connect.getNonQuery(chuoiTV);

                if (kq == 1)
                    MessageBox.Show("Đã Sửa", "Thông báo");
                else
                    MessageBox.Show("Chưa Sửa", "Thông báo");
                load_GridViewKH();
            }
                else
                    MessageBox.Show("Không tồn tại!!!", "Thông báo");
           
        }








        //private void btnTrangMenu_Click(object sender, EventArgs e)
        //{

        //}


    }
}
