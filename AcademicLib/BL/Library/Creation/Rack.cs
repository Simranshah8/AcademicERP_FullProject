using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class Rack
    {
        DA.Library.Creation.RackDB db = null;
        int _UserId = 0;
        public Rack(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.RackDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Creation.Rack beData)
        {
            bool isModify = beData.RackId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Creation.RackCollections GetAllRack(int EntityId)
        {
            return db.getAllRack(_UserId, EntityId);
        }
        public BE.Library.Creation.Rack GetRackById(int EntityId, int RackId)
        {
            return db.getRackById(_UserId, EntityId, RackId);
        }
        public ResponeValues DeleteById(int EntityId, int RackId)
        {
            return db.DeleteById(_UserId, EntityId, RackId);
        }
        public ResponeValues IsValidData(ref BE.Library.Creation.Rack beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.RackId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.RackId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Rack Name";
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
