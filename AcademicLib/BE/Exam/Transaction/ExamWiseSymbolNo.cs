using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
  public  class ExamWiseSymbolNo : ResponeValues
    {        

        public ExamWiseSymbolNo()
        {
            StartAlpha = "";
            ReExamSubjectList = new List<int>();
        }
        public int StudentId { get; set; }
        public int ExamTypeId { get; set; }
        public int StartNumber { get; set; }
        public int PadWith { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string SymbolNo { get; set; }
        public string StartAlpha { get; set; }

        public int ReExamTypeId { get; set; }
        public List<int> ReExamSubjectList { get; set; }
        public int? ClassYearId { get; set; }
        public int? SemesterId { get; set; }
    }
    public class ExamWiseSymbolNoCollections : System.Collections.Generic.List<ExamWiseSymbolNo> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

  

    
    public class ImportExamWiseSymbolNo
    {
        public string ExamType { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string SymbolNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
     
    }
}
 