using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Creation
{

	public class Coordinator
	{

		DA.Academic.Creation.CoordinatorDB db = null;

		int _UserId = 0;

		public Coordinator(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Creation.CoordinatorDB(hostName, dbName);
		}
		
		public ResponeValues SaveFormData(BE.Academic.Creation.CoordinatorCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.SaveUpdate(_UserId, dataColl);

			return resVal;
		}

		public BE.Academic.Creation.CoordinatorCollections GetCoordinatorClasswise (int EmployeeId)
		{
			return db.getAllCoordinatorClass(_UserId, EmployeeId);
		}

		public BE.Academic.Creation.CoordinatorCollections GetAllCoordinator()
		{
			return db.getAllCoordinator(_UserId);
		}

		public ResponeValues DeleteById(int EmployeeId)
		{
			return db.DeleteById(_UserId, EmployeeId);
		}

	}

}

