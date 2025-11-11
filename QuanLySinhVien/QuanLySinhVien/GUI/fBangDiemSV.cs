
using QuanLySinhVien.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class fBangDiemSV : Form
    {
        public fBangDiemSV()
        {
            InitializeComponent();
            btnSearch.Click += BtnSearch_Click;
            dgvDiemTrungBinhHocTap.AutoGenerateColumns = false;
            dgvBangDiem.AutoGenerateColumns = false;
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.ToUpper();
            if (string.IsNullOrEmpty(maSinhVien))
            {
                return;
            }

            List<DiemSinhVien> diemSinhVien = DiemDAL.DanhSachDiem
                .Where(d => d.SinhVien.MaSinhVien == maSinhVien)
                .Select(d => new DiemSinhVien
                {
                    MaSinhVien = d.MaSinhVien,
                    TenSinhVien=d.SinhVien.HoTen,
                    MaMonHoc=d.MonHoc.MaMonHoc,
                    TenMonHoc = d.TenMonHoc,
                    DiemTrungBinh = Math.Round(d.DiemTrungBinh, 2),
                    SoTinChi = d.MonHoc.SoTinChi,
                    SoTiet = d.MonHoc.SoTietLyThuyet + d.MonHoc.SoTietThucHanh,
                    Loai = d.DiemThangChu,
                    KetQua = d.KetQua
                  
                })
                .ToList();

            dgvBangDiem.DataSource = diemSinhVien;
            List<DiemSinhVien> diemTrungBinhHocTap = SinhVienDAL.DanhSachSinhVien
                .Where(dtb => dtb.MaSinhVien == maSinhVien)
                .Select(dtb => new DiemSinhVien
                {
                    DiemTrungBinhHocTap = Math.Round(dtb.DiemTrungBinhHocTap, 2),
                    SoTinChiTichLuy = dtb.SoTinChiTichLuy,
                    XepLoai = dtb.XepLoai
                })
                .ToList();
            dgvDiemTrungBinhHocTap.DataSource = diemTrungBinhHocTap;
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDoiMatKhau f = new fDoiMatKhau();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fThongTinChiTiet f= new fThongTinChiTiet();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
