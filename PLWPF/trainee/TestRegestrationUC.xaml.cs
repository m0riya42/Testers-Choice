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
    /// <summary>
    /// Interaction logic for TestRegestrationUC.xaml
    /// </summary>
    public partial class TestRegestrationUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        Trainee thisTrainee;
        string[] ChosenHourArray = { "09:00", "10:00", "11:00","12:00","13:00","14:00","15:00"};

        public TestRegestrationUC(Trainee sentTrainee)
        {
            InitializeComponent();

            //changing the name of the page
            System.Windows.Application.Current.Windows.OfType<TRAINEE>().SingleOrDefault(x => x.IsActive).UpdatePageName("Test Registration");


            //find the trainee from the list---> we need it so if there is changes the trainee we'll see will be the updated one.
         
            thisTrainee = bl.getTrainnes()[bl.FindTrainee(sentTrainee.Id)];
          //  thisTrainee = sentTrainee;

            //DateForTest.BlackoutDates
            chooseHour.ItemsSource= ChosenHourArray.ToArray();
        }

      
        private void Address_GotFocus(object sender, RoutedEventArgs e)
        {
            //when we put the focus on the text box the default text will disapear
            if ((((TextBox)sender).Text).Length == 0)
            {  ((TextBox)sender).Text= "";       
                if (((TextBox)sender).Name == "street")
                    OnStreet.Visibility = Visibility.Hidden;
                else if (((TextBox)sender).Name == "building")
                    OnBuilding.Visibility = Visibility.Hidden;
                else if (((TextBox)sender).Name == "city")
                    OnCity.Visibility = Visibility.Hidden;
            }
         }

            

        private void Address_LostFocus(object sender, RoutedEventArgs e)
        {
            //when we leaving the focus on the text box the default text will come back
            if ((((TextBox)sender).Text).Length == 0)
            {
                if (((TextBox)sender).Name == "street")
                    OnStreet.Visibility = Visibility.Visible;
                else if (((TextBox)sender).Name == "building")
                    OnBuilding.Visibility = Visibility.Visible;
                if (((TextBox)sender).Name == "city")
                    OnCity.Visibility = Visibility.Visible;
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            string hour = DateForTest.Text + " " + chooseHour.Text;

           // MessageBox.Show(hour);
           //    DateTime d=  DateTime.Parse(hour);
            try
            {
                if (DateTime.Parse(DateForTest.Text) < DateTime.Now)
                    throw new Exception("It's too late to set a test");

                 thisTrainee = bl.getTrainnes()[bl.FindTrainee(thisTrainee.Id)];

             bl.CheckValidTester(thisTrainee, DateTime.Parse(hour), new Address { street = street.Text, BuildNum = int.Parse(building.Text), city = city.Text });

                Test thisTest = thisTrainee.MyTests[thisTrainee.MyTests.Count - 1]; //keep this test

                DateProperty.Content = thisTest.DateAndHour; //print date
                AddProperty.Content = thisTest.StartTest.street + " " + thisTest.StartTest.BuildNum + " " + thisTest.StartTest.city;


                //change Grid:
                SetTest.Visibility = Visibility.Hidden;
                AboutTheTest.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                MessageBoxProject x = new MessageBoxProject("Attention", ex.Message);
                x.ShowDialog();
            }
        }

        private void city_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;

            update_Click(sender, e);
        }
    }
}
