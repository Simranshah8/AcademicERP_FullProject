using Dynamic.BusinessEntity.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Student
{
    public class Payment
    {
        public string RegNo { get; set; } = string.Empty;
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public double Amount { get; set; } = 0;
        public string ReferenceId { get; set; }
        public string Remarks { get; set; }
        public string HashData { get; set; }

        public int UptoMonthId { get; set; }


        //for Esewa
        public string request_id { get; set; }
        public double amount { get; set; }
        public string transaction_code { get; set;  }

    }
}
