using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Attendance
{

	public class EmpAttendanceSummary
	{

		DA.Attendance.EmpAttendanceSummaryDB db = null;

		int _UserId = 0;

		public EmpAttendanceSummary(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Attendance.EmpAttendanceSummaryDB(hostName, dbName);
		}

		public RE.Attendance.EmpAttendanceSummaryCollections GetEmpAttendanceSummary(string BranchIdColl, string DepartmentIdColl, string GroupIdColl, DateTime? DateFrom, DateTime? DateTo, int? EmpType,int? ReportType)
		{
			return db.getAllEmpAttendanceSummary(_UserId, BranchIdColl, DepartmentIdColl, GroupIdColl, DateFrom, DateTo, EmpType,ReportType);
		}
	}
}

