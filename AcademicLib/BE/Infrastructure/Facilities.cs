using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Infrastructure
{

	public class Facilities : ResponeValues
	{

		public int? FacilitiesId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
		public bool IsActive { get; set; }
		public Facilities()
		{
			SubFacilitiesColl = new SubFacilitiesCollections();
		}
		public SubFacilitiesCollections SubFacilitiesColl { get; set; }
	}
	public class SubFacilities
	{

		public int FacilitiesId { get; set; }
		public int SNo { get; set; }
		public string Name { get; set; } = "";
	}

	public class SubFacilitiesCollections : System.Collections.Generic.List<SubFacilities>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}
	public class FacilitiesCollections : System.Collections.Generic.List<Facilities>
	{
		public FacilitiesCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}



