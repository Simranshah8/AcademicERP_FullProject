using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Wallet
{
    public class PaymentGateway
    {
        DA.Wallet.PaymentGatewayDB db = null;
        int _UserId = 0;
        public PaymentGateway(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Wallet.PaymentGatewayDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Wallet.PaymentGateway beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues SavePaymentGatewayReturnURL(int ForGateWay, string ResponseLog)
        {
            return db.SavePaymentGatewayReturnURL(ForGateWay, ResponseLog);
        }
            public BE.Wallet.PaymentGatewayCollections GetAllPaymentGateway(int EntityId)
        {
            return db.getAllPaymentGateway(_UserId, EntityId);
        }
        
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Wallet.PaymentGateway beData, bool IsModify)
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
    }
}
