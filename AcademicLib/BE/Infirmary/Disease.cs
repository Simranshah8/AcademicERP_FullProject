using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Infirmary
{
    public class Disease : ResponeValues
    {

        public int? DiseaseId { get; set; }

        public int? OrderNo { get; set; }

        public string DiseaseName { get; set; }

        /*Later change it to enum*/
        public int Severity { get; set; }

        public string Description { get; set; }

        public DateTime? LogDateTime { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedLogDateTime { get; set; }

    }
    public class DiseaseCollections : System.Collections.Generic.List<Disease>
    {
        public DiseaseCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


    public class DiseaseSeverity : ResponeValues
    {

        public int SeverityId { get; set; }
        public string Severity { get; set; }
    }

    public class DiseaseSeverityCollections : System.Collections.Generic.List<DiseaseSeverity>
    {
        public DiseaseSeverityCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}