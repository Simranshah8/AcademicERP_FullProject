using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Attendance
{
   public class AttendanceStudentWise : ResponeValues
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int TranId { get; set; }
        public DateTime ForDate { get; set; }        
        public INOUTMODES InOutMode { get; set; }
        public int StudentId { get; set; }
        public ATTENDANCES? Attendance { get; set; }
        public int LateMin { get; set; }
        public string Remarks { get; set; }

        public bool Notify { get; set; }

        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Batch { get; set; }
        public string Factulty { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }

        public int? PeriodId { get; set; }

    }
    public class AttendanceStudentWiseCollections :List<AttendanceStudentWise> 
    {
        public AttendanceStudentWiseCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public enum INOUTMODES
    {
        IN=1,
        OUT=2,
        BOTH=3,
        LEAVE=4,
        LATE=5
    }

    public enum ATTENDANCES
    {
        PRESENT=1,
        ABSENT=2,
        LATE=3,
        LEAVE=4
    }
}
 