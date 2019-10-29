using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
    /// <summary>
    /// wait.xaml 的交互逻辑
    /// </summary>
    public partial class wait : Page
    {
        public wait()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            Query q = new Query();
            DataTable dt = q.appointmentAll();
            myGrid.ItemsSource = dt.DefaultView;
            //设置网格线  
            myGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
        }

    }
}
