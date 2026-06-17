using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{

	public class LeftType : Dynamic.BusinessLogic.Global.Common
	{

		AcademicLib.DA.Academic.Creation.LeftTypeDB db = null;

		int _UserId = 0;

		public LeftType(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Academic.Creation.LeftTypeDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Academic.Creation.LeftType beData)
		{
			bool isModify = beData.LeftTypeId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Academic.Creation.LeftTypeCollections GetAllLeftType(int EntityId)
		{
			return db.getAllLeftType(_UserId, EntityId);
		}
		public AcademicLib.BE.Academic.Creation.LeftType GetLeftTypeById(int EntityId, int LeftTypeId)
		{
			return db.getLeftTypeById(_UserId, EntityId, LeftTypeId);
		}
		public ResponeValues DeleteLeftTypeById(int EntityId, int LeftTypeId)
		{
			return db.DeleteLeftTypeById(_UserId, EntityId, LeftTypeId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Academic.Creation.LeftType beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.LeftTypeId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.LeftTypeId != 0)
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
				else
				{
					if (!string.IsNullOrEmpty(beData.Name))
					{
						var validName = IsValidName(beData.Name);
						if (!validName.IsSuccess)
							return validName;
					}
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

