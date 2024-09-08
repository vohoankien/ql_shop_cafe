using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCKChinhThuc
{
    public partial class FormBanHang : Form
    {
        private PictureBox currentSelectedPictureBox;
        private Dictionary<PictureBox, PictureBoxState> pictureStates = new Dictionary<PictureBox, PictureBoxState>();
        private Image coffeeTrong = Properties.Resources.icons8_coffee_trong; // Hình ảnh bàn trống
        private Image coffeeDangDung = Properties.Resources.icons8_coffee_dangdung; // Hình ảnh bàn đang dùng
        private Image coffeeDangChon = Properties.Resources.icons8_coffee_dangchon; // Hình ảnh đang chọn
        private DataTable dt = new DataTable(); // DataTable của trạng thái bàn

        bool tonTaiChiTietHoaDon(string mahd, string mahh)
        {
            string cauTV = "select count(*) from CHITIETHOADON where MaHDBH = "+mahd+" and MaHH = '"+mahh+"'";
            DBConnect db = new DBConnect();
            int kq = (int)db.getExcuteScalar(cauTV);
            if (kq != 0)
	        {
		        return true;
	        }
            return false;
        }

        public FormBanHang()
        {
            InitializeComponent();
            // Sự kiện TextChanged cho txtSoLuong và labelGiaMon
            labelGiaMon.TextChanged += new EventHandler(TinhToanThanhTien);
            btnThem.Click += new EventHandler(TinhToanThanhTien);
        }
        int TaoMaHoaDon()
        {
            int maBan = 0;

            //Tìm trong bảng BAN có mã như mẫu

            string cauTV = "select top 1 * from HoaDon order by (MaHDBH) desc";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(cauTV);
            string pn;
            pn = dt.Rows[0]["MaHDBH"].ToString();

            if (dt.Rows.Count > 0) // Đã có hóa đơn
            {
                //Đổi sang số chuyển sang số rồi cộng thêm 1               
                int stt = int.Parse(pn) + 1;
                maBan = stt;
            }
            else //Ngược lại thì chưa có hóa đơn nào
            {
                maBan = 1;
            }

            return maBan;
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
        private void loadGoiMonTuongUng(string mahd)
        {
            string ctv3 = "select MaHDBH,HANGHOA.MaHH,TenHH,GiaSP,SoLuong,ThanhTien from CHITIETHOADON left join HANGHOA on CHITIETHOADON.MaHH = HANGHOA.MaHH where MaHDBH = " + mahd + "";
            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            dt = db.getDataTable(ctv3);
            dataGridViewGoiMon.DataSource = dt;
            dataGridViewGoiMon.Columns["MaHH"].Visible = false;
            dataGridViewGoiMon.Columns["MaHDBH"].Visible = false;
            loadGridGoiMon();
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;

            if (clickedPictureBox != null)
            {
                string currentState = pictureStates[clickedPictureBox].TrangThai;

                if (currentSelectedPictureBox != null && currentSelectedPictureBox != clickedPictureBox)
                {
                    string prevState = pictureStates[currentSelectedPictureBox].TrangThai;
                    if (prevState == "Trống")
                    {
                        currentSelectedPictureBox.Image = coffeeTrong;
                    }
                    else if (prevState == "Đang dùng")
                    {
                        currentSelectedPictureBox.Image = coffeeDangDung;
                    }
                }
                clickedPictureBox.Image = coffeeDangChon;

                moKichHoatNut(btnMoBan);
                tatKichHoatNut(btnTinhTien);

                currentSelectedPictureBox = clickedPictureBox;

                if (clickedPictureBox != null)
                {
                    labelNgayHoaDon.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                    labelManhanvien.Text = this.Tag.ToString();
                    string current = pictureStates[clickedPictureBox].TrangThai;
                    if (current == "Trống")
                    {
                        labelMaHoaDon.Text = "";
                        moKichHoatNut(btnMoBan);
                        tatKichHoatNut(btnDongBan);
                        tatKichHoatNut(btnChuyenBan);
                        DataTable dt = new DataTable();
                        dataGridViewGoiMon.DataSource = dt;
                    }
                    else if (current == "Đang dùng")
                    {
                        labelMaHoaDon.Text = pictureStates[clickedPictureBox].MaHoaDon.ToString();
                        tatKichHoatNut(btnMoBan);
                        if (!string.IsNullOrEmpty(labelMaHoaDon.Text))
                        {
                            loadGoiMonTuongUng(labelMaHoaDon.Text);
                        }
                        if (dataGridViewGoiMon.RowCount == 0)
                        {
                            moKichHoatNut(btnDongBan);
                            moKichHoatNut(btnChuyenBan);
                            tatKichHoatNut(btnTinhTien);
                        }
                        else
                        {
                            tatKichHoatNut(btnDongBan);
                            moKichHoatNut(btnChuyenBan);
                            moKichHoatNut(btnTinhTien);
                        }                   
                    }
                }
            }
        }
        private void loadBan()
        {
            Dictionary<PictureBox, PictureBoxState> temp = new Dictionary<PictureBox, PictureBoxState>();
            for (int i = 0; i < 20; i++)
            {               
                string pictureBoxName = "pictureBox" + (i + 1);
                PictureBox pictureBox = Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;
                DBConnect db = new DBConnect();
                string ctv = "Select * from BAN";
                dt = db.getDataTable(ctv);

                if (pictureBox != null && dt.Rows.Count > i)
                {
                    string trangThai = dt.Rows[i]["TrangThai"].ToString();
                    string hdht = dt.Rows[i]["HDHienTai"].ToString();
                    PictureBoxState taoMoi = new PictureBoxState();
                    taoMoi.TrangThai = trangThai;
                    taoMoi.MaHoaDon = hdht;
                    temp.Add(pictureBox, taoMoi);

                    if (trangThai == "Trống")
                    {
                        pictureBox.Image = coffeeTrong;
                    }
                    else if (trangThai == "Đang dùng")
                    {
                        pictureBox.Image = coffeeDangDung;
                    }

                    pictureBox.Click += PictureBox_Click;
                }
            }
            pictureStates = temp;
        }
        private void FormBanHang_Load(object sender, EventArgs e)
        {
            loadBan();        
            loadComboboxLoaiHang();
            tatKichHoatNut(btnMoBan);
            tatKichHoatNut(btnDongBan);
            tatKichHoatNut(btnTinhTien);
            tatKichHoatNut(btnChuyenBan);
        }

        void loadComboboxLoaiHang()
        {
            DBConnect db = new DBConnect();
            string cauTruyVan = "select * from LOAIHH";
            //Thuc thi
            DataTable dt = db.getDataTable(cauTruyVan);

            cbbLoaiHang.DataSource = dt;
            cbbLoaiHang.DisplayMember = "TenLH";
            cbbLoaiHang.ValueMember = "MaLH";

            DataRow dr = dt.NewRow();
            dr["MaLH"] = "All";
            dr["TenLH"] = "Tất cả các loại";
            dt.Rows.InsertAt(dr, 0);

            cbbLoaiHang.SelectedIndex = 0;
            loadGridViewThucDon();
        }

        void loadGridViewThucDon()
        {
            DBConnect db = new DBConnect();
            string cauTruyVan;
            if (cbbLoaiHang.SelectedValue == "All")
            {
                cauTruyVan = "select MaHH,TenHH,GiaSP from HANGHOA";
            }
            else
                cauTruyVan = "select MaHH,TenHH,GiaSP from HANGHOA where MaLH = '" + cbbLoaiHang.SelectedValue + "'";
            //Thuc thi
            DataTable dt = db.getDataTable(cauTruyVan);

            dataGridViewThucDon.DataSource = dt;
        }

        private void cbbLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGridViewThucDon();
        }

        private void btnMoBan_Click(object sender, EventArgs e)
        {
            if (currentSelectedPictureBox != null)
            {
                DBConnect db = new DBConnect();
                //Tạo mới hóa đơn và đổ lên grid view
                if (pictureStates[currentSelectedPictureBox].TrangThai == "Trống")
                {
                    labelMaHoaDon.Text = TaoMaHoaDon().ToString();
                    string ngayXHD = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                    string MABAN = currentSelectedPictureBox.Tag.ToString();
                    string MANV = this.Tag.ToString();
                    string ctv2 = "Insert into HoaDon values(" + labelMaHoaDon.Text + ",'" + MANV + "','" + MABAN + "','" +ngayXHD+"',0,0,0,'KH001',0)";
                    db.getNonQuery(ctv2);
                    loadGoiMonTuongUng(labelMaHoaDon.Text);
                }
                string maBan = currentSelectedPictureBox.Tag.ToString();             
                string ctv = "Update BAN set TrangThai = N'Đang dùng' where MaBan = '" + maBan + "'";
                string ctv3 = "Update BAN set HDHienTai = "+labelMaHoaDon.Text+" where MaBan = '" + maBan + "'";
                db.getNonQuery(ctv);
                db.getNonQuery(ctv3);
                pictureStates[currentSelectedPictureBox].TrangThai = "Đang dùng";
                pictureStates[currentSelectedPictureBox].MaHoaDon = labelMaHoaDon.Text;
                tatKichHoatNut(btnMoBan);       
            }
        }

        private void btnDongBan_Click(object sender, EventArgs e)
        {
            if (currentSelectedPictureBox != null)
            {
                DBConnect db = new DBConnect();
                //Xóa hóa đơn và đổ lên grid view
                if (pictureStates[currentSelectedPictureBox].TrangThai == "Đang dùng")
                {                  
                    string ctv2 = "Delete from HoaDon where MaHDBH = "+ labelMaHoaDon.Text + "";
                    db.getNonQuery(ctv2);
                    DataTable dt = new DataTable();
                    dataGridViewGoiMon.DataSource = dt;
                    labelMaHoaDon.Text = "";
                }
                string maBan = currentSelectedPictureBox.Tag.ToString();
                string ctv = "Update BAN set TrangThai = N'Trống' where MaBan = '" + maBan + "'";
                string ctv3 = "Update BAN set HDHienTai = null where MaBan = '" + maBan + "'";
                db.getNonQuery(ctv3);
                db.getNonQuery(ctv);
                pictureStates[currentSelectedPictureBox].TrangThai = "Trống";
                pictureStates[currentSelectedPictureBox].MaHoaDon = labelMaHoaDon.Text;
                tatKichHoatNut(btnDongBan);
                tatKichHoatNut(btnChuyenBan);
            }
        }

        void DatabingdingGoiMon(DataTable dt)
        {
            // Xóa DataBindings trước khi liên kết mới
            labelMaMon.DataBindings.Clear();
            labelGiaMon.DataBindings.Clear();
            //Liên kết dữ liệu với label
            labelMaMon.DataBindings.Add("Text", dt, "MaHH");
            labelGiaMon.DataBindings.Add("Text", dt, "GiaSP");
        }

        void loadGridGoiMon()
        {
            DatabingdingGoiMon((DataTable)dataGridViewGoiMon.DataSource);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa DataBindings trước khi liên kết mới
                labelMaMon.DataBindings.Clear();
                labelGiaMon.DataBindings.Clear();
                // Tạo một DataRow mới và thêm dữ liệu vào
                //loadGoiMonTuongUng(labelMaHoaDon.Text);
                DataTable dt = (DataTable)dataGridViewGoiMon.DataSource;
                DataRow newRow = dt.NewRow();
                if (dataGridViewThucDon.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dataGridViewThucDon.SelectedCells[0].RowIndex;

                    if (selectedRowIndex >= 0 && selectedRowIndex < dataGridViewThucDon.Rows.Count)
                    {
                        DataGridViewRow selectedRow = dataGridViewThucDon.Rows[selectedRowIndex];

                        string maHH = selectedRow.Cells["MaHH"].Value.ToString();
                        string giaSP = selectedRow.Cells["GiaSP"].Value.ToString();
                        string tenHH = selectedRow.Cells["TenHH"].Value.ToString();

                        // Sử dụng maHH và giaSP ở đây
                        newRow["MaHDBH"] = labelMaHoaDon.Text;
                        newRow["MaHH"] = maHH;
                        newRow["TenHH"] = tenHH;
                        newRow["GiaSP"] = giaSP;
                        newRow["SoLuong"] = 1;
                        newRow["ThanhTien"] = int.Parse(giaSP) * int.Parse(newRow["SoLuong"].ToString());
                    }
                }

                // Thêm DataRow vào DataTable
                dt.Rows.Add(newRow);

                // Gán DataTable làm DataSource cho DataGridView
                dataGridViewGoiMon.DataSource = dt;
                //Liên kết dữ liệu với label
                loadGridGoiMon();            
                //Thêm lưu vào database
                //Không cần thêm vì labelgiamon đã gọi hàm lưu vào data base

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable layCTHDTuGoiMon(DataTable dt1)
        {
            //DataTable dt2 = dt1.DefaultView.ToTable(false, "MaHDBH", "MaHH", "SoLuong", "ThanhTien");
            DataTable dt2 = new DataTable();

            // Chọn cột để sao chép
            string[] selectedColumns = new string[] { "MaHDBH", "MaHH", "SoLuong", "ThanhTien" };

            // Tạo cấu trúc cột cho dt2
            foreach (string colName in selectedColumns)
            {
                DataColumn col = dt1.Columns[colName];
                dt2.Columns.Add(col.ColumnName, col.DataType);
            }

            // Sao chép dữ liệu từ dt1 sang dt2
            foreach (DataRow row in dt1.Rows)
            {
                DataRow newRow = dt2.Rows.Add();
                foreach (string colName in selectedColumns)
                {
                    newRow[colName] = row[colName];
                }
            }
            return dt2;
        }

        private void luuDataBaseGoiMon(DataTable dt)
        {
            int totalRows = dt.Rows.Count;
            int currentRow = 0;
            DBConnect db = new DBConnect();
            //Xóa hết hiện tại ròi thêm mới vào chitiethoadon
            string cauTV = "delete from CHITIETHOADON where MAHDBH = " + labelMaHoaDon.Text + "";
            db.getNonQuery(cauTV);
            foreach (DataRow row in dt.Rows)
            {
                //if (tonTaiChiTietHoaDon(row["MaHDBH"].ToString(), row["MaHH"].ToString()))
                //{
                //    string cauTV = "Update CHITIETHOADON set SOLUONG = " + row["SoLuong"].ToString() + " where MaHDBH = " + row["MaHDBH"].ToString() + " and MaHH = '" + row["MaHH"].ToString() + "'";
                //    db.getNonQuery(cauTV);
                //    continue;
                //}
                // Tạo câu truy vấn INSERT dựa vào dữ liệu của mỗi dòng
                string query = "Insert into CHITIETHOADON VALUES (" + row["MaHDBH"] + ", '" + row["MaHH"] + "', '" + row["SoLuong"] + "', '" + row["ThanhTien"] + "')";
                // Thực thi câu truy vấn sử dụng hàm GetNonQuery
                try
                {
                    int kq = db.getNonQuery(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                currentRow++;

                // Kiểm tra xem có phải dòng cuối cùng không
                if (currentRow == totalRows)
                {
                    // Đã đến dòng cuối cùng, thoát khỏi vòng lặp
                    break;
                }
            }
        }

        private void luuDataBaseHoaDon()
        {
            string tongtien;
            string goc = labelTongTien.Text;
            /// Xóa tất cả các ký tự không phải số từ chuỗi
            tongtien = Regex.Replace(goc, "[^0-9]", "");
            // Tạo câu truy vấn INSERT dựa vào dữ liệu của mỗi dòng
            string query = "Update HOADON set TONGTIEN = "+tongtien+" where MaHDBH = " + labelMaHoaDon.Text + "";

            DBConnect db = new DBConnect();
            // Thực thi câu truy vấn sử dụng hàm GetNonQuery
            try
            {
                int kq = db.getNonQuery(query);
                if (kq != 0)
                {
                    MessageBox.Show("Đã lưu Hóa Đơn thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewGoiMon.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy dòng đang chọn
                    DataGridViewRow selectedRow = dataGridViewGoiMon.SelectedRows[0];

                    // Lấy DataTable từ DataSource của DataGridView (giả sử DataSource là DataTable)
                    DataTable dataTable = (DataTable)dataGridViewGoiMon.DataSource;

                    // Xác định index của dòng đang chọn trong DataTable
                    int rowIndex = dataGridViewGoiMon.SelectedRows[0].Index;

                    // Kiểm tra index có hợp lệ không
                    if (rowIndex >= 0 && rowIndex < dataGridViewGoiMon.Rows.Count)
                    {
                        // Xóa dòng từ DataSource
                        dataGridViewGoiMon.Rows.RemoveAt(rowIndex);

                        // Gán lại DataSource mới cho DataGridView
                        dataGridViewGoiMon.DataSource = dataTable;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        // Hàm xử lý tính toán lại ThanhTien
        private void TinhToanThanhTien(object sender, EventArgs e)
        {
            int tongTien = 0;
            foreach (DataGridViewRow row in dataGridViewGoiMon.Rows)
            {
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                int giaSP = Convert.ToInt32(row.Cells["GiaSP"].Value);
                int thanhTien = soLuong * giaSP;
                tongTien += thanhTien;
                row.Cells["ThanhTien"].Value = thanhTien;
            }
            labelTongTien.Text = string.Format("{0:N0} VNĐ", tongTien);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                luuDataBaseGoiMon((DataTable)dataGridViewGoiMon.DataSource);
                luuDataBaseHoaDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            try
            {
                FormThanhToan tt = new FormThanhToan();
                OpenFormBanHang(tt, labelMaHoaDon.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenFormBanHang(FormThanhToan childForm, string mahd)
        {
            childForm.Tag = mahd;
            childForm.Show();
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            FormNhapMaBan formNhapMaBan = new FormNhapMaBan();
            formNhapMaBan.ShowDialog(); // Hiển thị Form nhập mã bàn

            string maBanCanChuyen = formNhapMaBan.MaNhap; // Lấy mã bàn từ Form nhập mã bàn

            if (!string.IsNullOrEmpty(maBanCanChuyen))
            {
                // Xử lý mã bàn đã nhập
                PictureBox pictureBoxTimDuoc = null;

                foreach (var groupBox in this.Controls.OfType<GroupBox>())
                {
                    pictureBoxTimDuoc = groupBox.Controls.OfType<PictureBox>().FirstOrDefault(p => p.Tag.ToString() == maBanCanChuyen);

                    if (pictureBoxTimDuoc != null)
                        break;
                }
                if (pictureBoxTimDuoc != null)
                {
                    //Đầu tiên bàn tìm được phải trống
                    if (pictureStates[pictureBoxTimDuoc].TrangThai == "Trống")
                    {
                        DBConnect db = new DBConnect();
                        //Cho bàn hiện tại về bàn trống
                        pictureStates[currentSelectedPictureBox].TrangThai = "Trống";
                        //Ảnh hiện tại về ảnh trống
                        currentSelectedPictureBox.Image = coffeeTrong;
                        //Hoán đổi mã hóa đơn cho nhau
                        pictureStates[pictureBoxTimDuoc].MaHoaDon = pictureStates[currentSelectedPictureBox].MaHoaDon;
                        pictureStates[currentSelectedPictureBox].MaHoaDon = "";
                        //Lưu lại dữ liệu
                        string maBan1 = currentSelectedPictureBox.Tag.ToString();
                        string ctv1 = "Update BAN set TrangThai = N'Trống' where MaBan = '" + maBan1 + "'";
                        string ctv2 = "Update BAN set HDHienTai = null where MaBan = '" + maBan1 + "'";
                        db.getNonQuery(ctv1);
                        db.getNonQuery(ctv2);
                        //Thay đổi bàn đang chọn là picturebox tìm được
                        currentSelectedPictureBox = pictureBoxTimDuoc;
                        //Cho bàn vừa chuyển về bàn đang dùng
                        pictureStates[currentSelectedPictureBox].TrangThai = "Đang dùng";
                        //Ảnh bàn vừa chuyển là đang dùng
                        currentSelectedPictureBox.Image = coffeeDangDung;
                        //Tắt nút kích hoạt chuyển bàn
                        tatKichHoatNut(btnChuyenBan);
                        //Lưu lại dữ liệu
                        //Tạo mới hóa đơn và đổ lên grid view
                        string maBan2 = currentSelectedPictureBox.Tag.ToString();
                        string ctv3 = "Update BAN set TrangThai = N'Đang dùng' where MaBan = '" + maBan2 + "'";
                        string ctv4 = "Update BAN set HDHienTai = " + labelMaHoaDon.Text + " where MaBan = '" + maBan2 + "'";
                        db.getNonQuery(ctv3);
                        db.getNonQuery(ctv4);
                        //Thay đổi mã bàn trong hóa đơn
                        string ctv5 = "Update HOADON set MaBan = '" + maBan2 + "' where MaHDBH = "+labelMaHoaDon.Text+"";
                        db.getNonQuery(ctv5);
                    }
                    else
                    {
                        MessageBox.Show("Bàn đang sử dụng không thể chuyển");
                    }
                }
                else
                {
                    // Không tìm thấy PictureBox có mã bàn như mong đợi
                    MessageBox.Show("Không tìm thấy mã bàn chuyển đến");
                }
            }
        }
    }
}
