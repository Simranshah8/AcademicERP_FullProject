using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class StudentPaymentFollowup
    {
        DA.FrontDesk.Transaction.StudentPaymentFollowupDB db = null;
        int _UserId = 0;

        public StudentPaymentFollowup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.StudentPaymentFollowupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.StudentPaymentFollowup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public RE.FrontDesk.StudentPaymentFollowupCollections getStudentWiseList(int? StudentId, int? AcademicYearId)
        {
            return db.getStudentWiseList(_UserId, StudentId, AcademicYearId);
        }

        public RE.FrontDesk.StudentPaymentFollowupCollections getFollowupList(DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getFollowupList(_UserId, dateFrom, dateTo);
        }

        public ResponeValues SaveClosed( int RefTranId, string Remarks)
        {
            return db.SaveClosed(_UserId, RefTranId, Remarks);
        }
            public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.StudentPaymentFollowup beData, bool IsModify)
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
                else if (beData.AcademicYearId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Academic Year";
                }
                else if (beData.StudentId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Student";
                }
                else
                {
                    
                    if(beData.NextFollowupDate.HasValue)
                    {
                        DateTime nextDate = beData.NextFollowupDate.Value;
                        if (beData.NextFollowupTime.HasValue)
                        {
                            var tm = beData.NextFollowupTime.Value;
                            beData.NextFollowupDateTime = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, tm.Hour, tm.Minute, tm.Second);
                        }
                        else
                            beData.NextFollowupDateTime = nextDate;

                    }

                    if (beData.FeeItemId.HasValue && beData.FeeItemId.Value == 0)
                        beData.FeeItemId = null;

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
