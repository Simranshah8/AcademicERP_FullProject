using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Creation
{
    public class ExamTypeWiseTemplate
    {
        DA.Exam.Creation.ExamTypeWiseTemplateDB db = null;
        int _UserId = 0;
        public ExamTypeWiseTemplate(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Creation.ExamTypeWiseTemplateDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Creation.ExamTypeWiseTemplateCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, beData);
            else
                return isValid;
        } 
        public BE.Exam.Creation.ExamTypeWiseTemplateCollections getExamTypeWiseTemplate(int? ExamTypeId, int? ExamTypeGroupId)
        {
            return db.getExamTypeWiseTemplate(_UserId, ExamTypeId,ExamTypeGroupId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Creation.ExamTypeWiseTemplateCollections beData)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
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

        //TODO: DelExamTypeWiseTemplate
        public ResponeValues DelExamTypeWiseTemplate(int? ExamTypeId, int? ExamTypeGroupId)
        {
            return db.DelExamTypeWiseTemplate(_UserId, ExamTypeId, ExamTypeGroupId);
        }
    }
}
