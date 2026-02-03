
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            if (sinhVien.Lop != null)
            {
                if (sinhVien.Lop.DanhSachSinhVien != null)
                {
                    sinhVien.Lop.DanhSachSinhVien.Remove(sinhVien);
                }
            }
            if (sinhVien.CoVan != null)
            {
                if (sinhVien.CoVan.DanhSachSinhVien != null)
                {
                    sinhVien.CoVan.DanhSachSinhVien.Remove(sinhVien);
                }
            }
            return true;
        }
        public List<SinhVien> TimKiemSinhVien(string tuKhoa,ComboBox cbo)
        {        tuKhoa = tuKhoa.ToUpper();
            if (cbo.SelectedItem.ToString() == "Mã sinh viên")
            {
                return danhSachSinhVien.Where(sv => sv.MaSinhVien.ToUpper().Contains(tuKhoa.ToUpper())).ToList();
            }
            if(cbo.SelectedItem.ToString() == "Tên sinh viên")
            {
                return danhSachSinhVien.Where(sv => sv.HoTen.ToUpper().Contains(tuKhoa.ToUpper())).ToList();
            }
            if(cbo.SelectedItem.ToString() == "Tên lớp")
            {
                return danhSachSinhVien.Where(sv => sv.Lop != null && sv.Lop.TenLop.ToUpper().Contains(tuKhoa.ToUpper())).ToList();
            }
            if(cbo.SelectedItem.ToString() == "Tên cố vấn")
            {
                return danhSachSinhVien.Where(sv => sv.CoVan != null && sv.CoVan.HoTen.ToUpper().Contains(tuKhoa.ToUpper())).ToList();
            }
            if(cbo.SelectedItem.ToString() == "Xếp loại")
            {
                return danhSachSinhVien.Where(sv => sv.XepLoai != null && sv.XepLoai.ToUpper().Contains(tuKhoa.ToUpper())).ToList();
            }
            if(cbo.SelectedItem.ToString()=="Giới tính")
            {
                return danhSachSinhVien.Where(sv => sv.GioiTinh != null && sv.GioiTinh.ToUpper().Contains(tuKhoa.ToUpper())).ToList();
            }
            return danhSachSinhVien.ToList();


        }
    }
}
