using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class StudentType
    {
        DA.Academic.Creation.StudentTypeDB db = null;
        int _UserId = 0;
        public StudentType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.StudentTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.StudentType beData)
        {
            bool isModify = beData.StudentTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.StudentTypeCollections GetAllStudentType(int EntityId)
        {
            return db.getAllStudentType(_UserId, EntityId);
        }
        public BE.Academic.Creation.StudentType GetStudentTypeById(int EntityId, int StudentTypeId)
        {
            return db.getStudentTypeById(_UserId, EntityId, StudentTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int StudentTypeId)
        {
            return db.DeleteById(_UserId, EntityId, StudentTypeId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.StudentType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.StudentTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.StudentTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter StudentType Name";
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
