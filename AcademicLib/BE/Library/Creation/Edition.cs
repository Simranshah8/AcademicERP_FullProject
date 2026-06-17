using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class Edition : AcademicLib.BE.Academic.Common
    {
        public int? EditionId { get; set; }

        public int id
        {
            get
            {
                if (EditionId.HasValue)
                    return EditionId.Value;
                return 0;
            }
        }
    }

    public class EditionCollections : System.Collections.Generic.List<Edition>
    {
        public EditionCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
