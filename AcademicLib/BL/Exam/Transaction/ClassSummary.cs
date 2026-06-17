using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class ClassSummary
	{

		DA.Exam.Transaction.ClassSummaryDB db = null;

		int _UserId = 0;

		public ClassSummary(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.ClassSummaryDB(hostName, dbName);
		}


        public BE.Exam.Transaction.ClassSummarySubjectClassWiseCollections GetClassSummarySubjectClassWise(int EntityId, int? ClassId)
        {
            return db.getClassSummarySubjectClassWise(_UserId, EntityId, ClassId);
        }

        public BE.Exam.Transaction.ClassSummaryCollections GetViewClassSummaryDetails(int EntityId, int? ClassId, int? SectionId, int? SubjectId, DateTime DateFrom, DateTime DateTo, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            return db.getViewClassSummaryDetails(_UserId, EntityId, ClassId, SectionId, SubjectId, DateFrom, DateTo, BatchId, ClassYearId, SemesterId);
        }
        public BE.Exam.Transaction.ClassSummaryDetailsByIdCollections GetClassSummaryDetailsById(int EntityId, int AcademicYearId, int ClassId, int EmployeeId, int SubjectId, int? LessonSno, DateTime TestDate, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            return db.getClassSummaryDetailsById(_UserId, EntityId, AcademicYearId, ClassId, EmployeeId, SubjectId, LessonSno, TestDate, BatchId, ClassYearId, SemesterId);
        }


    }




}

