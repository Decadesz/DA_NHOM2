using QuanLySinhVien.DAL;
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
    public partial class fThongTinChiTiet : Form
    {
        public fThongTinChiTiet()
        {
            InitializeComponent();
            txtUserName.Text = TaiKhoanDangNhapDAL.TaiKhoanHienTai.TenDangNhap;
            txtLoaiTaiKhoan.Text = TaiKhoanDangNhapDAL.TaiKhoanHienTai.LoaiTaiKhoanDangNhap;
        }
    }
}
