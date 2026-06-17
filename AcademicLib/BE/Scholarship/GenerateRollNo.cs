using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class GenerateRollNo : ResponeValues
	{

		public int? GenerateId { get; set; }
		public int SubjectId { get; set; }
		public int? StartNo { get; set; }
		public int? PadWidth { get; set; }
		public string Prefix { get; set; } = "";
		public string Suffix { get; set; } = "";
		public DateTime? LogDateTime { get; set; }
		public string LogMiti { get; set; } = "";
		public string CreateBy { get; set; } = "";
		public string SubjectName { get; set; } = "";
	}
	public class GenerateRollNoCollections : System.Collections.Generic.List<GenerateRollNo>
	{
		public GenerateRollNoCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

