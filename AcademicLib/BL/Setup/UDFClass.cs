using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class UDFClassBL
    {
        DA.Setup.UDFClassDB db = null;
        int _UserId = 0;
        public UDFClassBL(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.UDFClassDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(UDFClass beData)
        {
            bool isModify = beData.Id > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public UDFClassCollections GetAllUDFClass(int EntityId)
        {
            return db.getAllUDFClass(_UserId, EntityId);
        }
        public UDFClass GetUDFClassById(int EntityId, int UDFClassId)
        {
            return db.getUDFClassById(_UserId, EntityId, UDFClassId);
        }
        public ResponeValues DeleteById(int EntityId, int UDFClassId)
        {
            return db.DeleteById(_UserId, EntityId, UDFClassId);
        }
        public ResponeValues IsValidData(ref UDFClass beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.Id == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.Id != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter UDFClass Name";
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
