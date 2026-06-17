using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class CC
    {
        DA.Academic.Transaction.CCDB db = null;
        int _UserId = 0;
        public CC(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.CCDB(hostName, dbName);
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
        public BE.Academic.Transaction.TC getStudentDetailsForTCCC(int StudentId, int? AcademicYearId)
        {
            return db.getStudentDetailsForTCCC(_UserId, StudentId, AcademicYearId);
        }
        public BE.Academic.Transaction.TCCollections getAllCC()
        {
            return db.getAllCC(_UserId);
        }
        public BE.Academic.Transaction.TC getCCByStudentId(int StudentId, int? AcademicYearId)
        {
            return db.getCCByStudentId(_UserId, StudentId, AcademicYearId);
        }
        public ResponeValues DeleteById(int EntityId, int TCId)
        {
            return db.DeleteById(_UserId, EntityId, TCId);
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
                else if (string.IsNullOrEmpty(beData.FirstName))
                {
                    resVal.ResponseMSG = "Please ! Enter First Name";
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
                else if (!beData.IssueDate.HasValue || beData.IssueDate.Value.Year < 1900)
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
