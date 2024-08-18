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
    public partial class DichVu : Form
    {
        public DichVu()
        {
            InitializeComponent();
        }
        DBConnect db = new DBConnect();



        //void load_gird()
        //{
        //    DBConnect db = new DBConnect();
        //    string chuoitruyvan = "Select * from NHANVIEN";
        //    DataTable dt = db.getTable(chuoitruyvan);
        //    dgvDV.DataSource = dt;
        //}

        void load_GridViewDV()
        {
            string chuoitruyvan = "select * from DichVu ";
            DataTable dt = db.getTable(chuoitruyvan);

            dataGridView1.DataSource = dt;
        }

        bool ktTrungMDV(string ma)
        {
            DBConnect connect = new DBConnect();

            string chuoiTV = "select count(*) from DichVu where MaDV = '" + ma + "'";

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

     

     

  

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtMaDV.Text = GetValueFromCell(row, 0);
                txtGiaDV.Text = GetValueFromCell(row, 1);
                txtTenDV.Text = GetValueFromCell(row, 2);

            }


        }

     

      

        private void btnThem_Click(object sender, EventArgs e)
        {
          
                if (ktTrungMDV(txtMaDV.Text))
                {
                    DBConnect connect = new DBConnect();
                    string chuoiTV = "Insert into DichVu values ('" + txtMaDV.Text + "', N'" + txtTenDV.Text + "','" + txtGiaDV.Text + "')";

                    int kq = connect.getNonQuery(chuoiTV);

                    if (kq == 1)
                        MessageBox.Show("Đã thêm", "Thông báo");
                    else
                        MessageBox.Show("Chưa thêm", "Thông báo");
                    load_GridViewDV();
                }
                else
                    MessageBox.Show("Trùng mã Dịch Vụ!!!", "Thông báo");

            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
          
                if (ktTrungMDV(txtMaDV.Text) == false)
                {
                    DBConnect connect = new DBConnect();
                    string chuoiTV = "delete DichVu where MaDV = '" + txtMaDV.Text + "'";

                    int kq = connect.getNonQuery(chuoiTV);

                    if (kq == 1)
                        MessageBox.Show("Đã Xóa", "Thông báo");
                    else
                        MessageBox.Show("Chưa Xóa", "Thông báo");
                    load_GridViewDV();
                }
                else
                    MessageBox.Show("Không có sản phẩm có mã dịch vụ này!!!", "Thông báo");
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
          
                if (ktTrungMDV(txtMaDV.Text) == false)
                {
                    DBConnect connect = new DBConnect();
                    string chuoiTV = "update DichVu set GiaDV = '" + txtGiaDV.Text + "',TenDV = N'" + txtTenDV.Text + "'where MaDV = '" + txtMaDV.Text + "'";

                    int kq = connect.getNonQuery(chuoiTV);

                    if (kq == 1)
                        MessageBox.Show("Đã Sửa", "Thông báo");
                    else
                        MessageBox.Show("Chưa Sửa", "Thông báo");
                    load_GridViewDV();
                }
                else
                    MessageBox.Show("Không có sản phẩm có mã dịch vụ này!!!", "Thông báo");
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
           
                string chuoiTim = "select * from DichVu where MaDV = '" + txtTimKiem.Text + "' ";
                DataTable dt = db.getTable(chuoiTim);

                dataGridView1.DataSource = dt;
            
        }

        private void btnRS_Click(object sender, EventArgs e)
        {
           
                load_GridViewDV();
                txtMaDV.Text = "";
                txtGiaDV.Text = "";
                txtTenDV.Text = "";
                txtTimKiem.Text = "";
            
        }

        private void DichVu_Load(object sender, EventArgs e)
        {
          
                load_GridViewDV();
           
        }

        private void btnTrangMenu_Click(object sender, EventArgs e)
        {
          
                this.Hide();
                menu a = new menu();
                a.ShowDialog();
                a = null;
                this.Show();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    txtMaDV.Text = GetValueFromCell(row, 0);
                    txtGiaDV.Text = GetValueFromCell(row, 2);
                    txtTenDV.Text = GetValueFromCell(row, 1);

                }
            
        }
    }
}

