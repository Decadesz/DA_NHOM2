using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    
    internal class Lop
    {
        public const int siSoToiDa = 1000;
        private string maLop;
        private string tenLop;
        private List<SinhVien> danhSachSinhVien;
        private int soLuong;
        public Lop(string maLop, string tenLop)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            danhSachSinhVien = new List<SinhVien>();
        }
        public Lop()
        { 
            maLop= "";
            tenLop = "";
            danhSachSinhVien = new List<SinhVien>();
        }
        public string MaLop { get => maLop; set => maLop = value; }
        public string TenLop { get => tenLop; set => tenLop = value; }
        public int SoLuong { get => danhSachSinhVien.Count; set => soLuong = value; }
        internal List<SinhVien> DanhSachSinhVien { get => danhSachSinhVien; set => danhSachSinhVien = value; }
    }
}

