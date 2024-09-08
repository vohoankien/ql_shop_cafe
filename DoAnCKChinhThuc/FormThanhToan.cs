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
    public partial class FormThanhToan : Form
    {

        public FormThanhToan()
        {
            InitializeComponent();
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            string maHDBH = this.Tag.ToString();
            string cauTV = "select HoaDON.*,TenBan from HOADON left join Ban on HOADON.MaBan = BAN.MaBan where MaHDBH = " + maHDBH + "";
            DBConnect db = new DBConnect();
            DataTable dt = new DataTable();
            dt = db.getDataTable(cauTV);
            labelMaBan.Text = dt.Rows[0]["MaBan"].ToString();
            labelTenBan.Text = dt.Rows[0]["TenBan"].ToString();
            labelMaHoaDon.Text = this.Tag.ToString();
            labelMaNhanVien.Text = dt.Rows[0]["MaNV"].ToString();
            labelTongTien.Text = dt.Rows[0]["TongTien"].ToString();
            labelSoTienThanhToan.Text = labelTongTien.Text;
            string cautv2 = "select HANGHOA.MaHH,TenHH,SoLuong,GiaSP,ThanhTien from CHITIETHOADON left join HANGHOA on HANGHOA.MaHH = CHITIETHOADON.MaHH where MaHDBH = " + maHDBH + "";
            dt = db.getDataTable(cautv2);
            dataGridView1.DataSource = dt;
        }

        private void btnLayThongTinKH_Click(object sender, EventArgs e)
        {
            FormQLKhachHang temp = new FormQLKhachHang();
            temp.Show();
        }

        private void txtNhapMaKH_Click(object sender, EventArgs e)
        {
            txtNhapMaKH.Text = "";
        }

        private void txtNhapMaKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Kích hoạt sự kiện Click của button
                layDiemTichLuy(txtNhapMaKH.Text);
            }
        }

        private void txtNhapMaKH_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNhapMaKH.Text))
            {
                txtNhapMaKH.Text = "Nhập vào mã KH";
            }
            else
                layDiemTichLuy(txtNhapMaKH.Text);
        }

        private void layDiemTichLuy(string makh)
        {
            try
            {
                string cauTV = "select DiemTichLuy,TenKH from KHACHHANG where MaKH = '" + makh + "'";
                int diemTL = 0;
                DBConnect db = new DBConnect();
                DataTable dt = new DataTable();
                dt = db.getDataTable(cauTV);
                diemTL = int.Parse(dt.Rows[0]["DiemTichLuy"].ToString());
                labelDiemDangCo.Text = diemTL.ToString();
                labelHoTen.Text = dt.Rows[0]["TenKH"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void txtDungDiem_Click(object sender, EventArgs e)
        {
            txtDungDiem.Text = "";
        }

        private void txtDungDiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Kích hoạt sự kiện Click của button
                loadSoTienThanhToan(txtDungDiem.Text);
            }
        }

        private void loadSoTienThanhToan(string diemTichLuy)
        {
            try
            {
                int tongtien = int.Parse(labelTongTien.Text);
                int diemDangCo = int.Parse(labelDiemDangCo.Text);
                int dungDiem = int.Parse(txtDungDiem.Text);
                if (dungDiem > diemDangCo)
                {
                    throw new Exception("Điểm tích lũy vượt điểm hiện có ");
                }
                else
                {
                    labelSoTienThanhToan.Text = (tongtien - dungDiem).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnect db = new DBConnect();
                if (!string.IsNullOrEmpty(txtDungDiem.Text) && txtDungDiem.Text != "Nhập vào số điểm TL")
                {
                    //Sau khi dùng điểm thì trừ điểm trong database
                    string cauTV = "Update KhachHang set DiemTichLuy = DiemTichLuy - " + txtDungDiem.Text + " where MaKH = '" + txtNhapMaKH.Text + "'";                   
                    db.getNonQuery(cauTV);                   
                }
                //Cập nhật lại bàn
                string cauTV2 = "Update Ban set TrangThai = N'Trống' where MaBan = '" + labelMaBan.Text + "'";
                string cauTV3 = "Update Ban set HDHienTai = null where MaBan = '" + labelMaBan.Text + "'";
                db.getNonQuery(cauTV2);
                db.getNonQuery(cauTV3);
                //Cập nhật số tiền thanh toán trong hóa đơn
                string cauTV4 = "Update HOADON set SoTienThanhToan = " + labelSoTienThanhToan.Text + " where MaHDBH = "+this.Tag+"";
                db.getNonQuery(cauTV4);
                //Cập nhật cộng điểm tích lũy
                int congDiemTL = int.Parse(labelTongTien.Text) * 5 / 100;
                string cauTV5 = "Update KHACHHANG set DiemTichLuy = DiemTichLuy + "+congDiemTL+" where MaKH = '"+txtNhapMaKH.Text+"'";
                db.getNonQuery(cauTV5);
                FormReportHoaDon tt = new FormReportHoaDon();
                int tienDungDiem = 0;
                int tienKhachDua = int.Parse(txtTienKhachDua.Text);
                int tienThoi = int.Parse(labelTienThoi.Text);

                if (!string.IsNullOrEmpty(txtDungDiem.Text) && txtDungDiem.Text != "Nhập vào số điểm TL")
                {
                    tienDungDiem = int.Parse(txtDungDiem.Text);
                }
                OpenFormReport(tt, labelMaHoaDon.Text, tienKhachDua,tienThoi,tienDungDiem);
                this.Close();            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

        private void OpenFormReport(FormReportHoaDon childForm, string mahd, int tienkd, int tienthoi, int dungDiem)
        {           
            childForm.maHDBH = mahd;
            childForm.tienKhachDua = tienkd;
            childForm.tienThoi = tienthoi;
            childForm.dungDiemTL = dungDiem;
            childForm.ShowDialog();
        }

        private void txtDungDiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDungDiem.Text))
            {
                txtDungDiem.Text = "Nhập vào số điểm TL";
            }
            else
            {
                loadSoTienThanhToan(txtDungDiem.Text);
            }
        }

        private void txtTienKhachDua_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDungDiem.Text))
            {
                txtDungDiem.Text = "Nhập vào số điểm TL";
            }
        }

        private void txtTienKhachDua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Kích hoạt sự kiện Click của button
                loadSoTienThoi();
            }
        }

        private void loadSoTienThoi()
        {
            try
            {
                int soTienPhaiTra = int.Parse(labelSoTienThanhToan.Text);
                int tienDungDiem = 0;
                if (!string.IsNullOrEmpty(txtDungDiem.Text) && txtDungDiem.Text != "Nhập vào số điểm TL")
                {
                    tienDungDiem = int.Parse(txtDungDiem.Text);
                }
                int tienKhachDua = int.Parse(txtTienKhachDua.Text);
                labelTienThoi.Text = (tienKhachDua - soTienPhaiTra).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDungDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
