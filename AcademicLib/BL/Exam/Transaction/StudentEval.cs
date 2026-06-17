using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class StudentEval
	{

		DA.Exam.Transaction.StudentEvalDB db = null;

		int _UserId = 0;

		public StudentEval(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.StudentEvalDB(hostName, dbName);
		}

		public BE.Exam.Transaction.StudentEvalCollections GetStudentEvaluation(int EntityId, int? AcademicYearId, int ClassId,int SectionId, int ExamTypeId, string ExamTypeIdColl = null, string StudentIdColl = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
		{
			return db.getAllStudentEval(_UserId, EntityId, AcademicYearId, ClassId, SectionId, ExamTypeId, ExamTypeIdColl, StudentIdColl, BatchId, SemesterId, ClassYearId);
		}

		
	}

}

