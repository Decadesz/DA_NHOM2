namespace QuanLySinhVien
{
    partial class fBangDiemSV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtMaSinhVien = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvDiemTrungBinhHocTap = new System.Windows.Forms.DataGridView();
            this.dgvBangDiem = new System.Windows.Forms.DataGridView();
            this.MaSinhVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSinhVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaMonHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMonHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTinChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemTrungBinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KetQua = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemTrungBinhHocTap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTinChiTichLuy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XepLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemTrungBinhHocTap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBangDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtMaSinhVien);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1289, 108);
            this.panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(211, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 103;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtMaSinhVien
            // 
            this.txtMaSinhVien.Location = new System.Drawing.Point(92, 7);
            this.txtMaSinhVien.Name = "txtMaSinhVien";
            this.txtMaSinhVien.Size = new System.Drawing.Size(100, 20);
            this.txtMaSinhVien.TabIndex = 89;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(501, 109);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 88;
            this.btnRefresh.Text = "Tải lại";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(339, 109);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 87;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(417, 109);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 86;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(258, 109);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 85;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Mã sinh viên";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvDiemTrungBinhHocTap);
            this.panel2.Controls.Add(this.dgvBangDiem);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1289, 342);
            this.panel2.TabIndex = 1;
            // 
            // dgvDiemTrungBinhHocTap
            // 
            this.dgvDiemTrungBinhHocTap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiemTrungBinhHocTap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DiemTrungBinhHocTap,
            this.SoTinChiTichLuy,
            this.XepLoai});
            this.dgvDiemTrungBinhHocTap.Location = new System.Drawing.Point(929, 1);
            this.dgvDiemTrungBinhHocTap.Name = "dgvDiemTrungBinhHocTap";
            this.dgvDiemTrungBinhHocTap.ReadOnly = true;
            this.dgvDiemTrungBinhHocTap.Size = new System.Drawing.Size(337, 150);
            this.dgvDiemTrungBinhHocTap.TabIndex = 3;
            // 
            // dgvBangDiem
            // 
            this.dgvBangDiem.AllowUserToResizeColumns = false;
            this.dgvBangDiem.AllowUserToResizeRows = false;
            this.dgvBangDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBangDiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSinhVien,
            this.TenSinhVien,
            this.MaMonHoc,
            this.TenMonHoc,
            this.SoTiet,
            this.SoTinChi,
            this.DiemTrungBinh,
            this.Loai,
            this.KetQua});
            this.dgvBangDiem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvBangDiem.Location = new System.Drawing.Point(0, 3);
            this.dgvBangDiem.MultiSelect = false;
            this.dgvBangDiem.Name = "dgvBangDiem";
            this.dgvBangDiem.ReadOnly = true;
            this.dgvBangDiem.RowHeadersVisible = false;
            this.dgvBangDiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBangDiem.Size = new System.Drawing.Size(896, 339);
            this.dgvBangDiem.TabIndex = 2;
            this.dgvBangDiem.TabStop = false;
            // 
            // MaSinhVien
            // 
            this.MaSinhVien.DataPropertyName = "MaSinhVien";
            this.MaSinhVien.HeaderText = "Mã SV";
            this.MaSinhVien.Name = "MaSinhVien";
            this.MaSinhVien.ReadOnly = true;
            // 
            // TenSinhVien
            // 
            this.TenSinhVien.DataPropertyName = "TenSinhVien";
            this.TenSinhVien.HeaderText = "Tên sinh viên";
            this.TenSinhVien.Name = "TenSinhVien";
            this.TenSinhVien.ReadOnly = true;
            // 
            // MaMonHoc
            // 
            this.MaMonHoc.DataPropertyName = "MaMonHoc";
            this.MaMonHoc.HeaderText = "Mã môn học";
            this.MaMonHoc.Name = "MaMonHoc";
            this.MaMonHoc.ReadOnly = true;
            // 
            // TenMonHoc
            // 
            this.TenMonHoc.DataPropertyName = "TenMonHoc";
            this.TenMonHoc.HeaderText = "Tên môn học";
            this.TenMonHoc.Name = "TenMonHoc";
            this.TenMonHoc.ReadOnly = true;
            // 
            // SoTiet
            // 
            this.SoTiet.DataPropertyName = "SoTiet";
            this.SoTiet.HeaderText = "Số tiết";
            this.SoTiet.Name = "SoTiet";
            this.SoTiet.ReadOnly = true;
            // 
            // SoTinChi
            // 
            this.SoTinChi.DataPropertyName = "SoTinChi";
            this.SoTinChi.HeaderText = "Số tín chỉ";
            this.SoTinChi.Name = "SoTinChi";
            this.SoTinChi.ReadOnly = true;
            // 
            // DiemTrungBinh
            // 
            this.DiemTrungBinh.DataPropertyName = "DiemTrungBinh";
            this.DiemTrungBinh.HeaderText = "Điểm tổng kết";
            this.DiemTrungBinh.Name = "DiemTrungBinh";
            this.DiemTrungBinh.ReadOnly = true;
            // 
            // Loai
            // 
            this.Loai.DataPropertyName = "Loai";
            this.Loai.HeaderText = "Điểm chữ";
            this.Loai.Name = "Loai";
            this.Loai.ReadOnly = true;
            // 
            // KetQua
            // 
            this.KetQua.DataPropertyName = "KetQua";
            this.KetQua.HeaderText = "Kết quả";
            this.KetQua.Name = "KetQua";
            this.KetQua.ReadOnly = true;
            // 
            // DiemTrungBinhHocTap
            // 
            this.DiemTrungBinhHocTap.DataPropertyName = "DiemTrungBinhHocTap";
            this.DiemTrungBinhHocTap.HeaderText = "Điểm trung bình học tập";
            this.DiemTrungBinhHocTap.Name = "DiemTrungBinhHocTap";
            this.DiemTrungBinhHocTap.ReadOnly = true;
            // 
            // SoTinChiTichLuy
            // 
            this.SoTinChiTichLuy.DataPropertyName = "SoTinChiTichLuy";
            this.SoTinChiTichLuy.HeaderText = "Số tín chỉ đã tích lũy";
            this.SoTinChiTichLuy.Name = "SoTinChiTichLuy";
            this.SoTinChiTichLuy.ReadOnly = true;
            // 
            // XepLoai
            // 
            this.XepLoai.DataPropertyName = "XepLoai";
            this.XepLoai.HeaderText = "Xếp loại";
            this.XepLoai.Name = "XepLoai";
            this.XepLoai.ReadOnly = true;
            // 
            // fBangDiemSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "fBangDiemSV";
            this.Text = "Bảng điểm sinh viên";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemTrungBinhHocTap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBangDiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBangDiem;
        private System.Windows.Forms.TextBox txtMaSinhVien;
        private System.Windows.Forms.DataGridView dgvDiemTrungBinhHocTap;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaSinhVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenSinhVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMonHoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMonHoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTinChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemTrungBinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loai;
        private System.Windows.Forms.DataGridViewTextBoxColumn KetQua;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemTrungBinhHocTap;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTinChiTichLuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn XepLoai;
    }
}