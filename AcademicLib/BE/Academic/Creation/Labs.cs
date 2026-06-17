using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Creation
{

	public class Labs : ResponeValues
	{

		public int? LabsId { get; set; }
		public string LabName { get; set; } = "";
		public int? BuildingId { get; set; }
		public string BuildingName { get; set; } = "";
		public double? AreaCoveredByLab { get; set; }
		public string LabType { get; set; } = "";
		public bool AdequencyOfLabEquipment { get; set; }
		public bool HasInternetConnection { get; set; }
		public string EquipmentAtLab { get; set; } = "";
		public string Remarks { get; set; } = "";
	}
	public class LabsCollections : System.Collections.Generic.List<Labs>
	{
		public LabsCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }

	}

}

