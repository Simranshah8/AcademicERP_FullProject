using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AcademicLib.BL
{
    public class Global
    {
        DA.GlobalDB db = null;
        int _UserId = 0;
        public Global(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db=new DA.GlobalDB(hostName, dbName);
        }
        public AcademicLib.API.CRM.CustomerLoginLog getNoOfDataForCRM()
        {
            return db.getNoOfDataForCRM();
        }
        public Dynamic.BusinessEntity.Global.MasterNameCodeIdCollections getMasterId(Dynamic.BusinessEntity.Global.MasterNameCodeIdCollections paraColl)
        {
            return db.getMasterId(_UserId, paraColl);
        }
        public ResponeValues ReGenerateQROfStudentEmp()
        {
            return db.ReGenerateQROfStudentEmp(_UserId);
        }
        public ResponeValues UpdateMonthName()
        {
            return db.UpdateMonthName();
        }
            public ResponeValues ClearData(int ForEntityId)
        {
            return db.ClearData(_UserId, ForEntityId);
        }
        //public ResponeValues UpdateAllSubjectMapping()
        //{
        //    return db.UpdateAllSubjectMapping();
        //}
        public Dynamic.BusinessEntity.Security.UserCollections GetWebUser()
        {
            return db.GetWebUser(_UserId);
        }
            public BE.Global.NameAndRole GetNamePhotoPath()
        {
            return db.GetNamePhotoPath(_UserId);
        }
        public AcademicLib.BE.Global.CompanyPeriodMonthCollections GetCompanyPeriodMonth()
        {
            return db.GetCompanyPeriodMonth(_UserId);
        }
        public AcademicLib.BE.Global.StudentNotificationCollections GetStudentIdColl(string RegIdColl)
        {
            return db.GetStudentIdColl(_UserId, RegIdColl);
        }
            public ResponeValues UpdateUserPwd(int uId, string NewPwd)
        {
            ResponeValues resVal = new ResponeValues();

            if (string.IsNullOrEmpty(NewPwd))
            {
                resVal.ResponseMSG = "Please ! Enter New Pwd";
            }else if (uId == 0)
            {
                resVal.ResponseMSG = "Please ! Select User Name";
            }
            else
            {
                resVal = db.UpdateUserPwd(uId, NewPwd);
            }
            return resVal;
        }
        public ResponeValues GetNotificationCountForSend()
        {
            return db.GetNotificationCountForSend(_UserId);
        }
            public ResponeValues UpdatePwd(string OldPwd,string NewPwd,string UserName)
        {
            ResponeValues resVal = new ResponeValues();

            if(string.IsNullOrEmpty(OldPwd))
            {
                resVal.ResponseMSG = "Please ! Enter Old Pwd";
            }else if (string.IsNullOrEmpty(NewPwd))
            {
                resVal.ResponseMSG = "Please ! Enter New Pwd";
            }
            else
            {
                resVal = db.UpdatePwd(_UserId, OldPwd, NewPwd,UserName);
            }
            return resVal;
        }
        //forId 1=Student,2=Teacher
        public ResponeValues GetUserIdColl(string IdColl, int forId = 1)
        {
            return db.GetUserIdColl(_UserId, IdColl, forId);
        }
        public AcademicLib.BE.Global.StudentNotificationCollections GetStudentIdColl(string IdColl, int forId = 1)
        {
            return db.GetStudentIdColl(_UserId, IdColl, forId);
        }
        public AcademicLib.RE.Global.NotificationLogCollections GetNotificationLog(bool isGeneral, DateTime? dateFrom, DateTime? dateTo, ref int TotalRows, int PageNumber = 1, int RowsOfPage = 100,string For="")
        {
            return db.GetNotificationLog(_UserId, isGeneral, dateFrom, dateTo, ref TotalRows, PageNumber,RowsOfPage,For);
        }
        public AcademicLib.RE.Global.NotificationLogCollections GetNotificationLogForQuickAccess(int StudentId)
        {
            return db.GetNotificationLogForQuickAccess(_UserId, StudentId);
        }
            public AcademicLib.RE.Global.NotificationLogCollections GetTopNotificationLog()
        {
            return db.GetTopNotificationLog(_UserId);
        }
            public AcademicLib.BE.Global.CurrentDate GetDateDetail(DateTime? forDate)
        {
            return db.GetDateDetail(_UserId, forDate);
        }
        public AcademicLib.RE.Global.AdminDashboard GetAdminDashboard(int AcademicYearId, int? branchId)
        {
            return db.GetAdminDashboard(_UserId,AcademicYearId,branchId);
        }
        public AcademicLib.BE.Global.CompanyPeriodMonthCollections usp_GetMonthNameList(int AcademicYearId)
        {
            return db.usp_GetMonthNameList(_UserId, AcademicYearId);
        }
        public Dynamic.BusinessEntity.Security.User getUserPwdByQrCode( string QrCode)
        {
            return db.getUserPwdByQrCode(_UserId, QrCode);
        }
        public AcademicLib.BE.Global.CompanyPeriodMonthCollections getAcademicYearMonthList(int? AcademicYearId, int? StudentId, int? ClassId)
        {
            return db.getAcademicYearMonthList(_UserId, AcademicYearId, StudentId, ClassId);
        }
    }
}
