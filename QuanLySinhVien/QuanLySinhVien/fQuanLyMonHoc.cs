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
        public fQuanLyMonHoc()
        {
            InitializeComponent();
            caiDatDieuKhien();
            lamMoi();
        }
        private void sapXepMonHocTheoMa()
        {
            KhoDuLieu.DanhSachMonHoc.Sort((a, b) => string.Compare(a.MaMonHoc, b.MaMonHoc, StringComparison.CurrentCultureIgnoreCase));
        }
        private void lamMoi()
        {
            sapXepMonHocTheoMa();
            bindingSource.DataSource = null;
            bindingSource.DataSource = KhoDuLieu.DanhSachMonHoc;
            dgvMonHoc.Refresh();
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
                bindingSource.DataSource = KhoDuLieu.DanhSachMonHoc;
                dgvMonHoc.DataSource = bindingSource;
                return;
            }
            List<MonHoc> ketQua = new List<MonHoc>();
            switch (cboSearch.SelectedItem.ToString())
            {
                case "Mã môn học":
                    ketQua = KhoDuLieu.DanhSachMonHoc.Where(mh => !string.IsNullOrEmpty(mh.MaMonHoc) && mh.MaMonHoc.ToLower().Contains(timKiem)).ToList();
                    break;
                case "Tên môn học":
                    ketQua = KhoDuLieu.DanhSachMonHoc.Where(mh => !string.IsNullOrEmpty(mh.TenMonHoc) && mh.TenMonHoc.ToLower().Contains(timKiem)).ToList();
                    break;
                default:
                    ketQua = KhoDuLieu.DanhSachMonHoc;
                    break;
            }
            bindingSource.DataSource = ketQua;
            dgvMonHoc.DataSource = bindingSource;
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

            dgvMonHoc.AutoGenerateColumns = false;
            bindingSource.DataSource = KhoDuLieu.DanhSachMonHoc;
            dgvMonHoc.DataSource = bindingSource;
            if(KhoDuLieu.DanhSachMonHoc.Count==0)
            {
                anXoaSua();
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
        private bool kiemTraDauVao()
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
            if(!kiemTraDauVao())
            {
                return;
            }
            if(KhoDuLieu.DanhSachMonHoc.Any(m => m.MaMonHoc == txtMaMonHoc.Text.Trim()))
            {
                MessageBox.Show("Mã môn học đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MonHoc monHoc = new MonHoc
                (
                txtMaMonHoc.Text,
                txtTenMonHoc.Text,
                (int)nudSoTinChi.Value,
                (int)nudSoTietLyThuyet.Value,
                (int)nudSoTietThucHanh.Value
                );
            KhoDuLieu.DanhSachMonHoc.Add(monHoc);
            MessageBox.Show("Thêm môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hienXoaSua();
            lamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            MonHoc monHoc = dgvMonHoc.CurrentRow?.DataBoundItem as MonHoc;
            if(monHoc==null)
            {
                return;
            }

            string maMonHocCu = monHoc.MaMonHoc;
            string maMonHocMoi = txtMaMonHoc.Text;
            bool trungMa = KhoDuLieu.DanhSachMonHoc.Any(m => m.MaMonHoc.Equals(maMonHocMoi, StringComparison.OrdinalIgnoreCase) && m.MaMonHoc != maMonHocCu);

            if (trungMa)
            {
                MessageBox.Show("Mã môn học đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            monHoc.MaMonHoc = txtMaMonHoc.Text;
            monHoc.TenMonHoc = txtTenMonHoc.Text;
            monHoc.SoTinChi = (int)nudSoTinChi.Value;
            monHoc.SoTietLyThuyet = (int)nudSoTietLyThuyet.Value;
            monHoc.SoTietThucHanh = (int)nudSoTietThucHanh.Value;
            lamMoi();
            bindingSource.ResetBindings(false);
            MessageBox.Show("Cập nhật thông tin môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            MonHoc monHoc = dgvMonHoc.CurrentRow?.DataBoundItem as MonHoc;
            if(monHoc == null)
            {
                return;
            }
            
            if (MessageBox.Show($"Xóa môn học {monHoc.TenMonHoc}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                KhoDuLieu.DanhSachMonHoc.Remove(monHoc);
                MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(KhoDuLieu.DanhSachMonHoc.Count==0)
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
            KhoDuLieu.SaveMonHoc();
            MessageBox.Show("Đã lưu danh sách môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            KhoDuLieu.LoadMonHoc();
            bindingSource.DataSource = KhoDuLieu.DanhSachMonHoc;
            bindingSource.ResetBindings(false);
            MessageBox.Show("Đã tải lại danh sách môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            lamMoi();
            bindingSource.ResetBindings(false);
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

