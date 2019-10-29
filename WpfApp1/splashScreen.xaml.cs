using MyFirstRibbonProject;
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

namespace WpfApp1
{
    /// <summary>
    /// splashScreen.xaml 的交互逻辑
    /// </summary>
    public partial class splashScreen : Window
    {
        public splashScreen()
        {
            InitializeComponent();
        }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void Grid_Initialized(object sender, EventArgs e)
        {
           
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
           


        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Login1 login1 = new Login1();
            login1.Show();                
            this.Close();    
            dispatcherTimer.Stop();
        }
    }
}
