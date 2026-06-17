using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Security
{
	public class LastLoginLog
	{
		DA.Security.LastLoginLogDB db = null;

		int _UserId = 0;

		public LastLoginLog(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Security.LastLoginLogDB(hostName, dbName);
		}
	
		public RE.Security.LastLoginLogCollections GetAllLastLoginLog()
		{
			return db.getAllLastLoginLog(_UserId);
		}
		public RE.Security.LastLoginLogCollections getAllNeverLogin()
		{
			return db.getAllNeverLogin(_UserId);
		}

	}

}

