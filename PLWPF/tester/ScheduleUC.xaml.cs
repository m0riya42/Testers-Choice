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
using System.Collections.ObjectModel;
using System.Data;


namespace PLWPF.tester
{

    public partial class ScheduleUC : UserControl
    {


        IBL bl = BL.FactoryBL.getBL();
        Tester thisTester;
        public ScheduleUC(Tester sentTester)
        {
            thisTester = sentTester;
            InitializeComponent();
            Application.Current.Windows.OfType<TESTER>().SingleOrDefault(x => x.IsActive).UpdatePageName("My Schedule");
       
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 5; j++)
                {
                    Label t = new Label();

                    if (thisTester.WorkDay[i, j])
                    {
                    
                      t.Background= new BrushConverter().ConvertFromString("#d5cbb2") as SolidColorBrush;
                        t.BorderBrush= new BrushConverter().ConvertFromString("#0c1f31") as SolidColorBrush;
                        t.BorderThickness = new Thickness(1.0);
                        Grid.SetColumn(t, j + 1);
                        Grid.SetRow(t, i + 1);
                        sched.Children.Add(t);
                    }

                    else
                    {
                  
                        
                      t.Background = new BrushConverter().ConvertFromString("#FFFFFF") as SolidColorBrush;
                        t.BorderBrush = new BrushConverter().ConvertFromString("#0c1f31") as SolidColorBrush;
                        t.BorderThickness = new Thickness(1.0);

                        Grid.SetColumn(t, j + 1);
                        Grid.SetRow(t, i + 1);
                        sched.Children.Add(t);
                    }
                }






        }




        private void c_dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn column = e.Column as DataGridTextColumn;
            Binding binding = column.Binding as Binding;
            binding.Path = new PropertyPath(binding.Path.Path + ".Value");
        }



    }
}
            

    