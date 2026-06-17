using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS
{

    public class ProudAlumni : ResponeValues
    {

        public int? TranId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; } = "";
        public string DegreeDetails { get; set; } = "";
        public string CurrentCompany { get; set; } = "";
        public string Position { get; set; } = "";
        public int? OrderNo { get; set; }
        public string Description { get; set; } = "";
        public string ImagePath { get; set; } = "";
    }
    public class ProudAlumniCollections : List<ProudAlumni>
    {
        public ProudAlumniCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}

