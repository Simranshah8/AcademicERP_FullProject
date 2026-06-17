using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class ExamCenterMapping : ResponeValues
	{

		public int? TranId { get; set; }
		public string FromRollNo { get; set; } = "";
		public string ToRollNo { get; set; } = "";
		public int ExamCenterId { get; set; }
		public string ExamCenterName { get; set; } = "";
		public DateTime? LogDateTime { get; set; }
		public string LogMiti { get; set; } = "";
		public string CreateBy { get; set; } = "";
	}
	public class ExamCenterMappingCollections : System.Collections.Generic.List<ExamCenterMapping>
	{
		public ExamCenterMappingCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

