using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Transport.Creation
{
    public class TransportRoute
    {
        DA.Transport.Creation.TransportRouteDB db = null;
        int _UserId = 0;

        public TransportRoute(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Transport.Creation.TransportRouteDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Transport.Creation.TransportRoute beData)
        {
            bool isModify = beData.RouteId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Transport.Creation.TransportRouteCollections GetAllTransportRoute(int EntityId)
        {
            return db.getAllTransportRoute(_UserId, EntityId);
        }
        public BE.Transport.Creation.TransportRoute GetTransportRouteById(int EntityId, int RouteId)
        {
            return db.getTransportRouteById(_UserId, EntityId, RouteId);
        }
        public AcademicLib.BE.Transport.Creation.TransportRouteCollections getTransportRouteByVehicleId(int VehicleId)
        {
            return db.getTransportRouteByVehicleId(_UserId, VehicleId);
        }
            public ResponeValues DeleteById(int EntityId, int RouteId)
        {
            return db.DeleteById(_UserId, EntityId, RouteId);
        }
        public ResponeValues IsValidData(ref BE.Transport.Creation.TransportRoute beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.RouteId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.RouteId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Select Logo";
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
