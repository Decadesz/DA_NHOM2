using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySinhVien.BLL;
using QuanLySinhVien.DAL;

namespace QuanLySinhVien
{
    public partial class fCoVanHocTap : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private CoVanHocTapBLL coVanBLL = new CoVanHocTapBLL();
        public fCoVanHocTap()
        {
            InitializeComponent();
            CaiDatDieuKhien();
            LamMoi();
        }
        private void SapXepCoVanTheoMa()
        {
            coVanBLL.DanhSachCoVan.Sort((a, b) => string.Compare(a.MaCoVan, b.MaCoVan, StringComparison.CurrentCultureIgnoreCase));
        }
        private void LamMoi()
        {
            SapXepCoVanTheoMa();
            bindingSource.DataSource = null;
            bindingSource.DataSource = coVanBLL.DanhSachCoVan;
            dgvCoVan.Refresh();
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
                bindingSource.DataSource = coVanBLL.DanhSachCoVan;
                dgvCoVan.DataSource = bindingSource;
                return;
            }
            List<CoVanHocTap> ketQua = coVanBLL.TimKiemCoVan(timKiem);
            bindingSource.DataSource = ketQua;
            dgvCoVan.DataSource = bindingSource;
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

            dgvCoVan.AutoGenerateColumns = false;
            bindingSource.DataSource = coVanBLL.DanhSachCoVan;
            dgvCoVan.DataSource = bindingSource;
            if(coVanBLL.DanhSachCoVan.Count==0)
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
            dgvCoVan.SelectionChanged += DgvCoVan_SelectionChanged;
            cboSearch.Click += CboSearch_Click;
            btnSearch.Click += BtnSearch_Click;
            cboSearch.SelectedIndex = 0;
        }
        private bool KiemTraDauVao()
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
            if ((txtSoDienThoai.Text).Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có 10 chữ số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(!KiemTraDauVao())
            {
                return;
            }
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            CoVanHocTap coVan = new CoVanHocTap
                (
                txtMaCoVan.Text.ToUpper(),
                txtTenCoVan.Text,
                dtpNgaySinh.Value,
                gioiTinh,
                txtDiaChi.Text,
                txtSoDienThoai.Text,
                txtEmail.Text,
                new List<SinhVien>()
                );
            bool ketQuaThem=coVanBLL.ThemCoVan(coVan);
            if (ketQuaThem==false)
            {
                MessageBox.Show("Mã cố vấn đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Đã thêm cố vấn mới!");
            }
            HienXoaSua();
            LamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            CoVanHocTap coVan=dgvCoVan.CurrentRow?.DataBoundItem as CoVanHocTap;
            if(!KiemTraDauVao())
            {
                return;
            }
            bool ketQuaSua= coVanBLL.SuaCoVan(coVan, new CoVanHocTap
                (
                txtMaCoVan.Text.ToUpper(),
                txtTenCoVan.Text,
                dtpNgaySinh.Value,
                radNam.Checked ? "Nam" : "Nữ",
                txtDiaChi.Text,
                txtSoDienThoai.Text,
                txtEmail.Text,
                coVan.DanhSachSinhVien
                ));
            if (ketQuaSua==false)
            {
                MessageBox.Show("Mã cố vấn đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Đã sửa thông tin cố vấn!" ,"Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            bindingSource.ResetBindings(false);

        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            CoVanHocTap coVan=dgvCoVan.CurrentRow?.DataBoundItem as CoVanHocTap;
            
            if (MessageBox.Show($"Xóa {coVan.HoTen }?","Xác nhận",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                bool ketQuaXoa = coVanBLL.XoaCoVan(coVan);
                if(!ketQuaXoa)
                {
                    MessageBox.Show("Xóa cố vấn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Đã xóa cố vấn!");
                    LamMoi();
                }
                if (coVanBLL.DanhSachCoVan.Count == 0)
                {
                    AnXoaSua();
                }
                bindingSource.ResetBindings(false);
            }    
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CoVanHocTapDAL.SaveCoVan();
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
                CoVanHocTapDAL.LoadCoVan();
                MessageBox.Show("Đã tải danh sách cố vấn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
                if (coVanBLL.DanhSachCoVan.Count > 0)
                {
                    HienXoaSua();
                }
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
        private void ClearInput()
        {
            txtMaCoVan.Text = "";
            txtTenCoVan.Text = "";
            txtSoDienThoai.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
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

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
