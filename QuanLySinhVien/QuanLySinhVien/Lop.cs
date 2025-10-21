using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    
    internal class Lop
    {
        public const int siSoToiDa = 100;
        private string maLop;
        private string tenLop;
        private List<SinhVien> danhSachSinhVien;
        public int soLuong;
        public Lop(string maLop, string tenLop, int soLuong)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.soLuong = soLuong;
        }
        public Lop()
        { 
            maLop= "";
            tenLop = "";
            soLuong = 0;
        }
        public string MaLop { get => maLop; set => maLop = value; }
        public string TenLop { get => tenLop; set => tenLop = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        internal List<SinhVien> DanhSachSinhVien { get => danhSachSinhVien; set => danhSachSinhVien = value; }
    }
}
