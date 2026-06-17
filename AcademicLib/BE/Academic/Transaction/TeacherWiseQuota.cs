using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class TeacherWiseQuota : ResponeValues
    {
        public int? TranId { get; set; }
        public int? EmployeeId { get; set; }
        public int? BranchId { get; set; }
        public int? AcademicYearId { get; set; }
        public int? WeekDay { get; set; }
        public int? NoofPeriod { get; set; }
        public int? TotalPeriod { get; set; }
        public int? AssignedPeriod { get; set; }
        public string Name { get; set; } = "";
        public string EmployeeCode { get; set; } = "";
        public string Department { get; set; } = "";
        public string Designation { get; set; } = "";
        //Added Field
        public bool CanBlock { get; set; }
        public string EmpStatus { get; set; } = "";
        public int? RemainingQuota { get; set; }
    }
    public class TeacherWiseQuotaCollections : System.Collections.Generic.List<TeacherWiseQuota>
    {
        public TeacherWiseQuotaCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}