using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    class DtTransaction
    {
        public static patient Dt2patient(DataTable dataTable)
        {
            patient new_patient = new patient();
            if (dataTable.Rows.Count > 0)
            {
                new_patient.Name = dataTable.Rows[0][1].ToString();
                new_patient.ID1 = dataTable.Rows[0][0].ToString();
                new_patient.Gender = dataTable.Rows[0][2].ToString();
                new_patient.Birthday = Convert.ToDateTime(dataTable.Rows[0][3]);
                new_patient.SSN1 = dataTable.Rows[0][4].ToString();
                new_patient.Setup_date = Convert.ToDateTime(dataTable.Rows[0][5]);
                new_patient.Price = Convert.ToDouble(dataTable.Rows[0][6]);
                new_patient.Grade = dataTable.Rows[0][7].ToString();
            }
            return new_patient;
        }
    }
}
