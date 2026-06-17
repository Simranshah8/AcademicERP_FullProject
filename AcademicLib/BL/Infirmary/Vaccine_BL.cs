using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class Vaccine  
	{ 

		DA.VaccineDB db = null;

		int _UserId = 0;

		public Vaccine(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.VaccineDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Vaccine beData)
		{
			bool isModify = beData.VaccineId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.VaccineCollections GetAllVaccine(int EntityId)
		{
			return db.getAllVaccine(_UserId, EntityId);
		}
		public BE.Vaccine GetVaccineById(int EntityId, int VaccineId)
		{
			return db.getVaccineById(_UserId, EntityId, VaccineId);
		}
		public ResponeValues DeleteById(int EntityId, int VaccineId)
		{
			return db.DeleteById(_UserId, EntityId, VaccineId);
		}
		public ResponeValues IsValidData(ref BE.Vaccine beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.VaccineId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.VaccineId != 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
			}
			else if (beData.CUserId == 0)
			{
				resVal.ResponseMSG = "Invalid User for CRUD";
			}
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Vaccine Name ";
				}


				else if (!beData.VaccineForId.HasValue || beData.VaccineForId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Vaccine For Health Issues";
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

