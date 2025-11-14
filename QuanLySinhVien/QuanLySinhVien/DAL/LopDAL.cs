using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.DAL
{
    internal class LopDAL
    {
        public static List<Lop> DanhSachLop { get; private set; } = new List<Lop>();
        public static void LoadLop()
        {
            DanhSachLop.Clear();
            if (!File.Exists("danhSachLop.txt"))
                return;
            foreach (string line in File.ReadAllLines("danhSachLop.txt"))
            {
                string [] parts = line.Split(',');
                //phai >=3 vi can dung de luu so Luong
                if (parts.Length >= 3)
                {
                    Lop lop = new Lop(
                        parts[0],
                        parts[1]
                        );
                    DanhSachLop.Add(lop);
                }
            }
        }
        public static void SaveLop()
        {
            using (StreamWriter writer = new StreamWriter("danhSachLop.txt"))
            {
                foreach (Lop lop in DanhSachLop)
                {
                    writer.WriteLine($"{lop.MaLop},{lop.TenLop},{lop.SoLuong}");
                }
            }
        }
    }
}
