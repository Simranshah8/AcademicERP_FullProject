using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeItemGroup : ResponeValues
    {
        public int? FeeItemGroupId { get; set; }
        public string Name { get; set; }

        public List<int> FeeItemIdColl { get; set; }
    }

    public class FeeItemGroupCollections : System.Collections.Generic.List<FeeItemGroup>
    {
        public FeeItemGroupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
