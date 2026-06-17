using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class PostalServices
    {
        DA.FrontDesk.Transaction.PostalServiceDB db = null;

        int _UserId = 0;
        public PostalServices(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.FrontDesk.Transaction.PostalServiceDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.PostalService beData)
        {
            bool isModify = beData.PostalServicesId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }


        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.PostalService beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.PostalServicesId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.PostalServicesId != 0)
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
        public BE.FrontDesk.Transaction.PostalServiceCollections GetAllReceivedList(DateTime Fromdate, DateTime Todate)
        {
            return db.GetAllReceivedList(_UserId,Fromdate,Todate);
        }

        public BE.FrontDesk.Transaction.PostalService GetPostalServiceById(int UserId, int PostalServicesId)
        {
            return db.getPostalServiceById(_UserId, PostalServicesId);
        }

        public ResponeValues DeleteById(int PostalServicesId)
        {
            return db.DeleteReceivedById(_UserId, PostalServicesId);
        }

    }
}