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

namespace PLWPF.Admin
{
    /// <summary>
    /// Interaction logic for ADMIN.xaml
    /// </summary>
    public partial class ADMIN : Window
    {
        public ADMIN()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN

            InitializeComponent();
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


      
        public void UpdatePageName(string newName)// func changing HomePageLabel name 
        {
            HomePageGrid.Content = newName;
        }


        //------------------------->>>here
        private void listOfTester_Click(object sender, RoutedEventArgs e)
        {
            ListOfTesterUC lst = new ListOfTesterUC();
            this.Pages.Content = lst;

        }

        private void listOfTrainee_Click(object sender, RoutedEventArgs e)
        {
            ShowAllTraineesUC pUc = new ShowAllTraineesUC();
            this.Pages.Content = pUc;
        }

        private void ListOfTests_Click(object sender, RoutedEventArgs e)
        {
            ListOfTestUC tst = new ListOfTestUC();
            this.Pages.Content = tst;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsUC pUc = new StatisticsUC();
            this.Pages.Content = pUc;
        }

        private void listOfTrainee_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void LogOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ad = new MainWindow();
            ad.Show();
            Close();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            DeleteUserUC dl = new DeleteUserUC();
            this.Pages.Content = dl;

        }


        //----------------------------------->>>>>change pages:






    }
}
