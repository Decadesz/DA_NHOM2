using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.DAL
{
    internal class DiemDAL
    {
        public static List<Diem> DanhSachDiem { get; private set; } = new List<Diem>();
        public static void LoadDiem()
        {
            DanhSachDiem.Clear();
            if (!File.Exists("danhSachDiem.txt"))
                return;
            foreach (var line in File.ReadAllLines("danhSachDiem.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length >= 8)
                {
                    SinhVien sinhVien = SinhVienDAL.DanhSachSinhVien.FirstOrDefault(s => s.MaSinhVien == parts[0]);
                    MonHoc monHoc = MonHocDAL.DanhSachMonHoc.FirstOrDefault(m => m.MaMonHoc == parts[1]);
                    if (sinhVien != null && monHoc != null)
                    {
                        Diem diem = new Diem(
                            int.Parse(parts[2]),
                            int.Parse(parts[3]),
                            int.Parse(parts[4]),
                            double.Parse(parts[5]),
                            double.Parse(parts[6]),
                            double.Parse(parts[7]),
                            sinhVien,
                            monHoc
                            )
                        {
                            SinhVien = sinhVien,
                            MonHoc = monHoc
                        };
                        DanhSachDiem.Add(diem);
                        if (sinhVien != null)
                        {
                            sinhVien.DanhSachDiem.Add(diem);
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy sinh viên để thêm điểm!");
                        }
                        if (monHoc != null)
                        {
                            monHoc.DanhSachDiem.Add(diem);
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy môn học để thêm điểm!");
                        }

                    }
                }
            }
        }
        public static void SaveDiem()
        {
            using (StreamWriter writer = new StreamWriter("danhSachDiem.txt"))
            {
                foreach (var diem in DanhSachDiem)
                {
                    writer.WriteLine($"{diem.SinhVien?.MaSinhVien},{diem.MonHoc?.MaMonHoc},{diem.PhanTramLop},{diem.PhanTramGiuaKy},{diem.PhanTramCuoiKy},{diem.DiemLop},{diem.DiemGiuaKy},{diem.DiemCuoiKy}");
                }
            }
        }
        public static int TinhTongSoTinChi()
        {
            return MonHocDAL.DanhSachMonHoc.Sum(mh => mh.SoTinChi);
        }
        public static double TinhSoTinChiToiThieu()
        {
            return TinhTongSoTinChi() * 0.9;
        }
    }
}
