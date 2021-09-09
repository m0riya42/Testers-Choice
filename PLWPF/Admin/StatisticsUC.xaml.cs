using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using BL;
using BE;
namespace PLWPF.Admin
{
    /// <summary>
    /// Interaction logic for StatisticsUC.xaml
    /// </summary>
    public partial class StatisticsUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        public StatisticsUC()
        {
            InitializeComponent();

            #region chart-->pie num of users
            LiveCharts.SeriesCollection psc = new LiveCharts.SeriesCollection();
            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            PieSeries Trainees = new LiveCharts.Wpf.PieSeries
            {
                Values = new LiveCharts.ChartValues<decimal> { bl.getTrainnes().Count() },
            };
            Trainees.Title = "Trainnes";
            Trainees.DataLabels = true;
            Trainees.LabelPoint = PointLabel;
            Trainees.Fill = new BrushConverter().ConvertFromString("#4b384c") as SolidColorBrush;
            PieSeries Testers = new LiveCharts.Wpf.PieSeries
            {
                Values = new LiveCharts.ChartValues<decimal> { bl.getTesters().Count() },
            };
            Testers.Title = "Testers";
            Testers.DataLabels = true;
            Testers.LabelPoint = PointLabel;
            Testers.Fill = new BrushConverter().ConvertFromString("#a7a2c2") as SolidColorBrush;

            psc.Add(Trainees);
            psc.Add(Testers);


            foreach (LiveCharts.Wpf.PieSeries ps in psc)
            {
                myPieChart.Series.Add(ps);
            }
            #endregion
            #region chart-->pie num of ttests
            LiveCharts.SeriesCollection tst = new LiveCharts.SeriesCollection();
            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            //---> move to ibl
            //var p = from item in bl.getPassedTests()
            //        where item.FinalOutcome == true
            //        select item;
            //var h = from item in bl.getPassedTests()
            //        where item.FinalOutcome == false
            //        select item;

            PieSeries FailedTests = new LiveCharts.Wpf.PieSeries
            {
                Values = new LiveCharts.ChartValues<decimal> {bl.NotPassedTests().Count() },
            };
            FailedTests.Title = "Failed Tests";
            FailedTests.DataLabels = true;
            FailedTests.LabelPoint = PointLabel;
            FailedTests.Fill = new BrushConverter().ConvertFromString("#22ab99") as SolidColorBrush;


            //   int passed= bl.getPassedTests() where 
            PieSeries PassedTests = new LiveCharts.Wpf.PieSeries
            {
                Values = new LiveCharts.ChartValues<decimal> { bl.PassedTests().Count() },
            };
            PassedTests.Title = "Passed Tests";
            PassedTests.DataLabels = true;
            PassedTests.LabelPoint = PointLabel;
            PassedTests.Fill = new BrushConverter().ConvertFromString("#8d32a0") as SolidColorBrush;
            tst.Add(FailedTests);
            tst.Add(PassedTests);


            foreach (LiveCharts.Wpf.PieSeries ps in tst)
            {
                myPieChart2.Series.Add(ps);
            }
            #endregion
            #region chart-->angularSection
            //to ibl--->1
            //int max = 0;
            //Tester keep = new Tester();
            //foreach (Tester t in bl.getTesters())
            //{
            //    if ((DateTime.Now.Year) - (t.Birthday.Year) > max)
            //    {
            //        max = (DateTime.Now.Year) - (t.Birthday.Year);
            //        keep = t;
            //    }
            //}

            //to ibl--->2
            // int
            //max = 0;
            //Trainee keep1 = new Trainee();
            //foreach (Trainee t in bl.getTrainnes())
            //{
            //    if ((DateTime.Now.Year) - (t.Birthday.Year) > max)
            //    {
            //        max = (DateTime.Now.Year) - (t.Birthday.Year);
            //        keep1 = t;
            //    }
            //}
            Trainee keep = bl.BigAgeTrainee();
            TraineeAge.Value = (DateTime.Now.Year) - keep.Birthday.Year;
            TraineeLabel.Content += keep.Name + " " + keep.LName;

            Tester keep1 = bl.BigAgeTester();
            TesterAge.Value = (DateTime.Now.Year) - keep1.Birthday.Year;
            TesterLabel.Content += keep1.Name + " " + keep1.LName;
            //hp.NeedleFill = new BrushConverter().ConvertFromString("#8d32a0") as SolidColorBrush;



            //     Section.NeedleFill = new BrushConverter().ConvertFromString("#8d32a0") as SolidColorBrush;
            #endregion
        }
        public Func<ChartPoint, string> PointLabel { get; set; }
        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
