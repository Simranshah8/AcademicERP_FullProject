using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL
{

	public class PassoutStudents
	{

		DA.PassoutStudentsDB db = null;

		int _UserId = 0;

		public PassoutStudents(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.PassoutStudentsDB(hostName, dbName);
		}
		//public ResponeValues UpdatePassoutStudents(BE.PassoutStudentsCollections dataColl)
		//{
		//	ResponeValues resVal = new ResponeValues();

		//	resVal = db.UpdatePassoutStudents(_UserId, dataColl);

		//	return resVal;
		//}

		public ResponeValues UpdatePassoutStudents(BE.PassoutStudentsCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.UpdatePassoutStudents(_UserId, dataColl);

			return resVal;
		}

		public BE.StudentsForPassoutCollections GetStudentForPassout(int ClassId, int? SectionId,int? AcademicYearId, bool All = true, int? SemesterId = null, int? ClassYearId = null, int? typeId = null, int? BatchId = null,int? BranchId=null)
		{
			return db.getStudentListForPassout(_UserId, ClassId, SectionId, AcademicYearId, All, SemesterId, ClassYearId, typeId, BatchId,BranchId);
		}
	}

}

