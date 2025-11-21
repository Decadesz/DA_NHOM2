
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.BLL
{
    internal class LopBLL
    {
        private List<Lop> danhSachLop;
        public LopBLL()
        {
            this.danhSachLop = LopDAL.DanhSachLop;
        }
        public LopBLL(List<Lop> danhSachLop)
        {
            this.danhSachLop = danhSachLop;
        }
        internal List<Lop> DanhSachLop { get => danhSachLop; }
        public bool KiemTraLopTonTai(string maLop)
        {
            if(danhSachLop.Any(l => l.MaLop.Equals(maLop, StringComparison.CurrentCultureIgnoreCase)))
            { 
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ThemLop(Lop lop)
        {
            if(KiemTraLopTonTai(lop.MaLop))
            {
                return false;
            }
            danhSachLop.Add(lop);
            return true;
        }
        public bool XoaLop(Lop lop)
        {
            if (lop == null)
            {
                return false;
            }
            danhSachLop.Remove(lop);
            return true;
        }
        public bool SuaLop(Lop lopCu, Lop lopMoi)
        {
            if (lopCu == null)
            {
                return false;
            }
            if(lopCu.MaLop!=lopMoi.MaLop)
            {
                if (KiemTraLopTonTai(lopMoi.MaLop))
                {
                    return false;
                }
            }
            lopCu.MaLop = lopMoi.MaLop;
            lopCu.TenLop = lopMoi.TenLop;
            return true;
            
        }
        public List<Lop> TimLop(string tuKhoa)
        {
            tuKhoa = tuKhoa.ToUpper();
            return danhSachLop.Where(l =>
            l.MaLop.ToUpper().Contains(tuKhoa) ||
            l.TenLop.ToUpper().Contains(tuKhoa)
            ).ToList();
        }
    }
}
