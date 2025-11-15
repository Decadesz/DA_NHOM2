
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
            btnXemHetDiem.Click += BtnXemHetDiem_Click;
            dgvDiemTrungBinhHocTap.AutoGenerateColumns = false;
            dgvBangDiem.AutoGenerateColumns = false;
            dgvDiemHocKy.AutoGenerateColumns = false;
        }

        private void BtnXemHetDiem_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.ToUpper();
            if (string.IsNullOrEmpty(maSinhVien))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            List<DiemSinhVien> diemSinhVien = DiemDAL.DanhSachDiem
                .Where(d => d.SinhVien.MaSinhVien == maSinhVien)
                .Select(d => new DiemSinhVien
                {
                    MaSinhVien = d.MaSinhVien,
                    TenSinhVien = d.SinhVien.HoTen,
                    MaMonHoc = d.MonHoc.MaMonHoc,
                    TenMonHoc = d.TenMonHoc,
                    DiemTrungBinh = Math.Round(d.DiemTrungBinh, 2),
                    SoTinChi = d.MonHoc.SoTinChi,
                    SoTiet = d.MonHoc.SoTietLyThuyet + d.MonHoc.SoTietThucHanh,
                    Loai = d.DiemThangChu,
                    KetQua = d.KetQua,
                    TenHocKy = d.HocKy.TenHocKy
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
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.ToUpper();
            string tenHocKy = txtHocKy.Text.ToUpper();
            if (string.IsNullOrEmpty(maSinhVien))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(string.IsNullOrEmpty(tenHocKy))
            {
                MessageBox.Show("Vui lòng nhập học kỳ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (string.IsNullOrEmpty(maSinhVien))
            {
                return;
            }

            List<DiemSinhVien> diemSinhVien = DiemDAL.DanhSachDiem
                .Where(d => d.SinhVien.MaSinhVien == maSinhVien && d.HocKy.TenHocKy == tenHocKy)
                .Select(d => new DiemSinhVien
                {
                    MaSinhVien = d.MaSinhVien,
                    TenSinhVien = d.SinhVien.HoTen,
                    MaMonHoc = d.MonHoc.MaMonHoc,
                    TenMonHoc = d.TenMonHoc,
                    DiemTrungBinh = Math.Round(d.DiemTrungBinh, 2),
                    SoTinChi = d.MonHoc.SoTinChi,
                    SoTiet = d.MonHoc.SoTietLyThuyet + d.MonHoc.SoTietThucHanh,
                    Loai = d.DiemThangChu,
                    KetQua = d.KetQua,
                    TenHocKy = d.HocKy.TenHocKy
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
            List<Diem> diemGoc = DiemDAL.DanhSachDiem
                .Where(d => d.SinhVien.MaSinhVien == maSinhVien && d.HocKy.TenHocKy == tenHocKy)
                .ToList();
            if (diemGoc.Count == 0)
            {
                dgvDiemHocKy.DataSource = null;
                return;
            }
            int tongTinChi = diemGoc.Sum(d => d.MonHoc.SoTinChi);
            double tongDiem = diemGoc.Sum(d => d.DiemTrungBinh * d.MonHoc.SoTinChi);
            List<DiemSinhVien> diemHocKy = new List<DiemSinhVien>
            {
                new DiemSinhVien
                {
                    SoTinChiTichLuy= tongTinChi,
                    DiemTrungBinhHocTap = Math.Round(tongDiem / tongTinChi,2)
                }
            };
            lblTenHocKy.Text = "Học kỳ: " + tenHocKy;
            dgvDiemHocKy.DataSource = diemHocKy;
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
