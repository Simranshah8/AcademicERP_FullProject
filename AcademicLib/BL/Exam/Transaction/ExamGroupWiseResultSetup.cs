using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamGroupWiseResultSetup
    {
        DA.Exam.Transaction.ExamGroupWiseResultSetupDB db = null;
        int _UserId = 0;

        public ExamGroupWiseResultSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ExamGroupWiseResultSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.ExamGroupWiseResultSetup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.ExamGroupWiseResultSetup GetExamGroupWiseResultSetupByClassId(int ClassId, string SectionIdColl, int ExamTypeGroupId)
        {
            return db.getExamGroupWiseResultSetupByClassId(_UserId, ClassId, SectionIdColl, ExamTypeGroupId);
        }
        public BE.Exam.Transaction.ExamGroupWiseResultSetup GetExamGroupWiseResultSetupById(int EntityId, int ExamGroupWiseResultSetupId)
        {
            return db.getExamGroupWiseResultSetupById(_UserId, EntityId, ExamGroupWiseResultSetupId);
        }
        public ResponeValues DeleteById(int EntityId, int ExamGroupWiseResultSetupId)
        {
            return db.DeleteById(_UserId, EntityId, ExamGroupWiseResultSetupId);
        }


        public ResponeValues DeleteExamTypeGroupWiseById(int EntityId, int ClassId,  int ExamTypeId, string SectionIdColl="")
        {
            return db.DeleteExamTypeGroupById(_UserId, EntityId, ClassId,  ExamTypeId, SectionIdColl);
        }

        public ResponeValues IsValidData(ref BE.Exam.Transaction.ExamGroupWiseResultSetup beData, bool IsModify)
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
                else if (beData.ExamTypeGroupId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid ExamType Name";
                }
                else if (beData.ExamGroupWiseResultSetupDetailsColl == null || beData.ExamGroupWiseResultSetupDetailsColl.Count == 0)
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
