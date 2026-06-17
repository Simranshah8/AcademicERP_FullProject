using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.HomeWork
{
	public class Configuration
	{

		DA.HomeWork.ConfigurationDB db = null;

		int _UserId = 0;

		public Configuration(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.HomeWork.ConfigurationDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.HomeWork.Configuration beData, int? AcademicYearId)
		{
			bool isModify = beData.BranchId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(_UserId, beData, AcademicYearId, isModify);

			else
				return isValid;
		}

		public AcademicLib.BE.HomeWork.Configuration GetHAConfiguration(int EntityId, int? BranchId)
		{
			return this.db.getHAConfiguration(this._UserId, EntityId, BranchId);
		}
		public ResponeValues IsValidData(ref BE.HomeWork.Configuration beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.BranchId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.BranchId != 0)
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