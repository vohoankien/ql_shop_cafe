using CrystalDecisions.CrystalReports.Engine;
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
    public partial class FormReportThongKe : Form
    {
        public DataTable dt;
        public FormReportThongKe()
        {
            InitializeComponent();
        }
        private void FormReportThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                ThongKe report = new ThongKe();
                //report.Load("path_to_your_report_file"); // Load báo cáo

                report.SetDataSource(dt); // Đặt DataTable làm nguồn dữ liệu cho báo cáo
                //Report vừa tạo
                crystalReportViewer1.ReportSource = report;
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
