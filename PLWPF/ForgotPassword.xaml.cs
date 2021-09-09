using BE;
using BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
       

        IBL bl = BL.FactoryBL.getBL();
        Trainee ThisTrainee = new Trainee();
        Tester ThisTester = new Tester();
    public ForgotPassword(string Id)
    {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN

            InitializeComponent();

            string UserId = Id;


        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow pUc = new MainWindow();
            //pUc.Show();

            Close();


        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {//we can drag the window by a left click mouse
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(email.Text);
            if (e.Handled == false)
                this.email.BorderBrush = Brushes.Red;
            else
                this.email.BorderBrush = Brushes.LightSlateGray;
        }

        private void newPassword_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(email.Text);
            int temp; int choice;
            //check if email and id valid:
            if ((e.Handled == false) || IDtoReset.Text.Count() < 9)
            {
                MessageBoxProject x = new MessageBoxProject("Attention", "email can not send without valid address or ID");
                x.ShowDialog();
                return;
            }


            //finding the user in the lists
            try
            {
                temp = bl.FindTrainee(IDtoReset.Text);
                choice = 1;
            }
            catch
            {
                try
                {
                    temp = bl.FindTester(IDtoReset.Text);
                    choice = 2;
                }
                catch
                {
                    MessageBoxProject sendMassege = new MessageBoxProject("Attention", "This ID does not exist in the system");
                    sendMassege.ShowDialog();
                    return;
                }
            }

          //  if everything is ok, send the mail:
            string to = email.Text; //send mail to the Admin


            //to make sure the mail will work on any other computers:
            string keep = System.Environment.CurrentDirectory; 
            const string removeString = @"\bin\Debug";         
            string read = keep.Remove(keep.IndexOf(removeString), removeString.Length) + @"\pictures\NewMail.html";

            string mailbody = File.ReadAllText(read);

            if (choice == 1)
            {
                mailbody = mailbody.Replace("###ChangeNAME###", bl.getTrainnes()[temp].Name);// his Name
                mailbody = mailbody.Replace("###ChangePassword###", bl.getTrainnes()[temp].Password);// his new password
            }
            else
            {
                mailbody = mailbody.Replace("###ChangeNAME###", bl.getTesters()[temp].Name);// his Name
                mailbody = mailbody.Replace("###ChangePassword###", bl.getTesters()[temp].Password);// his new password
            }




            string from = "HelpMail384@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Tester's Choice--> Reset Password";
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = mailbody;


            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            NetworkCredential basicCredential = new NetworkCredential("HelpMail384@gmail.com", "Help_Mail332");
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.Credentials = basicCredential;


            try
            {
                client.Send(message);
                MessageBoxProject sendMassege = new MessageBoxProject("Attention", "email sent succefully ");
                sendMassege.ShowDialog();
                Close();


            }
            catch (Exception)
            {
                MessageBoxProject sendMassege = new MessageBoxProject("Attention", "email not recived");
                sendMassege.ShowDialog();
            }
        }

        //text input-->only numbers
        private void LettersBlock_Textinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void email_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;

            newPassword_Click(sender, e);

        }
    }
}
