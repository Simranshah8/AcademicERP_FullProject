using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class ExamWiseEvaluation
    {
        public int ExamTypeId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ExamTypeName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int NoOfStudent { get; set; }
        public int NoOfPass { get; set; }
        public int NoOfFail { get; set; }
        public double PassPer { get; set; }
        public double FailPer { get; set; }
    }
    public class ExamWiseEvaluationCollections : System.Collections.Generic.List<ExamWiseEvaluation>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
