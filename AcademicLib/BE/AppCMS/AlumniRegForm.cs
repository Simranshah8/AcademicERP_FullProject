using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class AlumniReg : ResponeValues
	{
		public int? AlumniRegId { get; set; }
		public string FullName { get; set; } = "";
		public DateTime? DOB { get; set; }
		public string OriginalAddress { get; set; } = "";
		public string CurrentAddress { get; set; } = "";
		public string Contact { get; set; } = "";
		public string Email { get; set; } = "";
		public string JoinedYear { get; set; } = "";
		public string StudiedUpTo { get; set; } = "";
		public string SEE { get; set; } = "";
		public string PlusTwo { get; set; } = "";
		public string MarksheetPath { get; set; } ="";
		public string ProfilePhoto { get; set; } = "";
		public string MemoryPhoto1 { get; set; } = "";
		public string MemoryPhoto2 { get; set; } = "";
		public byte[] MarksheetB { get; set; } 
		public byte[] ProfileB { get; set; } 
		public byte[] Memory1B { get; set; } 
		public byte[] Memory2B { get; set; } 
		public string DegreeTitle { get; set; } = "";
		public string University { get; set; } = "";
		public string CurPosition { get; set; } = "";
		public string CurCompany { get; set; } = "";
		public string CurUniversity { get; set; } = "";
		public string Achievements { get; set; } = "";
		public string Achievement_Doc { get; set; } = "";
		public byte[] AchievementB { get; set; }
		
		public string Bio { get; set; } = "";
		public string Remarks { get; set; } = "";

		public string DOB_BS { get; set; } = "";
		public string HashData { get; set; } = "";

	}
	public class AlumniRegCollections : System.Collections.Generic.List<AlumniReg>
	{
		public AlumniRegCollections()
		{
			ResponseMSG = "";
		}

		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

