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
using System.Text.RegularExpressions;
using BL;
using BE;
namespace PLWPF.tester
{
    /// <summary>
    /// Interaction logic for PersonalInfoUC.xaml
    /// </summary>
    public partial class PersonalInfoUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        Tester thisTester;

        CarType keepCarType;
        bool flagCar;
        //static int count = 0;// static counter to count the number of time we enter the event "CarChange_SelectionChanged"

        public PersonalInfoUC(Tester SentTester)
        {
            InitializeComponent();
            //changing the name of the page
            System.Windows.Application.Current.Windows.OfType<TESTER>().SingleOrDefault(x => x.IsActive).UpdatePageName("Personal Information");

            //find the tester from the list---> we need it so if there is changes the tester we'll see will be the updated one.
          int ind=  bl.FindTester(SentTester.Id);
            thisTester = bl.getTesters()[ind].ShallowCopy();

            //keep for checking changes
            keepCarType = thisTester.car;
            flagCar = false;// first enter

            // thisTester = SentTester;
            this.DataContext = thisTester;
            this.Gender.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.CarTypeC.ItemsSource = Enum.GetValues(typeof(BE.CarType));
        }


        private void changePicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
                thisTester.ImageSource = f.FileName;

                this.profile.Source = new BitmapImage(new Uri(f.FileName));
            }
        }

        private void lnameFill_LostFocus(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            for (int i = 0; i < this.lnameFill.Text.Length; i++)
            {
                if (((this.lnameFill.Text[i] < 65) || (this.lnameFill.Text[i] > 90)) && ((this.lnameFill.Text[i] > 122) || (this.lnameFill.Text[i] < 97)))//if the char is not between the ascii code of the characters
                { this.lnameFill.BorderBrush = Brushes.Red; flag = false; }
            }
            if (flag)
                this.lnameFill.BorderBrush = Brushes.LightSlateGray;

        }

        private void phoneFill_LostFocus(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (this.phoneFill.Text.Length < 10)
            {
                this.phoneFill.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (flag)
                this.phoneFill.BorderBrush = Brushes.LightSlateGray;
        }
        private void LettersBlock_Textinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void CarChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //carType changed
            if (flagCar) //if not first enter
            {
                if (CarTypeC.SelectedItem.ToString() != keepCarType.ToString() )
                    changeCarText.Visibility = Visibility.Visible;

            }

            else flagCar = true;
        }

        private void MaxTestFill_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.Parse(this.MaxTestFill.Text) > 35)
                this.MaxTestFill.BorderBrush = Brushes.Red;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                thisTester.address = new Address { street = streetFill.Text , BuildNum = int.Parse(buildingFill.Text), city =  cityFill.Text };
                bl.UpdateTester(thisTester);

                MessageBoxProject x = new MessageBoxProject("Attention", "Updated succefually");
                x.ShowDialog();
            }
            catch (Exception ep)
            {

                // MessageBox.Show(ep.Message);
                MessageBoxProject x = new MessageBoxProject("Attention", ep.Message);
                x.ShowDialog();
            }
        }
    }
}
