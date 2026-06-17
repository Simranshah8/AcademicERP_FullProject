using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class ClassWiseDiscountSetup : ResponeValues
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int DiscountTypeId { get; set; }
        public string Remarks { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
    }
    public class ClassWiseDiscountSetupCollections : System.Collections.Generic.List<ClassWiseDiscountSetup>
    {
        public ClassWiseDiscountSetupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
