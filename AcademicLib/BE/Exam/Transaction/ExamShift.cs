using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
  public  class ExamShift : ResponeValues
    {
        public int? ExamShiftId { set; get; }
        public string Name { set; get; }
        public TimeSpan StartTime { set; get; }
        public TimeSpan EndTime { set; get; }
    }
    public class ExamShiftCollections : System.Collections.Generic.List<ExamShift> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}