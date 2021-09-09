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
using System.Windows.Shapes;

namespace PLWPF
{
   
    public partial class OpenPage : Window
    {
        public System.Windows.Threading.DispatcherTimer dispatcherTimer;
        int timer = 0;
        public OpenPage()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 8);
            dispatcherTimer.Start();
            InitializeComponent();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer++;
            if (timer % 300 == 0)
            {
                new MainWindow().Show();
                this.Close();
                dispatcherTimer.Tick -= new EventHandler(dispatcherTimer_Tick);
            }
        }
    }
}
