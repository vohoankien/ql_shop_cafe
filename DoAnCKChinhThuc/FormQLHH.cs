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
    public partial class FormQLHH : Form
    {
        public FormQLHH()
        {
            InitializeComponent();
        }

        private void FormQL_HH_Load(object sender, EventArgs e)
        {
            loadDaTacbHH();
            loadDaTaDSHH();
            loadGridHH();
            btnHuyThem.Visible = false;
        }
        string TaoMaHH()
        {
            string maHH = "HH";

            //Tìm trong bảng BAN có mã như mẫu

            string cauTV = "select top 1 * from Hanghoa order by (maHH) desc";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(cauTV);
            string pn;
            pn = dt.Rows[0]["MaHH"].ToString();

            if (dt.Rows.Count > 0) // Đã có bàn
            {
                //Cắt 3 kí tự cuối chuyển sang số rồi cộng thêm 1               
                int stt = int.Parse(pn.Substring(2, 3)) + 1;

                //Bổ sung vào thêm cho đầy đủ ký tự
                if (stt < 10)
                {
                    maHH += "00" + stt;
                }
                else if (stt < 100)
                {
                    maHH += "0" + stt;
                }
                else
                {
                    maHH += stt;
                }
            }
            else //Ngược lại thì ngày chưa có cái mã phiếu nhập nào
            {
                maHH += "001";
            }

            return maHH;
        }

        //Kiểm tra xem có maHH này chưa


        void loadDaTacbHH ()
        {
            DBConnect db = new DBConnect();
            string cautruyvan = "select malh,tenlh from LoaiHH";
            DataTable dt = db.getDataTable(cautruyvan);
            cbTenLH.DataSource = dt;
            cbTenLH.DisplayMember = "TenLH";
            cbTenLH.ValueMember = "MaLH";
        }

        void loadDaTaDSHH ()
        {
            DBConnect db = new DBConnect();
            string cautruyvan = "select malh,mahh,tenhh,giasp from hanghoa";
            DataTable dt = db.getDataTable(cautruyvan);
            dtgvTTHH.DataSource = dt;
        }
     

        void DatabingdingHH(DataTable dt)
        {
            // Xóa DataBindings trước khi liên kết mới
            txtMAHH.DataBindings.Clear();
            txtTENHH.DataBindings.Clear();

            cbTenLH.DataBindings.Clear();
            //Liên kết dữ liệu với text box
            txtMAHH.DataBindings.Add("Text", dt, "MaHH");
            txtTENHH.DataBindings.Add("Text", dt, "TenHH");
            txtGIASP.DataBindings.Add("Text", dt, "GiaSP");
            cbTenLH.DataBindings.Add("SelectedValue", dt, "MaLH");
        }
        void loadGridHH()
        {
            DatabingdingHH((DataTable)(dtgvTTHH.DataSource));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnThem.Tag == "Đã Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMAHH.DataBindings.Clear();
                    txtTENHH.DataBindings.Clear();
                    txtGIASP.DataBindings.Clear();
                    cbTenLH.DataBindings.Clear();

                    if (string.IsNullOrEmpty(txtTENHH.Text) || cbTenLH.SelectedIndex == -1 || string.IsNullOrEmpty(txtTENHH.Text))
                    {
                        throw new Exception("Vui lòng nhập đầy đủ dữ liệu");
                    }

                    // Tạo một DataRow mới và thêm dữ liệu vào
                    DataTable dt = (DataTable)dtgvTTHH.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["MaHH"] = txtMAHH.Text;
                    newRow["TenHH"] = txtTENHH.Text;
                    newRow["GIaSP"] = txtGIASP.Text;
                    newRow["MaLH"] = cbTenLH.SelectedValue;

                    // Thêm DataRow vào DataTable
                    dt.Rows.Add(newRow);

                    // Gán DataTable làm DataSource cho DataGridView
                    dtgvTTHH.DataSource = dt;
                    btnThem.Text = "Thêm";
                    loadGridHH();
                    btnThem.Tag = "Chưa Click";
                    btnHuyThem.Visible = false;
                    txtMAHH.Enabled = true;
                }
                else if (btnThem.Tag == "Chưa Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMAHH.DataBindings.Clear();
                    txtTENHH.DataBindings.Clear();
                    txtGIASP.DataBindings.Clear();
                    cbTenLH.DataBindings.Clear();
                 
                    txtMAHH.Enabled = false;

                    txtMAHH.Text = TaoMaHH();
                    txtTENHH.Text = "";
                    txtGIASP.Text = "";
                    txtTENHH.Focus();
                   
                    cbTenLH.SelectedIndex = -1;
                    btnThem.Text = "Xong";
                    btnThem.Tag = "Đã Click";
                    btnHuyThem.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHuyThem_Click(object sender, EventArgs e)
        {
            loadGridHH();
            btnThem.Text = "Thêm";
            btnThem.Tag = "Chưa Click";
            btnHuyThem.Visible = false;
            txtMAHH.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgvTTHH.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy dòng đang chọn
                    DataGridViewRow selectedRow = dtgvTTHH.SelectedRows[0];

                    // Lấy DataTable từ DataSource của DataGridView (giả sử DataSource là DataTable)
                    DataTable dataTable = (DataTable)dtgvTTHH.DataSource;

                    // Xác định index của dòng đang chọn trong DataTable
                    int rowIndex = dtgvTTHH.SelectedRows[0].Index;


                    // Kiểm tra index có hợp lệ không
                    if (rowIndex >= 0 && rowIndex < dtgvTTHH.Rows.Count)
                    {
                        // Xóa dòng từ DataSource
                        dtgvTTHH.Rows.RemoveAt(rowIndex);

                        // Gán lại DataSource mới cho DataGridView
                        dtgvTTHH.DataSource = dataTable;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }

        }
        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn LƯU cơ sở dữ liệu này ?", "Xác nhận LƯU", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DBConnect db = new DBConnect();
                string cauTruyVan = "Select malh,mahh,tenhh,giasp from HangHoa";
                DataTable dt = (DataTable)dtgvTTHH.DataSource;
                int kq = db.updateDataTable(dt, cauTruyVan);
                if (kq != 0)
                {
                    MessageBox.Show("Lưu dữ liệu thành công");
                }
                else
                {
                    MessageBox.Show("Lưu dữ liệu không thành công");
                }
            }
        }

        private void txtGIASP_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
