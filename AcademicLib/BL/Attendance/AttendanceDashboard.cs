using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Attendance.Reporting
{
    public class AttendanceDashboard
    {
		DA.Attendance.Reporting.AttendanceDashboardDB db = null;
		int _UserId = 0;
		public AttendanceDashboard(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Attendance.Reporting.AttendanceDashboardDB(hostName, dbName);
		}
		public AcademicLib.RE.Attendance.Reporting.AttendanceDashboard getallAttendance(int UserId)
		{
			return db.getDashboard(_UserId);
		}
	}
}