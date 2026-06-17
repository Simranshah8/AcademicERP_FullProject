using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeMappingMediumWise : ResponeValues
    {
        public int TranId { get; set; }
        public int MediumId { get; set; }
        public int ClassId { get; set; }
        public int FeeItemId { get; set; } 
        public double Rate { get; set; }

        public string ClassName { get; set; }
        public string FeeItemName { get; set; }
    }
    public class FeeMappingMediumWiseCollections : System.Collections.Generic.List<FeeMappingMediumWise>
    {
        public FeeMappingMediumWiseCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
