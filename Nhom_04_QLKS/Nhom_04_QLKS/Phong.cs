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
    public partial class Phong : Form
    {
        DBConnect db = new DBConnect();
        public Phong()
        {
            InitializeComponent();
        }
        void load_GridViewKH()
        {
            string chuoitruyvan = "select * from Phong ";
            DataTable dt = db.getTable(chuoitruyvan);

            dataGridView1.DataSource = dt;
        }
        bool ktTrungMP(string ma)
        {
            DBConnect connect = new DBConnect();

            string chuoiTV = "select count(*) from Phong where MaPhong = '" + ma + "'";

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

        private void Phong_Load(object sender, EventArgs e)
        {
            load_GridViewP();

        }
        void load_GridViewP()
        {
            string chuoitruyvan = "select * from Phong ";
            DataTable dt = db.getTable(chuoitruyvan);

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    txtMaPhong.Text = GetValueFromCell(row, 0);
                    txtGiaPhong.Text = GetValueFromCell(row, 2);
                    txtLoaiPhong.Text = GetValueFromCell(row, 1);
                    txtTinhTrang.Text = GetValueFromCell(row, 3);


                }
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            if (ktTrungMP(txtMaPhong.Text))
            {
                DBConnect connect = new DBConnect();
                string chuoiTV = "Insert into Phong values ('" + txtMaPhong.Text + "' ,'" + txtLoaiPhong.Text + "','" + txtGiaPhong.Text + "','" + txtTinhTrang.Text + "')";

                int kq = connect.getNonQuery(chuoiTV);

                if (kq == 1)
                    MessageBox.Show("Đã thêm", "Thông báo");
                else
                    MessageBox.Show("Chưa thêm", "Thông báo");
                load_GridViewP();
            }
            else
                MessageBox.Show("Trùng mã phòng!!!", "Thông báo");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (ktTrungMP(txtMaPhong.Text) == false)
            {
                DBConnect connect = new DBConnect();
                string chuoiTV = "delete Phong where MaPhong = '" + txtMaPhong.Text + "'";

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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ktTrungMP(txtMaPhong.Text) == false)
            {
                DBConnect connect = new DBConnect();
                string chuoiTV = "update Phong set LoaiPhong = N'" + txtLoaiPhong.Text + "',GiaPhong = '" + txtGiaPhong.Text + "',TinhTrang =N'" + txtTinhTrang.Text + "' where MaPhong = '" + txtMaPhong.Text + "'";
                int kq = connect.getNonQuery(chuoiTV);

                if (kq == 1)
                    MessageBox.Show("Đã Sửa", "Thông báo");
                else
                    MessageBox.Show("Chưa Sửa", "Thông báo");
                load_GridViewP();
            }
            else
                MessageBox.Show("Không tồn tại!!!", "Thông báo");
        }

        private void btnRS_Click(object sender, EventArgs e)
        {
            load_GridViewP();
            txtMaPhong.Text = "";
            txtGiaPhong.Text = "";
            txtLoaiPhong.Text = "";
            txtTinhTrang.Text = "";
           
        }

        private void btnTrangMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu a = new menu();
            a.ShowDialog();
            a = null;
            this.Show();
        }
    }
}
