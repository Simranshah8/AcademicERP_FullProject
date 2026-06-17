using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class PostalService : ResponeValues
    {

        public string ReferenceNumber { get; set; }
        public int? PostalServicesId { get; set; }

        public string FromTitle { get; set; }
        public string RefrenceNo { get; set; }
        public string Address { get; set; }
        public string Totitle { get; set; }

        public string Remarks { get; set; }
        public DateTime Date { get; set; }
        public string Miti { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
        public string AttachDocumentPath { get; set; }
        public string Description { get; set; }

        public string PhotoPath { get; set; }

    }

    public class PostalServiceCollections : System.Collections.Generic.List<PostalService>
    {
        public PostalServiceCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
