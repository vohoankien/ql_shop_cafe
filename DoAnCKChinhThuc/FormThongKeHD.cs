using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCKChinhThuc
{
    public partial class FormThongKeHD : Form
    {
        public FormThongKeHD()
        {
            InitializeComponent();
        }

        void HienThiDSHD()
        {
            string chuoi = "Select hd.NgayXuatHD, hd.MaHDBH, hd.MaNV, ct.SoLuong, hh.TenHH, ct.MaHH, ct.ThanhTien, hd.GiamGia " +
                            "from HOADON hd, CHITIETHOADON ct, HANGHOA hh " +
                            "where hd.MaHDBH = ct.MaHDBH AND ct.MaHH = hh.MaHH";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(chuoi);
            dtgThongTinTK.DataSource = dt;
        }
        void tatKichHoatNut(Button btn)
        {
            btn.Enabled = false;
            btn.BackColor = Color.FromArgb(50, Color.Gray); // Tùy chỉnh độ trong suốt của màu xám
            btn.ForeColor = Color.FromArgb(50, Color.White); // Tùy chỉnh độ trong suốt của màu trắng
        }
        void moKichHoatNut(Button btn)
        {
            btn.Enabled = true;
            btn.BackColor = Color.DeepSkyBlue; // Tùy chỉnh độ trong suốt của màu xám
            btn.ForeColor = Color.White; // Tùy chỉnh độ trong suốt của màu trắng
        }
        private void ThongKeHD_Load(object sender, EventArgs e)
        {
            HienThiDSHD();
            tatKichHoatNut(btnReport);
        }

        private void btnXemTKDoanhThu_Click(object sender, EventArgs e)
        {
            try
            {
                string chuoiTV = "SELECT CONVERT(date, NgayXuatHD) AS 'NgayXuatHD',  SUM(SoTienThanhToan) AS 'DoanhThu' " +
                             "FROM HOADON " +
                             "WHERE NgayXuatHD >= '" + dtpNgayBD.Text + "' AND NgayXuatHD <= DATEADD(day, 1, '" + dtpNgayKT.Text + "') " +
                             "GROUP BY CONVERT(date, NgayXuatHD)";
                DBConnect db = new DBConnect();
                DataTable dt = db.getDataTable(chuoiTV);
                dtgThongTinTK.DataSource = dt;
                moKichHoatNut(btnReport);
            }   
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnTKSPBanNhieu_Click(object sender, EventArgs e)
        {
            string chuoi = "EXEC sp_ThongKeDSSPBanChayTheoTG '" + dtpNgayBD.Text + "','" + dtpNgayKT.Text+ "'";
    
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(chuoi);
            dtgThongTinTK.DataSource = dt;
            tatKichHoatNut(btnReport);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dtgThongTinTK.DataSource;
            FormReportThongKe tt = new FormReportThongKe();
            OpenFormReport(tt, dt);
        }

        private void OpenFormReport(FormReportThongKe childForm, DataTable dt)
        {
            childForm.dt = dt;
            childForm.ShowDialog();
        }
    }
}
