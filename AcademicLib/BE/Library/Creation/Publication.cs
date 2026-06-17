using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
  public  class Publication : ResponeValues
    {
        public Publication()
        {
            ContactPersonList = new List<PublicationContactPerson>();
        }
        public int? PublicationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }

        public string Description { get; set; }
        
        public byte[] Logo { get; set; }
        public string LogoPath { get; set; }

        public int id
        {
            get
            {
                if (PublicationId.HasValue)
                    return PublicationId.Value;
                return 0;
            }
        }

        public string text
        {
            get
            {
                return Name;
            }
        }

        public List<PublicationContactPerson> ContactPersonList { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
    }
    public class PublicationCollections : System.Collections.Generic.List<Publication> {
        public PublicationCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class PublicationContactPerson
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string MobileNo { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
    }
}
