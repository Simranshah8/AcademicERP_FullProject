using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class EmployeeRemarks
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int EnrollNo { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public DateTime ForDate { get; set; }
        public string ForMiti { get; set; }
        public string RemarsType { get; set; }
        public string Remarks { get; set; }
        public string FilePath { get; set; }
        public string UserName { get; set; }
        public string UserPhotoPath { get; set; }
        public string RemarksFor { get; set; }
        public double Point { get; set; }
        public int NY { get; set; }
        public string NM { get; set; }
        public int ND { get; set; }
        public DateTime? LogDate { get; set; }
        public string LogMiti { get; set; }
        public string MonthName { get; set; }
        public string RemarksBy { get; set; } = "";

    }
    public class EmployeeRemarksCollections : System.Collections.Generic.List<EmployeeRemarks>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
