using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class ExamGradeWiseEvaluation
    {
        public int ExamTypeId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ExamTypeName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Grade { get; set; }
        public int NoOfStudent { get; set; }
    }
    public class ExamGradeWiseEvaluationCollections : System.Collections.Generic.List<ExamGradeWiseEvaluation>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
