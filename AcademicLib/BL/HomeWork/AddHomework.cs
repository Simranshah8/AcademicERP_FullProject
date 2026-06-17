using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.HomeWork
{

	public class AddHomework
	{

		DA.HomeWork.AddHomeworkDB db = null;

		int _UserId = 0;

		public AddHomework(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.HomeWork.AddHomeworkDB(hostName, dbName);
		}
		public BE.HomeWork.TeacherNameCollections GetTeacherName(int EntityId)
		{
			return db.GetTeacherName(_UserId, EntityId);
		}

		public BE.HomeWork.TeacherWiseClassCollections GetTeacherWiseClass(int EntityId, int EmployeeId, int? AcademicYearId)
		{
			return db.GetTeacherWiseClass(_UserId, EntityId, EmployeeId, AcademicYearId);
		}
	}

}

