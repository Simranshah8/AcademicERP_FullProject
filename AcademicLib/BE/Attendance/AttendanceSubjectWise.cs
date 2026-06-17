using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Attendance
{
   public class AttendanceSubjectWise: AttendanceStudentWise
    {
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
    }
    public class AttendanceSubjectWiseCollections:List<AttendanceSubjectWise> 
    {
        public AttendanceSubjectWiseCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}