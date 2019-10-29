using System;
using System.Collections.Generic;
using System.Data;
using WpfApp1;

namespace Entity
{
    class CharData
    {
        private List<DateTime> lsTime = new List<DateTime>();
        private List<string> count = new List<string>();

        public void setData()
        {
            //读数据库将挂号时间和人数分别赋值给LsTime 和 count 近12天记录
            Query q = new Query();
            DataTable dt = q.Exportcount();
            int t = 0;
            foreach (DataRow dr in dt.Rows)
            {
                
                lsTime.Add(Convert.ToDateTime(dr[0].ToString()));
                count.Add(dr[1].ToString());
                if (t > 12)
                    break;
                t++;
            }
 
        }

        public CharData()
        {
        }

        public CharData(List<DateTime> Time, List<string> count)
        {
            lsTime = Time;
            this.count = count;
        }

        public List<string> Count { get {return count;} set { count = value; }}
        public List<DateTime> LsTime1 { get { return lsTime; } set { lsTime = value; } }
    }
}
