
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.BLL
{
    internal class DiemBLL
    {
        private List<Diem> danhSachDiem;
        public DiemBLL()
        {
            this.danhSachDiem = DiemDAL.DanhSachDiem;
        }
        public DiemBLL(List<Diem> danhSachDiem)
        {
            this.danhSachDiem = danhSachDiem;
        }
        internal List<Diem> DanhSachDiem { get => danhSachDiem; }
        public bool ThemDiem(Diem diem,SinhVien sinhVien,MonHoc monHoc,HocKy hocKy)
        {
            if(diem == null || sinhVien == null || monHoc == null || hocKy == null)
            {
                return false;
            }
            danhSachDiem.Add(diem);
            sinhVien.DanhSachDiem.Add(diem);
            monHoc.DanhSachDiem.Add(diem);
            hocKy.DanhSachDiem.Add(diem);
            diem.SinhVien = sinhVien;
            diem.MonHoc = monHoc;
            diem.HocKy = hocKy;
            return true;
        }
        public bool XoaDiem(Diem diem)
        {
            if (diem == null)
            {
                return false;
            }
            if(diem.SinhVien == null || diem.MonHoc == null || diem.HocKy == null)
            {
                return false;
            }
            diem.SinhVien.DanhSachDiem.Remove(diem);
            diem.MonHoc.DanhSachDiem.Remove(diem);
            diem.HocKy.DanhSachDiem.Remove(diem);
            danhSachDiem.Remove(diem);
            return true;

        }
        public bool SuaDiem(Diem diemCu,Diem diemMoi)
        {
            if (diemMoi == null)
            {
                return false;
            }
            else
            {
                diemCu.PhanTramLop = diemMoi.PhanTramLop;
                diemCu.PhanTramGiuaKy = diemMoi.PhanTramGiuaKy;
                diemCu.PhanTramCuoiKy = diemMoi.PhanTramCuoiKy;
                diemCu.DiemLop = diemMoi.DiemLop;
                diemCu.DiemGiuaKy = diemMoi.DiemGiuaKy;
                diemCu.DiemCuoiKy = diemMoi.DiemCuoiKy;
                diemCu.SinhVien = diemMoi.SinhVien;
                diemCu.MonHoc = diemMoi.MonHoc;
                diemCu.HocKy = diemMoi.HocKy;
                return true;
            } 
        }
        public List<Diem> TimDiem(string tuKhoa,ComboBox cbo)
        {
            tuKhoa = tuKhoa.ToUpper();
            if(cbo.SelectedItem.ToString()=="Mã sinh viên")
            {
                return danhSachDiem.Where(d => d.SinhVien.MaSinhVien.ToUpper().Contains(tuKhoa)).ToList();  
            }
            if(cbo.SelectedItem.ToString() == "Tên môn học")
            {
                return danhSachDiem.Where(d => d.MonHoc.TenMonHoc.ToUpper().Contains(tuKhoa)).ToList();
            }
            return danhSachDiem.ToList();
        }
    }
}
