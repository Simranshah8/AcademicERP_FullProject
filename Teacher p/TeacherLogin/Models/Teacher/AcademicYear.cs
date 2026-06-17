using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class AcademicYear
    {
        public int? AcademicYearId { get; set; }
        public int id
        {
            get
            {
                if (AcademicYearId.HasValue)
                    return AcademicYearId.Value;
                return 0;
            }
        }
        public string Name { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
        public bool IsRunning { get; set; }
        public int? CostClassId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string yearNepali { get; set; } = "";
        public string yearEnglish { get; set; } = "";
    }
}