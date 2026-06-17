using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class ClassTeacher

    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
     

        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }
    public class ClassTeacherCollection : System.Collections.Generic.List<ClassTeacher>
    {
        public ClassTeacherCollection()
        {
            ResponseMsg = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMsg { get; set; }

    }
}