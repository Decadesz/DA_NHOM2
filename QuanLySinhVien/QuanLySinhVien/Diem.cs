using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    internal class Diem
    {
        private int phanTramLop;
        private int phanTramGiuaKy;
        private int phanTramCuoiKy;
        private double diemLop;
        private double diemGiuaKy;
        private double diemCuoiKy;
        private double diemTB;
        private string loai;
        private SinhVien sinhVien;
        private MonHoc monHoc;
        public string MaSinhVien
        {
            get
            {
                if (SinhVien != null)
                {
                    if (SinhVien.MaSinhVien != null)
                        return SinhVien.MaSinhVien;
                    else
                        return "";
                }
                else
                {
                    return "";
                }
            }
        }
        public string TenMonHoc
        {
            get
            {
                if (MonHoc != null)
                {
                    if (MonHoc.TenMonHoc != null)
                        return MonHoc.TenMonHoc;
                    else
                        return "";
                }
                else
                {
                    return "";
                }
            }
        }
        public double DiemTB
        {
            get
            {

                diemTB = (diemLop * phanTramLop / 100) + (diemGiuaKy * phanTramGiuaKy / 100) + (diemCuoiKy * phanTramCuoiKy / 100);
                return diemTB;
            }
        }
        public Diem()
        {
            PhanTramLop = 0;
            PhanTramGiuaKy = 0;
            PhanTramCuoiKy = 0;
            DiemLop = 0;
            DiemGiuaKy = 0;
            DiemCuoiKy = 0;
        }
        public Diem(int phanTramLop, int phanTramGiuaKy, int phanTramCuoiKy, double diemLop, double diemGiuaKy, double diemCuoiKy)
        {
            this.PhanTramLop = phanTramLop;
            this.PhanTramGiuaKy = phanTramGiuaKy;
            this.PhanTramCuoiKy = phanTramCuoiKy;
            this.DiemLop=diemLop;
            this.DiemGiuaKy=diemGiuaKy;
            this.DiemCuoiKy=diemCuoiKy;
        }
        public string Loai
        {
            get
            {
                if(DiemTB >= 9)
                {
                    loai = "A+";
                }
                else if (DiemTB >= 8)
                {
                    loai = "A";
                }
                else if (DiemTB >= 7)
                {
                    loai = "B+";
                }
                else if(DiemTB>=6)
                {
                    loai = "B";
                }
                else if(DiemTB>=5)
                {
                    loai= "C+";
                }
                else
                {
                    loai = "F";
                }
                return loai;
            }
        }
        public int PhanTramLop { get => phanTramLop; set => phanTramLop = value; }
        public int PhanTramGiuaKy { get => phanTramGiuaKy; set => phanTramGiuaKy = value; }
        public int PhanTramCuoiKy { get => phanTramCuoiKy; set => phanTramCuoiKy = value; }
        public double DiemLop { get => diemLop; set => diemLop = value; }
        public double DiemGiuaKy { get => diemGiuaKy; set => diemGiuaKy = value; }
        public double DiemCuoiKy { get => diemCuoiKy; set => diemCuoiKy = value; }
        internal SinhVien SinhVien { get => sinhVien; set => sinhVien = value; }
        internal MonHoc MonHoc { get => monHoc; set => monHoc = value; }
    }
}
