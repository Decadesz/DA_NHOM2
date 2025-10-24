using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    internal class TaiKhoanDangNhap
    {
        private string tenDangNhap;
        private string matKhau;
        private string loaiTaiKhoanDangNhap;

        public TaiKhoanDangNhap(string tenDangNhap, string matKhau, string loaiTaiKhoanDangNhap)
        {
            this.TenDangNhap = tenDangNhap;
            this.MatKhau = matKhau;
            this.LoaiTaiKhoanDangNhap = loaiTaiKhoanDangNhap;
            
        }
        public TaiKhoanDangNhap()
        {
            this.TenDangNhap = "";
            this.MatKhau = "";
            this.LoaiTaiKhoanDangNhap = "";
        }

        public string TenDangNhap { get => tenDangNhap; set => tenDangNhap = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        public string LoaiTaiKhoanDangNhap { get => loaiTaiKhoanDangNhap; set => loaiTaiKhoanDangNhap = value; }
    }
}
