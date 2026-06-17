using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Semester : Common
    {
        public int? SemesterId { get; set; }
        public int id
        {
            get
            {
                if (SemesterId.HasValue)
                    return SemesterId.Value;
                return 0;
            }
        }

    }

    public class SemesterCollections : System.Collections.Generic.List<Semester>
    {
        public SemesterCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
