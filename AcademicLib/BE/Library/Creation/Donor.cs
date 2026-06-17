using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
  public  class Donor : ResponeValues
    {
        public int? DonorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }

        public int id
        {
            get
            {
                if (DonorId.HasValue)
                    return DonorId.Value;
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
        public byte[] Logo { get; set; }
        public string LogoPath { get; set; }
        public List<PublicationContactPerson> ContactPersonList { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
    }
    public class DonorCollections : System.Collections.Generic.List<Donor> {
        public DonorCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
