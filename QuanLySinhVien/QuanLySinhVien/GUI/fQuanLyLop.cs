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
    public partial class fQuanLyLop : Form
    {
        private BindingSource bindingSource=new BindingSource();
        private LopBLL lopBLL = new LopBLL();
        public fQuanLyLop()
        {
            InitializeComponent();
            CaiDatDieuKhien();
            LamMoi();
        }
        private void SapXepLopTheoMa()
        {
            lopBLL.DanhSachLop.Sort((a, b) => string.Compare(a.MaLop, b.MaLop, StringComparison.CurrentCultureIgnoreCase));
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
                bindingSource.DataSource = lopBLL.DanhSachLop;
                dgvLop.DataSource = bindingSource;
                return;
            }
            List<Lop> ketQua = lopBLL.TimLop(timKiem,cboSearch);
            bindingSource.DataSource = ketQua;
            dgvLop.DataSource = bindingSource;
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

            dgvLop.AutoGenerateColumns = false;
            bindingSource.DataSource = lopBLL.DanhSachLop;
            dgvLop.DataSource = bindingSource;
            if(lopBLL.DanhSachLop.Count==0)
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
            dgvLop.SelectionChanged += DgvLop_SelectionChanged;
            cboSearch.Click += CboSearch_Click;
            btnSearch.Click += BtnSearch_Click;
            cboSearch.SelectedIndex = 0;
        }
        private bool KiemTraDauVao()
        {
            if(string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Mã lớp không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Tên lớp không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void ClearInput()
        {
            txtMaLop.Text = "";
            txtTenLop.Text = "";
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(!KiemTraDauVao())
            {
                return;
            }

            Lop lop = new Lop(txtMaLop.Text.ToUpper(),
                                        txtTenLop.Text,
                                        new List<SinhVien>()
                                        );
            bool ketQuaThem= lopBLL.ThemLop(lop);
            if (ketQuaThem == false)
            {
                MessageBox.Show("Mã lớp đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienXoaSua();
                LamMoi();
                bindingSource.ResetBindings(false);
                ClearInput();
            }
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            
            Lop lop = dgvLop.CurrentRow?.DataBoundItem as Lop;
            if(!KiemTraDauVao())
            {
                return;
            }
            bool ketQuaSua = lopBLL.SuaLop(lop, new Lop(
                txtMaLop.Text.ToUpper(),
                txtTenLop.Text,
                lop.DanhSachSinhVien
                ));
            if(ketQuaSua == false)
            {
                MessageBox.Show("Mã lớp đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Sửa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
         
            bindingSource.ResetBindings(false);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if(dgvLop.CurrentRow==null)
            {
                return;
            }
            Lop lop = dgvLop.CurrentRow.DataBoundItem as Lop;
            if (MessageBox.Show($"Xóa lớp {lop.TenLop } ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool ketQuaXoa = lopBLL.XoaLop(lop);
                if (ketQuaXoa == false)
                {
                    MessageBox.Show("Xóa lớp thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (lopBLL.DanhSachLop.Count == 0)
                    {
                        AnXoaSua();
                    }
                    LamMoi();
                    bindingSource.ResetBindings(false);
                    ClearInput();
                }
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LopDAL.SaveLop();
                bindingSource.DataSource = lopBLL.DanhSachLop;
                bindingSource.ResetBindings(false);
                MessageBox.Show("Đã lưu danh sách lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                LopDAL.LoadLop();
                LamMoi();
                MessageBox.Show("Đã tải lại danh sách lớp!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (lopBLL.DanhSachLop.Count > 0)
                {
                    HienXoaSua();
                }
            }
            catch (Exception ex)
            {
                
                    MessageBox.Show($"Lỗi khi tải lại dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LamMoi();
            bindingSource.ResetBindings(false);
        }
        private void LamMoi()
        {
            SapXepLopTheoMa();
            bindingSource.DataSource = null;
            bindingSource.DataSource = lopBLL.DanhSachLop;
            dgvLop.Refresh();
        }
        private void DgvLop_SelectionChanged(object sender, EventArgs e)
        {
            Lop lop=dgvLop.CurrentRow?.DataBoundItem as Lop;
            if(lop==null)
            {
                return;
            }
            txtMaLop.Text = lop.MaLop;
            txtTenLop.Text = lop.TenLop;
        }
    }
}
