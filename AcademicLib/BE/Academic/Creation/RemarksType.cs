using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class RemarksType : Common
    {
        public int? RemarksTypeId { get; set; }
        public int id
        {
            get
            {
                if (RemarksTypeId.HasValue)
                    return RemarksTypeId.Value;
                return 0;
            }
        }

    }

    public class RemarksTypeCollections : System.Collections.Generic.List<RemarksType>
    {
        public RemarksTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
