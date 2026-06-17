using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class ScholarshipVerify
	{

		AcademicLib.DA.Scholarship.ScholarshipVerifyDB db = null;

		int _UserId = 0;

		public ScholarshipVerify(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Scholarship.ScholarshipVerifyDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Scholarship.ScholarshipVerify beData)
		{

			ResponeValues isValid = IsValidData(ref beData);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData);
			else
				return isValid;
		}
		public AcademicLib.BE.Scholarship.ScholarshipVerifyCollections GetAllScholarshipVerify(int EntityId)
		{
			return db.getAllScholarshipVerify(_UserId, EntityId);
		}
		public AcademicLib.BE.Scholarship.ScholarshipVerify GetScholarshipVerifyById(int EntityId, int TranId)
		{
			return db.getScholarshipVerifyById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Scholarship.ScholarshipVerify beData)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
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

