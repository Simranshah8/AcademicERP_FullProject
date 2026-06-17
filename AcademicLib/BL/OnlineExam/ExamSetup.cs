using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.OnlineExam
{
    public class ExamSetup
    {
        DA.OnlineExam.ExamSetupDB db = null;
        int _UserId = 0;

        public ExamSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.OnlineExam.ExamSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.OnlineExam.ExamSetup beData)
        {
            bool isModify = beData.ExamSetupId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public RE.OnlineExam.ExamSetupCollections GetAllExamSetup(int EntityId)
        {
            return db.getAllExamSetup(_UserId, EntityId);
        }
        public BE.OnlineExam.ExamSetup GetExamSetupById(int EntityId, int ExamSetupId)
        {
            return db.getExamSetupById(_UserId, EntityId, ExamSetupId);
        }
        public ResponeValues DeleteById(int EntityId, int ExamSetupId)
        {
            return db.DeleteById(_UserId, EntityId, ExamSetupId);
        }
        public List<BE.OnlineExam.ExamSetupQuestionModel> getExamSetupDetails(int ExamTypeId, int ClassId, string SectionIdColl, int SubjectId)
        {
            return db.getExamSetupDetails(_UserId, ExamTypeId, ClassId, SectionIdColl, SubjectId);
        }
        public RE.OnlineExam.ExamListCollections getExamList( int? ForType)
        {
            return db.getExamList(_UserId, ForType);
        }
        public API.OnlineExam.StudentForEvaluateCollections getStudentForEvaluate( int examSetupId,int classId,int? sectionId)
        {
            return db.getStudentForEvaluate(_UserId, examSetupId,classId,sectionId);
        }
            public RE.OnlineExam.ExamListCollections getExamListForEvaluate(DateTime dateFrom,DateTime dateTo,int examTypeId,int classId,int? sectionId,int subjectId)
        {
            return db.getExamListForEvaluate(_UserId, dateFrom,dateTo,examTypeId,classId,sectionId,subjectId);
        }
        public RE.OnlineExam.ExamListCollections getExamListForPreStudent()
        {
            return db.getExamListForPreStudent(_UserId);
        }
            public RE.OnlineExam.ExamList getExamSetupByIdForAPI(int ExamSetupId)
        {
            return db.getExamSetupByIdForAPI(_UserId, ExamSetupId);
        }
        public ResponeValues IsValidData(ref BE.OnlineExam.ExamSetup beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ExamSetupId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ExamSetupId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (beData.ExamTypeId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid ExamType Name";
                }

                else if (beData.ClassId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
                }

                else if (beData.SubjectId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Subject Name";
                }
                else if (string.IsNullOrEmpty(beData.Instruction))
                {
                    resVal.ResponseMSG = "Please ! Enter Exam Instruction Details";
                }
                else if (string.IsNullOrEmpty(beData.Lesson))
                {
                    resVal.ResponseMSG = "Please ! Enter Exam Lession Details";
                }
                else if (!beData.ExamDate.HasValue )
                {
                    resVal.ResponseMSG = "Please ! Enter Valid Exam Date";
                }else if (!beData.StartTime.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter Exam Start Time";
                }
                else
                {
                    var ed = beData.ExamDate.Value;
                    beData.StartTime = new DateTime(ed.Year, ed.Month, ed.Day, beData.StartTime.Value.Hour, beData.StartTime.Value.Minute, beData.StartTime.Value.Second);

                    if (beData.FullMarks < beData.PassMarks)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Full Mark Less Than Pass Mark";
                        return resVal;
                    }

                    if(beData.ExamDate.HasValue && beData.ResultDate.HasValue)
                    {
                        if (beData.ExamDate.Value > beData.ResultDate.Value)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Enter Result Date Greater Than Exam Date";
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
