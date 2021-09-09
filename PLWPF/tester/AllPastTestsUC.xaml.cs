using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for AllPastTestsUC.xaml
    /// </summary>
    public partial class AllPastTestsUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        Tester thisTester;
        ObservableCollection<Test> myTests; //keep the choosen ChooseTestCriterion
        ObservableCollection<Test> SortedLIst; //keep sorted lists

        public AllPastTestsUC(Tester sentTester)
        {         
            InitializeComponent();
            int ind = bl.FindTester(sentTester.Id);
            thisTester = bl.getTesters()[ind].ShallowCopy();

            Application.Current.Windows.OfType<TESTER>().SingleOrDefault(x => x.IsActive).UpdatePageName("All Past Tests");
        }

      
        private void SortByCriterion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortedLIst = myTests;

            switch (((ComboBoxItem)SortByCriterion.SelectedItem).Content.ToString())
            {

                case "": //go back to default
                    {
                        GridPastTests.ItemsSource = myTests;
                        break; 
                    }

                case "Date": //group by date
                    {

                        GridPastTests.ItemsSource = SortedLIst.OrderBy(order => order.DateAndHour).ToList();
                        break;
                    }
                case "Test Number": //group by test number
                    {                
                        GridPastTests.ItemsSource = SortedLIst.OrderBy(order => order.numberTest).ToList();              
                        break;
                    }
            }


        }  //func to sort the test by criterion

    
        private void ChooseTestCriterion_SelectionChanged(object sender, SelectionChangedEventArgs e)    //func to filter the est by creterion
        {
            SortByCriterion.IsHitTestVisible = true;
            switch (((ComboBoxItem)ChooseTestCriterion.SelectedItem).Content.ToString())
            {              
                case "All tests":
                    {
                     
                        myTests = new ObservableCollection<Test>();    //initialze the list

                    
                        foreach (Test t in thisTester.MyTests)    //add updated tests:
                            myTests.Add(t);

                    
                        foreach (Test t in bl.showUnUpdatedTests(thisTester))    //add unupdated tests:
                            myTests.Add(t);


                        //add to the list
                        GridPastTests.ItemsSource = myTests; 
                        SortByCriterion.SelectedItem = defaultSort; 
                        break;
                    }
                    
                case "tests not Updated":
                    {
                      
                        myTests = new ObservableCollection<Test>();  //initialze the list

                      
                        foreach (Test t in bl.showUnUpdatedTests(thisTester))  //save the new list
                            myTests.Add(t);

                        //add to the list
                        GridPastTests.ItemsSource = myTests;
                        SortByCriterion.SelectedItem = defaultSort;
                        break;
                    }
                case "Past test":
                    {

                      
                        myTests = new ObservableCollection<Test>();  //initialze the list

                      
                        foreach (Test t in bl.showPassedTests(thisTester))  //save the new list
                            myTests.Add(t);

                        //add to the list
                        GridPastTests.ItemsSource = myTests;
                        SortByCriterion.SelectedItem = defaultSort;
                        break;
                    }
            }
        }

       
    }
}
