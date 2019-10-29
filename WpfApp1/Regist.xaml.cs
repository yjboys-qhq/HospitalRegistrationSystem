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
    /// Regist.xaml 的交互逻辑
    /// </summary>
    public partial class Regist : Page
    {
        public Regist()
        {
            InitializeComponent();
        }

        

        private void write_btn_Click(object sender, RoutedEventArgs e)
        {
            string name_str = name.Text.Trim();
            string ssn_num = SSN.Text.Trim();
            DateTime birth =(DateTime)Birthday.SelectedDate;
            string gender = "";
            if (M.IsChecked==true)
            {
                gender = "男";
            }
            else
            {
                gender = "女";
            }
            DateTime setup = DateTime.Now;
            Entity.patient new_patient = new Entity.patient();
            new_patient.Name = name_str;
            new_patient.SSN1 = ssn_num;
            new_patient.Setup_date = setup;
            new_patient.Gender = gender;
            new_patient.Birthday = birth;
            Query query = new Query();
            string cardno=query.creatPatient(new_patient);
            MessageBox.Show("卡号：" + cardno, "发卡成功");
            cardNo.Text = cardno;
        }
    }
}
