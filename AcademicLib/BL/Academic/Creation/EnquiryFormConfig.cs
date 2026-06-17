using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Creation
{

	public class EnquiryFormConfig
	{

		DA.Academic.Creation.EnquiryFormConfigDB db = null;

		int _UserId = 0;

		public EnquiryFormConfig(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Creation.EnquiryFormConfigDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Academic.Creation.EnquiryFormConfig beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Academic.Creation.EnquiryFormConfig GetAllEnquiryFormConfig(int EntityId)
		{
			return db.getAllEnquiryFormConfig(_UserId, EntityId);
		}
		public ResponeValues IsValidData(ref BE.Academic.Creation.EnquiryFormConfig beData, bool IsModify)
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
				else if (beData.FormStatus == 0 || beData.FormStatus.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select FormStatus ";
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

