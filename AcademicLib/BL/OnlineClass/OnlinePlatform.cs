using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.OnlineClass
{
    public class OnlinePlatform
    {
        DA.OnlineClass.OnlinePlatformDB db = null;
        int _UserId = 0;
        public OnlinePlatform(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.OnlineClass.OnlinePlatformDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.OnlineClass.OnlinePlatform beData)
        {            
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData);
            else
                return isValid;
        }
        public BE.OnlineClass.OnlinePlatformCollections GetAllOnlinePlatform(int EntityId)
        {
            return db.getAllOnlinePlatform(_UserId, EntityId);
        }        
        public ResponeValues IsValidData(ref BE.OnlineClass.OnlinePlatform beData)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
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
        public RE.OnlineClass.DateWiseAttendanceCollections getDateWiseAttendance(DateTime forDate, int? classId, int? sectionId)
        {
            return db.getDateWiseAttendance(_UserId, forDate, classId, sectionId);
        }
        public RE.OnlineClass.DateWiseAttendanceCollections getSubjectWiseAttendance(DateTime fromDate, DateTime toDate, int? classId, int? sectionId, int subjectId)
        {
            return db.getSubjectWiseAttendance(_UserId, fromDate, toDate, classId, sectionId, subjectId);
        }
        public RE.OnlineClass.EmployeeAttendanceCollections getEmployeeAttendance( DateTime fromDate, DateTime toDate)
        {
            return db.getEmployeeAttendance(_UserId, fromDate, toDate);
        }
        public AcademicLib.RE.Academic.PassedOnlineClassCollections getEmployeeAttendanceDet(string tranIdColl)
        {
            return db.getEmployeeAttendanceDet(_UserId, tranIdColl);
        }

     }
}
