using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicERP.BL.Health.Transaction
{
    public class HealthCampaign
    {
        DA.Health.Transaction.HealthCampaignDB db = null;
        int _UserId = 0;
        public HealthCampaign(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Health.Transaction.HealthCampaignDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Health.Transaction.HealthCampaign beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Health.Transaction.HealthCampaignCollections GetAllHealthCampaign(int EntityId)
        {
            return db.getAllHealthCampaign(_UserId, EntityId);
        }

        public BE.Health.Transaction.HealthCampaign GetHealthCampaignById(int EntityId, int TranId)
        {
            return db.getHealthCampaignById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public AcademicLib.RE.Health.HealthReport getHealthReport(int? StudentId, int? EmployeeId)
        {
            return db.getHealthReport(_UserId, StudentId, EmployeeId);
        }
            public ResponeValues IsValidData(ref BE.Health.Transaction.HealthCampaign beData, bool IsModify)
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
                else if (string.IsNullOrEmpty(beData.CampaignName))
                {
                    resVal.ResponseMSG = "Please ! Enter HealthCampaign Name";
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