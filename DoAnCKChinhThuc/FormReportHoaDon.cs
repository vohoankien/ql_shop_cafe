using CrystalDecisions.Shared;
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
    public partial class FormReportHoaDon : Form
    {
        public FormReportHoaDon()
        {
            InitializeComponent();
        }
        public string maHDBH = "10";
        public int tienKhachDua = 0;
        public int tienThoi = 0;
        public int soTienThanhToan = 0;
        public int dungDiemTL = 0;

        DataTable loadDuLieuHD()
        {
            string ctv = "select * from CHITIETHOADON " +
                         "left join HOADON on CHITIETHOADON.MaHDBH = HOADON.MaHDBH " +
                         "left join HANGHOA on HANGHOA.MaHH = CHITIETHOADON.MaHH " +
                         "where CHITIETHOADON.MaHDBH = " + maHDBH + "";
            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            dt = db.getDataTable(ctv);
            return dt;
        }

        private void FormReportHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                HoaDon hd = new HoaDon();
                //Report vừa tạo
                hd.SetDataSource(loadDuLieuHD());
                // Truyền giá trị vào từng Parameter Fields
                ParameterFields paramFields = new ParameterFields();

                // Parameter Field 1
                ParameterField paramField1 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

                paramField1.Name = "tienKhachDua"; // Tên của Parameter Field 1 trong báo cáo
                paramDiscreteValue1.Value = tienKhachDua; // Giá trị từ textbox
                paramField1.CurrentValues.Add(paramDiscreteValue1);
                paramFields.Add(paramField1);

                // Parameter Field 2
                ParameterField paramField2 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue2 = new ParameterDiscreteValue();

                paramField2.Name = "tienThua"; // Tên của Parameter Field 2 trong báo cáo
                paramDiscreteValue2.Value = tienThoi; // Giá trị mong muốn
                paramField2.CurrentValues.Add(paramDiscreteValue2);
                paramFields.Add(paramField2);

                // Parameter Field 3
                ParameterField paramField3 = new ParameterField();
                ParameterDiscreteValue paramDiscreteValue3 = new ParameterDiscreteValue();

                paramField3.Name = "tienDungDiem"; // Tên của Parameter Field 3 trong báo cáo
                paramDiscreteValue3.Value = dungDiemTL; // Giá trị mong muốn
                paramField3.CurrentValues.Add(paramDiscreteValue3);
                paramFields.Add(paramField3);

                crystalReportViewer1.ParameterFieldInfo = paramFields; // Hiển thị báo cáo trong CrystalReportViewer
                //hd.Parameter_TienKhachDua = tienKhachDua;
                //hd.Parameter_DiemDung.CurrentValues = ;
                //Hiển thị báo cáo
                //FormReportHoaDon f = new FormReportHoaDon();
                //f.crystalReportViewer1.ReportSource = loadDuLieuHD();
                //HoaDon rpt = new HoaDon();
                crystalReportViewer1.ReportSource = hd;
                crystalReportViewer1.DisplayStatusBar = false;
                crystalReportViewer1.DisplayToolbar = true;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
