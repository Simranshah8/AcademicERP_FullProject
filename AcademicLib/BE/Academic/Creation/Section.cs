using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Section : Common
    {
        public int? SectionId { get; set; }
        public int id
        {
            get
            {
                if (SectionId.HasValue)
                    return SectionId.Value;
                return 0;
            }
        }

    }

    public class SectionCollections : System.Collections.Generic.List<Section>
    {
        public SectionCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
