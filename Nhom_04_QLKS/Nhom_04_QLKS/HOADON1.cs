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
    public partial class HOADON1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=MSI;Initial Catalog=QL_KHACHSAN8;Integrated Security=True");
        DBConnect db = new DBConnect();
        public HOADON1()
        {
            InitializeComponent();
        }

        public void load_cbbMAPHONG()
        {
            string query = "SELECT * FROM PHONG";
            DataTable ds = db.getTable(query);
            cbbMaPhong.DataSource = ds;
            cbbMaPhong.DisplayMember = "MAPHONG";
            cbbMaPhong.ValueMember = "MAPHONG";
        }

        public void load_cbbMANV()
        {
           
            string query = "SELECT * FROM NHANVIEN";
            DataTable ds = db.getTable(query);
            cbbMANV.DataSource = ds;
            cbbMANV.DisplayMember = "MANV";
            cbbMANV.ValueMember = "MANV";
        }
        public void load_cbbMADV()
        {
            
            string query = "SELECT * FROM DICHVU";
            DataTable ds = db.getTable(query);
            cbbMaDV.DataSource = ds;
            cbbMaDV.DisplayMember = "MADV";
            cbbMaDV.ValueMember = "MADV";

        }

        public void load_cbbMAKH()
        {
            string query = "SELECT * FROM KHACHHANG";
            DataTable ds = db.getTable(query);
            cbbMAKH.DataSource = ds;
            cbbMAKH.DisplayMember = "MAKH";
            cbbMAKH.ValueMember = "MAKH";

        }
        public void load_cbbMaHD_CTHD()
        {
            string query = "SELECT * FROM HOADON";
            DataTable ds = db.getTable(query);
            cbbMaHD.DataSource = ds;
            cbbMaHD.DisplayMember = "MAHD";
            cbbMaHD.ValueMember = "MAHD";
        }

        public void load_DTGV()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT * FROM HOADON";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dtgvHoadon.DataSource = dataTable;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu Hóa đơn: " + ex.Message);
            }
        }

        public void load_CTHD()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT * FROM CTHOADON";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dtgvCTHD.DataSource = dataTable;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu Hóa đơn: " + ex.Message);
            }
        }

        private void HOADON1_Load(object sender, EventArgs e)
        {
            load_DTGV();
            load_cbbMAKH();
            load_cbbMANV();
            load_CTHD();
            load_cbbMaHD_CTHD();
            load_cbbMAPHONG();
            load_cbbMADV();
            
        }

        float TINHTHANTIEN()
        {
            float total = 0;
                    float tongTien = (int.Parse(txtSoLuong.Text) * float.Parse(txtGiaDV.Text));
                    total += tongTien;                 
            return total;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMaHD.Text;
            string makh = cbbMAKH.SelectedValue.ToString();
            string manv = cbbMANV.SelectedValue.ToString();
            string ngay = txtNgay.Text;

            DataRow row = ((DataTable)dtgvHoadon.DataSource).NewRow();
            row["MAHD"] = ma;
            row["MANV"] = manv;
            row["MAKH"] = makh;
            row["NGAY"] = ngay;
            ((DataTable)dtgvHoadon.DataSource).Rows.Add(row);
            string chuoitv = "INSERT INTO HOADON (MAHD,MANV, MAKH, NGAY) values ('" + txtMaHD.Text + "','" + cbbMANV.SelectedValue + "','" + cbbMAKH.SelectedValue + "','" + txtNgay.Text + "')";
            
            int kq = db.getNonQuery(chuoitv);
            if (kq == 1)
            {
                MessageBox.Show("Thêm thành công");
            }
            else
                MessageBox.Show("Thêm thành công");
            
        }

        private void dtgvHoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dtgvHoadon.CurrentRow.Index;
            txtMaHD.Text = dtgvHoadon.Rows[index].Cells[0].Value.ToString();
            cbbMANV.Text = dtgvHoadon.Rows[index].Cells[1].Value.ToString();
            cbbMAKH.Text = dtgvHoadon.Rows[index].Cells[2].Value.ToString();
            txtNgay.Text = dtgvHoadon.Rows[index].Cells[3].Value.ToString();
        }

        private void btnThem1_Click(object sender, EventArgs e)
        {
            DataRow row1 = ((DataTable)dtgvCTHD.DataSource).NewRow();
            string ma1 = cbbMaHD.SelectedValue.ToString();
            string madv = cbbMaDV.SelectedValue.ToString();
            string map = cbbMaPhong.SelectedValue.ToString();
            string sl = txtSoLuong.Text;
            string điav = txtGiaDV.Text;
            txtThanhTien.Text = TINHTHANTIEN().ToString();
            ((DataTable)dtgvCTHD.DataSource).Rows.Add(row1);
            string chuoitv1 = "INSERT INTO CTHOADON (MAHD,MADV, MAPHONG, SOLUONG, GIADV, THANHTIEN) values ('" + cbbMaHD.SelectedValue + "','" + cbbMaDV.SelectedValue + "','" + cbbMaPhong.SelectedValue + "','" + txtSoLuong.Text + "','" + txtGiaDV.Text + "','" + txtThanhTien.Text + "')";
            int kq1 = db.getNonQuery(chuoitv1);
            if (kq1 == 1)
            {
                MessageBox.Show("Thêm thành công");
            }
            else
                MessageBox.Show("Thêm thành công");
            

        }

        private void dtgvCTHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dtgvCTHD.CurrentRow.Index;
            cbbMaHD.Text = dtgvCTHD.Rows[index].Cells[0].Value.ToString();
            cbbMaDV.Text = dtgvCTHD.Rows[index].Cells[1].Value.ToString();
            cbbMaPhong.Text = dtgvCTHD.Rows[index].Cells[2].Value.ToString();
            txtSoLuong.Text = dtgvCTHD.Rows[index].Cells[3].Value.ToString();
            txtGiaDV.Text = dtgvCTHD.Rows[index].Cells[4].Value.ToString();
            txtThanhTien.Text = dtgvCTHD.Rows[index].Cells[5].Value.ToString();
        }

        private void XOA_CT_Click(object sender, EventArgs e)
        {
            if (dtgvCTHD.SelectedRows.Count > 0)
            {
                // Lấy index của dòng được chọn
                int rowIndex = dtgvCTHD.SelectedRows[0].Index;

                // Xóa dòng tương ứng trong DataTable
                ((DataTable)dtgvCTHD.DataSource).Rows.RemoveAt(rowIndex);
            }
            string chuoitv = "DELETE FROM CTHOADON WHERE MAHD = '" + cbbMaHD.SelectedValue + "' AND MADV = '" + cbbMaDV.SelectedValue + "' AND MAPHONG = '" + cbbMaPhong.SelectedValue + "'";
            int kq = db.getNonQuery(chuoitv);
            if (kq < 0)
                MessageBox.Show("Không thể xóa dòng dữ liệu này");
            else
                MessageBox.Show("Bạn đã xóa thành công");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgvCTHD.SelectedRows.Count > 0)
            {
                int rowIndex = dtgvHoadon.SelectedRows[0].Index;
                ((DataTable)dtgvHoadon.DataSource).Rows.RemoveAt(rowIndex);
            }
            string chuoitv = "DELETE FROM HOADON WHERE MAHD = '" + txtMaHD.Text + "' AND MANV = '" + cbbMANV.SelectedValue + "' AND MAKH = '" + cbbMAKH.SelectedValue + "'";
            int kq = db.getNonQuery(chuoitv);
            if (kq < 0)
                MessageBox.Show("Không thể xóa dòng dữ liệu này");
            else
                MessageBox.Show("Bạn đã xóa thành công");
        }


        private void SUA_CT_Click(object sender, EventArgs e)
        {
            if (dtgvCTHD.SelectedRows.Count > 0)
            {
                int rowIndex = dtgvCTHD.SelectedRows[0].Index;
                dtgvCTHD.Rows[rowIndex].Cells["SoLuong"].Value = txtSoLuong.Text;
                dtgvCTHD.Rows[rowIndex].Cells["giadv"].Value = txtSoLuong.Text;

            }
            txtThanhTien.Text = TINHTHANTIEN().ToString();
            string chuoitv = "Update CTHOADON set soluong = '" + txtSoLuong.Text + "',  thanhtien = '"+txtThanhTien.Text+"' where mahd ='" + cbbMaHD.SelectedValue + "'";
            int kq = db.getNonQuery(chuoitv);            
            if (kq < 0)
                MessageBox.Show("Sửa thất bại");
            else
                MessageBox.Show("Bạn đã sửa thành công");
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
