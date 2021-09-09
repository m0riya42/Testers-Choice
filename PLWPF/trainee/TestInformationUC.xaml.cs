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
using BL;
using BE;

namespace PLWPF.trainee
{
    public partial class TestInformationUC : UserControl
    {

        IBL bl = BL.FactoryBL.getBL();
        Trainee thisTrainee;
        Test thisTest;

        public TestInformationUC(Trainee sentTrainee)
        {
            InitializeComponent();
            System.Windows.Application.Current.Windows.OfType<TRAINEE>().SingleOrDefault(x => x.IsActive).UpdatePageName("Test Information");

            int ind = bl.FindTrainee(sentTrainee.Id);
            thisTrainee = bl.getTrainnes()[ind].ShallowCopy();

            if (bl.ISFutureTest(thisTrainee))
            {
                HasTest.Visibility = Visibility.Visible;
                 thisTest = thisTrainee.MyTests[thisTrainee.MyTests.Count - 1]; //save the last test;
                this.DataContext = thisTest;
                address2.Content = thisTest.StartTest.street + " " + thisTest.StartTest.BuildNum + " " + thisTest.StartTest.city;
            }

            else
            {
                HasTest.Visibility = Visibility.Hidden;
                NoTest.Visibility = Visibility.Visible;
            }

        }

        private void deleteTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteTest(thisTest.numberTest); //delete the test from the big list+ the tester and trainee lists
                HasTest.Visibility = Visibility.Hidden;
                NoTest.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                MessageBoxProject x = new MessageBoxProject("Attention", ex.Message);
                x.ShowDialog();
            }
        }
    }
}
