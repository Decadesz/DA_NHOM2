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
        public fQuanLyLop()
        {
            InitializeComponent();
            caiDatDieuKhien();
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

            dgvLop.AutoGenerateColumns = false;
            bindingSource.DataSource = KhoDuLieu.DanhSachLop;
            dgvLop.DataSource = bindingSource;
            if(KhoDuLieu.DanhSachLop.Count==0)
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
            dgvLop.SelectionChanged += DgvLop_SelectionChanged;
        }
        private bool kiemTraDauVao()
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
            if(!kiemTraDauVao())
            {
                return;
            }
            if(KhoDuLieu.DanhSachLop.Any(l=>l.MaLop==txtMaLop.Text))
            {
                MessageBox.Show("Mã lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Lop lop = new Lop(txtMaLop.Text,txtTenLop.Text);
            KhoDuLieu.DanhSachLop.Add(lop);
            MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hienXoaSua();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            
            Lop lop = dgvLop.CurrentRow?.DataBoundItem as Lop;
            if(lop==null)
            {
                return;
            }
            string lopCu = lop.MaLop;
            string lopMoi = txtMaLop.Text;
            bool trungMa = KhoDuLieu.DanhSachLop.Any(l => l.MaLop.Equals(lopMoi, StringComparison.OrdinalIgnoreCase) && l.MaLop != lopMoi);

            if (trungMa)
            {
                MessageBox.Show("Mã lớp đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lop.MaLop = txtMaLop.Text;
            lop.TenLop= txtTenLop.Text;
            MessageBox.Show("Sửa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bindingSource.ResetBindings(false);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if(dgvLop.CurrentRow==null)
            {
                return;
            }
            Lop lop = dgvLop.CurrentRow.DataBoundItem as Lop;
            if (MessageBox.Show($"Xóa lớp{lop.TenLop }?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                KhoDuLieu.DanhSachLop.Remove(lop);
                MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(KhoDuLieu.DanhSachLop.Count==0)
                {
                    anXoaSua();
                }
                bindingSource.ResetBindings(false);
                ClearInput();
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                KhoDuLieu.SaveLop();
                bindingSource.DataSource = KhoDuLieu.DanhSachLop;
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
                KhoDuLieu.LoadLop();
                bindingSource.DataSource = KhoDuLieu.DanhSachLop;

                bindingSource.ResetBindings(false);
                MessageBox.Show("Đã tải lại danh sách lớp!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                
                    MessageBox.Show($"Lỗi khi tải lại dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            bindingSource.ResetBindings(false);
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
