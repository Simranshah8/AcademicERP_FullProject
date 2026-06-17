using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Infirmary
{
    public class HealthCampaign : ResponeValues
    {

        public int? HealthCampaignId { get; set; }

        public string HealthCampaignName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Organizer { get; set; }

        public string Description { get; set; }

        public bool IsVaccination { get; set; }

        public ClassSectionCollections ClassSectionIds { get; set; }

        public List<int> ExaminerIds { get; set; }

        public List<int> DiseaseIds { get; set; }

        public List<int> VaccineIds { get; set; }

        public List<int> TestIds { get; set; }

        public DateTime? LogDateTime { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedLogDateTime { get; set; }

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


    public class ClassSection : ResponeValues
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
    }

    public class ClassSectionCollections : System.Collections.Generic.List<ClassSection>
    {
        public ClassSectionCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}