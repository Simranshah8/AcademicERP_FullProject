using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class BenchColumn
	{

		DA.Exam.Transaction.BenchColumnDB db = null;

		int _UserId = 0;

		public BenchColumn(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.BenchColumnDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Exam.Transaction.BenchColumn beData)
		{
			bool isModify = beData.BenchColumnId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Exam.Transaction.BenchColumnCollections GetAllBenchColumn(int EntityId)
		{
			return db.getAllBenchColumn(_UserId, EntityId);
		}
		public BE.Exam.Transaction.BenchColumn GetBenchColumnById(int EntityId, int BenchColumnId)
		{
			return db.getBenchColumnById(_UserId, EntityId, BenchColumnId);
		}
		public ResponeValues DeleteById(int EntityId, int BenchColumnId)
		{
			return db.DeleteById(_UserId, EntityId, BenchColumnId);
		}

		public BE.Exam.Transaction.BenchColumnDetailCollections GetColumnNameById(int EntityId,int NoOfColumn)
		{
			return db.getColumnNameById(_UserId, EntityId, NoOfColumn);
		}
		public ResponeValues IsValidData(ref BE.Exam.Transaction.BenchColumn beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.BenchColumnId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.BenchColumnId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.BenchTypeName))
				{
					resVal.ResponseMSG = "Please ! Enter BenchTypeName ";
				}
				else
				{
					resVal.IsSuccess = true;
					resVal.ResponseMSG = "Valid";
				}
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			return resVal;
		}
	}

}

