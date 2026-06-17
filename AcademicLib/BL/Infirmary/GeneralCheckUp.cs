using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class GeneralCheckUp
	{

		DA.GeneralCheckUpDB db = null;

		int _UserId = 0;

		public GeneralCheckUp(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.GeneralCheckUpDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.GeneralCheckUp beData)
		{
			bool isModify = beData.GeneralCheckUpId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.GeneralCheckUpCollections GetAllGeneralCheckUp(int EntityId)
		{
			return db.getAllGeneralCheckUp(_UserId, EntityId);
		}
		public BE.GeneralCheckUp GetGeneralCheckUpById(int EntityId, int GeneralCheckUpId)
		{
			return db.getGeneralCheckUpById(_UserId, EntityId, GeneralCheckUpId);
		}
		public ResponeValues DeleteById(int EntityId, int GeneralCheckUpId)
		{
			return db.DeleteById(_UserId, EntityId, GeneralCheckUpId);
		}
		public ResponeValues IsValidData(ref BE.GeneralCheckUp beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.GeneralCheckUpId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.GeneralCheckUpId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Name ";
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

