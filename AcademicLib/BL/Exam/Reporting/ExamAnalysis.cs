using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Exam.Reporting
{
    public class ExamAnalysis
    {
		DA.Exam.Reporting.ExamAnalysisDB db = null;

		int _UserId = 0;

		public ExamAnalysis(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Reporting.ExamAnalysisDB(hostName, dbName);
		}
		public RE.Exam.Reporting.ExamAnalysis GetExamAnalysisData(int EntityId)
		{
			return db.GetExamAnalysisData(_UserId, EntityId);
		}
	}
}