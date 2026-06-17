using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.AppCMS.Creation
{
    public class MandatoryDisclosure : ResponeValues
    {
        public MandatoryDisclosure()
        {
            mandatoryDisclosureColl = new MandatoryDisclosureCollections();
        }
        public int? TranId;
        public string Title { get; set; } = "";
        public int? OrderNo { get; set; }
        public string Description { get; set; } = "";
        public MandatoryDisclosureCollections mandatoryDisclosureColl { get; set; }
    }
    public class MandatoryDisclosureCollections : List<MandatoryDisclosure>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}