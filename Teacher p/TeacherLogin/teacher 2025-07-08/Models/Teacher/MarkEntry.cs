using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{

    public class MarkEntryVal
    {

        public int classId { get; set; } 
        public int sectionId { get; set; } 
        public int subjectId { get; set; } 
        public int examTypeId { get; set; } 
        public int? casTypeId { get; set; }
        public DateTime? examDate { get; set; }
        
    }
    public class MarkEntry
    {
        public int ExamTypeId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int PaperType { get; set; }
        public string Remarks { get; set; }
        public string ObtainMarkTH { get; set; }
        public string ObtainMarkPR { get; set; }

        public double GraceMarkTH { get; set; }
        public double GraceMarkPR { get; set; }
        public string SubjectRemarks { get; set; }
    }
    public class MarkEntryCollections : System.Collections.Generic.List<MarkEntry>
    {

    }

    public class StudentForMarkEntry
    {
        public StudentForMarkEntry()
        {
            Name = "";
            RegdNo = "";
            BoardRegdNo = "";
            SymbolNo = "";
            PhotoPath = "";
        }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string SymbolNo { get; set; }
        public string PhotoPath { get; set; }
        public double CRTH { get; set; }
        public double CRPR { get; set; }
        public double FMTH { get; set; }
        public double FMPR { get; set; }
        public double PMTH { get; set; }
        public double PMPR { get; set; }
        public int PaperType { get; set; }
        public string ObtainMarkTH { get; set; }
        public string ObtainMarkPR { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public string Remarks { get; set; }
        public string SubjectRemarks { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }

    public class StudentForMarkEntryCollections : System.Collections.Generic.List<StudentForMarkEntry>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}