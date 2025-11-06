using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    internal class DiemSinhVien
    {
        private string maSinhVien;
        private string tenSinhVien;
        private string maMonHoc;
        private string tenMonHoc;
        private double diemTrungBinh;
        private int soTinChi;
        private int soTiet;
        private string loai;
        private double diemTrungBinhHocTap;
        private int soTinChiTichLuy;
        private string ketQua;
        private string xepLoai;

        public string MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string TenMonHoc { get => tenMonHoc; set => tenMonHoc = value; }
        public double DiemTrungBinh { get => diemTrungBinh; set => diemTrungBinh = value; }
        public int SoTinChi { get => soTinChi; set => soTinChi = value; }
        public int SoTiet { get => soTiet; set => soTiet = value; }
        public string Loai { get => loai; set => loai = value; }
        public string TenSinhVien { get => tenSinhVien; set => tenSinhVien = value; }
        public string MaMonHoc { get => maMonHoc; set => maMonHoc = value; }
        public double DiemTrungBinhHocTap { get => diemTrungBinhHocTap; set => diemTrungBinhHocTap = value; }
        public int SoTinChiTichLuy { get => soTinChiTichLuy; set => soTinChiTichLuy = value; }
        public string KetQua { get => ketQua; set => ketQua = value; }
        public string XepLoai { get => xepLoai; set => xepLoai = value; }

        public DiemSinhVien(string maSinhVien,string tenSinhVien,string maMonHoc, string tenMonHoc, double diemTrungBinh, int soTinChi, int soTiet, string loai)
        {
            this.MaSinhVien = maSinhVien;
            this.TenSinhVien= tenSinhVien;
            this.MaMonHoc = maMonHoc;
            this.TenMonHoc = tenMonHoc;
            this.DiemTrungBinh = diemTrungBinh;
            this.SoTinChi = soTinChi;
            this.SoTiet = soTiet;
            this.Loai = loai;
        }
        public DiemSinhVien()
        {
            this.MaSinhVien = "";
            this.TenSinhVien = "";
            this.MaMonHoc = "";
            this.TenMonHoc = "";
            this.DiemTrungBinh = 0;
            this.SoTinChi = 0;
            this.SoTiet = 0;
            this.Loai = "";
        }
    }
}
