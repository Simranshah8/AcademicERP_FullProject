using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class ExamCenter : ResponeValues
	{

		public int? ExamCenterId { get; set; }
		public string Name { get; set; } = "";
		public string Address { get; set; } = "";
		public string Email { get; set; } = "";
		public string ContactNo { get; set; } = "";
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
	}
	public class ExamCenterCollections : System.Collections.Generic.List<ExamCenter>
	{
		public ExamCenterCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


}

