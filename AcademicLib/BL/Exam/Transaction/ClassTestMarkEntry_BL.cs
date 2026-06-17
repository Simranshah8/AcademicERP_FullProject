using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class ClassTestMarkEntry
	{

		DA.Exam.Transaction.ClassTestMarkEntryDB db = null;

		int _UserId = 0;

		public ClassTestMarkEntry(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.ClassTestMarkEntryDB(hostName, dbName);
		}
		

		public ResponeValues SaveUpdate(AcademicLib.BE.Exam.Transaction.ClassTestMarkEntryCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.SaveUpdate(_UserId, dataColl);

			return resVal;
		}



        public BE.Exam.Transaction.CSubjectWiseLessonCollections GetCSubjectWiseLesson(int EntityId, int? SubjectId)
        {
            return db.getCSubjectWiseLesson(_UserId, EntityId, SubjectId);
        }

        public BE.Exam.Transaction.StudentsDetailsClassWiseCollections GetStudentsDetailsClassWise(int EntityId, int AcademicYearId, int ClassId, int? SectionId, int SubjectId, bool FilterSection, int? LessonSNo, DateTime TestDate, int EmployeeId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getStudentsDetailsClassWise(_UserId, EntityId, AcademicYearId, ClassId, SectionId, SubjectId, FilterSection, LessonSNo, TestDate, EmployeeId, BatchId, SemesterId, ClassYearId);
        }

    }

}

