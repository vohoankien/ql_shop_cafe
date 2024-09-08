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
    public partial class FormNhapMaBan : Form
    {
        public string MaNhap { get; private set; }

        public FormNhapMaBan()
        {
            InitializeComponent();
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            MaNhap = txtNhapMaBan.Text; // Lấy giá trị mã bàn từ TextBox
            this.Close(); // Đóng Form nhập mã bàn sau khi lấy giá trị mã bàn
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNhapMaBan_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
