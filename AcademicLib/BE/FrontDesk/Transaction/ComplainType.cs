using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class ComplainType : ResponeValues
    {
        public int? ComplainTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ComplainTypeFor { get; set; }

        public int OrderNo { get; set; }
    }
    public class ComplainTypeCollections : List<ComplainType> {
        public ComplainTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
