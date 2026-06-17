using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class FeeDues
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string RegNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; } 
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public bool IsLeft { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Dues { get; set; }

        public double DrDiscountAmt { get; set; }
        public double CrDiscountAmt { get; set; }
        public string PhotoPath { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
    public class FeeDuesCollections : System.Collections.Generic.List<FeeDues>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
