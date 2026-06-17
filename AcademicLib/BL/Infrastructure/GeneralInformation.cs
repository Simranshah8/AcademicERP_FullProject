using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Infrastructure
{

	public class GeneralInformation
	{

		DA.Infrastructure.GeneralInformationDB db = null;

		int _UserId = 0;

		public GeneralInformation(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Infrastructure.GeneralInformationDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Infrastructure.GeneralInformation beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Infrastructure.GeneralInformation GetAllGeneralInformation(int EntityId)
		{
			return db.getAllGeneralInformation(_UserId, EntityId);
		}
		public BE.Infrastructure.GeneralInformation GetGeneralInformationById(int EntityId, int TranId)
		{
			return db.getGeneralInformationById(_UserId, EntityId, TranId);
		}

		public BE.Infrastructure.EmpShortDet GetEmpShortDetbyId(int EntityId, int EmployeeId)
		{
			return db.getEmpShortDetbyId(_UserId, EntityId, EmployeeId);
		}
		public ResponeValues IsValidData(ref BE.Infrastructure.GeneralInformation beData, bool IsModify)
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

