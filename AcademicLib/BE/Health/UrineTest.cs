using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.BE.Health.Transaction
{
    public class UrineTest : ResponeValues
    {
        public UrineTest()
        {
            AttachUrTeColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
        public int? TranId { get; set; }
        public DateTime? TestDate { get; set; }
        public string Color { get; set; }
        public string Protein { get; set; }
        public string Sugar { get; set; }
        public string Transparency { get; set; }
        public string WBC { get; set; }
        public string RBC { get; set; }
        public string Others { get; set; }

        public string TestMiti { get; set; }

        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachUrTeColl { get; set; }
    }
    public class UrineTestCollections : System.Collections.Generic.List<UrineTest>
    {
        public UrineTestCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}