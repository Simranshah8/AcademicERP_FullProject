using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class ExamwiseAttendence : ResponeValues
    {        
        public int StudentId { get; set; }
        public int ExamTypeId { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { set; get; }
        public int AbsentDays { set; get; }
        public string Remarks { set; get; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        
    }
    public class ExamwiseAttendenceCollections : System.Collections.Generic.List<ExamwiseAttendence> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }   

    public class ImportExamWiseAttendance
    {
        public string AcademicYear { get; set; }
        public string ExamName { get; set;  }
        public string RegNo { get; set; }
        public DateTime? DateFrom_AD { get; set; }
        public DateTime? DateTo_AD { get; set; }
        public string DateFrom_BS { get; set; }
        public string DateTo_BS { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public string Remarks { get; set; }
    }
}
 