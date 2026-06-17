using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class ClassLevel
    {
        DA.Academic.Creation.ClassLevelDB db = null;
        int _UserId = 0;
        public ClassLevel(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.ClassLevelDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.ClassLevel beData)
        {
            bool isModify = beData.LevelId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.ClassLevelCollections GetAllClassLevel(int EntityId)
        {
            return db.getAllClassLevel(_UserId, EntityId);
        }
        public BE.Academic.Creation.ClassLevelCollections getAllClassLevelForTran(int EntityId, int AcademicYearId)
        {
            return db.getAllClassLevelForTran(_UserId, EntityId, AcademicYearId);
        }
        public BE.Academic.Creation.ClassLevel GetClassLevelById(int EntityId, int LevelId)
        {
            return db.getClassLevelById(_UserId, EntityId, LevelId);
        }
        public ResponeValues DeleteById(int EntityId, int LevelId)
        {
            return db.DeleteById(_UserId, EntityId, LevelId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.ClassLevel beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.LevelId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.LevelId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter ClassLevel Name";
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
