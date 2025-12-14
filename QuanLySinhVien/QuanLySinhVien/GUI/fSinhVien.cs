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
    using QuanLySinhVien.BLL;
    using QuanLySinhVien.DAL;
    using QuanLySinhVien.GUI;
namespace QuanLySinhVien
{
    public partial class fSinhVien : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private SinhVienBLL sinhVienBLL = new SinhVienBLL();
        public fSinhVien()
        {
            InitializeComponent();
            CaiDatDieuKhien();
            napDuLieuComboBox();
            LamMoi();

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

            dgvSinhVien.AutoGenerateColumns = false;
            bindingSource.DataSource = sinhVienBLL.DanhSachSinhVien;
            dgvSinhVien.DataSource = bindingSource;
            if (sinhVienBLL.DanhSachSinhVien.Count == 0)
            {
                AnXoaSua();
            }
            cboSearch.SelectedIndex = 0;
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
        }
        private void SapXepSinhVienTheoTen()
        {
            sinhVienBLL.DanhSachSinhVien.Sort((a, b) =>
            {
                string tenA = LayTenCuoi(a.HoTen);
                string tenB = LayTenCuoi(b.HoTen);

                int kq = string.Compare(tenA, tenB, StringComparison.OrdinalIgnoreCase);
                if (kq != 0)
                    return kq;
                //Neu ten giong nhau thi so sanh ho va ten dem
                return string.Compare(a.HoTen, b.HoTen, StringComparison.OrdinalIgnoreCase);
            });
        }

        private string LayTenCuoi(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
                return "";

            string[] parts = hoTen.Trim().Split(' ');
            return parts[parts.Length - 1];
        }
        private void SapXepSinhVienTheoLop()
        {
            //phai dung dinh dang D23_TH01 khong duoc xai D23_TH1
            sinhVienBLL.DanhSachSinhVien.Sort((a, b) => string.Compare(a.TenLop, b.TenLop, StringComparison.Ordinal));
        }
        private void SapXepSinhVienTheoMa()
        {
            sinhVienBLL.DanhSachSinhVien.Sort((a, b) => string.Compare(a.MaSinhVien, b.MaSinhVien, StringComparison.Ordinal));
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
                bindingSource.DataSource = sinhVienBLL.DanhSachSinhVien;
                dgvSinhVien.DataSource = bindingSource;
                return;
            }
            List<SinhVien> ketQua = sinhVienBLL.TimKiemSinhVien(timKiem);
            bindingSource.DataSource = ketQua;
            dgvSinhVien.DataSource = bindingSource;
        }
        private bool KiemTraDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtMaSinhVien.Text))
            {
                MessageBox.Show("Mã sinh viên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenSinhVien.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dtpNgaySinh.Value >= DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dtpNgayNhapHoc.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày nhập học không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((txtSoDienThoai.Text).Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có 10 chữ số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!KiemTraDauVao())
            {
                return;
            }
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            //ep kieu an toan
            Lop lop = cboTenLop.SelectedItem as Lop;
            CoVanHocTap coVan = cboTenCoVan.SelectedItem as CoVanHocTap;
            if (lop != null)
            {
                if (lop.SoLuong >= Lop.siSoToiDa)
                {
                    MessageBox.Show("Lớp đã đạt sĩ số tối đa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            SinhVien sinhVien = new SinhVien(
                txtMaSinhVien.Text.ToUpper(),
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
            bool ketQuaThem = sinhVienBLL.ThemSinhVien(sinhVien);
            if (ketQuaThem == false)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (lop != null)
                {
                    lop.DanhSachSinhVien.Add(sinhVien);
                }
                if (coVan != null)
                {
                    coVan.DanhSachSinhVien.Add(sinhVien);
                }
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            HienXoaSua();
            LamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = dgvSinhVien.CurrentRow?.DataBoundItem as SinhVien;
            if (!KiemTraDauVao())
            {
                return;
            }
            Lop lopCu = sinhVien.Lop;
            CoVanHocTap coVanMoi = cboTenCoVan.SelectedItem as CoVanHocTap;
            Lop lopMoi = cboTenLop.SelectedItem as Lop;
            if (lopMoi != null && lopMoi.SoLuong >= Lop.siSoToiDa && lopMoi != lopCu)
            {
                MessageBox.Show("Lớp đã đạt sĩ số tối đa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool ketQuaSua = sinhVienBLL.SuaSinhVien(sinhVien, new SinhVien(
                txtMaSinhVien.Text.ToUpper(),
                txtTenSinhVien.Text,
                radNam.Checked ? "Nam" : "Nữ",
                dtpNgaySinh.Value,
                dtpNgayNhapHoc.Value,
                txtDiaChi.Text,
                txtSoDienThoai.Text,
                lopMoi,
                coVanMoi,
                sinhVien.DanhSachDiem
                ));
            if (ketQuaSua == false)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Sửa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            LamMoi();
            bindingSource.ResetBindings(false);

        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = dgvSinhVien.CurrentRow?.DataBoundItem as SinhVien;

            if (MessageBox.Show($"Bạn có chắc muốn xóa sinh viên {sinhVien.HoTen}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ketQuaXoa = sinhVienBLL.XoaSinhVien(sinhVien);
                if (ketQuaXoa == false)
                {
                    MessageBox.Show("Xóa sinh viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {

                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (sinhVienBLL.DanhSachSinhVien.Count == 0)
                {
                    AnXoaSua();
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
                SinhVienDAL.SaveSinhVien();
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
                SinhVienDAL.LoadSinhVien();
                DiemDAL.LoadDiem();
                LamMoi();
                napDuLieuComboBox();
                MessageBox.Show("Đã tải danh sách sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (sinhVienBLL.DanhSachSinhVien.Count > 0)
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
            napDuLieuComboBox();
            SinhVienDAL.LoadSinhVien();
            DiemDAL.LoadDiem();
            LamMoi();
            bindingSource.ResetBindings(false);
        }
        private void LamMoi()
        {
            //SapXepSinhVienTheoMa();
            SapXepSinhVienTheoTen();
            SapXepSinhVienTheoLop();
            bindingSource.DataSource = null;
            bindingSource.DataSource = sinhVienBLL.DanhSachSinhVien;
            dgvSinhVien.Refresh();
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

            cboTenLop.DataSource = null;
            cboTenLop.DataSource = LopDAL.DanhSachLop;
            cboTenLop.DisplayMember = "TenLop";
            cboTenLop.ValueMember = "MaLop";


            cboTenCoVan.DataSource = null;
            cboTenCoVan.DataSource = CoVanHocTapDAL.DanhSachCoVan;
            cboTenCoVan.DisplayMember = "HoTen";
            cboTenCoVan.ValueMember = "MaCoVan";
        }
        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void quanLyMonHocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyMonHoc f = new fQuanLyMonHoc();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quanLyLopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fQuanLyLop f = new fQuanLyLop();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quanLyCoVanHocTapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fCoVanHocTap f = new fCoVanHocTap();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quanLyDiemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyDiem f = new fQuanLyDiem();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quanLyTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyTaiKhoan f = new fQuanLyTaiKhoan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quanLyHocKyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyHocKy f = new fQuanLyHocKy();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void bangDiemSinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBangDiemSV f = new fBangDiemSV();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void thongKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fThongKe f = new fThongKe();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void doiMatKhauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDoiMatKhau f = new fDoiMatKhau();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void thongTinTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fThongTinChiTiet f = new fThongTinChiTiet();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void dangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string loaiTK = TaiKhoanDangNhapDAL.TaiKhoanHienTai.LoaiTaiKhoanDangNhap;

            if (MessageBox.Show("Bạn có muốn lưu dữ liệu trước khi đăng xuất?","Lưu dữ liệu",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
            {
                this.Close();
                return;
            }
            try
            {
                if (loaiTK == "Phong Dao Tao")
                {
                    HocKyDAL.SaveHocKy();
                    CoVanHocTapDAL.SaveCoVan();
                    LopDAL.SaveLop();
                    MonHocDAL.SaveMonHoc();
                    SinhVienDAL.SaveSinhVien();
                    DiemDAL.SaveDiem();
                    TaiKhoanDangNhapDAL.SaveTaiKhoan();

                    MessageBox.Show("Đã lưu toàn bộ dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void fSinhVien_Load(object sender, EventArgs e)
        {
            LamMoi();
        }
    }
}
