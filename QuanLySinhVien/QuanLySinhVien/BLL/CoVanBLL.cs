
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySinhVien.DAL;
namespace QuanLySinhVien.BLL
{
    internal class CoVanBLL
    {
        private List<CoVanHocTap> danhSachCoVan;
        public CoVanBLL()
        {
            this.danhSachCoVan = CoVanHocTapDAL.DanhSachCoVan;
        }
        public CoVanBLL(List<CoVanHocTap> danhSachCoVan)
        {
            this.danhSachCoVan = danhSachCoVan;
        }

        internal List<CoVanHocTap> DanhSachCoVan { get => danhSachCoVan; }
        public bool KiemTraMaCoVanTonTai(string maCoVan)
        {
            if(danhSachCoVan.Any(cv => cv.MaCoVan.Equals(maCoVan, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã cố vấn đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }
       public bool ThemCoVan(CoVanHocTap coVan)
        {
            if (KiemTraMaCoVanTonTai(coVan.MaCoVan))
            { 
                return false; // Mã cố vấn đã tồn tại
            }
            danhSachCoVan.Add(coVan);
            return true;
        }
        public bool SuaCoVan(CoVanHocTap coVanCu, CoVanHocTap coVanMoi)
        { 
            if (coVanCu == null)
            {
                return false; // Cố vấn không tồn tại
            }
            if (!coVanCu.MaCoVan.Equals(coVanMoi.MaCoVan, StringComparison.CurrentCultureIgnoreCase) && KiemTraMaCoVanTonTai(coVanMoi.MaCoVan))
            {
                return false; // Mã cố vấn mới đã tồn tại
            }
            coVanCu.MaCoVan = coVanMoi.MaCoVan;
            coVanCu.HoTen = coVanMoi.HoTen;
            coVanCu.NgaySinh = coVanMoi.NgaySinh;
            coVanCu.GioiTinh = coVanMoi.GioiTinh;
            coVanCu.DiaChi = coVanMoi.DiaChi;
            coVanCu.SoDienThoai = coVanMoi.SoDienThoai;
            coVanCu.Email = coVanMoi.Email;
            return true;
        }
        public bool XoaCoVan(CoVanHocTap coVan)
        {
            if (coVan == null)
            {
                return false; // Cố vấn không tồn tại
            }
            danhSachCoVan.Remove(coVan);
            return true;
        }
        public List<CoVanHocTap> TimKiemCoVan(string tuKhoa)
        {
            tuKhoa = tuKhoa.ToUpper();
            return danhSachCoVan.Where(cv =>
                cv.MaCoVan.ToUpper().Contains(tuKhoa)
                || cv.HoTen.ToUpper().Contains(tuKhoa)
            ).ToList();
        }
    }
}
