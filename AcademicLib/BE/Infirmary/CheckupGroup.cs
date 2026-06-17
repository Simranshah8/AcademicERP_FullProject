using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class CheckupGroup : ResponeValues 
	{ 

		public int? CheckupGroupId { get; set; } 
		public string Name { get; set; } ="" ; 
		public string Description { get; set; } ="" ; 
		public bool ShowinStudentInfirmary { get; set; } 
		public int StudentInfirmaryOrderNo { get; set; } 
		public bool ShowinEmployeeInfirmary { get; set; } 
		public int EmployeeInfirmaryOrderNo { get; set; } 
		}
	public class CheckupGroupCollections : System.Collections.Generic.List<CheckupGroup>
	{
		public CheckupGroupCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

