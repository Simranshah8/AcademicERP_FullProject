using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Report
{

	public class StudentDetails
	{
		DA.Report.StudentDetailsDB db = null;
		int _UserId = 0;
		public StudentDetails(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Report.StudentDetailsDB(hostName, dbName);
		}
		public AcademicLib.RE.Report.StudentDetails PrintStudentInfo(int EntityId, int StudentId)
		{
			return db.PrintStudentInfo(_UserId, EntityId, StudentId);
		}

	}

}

