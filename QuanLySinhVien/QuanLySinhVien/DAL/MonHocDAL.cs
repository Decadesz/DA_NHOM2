using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.DAL
{
    internal class MonHocDAL
    {
        public static List<MonHoc> DanhSachMonHoc { get; private set; } = new List<MonHoc>();
        public static void LoadMonHoc()
        {
            DanhSachMonHoc.Clear();
            if (!File.Exists("danhSachMonHoc.txt"))
                return;
            foreach (var line in File.ReadAllLines("danhSachMonHoc.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length >= 5)
                {
                    MonHoc monHoc = new MonHoc(
                        parts[0],
                        parts[1],
                        int.Parse(parts[2]),
                        int.Parse(parts[3]),
                        int.Parse(parts[4]),
                        new List<Diem>()
                        );
                    DanhSachMonHoc.Add(monHoc);
                }
            }
        }
        public static void SaveMonHoc()
        {
            using (StreamWriter writer = new StreamWriter("danhSachMonHoc.txt"))
            {
                foreach (var mh in DanhSachMonHoc)
                {
                    writer.WriteLine($"{mh.MaMonHoc},{mh.TenMonHoc},{mh.SoTinChi},{mh.SoTietLyThuyet},{mh.SoTietThucHanh}");
                }
            }
        }
    }
}
