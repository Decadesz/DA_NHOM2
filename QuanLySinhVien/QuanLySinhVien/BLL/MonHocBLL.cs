
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.BLL
{
    internal class MonHocBLL
    {
        private List<MonHoc> danhSachMonHoc;
        public MonHocBLL()
        {
            this.danhSachMonHoc = MonHocDAL.DanhSachMonHoc;
        }
        public MonHocBLL(List<MonHoc> danhSachMonHoc)
        {
            this.danhSachMonHoc = danhSachMonHoc;
        }
        internal List<MonHoc> DanhSachMonHoc { get => danhSachMonHoc; }
        public bool KiemTraMaMonHocTonTai(string maMonHoc)
        {
            if(danhSachMonHoc.Any(mh => mh.MaMonHoc.Equals(maMonHoc, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã môn học đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ThemMonHoc(MonHoc monHoc)
        {
            if (KiemTraMaMonHocTonTai(monHoc.MaMonHoc))
            {
                
                return false; 
            }
            danhSachMonHoc.Add(monHoc);
            return true;
        }
        public bool SuaMonHoc(MonHoc monHocCu, MonHoc monHocMoi)
        {
            if (monHocCu == null)
            {
                return false; 
            }
            if (!monHocCu.MaMonHoc.Equals(monHocMoi.MaMonHoc, StringComparison.CurrentCultureIgnoreCase) && KiemTraMaMonHocTonTai(monHocMoi.MaMonHoc))
            {
                return false; 
            }
            monHocCu.MaMonHoc = monHocMoi.MaMonHoc;
            monHocCu.TenMonHoc = monHocMoi.TenMonHoc;
            monHocCu.SoTinChi = monHocMoi.SoTinChi;
            monHocCu.SoTietLyThuyet = monHocMoi.SoTietLyThuyet;
            monHocCu.SoTietThucHanh = monHocMoi.SoTietThucHanh;
            return true;
        }
        public bool XoaMonHoc(MonHoc monHoc)
        {
            if (monHoc == null)
            {
                return false; 
            }
            danhSachMonHoc.Remove(monHoc);
            return true;
        }
        public List<MonHoc> TimKiemMonHoc(string tuKhoa)
        {
            tuKhoa = tuKhoa.ToUpper();
            return danhSachMonHoc.Where(mh =>
                mh.MaMonHoc.ToUpper().Contains(tuKhoa) ||
                mh.TenMonHoc.ToUpper().Contains(tuKhoa)
            ).ToList();
        }
    }
}
