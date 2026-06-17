using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Exam.Reporting
{
    public class ExamDashboard
    {
		DA.Exam.Reporting.ExamDashboardDB db = null;

		int _UserId = 0;

		public ExamDashboard(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Reporting.ExamDashboardDB(hostName, dbName);
		}
		public RE.Exam.Reporting.ExamDashboard GetExamDashboardData(int EntityId)
		{
			return db.GetExamDashboardData(_UserId, EntityId);
		}
	}
}