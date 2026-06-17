using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Creation
{

	public class SubjectGroup  
	{ 

		DA.Academic.Creation.SubjectGroupDB db = null;

		int _UserId = 0;

		public SubjectGroup(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db =new DA.Academic.Creation.SubjectGroupDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Academic.Creation.SubjectGroup beData)
		{
			bool isModify = beData.SubjectGroupId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Academic.Creation.SubjectGroupCollections GetAllSubjectGroup(int EntityId)
		{
			return db.getAllSubjectGroup(_UserId, EntityId);
		}
		public BE.Academic.Creation.SubjectGroup GetSubjectGroupById(int EntityId, int SubjectGroupId)
		{
			return db.getSubjectGroupById(_UserId, EntityId, SubjectGroupId);
		}
		public ResponeValues DeleteById(int EntityId, int SubjectGroupId)
		{
			return db.DeleteById(_UserId, EntityId, SubjectGroupId);
		}
		public ResponeValues IsValidData(ref BE.Academic.Creation.SubjectGroup beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.SubjectGroupId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.SubjectGroupId != 0)
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

