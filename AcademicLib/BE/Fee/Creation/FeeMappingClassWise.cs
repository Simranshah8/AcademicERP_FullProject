using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeMappingClassWise : ResponeValues
    {
        public FeeMappingClassWise()
        {
            MonthColl = new List<FeeMappingMonth>();
        }
        public int TranId { get; set; }        
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int FeeItemId { get; set; }
        public string FeeItemName { get; set; }
        public double Rate { get; set; }
        public int ClassSNo { get; set; }
        public int FeeItemSNo { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int BatchId { get; set; }
        public int FacultyId { get; set; }

        public bool OneTimeApplicable { get; set; }

        public List<FeeMappingMonth> MonthColl { get; set; } = new List<FeeMappingMonth>();
    }
    public class FeeMappingClassWiseCollections : System.Collections.Generic.List<FeeMappingClassWise>
    {
        public FeeMappingClassWiseCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class FeeMappingMonth
    {
        public int MonthId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
