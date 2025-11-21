using QuanLySinhVien.BLL;
using QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.GUI
{
    public partial class fQuanLyHocKy : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private HocKyBLL hocKyBLL = new HocKyBLL();
        public fQuanLyHocKy()
        {
            InitializeComponent();
            CaiDatDieuKhien();
            LamMoi();
        }
        private void SapXepHocKyTheoMa()
        {
            hocKyBLL.DanhSachHocKy.Sort((a, b) => string.Compare(a.MaHocKy, b.MaHocKy, StringComparison.CurrentCultureIgnoreCase));
        }
        private void CaiDatDieuKhien()
        {
            dgvHocKy.AutoGenerateColumns = false;
            dgvHocKy.DataSource = bindingSource;
            cboSearch.SelectedIndex = 0;
            if(hocKyBLL.DanhSachHocKy.Count == 0)
            {
                AnXoaSua();
            }
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;
            btnLoad.Click += BtnLoad_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSearch.Click += BtnSearch_Click;
            cboSearch.Click += CboSearch_Click;
            dgvHocKy.SelectionChanged += DgvHocKy_SelectionChanged;
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
                bindingSource.DataSource = hocKyBLL.DanhSachHocKy;
                dgvHocKy.DataSource = bindingSource;
                return;
            }
            List<HocKy> ketQua = hocKyBLL.TimKiemHocKy(timKiem);
            bindingSource.DataSource = ketQua;
            dgvHocKy.DataSource = bindingSource;
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
        private bool KiemTraDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtMaHocKy.Text))
            {
                MessageBox.Show("Không được để mã học kỳ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenHocKy.Text))
            {
                MessageBox.Show("Không được để tên học kỳ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNamHoc.Text))
            {
                MessageBox.Show("Không được để năm học trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void ClearInput()
        {
            txtTenHocKy.Text = "";
            txtMaHocKy.Text = "";
            txtNamHoc.Text = "";
        }
        private void LamMoi()
        {
            SapXepHocKyTheoMa();
            bindingSource.DataSource = null;
            bindingSource.DataSource = hocKyBLL.DanhSachHocKy;
            dgvHocKy.Refresh();
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTraDauVao())
            {
                return;
            }
            HocKy hocKy = new HocKy
                (
                txtMaHocKy.Text.ToUpper(),
                txtTenHocKy.Text,
                int.Parse(txtNamHoc.Text),
                new List<Diem>()
                );
            bool ketQuaThem = hocKyBLL.ThemHocKy(hocKy);
            if (ketQuaThem == false)
            {
                MessageBox.Show("Mã học kỳ đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Đã thêm học kỳ mới!");
            }
            HienXoaSua();
            LamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvHocKy.CurrentRow == null)
            {
                return;
            }
            HocKy hocKyCu = dgvHocKy.CurrentRow?.DataBoundItem as HocKy;
            if (!KiemTraDauVao())
            {
                return;
            }
            HocKy hocKyMoi = new HocKy
                (
                txtMaHocKy.Text.ToUpper(),
                txtTenHocKy.Text,
                int.Parse(txtNamHoc.Text),
                hocKyCu.DanhSachDiem
                );
            bool ketQuaSua = hocKyBLL.SuaHocKy(hocKyCu, hocKyMoi);
            if (ketQuaSua == false)
            {
                MessageBox.Show("Mã học kỳ đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Đã sửa học kỳ!");
            }
            LamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvHocKy.CurrentRow == null)
            {
                return;
            }
            HocKy hocKy = dgvHocKy.CurrentRow?.DataBoundItem as HocKy;
            if (MessageBox.Show($"Xóa {hocKy.TenHocKy}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool ketQuaXoa = hocKyBLL.XoaHocKy(hocKy);
                if (ketQuaXoa == false)
                {
                    MessageBox.Show("Xóa học kỳ thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Đã xóa học kỳ!");
                }
                LamMoi();
                bindingSource.ResetBindings(false);
                ClearInput();
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.HocKyDAL.SaveHocKy();
                MessageBox.Show("Đã lưu danh sách học kỳ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu danh sách học kỳ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.HocKyDAL.LoadHocKy();
                MessageBox.Show("Đã tải lại danh sách học kỳ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
                if (hocKyBLL.DanhSachHocKy.Count > 0)
                {
                    HienXoaSua();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học kỳ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void DgvHocKy_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHocKy.CurrentRow == null)
            {
                return;
            }
            HocKy hocKy = dgvHocKy.CurrentRow?.DataBoundItem as HocKy;
            if (hocKy != null)
            {
                txtMaHocKy.Text = hocKy.MaHocKy;
                txtTenHocKy.Text = hocKy.TenHocKy;
                txtNamHoc.Text = hocKy.NamHoc.ToString();
            }
        }

        private void txtNamHoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
