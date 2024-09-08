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
    public partial class FormQLLLOAIHH : Form
    {
        public FormQLLLOAIHH()
        {
            InitializeComponent();
        }

        private void FormQLLLOAIHH_Load(object sender, EventArgs e)
        {
            loadDaTaDSLH();
            loadGridLH();
            btnHuyThem.Visible = false;
        }

        string TaoMaLH()
        {
            string MaLH = "LH";

            //Tìm trong bảng BAN có mã như mẫu

            string cauTV = "select top 1 * from LoaiHH order by (MaLH) desc";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(cauTV);
            string pn;
            pn = dt.Rows[0]["MaLH"].ToString();

            if (dt.Rows.Count > 0) // Đã có bàn
            {
                //Cắt 3 kí tự cuối chuyển sang số rồi cộng thêm 1               
                int stt = int.Parse(pn.Substring(2, 3)) + 1;

                //Bổ sung vào thêm cho đầy đủ ký tự
                if (stt < 10)
                {
                    MaLH += "00" + stt;
                }
                else if (stt < 100)
                {
                    MaLH += "0" + stt;
                }
                else
                {
                    MaLH += stt;
                }
            }
            else //Ngược lại thì ngày chưa có cái mã phiếu nhập nào
            {
                MaLH += "001";
            }

            return MaLH;
        }

       

        void loadDaTaDSLH()
        {
            DBConnect db = new DBConnect();
            string cautruyvan = "select * from LoaiHH";
            DataTable dt = db.getDataTable(cautruyvan);
            dtgvTTLH.DataSource = dt;
        }

        void DatabingdingLH(DataTable dt)
        {
            // Xóa DataBindings trước khi liên kết mới
            txtMALH.DataBindings.Clear();
            txtTENLH.DataBindings.Clear();

            
            //Liên kết dữ liệu với text box
            txtMALH.DataBindings.Add("Text", dt, "MaLH");
            txtTENLH.DataBindings.Add("Text", dt, "TenLH");
            txtMoTa.DataBindings.Add("Text", dt, "MoTa");
           
        }
        void loadGridLH()
        {
            DatabingdingLH((DataTable)(dtgvTTLH.DataSource));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnThem.Tag == "Đã Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMALH.DataBindings.Clear();
                    txtTENLH.DataBindings.Clear();
                    txtMoTa.DataBindings.Clear();
                 
                    if (string.IsNullOrEmpty(txtTENLH.Text) ||  string.IsNullOrEmpty(txtMoTa.Text))
                    {
                        throw new Exception("Vui lòng nhập đầy đủ dữ liệu");
                    }

                    // Tạo một DataRow mới và thêm dữ liệu vào
                    DataTable dt = (DataTable)dtgvTTLH.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["MaLH"] = txtMALH.Text;
                    newRow["TenLH"] = txtTENLH.Text;
                    newRow["MoTa"] = txtMoTa.Text;

                    // Thêm DataRow vào DataTable
                    dt.Rows.Add(newRow);

                    // Gán DataTable làm DataSource cho DataGridView
                    dtgvTTLH.DataSource = dt;
                    btnThem.Text = "Thêm";
                    loadGridLH();
                    btnThem.Tag = "Chưa Click";
                    btnHuyThem.Visible = false;
                    txtMALH.Enabled = true;
                }
                else if (btnThem.Tag == "Chưa Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMALH.DataBindings.Clear();
                    txtTENLH.DataBindings.Clear();
                    txtMoTa.DataBindings.Clear();
                  

                    txtMALH.Enabled = false;

                    txtMALH.Text = TaoMaLH();
                    txtTENLH.Text = "";
                    txtMoTa.Text = "";
                    txtTENLH.Focus();

                  
                    btnThem.Text = "Hoàn tất thêm";
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
            loadGridLH();
            btnThem.Text = "Thêm";
            btnThem.Tag = "Chưa Click";
            btnHuyThem.Visible = false;
            txtMALH.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgvTTLH.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy dòng đang chọn
                    DataGridViewRow selectedRow = dtgvTTLH.SelectedRows[0];

                    // Lấy DataTable từ DataSource của DataGridView (giả sử DataSource là DataTable)
                    DataTable dataTable = (DataTable)dtgvTTLH.DataSource;

                    // Xác định index của dòng đang chọn trong DataTable
                    int rowIndex = dtgvTTLH.SelectedRows[0].Index;


                    // Kiểm tra index có hợp lệ không
                    if (rowIndex >= 0 && rowIndex < dtgvTTLH.Rows.Count)
                    {
                        // Xóa dòng từ DataSource
                        dtgvTTLH.Rows.RemoveAt(rowIndex);

                        // Gán lại DataSource mới cho DataGridView
                        dtgvTTLH.DataSource = dataTable;
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
                string cauTruyVan = "Select * from LoaiHH";
                DataTable dt = (DataTable)dtgvTTLH.DataSource;
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

    }
}
