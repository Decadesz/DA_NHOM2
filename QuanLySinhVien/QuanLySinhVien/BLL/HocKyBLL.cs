using QuanLySinhVien;
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.BLL
{
    internal class HocKyBLL
    {
        private List<HocKy> danhSachHocKy;
        public HocKyBLL(List<HocKy> danhSachHocKy)
        {
            this.danhSachHocKy = danhSachHocKy;
        }
        public HocKyBLL()
        {
            this.danhSachHocKy = HocKyDAL.DanhSachHocKy;
        }
        internal List<HocKy> DanhSachHocKy { get => danhSachHocKy; }
        public bool KiemTraMaTonTai(string maHocKy)
        {
            if (danhSachHocKy.Any(mh => mh.MaHocKy.Equals(maHocKy, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã môn học đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ThemHocKy(HocKy hocKy)
        {
            if (KiemTraMaTonTai(hocKy.MaHocKy))
            {
                return false;
            }
            danhSachHocKy.Add(hocKy);
            return true;
        }
        public bool SuaHocKy(HocKy hocKyCu, HocKy hocKyMoi)
        {
            if (hocKyCu == null)
            {
                return false;
            }
            if (hocKyCu.MaHocKy != hocKyMoi.MaHocKy)
            {
                if (KiemTraMaTonTai(hocKyMoi.MaHocKy))
                {
                    return false;
                }
            }
            hocKyCu.MaHocKy = hocKyMoi.MaHocKy;
            hocKyCu.TenHocKy = hocKyMoi.TenHocKy;
            hocKyCu.NamHoc = hocKyMoi.NamHoc;
            return true;
        }
        public bool XoaHocKy(HocKy hocKy)
        {
            if (hocKy == null)
            {
                return false;
            }
            danhSachHocKy.Remove(hocKy);
            return true;
        }
        public  List<HocKy> TimKiemHocKy(string tuKhoa)
        {
            tuKhoa = tuKhoa.ToUpper();
            return danhSachHocKy.Where(hk =>
                hk.MaHocKy.ToUpper().Contains(tuKhoa) ||
                hk.TenHocKy.ToUpper().Contains(tuKhoa)||hk.NamHoc.ToString().Contains(tuKhoa)
            ).ToList();
        }
    }
}
