using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{
	public class UserNameList : ResponeValues
	{

		public int? UserId { get; set; }
		public string UserName { get; set; }

	}
	public class UserNameListCollections : System.Collections.Generic.List<UserNameList>
	{
		public UserNameListCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

