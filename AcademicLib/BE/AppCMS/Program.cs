using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class Program : ResponeValues
	{

		public int? ProgramId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int? OrderNo { get; set; }
	}
	public class ProgramCollections : System.Collections.Generic.List<Program>
	{
		public ProgramCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

