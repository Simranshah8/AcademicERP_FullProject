using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamTypeWiseResultSetup
    {
        DA.Exam.Transaction.ExamTypeWiseResultSetupDB db = null;
        int _UserId = 0;

        public ExamTypeWiseResultSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ExamTypeWiseResultSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.ExamTypeWiseResultSetup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.ExamTypeWiseResultSetup GetExamTypeWiseResultSetupByClassId(int ClassId, string SectionIdColl, int ExamTypeId)
        {
            return db.getExamTypeWiseResultSetupByClassId(_UserId, ClassId, SectionIdColl, ExamTypeId);
        }
        public BE.Exam.Transaction.ExamTypeWiseResultSetup GetExamTypeWiseResultSetupById(int EntityId, int ExamTypeWiseResultSetupId)
        {
            return db.getExamTypeWiseResultSetupById(_UserId, EntityId, ExamTypeWiseResultSetupId);
        }
        public ResponeValues DeleteById(int EntityId, int ExamTypeWiseResultSetupId)
        {
            return db.DeleteById(_UserId, EntityId, ExamTypeWiseResultSetupId);
        }
       
        public ResponeValues IsValidData(ref BE.Exam.Transaction.ExamTypeWiseResultSetup beData, bool IsModify)
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
                else if (beData.ClassId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
                }
                else if (beData.ExamTypeId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid ExamType Name";
                }
                else if (beData.ExamTypeWiseResultSetupDetailsColl == null || beData.ExamTypeWiseResultSetupDetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Suject Details";
                }
                else
                {
                    if (string.IsNullOrEmpty(beData.SectionIdColl))
                        beData.SectionIdColl = "";
                     
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
