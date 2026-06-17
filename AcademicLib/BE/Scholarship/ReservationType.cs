using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class ReservationType : ResponeValues
	{

		public int? TranId { get; set; }
		public string Name { get; set; } = "";
		public int ReservationGrpId { get; set; }
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
		public string ReservationGroupName { get; set; } = "";
	}
	public class ReservationTypeCollections : System.Collections.Generic.List<ReservationType>
	{
		public ReservationTypeCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

