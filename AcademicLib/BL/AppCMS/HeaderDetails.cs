using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{
	public class HeaderDetails
	{
		DA.AppCMS.Creation.HeaderDetailsDB db = null;

		int _UserId = 0;

		public HeaderDetails(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.HeaderDetailsDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.HeaderDetails beData)
		{
			bool isModify = beData.HeaderDetailId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}

		public BE.AppCMS.Creation.HeaderDetails GetHeaderDetailsById(int EntityId)
		{
			return db.getHeaderDetailsById(_UserId, EntityId);
		}
		public ResponeValues IsValidData(ref BE.AppCMS.Creation.HeaderDetails beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.HeaderDetailId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.HeaderDetailId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.CompanyName))
				{
					resVal.ResponseMSG = "Please ! Enter CompanyName ";
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

