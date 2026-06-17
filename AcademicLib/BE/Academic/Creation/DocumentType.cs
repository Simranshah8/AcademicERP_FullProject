using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class DocumentType : Common
    {
        public int? DocumentTypeId { get; set; }
        public int id
        {
            get
            {
                if (DocumentTypeId.HasValue)
                    return DocumentTypeId.Value;
                return 0;
            }
        }

    }

    public class DocumentTypeCollections : System.Collections.Generic.List<DocumentType>
    {
        public DocumentTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
