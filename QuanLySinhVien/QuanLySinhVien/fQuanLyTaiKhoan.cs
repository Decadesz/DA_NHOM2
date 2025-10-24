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
    
    public partial class fQuanLyTaiKhoan : Form
    {
        private BindingSource bindingSource = new BindingSource();
        public fQuanLyTaiKhoan()
        {
            InitializeComponent();
            caiDatDieuKhien();
        }
        private void caiDatDieuKhien()
        {

            dgvTaiKhoan.AutoGenerateColumns = false;
            bindingSource.DataSource = KhoDuLieu.DanhSachTaiKhoan;
            dgvTaiKhoan.DataSource = bindingSource;
            //event
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;
            btnLoad.Click += BtnLoad_Click;
            btnRefresh.Click += BtnRefresh_Click;
            dgvTaiKhoan.SelectionChanged += DgvTaiKhoan_SelectionChanged;
        }
        private bool kiemTraDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void ClearInput()
        {
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            cboLoaiTaiKhoan.SelectedIndex = 0;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!kiemTraDauVao()) return;

            if (KhoDuLieu.DanhSachTaiKhoan.Any(t => t.TenDangNhap.Equals(txtTenDangNhap.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TaiKhoanDangNhap taiKhoan = new TaiKhoanDangNhap(
                txtTenDangNhap.Text,
                txtMatKhau.Text,
                cboLoaiTaiKhoan.Text
            );

            KhoDuLieu.DanhSachTaiKhoan.Add(taiKhoan);
            MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            TaiKhoanDangNhap taiKhoan = dgvTaiKhoan.CurrentRow.DataBoundItem as TaiKhoanDangNhap;
            if (taiKhoan == null)
            {
                return;
            }
            if (!kiemTraDauVao())
            {
                return;
            }

            string tenMoi = txtTenDangNhap.Text;
            bool trungTen = KhoDuLieu.DanhSachTaiKhoan.Any(t =>
                t.TenDangNhap.Equals(tenMoi, StringComparison.OrdinalIgnoreCase) && t != taiKhoan);
            if (trungTen)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            taiKhoan.TenDangNhap = tenMoi;
            taiKhoan.MatKhau = txtMatKhau.Text;
            taiKhoan.LoaiTaiKhoanDangNhap = cboLoaiTaiKhoan.Text;

            MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bindingSource.ResetBindings(false);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            TaiKhoanDangNhap taiKhoan = dgvTaiKhoan.CurrentRow.DataBoundItem as TaiKhoanDangNhap;
            if (taiKhoan == null) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa tài khoản '{taiKhoan.TenDangNhap}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KhoDuLieu.DanhSachTaiKhoan.Remove(taiKhoan);
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bindingSource.ResetBindings(false);
                ClearInput();
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                KhoDuLieu.SaveTaiKhoan();
                MessageBox.Show("Đã lưu danh sách tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                KhoDuLieu.LoadTaiKhoan();
                bindingSource.DataSource = KhoDuLieu.DanhSachTaiKhoan;
                bindingSource.ResetBindings(false);
                MessageBox.Show("Đã tải lại danh sách tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void DgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            TaiKhoanDangNhap taiKhoan = dgvTaiKhoan.CurrentRow.DataBoundItem as TaiKhoanDangNhap;
            if (taiKhoan == null) return;

            txtTenDangNhap.Text = taiKhoan.TenDangNhap;
            txtMatKhau.Text = taiKhoan.MatKhau;
            cboLoaiTaiKhoan.Text = taiKhoan.LoaiTaiKhoanDangNhap;
        }

    }
}
