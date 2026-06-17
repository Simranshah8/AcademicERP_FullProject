using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.RE
{

    public class StudentHealthReport : ResponeValues
    {
        public int SNo { get; set; }
        public int TranId { get; set; }
        public int? StudentId { get; set; }
        public string ObservedOn { get; set; }
        public string AdmissionNo { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string ObservedAt { get; set; } = "";
        public string HealthIssue { get; set; }
        public bool IsAdmitted { get; set; }
        public string AdmittedAt { get; set; }
        public string AdmittedDate { get; set; }
        public bool MedicineGiven { get; set; }
        public string Age { get; set; } = "";
        public string PrescribedBy { get; set; }
        public DateTime? ObservedTime { get; set; }

    }

    public class StudentHealthReportCollections : List<StudentHealthReport>
    {
        public StudentHealthReportCollections()
        {
            ResponseMSG = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class StudentHealthPastHistory : ResponeValues
    {
        public int SNo { get; set; }
        public int TranId { get; set; }
        public int? StudentId { get; set; }
        public string ObservedOn { get; set; }
        public string AdmissionNo { get; set; }
        public string Name { get; set; }
        public int? RollNo { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string HealthIssue { get; set; }
        public string Details { get; set; }
        public bool MedicineGiven { get; set; }
        public string Age { get; set; } = "";

    }

    public class StudentHealthPastHistoryCollections : List<StudentHealthPastHistory>
    {
        public StudentHealthPastHistoryCollections()
        {
            ResponseMSG = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }

}
