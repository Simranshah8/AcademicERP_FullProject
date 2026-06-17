using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class MarkEntry
    {
        public int ExamTypeId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int PaperType { get; set; }
        public string Remarks { get; set; }        
        public string ObtainMarkTH { get; set; }
        public string ObtainMarkPR { get; set; }
        public string ObtainMark { get; set; }

        public double OM_TH { get; set; }
        public double OM_PR { get; set; }
        public double OM { get; set; }
        public string O_Grade { get; set; }
        public bool IsAbsentTH { get; set; }
        public bool IsAbsentPR { get; set; }
        public bool IsAbsent { get; set; }

        public int ReExamTypeId { get; set; }
        public string SubjectRemarks { get; set; }

    }
    public class MarkEntryCollections : System.Collections.Generic.List<MarkEntry>
    {

    }

    public class StudentWiseComment
    {
        public int StudentId { get; set; }
        public int ExamTypeId { get; set; }
        public string Comment { get; set; }
    }
    public class StudentWiseCommentCollections : System.Collections.Generic.List<StudentWiseComment>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
