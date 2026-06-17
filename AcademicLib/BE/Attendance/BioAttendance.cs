using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Attendance
{

	public class BioAttendence : ResponeValues
	{

		public int? TranId { get; set; }
		public int? StudentId { get; set; }
		public int? EmployeeId { get; set; }
		public bool AttendenceMode { get; set; }
		public DateTime? InOutTime { get; set; }
		public string Remarks { get; set; } = "";
		public string selectedFor { get; set; } = "";
		public DateTime? AttendenceDate { get; set; }

	}
	public class BioAttendenceCollections : List<BioAttendence>
	{
		public BioAttendenceCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

