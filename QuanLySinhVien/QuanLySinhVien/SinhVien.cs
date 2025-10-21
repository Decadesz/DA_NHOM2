using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    internal class SinhVien
    {
        private string maSinhVien;
        private string hoTen;
        private string gioiTinh;
        private DateTime ngaySinh;
        private DateTime ngayNhapHoc;
        private string diaChi;
        private string soDienThoai;

        
        private Lop lop;
        private CoVanHocTap coVan;
        private List<Diem> danhSachDiem;

        public SinhVien()
        {
            MaSinhVien = "";
            HoTen = "";
            GioiTinh = "";
            NgaySinh = DateTime.Now;
            NgayNhapHoc = DateTime.Now;
            DiaChi = "";
            SoDienThoai = "";
           
        }
        public SinhVien(string maSinhVien, string hoTen, string gioiTinh, DateTime ngaySinh, DateTime ngayNhapHoc,string diaChi, string soDienThoai)
        {
            this.MaSinhVien = maSinhVien;
            this.HoTen = hoTen;
            this.GioiTinh = gioiTinh;
            this.NgaySinh = ngaySinh;
            this.NgayNhapHoc = ngayNhapHoc;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
        }

        // Properties
        public string MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public DateTime NgayNhapHoc { get => ngayNhapHoc; set => ngayNhapHoc = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }

        internal Lop Lop { get => lop; set => lop = value; }
        internal CoVanHocTap CoVan { get => coVan; set => coVan = value; }
        internal List<Diem> DsDiem { get => danhSachDiem; set => danhSachDiem = value; }

       
        public double DiemTrungBinhHocTap
        {
            get
            {
                if (DsDiem.Count == 0)
                    return 0;
                else
                    return DsDiem.Average(d => d.DiemTB);
            }
        }
    }
}
