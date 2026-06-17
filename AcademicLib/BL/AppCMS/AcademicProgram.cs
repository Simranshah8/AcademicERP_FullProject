using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class AcademicProgram
    {
        DA.AppCMS.Creation.AcademicProgramDB db = null;
        int _UserId = 0;

        public AcademicProgram(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.AcademicProgramDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.AcademicProgram beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.AcademicProgramCollections GetAllAcademicProgram(int EntityId, string BranchCode)
        {
            return db.getAllAcademicProgram(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.AcademicProgram GetAcademicProgramById(int EntityId, int TranId)
        {
            return db.getAcademicProgramById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.AcademicProgram beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
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
