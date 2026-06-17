using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ExtraCertificateIssue : ResponeValues
    {
        public int SNo { get; set; }
        public int? TranId { get; set; }
        public int ExtraEntityId { get; set; }
        public int? AcademicYearId { get; set; }
        public int? AutoNumber { get; set; }
        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? DOB { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int? RollNo { get; set; }
        public string RegdNo { get; set; }
        public string Attributes { get; set; }

        public string ForName { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueMiti { get; set; }
    }

    public class ExtraCertificateIssueCollections : System.Collections.Generic.List<ExtraCertificateIssue>
    {
        public ExtraCertificateIssueCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}