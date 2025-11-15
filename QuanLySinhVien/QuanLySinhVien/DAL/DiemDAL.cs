using QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            foreach (string line in File.ReadAllLines("danhSachDiem.txt"))
            {
                string [] parts = line.Split(',');
                if (parts.Length >= 9)
                {
                    SinhVien sinhVien = SinhVienDAL.DanhSachSinhVien.FirstOrDefault(s => s.MaSinhVien == parts[0]);
                    MonHoc monHoc = MonHocDAL.DanhSachMonHoc.FirstOrDefault(m => m.MaMonHoc == parts[1]);
                    HocKy hocKy=HocKyDAL.DanhSachHocKy.FirstOrDefault(hk => hk.MaHocKy == parts[2]);
                    if (sinhVien != null && monHoc != null && hocKy!=null)
                    {
                        Diem diem = new Diem(
                            int.Parse(parts[3]),
                            int.Parse(parts[4]),
                            int.Parse(parts[5]),
                            double.Parse(parts[6]),
                            double.Parse(parts[7]),
                            double.Parse(parts[8]),
                            sinhVien,
                            monHoc,
                            hocKy
                            )
                        {
                            SinhVien = sinhVien,
                            MonHoc = monHoc,
                            HocKy = hocKy
                        };
                        DanhSachDiem.Add(diem);
                        if (sinhVien != null)
                        {
                            sinhVien.DanhSachDiem.Add(diem);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên để thêm điểm!");
                        }
                        if (monHoc != null)
                        {
                            monHoc.DanhSachDiem.Add(diem);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy môn học để thêm điểm!");
                        }
                        if(hocKy != null)
                        {
                            hocKy.DanhSachDiem.Add(diem);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy học kỳ để thêm điểm!");
                        }

                    }
                }
            }
        }
        public static void SaveDiem()
        {
            using (StreamWriter writer = new StreamWriter("danhSachDiem.txt"))
            {
                foreach (Diem diem in DanhSachDiem)
                {
                    writer.WriteLine($"{diem.SinhVien?.MaSinhVien},{diem.MonHoc?.MaMonHoc},{diem.HocKy?.MaHocKy},{diem.PhanTramLop},{diem.PhanTramGiuaKy},{diem.PhanTramCuoiKy},{diem.DiemLop},{diem.DiemGiuaKy},{diem.DiemCuoiKy}");
                }
            }
        }

    }
}
