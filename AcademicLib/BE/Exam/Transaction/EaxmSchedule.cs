using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public  class ExamSchedule :  ResponeValues
    {
        public int? ExamScheduleId { get; set; }
        public int ClassId { get; set; }
        public int[] SectionIdColl { get; set; }
        public int ExamTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }

        private List<ExamScheduleDetails>  _ExamScheduleDetails = new List<ExamScheduleDetails> ();
        public List<ExamScheduleDetails>  ExamScheduleDetailsColl
        {
            get
            {
                return _ExamScheduleDetails;
            }
            set
            {
                _ExamScheduleDetails = value;
            }
        }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }

        public string FromSectionIdColl { get; set; }
        public string ToSectionIdColl { get; set; }
        public string ToClassIdColl { get; set; }
    }
    public class ExamScheduleCollections : List<ExamSchedule> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ExamScheduleDetails
    {        
        public int ExamScheduleId { set; get; }
        public int? SubjectId { set; get; }
        public int PaperTypeId { set; get; }
        public DateTime? ExamDate { set; get; }
        public DateTime? StartTime { set; get; }
        public DateTime? EndTime { set; get; }
        public string Remarks { set; get; }
        public int? ExamShiftId { get; set; }

    }
    
}