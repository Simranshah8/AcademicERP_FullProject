using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.AppCMS.Creation
{
    public class RulesRegulation : ResponeValues
    {
        public RulesRegulation()
        {
            Title = "";
            Description = "";
            ImagePath = "";
        }
        public int? RulesRegulationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OrderNo { get; set; }
        public string SubmenuTitle { get; set; } = "";
        public string GuId { get; set; }
    }
    public class RulesRegulationCollections : System.Collections.Generic.List<RulesRegulation>
    {
        public RulesRegulationCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
