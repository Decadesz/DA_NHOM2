using QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuanLySinhVien.DAL
{
    internal class HocKyDAL
    {
        public static List<HocKy> DanhSachHocKy { get; private set; }=new List<HocKy>();
        public static void LoadHocKy()
        {
            DanhSachHocKy.Clear();
            if (!File.Exists("danhSachHocKy.txt"))
                return;
            foreach (string line in File.ReadAllLines("danhSachHocKy.txt"))
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    HocKy hocKy = new HocKy(
                        parts[0],
                        parts[1],
                        int.Parse(parts[2]),
                        new List<Diem>()
                        );
                    DanhSachHocKy.Add(hocKy);
                }
            }
        }
        public static void SaveHocKy()
        {
            using (StreamWriter writer = new StreamWriter("danhSachHocKy.txt"))
            {
                foreach (HocKy hocKy in DanhSachHocKy)
                {
                    writer.WriteLine($"{hocKy.MaHocKy},{hocKy.TenHocKy},{hocKy.NamHoc}");
                }
            }
        }
    }
}
