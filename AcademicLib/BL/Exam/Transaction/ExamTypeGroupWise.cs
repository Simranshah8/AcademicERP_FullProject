using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamTypeGroupWise
    {
        DA.Exam.Transaction.ExamTypeGroupWiseDB db = null;
        int _UserId = 0;

        public ExamTypeGroupWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ExamTypeGroupWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.ExamTypeGroupWise beData)
        {
            bool isModify = beData.ExamTypeGroupWiseId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.ExamTypeGroupWiseCollections GetAllExamTypeGroupWise(int EntityId)
        {
            return db.getAllExamTypeGroupWise(_UserId, EntityId);
        }
        public BE.Exam.Transaction.ExamTypeGroupWise GetExamTypeGroupWiseById(int EntityId, int ExamTypeGroupWiseId)
        {
            return db.getExamTypeGroupWiseById(_UserId, EntityId, ExamTypeGroupWiseId);
        }
        public ResponeValues DeleteById(int EntityId, int ExamTypeGroupWiseId)
        {
            return db.DeleteById(_UserId, EntityId, ExamTypeGroupWiseId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.ExamTypeGroupWise beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ExamTypeGroupWiseId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ExamTypeGroupWiseId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Name";
                //}
                //else if (beData.ShiftId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Shift ";
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
