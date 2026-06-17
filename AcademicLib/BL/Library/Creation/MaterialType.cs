using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class MaterialType
    {
        DA.Library.Creation.MaterialTypeDB db = null;
        int _UserId = 0;
        public MaterialType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.MaterialTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Creation.MaterialType beData)
        {
            bool isModify = beData.MaterialTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Creation.MaterialTypeCollections GetAllMaterialType(int EntityId)
        {
            return db.getAllMaterialType(_UserId, EntityId);
        }
        public BE.Library.Creation.MaterialType GetMaterialTypeById(int EntityId, int MaterialTypeId)
        {
            return db.getMaterialTypeById(_UserId, EntityId, MaterialTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int MaterialTypeId)
        {
            return db.DeleteById(_UserId, EntityId, MaterialTypeId);
        }
        public ResponeValues IsValidData(ref BE.Library.Creation.MaterialType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.MaterialTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.MaterialTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter MaterialType Name";
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
