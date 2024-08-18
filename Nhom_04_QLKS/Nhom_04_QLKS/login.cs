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
    public partial class login : Form
    {
        DBConnect db = new DBConnect(); 
        public login()
        {
            InitializeComponent();
        }
        public class GetDataUser
        {
            static public string tennhanvien;
            static public string phanquyen;

        }
        private void chboHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chboHienMK.Checked)
            {
                txt_MK.PasswordChar = '\0';
            }
            else
            {
                txt_MK.PasswordChar = '*';
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenTK = txt_TK.Text;
            string matKhau = txt_MK.Text;
           
            if (txt_TK.Text == "" || txt_MK.Text == "")
            {
                MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không được để trống !");


            }
            else
            {
                string sql = $"SELECT COUNT(*) FROM TAIKHOAN WHERE TENTK = '{tenTK}' AND MATKHAU = '{matKhau}'";

                int count = Convert.ToInt32(db.getScalar(sql));

                if (count > 0)
                {
                    // Đăng nhập thành công, chuyển sang form MenuForm

                    menu menuForm = new menu();
                    string sql1 = $"SELECT PHANQUYEN FROM TAIKHOAN WHERE TENTK = '{tenTK}' AND MATKHAU = '{matKhau}'";
                    object kq = db.getScalar(sql1);
                    if (kq != null)
                    {
                        GetDataUser.phanquyen = kq.ToString();
                    }
                    string sql2 = $"SELECT TENNV FROM NHANVIEN NV JOIN TAIKHOAN TK ON NV.TENTK = TK.TENTK WHERE NV.TENTK = '{tenTK}' AND TK.MATKHAU = '{matKhau}'";
                    object kq1 = db.getScalar(sql2);
                    if (kq1 != null)
                    {
                        GetDataUser.tennhanvien = kq1.ToString();
                    }


                    menuForm.Show();
                    this.Hide(); // Ẩn form đăng nhập

                    //MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
                }

            }
        }

        
    }
}
