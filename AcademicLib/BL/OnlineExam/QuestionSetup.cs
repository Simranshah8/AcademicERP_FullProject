using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.OnlineExam
{
    public class QuestionSetup
    {
        DA.OnlineExam.QuestionSetupDB db = null;
        int _UserId = 0;

        public QuestionSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.OnlineExam.QuestionSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.OnlineExam.QuestionSetup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.OnlineExam.QuestionSetupCollections GetAllQuestionSetup(int EntityId)
        {
            return db.getAllQuestionSetup(_UserId, EntityId);
        }
        public BE.OnlineExam.QuestionSetup GetQuestionSetupById(int EntityId, int TranId)
        {
            return db.getQuestionSetupById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public BE.OnlineExam.QuestionSetupCollections getQuestionSetupByExamSetupId( int ExamSetupId, int CategoryId)
        {
            return db.getQuestionSetupByExamSetupId(_UserId, ExamSetupId, CategoryId);
        }
        public BE.OnlineExam.QuestionSetupCollections getQuestionSetupList( int ExamTypeId, int ClassId, string SectionIdColl, int SubjectId,int examSetupId)
        {
            return db.getQuestionSetupList(_UserId, ExamTypeId, ClassId, SectionIdColl, SubjectId,examSetupId);
        }
        public BE.OnlineExam.QuestionSetupCollections getQuestionListForAPI( int ExamSetupId,int? StudentId=null)
        {
            bool isTeacher = false;
            if (StudentId.HasValue && StudentId.Value > 0)
                isTeacher = true;

            return db.getQuestionListForAPI(_UserId, ExamSetupId,isTeacher,StudentId);
        }
        public ResponeValues ExamCopyCheck(AcademicLib.API.Teacher.ExamCopyCheck beData)
        {
            return db.ExamCopyCheck(beData);
        }
            public ResponeValues IsValidData(ref BE.OnlineExam.QuestionSetup beData, bool IsModify)
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
                else if (string.IsNullOrEmpty(beData.Question) && string.IsNullOrEmpty(beData.QuestionPath))
                {
                    resVal.ResponseMSG = "Please ! Enter Question";
                }
                else
                {
                    foreach(var b in beData.DetailsColl)
                    {
                        if (b.SNo == 0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Enter Answer S.No.";
                            return resVal;
                        }

                        if (b.IsRightAnswer)
                            beData.AnswerSNo = b.SNo;
                    }

                    if(beData.DetailsColl!=null && beData.DetailsColl.Count > 0 && beData.ExamModal==2)
                    {
                        if (beData.DetailsColl.Count(p1 => p1.IsRightAnswer == true && !string.IsNullOrEmpty(p1.Answer)) == 0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Select Correct Answer";
                            return resVal;
                        }

                        if (beData.DetailsColl.Count(p1 => p1.IsRightAnswer == true) > 1)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Select Any One Correct Answer";
                            return resVal;
                        }
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
