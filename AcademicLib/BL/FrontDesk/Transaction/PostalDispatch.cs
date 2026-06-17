using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class PostalDispatch
    {
        AcademicLib.DA.FrontDesk.Transaction.PostalDispatchDB db = null;

        int _UserId = 0;

        public PostalDispatch(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.FrontDesk.Transaction.PostalDispatchDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.PostalDispatch beData)
        {
            bool isModify = beData.PostalDispatchId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }

        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.PostalDispatch beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.PostalDispatchId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.PostalDispatchId != 0)
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

        public BE.FrontDesk.Transaction.PostalDispatchCollections GetAllDispatchList(/*DateTime dateFrom, DateTime dateTo*/)
        {
            return db.GetAllDispatchList(_UserId/*, dateFrom, dateTo*/);
        }

        public BE.FrontDesk.Transaction.PostalDispatch GetDispatchById(int UserId, int PostalDispatchId)
        {
            return db.getPostalDispatchById(_UserId, PostalDispatchId);
        }

        public ResponeValues DeleteById(int PostalDispatchId)
        {
            return db.DeleteDispatchById(_UserId, PostalDispatchId);
        }


    }
}
