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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;

namespace PLWPF.Admin
{

    

    public partial class ShowAllTraineesUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        List<Trainee> keepTraineesLIst;//all the list
        List<Trainee> objListOrder;// sorted list

        public ShowAllTraineesUC()
        {
            InitializeComponent();
            Application.Current.Windows.OfType<ADMIN>().SingleOrDefault(x => x.IsActive).UpdatePageName("Show All Trainees");
            keepTraineesLIst = bl.getTrainnes();
        }


        //tab selections:
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Tab all students
            if (((TabItem)MyLIsts.SelectedItem).Header.ToString() == "Show All Trainees")
            {
                lvUsers.ItemsSource = keepTraineesLIst;
                CalNum.Content = lvUsers.Items.Count;
            }
            else //by criterion
            {
                CalNum1.Content = bl.getTrainnes().Count();//keep num of trAINEES
            }
        }   

            //choice grouping page:
        private void Choice_CheckedGrouping(object sender, RoutedEventArgs e)
        {
            ListView keep = new ListView();
            keep.HorizontalAlignment = HorizontalAlignment.Center;
            keep.HorizontalAlignment = HorizontalAlignment.Center;
            keep.BorderBrush = Brushes.Transparent;
            keep.Background = Brushes.Transparent;
            keep.Margin = new Thickness(3.2, 0.2, 1.6, 0.4);
            Width = 544;

            switch (((RadioButton)sender).Content.ToString())
            {
                case "Name":
                    {
                        #region Group by name

                        var groupList = bl.GroupTraineesByName();//keep grouping

                        foreach (IGrouping<string, Trainee> group in groupList)
                        {
                            Expander exp = new Expander(); //Create Expander object to grid title


                            ListBox po = new ListBox();//keep all groups in expanders
                            po.Background = Brushes.Transparent;
                            po.BorderBrush = Brushes.Transparent;
                            po.Width = 400;



                            foreach (Trainee t in group)
                            {
                                exp.Header = t.Name+" ----> "+group.Count()+" items";
                                po.Items.Add(t);
                            }

                            exp.Content = po;
                            exp.Width = 500;
                            exp.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                            keep.Items.Add(exp);

                        }
                        break;
#endregion
                    }
                case "Gender":
                    {
                        #region Group by Gender
                        var groupList = bl.GroupTraineesByGender();//keep grouping

                    foreach (IGrouping<Gender, Trainee> group in groupList)
                    {
                        Expander exp = new Expander(); //Create Expander object to grid title


                        ListBox po = new ListBox();//keep all groups in expanders
                        po.Background = Brushes.Transparent;
                        po.BorderBrush = Brushes.Transparent;
                        po.Width = 400;



                        foreach (Trainee t in group)
                        {
                            exp.Header = t.GenderType + " ----> " + group.Count() + " items";
                            po.Items.Add(t);
                        }

                        exp.Content = po;
                        exp.Width = 500;
                        exp.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                        keep.Items.Add(exp);

                    }
                    break;
                        #endregion
                    }
                case "GearBox":
                    {
                        #region Group by Gearbox
                        var groupList = bl.GroupTraineesByGearbox();//keep grouping

                        foreach (IGrouping<Gearbox, Trainee> group in groupList)
                        {
                            Expander exp = new Expander(); //Create Expander object to grid title


                            ListBox po = new ListBox();//keep all groups in expanders
                            po.Background = Brushes.Transparent;
                            po.BorderBrush = Brushes.Transparent;
                            po.Width = 400;



                            foreach (Trainee t in group)
                            {
                                exp.Header = t.gearbox + " ----> " + group.Count() + " items";
                                po.Items.Add(t);
                            }

                            exp.Content = po;
                            exp.Width = 500;
                            exp.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                            keep.Items.Add(exp);

                        }
                        break;
                        #endregion
                    }
                case "Number Of Tests":

                    {
                        #region Group by number of test

                        var groupList = bl.GroupTraineesByNumOfTests();//keep grouping

                        foreach (IGrouping<int, Trainee> group in groupList)
                        {
                            Expander exp = new Expander(); //Create Expander object to grid title


                            ListBox po = new ListBox();//keep all groups in expanders
                            po.Background = Brushes.Transparent;
                            po.BorderBrush = Brushes.Transparent;
                            po.Width = 400;



                            foreach (Trainee t in group)
                            {
                                exp.Header = t.MyTests.Count() + " ----> " + group.Count() + " items";
                                po.Items.Add(t);
                            }

                            exp.Content = po;
                            exp.Width = 500;
                            exp.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                            keep.Items.Add(exp);

                        }
                        break;
                        #endregion
                    }
            }
            listBorder.Child = keep;
        }

        //choice sorting page:
        private void Choice_Checked(object sender, RoutedEventArgs e)
        {
            switch(((RadioButton)sender).Content.ToString())
            {
                        case "Name":
                    {
                      objListOrder = keepTraineesLIst.OrderBy(order => order.Name).ToList();

                        break;
                    }
                case "Age":
                    {
                    
                     
                        objListOrder = keepTraineesLIst.OrderBy(order => order.Birthday).ToList();
                        break;
                    }
                case "Car Type":
                    {

                         objListOrder = keepTraineesLIst.OrderBy(order => order.car).ToList();
                      

                        break;
                    }
                case "Number Of Lessons":
                 
                    {
                      objListOrder = keepTraineesLIst.OrderBy(order => order.NumLessons).ToList();

                        break;
                    }

            }
            lvUsers.ItemsSource = objListOrder;

        }
    }
}
