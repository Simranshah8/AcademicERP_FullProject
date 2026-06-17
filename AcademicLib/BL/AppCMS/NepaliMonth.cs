using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class NepaliMonth
	{

		DA.AppCMS.Creation.NepaliMonthDB db = null;

		int _UserId = 0;

		public NepaliMonth(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.NepaliMonthDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.AppCMS.Creation.NepaliMonthCollections beData)
		{
			return db.SaveUpdate(_UserId, beData);
		}
		public AcademicLib.BE.AppCMS.Creation.NepaliMonthCollections GetAllMonth(int EntityId)
		{
			return db.getAllMonthName(_UserId, EntityId);
		}		 
		public ResponeValues DeleteById(int EntityId, int NM)
		{
			return db.DeleteById(_UserId, EntityId, NM);
		}
	 
	}

}

