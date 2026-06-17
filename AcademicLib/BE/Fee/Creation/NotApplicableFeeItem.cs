using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class NotApplicableFeeItem : ResponeValues
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int FeeItemId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
    }
    public class NotApplicableFeeItemCollections : System.Collections.Generic.List<NotApplicableFeeItem>
    {
        public NotApplicableFeeItemCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
