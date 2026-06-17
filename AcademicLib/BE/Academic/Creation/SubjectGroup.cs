using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Creation
{

	public class SubjectGroup : ResponeValues 
	{ 

		public int? SubjectGroupId { get; set; } 
		public string Name { get; set; } ="" ; 
		public string Code { get; set; } ="" ; 
		public int? BranchId { get; set; } 
	}

	public class SubjectGroupCollections: System.Collections.Generic.List<SubjectGroup>
    {
		public bool IsSuccess { get; set; }
		public string ResponseMSG { get; set; }
	}

}

