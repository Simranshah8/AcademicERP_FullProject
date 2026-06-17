using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Medium : Common
    {
        public int? MediumId { get; set; }
        public int id
        {
            get
            {
                if (MediumId.HasValue)
                    return MediumId.Value;
                return 0;
            }
        }

    }

    public class MediumCollections : System.Collections.Generic.List<Medium>
    {
        public MediumCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
