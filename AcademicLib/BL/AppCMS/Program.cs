using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class Program
	{

		DA.AppCMS.Creation.ProgramDB db = null;

		int _UserId = 0;

		public Program(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.ProgramDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.Program beData)
		{
			bool isModify = beData.ProgramId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}

		public BE.AppCMS.Creation.ProgramCollections GetAllProgram(int EntityId)
		{
			return db.getAllProgram(_UserId, EntityId);
		}
		public BE.AppCMS.Creation.Program getProgramById(int EntityId, int ProgramId)
		{
			return db.getProgramById(_UserId, EntityId, ProgramId);
		}
		public ResponeValues DeleteById(int EntityId, int ProgramId)
		{
			return db.DeleteById(_UserId, EntityId, ProgramId);
		}

		public ResponeValues IsValidData(ref BE.AppCMS.Creation.Program beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.ProgramId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.ProgramId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Program Name ";
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

