using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class VisionStatement
    {
        DA.AppCMS.Creation.VisionStatementDB db = null;
        int _UserId = 0;

        public VisionStatement(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.VisionStatementDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.VisionStatement beData)
        {
            bool isModify = beData.VisionStatementId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.VisionStatementCollections GetAllVisionStatement(int EntityId, string BranchCode)
        {
            return db.getAllVisionStatement(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.VisionStatement GetVisionStatementById(int EntityId, int VisionStatementId)
        {
            return db.getVisionStatementById(_UserId, EntityId, VisionStatementId);
        }
        public ResponeValues DeleteById(int EntityId, int VisionStatementId)
        {
            return db.DeleteById(_UserId, EntityId, VisionStatementId);
        }
        public AcademicLib.API.AppCMS.Introduction getIntroduction(string BranchCode)
        {
            return db.getIntroduction(_UserId,BranchCode);
        }
            public ResponeValues IsValidData(ref BE.AppCMS.Creation.VisionStatement beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.VisionStatementId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.VisionStatementId != 0)
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
