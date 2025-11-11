using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.TienIchHayHo
{
    internal class TienIch
    {
        public static int TinhTongSoTinChi()
        {
            return MonHocDAL.DanhSachMonHoc.Sum(mh => mh.SoTinChi);
        }
        public static double TinhSoTinChiToiThieu()
        {
            return TinhTongSoTinChi() * 0.9;
        }
    }
}
