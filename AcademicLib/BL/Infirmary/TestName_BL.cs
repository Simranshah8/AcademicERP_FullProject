using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class TestName
	{

		DA.TestNameDB db = null;

		int _UserId = 0;

		public TestName(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.TestNameDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.TestName beData)
		{
			bool isModify = beData.TestNameId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.TestNameCollections GetAllTestName(int EntityId)
		{
			return db.getAllTestName(_UserId, EntityId);
		}
		public BE.TestName GetTestNameById(int EntityId, int TestNameId)
		{
			return db.getTestNameById(_UserId, EntityId, TestNameId);
		}
		public ResponeValues DeleteById(int EntityId, int TestNameId)
		{
			return db.DeleteById(_UserId, EntityId, TestNameId);
		}
		public ResponeValues IsValidData(ref BE.TestName beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TestNameId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TestNameId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Test Name ";
				}
				//Added By Suresh on 19 Baishakh
				else if (beData.CheckupGroupId == 0 || beData.CheckupGroupId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select CheckupGroupId";
				}

				//Ends
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

