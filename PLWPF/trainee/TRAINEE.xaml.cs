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
using BL;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TRAINEE.xaml
    /// </summary>
    public partial class TRAINEE : Window
    {

        //------->>>>>>>>>>>>>>important properties
        Trainee thisTrainee;
     
        public TRAINEE(Trainee sentTrainee)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN

            thisTrainee = sentTrainee;
       
            InitializeComponent();
            hello.Content = " Hello "+sentTrainee.Name;
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
           trainee.personalInfoUC pUc = new trainee.personalInfoUC(thisTrainee);
            this.Pages.Content = pUc;

        }

        private void TestReg_Click(object sender, RoutedEventArgs e)
        {
            trainee.TestRegestrationUC pUc = new trainee.TestRegestrationUC(thisTrainee);
            this.Pages.Content = pUc;
        }

        private void TestInfo_Click(object sender, RoutedEventArgs e)
        {
            trainee.TestInformationUC pUc = new trainee.TestInformationUC(thisTrainee);
            this.Pages.Content = pUc;
        }

        private void MyTests_Click(object sender, RoutedEventArgs e)
        {
            trainee.MyTestsUC pUc = new trainee.MyTestsUC(thisTrainee);
            this.Pages.Content = pUc;
        }

        private void LogOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ad = new MainWindow();
            ad.Show();
            Close();
        }
    }
}
