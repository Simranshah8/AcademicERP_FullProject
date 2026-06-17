using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
   public class Rack : ResponeValues
    {
     public int? RackId { set; get; }
     public string Name { set; get; }
     public string RackNo { set; get; }
     public string Location { set; get; }
     public string Description { set; get; }

        public int id
        {
            get
            {
                if (RackId.HasValue)
                    return RackId.Value;
                return 0;
            }
        }

        public string text
        {
            get
            {
                return Name;
            }
        }
    }
    public class RackCollections : System.Collections.Generic.List<Rack> {
        public RackCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
