
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class BookTitle : AcademicLib.BE.Academic.Common
    {
        public int? BookTitleId { get; set; }

        public int id
        {
            get
            {
                if (BookTitleId.HasValue)
                    return BookTitleId.Value;
                return 0;
            }
        }

    }

    public class BookTitleCollections : System.Collections.Generic.List<BookTitle> {

        public BookTitleCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
