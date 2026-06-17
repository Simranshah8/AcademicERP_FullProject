using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class CommentSetup
    {
        DA.Exam.Transaction.CommentSetupDB db = null;
        int _UserId = 0;

        public CommentSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.CommentSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.CommentSetup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.CommentSetupCollections GetAllCommentSetup(int EntityId)
        {
            return db.getAllCommentSetup(_UserId, EntityId);
        }
        public BE.Exam.Transaction.CommentSetup GetCommentSetupById(int EntityId, int TranId)
        {
            return db.getCommentSetupById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.CommentSetup beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }                
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Comment))
                {
                    resVal.ResponseMSG = "Please ! Enter Comment Details";
                }             
                else
                {
                    if (beData.Wise == 4)
                    {
                        beData.MinVal = 0;
                        beData.MaxVal = 0;
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
