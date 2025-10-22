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
        BindingSource bindingSource=new BindingSource();
        public fQuanLyDiem()
        {
            InitializeComponent();
            caiDatDieuKhien();
            napDuLieuComboBox();
        }
        private void caiDatDieuKhien()
        {

            dgvDiem.AutoGenerateColumns = false;
            bindingSource.DataSource = KhoDuLieu.DanhSachDiem;
            dgvDiem.DataSource = bindingSource;
            //event
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;
            btnLoad.Click += BtnLoad_Click;
            btnRefresh.Click += BtnRefresh_Click;
            dgvDiem.SelectionChanged += DgvDiem_SelectionChanged;
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
                MessageBox.Show("Vui lòng chọn sinh viên!");
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
            KhoDuLieu.DanhSachDiem.Add(diem);
            sinhVien.DanhSachDiem.Add(diem);
            monHoc.DanhSachDiem.Add(diem);
            MessageBox.Show("Đã thêm danh sách điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bindingSource.ResetBindings(false);
            ClearInput();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Diem diem=dgvDiem.CurrentRow.DataBoundItem as Diem;
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
            MessageBox.Show("đã sửa điểm");
            bindingSource.ResetBindings(false);
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Diem diem = dgvDiem.CurrentRow.DataBoundItem as Diem;
            if(diem==null ) 
            { 
                return;
            }
            if(MessageBox.Show($"Xóa điểm của sinh viên {diem.SinhVien.HoTen} - môn{diem.MonHoc.TenMonHoc }?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                diem.SinhVien.DanhSachDiem.Remove(diem);
                diem.MonHoc.DanhSachDiem.Remove(diem);
                KhoDuLieu.DanhSachDiem.Remove(diem);
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
            Diem diem=dgvDiem.CurrentRow.DataBoundItem as Diem;
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
            cboMaSinhVien.DisplayMember = "TenSinhVien";
            cboMaSinhVien.ValueMember = "MaSinhVien";
        }

    }
}
