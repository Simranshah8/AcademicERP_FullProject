using PivotalERP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AcademicLib.BL.Communication.Creation
{
	public class ContactGroup
	{
		AcademicLib.DA.Communication.Creation.ContactGroupDB db = null;
		int _UserId = 0;

		public ContactGroup(int UserId, String hostname, String dbname)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Communication.Creation.ContactGroupDB(hostname, dbname);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Communication.Creation.ContactGroup beData)
		{
			bool isModify = beData.GroupId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}

		public AcademicLib.BE.Communication.Creation.ContactGroupCollection GetAllGroup(int EntityId)
		{
			return db.getAllContactGroup(_UserId, EntityId);
		}

		public AcademicLib.BE.Communication.Creation.ContactGroup GetContactGroupById(int EntityId, int GroupId)
		{
			return db.getContactGroupById(_UserId, EntityId, GroupId);
		}


		public ResponeValues DelGroupById(int EntityId, int GroupId)
		{
			return db.DeleteGroupById(_UserId, EntityId, GroupId);
		}


		public ResponeValues IsValidData(ref AcademicLib.BE.Communication.Creation.ContactGroup beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.GroupId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.GroupId != 0)
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
