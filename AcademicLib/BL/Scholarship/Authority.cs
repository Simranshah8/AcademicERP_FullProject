using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class Authority
	{

		DA.Scholarship.AuthorityDB db = null;

		int _UserId = 0;

		public Authority(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Scholarship.AuthorityDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Scholarship.Authority beData)
		{
			bool isModify = beData.AuthorityId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Scholarship.AuthorityCollections GetAllAuthority(int EntityId)
		{
			return db.getAllAuthority(_UserId, EntityId);
		}
		public BE.Scholarship.Authority GetAuthorityById(int EntityId, int AuthorityId)
		{
			return db.getAuthorityById(_UserId, EntityId, AuthorityId);
		}
		public ResponeValues DeleteById(int EntityId, int AuthorityId)
		{
			return db.DeleteById(_UserId, EntityId, AuthorityId);
		}
		public ResponeValues IsValidData(ref BE.Scholarship.Authority beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.AuthorityId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.AuthorityId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
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

