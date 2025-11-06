using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtUserName.Text;
            string matKhau = txtPass.Text;
            //Nếu không có file hoặc danh sách tài khoản file đọc rỗng thì tự tạo 1 tài khoản mặc định
            if (KhoDuLieu.DanhSachTaiKhoan.Count==0)
            {
                TaiKhoanDangNhap taiKhoanReset=new TaiKhoanDangNhap();
                taiKhoanReset.TenDangNhap = "1";
                taiKhoanReset.MatKhau = "1";
                taiKhoanReset.LoaiTaiKhoanDangNhap = "Phong Dao Tao";
                KhoDuLieu.DanhSachTaiKhoan.Add(taiKhoanReset);
                KhoDuLieu.SaveTaiKhoan();
                KhoDuLieu.LoadTaiKhoan();
            }
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(string .IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            TaiKhoanDangNhap taiKhoan = KhoDuLieu.DanhSachTaiKhoan.FirstOrDefault(t =>t.TenDangNhap.Equals(tenDangNhap, StringComparison.OrdinalIgnoreCase) &&t.MatKhau == matKhau);
            if (taiKhoan != null)
            {
                KhoDuLieu.TaiKhoanHienTai = taiKhoan;
                MessageBox.Show($"Đăng nhập thành công! Xin chào {taiKhoan.TenDangNhap} ({taiKhoan.LoaiTaiKhoanDangNhap})","Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    fGiaoDienNgoai f = new fGiaoDienNgoai();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    


        private void fDangNhap_Load(object sender, EventArgs e)
        {
            KhoDuLieu.LoadTaiKhoan();
        }
    }
}
