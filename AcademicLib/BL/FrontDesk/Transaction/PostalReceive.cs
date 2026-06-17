using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class PostalReceive
    {
        DA.FrontDesk.Transaction.PostalReceiveDB db = null;
        int _UserId = 0;

        public PostalReceive(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.PostalReceiveDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.PostalReceive beData)
        {
            bool isModify = beData.PostalReceiveId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.PostalReceiveCollections GetAllPostalReceive(int EntityId)
        {
            return db.getAllPostalReceive(_UserId, EntityId);
        }
        public BE.FrontDesk.Transaction.PostalReceive GetPostalReceiveById(int EntityId, int PostalReceiveId)
        {
            return db.getPostalReceiveById(_UserId, EntityId, PostalReceiveId);
        }
        public ResponeValues DeleteById(int EntityId, int PostalReceiveId)
        {
            return db.DeleteById(_UserId, EntityId, PostalReceiveId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.PostalReceive beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.PostalReceiveId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.PostalReceiveId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.FormTitle))
                //{
                //    resVal.ResponseMSG = "Please ! Enter FormTitle";
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
