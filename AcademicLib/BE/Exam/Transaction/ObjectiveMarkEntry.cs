using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ObjectiveMarkEntry: ResponeValues
    {

    }

    public class Student: ResponeValues
    {
        public int? StudentId { get; set; }
        public string RegNo { get; set; } = "";
        public string StudentName { get; set; } = "";
        public int? RollNo { get; set; }
        public string SymbolNo { get; set; } = "";
    }
    public class StudentCollection: System.Collections.Generic.List<Student>
    {
        public string ResponseMSG { get; set; } = "";
        public bool IsSuccess { get; set; }
    }


}