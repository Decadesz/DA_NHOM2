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
    public partial class fCoVanHocTap : Form
    {
        private BindingSource bindingSource = new BindingSource();
        public fCoVanHocTap()
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

            dgvCoVan.AutoGenerateColumns = false;
            bindingSource.DataSource = KhoDuLieu.DanhSachCoVan;
            dgvCoVan.DataSource = bindingSource;
            if(KhoDuLieu.DanhSachCoVan.Count==0)
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
            dgvCoVan.SelectionChanged += DgvCoVan_SelectionChanged;
        }
        private bool kiemTraDauVao()
        {
            if(string.IsNullOrWhiteSpace(txtMaCoVan.Text))
            {
                MessageBox.Show("Không được để mã cố vấn trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenCoVan.Text))
            {
                MessageBox.Show("Không được để tên cố vấn trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Không được để địa chỉ cố vấn trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Không được để email cố vấn trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Không được để số điện thoại cố vấn trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(dtpNgaySinh.Value>=DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void ClearInput()
        {
            txtMaCoVan.Text = "";
            txtTenCoVan.Text = "";
            txtSoDienThoai.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(!kiemTraDauVao())
            {
                return;
            }
            if(KhoDuLieu.DanhSachCoVan.Any(c=>c.MaCoVan== txtMaCoVan.Text))
            {
                    MessageBox.Show("Mã cố vấn đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            CoVanHocTap coVan = new CoVanHocTap
                (
                txtMaCoVan.Text,
                txtTenCoVan.Text,
                dtpNgaySinh.Value,
                gioiTinh,
                txtDiaChi.Text,
                txtSoDienThoai.Text,
                txtEmail.Text
                );
            KhoDuLieu.DanhSachCoVan.Add( coVan );
            MessageBox.Show("Đã thêm cố vấn mới!");
            hienXoaSua();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            CoVanHocTap coVan=dgvCoVan.CurrentRow?.DataBoundItem as CoVanHocTap;
            if(coVan==null)
            {
                return;
            }
            string maCoVanCu = coVan.MaCoVan;
            string maCoVanMoi = txtMaCoVan.Text;
            bool trungMa = KhoDuLieu.DanhSachCoVan.Any(cv => cv.MaCoVan.Equals(maCoVanMoi, StringComparison.OrdinalIgnoreCase) && cv.MaCoVan != maCoVanCu);

            if (trungMa)
            {
                MessageBox.Show("Mã cố vấn đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            coVan.MaCoVan= maCoVanMoi;
            coVan.HoTen=txtTenCoVan.Text;
            coVan.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
            coVan.NgaySinh = dtpNgaySinh.Value;
            coVan.SoDienThoai=txtSoDienThoai.Text;
            coVan.Email= txtEmail.Text;
            bindingSource.ResetBindings(false);

        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            CoVanHocTap coVan=dgvCoVan.CurrentRow?.DataBoundItem as CoVanHocTap;
            if(coVan==null)
            {
                return;
            }
            if (MessageBox.Show($"Xóa{coVan.HoTen }?","Xác nhận",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                KhoDuLieu.DanhSachCoVan.Remove(coVan);
                MessageBox.Show("Xóa cố vấn học tập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (KhoDuLieu.DanhSachCoVan.Count == 0)
                {
                    anXoaSua();
                }
                bindingSource.ResetBindings(false);
            }    
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                KhoDuLieu.SaveCoVan();
                MessageBox.Show("Đã lưu danh sách cố vấn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                KhoDuLieu.SaveCoVan();
                MessageBox.Show("Đã tải danh sách cố vấn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void DgvCoVan_SelectionChanged(object sender, EventArgs e)
        {
            CoVanHocTap coVan =dgvCoVan.CurrentRow?.DataBoundItem as CoVanHocTap;
            if (coVan == null)
            {
                return;
            }
            txtMaCoVan.Text = coVan.MaCoVan;
            txtTenCoVan.Text = coVan.HoTen;
            txtSoDienThoai.Text = coVan.SoDienThoai;
            txtEmail.Text = coVan.Email;
            txtDiaChi.Text = coVan.DiaChi;
        }
    }
}
