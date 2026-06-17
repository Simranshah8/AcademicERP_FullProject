using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Caste : Common
    {
        public int? CasteId { get; set; }
        public int id
        {
            get
            {
                if (CasteId.HasValue)
                    return CasteId.Value;
                return 0;
            }
        }

    }

    public class CasteCollections : System.Collections.Generic.List<Caste>
    {
        public CasteCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
