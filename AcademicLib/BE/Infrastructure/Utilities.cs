using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Infrastructure
{

	public class Utilities : ResponeValues
	{

		public int? UtilitiesId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
		public bool IsActive { get; set; }
		public Utilities()
		{
			SubUtilitiesColl = new SubUtilitiesCollections();
		}
		public SubUtilitiesCollections SubUtilitiesColl { get; set; }
	}
	public class SubUtilities
	{

		public int UtilitiesId { get; set; }
		public int SNo { get; set; }
		public string Name { get; set; } = "";
	}

	public class SubUtilitiesCollections : System.Collections.Generic.List<SubUtilities>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}


	public class UtilitiesCollections : System.Collections.Generic.List<Utilities>
	{
		public UtilitiesCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


}



