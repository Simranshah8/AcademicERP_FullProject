using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class ExtraCertificateIssue
    {
        DA.Academic.Transaction.ExtraCertificateIssueDB db = null;
        int _UserId = 0;
        public ExtraCertificateIssue(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.ExtraCertificateIssueDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.ExtraCertificateIssue beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }

        public ResponeValues getAutoNo(int ExtraEntityId,int? AcademicYearId)
        {
            return db.getAutoNo(_UserId, ExtraEntityId, AcademicYearId);
        }
            public BE.Academic.Transaction.ExtraCertificateIssueCollections GetAllExtraCertificateIssue(int EntityId)
        {
            return db.getAllExtraCertificateIssue(_UserId, EntityId);
        }

        public BE.Academic.Transaction.ExtraCertificateIssue GetExtraCertificateIssueById(int EntityId, int TranId)
        {
            return db.getExtraCertificateIssueById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.ExtraCertificateIssue beData, bool IsModify)
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
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter ExtraCertificateIssue Name";
                //}
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