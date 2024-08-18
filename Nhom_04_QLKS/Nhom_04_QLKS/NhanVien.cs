using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Nhom_04_QLKS
{
    public partial class NhanVien : Form
    {
       DBConnect db = new DBConnect();
        public NhanVien()
        {
            InitializeComponent();
        }
        void load_gird()
        {
           DBConnect db = new DBConnect();
            string chuoitruyvan = "Select * from NHANVIEN";
            DataTable dt = db.getTable(chuoitruyvan);
            dgvNV.DataSource = dt;
        }
        //---------BTN_TRANG_CHỦ------
        private void btnTrangMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu a = new menu();
            a.ShowDialog();
            a = null;
            this.Show();
        }

        //---------FROM LOAD----------
        private void NhanVien_Load(object sender, EventArgs e)
        {
            load_gird();
        }

        //--------Kiểm tra trùng---------
        bool ktTrungMNV(string ma)
        {

            string chuoiTV = "select count(*) from NhanVien where MaNV = '" + ma + "'";

            int kq = (int)db.getScalar(chuoiTV);
            if (kq != 0)
                return false;
            return true;
        }
        bool ktTrungTK(string ma)
        {
            DBConnect db = new DBConnect();
            string chuoiTV = "select count(*) from TAIKHOAN where TenTK = '" + ma + "'";

            int kq = (int)db.getScalar(chuoiTV);
            if (kq != 0)
                return false;
            return true;
        }
        //-----------------//


        private void btnThem_Click(object sender, EventArgs e)
        {


            if (ktTrungTK(txtTK.Text))
            {


                string chuoiTV1 = "Insert into TaiKhoan values ('" + txtTK.Text + "','" + txtMauKhau.Text + "',N'Nhân Viên')";
                db.getNonQuery(chuoiTV1);
                if (ktTrungMNV(txtMaNV.Text))
                {

                    string chuoiTV = "Insert into NhanVien values ('" + txtMaNV.Text + "', N'" + txtTenNV.Text + "','" + txtSDT.Text + "','" + txtCCCD.Text + "','"+txtTuoi.Text+"','" + txtTK.Text + "')";

                    int kq = db.getNonQuery(chuoiTV);

                    if (kq == 1)
                        MessageBox.Show("Đã thêm", "Thông báo");
                    else
                        MessageBox.Show("Chưa thêm", "Thông báo");
                    load_gird();
                }
                else
                {
                    string chuoiTV3 = "delete TaiKhoan where TenTK = '" + txtTK.Text + "'";
                    db.getNonQuery(chuoiTV3);
                    MessageBox.Show("Trùng mã nhân viên!!!", "Thông báo");
                }
            }
            else
                MessageBox.Show("Tên tài khoản này đã tồn tại!!!", "Thông báo");

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (ktTrungMNV(txtMaNV.Text) == false)
            {
                DBConnect connect = new DBConnect();
                string chuoiTV = "delete NhanVien where MaNV = '" + txtMaNV.Text + "'";
                int kq = connect.getNonQuery(chuoiTV);
                string chuoiTV1 = "delete TaiKhoan where TenTK = '" + txtTK.Text + "'";
                int kq1 = connect.getNonQuery(chuoiTV1);
                if (kq == 1)
                    MessageBox.Show("Đã Xóa", "Thông báo");
                else
                    MessageBox.Show("Chưa Xóa", "Thông báo");
                load_gird() ;
            }
            else
                MessageBox.Show("Không có nhân viên có nhân viên này!!!", "Thông báo");
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNV.Rows.Count)
            {
                DataGridViewRow row = dgvNV.Rows[e.RowIndex];
                txtMaNV.Text = GetValueFromCell(row, 0);
                txtTenNV.Text = GetValueFromCell(row, 1);
                txtSDT.Text = GetValueFromCell(row, 2);
                txtCCCD.Text = GetValueFromCell(row, 3);
                txtTuoi.Text = GetValueFromCell(row, 4);
                txtTK.Text = GetValueFromCell(row, 5);

            }
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ktTrungMNV(txtMaNV.Text))
            {
                string sql = "select Taikhoan.TenTK from taikhoan join NhanVien on NhanVien.TenTK = TaiKhoan.TenTK where MaNV = '" + txtMaNV.Text + "'";
                object tentk = db.getScalar(sql);
                string chuoiTV2 = "delete NhanVien where MaNV = '" + txtMaNV.Text + "'";
                db.getNonQuery(chuoiTV2);
                string chuoixoa = "delete TaiKhoan where TenTK = '" + tentk + "'";
                int xong = db.getNonQuery(chuoixoa);
                if (xong == 1)
                {
                    if (ktTrungTK(txtTK.Text))
                    {

                        // Cập nhật thông tin tài khoản
                        string chuoiTK = "Insert into TaiKhoan values ('" + txtTK.Text + "','" + txtMauKhau.Text + "',N'Nhân Viên')";
                        int kqTK = db.getNonQuery(chuoiTK);
                        if (kqTK == 1)
                        {
                            if (ktTrungMNV(txtMaNV.Text))
                            {
                                // Cập nhật thông tin nhân viên sau khi cập nhật tài khoản thành công
                                string chuoiNV = "Insert into NhanVien values ('" + txtMaNV.Text + "', N'" + txtTenNV.Text + "','" + txtSDT.Text + "','" + txtCCCD.Text + "','" + txtTuoi.Text + "','" + txtTK.Text + "')";
                                int kqNV = db.getNonQuery(chuoiNV);

                                if (kqNV == 1)
                                {
                                    MessageBox.Show("Đã Sửa", "Thông báo");
                                    load_gird(); // Tải lại dữ liệu lên grid sau khi cập nhật thành công
                                }
                                else
                                {
                                    MessageBox.Show("Chưa Sửa thông tin nhân viên", "Thông báo");
                                }
                            }
                            else
                                MessageBox.Show("Trùng mã nhân viên!!!", "Thông báo");

                        }
                        else
                        {
                            MessageBox.Show("Chưa Sửa thông tin tài khoản", "Thông báo");
                        }
                    }
                    else
                        MessageBox.Show("Tên tài khoản này đã tồn tại!!!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Chưa Update TaiKhoan!!!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Không có nhân viên có mã này!!!", "Thông báo");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string chuoiTim = "select * from NhanVien where MaNV = '" + txtTimKiem.Text + "' ";
            DataTable dt = db.getTable(chuoiTim);

            dgvNV.DataSource = dt;
        }
    }
}
