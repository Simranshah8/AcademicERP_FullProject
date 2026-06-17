using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Transaction
{
    public class BookIssue
    {
        DA.Library.Transaction.BookIssueDB db = null;
        int _UserId = 0;
        public BookIssue(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Transaction.BookIssueDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Library.Transaction.BookIssue beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Transaction.BookIssueCollections GetAllBookIssue(int EntityId)
        {
            return null;
            //return db.getAllBookIssue(_UserId, EntityId);
        }
        public BE.Library.Transaction.BookIssue GetBookIssueById(int EntityId, int TranId)
        {
            return null;
            //return db.getBookIssueById(_UserId, EntityId, TranId);
        }
       
        public ResponeValues getBookIssueNo()
        {
            return db.getBookIssueNo(_UserId);
        }
        public RE.Library.BookDetailsCollections getBookDetailsList()
        {
            return db.getBookDetailsList(_UserId);
        }
        public BE.Library.Transaction.CreditRules getCreditRules( int? StudentId, int? EmployeeId)
        {
            return db.getCreditRules(_UserId, StudentId, EmployeeId);
        }
            public BE.Library.Transaction.PreviousBookCollections getPreviousBookDueList( int? StudentId, int? EmployeeId)
        {
            return db.getPreviousBookDueList(_UserId, StudentId, EmployeeId);
        }
            public ResponeValues IsValidData(ref BE.Library.Transaction.BookIssue beData, bool IsModify)
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
                else
                {
                 
                    if((!beData.StudentId.HasValue || beData.StudentId==0) && (!beData.EmployeeId.HasValue || beData.EmployeeId==0))
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Select Student Or Employee";
                        return resVal;
                    }

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
