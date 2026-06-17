using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class EmployeeBirthDay
    {
        public EmployeeBirthDay()
        {
            Code = "";
            Name = "";
            Department = "";
            Designation = "";
            FatherName = "";
            ContactNo = "";
            PhotoPath = "";
            Address = "";
        }
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int EnrollNo { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public int AgeYear { get; set; }
        public int AgeMonth { get; set; }
        public int AgeDay { get; set; }
        public string PhotoPath { get; set; }
        public DateTime DOB_AD { get; set; }
        public string DOB_BS { get; set; }

        public string Age
        {
            get
            {
                string a = "";

                if (AgeYear > 0)
                    a = AgeYear.ToString() + " Y ";

                if (AgeMonth > 0)
                    a = a + AgeMonth.ToString() + "M ";

                if (AgeDay > 0)
                    a = a + AgeDay.ToString() + " D ";

                return a;
            }
        }
    }
    public class EmployeeBirthDayCollections : System.Collections.Generic.List<EmployeeBirthDay> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
