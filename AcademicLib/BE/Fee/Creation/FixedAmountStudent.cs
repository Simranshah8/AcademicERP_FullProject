using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FixedAmountStudent : ResponeValues
    {
        public FixedAmountStudent()
        {
            MonthIdColl = new List<int>();
        }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int TranId { get; set; }
        public int StudentId { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }

        public List<int> MonthIdColl { get; set; }
    }
    public class FixedAmountStudentCollections : System.Collections.Generic.List<FixedAmountStudent>
    {
        public FixedAmountStudentCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
