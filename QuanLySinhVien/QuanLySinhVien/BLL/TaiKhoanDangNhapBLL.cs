
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.BLL
{
    internal class TaiKhoanDangNhapBLL
    {
        private List<TaiKhoanDangNhap> danhSachTaiKhoan;
        public TaiKhoanDangNhapBLL()
        {
            this.danhSachTaiKhoan = TaiKhoanDangNhapDAL.DanhSachTaiKhoan;
        }
        public TaiKhoanDangNhapBLL(List<TaiKhoanDangNhap> taiKhoanDangNhap)
        {
            this.danhSachTaiKhoan = taiKhoanDangNhap;
        }
        internal List<TaiKhoanDangNhap> DanhSachTaiKhoan { get => danhSachTaiKhoan;}
        public bool KiemTraTenDangNhapTonTai(string tenDangNhap)
        {
            if(danhSachTaiKhoan.Any(tk => tk.TenDangNhap.Equals(tenDangNhap, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ThemTaiKhoan(TaiKhoanDangNhap taiKhoan)
        {
            if (KiemTraTenDangNhapTonTai(taiKhoan.TenDangNhap))
            {
                return false; 
            }
            danhSachTaiKhoan.Add(taiKhoan);
            return true;
        }
        public bool XoaTaiKhoan(TaiKhoanDangNhap taiKhoan)
        {
            if (taiKhoan == null)
            {
                return false; 
            }
            danhSachTaiKhoan.Remove(taiKhoan);
            return true;
        }
        public bool SuaTaiKhoan(TaiKhoanDangNhap taiKhoanCu, TaiKhoanDangNhap taiKhoanMoi)
        {
            if (taiKhoanCu == null)
            {
                return false; 
            }
            if(taiKhoanCu.TenDangNhap!=taiKhoanMoi.TenDangNhap)
            {
                if (KiemTraTenDangNhapTonTai(taiKhoanMoi.TenDangNhap))
                {
                    return false;
                }
            }
            taiKhoanCu.TenDangNhap = taiKhoanMoi.TenDangNhap;
            taiKhoanCu.MatKhau = taiKhoanMoi.MatKhau;
            taiKhoanCu.LoaiTaiKhoanDangNhap = taiKhoanMoi.LoaiTaiKhoanDangNhap;
            return true;
        }
        public List<TaiKhoanDangNhap> TimTaiKhoan(string tuKhoa)
        {
            tuKhoa = tuKhoa.ToUpper();
            return danhSachTaiKhoan.Where(mh =>
                mh.TenDangNhap.ToUpper().Contains(tuKhoa)
            ).ToList();
        }
    }
}
