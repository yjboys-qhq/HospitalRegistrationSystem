using Entity;
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

namespace WpfApp1
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            //Print.CloseEvent += OnStep;
        }
        string charge_amount = "";
        string paid = "";
        string change = "";
        double blance_now = 0;
        string cardId = "";
        private void query_btn_Click(object sender, RoutedEventArgs e)
        {
            cardId = cardNo.Text.Trim();
            Query query = new Query();
            patient pa = DtTransaction.Dt2patient(query.Query_by_patient_id(cardId));
            if (pa.Name == null)
            {
                MessageBox.Show("无法找到该病人", "提示");
            }
            balancebox.Text = pa.Price.ToString();
            blance_now = pa.Price;
        }

        private void charge_amount_LostFocus(object sender, RoutedEventArgs e)
        {
            charge_amount = charge_amount_txtbox.Text.Trim();
            
        }

        private void paid_textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            paid = paid_textbox.Text;
            if (paid != ""&&charge_amount!="")
            {
                double paid_amount = Convert.ToDouble(paid);
                double charge = Convert.ToDouble(charge_amount);
                double change = paid_amount - charge;
                if (change >= 0)
                {
                    queryBtn.IsEnabled = true;
                }
                change_texbox.Text = change.ToString("0.00");                
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double paid_amount = Convert.ToDouble(paid);
            double charge = Convert.ToDouble(charge_amount);
            double change = paid_amount - charge;
            if (change >= 0)
            {
                queryBtn.IsEnabled = true;
                MessageBoxResult result=MessageBox.Show("就诊卡号：" + cardId + "\r\n金额：" + charge_amount, "确认充值", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                { 
                    double blance_after = blance_now + Convert.ToDouble(charge_amount);
                    Query query = new Query();
                    query.Update_bal(cardId, blance_after);
                    query_btn_Click(sender, e);
                    patient pa=DtTransaction.Dt2patient(query.Query_by_patient_id(cardId));
                    string path = Print2word.patient2word(Convert.ToDouble(charge_amount), pa.Name, (string)Application.Current.Properties["userName"]);
                    Print print = new Print(path);
                    print.Show();
                    queryBtn.IsEnabled = false;
                }
                else
                {
                    queryBtn.IsEnabled = true;
                }
            }
            else
            {
                queryBtn.IsEnabled = false;
                change_texbox.Text = change.ToString("0.00");
            }
          
            

           
        }

        private void refund_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("就诊卡号：" + cardId + "\r\n退款金额："+ blance_now, "确认退费？", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                double blance_after = 0;
                Query query = new Query();
                query.Update_bal(cardId, blance_after);
                query_btn_Click(sender, e);
            }
        }
        public void OnStep(object sender,Boolean tmp)
        {

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                queryBtn.IsEnabled = true;//设置对应xaml中控件的属性
            }));
        }
    }
}

