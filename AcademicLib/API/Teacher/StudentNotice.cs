using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class StudentNotice
    {
        public string studentIdColl { get; set; }
        public string title { get; set; }

        public string description { get; set; }

        public bool parents { get; set; }
        public string branchCode { get; set; }

    }

    public class EmployeeNotice
    {
        public string employeeIdColl { get; set; }
        public string title { get; set; }

        public string description { get; set; }

        public bool parents { get; set; }
        public string branchCode { get; set; }

    }
}
