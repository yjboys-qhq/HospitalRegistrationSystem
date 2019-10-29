using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
using Tool;
using WindowsFormsApp1;
using WpfApp1.Tool;

namespace WpfApp1
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }
        string type = "";
        string departments = "";
        private patient patient_fin;
        string price = "";
        string tip_type = "";
        int remain = 0;
        private void query_Button_Click(object sender, RoutedEventArgs e)
        {

            int max = 0;

            if (normal.IsChecked == true)
            {
                tip_type = "普通号";
                type = "N";
                max = 200;
                price = "5";
            }
            else if (special.IsChecked == true)
            {
                tip_type = "专家号";
                type = "S";
                max = 50;
                price = "20";
            }
            departments = Combobox.SelectedValue.ToString();

            Query query = new Query();
            DataTable dt = query.Query_dep(departments, type);
            remain = max - dt.Rows.Count;
            Thickness myThickness = new Thickness();
            myThickness.Left = 1;
            myThickness.Top = 1;
            myThickness.Right = 1;
            myThickness.Bottom = 1;
            border.BorderThickness = myThickness;
            tip.Text = departments + "剩余" + tip_type + ":" + remain.ToString();
        }

        private void read_Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ID = cardNo.Text.Trim();
            patient tmp = new patient
            {
                ID1 = ID
            };
            Query query = new Query();
            DataTable dt = query.Query_by_patient_id(ID);
            patient result = DtTransaction.Dt2patient(dt);
            patient_fin = result;
            if (result.Name == null)
            {
                MessageBox.Show("无法找到该病人", "提示");
                name.Text = "";
                gender.Text = "";
                balance.Text = "";
                age.Text = "";
                start_date.Text = "";
                query_btn.IsEnabled = false;
            }

            else
            {
                name.Text = result.Name;
                gender.Text = result.Gender;
                balance.Text = result.Price.ToString();
                age.Text = (DateTime.Now.Year - result.Birthday.Year).ToString();
                start_date.Text = result.Setup_date.ToString();
                start_date.DisplayDate = result.Setup_date;
                query_btn.IsEnabled = true;
            }

        }

        private void register_Initialized(object sender, EventArgs e)
        {
            Combobox.Items.Add("内科");
            Combobox.Items.Add("外科");
            Combobox.Items.Add("骨科");
            Combobox.Items.Add("妇科");
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (remain > 0)
            {
                if (patient_fin.Price - Convert.ToDouble(price) >=0)
                {
                    border.BorderBrush = System.Windows.Media.Brushes.Blue;
                    MessageBoxResult result = MessageBox.Show("确认挂号？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    string order_id = DateTime.Now.ToString("HHmmss") + Guid.NewGuid().ToString().Substring(5, 10);
                    if (result == MessageBoxResult.Yes)
                    {
                        string ID = cardNo.Text.Trim();
                        Query query = new Query();
                        double balance_num = Convert.ToDouble(balance.Text);

                        query.Update_gua(ID, type, departments, "asc", balance_num, order_id);
                        CreateQr qr = new CreateQr();
                        Bitmap img = qr.ToQrcode(patient_fin, "HBTCM");//后一个参数为AES加密密码
                        BMPHelper.mitmap2jpg(img, @"model\tmp\test.jpg");
                        string picpath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\model\tmp\test.jpg";
                        string user = (string)Application.Current.Properties["userName"];
                        string path = Print2word.patient2word(patient_fin, order_id, departments, price, tip_type, user, picpath);
                        Print print = new Print(path);
                        print.Show();
                    }
                    read_Button_Click_1(sender, e);
                    query_Button_Click(sender, e);
                }
                else
                {
                    border.BorderBrush = System.Windows.Media.Brushes.Red;
                    MessageBox.Show("就诊卡余额不足，请充值后再试", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                border.BorderBrush = System.Windows.Media.Brushes.Red;
                MessageBox.Show("剩余号量不足", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void cardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                read_Button_Click_1(sender, e);
            }
        }
    }
}
