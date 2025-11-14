using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.DAL
{
    internal class CoVanHocTapDAL
    {
        public static List<CoVanHocTap> DanhSachCoVan { get; private set; } = new List<CoVanHocTap>();
        public static void LoadCoVan()
        {
            DanhSachCoVan.Clear();
            if (!File.Exists("danhSachCoVan.txt"))
                return;
            foreach (string line in File.ReadAllLines("danhSachCoVan.txt"))
            {
                string [] parts = line.Split(',');
                if (parts.Length >= 7)
                {
                    
                    DateTime ngay = DateTime.ParseExact(parts[2].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    CoVanHocTap coVan = new CoVanHocTap(
                        parts[0],
                        parts[1],
                        ngay,
                        parts[3],
                        parts[4],
                        parts[5],
                        parts[6],
                        new List<SinhVien>()
                        );
                    DanhSachCoVan.Add(coVan);
                }
            }
        }
        public static void SaveCoVan()
        {
            using (StreamWriter writer = new StreamWriter("danhSachCoVan.txt"))
            {
                foreach (CoVanHocTap cv in DanhSachCoVan)
                {
                    writer.WriteLine($"{cv.MaCoVan},{cv.HoTen},{cv.NgaySinh.ToString("dd/MM/yyyy")},{cv.GioiTinh},{cv.DiaChi},{cv.SoDienThoai},{cv.Email}");
                }
            }
        }
    }
}
