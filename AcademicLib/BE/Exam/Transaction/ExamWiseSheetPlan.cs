using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.BusinessEntity.Exam.Transaction
{
   public class ExamWiseSheetPlan :  ResponeValues
    {
        public int Exam { get; set; }
        public int ExaShiftId { get; set; }
        public int ClassId { get; set; }
        public string Room { get; set; }
        public int SelectReportTemplate { get; set; }
    }
    public class ExamWiseSheetPlanCollections : System.Collections.Generic.List<ExamWiseSheetPlan> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
