using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ClassSchedule 
    {
        public ClassSchedule()
        {
            AlternetColl = new List<ClassScheduleAlternet>();
        }
        public int? TranId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int ClassShiftId { get; set; }
        public int DayId { get; set; }
        public int Period { get; set; }
        public int? SubjectId { get; set; }
        public int? EmployeeId { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }

        public List<ClassScheduleAlternet> AlternetColl { get; set; }
    }
    public class ClassScheduleAlternet
    {
        public int? TranId { get; set; }
        public int? SubjectId { get; set; }
        public int? EmployeeId { get; set; }
    }

    public class ClassScheduleCollections : System.Collections.Generic.List<ClassSchedule>
    {
        public ClassScheduleCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
