using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Academic.Transaction
{
    public class PhotoStatus
    {
		DA.Academic.Transaction.PhotoStatusDB db = null;

		int _UserId = 0;

		public PhotoStatus(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Transaction.PhotoStatusDB(hostName, dbName);
		}
		public BE.Academic.Transaction.PhotoStatus GetAllPhotoStatus(int EntityId, int AcademicYearId, int BranchId)
		{
			return db.getAllPhotoStatus(_UserId, EntityId,AcademicYearId,BranchId);
		}
	}
}