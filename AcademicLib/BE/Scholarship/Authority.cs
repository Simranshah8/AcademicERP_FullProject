using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class Authority : ResponeValues
	{

		public int? AuthorityId { get; set; }
		public string Name { get; set; } = "";
		public string Email { get; set; } = "";
		public string ContactNo { get; set; } = "";
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
	}
	public class AuthorityCollections : System.Collections.Generic.List<Authority>
	{
		public AuthorityCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

