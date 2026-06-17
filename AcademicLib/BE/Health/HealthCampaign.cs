using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.BE.Health.Transaction
{
    public class HealthCampaign : ResponeValues
    {
        public HealthCampaign()
        {
            HealthCampaignColl = new List<HealthCampaign_HC>();
        }
        public int? TranId { get; set; }
        public string CampaignName { get; set; }
        public DateTime? ForDate { get; set; }
        public string OrganizedBy { get; set; }
        public string Description { get; set; }

        public string ForMiti { get; set; }

        public List<HealthCampaign_HC> HealthCampaignColl { get; set; }
    }
    public class HealthCampaignCollections : System.Collections.Generic.List<HealthCampaign>
    {
        public HealthCampaignCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class HealthCampaign_HC
    {
        public int TranId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
    }
}