using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class StudentList
	{

		DA.Scholarship.StudentListDB db = null;

		int _UserId = 0;

		public StudentList(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Scholarship.StudentListDB(hostName, dbName);
		}
		
		public BE.Scholarship.StudentListCollections GetAllStudentList(int EntityId, int? SubjectId)
		{
			return db.getAllStudentList(_UserId, EntityId, SubjectId);
		}
		
	}

}

