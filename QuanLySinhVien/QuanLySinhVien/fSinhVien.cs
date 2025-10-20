using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QuanLySinhVien
{
    public partial class fSinhVien : Form
    {
        public fSinhVien()
        {
            InitializeComponent();
           
        }
        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyTaiKhoan f = new fQuanLyTaiKhoan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyLop f = new fQuanLyLop();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýCVHTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fCoVanHocTap f = new fCoVanHocTap();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyMonHoc f = new fQuanLyMonHoc();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyDiem f = new fQuanLyDiem();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void thôngTinChiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fThongTinChiTiet f = new fThongTinChiTiet();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDoiMatKhau f = new fDoiMatKhau();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
