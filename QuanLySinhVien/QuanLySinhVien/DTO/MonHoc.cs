using QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    internal class MonHoc
    {
        private string maMonHoc;
        private string tenMonHoc;
        private int soTinChi;
        private int soTietLyThuyet;
        private int soTietThucHanh;
        private List<Diem> danhSachDiem;
        public MonHoc(string maMonHoc, string tenMonHoc, int soTinChi,int soTietLyThuyet,int soTietThucHanh,List<Diem>danhSachDiem)
        {
            this.MaMonHoc = maMonHoc;
            this.tenMonHoc = tenMonHoc;
            this.soTinChi = soTinChi;
            this.soTietLyThuyet = soTietLyThuyet;
            this.soTietThucHanh = soTietThucHanh;
            this.danhSachDiem = danhSachDiem;
        }
        public MonHoc()
        {
            MaMonHoc = "";
            tenMonHoc = "";
            soTinChi = 0;
            soTietLyThuyet = 0;
            soTietThucHanh = 0;
            DanhSachDiem = new List<Diem>();
        }
        public string MaMonHoc { get => maMonHoc; set => maMonHoc = value; }
        public string TenMonHoc { get => tenMonHoc; set => tenMonHoc = value; }
        public int SoTinChi { get => soTinChi; set => soTinChi = value; }
        public int SoTietLyThuyet { get => soTietLyThuyet; set => soTietLyThuyet = value; }
        public int SoTietThucHanh { get => soTietThucHanh; set => soTietThucHanh = value; }
        public List<Diem> DanhSachDiem { get => danhSachDiem; set => danhSachDiem = value; }
    }
}

