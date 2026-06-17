using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class AdmissionProcedure
    {
        DA.AppCMS.Creation.AdmissionProcedureDB db = null;
        int _UserId = 0;

        public AdmissionProcedure(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.AdmissionProcedureDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.AdmissionProcedure beData)
        {
            bool isModify = beData.AdmissionProcedureId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.AdmissionProcedureCollections GetAllAdmissionProcedure(int EntityId, string BranchCode)
        {
            return db.getAllAdmissionProcedure(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.AdmissionProcedure GetAdmissionProcedureById(int EntityId, int AdmissionProcedureId)
        {
            return db.getAdmissionProcedureById(_UserId, EntityId, AdmissionProcedureId);
        }
        public ResponeValues DeleteById(int EntityId, int AdmissionProcedureId)
        {
            return db.DeleteById(_UserId, EntityId, AdmissionProcedureId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.AdmissionProcedure beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.AdmissionProcedureId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.AdmissionProcedureId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Title))
                {
                    resVal.ResponseMSG = "Please ! Enter Heading ";
                } 
                else if (string.IsNullOrEmpty(beData.Description))
                {
                    resVal.ResponseMSG = "Please ! Enter Description ";
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
