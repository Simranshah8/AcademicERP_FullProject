using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Infrastructure
{

	public class LandDetails : ResponeValues
	{

		public int? LandDetailsId { get; set; }
		public string TotalArea { get; set; } = "";
		public int? OwnerShipId { get; set; }
		public string OtherOwnerShip { get; set; } = "";
		public int? UtilizationId { get; set; }
		public string OtherUtilizationType { get; set; } = "";
		public string Attachment { get; set; } = "";
		public byte[] Photo { get; set; }
		public string LandRemarks { get; set; } = "";
		public string OwnerShip { get; set; } = "";
		public string Utilization { get; set; } = "";

		public string unit { get; set; } = "";
		public string sheetNo { get; set; } = "";
		public string kittaNo { get; set; } = "";
	}

	public class LandDetailsCollections : System.Collections.Generic.List<LandDetails>
	{
		public LandDetailsCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

