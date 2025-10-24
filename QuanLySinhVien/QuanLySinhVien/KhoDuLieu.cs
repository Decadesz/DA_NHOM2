using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//thu vien doc ghi file
using System.IO;

namespace QuanLySinhVien
{
    internal static class KhoDuLieu
    {
        // Các danh sách lưu trữ dữ liệu
        public static List<SinhVien> DanhSachSinhVien { get; private set; } = new List<SinhVien>();
        public static List<Lop> DanhSachLop { get; private set; } = new List<Lop>();
        public static List<CoVanHocTap> DanhSachCoVan { get; private set; } = new List<CoVanHocTap>();
        public static List<MonHoc> DanhSachMonHoc { get; private set; } = new List<MonHoc>();
        public static List<Diem> DanhSachDiem { get; private set; } = new List<Diem>();
        public static List<TaiKhoanDangNhap> DanhSachTaiKhoan { get; private set; } = new List<TaiKhoanDangNhap>();
        public static TaiKhoanDangNhap TaiKhoanHienTai { get; set; }


        //Sinh Vien
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
                    Lop lop = DanhSachLop.FirstOrDefault(l => l.MaLop == parts[7]);
                    if (lop == null)
                    {
                        lop = new Lop(parts[7], "khong ro");
                    };
                    CoVanHocTap coVan = DanhSachCoVan.FirstOrDefault(c => c.MaCoVan == parts[8]);
                    if (coVan == null)
                    {
                        coVan = new CoVanHocTap(parts[8], "khong ro", DateTime.Now, "khong ro", "khong ro", "khong ro", "khong ro");
                    };
                    SinhVien sinhVien = new SinhVien(
                        parts[0],
                        parts[1],
                        parts[2],
                        DateTime.Parse(parts[3]),
                        DateTime.Parse(parts[4]),
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
                    coVan.DanhSachSinhVien.Add(sinhVien);
                }
            }
        }
        public static void SaveSinhVien()
        {
            using (StreamWriter writer = new StreamWriter("danhSachSinhVien.txt"))
            {
                foreach (var sv in DanhSachSinhVien)
                {
                    writer.WriteLine($"{sv.MaSinhVien},{sv.HoTen},{sv.GioiTinh},{sv.NgaySinh},{sv.NgayNhapHoc},{sv.DiaChi},{sv.SoDienThoai},{sv.Lop?.MaLop},{sv.CoVan?.MaCoVan}");
                }
            }
        }
        //Mon Hoc
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
                        int.Parse(parts[4])
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
        //Diem
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
                    SinhVien sinhVien = DanhSachSinhVien.FirstOrDefault(s => s.MaSinhVien == parts[0]);
                    MonHoc monHoc = DanhSachMonHoc.FirstOrDefault(m => m.MaMonHoc == parts[1]);
                    if (sinhVien != null && monHoc != null)
                    {
                        Diem diem = new Diem(
                            int.Parse(parts[2]),
                            int.Parse(parts[3]),
                            int.Parse(parts[4]),
                            double.Parse(parts[5]),
                            double.Parse(parts[6]),
                            double.Parse(parts[7])
                            )
                        {
                            SinhVien = sinhVien,
                            MonHoc = monHoc
                        };
                        DanhSachDiem.Add(diem);
                        if(sinhVien != null)
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
        //Lop
        public static void LoadLop()
        {
            DanhSachLop.Clear();
            if (!File.Exists("danhSachLop.txt"))
                return;
            foreach (var line in File.ReadAllLines("danhSachLop.txt"))
            {
                var parts = line.Split(',');
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
                foreach (var lop in DanhSachLop)
                {
                    writer.WriteLine($"{lop.MaLop},{lop.TenLop},{lop.SoLuong}");
                }
            }
        }
        //Co Van Hoc Tap
        public static void LoadCoVan()
        {
            DanhSachCoVan.Clear();
            if (!File.Exists("danhSachCoVan.txt"))
                return;
            foreach (var line in File.ReadAllLines("danhSachCoVan.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length >= 7)
                {
                    CoVanHocTap coVan = new CoVanHocTap(
                        parts[0],
                        parts[1],
                        DateTime.Parse(parts[2]),
                        parts[3],
                        parts[4],
                        parts[5],
                        parts[6]
                        );
                    DanhSachCoVan.Add(coVan);
                }
            }
        }
        public static void SaveCoVan()
        {
            using (StreamWriter writer = new StreamWriter("danhSachCoVan.txt"))
            {
                foreach (var cv in DanhSachCoVan)
                {
                    writer.WriteLine($"{cv.MaCoVan},{cv.HoTen},{cv.NgaySinh},{cv.GioiTinh},{cv.DiaChi},{cv.SoDienThoai},{cv.Email}");
                }
            }
        }
        //Tai khoan
        public static void LoadTaiKhoan()
        {
            DanhSachTaiKhoan.Clear();
            if (!File.Exists("taiKhoan.txt"))
                return;
            foreach (var line in File.ReadAllLines("taiKhoan.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    TaiKhoanDangNhap tk = new TaiKhoanDangNhap(parts[0], parts[1], parts[2]);
                    DanhSachTaiKhoan.Add(tk);
                }
            }
        }
        public static void SaveTaiKhoan()
        {
            using (StreamWriter writer = new StreamWriter("taiKhoan.txt"))
            {
                foreach (var tk in DanhSachTaiKhoan)
                {
                    writer.WriteLine($"{tk.TenDangNhap},{tk.MatKhau},{tk.LoaiTaiKhoanDangNhap}");
                }
            }
        }
        //Ham load va save toan bo du lieu moi khi chay ung dung va thoat ung dung
        public static void LoadDuLieu()
        {
            LoadLop();
            LoadCoVan();
            LoadMonHoc();
            LoadSinhVien();
            LoadDiem();
            LoadTaiKhoan();
        }
        public static void SaveDuLieu()
        {
            SaveLop();
            SaveCoVan();
            SaveMonHoc();
            SaveSinhVien();
            SaveDiem();
            SaveTaiKhoan();
        }
    }
}
