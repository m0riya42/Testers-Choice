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

namespace PLWPF.Admin
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ListOfTestUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        List<Test> keepTestsLIst;//all the list
        List<Test> objListOrder;// sorted list
        //GridViewColumnHeader _lastHeaderClicked = null;
        //ListSortDirection _lastDirection = ListSortDirection.Ascending;
        public ListOfTestUC()
        {
            InitializeComponent();
            Application.Current.Windows.OfType<ADMIN>().SingleOrDefault(x => x.IsActive).UpdatePageName("Show All Tests");
            keepTestsLIst = bl.getAllTest();

        }

        private void TabControlTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var groupList = new ObservableCollection<object>();
            object ob = new object();


            if (((TabItem)TabControlTest.SelectedItem).Header.ToString() == "All Tests")
            {
                lvUsers.ItemsSource = keepTestsLIst;

            }
            if (((TabItem)TabControlTest.SelectedItem).Header.ToString() == "Group By Date")
            {
                foreach (var item in bl.GroupTestsByDate())
                {
                    foreach (object t in item)
                        groupList.Add(t);
                    groupList.Add(ob);

                }
                GroupDate.ItemsSource = groupList;

            }
            if (((TabItem)TabControlTest.SelectedItem).Header.ToString() == "Group By Car")
            {
                foreach (var item in bl.GroupTestByCarType())
                {
                    foreach (object t in item)
                        groupList.Add(t);
                    groupList.Add(ob);

                }
                GroupCarType.ItemsSource = groupList;

            }
            if (((TabItem)TabControlTest.SelectedItem).Header.ToString() == "Group By Final Outcome")
            {

                foreach (var item in bl.GroupTestByIsPassed())
                {
                    foreach (object t in item)
                        groupList.Add(t);
                    groupList.Add(ob);

                }
                GroupFinalResult.ItemsSource = groupList;
                
            }
        }

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            switch (headerClicked.Content.ToString())
            {
                case "Number Test":
                    {
                        objListOrder = keepTestsLIst.OrderBy(order => order.numberTest).ToList();
                        break;
                    }
                case "Car Type":
                    {
                        objListOrder = keepTestsLIst.OrderBy(order => order.car).ToList();
                        break;
                    }
                case "Date":
                    {
                        objListOrder = keepTestsLIst.OrderBy(order => order.DateAndHour).ToList();
                        break;
                    }
                case "Tester ID":
                    {
                        objListOrder = keepTestsLIst.OrderBy(order => order.TesterId).ToList();
                        break;
                    }
                case "Trainee ID":
                    {
                        objListOrder = keepTestsLIst.OrderBy(order => order.StudentId).ToList();
                        break;
                    }

            }
            lvUsers.ItemsSource = objListOrder;

        }

    }
}
