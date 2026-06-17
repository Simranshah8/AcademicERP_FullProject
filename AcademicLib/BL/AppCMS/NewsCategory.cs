using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class NewsCategory
	{

		DA.AppCMS.Creation.NewsCategoryDB db = null;

		int _UserId = 0;

		public NewsCategory(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.NewsCategoryDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.NewsCategory beData)
		{
			bool isModify = beData.CategoryId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}

		public BE.AppCMS.Creation.NewsCategoryCollections GetAllNewsCategory(int EntityId)
		{
			return db.getAllNewsCategory(_UserId, EntityId);
		}
		public BE.AppCMS.Creation.NewsCategory getNewsCategoryById(int EntityId,int CategoryId)
		{
			return db.getNewsCategoryById(_UserId, EntityId, CategoryId);
		}
		public ResponeValues DeleteById(int EntityId,int CategoryId)
		{
			return db.DeleteById(_UserId, EntityId, CategoryId);
		}

		public ResponeValues IsValidData(ref BE.AppCMS.Creation.NewsCategory beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.CategoryId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.CategoryId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Category Name ";
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

