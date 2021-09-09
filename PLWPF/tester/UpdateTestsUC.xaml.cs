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
using BE;
using BL;

namespace PLWPF.tester
{

    public partial class UpdateTestsUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        Test thisTest;
        Tester thisTester;
        public UpdateTestsUC(Tester SentTester)
        {
            InitializeComponent();
            thisTester = SentTester; //.ShallowCopy(); //deap copy
            System.Windows.Application.Current.Windows.OfType<TESTER>().SingleOrDefault(x => x.IsActive).UpdatePageName("Update Tests");
        }

      


        private void findTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = bl.FindTest(int.Parse(numberTest.Text));
                thisTest = bl.getTests()[i].ShallowCopy();
                if (thisTest.TesterId != thisTester.Id)
                    throw new Exception("Erorr!! this is not Your test to update!");
                if (thisTest.DateAndHour>DateTime.Now)
                    throw new Exception("Erorr!! you can't update a future test!");

                outsideGrid.Visibility = Visibility.Visible;
                numTestGrid.Visibility = Visibility.Hidden;
                updateButton.Visibility = Visibility.Visible;
                this.DataContext = thisTest;

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                MessageBoxProject x = new MessageBoxProject("Attention", ex.Message);
                x.ShowDialog();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                bl.UpdateTest(thisTest);
                FinalOutcome.Content = thisTest.FinalOutcome;

                resultGrid.Visibility = Visibility.Visible;
             
                outsideGrid.Visibility = Visibility.Hidden;
                updateButton.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                MessageBoxProject x = new MessageBoxProject("Attention", ex.Message);
                x.ShowDialog();
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {


            if ((sender as CheckBox).Name == "DistanceCheckBox")
            {
                if (thisTest.details.KeptDistance == true)
                    thisTest.details.KeptDistance = false;
                else
                    thisTest.details.KeptDistance = true;
            }

            if ((sender as CheckBox).Name == "reverseParkingCheckBox")
            {
                if (thisTest.details.reverseParking == true)
                    thisTest.details.reverseParking = false;
                else
                    thisTest.details.reverseParking = true;
            }

            if ((sender as CheckBox).Name == "mirrorsCheckBox")
            {
                if (thisTest.details.mirrors == true)
                    thisTest.details.mirrors = false;
                else
                    thisTest.details.mirrors = true;
            }
            if ((sender as CheckBox).Name == "signalCheckBox")
            {
                if (thisTest.details.signal == true)
                    thisTest.details.signal = false;
                else
                    thisTest.details.signal = true;
            }
            if ((sender as CheckBox).Name == "speedCheckBox")
            {
                if (thisTest.details.speed == true)
                    thisTest.details.speed = false;
                else
                    thisTest.details.speed = true;
            }
            if ((sender as CheckBox).Name == "TesterInvolvedCheckBox")
            {
                if (thisTest.details.TesterInvolved == true)
                    thisTest.details.TesterInvolved = false;
                else
                    thisTest.details.TesterInvolved = true;
            }
            if ((sender as CheckBox).Name == "EnterToJuctionCheckBox")
            {
                if (thisTest.details.EnterToJuction == true)
                    thisTest.details.EnterToJuction = false;
                else
                    thisTest.details.EnterToJuction = true;
            }
            if ((sender as CheckBox).Name == "PrepareToDriveCheckBox")
            {
                if (thisTest.details.PrepareToDrive == true)
                    thisTest.details.PrepareToDrive = false;
                else
                    thisTest.details.PrepareToDrive = true;
            }


        }

        private void numberTest_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;

            findTestButton_Click(sender, e);
        }

        private void testerNote_LostFocus(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(testerNote.Document.ContentStart, testerNote.Document.ContentEnd);//convert rich text to string

            thisTest.details.TesterNote = textRange.Text.ToString();
        }

        
    }
}
