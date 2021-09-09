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
using System.Collections.ObjectModel;
using BE;
using BL;
namespace PLWPF.trainee
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MyTestsUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();

        Trainee ThisTrainee;
        List<Test> updatedTests = new List<Test>();
        // int mone = 0;
        public MyTestsUC(Trainee sentTrainee)
        {
            int ind = bl.FindTrainee(sentTrainee.Id);
            ThisTrainee = bl.getTrainnes()[ind].ShallowCopy();

            InitializeComponent();
            Application.Current.Windows.OfType<TRAINEE>().SingleOrDefault(x => x.IsActive).UpdatePageName("My Tests");
            if (ThisTrainee.MyTests.Count != 0)
            {
                //keep the list as depandency property
                var myTests = new ObservableCollection<Test>();
                foreach (Test t in ThisTrainee.MyTests)
                    myTests.Add(t);

                //add to the list
                foreach (Test T in myTests)
                {
                    if (T.IsTestUpdate == true)
                        updatedTests.Add(T);
                    GridFutureTests.Items.Add(T);
                }

                //show Testers notes
                if (updatedTests.Count == 0)
                {
                    TestChoice.IsEnabled = false;
                }
                else
                {
                    for (int i = 1; i < updatedTests.Count + 1; i++)
                        TestChoice.Items.Add(i + " ");
                }
                // GridFutureTests.ItemsSource = myTests;///--->prints all the list values.

            }
            else//no  tests
            {
                GridFutureTests.Visibility = Visibility.Hidden;
                titleLabel.Visibility = Visibility.Hidden;
                NoTest.Visibility = Visibility.Visible;
                TesterNoteWrap.Visibility = Visibility.Hidden;
                stat.Visibility = Visibility.Hidden;
            }

        }

        private void TestChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = int.Parse(TestChoice.SelectedItem.ToString());
            Notes.Text = updatedTests[index - 1].details.TesterNote;



        }

        private void stat_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false, flag2 = false;
            foreach (Test t in ThisTrainee.MyTests)
                if (t.IsTestUpdate == true)
                {
                    if (t.FinalOutcome == false)
                        flag2 = true; //save unpassed tests
                    flag = true;// save updated tests
                    break;
                }
            if (flag == true)
            {
                if (flag2 == true)
                {
                    STATICS nPage = new STATICS(ThisTrainee);
                    nPage.Show();
                }
                else
                {
                    MessageBoxProject at = new MessageBoxProject("Pay Attions!", @"congratulations! you passed the test!
there are'nt any statistics to watch.");
                    at.ShowDialog();
                }
            }
            else
            {
                MessageBoxProject at = new MessageBoxProject("Warning!", "there isn't any update test");
                at.ShowDialog();
            }
        }
    }     
}
