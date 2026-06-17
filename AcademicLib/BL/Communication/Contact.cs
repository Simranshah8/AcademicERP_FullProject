using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Communication.Creation
{
	public class ContactDetails
	{
		AcademicLib.DA.Communication.Creation.ContactDB db = null;
		int _UserId = 0;

		public ContactDetails(int UserId, String hostname, String dbname)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Communication.Creation.ContactDB(hostname, dbname);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Communication.Creation.ContactDetails bedata)
		{
			bool isModify = bedata.ContactId > 0;
			ResponeValues isValid = IsValidData(ref bedata, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(bedata, isModify);
			else
				return isValid;
		}

		public AcademicLib.BE.Communication.Creation.ContactDetailsCollection GetAllContactDetails(int EntityId)
		{
			return db.getAllContact(_UserId, EntityId);
		}


		public AcademicLib.BE.Communication.Creation.ContactDetails GetContactById(int EntityId, int ContactId)
		{
			return db.getCommunicationContactById(_UserId, EntityId, ContactId);
		}


		public ResponeValues DelContactDetailsById(int EntityId, int ContactId)
		{
			return db.DeleteById(_UserId, EntityId, ContactId);
		}





		public ResponeValues IsValidData(ref AcademicLib.BE.Communication.Creation.ContactDetails beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.ContactId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.ContactId != 0)
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