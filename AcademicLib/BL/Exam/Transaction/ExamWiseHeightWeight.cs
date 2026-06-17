using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class ExamWiseHeightWeight
	{
		DA.Exam.Transaction.ExamWiseHeightWeightDB db = null;
		int _UserId = 0;

		public ExamWiseHeightWeight(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.ExamWiseHeightWeightDB(hostName, dbName);
		}
		public ResponeValues TransforHeightWeight(int FromExamTypeId, int ToExamTypeId)
		{
			return db.TransforHeightWeight(_UserId, FromExamTypeId, ToExamTypeId);
		}
		public AcademicLib.BE.Exam.Transaction.ExamWiseHeightWeightCollections GetExamWiseHeightWeights()
		{
			return db.GetExamWiseHeightWeights(_UserId);
		}
		public ResponeValues DeleteById(int TranId)
		{
			return db.DeleteById(_UserId, TranId);
		}

	}

}

