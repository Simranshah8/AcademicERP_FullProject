using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class GrievanceRedressalTeam : ResponeValues
	{

		public int? GrievanceRedressalId { get; set; }
		public string Name { get; set; } = "";
		public string Designation { get; set; } = "";
		public string Qualification { get; set; } = "";
		public string Contact { get; set; } = "";
		public string Email { get; set; } = "";
		public string Department { get; set; } = "";
		public int? OrderNo { get; set; }
		public string Image { get; set; } = "";
		public byte[] PhotoB { get; set; }
	}
	public class GrievanceRedressalTeamCollections : System.Collections.Generic.List<GrievanceRedressalTeam>
	{
		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}
}

