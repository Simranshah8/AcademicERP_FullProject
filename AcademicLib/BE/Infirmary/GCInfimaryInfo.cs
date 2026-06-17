using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Infirmary
{
    public class GCInfirmaryInfo : ResponeValues
    {

        public int GCInfirmaryId { get; set; }

        public ClassSection ClassSection { get; set; }

        public int GeneralCheckupId { get; set; }

        public GeneralCheckup GeneralCheckup { get; set; }

        public string Remarks { get; set; }

        public string DiseaseNames { get; set; }

        public DateTime OnDate { get; set; }

        public int TotalStudents { get; set; }

        public int PresentStudents { get; set; }

        public int AbsentStudents { get; set; }

    }
    public class GCInfirmaryInfoCollections : System.Collections.Generic.List<GCInfirmaryInfo>
    {
        public GCInfirmaryInfoCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}