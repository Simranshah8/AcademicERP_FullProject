using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Payroll
{

	public class PayHeadGroup
	{

		DA.Payroll.PayHeadGroupDB db = null;

		int _UserId = 0;

		public PayHeadGroup(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Payroll.PayHeadGroupDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Payroll.PayHeadGroup beData)
		{
			bool isModify = beData.PayHeadGroupId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Payroll.PayHeadGroupCollections GetAllPayHeadGroup(int EntityId)
		{
			return db.getAllPayHeadGroup(_UserId, EntityId);
		}
		public AcademicLib.BE.Payroll.PayHeadGroup GetPayHeadGroupById(int EntityId, int PayHeadGroupId)
		{
			return db.getPayHeadGroupById(_UserId, EntityId, PayHeadGroupId);
		}
		public ResponeValues DeleteById(int EntityId, int PayHeadGroupId)
		{
			return db.DeleteById(_UserId, EntityId, PayHeadGroupId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.PayHeadGroup beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.PayHeadGroupId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.PayHeadGroupId != 0)
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

