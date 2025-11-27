using QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    internal class CoVanHocTap
    {
        string maCoVan;
        string hoTen;
        DateTime ngaySinh;
        string gioiTinh;
        string diaChi;
        private string soDienThoai;
        private string email;
        private List<SinhVien> danhSachSinhVien;
        public CoVanHocTap()
        {
            MaCoVan = "";
            HoTen = "";
            NgaySinh = DateTime.Now;
            GioiTinh = "";
            DiaChi = "";
            Email = "";
            danhSachSinhVien = new List<SinhVien>();
        }
        public CoVanHocTap(string maCoVan, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi,string soDienThoai,string email,List<SinhVien> danhSachSinhVien)
        {
            this.MaCoVan = maCoVan;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
            this.Email = email;
            this.danhSachSinhVien = danhSachSinhVien;
        }
        public string MaCoVan { get => maCoVan; set => maCoVan = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string Email { get => email; set => email = value; }
        internal List<SinhVien> DanhSachSinhVien { get => danhSachSinhVien; set => danhSachSinhVien = value; }
    }
}