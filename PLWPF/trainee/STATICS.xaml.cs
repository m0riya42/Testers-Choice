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
using System.Windows.Shapes;
using BE;
using BL;
using LiveCharts;
using LiveCharts.Wpf;


namespace PLWPF.trainee
{
    /// <summary>
    /// Interaction logic for STATICS.xaml
    /// </summary>
    public partial class STATICS : Window
    {
       
        IBL bl = BL.FactoryBL.getBL();
        int[] keep;

        public STATICS(Trainee sentTrainee)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN
            InitializeComponent();
       

            //new chart:
            SeriesCollection = new SeriesCollection();

            int mone = 0; //count tests to the information at left
            foreach (Test t in sentTrainee.MyTests)
            {
                if (t.IsTestUpdate == true)
                {
                    mone++;
                    StackedColumnSeries TestX = new StackedColumnSeries(); //new stack to coulm--> for each test
                    TestX.StackMode = StackMode.Values; //mode--> value.. (ther are 2 options, one of them is precent)
                    TestX.Title = "Test " + mone + ": "; //test name


                    ChartValues<int> values1 = new ChartValues<int>();
                    keep = bl.StaticTest(t);//keep the results of the tests

                    for (int i = 0; i < 8; i++)
                    {
                        if (keep[i] == 1)// count failures
                            values1.Add(1);
                        else
                            values1.Add(0);

                    }
                          TestX.Values = values1;

                    //design the chart values:          
                    TestX.Stroke = Brushes.Black;
                    TestX.StrokeThickness = 1;

                    SeriesCollection.Add(TestX);// add to the chart

                }
            }
                //list of criterions:
                Labels = new[] { "Kept Distance", "reverse Parking", "mirrors", "signalls", "speed", "Tester Involved", "Enter To Juction", "Prepare To Drive" };
               
                Formatter = value => value + " Times failed"; //count failed criterion

                DataContext = this;
           
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }




        //--->>>events:
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {//we can drag the window by a left click mouse
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


    }
}
