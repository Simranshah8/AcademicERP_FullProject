using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class GrievanceRedressalTeam: Dynamic.BusinessLogic.Global.Common
	{

		DA.AppCMS.Creation.GrievanceRedressalTeamDB db = null;

		int _UserId = 0;

		public GrievanceRedressalTeam(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.GrievanceRedressalTeamDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.GrievanceRedressalTeam beData)
		{
			bool isModify = beData.GrievanceRedressalId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.AppCMS.Creation.GrievanceRedressalTeamCollections GetAllGrievanceRedressalTeam(int EntityId)
		{
			return db.getAllGrievanceRedressalTeam(_UserId, EntityId);
		}
		public BE.AppCMS.Creation.GrievanceRedressalTeam GetGrievanceRedressalTeamById(int EntityId, int GrievanceRedressalId)
		{
			return db.getGrievanceRedressalTeamById(_UserId, EntityId, GrievanceRedressalId);
		}
		public ResponeValues DeleteById(int EntityId, int GrievanceRedressalId)
		{
			return db.DeleteById(_UserId, EntityId, GrievanceRedressalId);
		}
		public ResponeValues IsValidData(ref BE.AppCMS.Creation.GrievanceRedressalTeam beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.GrievanceRedressalId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.GrievanceRedressalId != 0)
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
				else if (string.IsNullOrEmpty(beData.Designation))
				{
					resVal.ResponseMSG = "Please ! Enter Designation ";
				}
				
				else if (string.IsNullOrEmpty(beData.Contact))
				{
					resVal.ResponseMSG = "Please ! Enter Contact ";
				}
				
				else if (string.IsNullOrEmpty(beData.Image))
				{
					
					resVal.ResponseMSG = "Please ! Upload Photo";
				}
				else
				{
					
					if (!string.IsNullOrEmpty(beData.Contact))
					{
						var validNo = IsValidContactNo(beData.Contact);
						if (!validNo.IsSuccess)
							return new ResponeValues
							{
								IsSuccess = false,
								ResponseMSG = "Contact number is invalid. Please enter a valid 10-digit number."
							};
					}


					if (!string.IsNullOrEmpty(beData.Email))
					{
						var validNo = IsValidEmail(beData.Email);
						if (!validNo.IsSuccess)
							return new ResponeValues
							{
								IsSuccess = false,
								ResponseMSG = "Email is Invalid. Please enter Correct Email."
							};
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

