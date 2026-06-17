using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class ThemeConfig
	{

		DA.AppCMS.Creation.ThemeConfigDB db = null;

		int _UserId = 0;

		public ThemeConfig(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.ThemeConfigDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.ThemeConfig beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		
		public BE.AppCMS.Creation.ThemeConfig GetAllThemeConfig(int EntityId)
		{
			return db.getAllThemeConfig(_UserId, EntityId);
		}

		public ResponeValues IsValidData(ref BE.AppCMS.Creation.ThemeConfig beData, bool IsModify)
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
				else if (beData.ThemeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Theme ";
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

