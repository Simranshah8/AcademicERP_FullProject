using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Attendance
{
	public class PendingAttendance : ResponeValues
	{
		public int? TranId { get; set; }
		public DateTime ForDate { get; set; }
		public string ForMiti { get; set; } = "";
		public string ClassName { get; set; } = "";
		public string Section { get; set; } = "";
		public string Batch { get; set; } = "";
		public string Semester { get; set; } = "";
		public string ClassYear { get; set; } = "";
		public string ClassTeacher { get; set; } = "";
		public string Coordinator { get; set; } = "";
		public string HOD { get; set; } = "";
		
	}

	public class PendingAttendanceCollections : System.Collections.Generic.List<PendingAttendance>
	{
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}