using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class MessFeeItemMapping : ResponeValues
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int FeeItemId { get; set; }
        public string Remarks { get; set; }
    }
    public class MessFeeItemMappingCollections : System.Collections.Generic.List<MessFeeItemMapping>
    {
        public MessFeeItemMappingCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
