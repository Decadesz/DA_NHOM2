using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.DTO
{
    internal class ThongKe
    {
        private string xepLoai;
        private int soLuong;

        public string XepLoai { get => xepLoai; set => xepLoai = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public ThongKe()
        {
            this.xepLoai = "";
            this.soLuong = 0;
        }
        public ThongKe(string xepLoai, int soLuong)
        {
            this.xepLoai = xepLoai;
            this.soLuong = soLuong;
        }
    }
}
