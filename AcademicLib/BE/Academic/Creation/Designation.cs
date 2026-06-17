using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Designation : Common
    {
        public int? DesignationId { get; set; }
        public int id
        {
            get
            {
                if (DesignationId.HasValue)
                    return DesignationId.Value;
                return 0;
            }
        }

    }

    public class DesignationCollections : System.Collections.Generic.List<Designation>
    {
        public DesignationCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
