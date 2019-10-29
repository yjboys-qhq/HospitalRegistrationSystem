using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;

namespace WpfApp1
{
    class Query
    {
        string startpath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\db\bladia.accdb";
        public DataTable Query_by_patient_id(string id)
        {        
            string baseSQL = "SELECT * FROM 病人信息索引表 where patient_id = \"" + id + "\"";            
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            DataTable dataTable = dBHelperAccess.SelectToDataTable(baseSQL);
            dBHelperAccess.Close();
            return dataTable;
        }

        public DataTable Query_dep(string dep,string type)
        {
            string  date=DateTime.Now.ToString("yyyy/MM/dd");
            string baseSQL = "SELECT 挂号表.* FROM 挂号表 where Department=\"" + dep + "\" and isPro=\"" + type + "\" and date_time=#"+date+"#";
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            DataTable dataTable = dBHelperAccess.SelectToDataTable(baseSQL);
            dBHelperAccess.Close();
            return dataTable;
        }

        public void Update_gua(string id,string type,string dep,string doc,double balance,string time)
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd");            
            string baseSQL = "INSERT INTO 挂号表 values(\"" + id + "\",\"" + time + "\",#" + date + "#,\"" + doc + "\",\"" + dep + "\",\"" + type + "\")";
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            dBHelperAccess.ExecuteSQLNonquery(baseSQL);
            if (type.Equals("S"))
                balance -= 20;
            if (type.Equals("N"))
                balance -= 5;
            string balanceSQL = "UPDATE 病人信息索引表 SET price=" + balance + " where patient_id = \"" + id + "\"";
            dBHelperAccess.ExecuteSQLNonquery(balanceSQL);
            dBHelperAccess.Close();
        }

        public DataTable Exportall()
        {
            string baseSQL = "SELECT * FROM 挂号表";
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            DataTable dataTable = dBHelperAccess.SelectToDataTable(baseSQL);
            dBHelperAccess.Close();
            return dataTable;
        }

        public DataTable appointmentAll()//预约人数
        {
            string baseSQL = "SELECT * FROM 预约表";
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            DataTable dataTable = dBHelperAccess.SelectToDataTable(baseSQL);
            dBHelperAccess.Close();
            return dataTable;
        }


        public DataTable Exportcount()
        {
            string baseSQL = "SELECT date_time, count(date_time) as peopleCount FROM 挂号表 group by date_time";
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            DataTable dataTable = dBHelperAccess.SelectToDataTable(baseSQL);
            dBHelperAccess.Close();
            return dataTable;
        }

        public void Update_bal(string id,double balance)
        {
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            string balanceSQL = "UPDATE 病人信息索引表 SET price=" + balance + " where patient_id = \"" + id + "\"";
            dBHelperAccess.ExecuteSQLNonquery(balanceSQL);
            dBHelperAccess.Close();
        }

        public DataTable Query_by_user(string username)
        {
            string baseSQL = "SELECT * FROM 用户表 where username = \"" + username + "\"";
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);
            DataTable dataTable = dBHelperAccess.SelectToDataTable(baseSQL);
            dBHelperAccess.Close();
            return dataTable;
        }
        public string creatPatient(patient patient)
        {
            DBHelperAccess dBHelperAccess = new DBHelperAccess(startpath);

            Random rad = new Random();
            int value = rad.Next(1000, 10000);
            string cardno = DateTime.Now.ToString("yyyyMMddHHmmss") + value.ToString();
            string birthday = patient.Birthday.ToString("yyyy/MM/dd");
            string setup = patient.Setup_date.ToString("yyyy/MM/dd");
            string baseSQL = "INSERT INTO 病人信息索引表 values(\"" + cardno + "\",\"" + patient.Name + "\",\"" + patient.Gender + "\",#" + birthday + "#,\"" + patient.SSN1 + "\",#" + setup + "#,\"" + 0 + "\",\"" + 0 + "\")";
            dBHelperAccess.ExecuteSQLNonquery(baseSQL);
            dBHelperAccess.Close();
            return cardno;
        }
    }
}
