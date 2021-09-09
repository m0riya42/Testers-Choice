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
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using BL;
using BE;
using System.Windows.Media.Converters;


namespace PLWPF.trainee
{
    /// <summary>
    /// Interaction logic for personalInfoUC.xaml
    /// </summary>
    public partial class personalInfoUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        Trainee thisTrainee;
        CarType keepCarType; Gearbox keepGearbox;
        // int count;//  counter to count the number of time we enter the event "CarChange_SelectionChanged"
        bool flagCar;
        public personalInfoUC(Trainee sentTrainee)
        {
            InitializeComponent();
            //changing the name of the page
            System.Windows.Application.Current.Windows.OfType<TRAINEE>().SingleOrDefault(x => x.IsActive).UpdatePageName("Personal Information");


            //find the trainee from the list---> we need it so if there is changes the trainee we'll see will be the updated one.
            int ind = bl.FindTrainee(sentTrainee.Id);
            thisTrainee = bl.getTrainnes()[ind].ShallowCopy();

            //keep for checking changes
            keepCarType = thisTrainee.car;
            keepGearbox = thisTrainee.gearbox;
            flagCar = false;// first enter

            this.DataContext = thisTrainee;
            this.Gender.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.CarTypeC.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            this.GearBoxC.ItemsSource = Enum.GetValues(typeof(BE.Gearbox));
        }
       

        private void changePicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
               thisTrainee.ImageSource = f.FileName;

                this.profile.Source = new BitmapImage(new Uri(f.FileName));
                //string p =f.FileName;
                //this.profile.ImageSource = new BitmapImage(new Uri(p));

            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                thisTrainee.address = new Address { street = streetFill.Text, BuildNum = int.Parse(buildingFill.Text), city =cityFill.Text };
                bl.UpdateStudent(thisTrainee);

                MessageBoxProject x = new MessageBoxProject("Attention", "Updated succefually");
                x.ShowDialog();
            }
            catch(Exception ep)
            {
                // MessageBox.Show(ep.Message);
                MessageBoxProject x = new MessageBoxProject("Attention", ep.Message);
                x.ShowDialog();
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

        //private void lessonsFill_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    bool flag = true;
        //    for (int i = 0; i < lessonsFill.Text.Length; i++)
        //        if ((lessonsFill.Text[i] < 48) || (lessonsFill.Text[i] > 57))//if the char is not between the ascii code of the digits
        //        {
        //            flag = false;
        //            lessonsFill.BorderBrush = Brushes.Red;
        //        }

        //    if (flag)
        //        this.lessonsFill.BorderBrush = Brushes.LightSlateGray;
        //}

        private void LettersBlock_Textinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


        private void CarChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //carType or GearBox changes
            if (flagCar) //if not first enter
            {
                if (CarTypeC.SelectedItem.ToString() != keepCarType.ToString() || (GearBoxC.SelectedItem.ToString() != keepGearbox.ToString()))
                    changeCarText.Visibility = Visibility.Visible;

            }

           else  flagCar = true;


            //count++;
            //if (count>2)
        }


    }
    }

