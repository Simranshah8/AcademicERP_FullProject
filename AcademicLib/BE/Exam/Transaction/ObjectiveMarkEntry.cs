using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Exam.Transaction
{
   
    public class Student: ResponeValues
    {
        public int? StudentId { get; set; }
        public int? TranId { get; set; }
        public int? ObjectiveId { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? SubjectId { get; set; }
        public int? ExamTypeId { get; set; }
        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? SNo { get; set; }
        public string RegNo { get; set; } = "";
        public string StudentName { get; set; } = "";
        public int? RollNo { get; set; }
        public string SymbolNo { get; set; } = "";
        public string Remarks { get; set; } = "";
        public double? TotObjMark { get; set; }
        public double? Mark { get; set; }
        public bool? ColumnWiseFocus { get; set; }

    }
    public class StudentCollection: System.Collections.Generic.List<Student>
    {
        public string ResponseMSG { get; set; } = "";
        public bool IsSuccess { get; set; }
    }


}