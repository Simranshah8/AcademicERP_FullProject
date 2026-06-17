using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.DashBoard
{

	public class DashboardAnalysisReport
	{
		DA.Academic.Dashboard.DashboardAnalysisReportDB db = null;
		int _UserId = 0;
		public DashboardAnalysisReport(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Dashboard.DashboardAnalysisReportDB(hostName, dbName);
		}


		public BE.Academic.Dashboard.DashboardAnalysisReport GetAcademicAnalysisReport(int AcademicYearId,  int? EntityId, int? BranchId)
		{
			return db.GetAcademicAnalysisReport(_UserId, AcademicYearId, EntityId, BranchId);
		}

	}




}

