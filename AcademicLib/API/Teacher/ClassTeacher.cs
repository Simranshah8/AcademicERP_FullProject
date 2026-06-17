using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class ClassTeacher
    {
        public ClassTeacher()
        {
            ClassName = "";
            SectionName = "";
        }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }

        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
    public class ClassTeacherCollections : System.Collections.Generic.List<ClassTeacher>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
