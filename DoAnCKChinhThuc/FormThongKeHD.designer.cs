namespace DoAnCKChinhThuc
{
    partial class FormThongKeHD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormThongKeHD));
            this.panel11 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
            this.dtgThongTinTK = new System.Windows.Forms.DataGridView();
            this.MAHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgXuatHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOLUONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GIAMGIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnTKSPBanNhieu = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnXemTKDoanhThu = new System.Windows.Forms.Button();
            this.panel11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgThongTinTK)).BeginInit();
            this.SuspendLayout();
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.groupBox1);
            this.panel11.Location = new System.Drawing.Point(27, 31);
            this.panel11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1335, 770);
            this.panel11.TabIndex = 40;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpNgayKT);
            this.groupBox1.Controls.Add(this.dtpNgayBD);
            this.groupBox1.Controls.Add(this.dtgThongTinTK);
            this.groupBox1.Controls.Add(this.btnTKSPBanNhieu);
            this.groupBox1.Controls.Add(this.btnReport);
            this.groupBox1.Controls.Add(this.btnXemTKDoanhThu);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(24, 25);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1297, 729);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê dịch vụ doanh thu";
            // 
            // dtpNgayKT
            // 
            this.dtpNgayKT.CustomFormat = "yyyy-MM-dd";
            this.dtpNgayKT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKT.Location = new System.Drawing.Point(246, 100);
            this.dtpNgayKT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpNgayKT.Name = "dtpNgayKT";
            this.dtpNgayKT.Size = new System.Drawing.Size(224, 30);
            this.dtpNgayKT.TabIndex = 43;
            // 
            // dtpNgayBD
            // 
            this.dtpNgayBD.CustomFormat = "yyyy-MM-dd";
            this.dtpNgayBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBD.Location = new System.Drawing.Point(246, 49);
            this.dtpNgayBD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpNgayBD.Name = "dtpNgayBD";
            this.dtpNgayBD.Size = new System.Drawing.Size(224, 30);
            this.dtpNgayBD.TabIndex = 42;
            // 
            // dtgThongTinTK
            // 
            this.dtgThongTinTK.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgThongTinTK.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgThongTinTK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgThongTinTK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MAHD,
            this.NgXuatHD,
            this.TENHH,
            this.MAHH,
            this.MANV,
            this.SOLUONG,
            this.GIAMGIA,
            this.ThanhTien});
            this.dtgThongTinTK.Location = new System.Drawing.Point(56, 245);
            this.dtgThongTinTK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgThongTinTK.Name = "dtgThongTinTK";
            this.dtgThongTinTK.RowHeadersWidth = 51;
            this.dtgThongTinTK.RowTemplate.Height = 24;
            this.dtgThongTinTK.Size = new System.Drawing.Size(1210, 454);
            this.dtgThongTinTK.TabIndex = 41;
            // 
            // MAHD
            // 
            this.MAHD.DataPropertyName = "MaHDBH";
            this.MAHD.HeaderText = "Mã hóa đơn";
            this.MAHD.MinimumWidth = 6;
            this.MAHD.Name = "MAHD";
            // 
            // NgXuatHD
            // 
            this.NgXuatHD.DataPropertyName = "NgayXuatHD";
            this.NgXuatHD.HeaderText = "Ngày xuất hóa đơn";
            this.NgXuatHD.MinimumWidth = 6;
            this.NgXuatHD.Name = "NgXuatHD";
            // 
            // TENHH
            // 
            this.TENHH.DataPropertyName = "TenHH";
            this.TENHH.HeaderText = "Tên hàng hóa";
            this.TENHH.MinimumWidth = 6;
            this.TENHH.Name = "TENHH";
            // 
            // MAHH
            // 
            this.MAHH.DataPropertyName = "MaHH";
            this.MAHH.HeaderText = "Mã hàng hóa";
            this.MAHH.MinimumWidth = 6;
            this.MAHH.Name = "MAHH";
            // 
            // MANV
            // 
            this.MANV.DataPropertyName = "MaNV";
            this.MANV.HeaderText = "Mã nhân viên";
            this.MANV.MinimumWidth = 6;
            this.MANV.Name = "MANV";
            // 
            // SOLUONG
            // 
            this.SOLUONG.DataPropertyName = "SoLuong";
            this.SOLUONG.HeaderText = "Số lượng";
            this.SOLUONG.MinimumWidth = 6;
            this.SOLUONG.Name = "SOLUONG";
            // 
            // GIAMGIA
            // 
            this.GIAMGIA.DataPropertyName = "GiamGia";
            this.GIAMGIA.HeaderText = "Giảm giá";
            this.GIAMGIA.MinimumWidth = 6;
            this.GIAMGIA.Name = "GIAMGIA";
            // 
            // ThanhTien
            // 
            this.ThanhTien.DataPropertyName = "ThanhTien";
            this.ThanhTien.HeaderText = "Thành Tiền";
            this.ThanhTien.MinimumWidth = 6;
            this.ThanhTien.Name = "ThanhTien";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(51, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(192, 25);
            this.label12.TabIndex = 1;
            this.label12.Text = "Chọn ngày kết thúc :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(51, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(189, 25);
            this.label11.TabIndex = 0;
            this.label11.Text = "Chọn ngày bắt đầu :";
            // 
            // btnTKSPBanNhieu
            // 
            this.btnTKSPBanNhieu.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnTKSPBanNhieu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTKSPBanNhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTKSPBanNhieu.ForeColor = System.Drawing.Color.White;
            this.btnTKSPBanNhieu.Image = ((System.Drawing.Image)(resources.GetObject("btnTKSPBanNhieu.Image")));
            this.btnTKSPBanNhieu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTKSPBanNhieu.Location = new System.Drawing.Point(464, 155);
            this.btnTKSPBanNhieu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTKSPBanNhieu.Name = "btnTKSPBanNhieu";
            this.btnTKSPBanNhieu.Size = new System.Drawing.Size(394, 56);
            this.btnTKSPBanNhieu.TabIndex = 6;
            this.btnTKSPBanNhieu.Text = "Xem sản phẩm bán nhiều nhất";
            this.btnTKSPBanNhieu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTKSPBanNhieu.UseVisualStyleBackColor = false;
            this.btnTKSPBanNhieu.Click += new System.EventHandler(this.btnTKSPBanNhieu_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Image = global::DoAnCKChinhThuc.Properties.Resources.icons8_report_35;
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(872, 155);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(394, 56);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "Xuất doanh thu (Report)";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnXemTKDoanhThu
            // 
            this.btnXemTKDoanhThu.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnXemTKDoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnXemTKDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemTKDoanhThu.ForeColor = System.Drawing.Color.White;
            this.btnXemTKDoanhThu.Image = ((System.Drawing.Image)(resources.GetObject("btnXemTKDoanhThu.Image")));
            this.btnXemTKDoanhThu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemTKDoanhThu.Location = new System.Drawing.Point(56, 155);
            this.btnXemTKDoanhThu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXemTKDoanhThu.Name = "btnXemTKDoanhThu";
            this.btnXemTKDoanhThu.Padding = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.btnXemTKDoanhThu.Size = new System.Drawing.Size(394, 56);
            this.btnXemTKDoanhThu.TabIndex = 5;
            this.btnXemTKDoanhThu.Text = "Xem doanh thu";
            this.btnXemTKDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXemTKDoanhThu.UseVisualStyleBackColor = false;
            this.btnXemTKDoanhThu.Click += new System.EventHandler(this.btnXemTKDoanhThu_Click);
            // 
            // FormThongKeHD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1411, 836);
            this.Controls.Add(this.panel11);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormThongKeHD";
            this.Text = "ThongKeHD";
            this.Load += new System.EventHandler(this.ThongKeHD_Load);
            this.panel11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgThongTinTK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTKSPBanNhieu;
        private System.Windows.Forms.Button btnXemTKDoanhThu;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dtgThongTinTK;
        private System.Windows.Forms.DateTimePicker dtpNgayBD;
        private System.Windows.Forms.DateTimePicker dtpNgayKT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgXuatHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANV;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOLUONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIAMGIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTien;
        private System.Windows.Forms.Button btnReport;
    }
}