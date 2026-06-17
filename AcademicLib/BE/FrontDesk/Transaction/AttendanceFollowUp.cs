using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.FrontDesk.Transaction
{

	public class AttendanceFollowUp : ResponeValues
	{

		public int? TranId { get; set; }
		public int? StudentId { get; set; }
		public DateTime? FollowUpDate { get; set; }
		public string FollowupMiti { get; set; } = "";
		public int FollowUpTo { get; set; }
		public string ContactNo { get; set; } = "";
		public int? FollowUpStatus { get; set; }
		public string FollowUpRemarks { get; set; } = "";
		public string FollowUpBy { get; set; } = "";
		public string StudentName { get; set; } = "";
		public int RollNo { get; set; }
		public string RegNo { get; set; }
		public string ClassName { get; set; } = "";
		public string SectionName { get; set; } = "";
		public string Batch { get; set; } = "";
		public string ClassYear { get; set; } = "";
		public string Semester { get; set; } = "";
	}

	public class AttendanceFollowUpColl : List<AttendanceFollowUp>
	{
		public AttendanceFollowUpColl()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }


	}
}

