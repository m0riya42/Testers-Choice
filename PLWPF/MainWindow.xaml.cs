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
using PLWPF.trainee;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BL.FactoryBL.getBL();

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN
            InitializeComponent();
            // Create the application's main window

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


        private void Trainee_Window(Trainee t)
        {

            TRAINEE stud = new TRAINEE(t);
            stud.Show();
            Close();
        }

        private void Tester_Window(Tester t)
        {
            //   TESTER stud = new TESTER(bl.getTrainnes()[0]);
            tester.TESTER thisOne = new tester.TESTER(t);
            thisOne.Show();
            Close();
        }

        private void Admin_Window()
        {
            Admin.ADMIN ad = new Admin.ADMIN();
            ad.Show();
            Close();
        }

        private void Sumbit_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text == "123456789" && passwordBox.Password == "1111111111")//if it is the admin
            {
                Admin_Window();
                return;
            }
            try
            {
                Trainee temp = bl.getTrainnes()[bl.FindTrainee(IDBox.Text)];
                if (temp.Password != passwordBox.Password)
                    throw new Exception("The password you entered is incorrect");
                Trainee_Window(temp);
            }

            catch 
            {
                try
                {
                    Tester temp = bl.getTesters()[bl.FindTester(IDBox.Text)];
                    if (temp.Password != passwordBox.Password)
                        throw new Exception("The password you entered is incorrect");
                    Tester_Window(temp);
              }
                catch
                {
                    MessageBoxProject sendMassege = new MessageBoxProject("Attention", "This ID does not exist in the system");
                    sendMassege.ShowDialog();
                }
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration re = new Registration();
            re.ShowDialog();
           // Close();
        }

        private void ForgetPasswordLabel_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword fe = new ForgotPassword(IDBox.Text);
            fe.ShowDialog();
          //  Close();
        }

        private void passwordBox_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;

            Sumbit_Click(sender, e);


    }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenPage x = new OpenPage();
            x.Show();
        }

        //private void Window_KeyUp(object sender, KeyEventArgs e)
        //{

        //    if (e.Key != System.Windows.Input.Key.PrintScreen) return;

        //    this.WindowState = WindowState.Minimized;
        //}
        //statics
        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    STATICS X = new STATICS(bl.getTrainnes()[1]);
        //    X.Show();
        //    Close();
        //}
    }
}
