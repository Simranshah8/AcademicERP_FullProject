using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class SocialMedia  
	{ 

		DA.AppCMS.Creation.SocialMediaDB db = null;

		int _UserId = 0;

		public SocialMedia(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.SocialMediaDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.AppCMS.Creation.SocialMedia beData)
		{
			bool isModify = beData.SocialMediaId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.AppCMS.Creation.SocialMediaCollections GetAllSocialMedia(int EntityId)
		{
			return db.getAllSocialMedia(_UserId, EntityId);
		}
		public AcademicLib.BE.AppCMS.Creation.SocialMedia GetSocialMediaById(int EntityId, int SocialMediaId)
		{
			return db.getSocialMediaById(_UserId, EntityId, SocialMediaId);
		}
		public ResponeValues DeleteById(int EntityId, int SocialMediaId)
		{
			return db.DeleteById(_UserId, EntityId, SocialMediaId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.AppCMS.Creation.SocialMedia beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.SocialMediaId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.SocialMediaId != 0)
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
			else if (string.IsNullOrEmpty(beData.IconPath))
			{
				resVal.ResponseMSG = "Please ! Enter IconPath ";
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

