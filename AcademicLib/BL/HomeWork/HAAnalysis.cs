using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Homework.Reporting
{
    public class HAAnalysis
    {
		DA.Homework.Reporting.HAAnalysisDB db = null;

		int _UserId = 0;

		public HAAnalysis(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Homework.Reporting.HAAnalysisDB(hostName, dbName);
		}
		public RE.Homework.Reporting.HAAnalysis GetAnalysisData(int EntityId)
		{
			return db.getAnalysisData(_UserId, EntityId);
		}
	}
}