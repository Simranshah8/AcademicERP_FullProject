using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class ClassYear : Common
    {
        public int? ClassYearId { get; set; }
        public int id
        {
            get
            {
                if (ClassYearId.HasValue)
                    return ClassYearId.Value;
                return 0;
            }
        }

    }

    public class ClassYearCollections : System.Collections.Generic.List<ClassYear>
    {
        public ClassYearCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
