
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.BLL
{
    internal class SinhVienBLL
    {
        private List<SinhVien> danhSachSinhVien;
        public SinhVienBLL()
        {
            this.danhSachSinhVien = SinhVienDAL.DanhSachSinhVien;
        }
        public SinhVienBLL(List<SinhVien> danhSachSinhVien)
        {
            this.danhSachSinhVien = danhSachSinhVien;
        }
        internal List<SinhVien> DanhSachSinhVien { get => danhSachSinhVien; }
        public bool KiemTraMaSinhVienTonTai(string maSinhVien)
        {
            if( danhSachSinhVien.Any(sv => sv.MaSinhVien.Equals(maSinhVien, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ThemSinhVien(SinhVien sinhVien)
        {
            if (KiemTraMaSinhVienTonTai(sinhVien.MaSinhVien))
            {
                
                return false;
            }
            danhSachSinhVien.Add(sinhVien);
            return true;
        }
        public bool SuaSinhVien(SinhVien sinhVienCu, SinhVien sinhVienMoi)
        {
            if (sinhVienCu == null)
            {
                return false;
            }
           if(sinhVienCu.MaSinhVien != sinhVienMoi.MaSinhVien)
            {
                if (KiemTraMaSinhVienTonTai(sinhVienMoi.MaSinhVien))
                {
                    return false;
                }
            }
            sinhVienCu.MaSinhVien = sinhVienMoi.MaSinhVien;
            sinhVienCu.HoTen = sinhVienMoi.HoTen;
            sinhVienCu.NgaySinh = sinhVienMoi.NgaySinh;
            sinhVienCu.GioiTinh = sinhVienMoi.GioiTinh;
            sinhVienCu.DiaChi = sinhVienMoi.DiaChi;
            sinhVienCu.SoDienThoai = sinhVienMoi.SoDienThoai;
            sinhVienCu.NgayNhapHoc = sinhVienMoi.NgayNhapHoc;
            sinhVienCu.Lop = sinhVienMoi.Lop;
            sinhVienCu.CoVan = sinhVienMoi.CoVan;
            return true;
        }
        public bool XoaSinhVien(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                return false;
            }
            danhSachSinhVien.Remove(sinhVien);
            return true;
        }
        public List<SinhVien> TimKiemSinhVien(string tuKhoa)
        {
            tuKhoa = tuKhoa.ToUpper();
            return danhSachSinhVien.Where(sv =>
                sv.MaSinhVien.ToUpper().Contains(tuKhoa) ||
                sv.HoTen.ToUpper().Contains(tuKhoa) ||
                sv.GioiTinh.ToUpper().Contains(tuKhoa) ||
                sv.TenLop.ToUpper().Contains(tuKhoa) ||
                sv.TenCoVan.ToUpper().Contains(tuKhoa)
            ).ToList();
        }
    }
}
