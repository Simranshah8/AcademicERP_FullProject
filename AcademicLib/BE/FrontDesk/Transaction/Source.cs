using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
   public class Source : ResponeValues
    {
        public int? SourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public int Status { get; set; }
        //for sunway
        public bool IsRefDetReq { get; set; }
        public string FieldName { get; set; }
    }
    public class SourceCollections : List<Source> {
        public SourceCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
 