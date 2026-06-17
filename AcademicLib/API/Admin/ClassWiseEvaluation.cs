using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class ClassWiseEvaluation
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int ExamTypeId { get; set; }
        public int StudentId { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string PhotoPath { get; set; }
        public double ObtainMark { get; set; }
        public double Per { get; set; }
        public string Division { get; set; }
        public string Grade { get; set; }
        public double GPA { get; set; }
        public int RankInClass { get; set; }
        public int RankInSection { get; set; }
        public bool IsFail { get; set; }

    }
    public class ClassWiseEvaluationCollections : System.Collections.Generic.List<ClassWiseEvaluation> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
