using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class PostalCallLog : AcademicLib.BL.CommonBL
    {
        DA.FrontDesk.Transaction.PostalCallLogDB db = null;
        int _UserId = 0;

        public PostalCallLog(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.PostalCallLogDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.PostalCallLog beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.PostalCallLogCollections GetAllPostalCallLog(int EntityId, DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getAllPostalCallLog(_UserId, EntityId,dateFrom,dateTo);
        }
        public BE.FrontDesk.Transaction.PostalCallLog GetPostalCallLogById(int EntityId, int PostalCallLogId)
        {
            return db.getPostalCallLogById(_UserId, EntityId, PostalCallLogId);
        }
        public ResponeValues DeleteById(int EntityId, int PostalCallLogId)
        {
            return db.DeleteById(_UserId, EntityId, PostalCallLogId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.PostalCallLog beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                //Added contact validation
                var vMobile = base.IsValidContactNo(beData.Contact);

                if (vMobile.IsSuccess == false)
                {
                    return vMobile;
                }
                //Ends
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
                //else if (string.IsNullOrEmpty(beData.FirstName))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Name";
                //}
                //else if (bedata.studentid == 0)
                //{
                //    resval.responsemsg = "please ! select employee";
                //}

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
