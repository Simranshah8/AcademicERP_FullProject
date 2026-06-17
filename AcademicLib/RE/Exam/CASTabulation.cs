using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class CASTabulation 
    {
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public string BoardName { get; set; }
        public string BoardRegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; } 
        public int RollNo { get; set; }
        public string SubjectName { get; set; } 
        public string SubjectCodeTH { get; set; }
        public string SubjectCodePR { get; set; }
        public string SubjectCode { get; set; }
        public string CASType { get; set; }
        public int CASOrderNo { get; set; }
        public DateTime? ExamDate { get; set; }
        public string ExamMiti { get; set; }
        public double FullMark { get; set; }
        public double ObtainMark { get; set; }
        public string Remarks { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }
        public string ExamTypeName { get; set; }
        public int ExamTypeOrderNo { get; set; }
        public int SubjectOrderNo { get; set; }
        public string Under { get; set; }
        public string Scheme { get; set; }
        public string SymbolNo { get; set; }
        public bool IsAbsent { get; set; }
    }
    public class CASTabulationCollections : System.Collections.Generic.List<CASTabulation>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
