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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        IBL bl = BL.FactoryBL.getBL();
        Trainee ThisTrainee = new Trainee();
        Tester ThisTester = new Tester();

        bool[,] keepButton = new bool[7, 5];
        public Registration()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN

            InitializeComponent();
            birthdayFill.SelectedDate = new DateTime(1928, 01, 01);


            this.Gender.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.CarType.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            this.GearBox.ItemsSource = Enum.GetValues(typeof(BE.Gearbox));

            //create schedual table
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 5; j++)
                {

                    Button t = new Button();
                    t.Name = "G" + i + j;
                    t.Click += T_Click;
                    //t.Style = (Style)App.Current.Resources["SCButtonStyle"];

                    t.Background = Brushes.Transparent;
                    t.BorderBrush = new BrushConverter().ConvertFromString("#0c1f31") as SolidColorBrush;
                    t.BorderThickness = new Thickness(1.0);

                    Grid.SetColumn(t, j + 1);
                    Grid.SetRow(t, i + 1);
                    sched.Children.Add(t);

                }


        }


        /////////////////////////------>>>Regular events:
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {//we can drag the window by a left click mouse
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        //------------------------->>>


        #region All Other Events

        // string lost focos
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
        private void fnameFill_LostFocus(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            for (int i = 0; i < this.lnameFill.Text.Length; i++)
            {
                if (((this.fnameFill.Text[i] < 65) || (this.fnameFill.Text[i] > 90)) && ((this.fnameFill.Text[i] > 122) || (this.fnameFill.Text[i] < 97)))//if the char is not between the ascii code of the characters
                { this.fnameFill.BorderBrush = Brushes.Red; flag = false; }
            }
            if (flag)
                this.fnameFill.BorderBrush = Brushes.LightSlateGray;

        }



        //text input-->only numbers
        private void LettersBlock_Textinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        //select user: tester/trainee
        private void choiceCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)choiceCombo.SelectedItem).Content.ToString() != "")
            {
                switch (((ComboBoxItem)choiceCombo.SelectedItem).Content.ToString())
                {


                    case "Trainee":
                        {
                            PropertiesGrid.DataContext = ThisTrainee;
                            TraineeGrid.DataContext = ThisTrainee;

                            TraineeGrid.Visibility = Visibility.Visible;
                            createUser.Visibility = Visibility.Visible;//button

                            choiceCombo.IsEnabled = false;//block type choice
                            birthdayFill.SelectedDate = new DateTime(1928, 01, 01);

                            buildingFill.Text = "0";
                            lessonsFill.Text = "0";
                            break;
                        }
                    case "Tester":
                        {
                            PropertiesGrid.DataContext = ThisTester;
                            TesterGrid.DataContext = ThisTester;

                            TesterGrid.Visibility = Visibility.Visible;
                            createUser.Visibility = Visibility.Visible;//button
                            choiceCombo.IsEnabled = false; //block type choice
                            birthdayFill.SelectedDate = new DateTime(1949, 01, 01);

                            buildingFill.Text = "0";



                            break;
                        }
                }
            }
        }


        //buttons:
        private void createUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check global empty fields
                if (idFill.Text.Count() < 9 || fnameFill.Text.Count() == 0 || lnameFill.Text.Count() == 0 || phoneFill.Text.Count() == 0 || streetFill.Text.Count() == 0 || cityFill.Text.Count() == 0)
                    throw new Exception("Can not Save new User while all the field is not updated");




                // new address-->to update the user address
                Address p = new Address { BuildNum = int.Parse(buildingFill.Text), city = cityFill.Text, street = streetFill.Text};
                if (((ComboBoxItem)choiceCombo.SelectedItem).Content.ToString() == "Tester")
                {
                    //save new user:

                    if (MaxDistanceFill.Text == "0")
                        throw new Exception("Update Distance field");
                    if (MaxTestFill.Text == "0")
                        throw new Exception("Update Max Test field");

                    ThisTester.address = p;
                    bl.addTester(ThisTester);


                    //Create new password:
                    GetPassword.Visibility = Visibility.Visible;
                    ChooseType.Visibility = Visibility.Hidden;
                    PropertiesGrid.Visibility = Visibility.Hidden;
                    TesterGrid.Visibility = Visibility.Hidden;
                    createUser.Visibility = Visibility.Hidden;//button

                }
                if (((ComboBoxItem)choiceCombo.SelectedItem).Content.ToString() == "Trainee")
                {
                    //save new user:
                    ThisTrainee.address = p;
                    bl.addStudent(ThisTrainee);


                    //Create new password:
                    GetPassword.Visibility = Visibility.Visible;
                    ChooseType.Visibility = Visibility.Hidden;
                    PropertiesGrid.Visibility = Visibility.Hidden;
                    TraineeGrid.Visibility = Visibility.Hidden;
                    createUser.Visibility = Visibility.Hidden;//button


                }
            }
            catch (Exception ex)
            {
                MessageBoxProject pg = new MessageBoxProject("Attention", ex.Message);
                pg.ShowDialog();
            }
        }
        private void ConfirmPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PasswordBox.Password != ConfirmPasswordBox.Password)
                    throw new Exception();
                if (((ComboBoxItem)choiceCombo.SelectedItem).Content.ToString() == "Tester")
                {
                    ThisTester.Password = PasswordBox.Password;
                    bl.UpdateTester(ThisTester);

                    GetPassword.Visibility = Visibility.Hidden;
                    TesterSchedualCanvas.Visibility = Visibility.Visible;
                    //save password and open schedual grid
                }
                if (((ComboBoxItem)choiceCombo.SelectedItem).Content.ToString() == "Trainee")
                {
                    ThisTrainee.Password = PasswordBox.Password;
                    bl.UpdateStudent(ThisTrainee);

                    MessageBoxProject PG = new MessageBoxProject("", "User successufully saved to system");
                    PG.ShowDialog();
                    Close(); //save password and get out
                }
            }
            catch
            {
                ConfirmPasswordBox.BorderBrush = Brushes.Red;
            }
        }


        private void T_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if ((sender as Button).Background == Brushes.Transparent)

                {
                    //special input
                    BitmapImage bitimg = new BitmapImage();
                    bitimg.BeginInit();
                    bitimg.UriSource = new Uri(@"/pictures/v2.png", UriKind.RelativeOrAbsolute);
                    bitimg.EndInit();
                    Image img = new Image();
                    img.Stretch = Stretch.UniformToFill;
                    img.Source = bitimg;

                    // Set Button.Content
                    (sender as Button).Content = img;

                    // Set Button.Background
                    (sender as Button).Background = new ImageBrush(bitimg);
                    //  MessageBox.Show(((sender as Button).Name[1])+" "+ ((sender as Button).Name[2]));
                    //   int i= (sender as Button).Name[1]-'0';
                    keepButton[(sender as Button).Name[1] - '0', (sender as Button).Name[2] - '0'] = true;

                }
                else
                {

                    (sender as Button).Content = null;
                    (sender as Button).Background = Brushes.Transparent;
                    // MessageBox.Show(((sender as Button).Name[1]) + " " + ((sender as Button).Name[2]));
                    keepButton[(sender as Button).Name[1] - '0', (sender as Button).Name[2] - '0'] = false;


                }

            }
            catch (Exception ex)
            {

                MessageBoxProject pg = new MessageBoxProject("Attention", ex.Message);
                pg.ShowDialog();
            }
        }   // button schedual click event
        private void UpdateHours_Click(object sender, RoutedEventArgs e)// updating schedual 
        {

            //create schedual table
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 5; j++)
                {
                    ThisTester.WorkDay[i, j] = keepButton[i, j];
                }
            bl.UpdateTester(ThisTester);
            MessageBoxProject PG = new MessageBoxProject("", "User successufully saved to system");
            PG.ShowDialog();
            Close();
        }


        //password strength:
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //mone++;
            PasswordScore temp = bl.CheckStrength((sender as PasswordBox).Password);
            if (temp.ToString() == "Blank")
            {
                ProgressBarIndicator.Value = 0;
                scoreDisplayText.Content = "Blank";

            }
            if (temp.ToString() == "VeryWeak")
            {
                ProgressBarIndicator.Value = 33;
                scoreDisplayText.Content = "Very Weak";

            }
            if (temp.ToString() == "Weak")
            {
                ProgressBarIndicator.Value = 45;
                scoreDisplayText.Content = "Weak";

            }
            if (temp.ToString() == "Medium")
            {
                ProgressBarIndicator.Value = 60;
                scoreDisplayText.Content = "Medium";

            }
            if (temp.ToString() == "Strong")
            {
                ProgressBarIndicator.Value = 75;
                scoreDisplayText.Content = "Strong";

            }
            if (temp.ToString() == "VeryStrong")
            {
                ProgressBarIndicator.Value = 100;
                scoreDisplayText.Content = "Very Strong";


            }

        }
        #endregion
        private void Reg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;

                createUser_Click(sender, e);
            }
        }





    }

}

