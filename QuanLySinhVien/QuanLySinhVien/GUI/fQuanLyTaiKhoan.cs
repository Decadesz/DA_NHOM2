using QuanLySinhVien.BLL;
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
    
    public partial class fQuanLyTaiKhoan : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private TaiKhoanDangNhapBLL taiKhoanDangNhapBLL = new TaiKhoanDangNhapBLL();
        public fQuanLyTaiKhoan()
        {
            InitializeComponent();
            CaiDatDieuKhien();
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            CboSearch_Click(sender, e);
        }
        private void CboSearch_Click(object sender, EventArgs e)
        {
            string timKiem = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(timKiem) || cboSearch.SelectedIndex == 0)
            {
                bindingSource.DataSource = taiKhoanDangNhapBLL.DanhSachTaiKhoan;
                dgvTaiKhoan.DataSource = bindingSource;
                return;
            }
            List<TaiKhoanDangNhap> ketQua = taiKhoanDangNhapBLL.TimTaiKhoan(timKiem);

            bindingSource.DataSource = ketQua;
            dgvTaiKhoan.DataSource = bindingSource;
        }
        private void AnXoaSua()
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void HienXoaSua()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
        private void CaiDatDieuKhien()
        {

            dgvTaiKhoan.AutoGenerateColumns = false;
            bindingSource.DataSource = taiKhoanDangNhapBLL.DanhSachTaiKhoan;
            dgvTaiKhoan.DataSource = bindingSource;
            if(taiKhoanDangNhapBLL.DanhSachTaiKhoan.Count==0)
            {
                AnXoaSua();
            }
            //event
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;
            btnLoad.Click += BtnLoad_Click;
            btnRefresh.Click += BtnRefresh_Click;
            dgvTaiKhoan.SelectionChanged += DgvTaiKhoan_SelectionChanged;
            cboSearch.Click += CboSearch_Click;
            btnSearch.Click += BtnSearch_Click;
            cboSearch.SelectedIndex= 0;
        }
        private bool KiemTraDauVao()
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
            if (!KiemTraDauVao()) return;

            

            TaiKhoanDangNhap taiKhoan = new TaiKhoanDangNhap(
                txtTenDangNhap.Text.ToUpper(),
                txtMatKhau.Text,
                cboLoaiTaiKhoan.Text
            );

           bool ketQuaThem = taiKhoanDangNhapBLL.ThemTaiKhoan(taiKhoan);
            if (ketQuaThem == false)
            {
                return;
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            HienXoaSua();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            TaiKhoanDangNhap taiKhoan = dgvTaiKhoan.CurrentRow?.DataBoundItem as TaiKhoanDangNhap;
            if (!KiemTraDauVao())
            {
                return;
            }

            bool ketQuaSua = taiKhoanDangNhapBLL.SuaTaiKhoan(taiKhoan, new TaiKhoanDangNhap(
                txtTenDangNhap.Text.ToUpper(),
                txtMatKhau.Text,
                cboLoaiTaiKhoan.Text
            ));
            if (ketQuaSua == false)
            {
                return;
            }
            else
            {
                MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bindingSource.ResetBindings(false);
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            TaiKhoanDangNhap taiKhoan = dgvTaiKhoan.CurrentRow?.DataBoundItem as TaiKhoanDangNhap;

            if (MessageBox.Show($"Bạn có chắc muốn xóa tài khoản '{taiKhoan.TenDangNhap}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                bool ketQuaXoa = taiKhoanDangNhapBLL.XoaTaiKhoan(taiKhoan);
                if (ketQuaXoa == false)
                {
                    MessageBox.Show("Xóa tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (taiKhoanDangNhapBLL.DanhSachTaiKhoan.Count == 0)
                    {
                        AnXoaSua();
                    }
                    bindingSource.ResetBindings(false);
                    ClearInput();
                }
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanDangNhapDAL.SaveTaiKhoan();
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
                TaiKhoanDangNhapDAL.LoadTaiKhoan();
                bindingSource.DataSource = taiKhoanDangNhapBLL.DanhSachTaiKhoan;
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
            TaiKhoanDangNhap taiKhoan = dgvTaiKhoan.CurrentRow?.DataBoundItem as TaiKhoanDangNhap;
            if (taiKhoan == null) return;

            txtTenDangNhap.Text = taiKhoan.TenDangNhap;
            txtMatKhau.Text = taiKhoan.MatKhau;
            cboLoaiTaiKhoan.Text = taiKhoan.LoaiTaiKhoanDangNhap;
        }

    }
}
