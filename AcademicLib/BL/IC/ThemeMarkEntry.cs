using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ThemeMarkEntry
    {
		DA.Exam.Transaction.ThemeMarkEntryDB db = null;
		int _UserId = 0;
		public ThemeMarkEntry(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.ThemeMarkEntryDB(hostName, dbName);
		}
		public ResponeValues SaveUpdate( List<BE.Exam.Transaction.ThemeMarkEntry> dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			resVal = db.SaveUpdate(_UserId,  dataColl);
			return resVal;
		}

		public BE.Exam.Transaction.ThemeWiseMarkEntryCollections GetThemeWiseMarkEntry(int EntityId, int? AcademicYearId, int ClassId, int? SectionId, int SubjectId, int LessonId, int AssessmentTypeId, int? CFAssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			return db.GetThemeWiseMarkEntry(_UserId, EntityId, AcademicYearId, ClassId, SectionId, SubjectId, LessonId,  AssessmentTypeId, CFAssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
		}

		public ResponeValues DelThemeMarkEntry(int EntityId, int ClassId, int? SectionId, int SubjectId, int LessonId, int? AssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			return db.DelThemeMarkEntry(_UserId, EntityId, ClassId, SectionId, SubjectId, LessonId, AssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
		}


	}
}
