using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class MarkSubmit
    {
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public bool IsPending { get; set; }
        public DateTime? SubmitDateTime_AD { get; set; }
        public string SubmitDate_BS { get; set; }
        public string UserName { get; set; }
        public string ClassTeacher { get; set; }
        public string SubjectTeacherName { get; set; }
        public string TeacherContactNo { get; set; }
        public int UserId { get; set; }
        public DateTime? DeadLine_AD { get; set; }
        public string DeadLine_BS { get; set; }
        public int? DueDays { get; set; }
    }
    public class MarkSubmitCollections : System.Collections.Generic.List<MarkSubmit>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}