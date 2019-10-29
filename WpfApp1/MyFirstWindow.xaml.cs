using System;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using Tool;
using WpfApp1;

namespace MyFirstRibbonProject
{
    /// <summary>
    /// MyFirstWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MyFirstWindow
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MyFirstWindow()
        {
            this.InitializeComponent();
            main m = new main();
            MainFrame.Content = m;
            MainFrame.Height = m.Height;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page2 page2 = new Page2();           
            MainFrame.Content = page2;                    
            MainFrame.Height = page2.Height;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page3 page3 = new Page3();
            MainFrame.Content = page3;
            MainFrame.Height = page3.Height;
        }

        private void Button_Click_wait(object sender, RoutedEventArgs e)
        {
            //预约
            wait wait = new wait();
            MainFrame.Content = wait;
            MainFrame.Height = wait.Height;
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
                MessageBoxResult result = System.Windows.MessageBox.Show("您确认要退出综合挂号系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else if (result == MessageBoxResult.No)
                {
                     e.Cancel = true;
                } 
        }

        private void Button_Click_bar(object sender, RoutedEventArgs e)
        {
            bar chars = new bar();
            MainFrame.Content = chars;
            MainFrame.Height = chars.Height;
        }

        private void Button_Click_pie(object sender, RoutedEventArgs e)
        {
            pie chars = new pie();
            MainFrame.Content = chars;
            MainFrame.Height = chars.Height;
        }

        private void Button_Click_spline(object sender, RoutedEventArgs e)
        {
            spline chars = new spline();
            MainFrame.Content = chars;
            MainFrame.Height = chars.Height;
        }

        private void Button_Click_help(object sender, RoutedEventArgs e)
        {
            help h = new help();
            MainFrame.Content = h;
            MainFrame.Height = h.Height;
        }

        private void Button_Click_about(object sender, RoutedEventArgs e)
        {
            about a = new about();
            MainFrame.Content = a;
            MainFrame.Height = a.Height;
        }

        private void Button_Click_area(object sender, RoutedEventArgs e)
        {
            area a = new area();
            MainFrame.Content = a;
            MainFrame.Height = a.Height;
        }

        private void Button_Click_point(object sender, RoutedEventArgs e)
        {
            point p = new point();
            MainFrame.Content = p;
            MainFrame.Height = p.Height;
        }

        private void Button_Click_logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("您确认要退出综合挂号系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        private void Button_Click_archives(object sender, RoutedEventArgs e)
        {
            //建档page
            Regist regist = new Regist();
            MainFrame.Content = regist;
            MainFrame.Height = regist.Height;
        }
        
        private void Button_Click_saveTocsv(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return ;
            }
            string path = m_Dialog.SelectedPath.Trim();
            DateTime d = DateTime.Now;
            string save = d.ToString("yyyy年MM月dd日");
            path = path +"\\" +save + "保存挂号数据.csv";
            Query query = new Query();
            DataTable dt = query.Exportall();
            Csv.SaveCSV(dt, path);
            path = "数据保存路径为：" + path;
            System.Windows.MessageBox.Show(@path,"提示");
        }

        private void Button_Click_saveToexcel(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string path = m_Dialog.SelectedPath.Trim();
            DateTime d = DateTime.Now;
            string save = d.ToString("yyyy年MM月dd日");
            path = path + "\\" + save + "保存挂号数据.xls";
            Query query = new Query();
            DataTable dt = query.Exportall();
            toExcel.ExportExcelWithAspose(dt, path);
            path = "数据保存路径为：" + path;
            System.Windows.MessageBox.Show(@path, "提示");
        }

        private string SaveDir()
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return "";
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            return m_Dir;
        }
    }
}
