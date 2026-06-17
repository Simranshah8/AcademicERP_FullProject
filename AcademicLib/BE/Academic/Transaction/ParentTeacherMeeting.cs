using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Transaction
{

	public class ParentTeacherMeeting : ResponeValues
	{
		public int? TranId { get; set; }
		public int? ClassId { get; set; }
		public int? SectionId { get; set; }
		public string ClassSection { get; set; } = "";
		public string PTMByName { get; set; } = "";
		public DateTime? PTMDate { get; set; }
		public int? PTMBy { get; set; }
		public string Description { get; set; } = "";
		public string PTMDateBS { get; set; } = "";
		public int? AcademicYearId { get; set; }
		public ParentTeacherMeeting()
		{
			StudentPTMColl = new StudentPTMCollections();
		}
		public StudentPTMCollections StudentPTMColl { get; set; }
	}
	public class StudentPTM
	{

		public int? StudentId { get; set; }
		public string StudentName { get; set; } = "";
		public string AdmNo { get; set; } = "";
		public int? RollNo { get; set; }
		public string PTMAttendBy { get; set; } = "";
		public string TeacherRemarks { get; set; } = "";
		public string ParentRemarks { get; set; } = "";
		public string Recommendation { get; set; } = "";	
		public int? TranId { get; set; }
		public string SectionName { get; set; } = "";
	}

	public class StudentPTMCollections : System.Collections.Generic.List<StudentPTM>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}
	public class ParentTeacherMeetingCollections : System.Collections.Generic.List<ParentTeacherMeeting>
	{
		public ParentTeacherMeetingCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}



