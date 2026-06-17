using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Infirmary
{

    public class TestResults
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }

    public class StudentTestResults
    {
        public int StudentId { get; set; }
        public string Remarks { get; set; }
        public List<TestResults> Results { get; set; }
    }

    public class HCInfirmary : ResponeValues
    { 
        public int HCInfirmaryId { get; set; }
        public int HealthCampaignId { get; set; }

        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public DateTime OnDate { get; set; }

        public string Remarks { get; set; }

        public List<int> ExaminerIds { get; set; }

        public List<StudentTestResults> StudentTestDetails { get; set; }

        public DateTime? LogDateTime { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedLogDateTime { get; set; }

    }

    public class HCInfirmaryCollections : System.Collections.Generic.List<HCInfirmary>
    {
        public HCInfirmaryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}