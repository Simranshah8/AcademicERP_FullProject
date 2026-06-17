using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class AppliedSchool
	{

		AcademicLib.DA.Scholarship.AppliedSchoolDB db = null;

		int _UserId = 0;

		public AppliedSchool(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Scholarship.AppliedSchoolDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Scholarship.AppliedSchool beData)
		{
			bool isModify = beData.SchoolId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Scholarship.AppliedSchoolCollections GetAllAppliedSchool(int EntityId)
		{
			return db.getAllAppliedSchool(_UserId, EntityId);
		}
		public AcademicLib.BE.Scholarship.AppliedSchool GetAppliedSchoolById(int EntityId, int SchoolId)
		{
			return db.getAppliedSchoolById(_UserId, EntityId, SchoolId);
		}
		public ResponeValues DeleteById(int EntityId, int SchoolId)
		{
			return db.DeleteById(_UserId, EntityId, SchoolId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Scholarship.AppliedSchool beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.SchoolId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.SchoolId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if(beData.SchoolSubjectListColl==null || beData.SchoolSubjectListColl.Count == 0)
                {
					resVal.ResponseMSG = "Please ! Select Subject";
                }
				else
				{
					var countSub = beData.SchoolSubjectListColl.Where(p1 => p1.AllowSubject == true).Count();
                    if (countSub == 0)
                    {
						resVal.IsSuccess = false;
						resVal.ResponseMSG = "Please ! Select Subject";
						return resVal;
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

