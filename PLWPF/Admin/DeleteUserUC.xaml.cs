using BL;
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
using System.Text.RegularExpressions;

namespace PLWPF.Admin
{
    
    public partial class DeleteUserUC : UserControl
    {
        IBL bl = BL.FactoryBL.getBL();
        string RadioChoice = "Empty";
            
        public DeleteUserUC()
        {
            InitializeComponent();
            Application.Current.Windows.OfType<ADMIN>().SingleOrDefault(x => x.IsActive).UpdatePageName("Delete User");
           
        }

        //text input-->only numbers
        private void LettersBlock_Textinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void DeleteU_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (IdToDelete.Text.Count() <9)
                    throw new Exception("Enter User ID");
                if (RadioChoice == "Empty")
                    throw new Exception("Choose User Type");
                else
                {
                    if (RadioChoice == "Tester")
                    {
                        bl.deleteTester(IdToDelete.Text);
                        T1.IsChecked = false;// empty radio button
                    }
                    else
                    {
                        bl.deleteStudent(IdToDelete.Text);
                        T2.IsChecked = false;// empty radio button

                    }

                    IdToDelete.Text = ""; //empty text box
                    

                    MessageBoxProject pg = new MessageBoxProject("Attention", "User Deleted Succefully");
                    pg.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBoxProject pg = new MessageBoxProject("Attention", ex.Message);
                pg.ShowDialog();

            }
        

        }

        private void Choice_Checked(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Content.ToString())
            {
                case "Tester":
                    {
                        RadioChoice = "Tester";

                        break;
                    }
                case "Trainee":
                    {
                        RadioChoice = "Trainee";

                        break;
                    }
            }

        }

        private void DeleteU_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;

            DeleteU_Click(sender, e);

        }

        private void RadioButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;

            ((RadioButton)sender).IsChecked = true;
            Choice_Checked(sender, e);
        }
    }
}
