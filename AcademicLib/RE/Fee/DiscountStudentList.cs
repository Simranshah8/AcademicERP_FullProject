using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class DiscountStudent
    {
        public DiscountStudent()
        {
            ClassName = "";
            SectionName = "";
        }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string Address { get; set; }
        public string Caste { get; set; }
        public string TranType { get; set; }
        public string DiscountType { get; set; }
        public string Remarks { get; set; }
        public string Details { get; set; }
        public string TransportPoint { get; set; }
        public string TransportRoute { get; set; }
        public string RoomName { get; set; }
        public bool IsLeft { get; set; }
        public string ClassSec
        {
            get
            {
                return ClassName + " " + SectionName;
            }
        }
        public int UserId { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
    }
    public class DiscountStudentCollections : System.Collections.Generic.List<DiscountStudent>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
