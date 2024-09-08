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
    public partial class GiaoDienAdmin : Form
    {
        public GiaoDienAdmin()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.Resize += GiaoDienAdmin_Resize; // Gán sự kiện Resize cho form
            UpdateButtonPosition(); // Gọi hàm để cập nhật vị trí của nút khi form được tạo
        }
        private void UpdateButtonPosition()
        {
            //Cập nhật vị trí của label
            labelChao.Location = new Point(this.Width - btnDoiMatKhau.Width - 350, 5); // 25px từ biên và đỉnh
            // Cập nhật vị trí của nút (button) ở góc phải trên cùng của form
            btnDoiMatKhau.Location = new Point(this.Width - btnDoiMatKhau.Width - 350, 25); // 25px từ biên và đỉnh
            btnDangXuat.Location = new Point(this.Width - btnDangXuat.Width - 200, 25); // 25px từ biên và đỉnh
        }
        private string hoTen;
        private string taiKhoan;
        public GiaoDienAdmin(string taiKhoan, string hoTen)
        {
            InitializeComponent();
            this.hoTen = hoTen;
            this.taiKhoan = taiKhoan;
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild!=null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void OpenChildForm(Form childForm, string tk)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            childForm.Tag = tk;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLNV());
            label1.Text = button2.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormBanHang());
            label1.Text = button1.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLBan());
            label1.Text = button6.Text;
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            DoiMatKhauDangNhap dmk= new DoiMatKhauDangNhap(taiKhoan,hoTen);
            dmk.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                DangNhap dangNhap = new DangNhap();
                dangNhap.Show();
            }
        }

        private void GiaoDienAdmin_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
            labelChao.Text = "Chào : " + hoTen;
            UpdateButtonPosition();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormBanHang(),taiKhoan);
            label1.Text = button1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLKhachHang());
            label1.Text = button3.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLLLOAIHH());
            label1.Text = button4.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThongKeHD());
            label1.Text = button7.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLHH());
            label1.Text = button5.Text;
        }

        private void GiaoDienAdmin_Resize(object sender, EventArgs e)
        {
            UpdateButtonPosition(); // Gọi hàm cập nhật vị trí nút khi form thay đổi kích thước
        }

    }
}
