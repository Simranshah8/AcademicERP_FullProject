using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class ClassYear
    {
        DA.Academic.Creation.ClassYearDB db = null;
        int _UserId = 0;
        public ClassYear(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.ClassYearDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.ClassYear beData)
        {
            bool isModify = beData.ClassYearId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.ClassYearCollections GetAllClassYear(int EntityId)
        {
            return db.getAllClassYear(_UserId, EntityId);
        }
        public BE.Academic.Creation.ClassYearCollections getAllClassYearForTran(int EntityId, int AcademicYearId)
        {
            return db.getAllClassYearForTran(_UserId, EntityId, AcademicYearId);
        }
        public BE.Academic.Creation.ClassYear GetClassYearById(int EntityId, int ClassYearId)
        {
            return db.getClassYearById(_UserId, EntityId, ClassYearId);
        }
        public ResponeValues DeleteById(int EntityId, int ClassYearId)
        {
            return db.DeleteById(_UserId, EntityId, ClassYearId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.ClassYear beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ClassYearId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ClassYearId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter ClassYear Name";
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
