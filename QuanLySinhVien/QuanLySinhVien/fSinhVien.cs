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
        private BindingSource bindingSource =new BindingSource();
        public fSinhVien()
        {
            InitializeComponent();
            caiDatDieuKhien();
            napDuLieuComboBox();
            lamMoi();

        }
        private void lamMoi()
        {
            sapXepSinhVienTheoMa();
            sapXepSinhVienTheoLop();
            bindingSource.DataSource = null;
            bindingSource.DataSource = KhoDuLieu.DanhSachSinhVien;
            dgvSinhVien.Refresh();
        }
        private void anXoaSua()
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void hienXoaSua()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
        private void caiDatDieuKhien()
        {

            dgvSinhVien.AutoGenerateColumns = false;
            bindingSource.DataSource = KhoDuLieu.DanhSachSinhVien;
            dgvSinhVien.DataSource = bindingSource;
            if(KhoDuLieu.DanhSachSinhVien.Count==0)
            {
                anXoaSua();
            }
            cboSearch.SelectedIndex= 0;
            //event
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;
            btnLoad.Click += BtnLoad_Click;
            btnRefresh.Click += BtnRefresh_Click;
            cboSearch.Click += CboSearch_Click;
            btnSearch.Click += BtnSearch_Click;
            dgvSinhVien.SelectionChanged += DgvSinhVien_SelectionChanged;
            //Tắt chức năng quản lý tài khoản nếu không phải tài khoản lại phong dao tao
            if (KhoDuLieu.TaiKhoanHienTai.LoaiTaiKhoanDangNhap != "Phong Dao Tao")
            {
                quảnLýTàiKhoảnToolStripMenuItem.Visible = false;
            }
        }
        private void sapXepSinhVienTheoLop()
        {
            KhoDuLieu.DanhSachSinhVien.Sort((a, b) => string.Compare(a.TenLop, b.TenLop, StringComparison.Ordinal));
        }
        private void sapXepSinhVienTheoMa()
        {
            KhoDuLieu.DanhSachSinhVien.Sort((a, b) => string.Compare(a.MaSinhVien, b.MaSinhVien, StringComparison.Ordinal));
        }
        private void BtnSearch_Click(object sender,EventArgs e)
        {
            CboSearch_Click(sender, e);
        }
        private void CboSearch_Click(object sender,EventArgs e)
        {
            string timKiem=txtSearch.Text.Trim().ToLower();
            if(string.IsNullOrEmpty(timKiem) || cboSearch.SelectedIndex == 0)
            {
                bindingSource.DataSource = KhoDuLieu.DanhSachSinhVien;
                dgvSinhVien.DataSource = bindingSource;
                return;
            }
            List<SinhVien> ketQua=new List<SinhVien>();
            switch(cboSearch.SelectedItem.ToString())
            {
                case "Mã sinh viên":
                    ketQua = KhoDuLieu.DanhSachSinhVien.Where(s => !string.IsNullOrEmpty(s.MaSinhVien) && s.MaSinhVien.ToLower().Contains(timKiem)).ToList();
                    break;
                case "Tên sinh viên":
                    ketQua = KhoDuLieu.DanhSachSinhVien.Where(s => !string.IsNullOrEmpty(s.HoTen) && s.HoTen.ToLower().Contains(timKiem)).ToList();
                    break;
                case "Giới tính":
                    ketQua = KhoDuLieu.DanhSachSinhVien.Where(s => !string.IsNullOrEmpty(s.GioiTinh) && s.GioiTinh.ToLower().Contains(timKiem)).ToList();
                    break;
                case "Tên lớp":
                    ketQua = KhoDuLieu.DanhSachSinhVien.Where(s => !string.IsNullOrEmpty(s.TenLop) && s.TenLop.ToLower().Contains(timKiem)).ToList();
                    break;
                case "Tên cố vấn":
                    ketQua = KhoDuLieu.DanhSachSinhVien.Where(s => !string.IsNullOrEmpty(s.TenCoVan) && s.TenCoVan.ToLower().Contains(timKiem)).ToList();
                    break;
                default:
                    ketQua = KhoDuLieu.DanhSachSinhVien;
                    break;
            }
            bindingSource.DataSource = ketQua;
            dgvSinhVien.DataSource = bindingSource;
        }
        private bool kiemTraDauVao()
        {
            if(string.IsNullOrWhiteSpace(txtMaSinhVien.Text))
            {
                MessageBox.Show("Mã sinh viên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(string.IsNullOrWhiteSpace(txtTenSinhVien.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(dtpNgaySinh.Value >= DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(dtpNgayNhapHoc.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày nhập học không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void ClearInput()
        {
            txtMaSinhVien.Text = "";
            txtTenSinhVien.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            txtDiaChi.Text = "";
            dtpNgayNhapHoc.Value = DateTime.Now;
            txtSoDienThoai.Text = "";
            radNam.Checked = true;
            if (cboTenCoVan.Items.Count > 0)
            {
                cboTenCoVan.SelectedIndex = 0;
            }
            if (cboTenLop.Items.Count > 0)
            {
                cboTenLop.SelectedIndex = 0;
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(!kiemTraDauVao())
            {
                return;
            }
            if(KhoDuLieu.DanhSachSinhVien.Any(sv => sv.MaSinhVien == txtMaSinhVien.Text))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            //ep kieu an toan
            Lop lop =cboTenLop.SelectedItem as Lop;
            CoVanHocTap coVan = cboTenCoVan.SelectedItem as CoVanHocTap;
            if (lop != null)
            {
                if (lop.SoLuong >= Lop.siSoToiDa)
                {
                    MessageBox.Show("Lớp đã đạt sĩ số tối đa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            SinhVien sinhVien= new SinhVien(
                txtMaSinhVien.Text,
                txtTenSinhVien.Text,
                gioiTinh,
                dtpNgaySinh.Value,
                dtpNgayNhapHoc.Value,
                txtDiaChi.Text,
                txtSoDienThoai.Text,
                lop,
                coVan,
                new List<Diem>()
                );
            KhoDuLieu.DanhSachSinhVien.Add(sinhVien);
            if (lop != null)
            {
                lop.DanhSachSinhVien.Add(sinhVien);
            }
            if(coVan != null)
            {
                coVan.DanhSachSinhVien.Add(sinhVien);
            }
            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hienXoaSua();
            lamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = dgvSinhVien.CurrentRow?.DataBoundItem as SinhVien;
            if (sinhVien == null)
            {
                return;
            }
            if (!kiemTraDauVao())
            {
                return;
            }
            Lop lopCu = sinhVien.Lop;
            Lop lopMoi = cboTenLop.SelectedItem as Lop;
            if (lopMoi != null && lopMoi.SoLuong >= Lop.siSoToiDa && lopMoi != lopCu)
            {
                MessageBox.Show("Lớp đã đạt sĩ số tối đa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Kiểm tra trùng mã sinh viên
            string maSVCu = sinhVien.MaSinhVien;
            string maSVmoi = txtMaSinhVien.Text.Trim();
            bool trungMa = KhoDuLieu.DanhSachSinhVien.Any(sv => sv.MaSinhVien == maSVmoi && sv.MaSinhVien != maSVCu);
            if (trungMa)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại! Vui lòng nhập mã khác.","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sinhVien.MaSinhVien = txtMaSinhVien.Text;
            sinhVien.HoTen = txtTenSinhVien.Text;
            sinhVien.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
            sinhVien.NgaySinh = dtpNgaySinh.Value;
            sinhVien.DiaChi = txtDiaChi.Text;
            sinhVien.NgayNhapHoc = dtpNgayNhapHoc.Value;
            sinhVien.SoDienThoai = txtSoDienThoai.Text;
            sinhVien.Lop = cboTenLop.SelectedItem as Lop;
            sinhVien.CoVan = cboTenCoVan.SelectedItem as CoVanHocTap;
            //Cập nhật lớp
            if (lopCu != lopMoi)
            {

                if (lopCu != null)
                {
                    lopCu.DanhSachSinhVien.Remove(sinhVien);
                    lopCu.SoLuong--;
                }
                if (lopMoi != null)
                {
                    lopMoi.DanhSachSinhVien.Add(sinhVien);
                    lopMoi.SoLuong++;
                }
            }
            lamMoi();
            bindingSource.ResetBindings(false);
            MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = dgvSinhVien.CurrentRow?.DataBoundItem as SinhVien;
            if (sinhVien ==null)
            {
                return;
            }
            if (MessageBox.Show($"Bạn có chắc muốn xóa sinh viên {sinhVien.HoTen}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (sinhVien.Lop != null)
                {
                    sinhVien.Lop.DanhSachSinhVien.Remove(sinhVien);
                }
                if (sinhVien.CoVan != null)
                {
                    sinhVien.CoVan.DanhSachSinhVien.Remove(sinhVien);
                }
                KhoDuLieu.DanhSachSinhVien.Remove(sinhVien);
                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(KhoDuLieu.DanhSachSinhVien.Count==0)
                {
                    anXoaSua();
                }
                lamMoi();
                bindingSource.ResetBindings(false);
                ClearInput();
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                KhoDuLieu.SaveSinhVien();
                MessageBox.Show("Đã lưu danh sách sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                KhoDuLieu.LoadSinhVien();
                bindingSource.ResetBindings(false);
                napDuLieuComboBox();
                MessageBox.Show("Đã tải danh sách sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            bindingSource.ResetBindings(false);
            napDuLieuComboBox();
            lamMoi();
        }
        private void DgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            SinhVien sinhVien = dgvSinhVien.CurrentRow?.DataBoundItem as SinhVien;
            if (sinhVien == null)
            {
                return;
            }
            txtMaSinhVien.Text = sinhVien.MaSinhVien;
            txtTenSinhVien.Text = sinhVien.HoTen;
            txtDiaChi.Text = sinhVien.DiaChi;
            txtSoDienThoai.Text = sinhVien.SoDienThoai;
            dtpNgaySinh.Value = sinhVien.NgaySinh;
            dtpNgayNhapHoc.Value = sinhVien.NgayNhapHoc;
            if (sinhVien.GioiTinh == "Nam")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }
            cboTenCoVan.SelectedItem = sinhVien.CoVan;
            cboTenLop.SelectedItem = sinhVien.Lop;
            
        }
        private void napDuLieuComboBox()
        {
            
            cboTenLop.DataSource = KhoDuLieu.DanhSachLop;
            cboTenLop.DisplayMember = "TenLop";
            cboTenLop.ValueMember = "MaLop";

            
            cboTenCoVan.DataSource = KhoDuLieu.DanhSachCoVan;
            cboTenCoVan.DisplayMember = "HoTen";
            cboTenCoVan.ValueMember = "MaCoVan";
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
