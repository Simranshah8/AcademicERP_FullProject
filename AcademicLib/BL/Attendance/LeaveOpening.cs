using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Attendance
{
    public class LeaveOpening
    {
        DA.Attendance.LeaveOpeningDB db = null;
        int _UserId = 0;
        public LeaveOpening(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Attendance.LeaveOpeningDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Attendance.LeaveOpening beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Attendance.LeaveOpeningCollections GetAllLeaveOpening(int EntityId)
        {
            return db.getAllLeaveOpening(_UserId);
        }
        public AcademicLib.BE.Attendance.LeaveOpening getLeaveOpeningById(int EmployeeId, int PeriodId)
        {
            return db.getLeaveOpeningById(_UserId, EmployeeId, PeriodId);
        }
            public ResponeValues DeleteById(int EntityId, int LeaveOpeningId)
        {
            return db.DeleteById(_UserId, EntityId, LeaveOpeningId);
        }
        //Added for MeployeeWise Leave opening
        public BE.Attendance.LeaveOpeningCollections getAllEmpLeaveOpening(int? BranchId, int? DepartmentId, int? CategoryId, int PeriodId)
        {
            return db.getAllEmpLeaveOpening(_UserId, BranchId, DepartmentId, CategoryId, PeriodId);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Attendance.LeaveOpeningCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            foreach (var beData in dataColl)
            {
                if (beData.PeriodId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Period to save Leave Oepning ";
                    return resVal;
                }
            }
            resVal = db.SaveUpdateLeaveOpeningColl(_UserId, dataColl);
            return resVal;
        }

        public ResponeValues DeleteEmpById(int? TranId, int? PeriodId)
        {
            return db.DeleteEmpById(_UserId, TranId, PeriodId);
        }
        //Ends
        public ResponeValues IsValidData(ref BE.Attendance.LeaveOpening beData, bool IsModify)
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
