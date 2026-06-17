using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class NewsSection
	{

		DA.AppCMS.Creation.NewsSectionDB db = null;

		int _UserId = 0;

		public NewsSection(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.NewsSectionDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.NewsSection beData)
		{
			bool isModify = beData.NewsSectionId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}

		public BE.AppCMS.Creation.NewsSectionCollections GetAllNewsSection(int EntityId)
		{
			return db.getAllNewsSection(_UserId, EntityId);
		}
		public BE.AppCMS.Creation.NewsSection getNewsSectionById(int EntityId, int NewsSectionId)
		{
			return db.getNewsSectionById(_UserId, EntityId, NewsSectionId);
		}
		public ResponeValues DeleteById(int EntityId, int NewsSectionId)
		{
			return db.DeleteById(_UserId, EntityId, NewsSectionId);
		}

		public ResponeValues IsValidData(ref BE.AppCMS.Creation.NewsSection beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.NewsSectionId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.NewsSectionId != 0)
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

