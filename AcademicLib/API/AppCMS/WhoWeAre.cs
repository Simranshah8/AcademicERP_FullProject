using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.AppCMS
{
    public class WhoWeAre : ResponeValues
    {
        public WhoWeAre()
        {
            AboutDet = new BE.AppCMS.Creation.AboutUs();
            AdmissionProcedureList = new List<BE.AppCMS.Creation.AdmissionProcedure>();
            RulesRegulationList = new List<BE.AppCMS.Creation.RulesRegulation>();
            ContactDet = new BE.AppCMS.Creation.Contact();
        }
        public BE.AppCMS.Creation.AboutUs AboutDet { get; set; }
        public List<BE.AppCMS.Creation.AdmissionProcedure> AdmissionProcedureList { get; set; }
        public List<BE.AppCMS.Creation.RulesRegulation> RulesRegulationList { get; set; }

        public BE.AppCMS.Creation.Contact ContactDet { get; set; }
    }
}
