using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class SENT : ResponeValues
    {
        public SENT()
        {
            Name = "";
            Title = "";
            Description = "";
            Recipients = "";
            EmailBCC = "";
            EmailCC = "";
        }
        public int? TranId { get; set; }        
        public int ForATS { get; set; }
        public int TemplateType { get; set; }
        public int ActionType { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Recipients { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
    }

    public class SENTCollections : System.Collections.Generic.List<SENT>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}
