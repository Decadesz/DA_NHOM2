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
    public partial class fQuanLyMonHoc : Form
    {
        private BindingSource bindingSource =new BindingSource();
        private MonHocBLL monHocBLL = new MonHocBLL();
        public fQuanLyMonHoc()
        {
            InitializeComponent();
            CaiDatDieuKhien();
            LamMoi();
        }
        private void SapXepMonHocTheoMa()
        {
            monHocBLL.DanhSachMonHoc.Sort((a, b) => string.Compare(a.MaMonHoc, b.MaMonHoc, StringComparison.CurrentCultureIgnoreCase));
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
                bindingSource.DataSource = monHocBLL.DanhSachMonHoc;
                dgvMonHoc.DataSource = bindingSource;
                return;
            }
            List<MonHoc> ketQua = monHocBLL.TimKiemMonHoc(timKiem,cboSearch);

            bindingSource.DataSource = ketQua;
            dgvMonHoc.DataSource = bindingSource;
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

            dgvMonHoc.AutoGenerateColumns = false;
            bindingSource.DataSource = monHocBLL.DanhSachMonHoc;
            dgvMonHoc.DataSource = bindingSource;
            if(monHocBLL.DanhSachMonHoc.Count==0)
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
            dgvMonHoc.SelectionChanged += DgvMonHoc_SelectionChanged;
            cboSearch.Click += CboSearch_Click;
            btnSearch.Click += BtnSearch_Click;
            cboSearch.SelectedIndex = 0;
        }
        private bool KiemTraDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtMaMonHoc.Text))
            {
                MessageBox.Show("Mã môn học không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenMonHoc.Text))
            {
                MessageBox.Show("Tên môn học không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
            private void ClearInput()
        {
            txtMaMonHoc.Text = "";
            txtTenMonHoc.Text = "";
            nudSoTinChi.Value = 0;
            nudSoTietLyThuyet.Value = 0;
            nudSoTietThucHanh.Value = 0;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(!KiemTraDauVao())
            {
                return;
            }
            
            MonHoc monHoc = new MonHoc
                (
                txtMaMonHoc.Text.ToUpper(),
                txtTenMonHoc.Text,
                (int)nudSoTinChi.Value,
                (int)nudSoTietLyThuyet.Value,
                (int)nudSoTietThucHanh.Value,
                new List<Diem>()
                );
            bool ketQuaThem = monHocBLL.ThemMonHoc(monHoc);
            if (ketQuaThem == false)
            {
                MessageBox.Show("Mã môn học đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Thêm môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            HienXoaSua();
            LamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            MonHoc monHoc = dgvMonHoc.CurrentRow?.DataBoundItem as MonHoc;
            if(!KiemTraDauVao())
            {
                return;
            }
            bool ketQuaSua = monHocBLL.SuaMonHoc(monHoc, new MonHoc
                (
                txtMaMonHoc.Text.ToUpper(),
                txtTenMonHoc.Text,
                (int)nudSoTinChi.Value,
                (int)nudSoTietLyThuyet.Value,
                (int)nudSoTietThucHanh.Value,
                monHoc.DanhSachDiem
                ));
            if (ketQuaSua == false)
            {
                MessageBox.Show("Mã môn học đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LamMoi();
            bindingSource.ResetBindings(false);
            
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            MonHoc monHoc = dgvMonHoc.CurrentRow?.DataBoundItem as MonHoc;
            
            
            if (MessageBox.Show($"Xóa môn học {monHoc.TenMonHoc}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool ketQuaXoa = monHocBLL.XoaMonHoc(monHoc);
                if(ketQuaXoa == false)
                {
                    MessageBox.Show("Xóa môn học thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (monHocBLL.DanhSachMonHoc.Count==0)
                {
                    AnXoaSua();
                }
                LamMoi();
                bindingSource.ResetBindings(false);
                ClearInput();
            }
            
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {   try
            {
                MonHocDAL.SaveMonHoc();
                MessageBox.Show("Đã lưu danh sách môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi khi lưu danh sách môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            MonHocDAL.LoadMonHoc();
            LamMoi();
            MessageBox.Show("Đã tải lại danh sách môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(monHocBLL.DanhSachMonHoc.Count > 0)
            {
                HienXoaSua();
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LamMoi();
            bindingSource.ResetBindings(false);
        }
        private void LamMoi()
        {
            SapXepMonHocTheoMa();
            bindingSource.DataSource = null;
            bindingSource.DataSource = monHocBLL.DanhSachMonHoc;
            dgvMonHoc.Refresh();
        }
        private void DgvMonHoc_SelectionChanged(object sender, EventArgs e)
        {
            MonHoc monHoc=dgvMonHoc.CurrentRow?.DataBoundItem as MonHoc;
            if (monHoc == null)
            {
                return;
            }
                txtMaMonHoc.Text = monHoc.MaMonHoc;
                txtTenMonHoc.Text = monHoc.TenMonHoc;
                nudSoTinChi.Value = monHoc.SoTinChi;
                nudSoTietLyThuyet.Value = monHoc.SoTietLyThuyet;
                nudSoTietThucHanh.Value = monHoc.SoTietThucHanh;
        }
    }
}

