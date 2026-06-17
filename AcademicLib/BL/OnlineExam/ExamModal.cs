using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.OnlineExam
{
    public class ExamModal
    {
        DA.OnlineExam.ExamModalDB db = null;
        int _UserId = 0;

        public ExamModal(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.OnlineExam.ExamModalDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.OnlineExam.ExamModal beData)
        {
            bool isModify =  beData.CategoryId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.OnlineExam.ExamModalCollections GetAllExamModal(int EntityId)
        {
            return db.getAllExamModal(_UserId, EntityId);
        }
        public BE.OnlineExam.ExamModal GetExamModalById(int EntityId, int CategoryId)
        {
            return db.getExamModalById(_UserId, EntityId, CategoryId);
        }
        public ResponeValues DeleteById(int EntityId, int CategoryId)
        {
            return db.DeleteById(_UserId, EntityId, CategoryId);
        }
        public ResponeValues IsValidData(ref BE.OnlineExam.ExamModal beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.CategoryId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.CategoryId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.CategoryName))
                {
                    resVal.ResponseMSG = "Please ! Enter Category Name";
                }
                else
                {
                    if (!beData.NumberingMethod.HasValue || beData.NumberingMethod.Value == 0)
                        beData.NumberingMethod = 2;

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
