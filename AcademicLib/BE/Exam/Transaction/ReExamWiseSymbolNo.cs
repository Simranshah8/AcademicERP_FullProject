using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ReExamWiseSymbolNo
    {
        public ReExamWiseSymbolNo()
        {
            ExamSubjectList = new List<ReExamSubject>();
            SymbolNo = "";
            PadWith = 0;
            Prefix = "";
            Suffix = "";
            StartAlpha = "";
            StartNumber = 1;
          
        }
        public int ExamTypeId { get; set; }
        public int ReExamTypeId { get; set; }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string SymbolNo { get; set; }
        public int StartNumber { get; set; }
        public int PadWith { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string StartAlpha { get; set; }

        public List<ReExamSubject> ExamSubjectList { get; set; }
       
    }
    public class ReExamWiseSymbolNoCollections : System.Collections.Generic.List<ReExamWiseSymbolNo>
    {
        public ReExamWiseSymbolNoCollections()
        {
            SubjectList = new List<Academic.Creation.Subject>();
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

        public List<BE.Academic.Creation.Subject> SubjectList { get; set; }
    }
    public class ReExamSubject
    {
        public int SubjectId { get; set; }
        public string ObtainMark { get; set; }
        public bool IsFail { get; set; }
        public bool ConductReExam { get; set; }
        public bool IsFailTH { get; set; }
        public bool IsFailPR { get; set; }
    }
}
