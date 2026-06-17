using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace AcademicLib.BL.Infirmary
{
    public class GeneralCheckup
    {
        DA.Infirmary.GeneralCheckupDB db = null;
        int _UserId = 0;
        public GeneralCheckup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Infirmary.GeneralCheckupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Infirmary.GeneralCheckup GeneralCheckup)
        {
            ResponeValues isValid = new ResponeValues();
            isValid.IsSuccess = true; isValid.ResponseMSG = "";
            int idx = 0;

            GeneralCheckup.CUserId = _UserId;
            GeneralCheckup.LogDateTime = DateTime.Now;

            return db.SaveGeneralCheckup(GeneralCheckup, _UserId);

        }

        public BE.Infirmary.GeneralCheckupCollections getAllGeneralCheckups() => db.getAllGeneralCheckups();

        public ResponeValues IsValidData(BE.Infirmary.GeneralCheckup data, int idx)
        {
            ResponeValues resVal = new ResponeValues();

            resVal.IsSuccess = true;
            resVal.ResponseMSG = "Valid";

            return resVal;
        }

        public BE.Infirmary.GeneralCheckup getGeneralCheckupById(int GeneralCheckupId) => db.getGeneralCheckupById(GeneralCheckupId);
        public ResponeValues deleteGeneralCheckupById(int GeneralCheckupId) => db.deleteGeneralCheckupById(GeneralCheckupId, _UserId);
    }
}