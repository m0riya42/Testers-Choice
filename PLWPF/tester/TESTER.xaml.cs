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
namespace PLWPF.tester
{
    //------->>>>>>>>>>>>>>important properties
    public partial class TESTER : Window
    {
        Tester thisTester;
        DateTime KeepDate = DateTime.Now.Date;//אולי להוסיף תהליכון להמשך מניית הזמן

        public TESTER(Tester sentTester)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN

            thisTester = sentTester;
            InitializeComponent();
            hello.Content = "Hello " + sentTester.Name;        
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {//we can drag the window by a left click mouse
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //------------------------->>>here
        public void UpdatePageName(string newName)// func changing HomePageLabel name 
        {
            HomePageGrid.Content = newName;
        }


        //----------------------------------->>>>>change pages:


        private void PerInfo_Click(object sender, RoutedEventArgs e)//first page- personal information
        {

            PersonalInfoUC pUc = new PersonalInfoUC(thisTester);
            this.Pages.Content = pUc;
        }

      
        private void updateTest_Click(object sender, RoutedEventArgs e)
        {
            UpdateTestsUC pUc = new UpdateTestsUC(thisTester);
            this.Pages.Content = pUc;
        }

        private void futureT_Click(object sender, RoutedEventArgs e)
        {
            FutureTests pUc = new FutureTests(thisTester);
            this.Pages.Content = pUc;
        }

      

        private void SchedButton_Click(object sender, RoutedEventArgs e)
        {
            ScheduleUC pUc = new ScheduleUC(thisTester);
            this.Pages.Content = pUc;
        }

        private void PastTest_Click(object sender, RoutedEventArgs e)
        {
            AllPastTestsUC pUc = new AllPastTestsUC(thisTester);
            this.Pages.Content = pUc;
        }

        private void LogOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ad = new MainWindow();
            ad.Show();
            Close();
        }

        private void SpecialReq_Click(object sender, RoutedEventArgs e)
        {
            SpecialRequestsUC pUc = new SpecialRequestsUC(thisTester);
            this.Pages.Content = pUc;
        }
    }
}
