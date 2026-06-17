using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Setup
{
    public class BillConfiguration
    {
        DA.Fee.Setup.BillConfigurationDB db = null;
        int _UserId = 0;

        public BillConfiguration(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Setup.BillConfigurationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Fee.Setup.BillConfiguration beData)
        {
            return db.SaveUpdate(beData);
        }    
        public BE.Fee.Setup.BillConfiguration GetBillConfigurationById(int EntityId)
        {
            return db.getBillConfigurationById(_UserId, EntityId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Setup.BillConfiguration beData, bool IsModify)
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
                //else if (string.IsNullOrEmpty(beData.Logo))
                //{
                //    resVal.ResponseMSG = "Please ! Select Logo";
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
