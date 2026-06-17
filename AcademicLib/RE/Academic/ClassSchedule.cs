using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class ClassSchedule
    {
        public ClassSchedule()
        {
            ShiftName = "";
            ShiftStartTime = "";
            ShiftEndTime = "";
            ClassName = "";
            SectionName = "";
            StartTime = "";
            EndTime = "";
            SubjectName = "";
            TeacherAddress = "";
            TeacherContactNo = "";
            TeacherName = "";
            Level = "";
            Faculty = "";
            Semester = "";
            ClassYear = "";
            Batch = "";
        }
        public int ShiftId { get; set;  }
        public string ShiftName { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public int NoOfBreak { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int DayId { get; set; }
        public int Period { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; } 
        public string TeacherName { get; set; }
        public string TeacherContactNo { get; set; }
        public string TeacherAddress { get; set; }
        public int Duration { get; set; }

        public string ForType { get; set; }
        public string TeacherPhotoPath { get; set; }
        
        public string SectionIdColl { get; set; }

        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }
    }

    public class ClassScheduleCollections : System.Collections.Generic.List<ClassSchedule> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
