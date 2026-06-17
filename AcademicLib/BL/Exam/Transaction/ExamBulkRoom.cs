using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class ExamBulkRoom
	{

		AcademicLib.DA.Exam.Transaction.ExamBulkRoomDB db = null;

		int _UserId = 0;

		public ExamBulkRoom(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Exam.Transaction.ExamBulkRoomDB(hostName, dbName);
		}

		public ResponeValues SaveFormData(AcademicLib.BE.Exam.Transaction.ExamBulkRoomCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.SaveUpdate(_UserId, dataColl);

			return resVal;
		}
		public BE.Exam.Transaction.ExamBulkRoomCollections GetAllExamBulkRoom(int EntityId)
		{
			return db.getAllExamBulkRoom(_UserId, EntityId);
		}

	}

}

