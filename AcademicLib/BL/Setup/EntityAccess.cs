using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class EntityAccess
    {
        DA.Setup.EntityAccessDB db = null;
        int _UserId = 0;

        public EntityAccess(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.EntityAccessDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Setup.EntityAccessCollections dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, dataColl);
            else
                return isValid;
        }
        public BE.Setup.EntityAccessCollections getEntityAccessList(int? UserId,int? GroupId)
        {
            return db.getEntityAccessList(_UserId,UserId,GroupId);
        }
        public BE.Setup.EntityAccessCollections getEntityAccessList(int UserId)
        {
            return db.getEntityAccessList(_UserId, UserId, null);
        }
        public ResponeValues checkEntity( int EntityId, int Action)
        {
            return db.checkEntity(_UserId, EntityId, Action);
        }
        public API.Admin.LastLoginLogCollections getLoginLog( int? ForUserId, string ForUser, DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getLoginLog(_UserId, ForUserId, ForUser, dateFrom, dateTo);
        }
            public ResponeValues IsValidData(ref BE.Setup.EntityAccessCollections dataColl)
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
