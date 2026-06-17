using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class HouseName : Common
    {
        public HouseName()
        {
            HouseInchargeIdColl = new List<int>();
            HouseMemberIdColl = new List<int>();
        }
        public int? HouseNameId { get; set; }
        public int id
        {
            get
            {
                if (HouseNameId.HasValue)
                    return HouseNameId.Value;
                return 0;
            }
        }

        public int? CoOrdinatorId { get; set; }
        public string CoOrdinatorName { get; set; }
        public string CoOrdinatorCode { get; set; }

        public int? CaptainId_Boy { get; set; }
        public int? ViceCaptainId_Boy { get; set; }
        public int? CaptainId_Girl { get; set; }
        public int? ViceCaptainId_Girl { get; set; }
        public List<int> HouseInchargeIdColl { get; set; }
        public List<int> HouseMemberIdColl { get; set; }
    }

    public class HouseNameCollections : System.Collections.Generic.List<HouseName>
    {
        public HouseNameCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
