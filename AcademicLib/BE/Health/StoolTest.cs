using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.BE.Health.Transaction
{
    public class StoolTest : ResponeValues
    {
        public StoolTest()
        {
            AttachStTeColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
        public int? TranId { get; set; }
        public DateTime? TestDate { get; set; }
        public string Color { get; set; }
        public string Mucus { get; set; }
        public string Puscell { get; set; }
        public string RBC { get; set; }
        public string Cyst { get; set; }
        public string Ova { get; set; }
        public string Others { get; set; }
        public string TestMiti { get; set; }

        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachStTeColl { get; set; }
    }
    public class StoolTestCollections : System.Collections.Generic.List<StoolTest>
    {
        public StoolTestCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}