using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Creation
{

	public class Labs
	{

		DA.Academic.Creation.LabsDB db = null;

		int _UserId = 0;

		public Labs(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Creation.LabsDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Academic.Creation.Labs beData)
		{
			bool isModify = beData.LabsId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Academic.Creation.LabsCollections GetAllLabs()
		{
			return db.getAllLabs(_UserId);
		}
		public BE.Academic.Creation.Labs GetLabsById(int EntityId, int LabsId)
		{
			return db.getLabsById(_UserId, EntityId, LabsId);
		}
		public ResponeValues DeleteById(int EntityId, int LabsId)
		{
			return db.DeleteById(_UserId, EntityId, LabsId);
		}
		public ResponeValues IsValidData(ref BE.Academic.Creation.Labs beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.LabsId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.LabsId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.LabName))
				{
					resVal.ResponseMSG = "Please ! Enter LabName ";
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

