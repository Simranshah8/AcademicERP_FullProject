using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Transaction
{

	public class ContinuousConfirmation
	{

		DA.Academic.Transaction.ContinuousConfirmationDB db = null;

		int _UserId = 0;

		public ContinuousConfirmation(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Transaction.ContinuousConfirmationDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Academic.Transaction.ContinuousConfirmation beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Academic.Transaction.ContinuousConfirmationCollections GetAllContinuousConfirmation(int EntityId, int? ClassId, int? SectionId)
		{
			return db.getAllContinuousConfirmation(_UserId, EntityId, ClassId, SectionId);
		}
		public BE.Academic.Transaction.ContinuousConfirmation GetContinuousConfirmationById(int EntityId, int TranId)
		{
			return db.getContinuousConfirmationById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
		public ResponeValues IsValidData(ref BE.Academic.Transaction.ContinuousConfirmation beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TranId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TranId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				
				//else if (beData.StudentId == 0)
				//{
				//	resVal.ResponseMSG = "Please ! Select Student ";
				//}
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

