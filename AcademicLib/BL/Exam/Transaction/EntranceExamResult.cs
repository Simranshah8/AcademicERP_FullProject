using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Exam.Transaction
{
    public class EntranceExamResult
    {
        DA.Exam.Transaction.EntranceExamResultDB db = null;

        int _UserId = 0;

        public EntranceExamResult(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.EntranceExamResultDB(hostName, dbName);
        }
        public BE.Exam.Transaction.MarkEntryCollections GetEntranceResult(int? EntityId,int? ClassId)
        {
            return db.GetEntranceResult(_UserId, EntityId, ClassId);
        }
        public ResponeValues SaveFormData(List<BE.Exam.Transaction.EntranceMarkEntry> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.SaveUpdate(_UserId, dataColl);

            return resVal;

            //bool isModify = beData.TranId > 0;
            //ResponeValues isValid = IsValidData(ref beData, isModify);
            //if (isValid.IsSuccess)
            //    return db.SaveUpdate(beData);
            //else
            //    return isValid;
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.EntranceMarkEntry beData, bool IsModify)
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

        //Add by prashtant Baishak 02

        public BE.Exam.Transaction.MarkSetupCollections GetMarkSetup(int? EntityId, int? ClassId)
        {
            return db.GetMarkSetup(_UserId, EntityId, ClassId);
        }

        public ResponeValues SaveMarkSetup(List<BE.Exam.Transaction.EntranceMarkSetup> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.SaveMarkSetup(_UserId, dataColl);

            return resVal;

        }
    }
}