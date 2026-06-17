using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class StudentAchievement
    {
        public int? StudentId { get; set; }
        public string Remarks { get; set; }
        public double? Point { get; set; }
        public int ExamTypeId { get; set; }
        public int RemarksTypeId { get; set; }
        public int RemarksFor { get; set; }
        public DateTime? ForDate { get; set; }
    }

    public class PrevAchievement
    {
        public int StudentId { get; set; }
        public int ExamTypeId { get; set; }
        public string Remarks { get; set; }
        public double Point { get; set; }
    }   
}