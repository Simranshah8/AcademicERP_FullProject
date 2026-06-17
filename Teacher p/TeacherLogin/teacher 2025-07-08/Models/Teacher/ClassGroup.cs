using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class ClassGroup  
    {
        public ClassGroup()
        {
            Name = "";
            SubjectName = "";
            EmployeeName = "";
            ForClass = "";
            DetailsColl = new List<ClassGroupDetails>();
        }
        public int? ClassGroupId { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public int EmployeeId { get; set; }
        public List<ClassGroupDetails> DetailsColl { get; set; }

        public string SubjectName { get; set; }
        public string EmployeeName { get; set; }
        public string ForClass { get; set; }
    }
 

    public class ClassGroupDetails
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
    }
}