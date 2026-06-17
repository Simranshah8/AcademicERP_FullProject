using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
  public  class ExamAttendance : ResponeValues
    {
        public int ExamAttendanceId { get; set; }
        public DateTime Date { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int PaperTypeId { get; set; }
        public int ExamTypeId { get; set; }
        private List<ExamAttendanceDetails>  _ExamAttendanceDetailsCollections = new List<ExamAttendanceDetails>();
        public List<ExamAttendanceDetails> ExamAttendanceDetailsColl
        {
            get
            {
                return _ExamAttendanceDetailsCollections;
            }
            set
            {
                _ExamAttendanceDetailsCollections = value;
            }
        }
    }
    public class ExamAttendanceCollections : System.Collections.Generic.List<ExamAttendance> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ExamAttendanceDetails
    {
        public int TranId { set; get; }
        public int ExamAttendanceId { set; get; }
        public int StudentId { set; get; }
        public string StudentName { set; get; }
        public int SubjectId { set; get; }
        public bool IsPresect { set; get; }
        public bool IsAbsent { set; get; }
        public string Remarks { set; get; }

    }
    public class ExamAttendanceDetailsCollections : System.Collections.Generic.List<ExamAttendanceDetails> { }
}
