using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Transaction
{

	public class LessonPlanning : ResponeValues
	{

		public int? TranId { get; set; }
		public int? AcademicYearId { get; set; }
		public int? BranchId { get; set; }
		public int? ClassId { get; set; }
		public string SectionIdColl { get; set; } = "";
		public int? BatchId { get; set; }
		public int? SemesterId { get; set; }
		public int? ClassYearId { get; set; }
		public int? SubjectId { get; set; }
		public int? EmployeeId { get; set; }
		public int? LessonSNo { get; set; }
		public int? TopicSNo { get; set; }
		public DateTime? PlanStartDate { get; set; }
		public DateTime? PlanEndDate { get; set; }
		public string LessonName { get; set; } = "";
		public string TopicName { get; set; } = "";
		public LessonPlanning()
		{
			DetailsColl = new LessonPlanningContentCollections();
		}
		public LessonPlanningContentCollections DetailsColl { get; set; }
	}
	public class LessonPlanningContent
	{

		public int TranId { get; set; }
		public DateTime? ForDate { get; set; }
		public string Content { get; set; } = "";
	}

	public class LessonPlanningContentCollections : System.Collections.Generic.List<LessonPlanningContent>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class LessonPlanningCollections : System.Collections.Generic.List<LessonPlanning>
	{
		public LessonPlanningCollections()
		{
			ResponseMSG = "";
			DetailsColl = new LessonPlanningContentCollections();
		}
		public LessonPlanningContentCollections DetailsColl { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}
