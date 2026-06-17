using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace AcademicLib.BL.Infirmary
{
    public class HealthCampaign
    {
        DA.Infirmary.HealthCampaignDB db = null;
        int _UserId = 0;
        public HealthCampaign(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Infirmary.HealthCampaignDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Infirmary.HealthCampaign healthCampaign)
        {
            ResponeValues isValid = new ResponeValues();
            isValid.IsSuccess = true; isValid.ResponseMSG = "";
            int idx = 0;


            // step 2
            healthCampaign.CUserId = _UserId;
            healthCampaign.LogDateTime = DateTime.Now;

            if (healthCampaign.HealthCampaignId == null)
            {
                healthCampaign.CUserId = _UserId;
                healthCampaign.LogDateTime = DateTime.Now;
            }

            return db.SaveHealthCampaign(healthCampaign, _UserId);

        }

        public BE.Infirmary.HealthCampaignCollections getAllHealthCampaigns() => db.getAllHealthCampaigns();

        public ResponeValues IsValidData(BE.Infirmary.HealthCampaign data, int idx)
        {
            ResponeValues resVal = new ResponeValues();

            resVal.IsSuccess = true;
            resVal.ResponseMSG = "Valid";

            return resVal;
        }

        public BE.Infirmary.HealthCampaign getHealthCampaignById(int healthCampaignId) => db.getHealthCampaignById(healthCampaignId);
        public ResponeValues deleteHealthCampaignById(int healthCampaignId) => db.deleteHealthCampaignById(healthCampaignId, _UserId);

        public BE.Infirmary.ClassSectionCollections getAllClassSection() => db.getAllClassSection();
    }
}