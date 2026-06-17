using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Infirmary
{
    public class HCInfirmaryInfo : ResponeValues
    {

        public int HCInfirmaryId { get; set; }

        public ClassSection ClassSection { get; set; }

        public int HealthCampaignId { get; set; }

        public HealthCampaign HealthCampaign { get; set; }

        public string Remarks { get; set; }

        public string DiseaseNames { get; set; }

        public DateTime OnDate { get; set; }

        public int TotalStudents { get; set; }

        public int PresentStudents { get; set; }

        public int AbsentStudents { get; set; }

    }
    public class HCInfirmaryInfoCollections : System.Collections.Generic.List<HCInfirmaryInfo>
    {
        public HCInfirmaryInfoCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}