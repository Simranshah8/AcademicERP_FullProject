using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class CreateLessonPlan : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public string LessonWithTopic { get; set; }
        public DateTime DateRange { get; set; }
        public int DaysCount { get; set; }
        public int Status { get; set; }
        public string CompletedBy { get; set; }
        
    }
    public class CreateLessonPlanCollections : List<CreateLessonPlan> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
