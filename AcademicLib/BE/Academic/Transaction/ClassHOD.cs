using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ClassHOD
    {
        public int DepartmentId { get; set; }

        public int EmployeeId { get; set; }

        public int ShiftId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public bool IsHod { get; set; }

        public int HODId { get; set; }
        public string DepartmentName { get; set; }
        public string TeacherName { get; set; }
        public string ShiftName { get; set; }

        public int? BatchId { get; set; }
        public int? ClassYearId { get; set; }
        public int? SemesterId { get; set; }
        public string Batch { get; set; }
        public string ClassYear { get; set; }
        public string Semester { get; set; }
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? ClassShiftId { get; set; }
    }
    public class ClassHODCollections : System.Collections.Generic.List<ClassHOD>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
