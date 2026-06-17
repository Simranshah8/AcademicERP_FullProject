using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class ClassWiseAttendance
    {
        public DateTime forDate { get; set; }
        public int inOutMode { get; set; }
        public int? sectionId { get; set; }
        public int classId { get; set; }
        public int StudentId { get; set; }
        public int Attendance { get; set; }
        public int LateMin { get; set; }
        public string Remarks { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}