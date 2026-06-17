using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Faculty : Common
    {
        public int? FacultyId { get; set; }
        public int id
        {
            get
            {
                if (FacultyId.HasValue)
                    return FacultyId.Value;
                return 0;
            }
        }

    }

    public class FacultyCollections : System.Collections.Generic.List<Faculty>
    {
        public FacultyCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
