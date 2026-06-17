using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Payroll
{

	public class EmployeeGroup
	{

		DA.Payroll.EmployeeGroupDB db = null;

		int _UserId = 0;

		public EmployeeGroup(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Payroll.EmployeeGroupDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Payroll.EmployeeGroup beData)
		{
			bool isModify = beData.EmployeeGroupId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Payroll.EmployeeGroupCollections GetAllEmployeeGroup(int EntityId)
		{
			return db.getAllEmployeeGroup(_UserId, EntityId);
		}
		public AcademicLib.BE.Payroll.EmployeeGroup GetEmployeeGroupById(int EntityId, int EmployeeGroupId)
		{
			return db.getEmployeeGroupById(_UserId, EntityId, EmployeeGroupId);
		}
		public ResponeValues DeleteById(int EntityId, int EmployeeGroupId)
		{
			return db.DeleteById(_UserId, EntityId, EmployeeGroupId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.EmployeeGroup beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.EmployeeGroupId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.EmployeeGroupId != 0)
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
				else if (beData.BaseGroupId == 0)
				{
					resVal.ResponseMSG = "Please ! Select BaseGroup ";
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

