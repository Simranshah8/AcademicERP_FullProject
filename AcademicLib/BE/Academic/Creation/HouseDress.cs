using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class HouseDress : Common
    {
        public int? HouseDressId { get; set; }
        public int id
        {
            get
            {
                if (HouseDressId.HasValue)
                    return HouseDressId.Value;
                return 0;
            }
        }

        public int? HouseNameId { get; set; }
        public string ColorCode { get; set; }
        public string HouseName { get; set; }
    }

    public class HouseDressCollections : System.Collections.Generic.List<HouseDress>
    {
        public HouseDressCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
