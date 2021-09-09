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

namespace PLWPF.Admin
{
    /// <summary>
    /// Interaction logic for ListOfTesterUC.xaml
    /// </summary>
    public partial class ListOfTesterUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        public ListOfTesterUC()
        {
            InitializeComponent();
            Application.Current.Windows.OfType<ADMIN>().SingleOrDefault(x => x.IsActive).UpdatePageName("Show All Testers");
        }

        private void ChooseTesterCriterion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBoxItem)ChooseTesterCriterion.SelectedItem).Content.ToString())
            {

                case "": //go back to default
                    {
                        GridAllTesters.ItemsSource = bl.getTesters();
                        break;
                    }
                case "Name": //group by name
                    {
                        List<Tester> groupList = new List<Tester>();
                        foreach (var item in bl.GroupTestersByName())
                        {
                            foreach (Tester t in item)
                                groupList.Add(t);
                        }
                        GridAllTesters.ItemsSource = groupList;
                        break;
                    }
                case "Car Type": //group by cartype
                    {
                        List<Tester> groupList = new List<Tester>();
                        foreach (var item in bl.GroupTestersByCarType())
                        {
                            foreach (Tester t in item)
                                groupList.Add(t);
                        }
                        GridAllTesters.ItemsSource = groupList;
                        break;
                    }
                case "Seniority": //group by seniority
                    {
                        List<Tester> groupList = new List<Tester>();
                        foreach (var item in bl.GroupTestersBySeniority())
                        {
                            foreach (Tester t in item)
                                groupList.Add(t);
                        }
                        GridAllTesters.ItemsSource = groupList;
                        break;
                    }
            }
        }
    }
}