using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Academic.Creation
{
    public class TermSyllabus : ResponeValues
    {

		public int? TranId { get; set; }
		public int BranchId { get; set; }
		public int ExamTypeId { get; set; }
		public int ClassId { get; set; }
		public int SubjectId { get; set; }
		public int? LessonId { get; set; }
		public string TopicId { get; set; }
		public string Topics { get; set; } = "";
		public string TopicNames { get; set; } = "";
		public string Content { get; set; } = "";
		public int? TeachingPeriod { get; set; }
		public string TeachingMethods { get; set; } = "";
		public string TeachingMaterials { get; set; } = "";
		public string Evaluation { get; set; } = "";
		public string Remarks { get; set; } = "";
		public int? BatchId { get; set; }
		public int? SemesterId { get; set; }
		public int? ClassYearId { get; set; }
		public string ExamName { get; set; } = "";
		public string ClassName { get; set; } = "";
		public string SubjectName { get; set; } = "";
		public int? LessonCount { get; set; }
		public int? TopicCount { get; set; }
		public string LessonName { get; set; } = "";
		public string Semester { get; set; } = "";
		public string ClassYear { get; set; } = "";
	}
    public class TermSyllabusColl : List<TermSyllabus>
    {
        public TermSyllabusColl()
        {
            ResponseMSG = " ";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }



}