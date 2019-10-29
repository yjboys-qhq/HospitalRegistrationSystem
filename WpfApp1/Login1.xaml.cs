using MyFirstRibbonProject;
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
using System.Windows.Shapes;
using Tool;
namespace WpfApp1
{
    
    /// <summary>
    /// Login1.xaml 的交互逻辑
    /// </summary>
    public partial class Login1 : Window
    {
        private int i;
        ValidCode validCode = new ValidCode(5, ValidCode.CodeType.Alphas);
        public Login1()
        {
            InitializeComponent();
        }
        private void init()
        {
            this.i = 0;
            this.imgcode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
        }
        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            Query q = new Query();
            string name = "";
            string pass = "";
            string type = "";
            if (username.Text == "" || password.Password == "")
            {
                MessageBox.Show("用户名或密码不能为空!", "提示");
            }
            else
            {

                DataTable dt = q.Query_by_user(username.Text);
                if (dt.Rows.Count > 0)
                {
                    name = dt.Rows[0][1].ToString();
                    pass = dt.Rows[0][2].ToString();
                    type = dt.Rows[0][3].ToString();
                }

                string code = validCode.CheckCode;
                if (validcode.Text.ToUpper() == code.ToUpper())
                {
                    if (username.Text == name && password.Password == pass)
                    {
                        Application.Current.Properties["userName"] = name;
                        if (type == "1")
                        {
                            MyFirstWindow myFirstWindow = new MyFirstWindow();
                            myFirstWindow.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        i++;
                        if (i == 3)
                        {
                            MessageBox.Show("连续输入三次错误,系统将退出!", "提示");
                            Application.Current.Shutdown();
                        }
                        else
                        {
                            if (username.Text != name)
                            {
                                MessageBox.Show("用户名或者密码错误！", "提示");
                            }
                            else if (password.Password != pass)
                            {
                                MessageBox.Show("用户名或者密码错误！", "提示");
                                reset_Click(sender, e);
                            }
                            else
                                MessageBox.Show("用户名或者密码错误！", "提示");
                            username.Text = "";
                            password.Password = "";
                            username.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("验证码输入错误!", "提示");
                }
            }
            
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            username.Text = "";
            password.Password = "";
            validcode.Text = "";
        }

        private void imgcode_MouseEnter(object sender, MouseEventArgs e)
        {
            this.imgcode.Source = BitmapFrame.Create(validCode.CreateCheckCodeImage());
        }

        private void init(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void validcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                submit_Click(sender, e);
            }
        }
    }
}
