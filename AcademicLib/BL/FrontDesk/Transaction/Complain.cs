using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class Complain : AcademicLib.BL.CommonBL
	{

		AcademicLib.DA.FrontDesk.Transaction.ComplainDB db = null;

		int _UserId = 0;

		public Complain(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db =new DA.FrontDesk.Transaction.ComplainDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.FrontDesk.Transaction.Complain beData)
		{
			bool isModify = beData.ComplainId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.FrontDesk.Transaction.ComplainCollections GetAllComplain(int EntityId, DateTime? dateFrom, DateTime? dateTo, int? SourceId, int? ComplainTypeId, int? StatusId)
		{
			return db.getAllComplain(_UserId, EntityId, dateFrom, dateTo, SourceId, ComplainTypeId, StatusId);
		}
		public AcademicLib.BE.FrontDesk.Transaction.Complain GetComplainById(int EntityId, int ComplainId)
		{
			return db.getComplainById(_UserId, EntityId, ComplainId);
		}
		public ResponeValues DeleteById(int EntityId, int ComplainId)
		{
			return db.DeleteById(_UserId, EntityId, ComplainId);
		}

		public ResponeValues SaveComplainReply(AcademicLib.BE.FrontDesk.Transaction.ComplainReply beData)
		{
			ResponeValues resVal = new ResponeValues();

			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (beData.ComplainId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " Complain Reply";
			}
			else if (beData.CUserId == 0)
			{
				resVal.ResponseMSG = "Invalid User for CRUD";
			}


			else if (string.IsNullOrEmpty(beData.Remarks))
			{
				resVal.ResponseMSG = "Please ! Enter Remarks";
			}

			else
			{
				resVal = db.SaveComplainReply(beData);
			}

			return resVal;

		}

		public ResponeValues IsValidData(ref AcademicLib.BE.FrontDesk.Transaction.Complain beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				//Added contact validation
				var vMobile = base.IsValidContactNo(beData.PhoneNo);

				if (vMobile.IsSuccess == false)
				{
					return vMobile;
				}
				//Ends
				else if (IsModify && beData.ComplainId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.ComplainId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.ComplainBy == 0)
				{
					resVal.ResponseMSG = "Please ! Select ComplainBy ";
				}
				else if (!beData.ComplainTypeId.HasValue || beData.ComplainTypeId == 0)
				{
					resVal.ResponseMSG = "Please ! Select ComplainType ";
				}
				else if (!beData.SourceId.HasValue || beData.SourceId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Source ";
				}
				//else if (!beData.AssignToId.HasValue || beData.AssignToId == 0)
				//{
				//	resVal.ResponseMSG = "Please ! Select AssignTO ";
				//}
				else if (string.IsNullOrEmpty(beData.OthersName))
				{
					resVal.ResponseMSG = "Please ! Enter Name";
				}
				else if (string.IsNullOrEmpty(beData.Remarks))
				{
					resVal.ResponseMSG = "Please ! Enter Complain Details";
				}
				else if (string.IsNullOrEmpty(beData.PhoneNo))
				{
					resVal.ResponseMSG = "Please ! Enter Phone Number";
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

