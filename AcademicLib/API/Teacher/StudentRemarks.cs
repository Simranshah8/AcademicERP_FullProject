using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class StudentRemarks
    {
        public DateTime? forDate { get; set; }
        public int remarksTypeId { get; set; }
        public string description { get; set; }

        public string studentIdColl { get; set; }
        public bool parents { get; set; }
        public double point { get; set; }
        public BE.Academic.Transaction.REMARKSFOR RemarksFor { get; set; }
    }

    public class EmployeeRemarks
    {
        public DateTime? forDate { get; set; }
        public int remarksTypeId { get; set; }
        public string description { get; set; }

        public string employeeIdColl { get; set; }
        public bool parents { get; set; }
        public double point { get; set; }
    }
}
