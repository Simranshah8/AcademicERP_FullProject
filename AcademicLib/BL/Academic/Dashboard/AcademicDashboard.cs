using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.DashBoard
{

	public class AcademicDashBoard
	{
		DA.Academic.Dashboard.AcademicDashBoardDB db = null;
		int _UserId = 0;
		public AcademicDashBoard(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Dashboard.AcademicDashBoardDB(hostName, dbName);
		}


		public BE.Academic.Dashboard.AcademicDashBoard GetAcademicDashboard(int AcademicYearId, int? ClassShiftId, int? EntityId,int? BranchId)
		{
			return db.GetAcademicDashboard(_UserId, AcademicYearId, ClassShiftId, EntityId, BranchId);
		}

	}




}

