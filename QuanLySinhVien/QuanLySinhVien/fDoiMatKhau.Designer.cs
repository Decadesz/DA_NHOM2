namespace QuanLySinhVien
{
    partial class fDoiMatKhau
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
            this.txtPassNew = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txVerifyPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResetPass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPassNew
            // 
            this.txtPassNew.Location = new System.Drawing.Point(15, 79);
            this.txtPassNew.Name = "txtPassNew";
            this.txtPassNew.Size = new System.Drawing.Size(172, 20);
            this.txtPassNew.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Mật khẩu mới:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(15, 25);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(172, 20);
            this.txtPass.TabIndex = 11;
            this.txtPass.Text = "******";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Mật khẩu cũ:";
            // 
            // txVerifyPass
            // 
            this.txVerifyPass.Location = new System.Drawing.Point(15, 121);
            this.txVerifyPass.Name = "txVerifyPass";
            this.txVerifyPass.Size = new System.Drawing.Size(172, 20);
            this.txVerifyPass.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = " Xác nhận mật khẩu mới:";
            // 
            // btnResetPass
            // 
            this.btnResetPass.Location = new System.Drawing.Point(15, 157);
            this.btnResetPass.Name = "btnResetPass";
            this.btnResetPass.Size = new System.Drawing.Size(75, 23);
            this.btnResetPass.TabIndex = 16;
            this.btnResetPass.Text = "Cập nhật";
            this.btnResetPass.UseVisualStyleBackColor = true;
            // 
            // fDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 214);
            this.Controls.Add(this.btnResetPass);
            this.Controls.Add(this.txVerifyPass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassNew);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.label2);
            this.Name = "fDoiMatKhau";
            this.Text = "Đổi mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassNew;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txVerifyPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResetPass;
    }
}