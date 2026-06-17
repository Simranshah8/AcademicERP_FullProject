using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
   public class PostalReceive : ResponeValues
    {
        public int? PostalReceiveId { get; set; }
        public string FormTitle { get; set; }
        public string ReferenceNumber { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string AttachDocument { get; set; }
        public string AttacDocumentPath { get; set; }
        public string Remarks { get; set; }
    }
    public class PostalReceiveCollections : List<PostalReceive> {
        public PostalReceiveCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
 
 