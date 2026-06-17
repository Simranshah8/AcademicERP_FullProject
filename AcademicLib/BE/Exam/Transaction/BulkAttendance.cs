using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class BulkAttendance : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int ExamTypeId { get; set; }
        public int WorkingDays { get; set; }
        public int StudentId { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public string Remarks { get; set; }
    }
    public class BulkAttendanceCollections : List<BulkAttendance> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
