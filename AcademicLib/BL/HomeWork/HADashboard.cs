using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Homework.Reporting
{
    public class HADashboard
    {
		DA.Homework.Reporting.HADashboard db = null;

		int _UserId = 0;

		public HADashboard(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Homework.Reporting.HADashboard(hostName, dbName);
		}
		public RE.Homework.Reporting.HADashboard GetDashboardData(int EntityId)
		{
			return db.getDashboardData(_UserId, EntityId);
		}
	}
}