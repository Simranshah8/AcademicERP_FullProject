using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class TC
    {
        DA.Academic.Transaction.TCDB db = null;
        int _UserId = 0;
        public TC(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.TCDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.TC beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues Request(BE.Academic.Transaction.TCCCRequest beData)
        {
            ResponeValues resVal = new ResponeValues();
            if(!beData.DOB.HasValue && string.IsNullOrEmpty(beData.ContactNo) && !beData.StudentId.HasValue)
            {
                resVal.ResponseMSG = "Please ! Enter DOB or Contact No.";
            }
            else if(string.IsNullOrEmpty(beData.RegdNo) && !beData.StudentId.HasValue)
            {
                resVal.ResponseMSG = "Please ! Enter Reg. No.";
            }
            else
            {
                resVal=db.Request(beData);
            }

            return resVal;
        }
        public RE.Academic.TCCCRequestCollections getAllRequest(int? AcademicYearId, int? ReportType)
        {
            return db.getAllRequest(_UserId, AcademicYearId, ReportType);
        }
            public BE.Academic.Transaction.TC getStudentDetailsForTCCC(int StudentId,int AcademicYearId)
        {
            return db.getStudentDetailsForTCCC(_UserId, StudentId,AcademicYearId);
        }
        public BE.Academic.Transaction.TCCollections getAllTC()
        {
            return db.getAllTC(_UserId);
        }
            public ResponeValues DeleteById(int EntityId, int TCId)
        {
            return db.DeleteById(_UserId, EntityId, TCId);
        }
        public BE.Academic.Transaction.TC getTCByStudentId( int StudentId, int? AcademicYearId)
        {
            return db.getTCByStudentId(_UserId, StudentId, AcademicYearId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.TC beData, bool IsModify)
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
                else if (string.IsNullOrEmpty(beData.FirstName))
                {
                    resVal.ResponseMSG = "Please ! Enter First Name";
                }
                else if (string.IsNullOrEmpty(beData.LastName))
                {
                    resVal.ResponseMSG = "Please ! Enter Last Name";
                }
                else if (string.IsNullOrEmpty(beData.FullAddress))
                {
                    resVal.ResponseMSG = "Please ! Enter Full Address";
                }
                else if (string.IsNullOrEmpty(beData.FatherName))
                {
                    resVal.ResponseMSG = "Please ! Enter Father Name";
                }
                else if(!beData.IssueDate.HasValue || beData.IssueDate.Value.Year < 1900)
                {
                    resVal.ResponseMSG = "Please ! Enter Issue Date";
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
