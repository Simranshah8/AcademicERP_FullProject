using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Transport.Creation
{
    public class Vehicle
    {
         DA.Transport.Creation.VehicleDB db = null;
        int _UserId = 0;

        public Vehicle(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Transport.Creation.VehicleDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Transport.Creation.Vehicle beData)
        {
            bool isModify = beData.VehicleId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Transport.Creation.VehicleCollections GetAllVehicle(int EntityId)
        {
            return db.getAllVehicle(_UserId, EntityId);
        }
        public BE.Transport.Creation.Vehicle GetVehicleById(int EntityId, int VehicleId)
        {
            return db.getVehicleById(_UserId, EntityId, VehicleId);
        }
        public ResponeValues DeleteById(int EntityId, int VehicleId)
        {
            return db.DeleteById(_UserId, EntityId, VehicleId);
        }
        public AcademicLib.API.Admin.VehicleDetailCollections admin_VehicleList()
        {
            return db.admin_VehicleList(_UserId);
        }
            public ResponeValues IsValidData(ref BE.Transport.Creation.Vehicle beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.VehicleId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.VehicleId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
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
