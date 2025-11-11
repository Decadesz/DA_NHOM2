using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QuanLySinhVien.DAL
{
    internal class SinhVienDAL
    {
        public static List<SinhVien> DanhSachSinhVien { get; private set; } = new List<SinhVien>();
        public static void LoadSinhVien()
        {
            DanhSachSinhVien.Clear();
            if (!File.Exists("danhSachSinhVien.txt"))
                return;
            foreach (var line in File.ReadAllLines("danhSachSinhVien.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length >= 9)
                {
                    //InvariantCulture để không cần quan tâm về định dạng ngày tháng của hệ thống trên máy khác
                    DateTime ngaySinh = DateTime.ParseExact(parts[3].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ngayNhapHoc = DateTime.ParseExact(parts[4].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Lop lop = LopDAL.DanhSachLop.FirstOrDefault(l => l.MaLop == parts[7]);
                    if (lop == null)
                    {
                        lop = new Lop(parts[7], "khong ro");
                    };
                    CoVanHocTap coVan = CoVanHocTapDAL.DanhSachCoVan.FirstOrDefault(c => c.MaCoVan == parts[8]);
                    if (coVan == null)
                    {
                        coVan = new CoVanHocTap(parts[8], "khong ro", ngaySinh, "khong ro", "khong ro", "khong ro", "khong ro",null);
                    };
                    SinhVien sinhVien = new SinhVien(
                        parts[0],
                        parts[1],
                        parts[2],
                        ngaySinh,
                        ngayNhapHoc,
                        parts[5],
                        parts[6],
                        lop,
                        coVan,
                        new List<Diem>()
                        );
                    //Thêm sinh viên vào danh sách sinh viên chung
                    DanhSachSinhVien.Add(sinhVien);
                    //Thêm sinh viên vào danh sách của lớp và cố vấn học tập
                    lop.DanhSachSinhVien.Add(sinhVien);
                    if (coVan.HoTen != "khong ro")
                    {
                        coVan.DanhSachSinhVien.Add(sinhVien);
                    }
                }
            }
        }
        public static void SaveSinhVien()
        {
            using (StreamWriter writer = new StreamWriter("danhSachSinhVien.txt"))
            {
                foreach (var sv in DanhSachSinhVien)
                {
                    writer.WriteLine($"{sv.MaSinhVien},{sv.HoTen},{sv.GioiTinh},{sv.NgaySinh.ToString("dd/MM/yyyy")},{sv.NgayNhapHoc.ToString("dd/MM/yyyy")},{sv.DiaChi},{sv.SoDienThoai},{sv.Lop?.MaLop},{sv.CoVan?.MaCoVan}");
                }
            }
        }
    }
}
