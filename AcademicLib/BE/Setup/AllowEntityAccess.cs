using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class AllowEntityAccess
    {
        public int TranId { get; set; } 
        public int id { get; set; }
        public string text { get; set; }
        public bool IsAllow { get; set; }

        public int? ForUserId { get; set; }
        public int? ForGroupId { get; set; }
    }
    public class AllowEntityAccessCollections : System.Collections.Generic.List<AllowEntityAccess>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }

   
    

}
