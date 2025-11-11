using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySinhVien.DAL;
namespace QuanLySinhVien
{
    public partial class fDoiMatKhau : Form
    {
        public fDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            if (TaiKhoanDangNhapDAL.TaiKhoanHienTai == null)
            {
                MessageBox.Show("Không có tài khoản nào đang đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string matKhauCu = txtPass.Text;
            string matKhauMoi = txtPassNew.Text;
            string xacNhan = txVerifyPass.Text;

            
            if (string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi) || string.IsNullOrWhiteSpace(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (matKhauMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (TaiKhoanDangNhapDAL.TaiKhoanHienTai.MatKhau != matKhauCu)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            TaiKhoanDangNhapDAL.TaiKhoanHienTai.MatKhau = matKhauMoi;


            TaiKhoanDangNhapDAL.SaveTaiKhoan();

            MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
