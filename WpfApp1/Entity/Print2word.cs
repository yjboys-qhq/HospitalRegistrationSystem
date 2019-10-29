using Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tool;

namespace WpfApp1
{
    class Print2word
    {
        static string model1path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\model\挂号单.docx";
        static string model2path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\model\收费收据.docx";
        public static string patient2word(patient patient, string Order_id, string Dep, string price, string type, string user, string ImgPath)
        {
            AspostWord docHelper = new AspostWord();
            docHelper.CreateNewDocument(model1path);
            docHelper.InsertValue("姓名", patient.Name);
            docHelper.InsertValue("性别", patient.Gender);
            docHelper.InsertValue("年龄", (DateTime.Now.Year - patient.Birthday.Year).ToString());
            docHelper.InsertValue("科别", Dep);
            docHelper.InsertValue("挂号单号", Order_id);
            docHelper.InsertValue("挂号费", price);
            docHelper.InsertValue("挂号员", user);
            docHelper.InsertValue("号别", type);
            docHelper.InsertValue("工本费", "0");
            docHelper.InsertPicture("二维码", ImgPath);
            docHelper.InsertValue("挂号日期", DateTime.Now.ToLongDateString());
            DateTime time = DateTime.Now;
            Random rd = new Random();
            int t = rd.Next(1,10);
            string savename = time.ToString("yyyy年mm月dd日HH时mm分ss秒ffff毫秒") + "挂号单" + t.ToString() + ".xps";
            string savePath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\model\tmp\"+savename;
            docHelper.SaveDocument(savePath);
            return savePath;
        }
        public static string patient2word(double amount, string name, string user)
        {
            AspostWord docHelper = new AspostWord();
            docHelper.CreateNewDocument(model2path);
            docHelper.InsertValue("收费日期", DateTime.Now.ToLongDateString());
            docHelper.InsertValue("姓名", name);
            docHelper.InsertValue("收费员", user);
            docHelper.InsertValue("机制号", DateTime.Now.ToString("HHmmss") + Guid.NewGuid().ToString().Substring(5, 10));
            docHelper.InsertValue("收费详细", "就诊卡充值：" + amount.ToString()+"\r\n总金额：" + amount.ToString());
            DateTime time = DateTime.Now;
            Random rd = new Random();
            int t = rd.Next(1, 10);
            string pathname = time.ToString("yyyy年mm月dd日HH时mm分ss秒ffff毫秒") + "收费收据" + t.ToString() + ".xps";
            string savePath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\model\tmp\"+pathname;
            docHelper.SaveDocument(savePath);
            return savePath;
        }

    }
}
