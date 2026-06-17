using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.FrontDesk.Transaction
{

	public class AttendanceFollowUp
	{

		DA.FrontDesk.Transaction.AttendanceFollowUpDB db = null;

		int _UserId = 0;

		public AttendanceFollowUp(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.FrontDesk.Transaction.AttendanceFollowUpDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.FrontDesk.Transaction.AttendanceFollowUp beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
        public AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUpColl GetAllAttendanceFollowUp(DateTime? DateFrom, DateTime? DateTo, int? ClassId, int? SectionId, int? AcademicYearId, int? BatchId, int? SemesterId, int? ClassYearId, int? ClassShiftId)
        {
            return db.getAllAttendanceFollowUp(_UserId, DateFrom, DateTo, ClassId, SectionId, AcademicYearId, BatchId, SemesterId, ClassYearId, ClassShiftId);
        }
        //public AcademicLib.BE.AttendanceFollowUp.Reporting.AttendanceFollowUp GetAttendanceFollowUpById(int EntityId, int TranId)
        //{
        //	return db.getAttendanceFollowUpById(_UserId, EntityId, TranId);
        //}
        //public ResponeValues DeleteById(int EntityId, int TranId)
        //{
        //	return db.DeleteById(_UserId, EntityId, TranId);
        //}


        public RE.FrontDesk.AttendanceFollowupCollections getStudentAttendanceFollowup(int? StudentId)
        {
            return db.getStudentAttendanceFollowup(_UserId, StudentId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUp beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
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

