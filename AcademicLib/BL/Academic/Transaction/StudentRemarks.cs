using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class StudentRemarks
    {

        DA.Academic.Transaction.StudentRemarksDB db = null;
        int _UserId = 0;

        public StudentRemarks(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.StudentRemarksDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.StudentRemarks beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues SaveUpdate(List<BE.Academic.Transaction.StudentRemarks> dataColl)
        {
            return db.SaveUpdate(_UserId, dataColl);
        }
            public BE.Academic.Transaction.StudentRemarksCollections getRemarks(int StudentId)
        {
            return db.getRemarks(_UserId, StudentId);
        }
        public AcademicLib.RE.Academic.StudentRemarksCollections getRemarksList(int AcademicYearId, DateTime dateFrom, DateTime dateTo, int? remarksTypeId, int? studentId,bool IsStudentUser=false, int? remarksFor=null)
        {
            return db.getRemarksList(_UserId,AcademicYearId, dateFrom, dateTo, remarksTypeId,studentId,IsStudentUser,remarksFor);
        }

            public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.StudentRemarks beData, bool IsModify)
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
                else if(string.IsNullOrEmpty(beData.Remarks))
                {
                    resVal.ResponseMSG = "Please ! Enter Remarks";
                }
                else if (beData.RemarksTypeId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Remark Type";
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
