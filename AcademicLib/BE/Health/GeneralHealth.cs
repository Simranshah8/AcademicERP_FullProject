using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.BE.Health.Transaction
{
    public class GeneralHealth : ResponeValues
    {
        public GeneralHealth() 
        {
            AttachGenHeColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
        public int? TranId { get; set; }
        public DateTime? ForDate { get; set; }
        public int? CampaignProgram { get; set; }
        public string Remarks { get; set; }

        public string ForMiti { get; set; }

        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public string CampaignName { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachGenHeColl { get; set; }
    }
    public class GeneralHealthCollections : System.Collections.Generic.List<GeneralHealth>
    {
        public GeneralHealthCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}