using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FormEntity
{
    public class EntityFieldsAllow
    {
        DA.FormEntity.EntityFieldsAllowDB db = null;
        int _UserId = 0;

        public EntityFieldsAllow(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.FormEntity.EntityFieldsAllowDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FormEntity.EntityFieldsAllowCollections dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, dataColl);
            else
                return isValid;
        }
        public List<int> getAllowFields(int ForUserId,int EntityId)
        {
            List<int> tmpColl = new List<int>();
            tmpColl = db.getAllowFields(_UserId, ForUserId, EntityId);
            //if (ForUserId != 1)
            //{
            //    tmpColl= db.getAllowFields(_UserId, ForUserId, EntityId);
            //}

            return tmpColl;
        }
        public ResponeValues CheckAllowFields(  int? ForUserId, int EntityId, int FieldId)
        {
            return db.CheckAllowFields(_UserId, ForUserId, EntityId, FieldId);
        }
            public ResponeValues IsValidData(ref BE.FormEntity.EntityFieldsAllowCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (dataColl == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
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
