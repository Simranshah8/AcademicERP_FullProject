using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Exam.Transaction
{

    public class ClassSummary : ResponeValues
    {

        public int? TranId { get; set; }
        public DateTime? TestDate { get; set; }
        public int? ClassId { get; set; }
        public int? SubjectId { get; set; }
        public int? LessonId { get; set; }
        public string LessonName { get; set; } = "";
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; } = "";
        public double? FullMarks { get; set; }
        public double? PassMarks { get; set; }
        public int? PresentStudent { get; set; }
        public int? TotalStudent { get; set; }
        public int? LessonSno { get; set; }
        public string TestDateBS { get; set; } = "";
        public int? TotalPassStudent { get; set; }

    }
    public class ClassSummaryCollections : System.Collections.Generic.List<ClassSummary>
    {
        public ClassSummaryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ClassSummarySubjectClassWise : ResponeValues
    {

        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";

    }

    public class ClassSummarySubjectClassWiseCollections : System.Collections.Generic.List<ClassSummarySubjectClassWise>
    {
        public ClassSummarySubjectClassWiseCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ClassSummaryDetailsById : ResponeValues
    {

        public int? TranId { get; set; }
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? LessonId { get; set; } 
        public string LessonName { get; set; } = "";
        public DateTime? TestDate { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; } = "";
        public int? StudentId { get; set; }
        public string RegdNo { get; set; } = "";
        public int? RollNo { get; set; }
        public string StudentName { get; set; } = "";
        public string BoardRegdNumber { get; set; } = "";
        public double? FullMarks { get; set; }
        public double? PassMarks { get; set; }
        public string Remarks { get; set; } = "";
        public double? ObtMarks { get; set; } 
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";

        public bool IsAbsent { get; set; }

    }

    public class ClassSummaryDetailsByIdCollections : System.Collections.Generic.List<ClassSummaryDetailsById>
    {
        public ClassSummaryDetailsByIdCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}

