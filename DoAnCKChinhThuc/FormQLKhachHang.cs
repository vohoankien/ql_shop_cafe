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
    public partial class FormQLKhachHang : Form
    {
        public FormQLKhachHang()
        {
            InitializeComponent();
        }

        void DSKhachHang()
        {
            DBConnect db = new DBConnect();
            string chuoi = "Select * from KHACHHANG";
            DataTable t = db.getDataTable(chuoi);
            dgvDSKhachHang.DataSource = t;

            string chuoitruyvan = "Select * from LOAIKH";
            DataTable dat = db.getDataTable(chuoitruyvan);
            DataRow drw = dat.NewRow();
            //drw["MaLKH"] = "All";
            //drw["TenLKH"] = "Tat ca cac loai khach";
            //dat.Rows.InsertAt(drw, 0);

            cboLoaiKh.DataSource = dat;
            cboLoaiKh.DisplayMember = "TenLKH";
            cboLoaiKh.ValueMember = "MaLKH";
        }

        string TaoMaKhachHang()
        {
            string maKH = "KH";

            //Tìm trong bảng KhachHang có mã như mẫu

            string cauTV = "select top 1 * from KhachHang order by (MaKH) desc";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(cauTV);
            string pn;
            pn = dt.Rows[0]["MaKH"].ToString();

            if (dt.Rows.Count > 0) // Đã có bàn
            {
                //Cắt 3 kí tự cuối chuyển sang số rồi cộng thêm 1               
                int stt = int.Parse(pn.Substring(2, 3)) + 1;

                //Bổ sung vào thêm cho đầy đủ ký tự
                if (stt < 10)
                {
                    maKH += "00" + stt;
                }
                else if (stt < 100)
                {
                    maKH += "0" + stt;
                }
                else
                {
                    maKH += stt;
                }
            }
            else //Ngược lại thì ngày chưa có cái mã phiếu nhập nào
            {
                maKH += "001";
            }

            return maKH;
        }

        void ClearDataBingDing()
        {
            txtMaKH.DataBindings.Clear();
            txtTenKH.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtDiemTichLuy.DataBindings.Clear();
            cboLoaiKh.DataBindings.Clear();
        }

        void DatabingdingKH(DataTable dt)
        {
            // Xóa DataBindings trước khi liên kết mới
            ClearDataBingDing();
            //Liên kết dữ liệu với text box
            txtMaKH.DataBindings.Add("Text", dt, "MaKH");
            txtTenKH.DataBindings.Add("Text", dt, "TenKH");
            txtDiaChi.DataBindings.Add("Text", dt, "DiaChi");
            txtSDT.DataBindings.Add("Text", dt, "SDT");
            txtDiemTichLuy.DataBindings.Add("Text", dt, "DiemTichLuy");
            // Liên kết dữ liệu cho ComboBox
            cboLoaiKh.DataBindings.Add("SelectedValue", dt, "MaLKH");
        }

        void loadGridKH()
        {
            DatabingdingKH((DataTable)dgvDSKhachHang.DataSource);
        }

        private void QuanLyKhachHang_Load(object sender, EventArgs e)
        {
            DSKhachHang();
            loadGridKH();
            btnHuyThem.Visible = false;
        }

        bool KTMaLopTrung(string k)
        {
            DBConnect db = new DBConnect();
            string chuoiTruyVan = "Select count(*) from KHACHHANG where MaKH = '" + k + "'";
            int kq = (int)db.getExcuteScalar(chuoiTruyVan);
            if (kq == 0)
                return true;
            return false;
        }

        bool KTKhoaNgoai(string k)
        {
            DBConnect db = new DBConnect();
            string chuoitruyvan = "Select count(*) from HOADON where MaKH = '" + k + "'";
            int kq = (int)db.getExcuteScalar(chuoitruyvan);
            if (kq == 0)
                return true;
            return false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnThem.Tag == "Đã Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    ClearDataBingDing();
                    if (string.IsNullOrEmpty(txtTenKH.Text) || string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtDiemTichLuy.Text) || cboLoaiKh.SelectedIndex == -1)
                    {
                        throw new Exception("Vui lòng nhập đầy đủ dữ liệu");
                    }

                    // Tạo một DataRow mới và thêm dữ liệu vào
                    DataTable dt = (DataTable)dgvDSKhachHang.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["MaKH"] = txtMaKH.Text;
                    newRow["TenKH"] = txtTenKH.Text;
                    newRow["DiaChi"] = txtDiaChi.Text;
                    newRow["SDT"] = txtSDT.Text;
                    newRow["DiemTichLuy"] = txtDiemTichLuy.Text;
                    newRow["MaLKH"] = cboLoaiKh.SelectedValue;

                    // Thêm DataRow vào DataTable
                    dt.Rows.Add(newRow);

                    // Gán DataTable làm DataSource cho DataGridView
                    dgvDSKhachHang.DataSource = dt;
                    btnThem.Text = "Thêm";
                    loadGridKH();
                    btnThem.Tag = "Chưa Click";
                    btnHuyThem.Visible = false;
                    txtMaKH.Enabled = true;
                }
                else if (btnThem.Tag == "Chưa Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    ClearDataBingDing();

                    txtMaKH.Enabled = false;

                    txtMaKH.Text = TaoMaKhachHang();
                    txtTenKH.Text = "";
                    txtTenKH.Focus();
                    txtDiaChi.Text = "";
                    txtSDT.Text = "";
                    txtDiemTichLuy.Text = "";
                    cboLoaiKh.SelectedIndex = -1;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSKhachHang.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy dòng đang chọn
                    DataGridViewRow selectedRow = dgvDSKhachHang.SelectedRows[0];

                    // Lấy DataTable từ DataSource của DataGridView (giả sử DataSource là DataTable)
                    DataTable dataTable = (DataTable)dgvDSKhachHang.DataSource;

                    // Xác định index của dòng đang chọn trong DataTable
                    int rowIndex = dgvDSKhachHang.SelectedRows[0].Index;

                    

                    //Kiểm tra khóa ngoại trong bảng KhachHang
                    if(KTKhoaNgoai(txtMaKH.Text) == false)
                    {
                        MessageBox.Show("Tồn tại khóa ngoại với HOADON nên không thể xóa");
                        return;
                    }

                    // Kiểm tra index có hợp lệ không
                    if (rowIndex >= 0 && rowIndex < dgvDSKhachHang.Rows.Count)
                    {
                        // Xóa dòng từ DataSource
                        dgvDSKhachHang.Rows.RemoveAt(rowIndex);

                        // Gán lại DataSource mới cho DataGridView
                        dgvDSKhachHang.DataSource = dataTable;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn LƯU cơ sở dữ liệu này ?", "Xác nhận LƯU", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DBConnect db = new DBConnect();
                string cauTruyVan = "Select * from KHACHHANG";
                DataTable dt = (DataTable)dgvDSKhachHang.DataSource;
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            string cauTruyVan;
            if (txtTim.Text.ToString() == "Nhập mã khách hàng hoặc SĐT")
            {
                cauTruyVan = "select * from KHACHHANG";
            }
            else
            {
                cauTruyVan = "select * from KHACHHANG where MaKH = '" + txtTim.Text + "' or SDT like N'%" + txtTim.Text + "%'";
            }
            DBConnect db = new DBConnect();
            //Thuc thi
            DataTable dt = db.getDataTable(cauTruyVan);
            //Them khoa chinh
            DataColumn[] key = new DataColumn[1];
            key[0] = dt.Columns["MaKH"];

            dt.PrimaryKey = key;
            //Do du lieu len datagridview
            dgvDSKhachHang.DataSource = dt;
            loadGridKH();
        }

        private void txtTim_Click(object sender, EventArgs e)
        {
            txtTim.Clear();
        }

        private void txtTim_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTim.Text))
            {
                txtTim.Text = "Nhập mã khách hàng hoặc SĐT";
            }
        }

        private void btnHuyThem_Click(object sender, EventArgs e)
        {
            loadGridKH();
            btnThem.Text = "Thêm";
            btnThem.Tag = "Chưa Click";
            btnHuyThem.Visible = false;
            txtMaKH.Enabled = true;
        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
            string chuoi = "Select * from HOADON where MaKH = '" + txtMaKH.Text + "'";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(chuoi);
            dtvLichSuGiaoDich.DataSource = dt;
        }
    }
}
