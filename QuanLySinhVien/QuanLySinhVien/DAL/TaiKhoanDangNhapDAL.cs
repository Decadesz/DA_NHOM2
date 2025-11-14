using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.DAL
{
    internal class TaiKhoanDangNhapDAL
    {
        public static List<TaiKhoanDangNhap> DanhSachTaiKhoan { get; private set; } = new List<TaiKhoanDangNhap>();
        public static TaiKhoanDangNhap TaiKhoanHienTai { get; set; }
        public static void LoadTaiKhoan()
        {
            DanhSachTaiKhoan.Clear();
            if (!File.Exists("taiKhoan.txt"))
                return;
            foreach (string line in File.ReadAllLines("taiKhoan.txt"))
            {
                string[] parts = line.Split(',');
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
                foreach (TaiKhoanDangNhap tk in DanhSachTaiKhoan)
                {
                    writer.WriteLine($"{tk.TenDangNhap},{tk.MatKhau},{tk.LoaiTaiKhoanDangNhap}");
                }
            }
        }
    }
}
