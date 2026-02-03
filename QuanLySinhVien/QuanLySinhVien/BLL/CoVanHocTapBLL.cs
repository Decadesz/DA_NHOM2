
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySinhVien.DAL;
namespace QuanLySinhVien.BLL
{
    internal class CoVanHocTapBLL
    {
        private List<CoVanHocTap> danhSachCoVan;
        public CoVanHocTapBLL()
        {
            this.danhSachCoVan = CoVanHocTapDAL.DanhSachCoVan;
        }
        public CoVanHocTapBLL(List<CoVanHocTap> danhSachCoVan)
        {
            this.danhSachCoVan = danhSachCoVan;
        }

        internal List<CoVanHocTap> DanhSachCoVan { get => danhSachCoVan; }
        public bool KiemTraMaCoVanTonTai(string maCoVan)
        {
            if(danhSachCoVan.Any(cv => cv.MaCoVan.Equals(maCoVan, StringComparison.CurrentCultureIgnoreCase)))
            {
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
                return false; 
            }
            danhSachCoVan.Add(coVan);
            return true;
        }
        public bool SuaCoVan(CoVanHocTap coVanCu, CoVanHocTap coVanMoi)
        { 
            if (coVanCu == null)
            {
                return false; 
            }
            if(coVanCu.MaCoVan!=coVanMoi.MaCoVan)
            {
                if (KiemTraMaCoVanTonTai(coVanMoi.MaCoVan))
                {
                    return false;
                }
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
                return false; 
            }
            foreach (SinhVien sv in SinhVienDAL.DanhSachSinhVien)
            {
                if (sv.CoVan== coVan)
                {
                    sv.CoVan = null;
                }
            }
            danhSachCoVan.Remove(coVan);
            return true;
        }
        public List<CoVanHocTap> TimKiemCoVan(string tuKhoa,ComboBox cbo)
        {
            tuKhoa = tuKhoa.ToUpper();
            if(cbo.SelectedItem.ToString()=="Mã cố vấn")
            {
                return danhSachCoVan.Where(cv => cv.MaCoVan.ToUpper().Contains(tuKhoa)).ToList();
            }
            if(cbo.SelectedItem.ToString()=="Tên cố vấn")
            {
                return danhSachCoVan.Where(cv => cv.HoTen.ToUpper().Contains(tuKhoa)).ToList();
            }
            return danhSachCoVan.ToList();
        }
    }
}
