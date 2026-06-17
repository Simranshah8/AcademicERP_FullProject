using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
  public  class Author: ResponeValues
    {
        public int? AuthorId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public string Nationality { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Description { get; set; }

        public string SpinerAuthorMark { get; set; }
        public int id
        {
            get
            {
                if (AuthorId.HasValue)
                    return AuthorId.Value;
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
        
    }
    public class AuthorCollections : System.Collections.Generic.List<Author> {
        public AuthorCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
