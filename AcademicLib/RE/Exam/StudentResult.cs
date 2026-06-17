using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class StudentResult
    {
        public StudentResult()
        {
            SubjectMarkColl = new List<StudentSubjectResult>();
            SubjectList = new List<StudentSubject>();
        }
        public int ExamOrderNo { get; set; }
        public int ExamTypeId { get; set; }
        public string ExamTypeName { get; set; }
        public double FM { get; set; }
        public double PM { get; set; } 
        public double OM { get; set; } 
        public double Per { get; set; }
        public string Division { get; set; }
        public double GPA { get; set; }
        public string Grade { get; set; }
        public int RankInClass { get; set; }
        public int RankInSection { get; set; }
        public string Result { get; set; }

        public List<StudentSubjectResult> SubjectMarkColl { get; set; }
        public List<StudentSubject> SubjectList { get; set; }
    }
    public class StudentResultCollections : System.Collections.Generic.List<StudentResult> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentSubjectResult
    {        
        public string SubjectName { get; set; }
        public int SNo { get; set; }
        public double FM { get; set; }
        public double PM { get; set; }
        public double OM { get; set; }
        public double Per { get; set; }
        public string Grade { get; set; }
        public double GP { get; set; }
        public int SubjectId { get; set; }
        public double ObtainTH { get; set; }
        public double ObtainPR { get; set; }
        public bool IsFail { get; set; }
        public bool IsFailPR { get; set; }
        public bool IsFailTH { get; set; }
    }

    public class StudentSubject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public int SNo { get; set; }
        public double AOM { get; set; }
        public double APer { get; set; }


    }
}
