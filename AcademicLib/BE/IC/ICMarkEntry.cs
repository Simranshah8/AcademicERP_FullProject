using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Exam.Transaction
{

	public class ICMarkEntry : ResponeValues
	{
		public int? TranId { get; set; }
		public int? BatchId { get; set; }
		public int? SemesterId { get; set; }
		public int? ClassYearId { get; set; }
		public int? AcademicYearId { get; set; }
		public int? BranchId { get; set; }
		public int? ClassId { get; set; }
		public int? SectionId { get; set; }
		public int? SubjectId { get; set; }
		public int? LessonId { get; set; }
		public string TopicName { get; set; } = "";
		public int? StudentId { get; set; }
		public int? IndicatorSNo { get; set; }
		public bool Evaluation { get; set; }
		public int? Marks { get; set; }
		public string Remarks { get; set; } = "";
		public int? EvaluationAreaId { get; set; }
		public int? IndicatorId { get; set; }
		public int? AssessmentTypeId { get; set; }
		public DateTime AssessmentDate { get; set; }
	}
	public class ICMarkEntryCollections : System.Collections.Generic.List<ICMarkEntry>
	{
		public ICMarkEntryCollections()
		{
			ResponseMSG = "";

		}
		public int? CUserId { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}



	public class ICStudentsDetail: ResponeValues
	{

		public int? TranId { get; set; }
		public int? ClassId { get; set; }
		public int? SectionId { get; set; }
		public string SectionName { get; set; } = "";
		public string RegdNo { get; set; } = "";
		public int? RollNumber { get; set; }
		public string StudentName { get; set; } = "";
		public bool Evaluation { get; set; }
		public int? Marks { get; set; }
		public string Remarks { get; set; } = "";
		public int? AcademicYearId { get; set; }
		public string IndicatorName { get; set; } = "";
		public int? StudentId { get; set; }
		public int? SNo { get; set; }

		//Added on Baishakh 6 2082
		public int? EvaluationAreaId { get; set; }
		public int? IndicatorId { get; set; }
		public int? AssessmentTypeId { get; set; }
		public DateTime AssessmentDate { get; set; }
	}

	public class ICStudentsDetailCollections : System.Collections.Generic.List<ICStudentsDetail>
	{
		public ICStudentsDetailCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


	public class TopicForStudentWiseIC : ResponeValues
	{
		public int? TranId { get; set; }
		public string TopicName { get; set; } = "";
		public string IndicatorName { get; set; } = "";
		public int? IndicatorId { get; set; }
		public int? Marks { get; set; }
		public string Remarks { get; set; } = "";
		public bool Evaluation { get; set; }
		public int? SNo { get; set; }
		//Added on Baishakh 6 2082
		public int? EvaluationAreaId { get; set; }
	}

	public class TopicForStudentWiseICCollections : System.Collections.Generic.List<TopicForStudentWiseIC>
	{
		public TopicForStudentWiseICCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


	public class ICMArkEntryStatus : ResponeValues
	{
		
		public string TopicName { get; set; } = "";
		public bool IsPending { get; set; }
		public DateTime SubmitDateTime_AD { get; set; }
		public string SubmitDateTime_BS { get; set; }	
		public string UserName { get; set; } = "";
		public string ClassTeacher { get; set; } = "";
		public string SubjectTeacher { get; set; } = "";
		public string TeacherContactNo { get; set; } = "";
		public int UserId { get; set; }

	}

	public class ICMArkEntryStatusCCollections : System.Collections.Generic.List<ICMArkEntryStatus>
	{
		public ICMArkEntryStatusCCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}



