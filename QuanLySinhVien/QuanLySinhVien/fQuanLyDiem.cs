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
    public partial class fQuanLyDiem : Form
    {
        private BindingSource bindingSource=new BindingSource();
        public fQuanLyDiem()
        {
            InitializeComponent();
            caiDatDieuKhien();
            napDuLieuComboBox();
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

            dgvDiem.AutoGenerateColumns = false;
            bindingSource.DataSource = KhoDuLieu.DanhSachDiem;
            dgvDiem.DataSource = bindingSource;
            if(KhoDuLieu.DanhSachDiem.Count==0)
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
            string timKiem = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(timKiem) || cboSearch.SelectedIndex == 0)
            {
                bindingSource.DataSource = KhoDuLieu.DanhSachDiem;
                dgvDiem.DataSource = bindingSource;
                return;
            }
            List<Diem> ketQua = new List<Diem>();
            switch (cboSearch.SelectedItem.ToString())
            {
                case "Mã sinh viên":
                    ketQua = KhoDuLieu.DanhSachDiem.Where(d => !string.IsNullOrEmpty(d.MaSinhVien) && d.MaSinhVien.ToLower().Contains(timKiem)).ToList();
                    break;
                case "Tên môn học":
                    ketQua = KhoDuLieu.DanhSachDiem.Where(d => !string.IsNullOrEmpty(d.TenMonHoc) && d.TenMonHoc.ToLower().Contains(timKiem)).ToList();
                    break;
                default:
                    ketQua = KhoDuLieu.DanhSachDiem;
                    break;
            }
            bindingSource.DataSource = ketQua;
            dgvDiem.DataSource = bindingSource;
        }
        private void ClearInput()
        {
            nudPhanTramLop.Value = 10;
            nudPhanTramGiuaKy.Value = 30;
            nudPhanTramCuoiKy.Value = 60;
            txtDiemLop.Text = "";
            txtDiemGiuaKy.Text = "";
            txtDiemThi.Text = "";
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien=cboMaSinhVien.SelectedItem as SinhVien;
            MonHoc monHoc=cboMaMonHoc.SelectedItem as MonHoc;
            if(sinhVien==null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(monHoc==null)
            {
                MessageBox.Show("Vui lòng chọn môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Diem diem = new Diem
                (
                (int)nudPhanTramLop.Value,
                (int)nudPhanTramGiuaKy.Value,
                (int)nudPhanTramCuoiKy.Value,
                double.Parse(txtDiemLop.Text),
                double.Parse(txtDiemGiuaKy.Text),
                double.Parse(txtDiemThi.Text)
                );
            //gán lại điểm của sinh viên nào và điểm môn nào để xài trong datagridview
            diem.SinhVien = sinhVien;
            diem.MonHoc = monHoc;
            KhoDuLieu.DanhSachDiem.Add(diem);
            sinhVien.DanhSachDiem.Add(diem);
            monHoc.DanhSachDiem.Add(diem);
            MessageBox.Show("Đã thêm danh sách điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hienXoaSua();
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Diem diem=dgvDiem.CurrentRow?.DataBoundItem as Diem;
            if(diem==null)
            {
                return;
            }
            diem.PhanTramLop = (int)nudPhanTramLop.Value;
            diem.PhanTramGiuaKy= (int)nudPhanTramGiuaKy .Value;
            diem.PhanTramCuoiKy=(int)nudPhanTramCuoiKy .Value;
            diem.DiemLop=double.Parse(txtDiemLop.Text);
            diem.DiemGiuaKy=double.Parse(txtDiemGiuaKy.Text);
            diem.DiemCuoiKy=double.Parse(txtDiemThi.Text);
            MessageBox.Show("đã sửa điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bindingSource.ResetBindings(false);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Diem diem = dgvDiem.CurrentRow?.DataBoundItem as Diem;
            if(diem==null ) 
            { 
                return;
            }
            if(MessageBox.Show($"Xóa điểm của sinh viên {diem.SinhVien.HoTen} - môn{diem.MonHoc.TenMonHoc }?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                diem.SinhVien.DanhSachDiem.Remove(diem);
                diem.MonHoc.DanhSachDiem.Remove(diem);
                KhoDuLieu.DanhSachDiem.Remove(diem);
                MessageBox.Show("Xóa điểm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (KhoDuLieu.DanhSachDiem.Count==0)
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
                KhoDuLieu.SaveDiem();
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
                KhoDuLieu.SaveDiem();
                MessageBox.Show("Đã tải danh sách điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            bindingSource.ResetBindings(false);
            ClearInput() ;
        }
        private void DgvDiem_SelectionChanged(object sender, EventArgs e)
        {
            Diem diem=dgvDiem.CurrentRow?.DataBoundItem as Diem;
            if(diem==null)
            {
                return;
            }
            cboMaSinhVien.SelectedItem = diem.SinhVien;
            cboMaMonHoc.SelectedItem = diem.MonHoc;
            nudPhanTramLop.Value = diem.PhanTramLop;
            nudPhanTramGiuaKy.Value=diem.PhanTramGiuaKy;
            nudPhanTramCuoiKy.Value = diem.PhanTramCuoiKy;
            txtDiemLop.Text = diem.DiemLop.ToString();
            txtDiemGiuaKy.Text=diem.DiemGiuaKy.ToString();
            txtDiemThi.Text=diem.DiemCuoiKy.ToString() ;
        }
        private void napDuLieuComboBox()
        {
            cboMaMonHoc.DataSource = KhoDuLieu.DanhSachMonHoc;
            cboMaMonHoc.DisplayMember = "TenMonHoc";
            cboMaMonHoc.ValueMember = "MaMonHoc";

            cboMaSinhVien.DataSource = KhoDuLieu.DanhSachSinhVien;
            cboMaSinhVien.DisplayMember = "MaSinhVien";
            cboMaSinhVien.ValueMember = "MaSinhVien";
        }

    }
}
