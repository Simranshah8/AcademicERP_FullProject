using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class AchievementSection
	{

		DA.AppCMS.Creation.AchievementSectionDB db = null;

		int _UserId = 0;

		public AchievementSection(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.AchievementSectionDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.AchievementSection beData)
		{
			bool isModify = beData.AchievementSectionId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}

		public BE.AppCMS.Creation.AchievementSectionCollections GetAllAchievementSection(int EntityId)
		{
			return db.getAllAchievementSection(_UserId, EntityId);
		}
		public BE.AppCMS.Creation.AchievementSection getAchievementSectionById(int EntityId, int AchievementSectionId)
		{
			return db.getAchievementSectionById(_UserId, EntityId, AchievementSectionId);
		}
		public ResponeValues DeleteById(int EntityId, int AchievementSectionId)
		{
			return db.DeleteById(_UserId, EntityId, AchievementSectionId);
		}

		public ResponeValues IsValidData(ref BE.AppCMS.Creation.AchievementSection beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.AchievementSectionId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.AchievementSectionId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.Headline))
				{
					resVal.ResponseMSG = "Please ! Enter Headline ";
				}
				else if(beData.CategoryId.HasValue== false || beData.CategoryId == 0)
                {
					resVal.ResponseMSG = "Please ! Select Category";
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

