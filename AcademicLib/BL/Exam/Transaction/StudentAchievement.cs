using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class StudentAchievement
	{

		AcademicLib.DA.Exam.Transaction.StudentAchievementDB db = null;

		int _UserId = 0;

		public StudentAchievement(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Exam.Transaction.StudentAchievementDB(hostName, dbName);
		}

		public AcademicLib.BE.Exam.Transaction.PrevAchievementCollections GetPreviousAchievement(int EntityId, int StudentId, int ExamTypeId/*, int RemarksTypeId*/)
		{
			return db.getPreviousAchievement(_UserId, EntityId, StudentId, ExamTypeId/*, RemarksTypeId*/);
		}

		public ResponeValues SaveFormData(AcademicLib.BE.Exam.Transaction.StudentAchievementCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.SaveUpdate(_UserId, dataColl);

			return resVal;
		}

		public AcademicLib.BE.Exam.Transaction.StudentListForAchievementCollections GetStudentForAchievement(int ClassId, int? SectionId, int? AcademicYearId)
		{
			return db.getStudentListForAchievement(_UserId, ClassId, SectionId, AcademicYearId);
		}
		public ResponeValues DeleteAchievementById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
	}

}

