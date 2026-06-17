using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Attendance
{
    public class LeaveRequest
    {
        DA.Attendance.LeaveRequestDB db = null;
        int _UserId = 0;
        public LeaveRequest(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Attendance.LeaveRequestDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Attendance.LeaveRequest beData)
        {
            bool isModify = beData.LeaveRequestId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues SaveFromApp(AcademicLib.API.Attendance.LeaveRequest beData)
        {
            return db.SaveFromApp(beData);
        }
            public BE.Attendance.LeaveRequestCollections GetAllLeaveRequest(int EntityId)
        {
            return db.GetAllLeaveRequest(_UserId);
        }
        public BE.Attendance.LeaveRequest GetLeaveRequestById(int EntityId, int LeaveRequestId)
        {
            return db.getLeaveRequestById(_UserId, LeaveRequestId);
        }
        public ResponeValues DeleteById(int EntityId, int LeaveRequestId)
        {
            return db.DeleteById(_UserId,EntityId, LeaveRequestId);
        }
        public AcademicLib.RE.Attendance.EmpLeaveRequestCollections getEmpLeaveRequestLst(DateTime? dateFrom, DateTime? dateTo, int LeaveStatus,int? EmployeeId)
        {
            return db.getEmpLeaveRequestLst(_UserId, dateFrom, dateTo, LeaveStatus,EmployeeId);
        }
        public AcademicLib.RE.Attendance.StudentLeaveRequestCollections getStudentLeaveRequestLst(DateTime? dateFrom, DateTime? dateTo, int LeaveStatus, int? StudentId, int? ClassId, int? SectionId, int? AcademicYearId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.getStudentLeaveRequestLst(_UserId, dateFrom, dateTo, LeaveStatus, StudentId, ClassId, SectionId, AcademicYearId, BatchId,SemesterId,ClassYearId);
        }
            public ResponeValues LeaveApproved(AcademicLib.API.Attendance.LeaveApprove beData)
        {
            return db.LeaveApproved(beData);
        }

            public ResponeValues IsValidData(ref BE.Attendance.LeaveRequest beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.LeaveRequestId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.LeaveRequestId != 0)
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
