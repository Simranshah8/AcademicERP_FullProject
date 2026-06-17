using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class ServiceType
    {
        DA.Academic.Creation.ServiceTypeDB db = null;
        int _UserId = 0;
        public ServiceType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.ServiceTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.ServiceType beData)
        {
            bool isModify = beData.ServiceTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.ServiceTypeCollections GetAllServiceType(int EntityId)
        {
            return db.getAllServiceType(_UserId, EntityId);
        }
        public BE.Academic.Creation.ServiceType GetServiceTypeById(int EntityId, int ServiceTypeId)
        {
            return db.getServiceTypeById(_UserId, EntityId, ServiceTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int ServiceTypeId)
        {
            return db.DeleteById(_UserId, EntityId, ServiceTypeId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.ServiceType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ServiceTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ServiceTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter ServiceType Name";
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
