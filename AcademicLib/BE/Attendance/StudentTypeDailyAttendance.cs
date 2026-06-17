using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Attendance
{

	public class StudentTypeDailyAttendance : ResponeValues 
	{ 

		public int? TranId { get; set; } 
		public DateTime ForDate { get; set; } 
		public int StudentTypeId { get; set; } 
		public int StudentId { get; set; } 
		public int Attendance { get; set; } 
		public string Remarks { get; set; } ="" ; 
		public double? Lat { get; set; } 
		public double? Lng { get; set; } 
		public string LiveLocation { get; set; } ="" ;
		public int LateMin { get; set; }
		public string RegdNo { get; set; }
		public int RollNo { get; set; }
		public string Name { get; set; }
		public string PhotoPath { get; set; }
		public string ClassName { get; set; }
		public string SectionName { get; set; }
		public string Batch { get; set; }
		public string Factulty { get; set; }
		public string Level { get; set; }
		public string Semester { get; set; }
		public string ClassYear { get; set; }

		public int? BatchId { get; set; }
		public int? SemesterId { get; set; }
		public int? ClassYearId { get; set; }

		public int? PeriodId { get; set; }
		public int? InOutMode { get; set; }
		public string StudentType { get; set; }
	}

	public class StudentTypeDailyAttendanceCollections : System.Collections.Generic.List<StudentTypeDailyAttendance>
    {
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

