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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Collections.Specialized;
namespace PLWPF.tester
{
    /// <summary>
    /// Interaction logic for SpecialRequestsUC.xaml
    /// </summary>
    public partial class SpecialRequestsUC : UserControl
    {

        IBL bl = BL.FactoryBL.getBL();
        Tester thisTester;
        string keepTesterName;
        string eMail = ConfigurationManager.AppSettings.Get("Email");
        string eMail_password = ConfigurationManager.AppSettings.Get("Password");

        public SpecialRequestsUC(Tester sentTester)
        {
            thisTester = sentTester;
            InitializeComponent();
            Application.Current.Windows.OfType<TESTER>().SingleOrDefault(x => x.IsActive).UpdatePageName("Special Requests");

            keepTesterName = thisTester.Name + " " + thisTester.LName;
            TesterName.Text = keepTesterName;
            this.DataContext = thisTester;
            InitializeComponent();
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {


            e.Handled = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(Email.Text);
            if (e.Handled == false)
                this.Email.BorderBrush = Brushes.Red;
            else
                this.Email.BorderBrush = Brushes.LightSlateGray;

          

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(Email.Text);
           
             TextRange textRange = new TextRange(MyNote.Document.ContentStart, MyNote.Document.ContentEnd);//convert rich text to string
            if ((e.Handled == false) || textRange.Text.Count()==0) //*MyNote.Text.Count() == 0*/)
            {
                MessageBoxProject x = new MessageBoxProject("Attention", "email can not send without valid address or message");
                x.ShowDialog();
            }

            else
            {

                string to = eMail; //send mail to the Admin
                                                     //string mailbody = @"new password: hhh";
                                                     // string read = @"C:\Users\moriy\Downloads\Telegram Desktop\09-01-19\PLWPF\pictures\SentMessage.html";


                //to make sure the mail will work on any other computers:
                string keep = System.Environment.CurrentDirectory;
                const string removeString = @"\bin\Debug";
                string read = keep.Remove(keep.IndexOf(removeString), removeString.Length) + @"\pictures\SentSPecialReq.html";


               // string read = @"\pictures\SentSPecialReq.html";
                // string read = @"D:\EVENT.html";

                string mailbody = File.ReadAllText(read);

                mailbody = mailbody.Replace("###ChangeNAME###", keepTesterName);
                mailbody = mailbody.Replace("###ChangeMAIL###", Email.Text.ToString());
                mailbody = mailbody.Replace("###MessageHERE###", textRange.Text.ToString());


                string from = eMail;
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Tester's Choice--> Special Request from-" + keepTesterName;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = mailbody;


                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                NetworkCredential basicCredential = new NetworkCredential( eMail,eMail_password);
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = basicCredential;


                try
                {
                    client.Send(message);
                    //  empty message box and hide it
                    textRange.Text = "";
                    Email.Text = "";
                    SendMessageBox.Visibility = Visibility.Hidden;
                    NotRecivedMail.Visibility = Visibility.Hidden;//in case it was open before

                    AboutTheMail.Visibility = Visibility.Visible;
                }

                catch (Exception)
                {
                    //  MessageBox.Show("email not recived");
                    NotRecivedMail.Visibility = Visibility.Visible;
                }
            }
        }

       

        private void newMail_Click(object sender, RoutedEventArgs e)
        {

            AboutTheMail.Visibility = Visibility.Hidden; //hide it again
            SendMessageBox.Visibility = Visibility.Visible; //new message

        }
    }
}
