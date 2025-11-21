using QuanLySinhVien.DAL;
using QuanLySinhVien.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLySinhVien.GUI
{
    public partial class fThongKe : Form
    {
        public fThongKe()
        {
            InitializeComponent();
            LoadXepLoai();
        }
        private void LoadXepLoai()
        {
            chartXepLoai.Series.Clear();
            Series series = new Series("Xếp loại");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            chartXepLoai.Legends[0].Enabled = true;

            List<ThongKe> data = SinhVienDAL.DanhSachSinhVien
                .GroupBy(sv => sv.XepLoai )
                .Select(g => new ThongKe 
                { XepLoai = g.Key,
                  SoLuong = g.Count() })
                .ToList();

            foreach (ThongKe item in data)
            {
                series.Points.AddXY(item.XepLoai, item.SoLuong);
            }
            foreach (DataPoint p in series.Points)
            {
                double val = p.YValues[0];
                double total = series.Points.Sum(x => x.YValues[0]);
                double percent = val / total * 100;

                p.Label = $"{p.AxisLabel}: {percent:0.#}% ({val})";
            }
            chartXepLoai.Series.Add(series);
        }
    }
}
