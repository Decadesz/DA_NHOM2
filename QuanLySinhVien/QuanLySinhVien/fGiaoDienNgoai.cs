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
    public partial class fGiaoDienNgoai : Form
    {
        public fGiaoDienNgoai()
        {
            InitializeComponent();
            phanQuyen();
        }
        public void phanQuyen()
        {
            if (KhoDuLieu.TaiKhoanHienTai.LoaiTaiKhoanDangNhap == "Sinh Vien")
            {
                btnTaiKhoan.Visible = false;
                btnMonHoc.Visible = false;
                btnLop.Visible = false;
                btnDiem.Visible = false;
                btnCoVan.Visible = false;
            }
            else if (KhoDuLieu.TaiKhoanHienTai.LoaiTaiKhoanDangNhap == "Giang Vien")
            {
                btnSinhVien.Visible = false;
                btnTaiKhoan.Visible = false;
                btnMonHoc.Visible = false;
                btnCoVan.Visible = false;
                btnLop.Visible = false;
            }
        }
        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            fSinhVien f = new fSinhVien();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            fQuanLyMonHoc f = new fQuanLyMonHoc();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            fBangDiemSV f = new fBangDiemSV();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnLop_Click(object sender, EventArgs e)
        {
            fQuanLyLop f = new fQuanLyLop();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnDiem_Click(object sender, EventArgs e)
        {
            fQuanLyDiem f = new fQuanLyDiem();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnThongTinTaiKhoan_Click(object sender, EventArgs e)
        {
            fThongTinChiTiet f = new fThongTinChiTiet();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnCoVan_Click(object sender, EventArgs e)
        {
            fCoVanHocTap f = new fCoVanHocTap();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            fQuanLyTaiKhoan f = new fQuanLyTaiKhoan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            fDoiMatKhau f = new fDoiMatKhau();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
  
}
