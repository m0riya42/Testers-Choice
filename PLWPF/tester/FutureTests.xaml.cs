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
using System.Collections.ObjectModel;

namespace PLWPF.tester
{
    /// <summary>
    /// Interaction logic for FutureTests.xaml
    /// </summary>
    public partial class FutureTests : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        Tester thisTester;



        public FutureTests(Tester sentTester)
        {
            InitializeComponent();
            int ind = bl.FindTester(sentTester.Id);
            thisTester = bl.getTesters()[ind].ShallowCopy();

            //the header of the page
            Application.Current.Windows.OfType<TESTER>().SingleOrDefault(x => x.IsActive).UpdatePageName("Future Tests");

            var keepList = bl.SpecificFutureTest(thisTester).ToList(); //func to return the future test+their count
            //check if there is future tests at all
            if (keepList.Count() != 0)
            {
                //keep the list as depandency property
                var myTests = new ObservableCollection<object>();
                foreach (object t in keepList)
                    myTests.Add(t);

                //add to the list
                foreach (object T in myTests)
                {
                    GridFutureTests.Items.Add(T);
                }
                // GridFutureTests.ItemsSource = myTests;///--->prints all the list values.. not good in our 

            }
            else//no future tests
            {
                GridFutureTests.Visibility = Visibility.Hidden;
                titleLabel.Visibility = Visibility.Hidden;
                NoTest.Visibility = Visibility.Visible;
            }
        }

    }
}
