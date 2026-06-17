using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class CopyExamType : ResponeValues 
	{ 

		public int? CopyExamTypeId { get; set; } 
		public int? FromExamTermId { get; set; } 
		public int? ToExamTermId { get; set; }
		public string FromExamName { get; set; } = "";
		public string ToExamName { get; set; } = "";
		public string CopyBy { get; set; } = "";
		public DateTime? LogDateTime { get; set; } 
	}
	public class CopyExamTypeCollections : System.Collections.Generic.List<CopyExamType>
	{
		public CopyExamTypeCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}
