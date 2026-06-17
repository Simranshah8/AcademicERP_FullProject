using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Wallet
{
    public class OnlinePayment
    {
        DA.Wallet.OnlinePaymentDB db = null;
        int _UserId = 0;
        public OnlinePayment(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Wallet.OnlinePaymentDB(hostName, dbName);
        }
        public ResponeValues SavePaymentLog(BE.Wallet.OnlinePayment beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveWalletLog(beData);
            else
                return isValid;
          
        }
        public AcademicLib.BE.Wallet.PaymentGateWayCollections GetPaymentGateWays()
        {
            return db.GetPaymentGateWayList(_UserId);
        }
        public ResponeValues IsValidData(ref BE.Wallet.OnlinePayment beData )
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
                else if (string.IsNullOrEmpty(beData.MobileNo))
                {
                    resVal.ResponseMSG = "Please ! Enter Mobile Name";
                }
                else if (string.IsNullOrEmpty(beData.From))
                {
                    resVal.ResponseMSG = "Please ! Enter From Request Android/IOS/Web";
                }
                else if (string.IsNullOrEmpty(beData.ReferenceId))
                {
                    resVal.ResponseMSG = "Please ! Enter Reference Id";
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
