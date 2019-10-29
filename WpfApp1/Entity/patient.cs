using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    class patient
    {
        private string name;
        private string gender;
        private string ID;
        private DateTime birthday;
        private DateTime setup_date;
        private double price;
        private string SSN;
        private string grade;

        public patient()
        {

        }

        public patient(string name, string gender, string iD, DateTime birthday, DateTime setup_date, double price, string sSN, string grade)
        {
            this.Name = name;
            this.Gender = gender;
            ID1 = iD;
            this.Birthday = birthday;
            this.Setup_date = setup_date;
            this.Price = price;
            SSN1 = sSN;
            this.Grade = grade;
        }

        public string Name { get {return name; }set { name = value; }}
        public string Gender { get {return gender;} set { gender = value; }}
        public string ID1 { get {return ID; }set { ID = value; }}
        public DateTime Birthday { get {return birthday;} set { birthday = value; }}
        public DateTime Setup_date { get {return setup_date;} set { setup_date = value; }}
        public double Price { get {return price; }set { price = value; }}
        public string SSN1 { get {return SSN; }set { SSN = value; }}
        public string Grade { get { return grade; } set { grade = value; } }
    }
}
