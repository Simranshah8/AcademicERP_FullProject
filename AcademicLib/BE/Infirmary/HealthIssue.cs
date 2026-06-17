using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class HealthIssue : ResponeValues 
	{ 

		public int? HealthIssueId { get; set; } 
		public string Name { get; set; } ="" ; 
		public int? Severity { get; set; }  
		public int OrderNo { get; set; } 
		public string Description { get; set; } ="" ; 
		public string SeverityTypeName { get; set; } ="" ; 
		}
	public class HealthIssueCollections : System.Collections.Generic.List<HealthIssue>
	{
		public HealthIssueCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

