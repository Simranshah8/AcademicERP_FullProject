using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ObjectiveMarkEntry
    {
		DA.Exam.Transaction.ObjectiveMarkEntryDB db = null;

		int _UserId = 0;

		public ObjectiveMarkEntry(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.ObjectiveMarkEntryDB(hostName, dbName);
		}

		public BE.Exam.Transaction.StudentCollection GetStudentList(int EntityId, int ClassId, int? SectionId)
		{
			return db.GetStudentList(EntityId, _UserId, ClassId, SectionId);
		}
		public BE.Exam.Transaction.ObjectiveDetailsCollections GetObjectiveList(int EntityId, int ClassId, int? SectionId, int SubjectId, int ExamTypeId)
		{
			return db.GetObjectiveList(EntityId, _UserId, ClassId, SectionId, SubjectId, ExamTypeId);
		}
	}
}