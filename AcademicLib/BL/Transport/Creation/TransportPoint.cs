using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Transport.Creation
{
    public class TransportPoint
    {
        DA.Transport.Creation.TransportPointDB db = null;
        int _UserId = 0;

        public TransportPoint(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Transport.Creation.TransportPointDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Transport.Creation.TransportPoint beData)
        {
            bool isModify = beData.PointId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Transport.Creation.TransportPointCollections GetAllTransportPoint(int EntityId)
        {
            return db.getAllTransportPoint(_UserId, EntityId);
        }
        public BE.Transport.Creation.TransportPoint GetTransportPointById(int EntityId, int PointId)
        {
            return db.getTransportPointById(_UserId, EntityId, PointId);
        }
        public ResponeValues DeleteById(int EntityId, int PointId)
        {
            return db.DeleteById(_UserId, EntityId, PointId);
        }
        public AcademicLib.API.Teacher.TransportRouteCollections getAllPickupPointsForMap()
        {
            return db.getAllPickupPointsForMap(_UserId);
        }
        public AcademicLib.API.Student.TransportPoint getPickupPoint()
        {
            return db.getPickupPoint(_UserId);
        }
        public AcademicLib.RE.Transport.MSGForPickupPointCollections getPickUpMSG(string pointIdColl,int msgFor, string nextPointIdColl, string pastPointIdColl)
        {
            return db.getPickUpMSG(_UserId, pointIdColl,msgFor, nextPointIdColl, pastPointIdColl);
        }
            public ResponeValues IsValidData(ref BE.Transport.Creation.TransportPoint beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.PointId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.PointId != 0)
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
                }else if(beData.RouteIdColl==null || beData.RouteIdColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Route Name";
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
