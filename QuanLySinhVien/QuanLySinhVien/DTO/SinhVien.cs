
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLySinhVien.TienIchHayHo;
namespace QuanLySinhVien
{
    internal class SinhVien
    {
        public double soTinChiToiThieu =TienIch.TinhSoTinChiToiThieu();
        private string maSinhVien;
        private string hoTen;
        private string gioiTinh;
        private DateTime ngaySinh;
        private DateTime ngayNhapHoc;
        private string diaChi;
        private string soDienThoai;
        private double diemTrungBinhHocTap;
        private int soTinChiTichLuy;
        private string xepLoai;

        private Lop lop;
        private CoVanHocTap coVan;
        private List<Diem> danhSachDiem;
        public string TenLop
        {
            get
            {
                if (Lop != null)
                {
                    if (Lop.TenLop != null)
                        return Lop.TenLop;
                    else
                        return "";
                }
                else
                {
                    return "";
                }
            }
        }
        public string TenCoVan
        {
            get
            {
                if (CoVan != null)
                {
                    if (CoVan.HoTen != null)
                        return CoVan.HoTen;
                    else
                        return "";
                }
                else
                {
                    return "";
                }
            }
        }
        public SinhVien()
        {
            MaSinhVien = "";
            HoTen = "";
            GioiTinh = "";
            NgaySinh = DateTime.Now;
            NgayNhapHoc = DateTime.Now;
            DiaChi = "";
            SoDienThoai = "";
            CoVan = null;
            Lop = null;
            DanhSachDiem = new List<Diem>();
        }
        public SinhVien(string maSinhVien, string hoTen, string gioiTinh, DateTime ngaySinh, DateTime ngayNhapHoc,string diaChi, string soDienThoai,Lop lop,CoVanHocTap coVan,List<Diem>danhSachDiem)
        {
            this.MaSinhVien = maSinhVien;
            this.HoTen = hoTen;
            this.GioiTinh = gioiTinh;
            this.NgaySinh = ngaySinh;
            this.NgayNhapHoc = ngayNhapHoc;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
            this.Lop = lop;
            this.CoVan = coVan;
            this.DanhSachDiem = danhSachDiem;
           

        }

        public string MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public DateTime NgayNhapHoc { get => ngayNhapHoc; set => ngayNhapHoc = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }

        internal Lop Lop { get => lop; set => lop = value; }
        internal CoVanHocTap CoVan { get => coVan; set => coVan = value; }
        public List<Diem> DanhSachDiem { get => danhSachDiem; set => danhSachDiem = value; }
        public double DiemTrungBinhHocTap { get => TinhDiemTrungBinhHocTap(); }
        public int SoTinChiTichLuy { get => TinhSoTinChiTichLuy(); }
        public string XepLoai { get => TinhXepLoai(); }

        public bool KiemTraSoTinChiTotNghiep()
        {
            if (SoTinChiTichLuy >= soTinChiToiThieu)
                return true;
            else
                return false;
        }
        public string TinhXepLoai()
        {
            if(KiemTraSoTinChiTotNghiep() == false)
            {
                return "Chưa đủ tín chỉ để xét loại";
            }
            double dtb = DiemTrungBinhHocTap;
            if (dtb >= 9)
                return "Xuất sắc";
            else if (dtb >= 8)
                return "Giỏi";
            else if (dtb >= 7)
                return "Khá";
            else if (dtb >= 5)
                return "Trung bình";
            else
                return "Yếu";
        }
        public int TinhSoTinChiTichLuy()
        {
            
                if (DanhSachDiem == null || DanhSachDiem.Count == 0)
                { 
                    return 0;
                }
                return DanhSachDiem
                    .Where(d => d.DiemTrungBinh >= 5 && d.MonHoc != null && d.DiemThangChu != "F")
                    .Sum(d => d.MonHoc.SoTinChi);
        }


        public double TinhDiemTrungBinhHocTap()
        {
                int tongTin = SoTinChiTichLuy;
                if (tongTin == 0) return 0;
                double diemTrungBinh= DanhSachDiem
                    .Where(d => d.DiemTrungBinh >= 5)
                    .Sum(d => d.DiemTongMonHoc) / tongTin;
                return Math.Round(diemTrungBinh,2);
        }
    }
}

