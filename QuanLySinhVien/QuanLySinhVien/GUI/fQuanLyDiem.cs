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
    public partial class fQuanLyDiem : Form
    {
        private BindingSource bindingSource=new BindingSource();
        private DiemBLL diemBLL = new DiemBLL();
        public fQuanLyDiem()
        {
            InitializeComponent();
            caiDatDieuKhien();
            napDuLieuComboBox();
            LamMoi();
        }
        private void SapXepDiemTheoMaSinhVien()
        {
            diemBLL.DanhSachDiem.Sort((a, b) => string.Compare(a.MaSinhVien, b.MaSinhVien, StringComparison.CurrentCultureIgnoreCase));
        }
        private void LamMoi()
        {
            SapXepDiemTheoMaSinhVien();
            bindingSource.DataSource = null;
            bindingSource.DataSource = diemBLL.DanhSachDiem;
            dgvDiem.Refresh();
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
        private void caiDatDieuKhien()
        {

            dgvDiem.AutoGenerateColumns = false;
            bindingSource.DataSource = diemBLL.DanhSachDiem;
            dgvDiem.DataSource = bindingSource;
            if(diemBLL.DanhSachDiem.Count==0)
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
            dgvDiem.SelectionChanged += DgvDiem_SelectionChanged;
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            CboSearch_Click(sender, e);
        }
        private void CboSearch_Click(object sender, EventArgs e)
        {
            string timKiem = txtSearch.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(timKiem) || cboSearch.SelectedIndex == 0)
            {
                bindingSource.DataSource = diemBLL.DanhSachDiem;
                dgvDiem.DataSource = bindingSource;
                return;
            }
            List<Diem> ketQua = diemBLL.TimDiem(timKiem);

            bindingSource.DataSource = ketQua;
            dgvDiem.DataSource = bindingSource;
        }
        private bool kiemTraDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtDiemLop.Text))
            {
                MessageBox.Show("Điểm lớp không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiemGiuaKy.Text))
            {
                MessageBox.Show("Điểm giữa kỳ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiemThi.Text))
            {
                MessageBox.Show("Điểm giữa kỳ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (double.Parse(txtDiemLop.Text)>10 || double.Parse(txtDiemLop.Text)<0)
            {
                MessageBox.Show("Điểm lớp không được lớn hơn 10 hoặc nhỏ hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (double.Parse(txtDiemGiuaKy.Text) > 10 || double.Parse(txtDiemGiuaKy.Text) < 0)
            {
                MessageBox.Show("Điểm giữa kỳ không được lớn hơn 10 hoặc nhỏ hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (double.Parse(txtDiemThi.Text) > 10 || double.Parse(txtDiemThi.Text) < 0)
            {
                MessageBox.Show("Điểm cuối kỳ không được lớn hơn 10 hoặc nhỏ hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboTenHocKy.SelectedItem == null)
            {
                MessageBox.Show("Chưa có học kỳ nào,hãy thêm học kỳ!", "Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cboTenMonHoc.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn môn học nào,hãy nhập môn học!", "Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!kiemTraDauVao())
            {
                return;
            }
            
            if (!SinhVienDAL.DanhSachSinhVien.Any(sv => sv.MaSinhVien.Equals(txtMaSinhVien.Text.ToUpper(), StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SinhVien sinhVien = SinhVienDAL.DanhSachSinhVien.FirstOrDefault(sv => sv.MaSinhVien.Equals(txtMaSinhVien.Text.ToUpper(), StringComparison.CurrentCultureIgnoreCase));
            MonHoc monHoc=cboTenMonHoc.SelectedItem as MonHoc;
            HocKy hocKy=cboTenHocKy.SelectedItem as HocKy;

            if (sinhVien==null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Diem diem = new Diem
                (
                (int)nudPhanTramLop.Value,
                (int)nudPhanTramGiuaKy.Value,
                (int)nudPhanTramCuoiKy.Value,
                double.Parse(txtDiemLop.Text),
                double.Parse(txtDiemGiuaKy.Text),
                double.Parse(txtDiemThi.Text),
                sinhVien,
                monHoc,
                hocKy

                );
            bool ketQuaThem = diemBLL.ThemDiem(diem,sinhVien,monHoc,hocKy);
            if (ketQuaThem == false)
            {
                return;
            }
            else
            {
                MessageBox.Show("Đã thêm danh sách điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            HienXoaSua();
            LamMoi();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Diem diem=dgvDiem.CurrentRow?.DataBoundItem as Diem;
            if (!SinhVienDAL.DanhSachSinhVien.Any(sv => sv.MaSinhVien.Equals(txtMaSinhVien.Text.ToUpper(), StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SinhVien sinhVien = SinhVienDAL.DanhSachSinhVien.FirstOrDefault(sv => sv.MaSinhVien.Equals(txtMaSinhVien.Text.ToUpper(), StringComparison.CurrentCultureIgnoreCase));
            MonHoc monHoc = cboTenMonHoc.SelectedItem as MonHoc;
            HocKy hocKy=cboTenHocKy.SelectedItem as HocKy;
            if (!kiemTraDauVao())
            {
                return;
            }
            bool ketQuaSua = diemBLL.SuaDiem(diem,new Diem
                (
                (int)nudPhanTramLop.Value,
                (int)nudPhanTramGiuaKy.Value,
                (int)nudPhanTramCuoiKy.Value,
                double.Parse(txtDiemLop.Text),
                double.Parse(txtDiemGiuaKy.Text),
                double.Parse(txtDiemThi.Text),
                sinhVien,
                monHoc,
                hocKy
                ));
            if (ketQuaSua == false)
            {
                return;
            }
            else
            {
                MessageBox.Show("Đã sửa điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LamMoi();
            bindingSource.ResetBindings(false);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Diem diem = dgvDiem.CurrentRow?.DataBoundItem as Diem;
           
            if(MessageBox.Show($"Xóa điểm của sinh viên {diem.SinhVien.HoTen} - môn: {diem.MonHoc.TenMonHoc }?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ketQuaXoa = diemBLL.XoaDiem(diem);
                if (ketQuaXoa == false)
                {
                    MessageBox.Show("Xóa điểm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Xóa điểm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (diemBLL.DanhSachDiem.Count==0)
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
                DiemDAL.SaveDiem();
                MessageBox.Show("Đã lưu danh sách điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch(Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                DiemDAL.LoadDiem();
                MessageBox.Show("Đã tải danh sách điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
                if (diemBLL.DanhSachDiem.Count > 0)
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
            DiemDAL.LoadDiem();
            bindingSource.ResetBindings(false);
            ClearInput() ;
        }
        private void ClearInput()
        {
            nudPhanTramLop.Value = 10;
            nudPhanTramGiuaKy.Value = 30;
            nudPhanTramCuoiKy.Value = 60;
            txtDiemLop.Text = "0";
            txtDiemGiuaKy.Text = "0";
            txtDiemThi.Text = "0";
        }
        private void DgvDiem_SelectionChanged(object sender, EventArgs e)
        {
            Diem diem=dgvDiem.CurrentRow?.DataBoundItem as Diem;
            if(diem==null)
            {
                return;
            }
            txtMaSinhVien.Text = diem.SinhVien.MaSinhVien;
            cboTenMonHoc.SelectedValue = diem.MonHoc.MaMonHoc;
            cboTenHocKy.SelectedValue = diem.HocKy.MaHocKy;
            nudPhanTramLop.Value = diem.PhanTramLop;
            nudPhanTramGiuaKy.Value=diem.PhanTramGiuaKy;
            nudPhanTramCuoiKy.Value = diem.PhanTramCuoiKy;
            txtDiemLop.Text = diem.DiemLop.ToString();
            txtDiemGiuaKy.Text=diem.DiemGiuaKy.ToString();
            txtDiemThi.Text=diem.DiemCuoiKy.ToString() ;
        }
        private void napDuLieuComboBox()
        {
            cboTenMonHoc.DataSource = MonHocDAL.DanhSachMonHoc;
            cboTenMonHoc.DisplayMember = "TenMonHoc";
            cboTenMonHoc.ValueMember = "MaMonHoc";

            cboTenHocKy.DataSource = HocKyDAL.DanhSachHocKy;
            cboTenHocKy.DisplayMember = "TenHocKy";
            cboTenHocKy.ValueMember = "MaHocKy";
        }
        private void txtDiemLop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDiemGiuaKy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDiemThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
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

            if (MessageBox.Show("Bạn có muốn lưu dữ liệu trước khi đăng xuất?", "Lưu dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                this.Close();
                return;
            }
            try
            {
                if (loaiTK == "Giang Vien")
                {
                    DiemDAL.SaveDiem();
                    MessageBox.Show("Đã lưu danh sách điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void fQuanLyDiem_Load(object sender, EventArgs e)
        {
            DiemDAL.LoadDiem();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
    }
}
