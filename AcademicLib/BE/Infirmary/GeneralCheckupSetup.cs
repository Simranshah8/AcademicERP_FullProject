using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Infirmary
{
    public class GeneralCheckup : ResponeValues
    {

        public int? GeneralCheckupId { get; set; }

        public string GeneralCheckupName { get; set; }

        public int Month { get; set; }

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
    public class GeneralCheckupCollections : System.Collections.Generic.List<GeneralCheckup>
    {
        public GeneralCheckupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}