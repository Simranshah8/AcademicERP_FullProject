using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ClassTeacher : ResponeValues
    {
        public int? ClassTeacherId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }

        public int TeacherId { get; set; }
       
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }

        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }

    }
    public class ClassTeacherCollections : List<ClassTeacher> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
