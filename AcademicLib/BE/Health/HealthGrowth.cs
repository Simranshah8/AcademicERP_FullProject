using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.BE.Health.Transaction
{
    public class HealthGrowth : ResponeValues
    {
        public HealthGrowth()
        {
            AttachHelGroColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
        public int? TranId { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public DateTime? TestDate { get; set; }
        public string TestBy { get; set; }
        public string Remarks { get; set; }

        public string TestMiti { get; set; }
        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachHelGroColl { get; set; }
    }

    public class HealthGrowthCollections : System.Collections.Generic.List<HealthGrowth>
    {
        public HealthGrowthCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}