using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Exam.Transaction
{

	public class Indicator : ResponeValues
	{

		public int? TranId { get; set; }
		public int? BranchId { get; set; }
		public int? ClassId { get; set; }
		public int? SubjectId { get; set; }
		public int? LessonId { get; set; }
		public int? TopicId { get; set; }
		public string TopicName { get; set; } = "";
		public string IndicatorName { get; set; } = "";
		public string LessonName { get; set; } = "";
		public Indicator()
		{
			IndicatorDetailsColl = new IndicatorDetailsCollections();
		}
		public IndicatorDetailsCollections IndicatorDetailsColl { get; set; }
	}
	public class IndicatorDetails
	{

		public int SNo { get; set; }
		public int TranId { get; set; }
		public string IndicatorName { get; set; } = "";
	}

	public class IndicatorDetailsCollections : System.Collections.Generic.List<IndicatorDetails>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class IndicatorCollections : System.Collections.Generic.List<Indicator>
	{
		public IndicatorCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


	

	public class SubjectLessonWise : ResponeValues
	{

		public int? LessonId { get; set; }
		public string LessonName { get; set; } = "";
		public int? LessonSno { get; set; }

	}


	public class SubjectLessonWiseCollections : System.Collections.Generic.List<SubjectLessonWise>
	{
		public SubjectLessonWiseCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}



	public class LessonTopicDetailsWise : ResponeValues
	{

		public int? LessonId { get; set; }
		public string TopicName { get; set; } = "";
		public int? TopicId { get; set; }
	}


	public class LessonTopicDetailsWiseCollections : System.Collections.Generic.List<LessonTopicDetailsWise>
	{
		public LessonTopicDetailsWiseCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


	public class TopicWiseIndicator : ResponeValues
	{
		public int? IndicatorId { get; set; }
		public int? LessonId { get; set; }
		public string IndicatorName { get; set; } = "";
		public int? TopicId { get; set; }

		//Added By Suresh
		public int? TranId { get; set; }
	}


	public class TopicWiseIndicatorCollections : System.Collections.Generic.List<TopicWiseIndicator>
	{
		public TopicWiseIndicatorCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	//Added For Summary on shrawan 11 2084 by suresh below
	public class IndicatorSummary : ResponeValues
	{

		public int? TotalLessonIds { get; set; }
		public int? TotalTopicNames { get; set; }
		public int? TotalIndicatorNames { get; set; }
	}


	public class IndicatorSummaryCollections : System.Collections.Generic.List<IndicatorSummary>
	{
		public IndicatorSummaryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}



}



