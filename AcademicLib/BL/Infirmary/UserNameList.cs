using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class UserNameList
	{

		DA.UserNameListDB db = null;

		int _UserId = 0;

		public UserNameList(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.UserNameListDB(hostName, dbName);
		}

		public BE.UserNameListCollections GetAllUserNameList(int EntityId)
		{
			return db.getAllUserNameList(_UserId, EntityId);
		}

		internal object getAllUserNameList(int v)
		{
			throw new NotImplementedException();
		}
	}

}