using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    internal class HocKy
    {
        private string maHocKy;
        private string tenHocKy;
        private int namHoc;
        private List<Diem> danhSachDiem;
        public HocKy()
        {
            maHocKy = "";
            tenHocKy = "";
            namHoc = 0;
            this.danhSachDiem = new List<Diem>();
        }
        public HocKy(string maHocKy, string tenHocKy, int namHoc,List<Diem> danhSachDiem)
        {
            this.maHocKy = maHocKy;
            this.tenHocKy = tenHocKy;
            this.namHoc = namHoc;
            this.danhSachDiem = danhSachDiem;
        }

        public string MaHocKy { get => maHocKy; set => maHocKy = value; }
        public string TenHocKy { get => tenHocKy; set => tenHocKy = value; }
        public int NamHoc { get => namHoc; set => namHoc = value; }
        internal List<Diem> DanhSachDiem { get => danhSachDiem; set => danhSachDiem = value; }
    }
}
