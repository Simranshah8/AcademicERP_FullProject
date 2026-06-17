using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
   public class GatePassStudent : ResponeValues
    {
        public int? GatePassStudentId { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime ValidityTime { get; set; }
        public DateTime InTime { get; set; }
        public string Remarks { get; set; }
        public string AttachDocument { get; set; }
        public string AttachDocumentPath { get; set; }
    }
    public class GatePassStudentCollections : List<GatePassStudent> {
        public GatePassStudentCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
 