using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Attendance
{
    public class AllowLeaveType
    {
        DA.Attendance.AllowLeaveTypeDB db = null;
        int _UserId = 0;
        public AllowLeaveType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Attendance.AllowLeaveTypeDB(hostName, dbName);
        }

        public ResponeValues SaveUpdate(AcademicLib.BE.Attendance.AllowLeaveTypeCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.UpdateAllowLeaveType(_UserId, dataColl);

            return resVal;
        }

        public AcademicLib.BE.Attendance.EmployeeForAllowLeaveTypeCollections getAllAllowLeaveType(int EntityId, int? BranchId, int? DepartmentId, int? DesignationId, int? LevelId, int? EmployeeGroupId)
        {
            return db.getAllAllowLeaveType(_UserId, EntityId, BranchId, DepartmentId, DesignationId, LevelId, EmployeeGroupId);
        }
      
        public AcademicLib.BE.Attendance.EmployeeForAllowLeaveTypeCollections GetLeaveTypeByEmployee(int UsersId)
        {
            return db.GetLeaveTypeByEmployee(_UserId, UsersId);
        }

        public ResponeValues DeleteById(int EntityId)
        {
            return db.DeleteById(_UserId, EntityId);
        }
    }
}