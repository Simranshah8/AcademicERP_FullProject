using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Payroll
{

	public class EmployeeGroup : ResponeValues
	{

		public int? EmployeeGroupId { get; set; }
		public string Name { get; set; } = "";
		public string Alias { get; set; } = "";
		public int BaseGroupId { get; set; }
		public string Description { get; set; } = "";
		public int? LedgerId { get; set; }
		public int? BranchId { get; set; }

		public int? id
		{
			get
			{
				return this.EmployeeGroupId;
			}
		}
		public string text
		{
			get
			{
				return this.Name;
			}
		}
	}
	public class EmployeeGroupCollections : System.Collections.Generic.List<EmployeeGroup>
	{
		public EmployeeGroupCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

