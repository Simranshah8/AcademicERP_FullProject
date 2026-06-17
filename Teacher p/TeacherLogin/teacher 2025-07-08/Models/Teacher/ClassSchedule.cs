using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class ClassSchedule
    {
        public int ShiftId { get; set; }
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
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string TeacherContactNo { get; set; }
        public string TeacherAddress { get; set; }
        public string ForType { get; set; }
        //
        public int ClassShiftId { get; set; }
        public string Name { get; set; }
        public int WeeklyDayOff { get; set; }
        public int NoofBreak { get; set; }
        public int Duration { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

        public int SubjectId { get; set; }
        public string SectionIdColl { get; set; }
    }

    public class ClassScheduleId
    {
        public int classShiftId { get; set; }
        public int classId { get; set; }
    }
}