using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamSchedule
    {
        DA.Exam.Transaction.ExamScheduleDB db = null;
        int _UserId = 0;

        public ExamSchedule(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ExamScheduleDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.ExamSchedule beData)
        {
            bool isModify = beData.ExamScheduleId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.ExamScheduleCollections GetAllExamSchedule(int EntityId)
        {
            return null;
          //  return db.getAllExamSchedule(_UserId, EntityId);
        }
        public AcademicLib.RE.Exam.ExamScheduleStatusCollections GetExamScheduleStatus( int ExamTypeId)
        {
            return db.GetExamScheduleStatus(_UserId, ExamTypeId);
        }
            public AcademicLib.BE.Exam.Transaction.ExamSchedule getExamScheduleById( int EntityId, int ClassId, string SectionIdColl, int ExamTypeId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            return db.getExamScheduleById(_UserId, EntityId,ClassId,SectionIdColl,ExamTypeId, SemesterId, ClassYearId, BatchId);
        }
        public ResponeValues DeleteById(int EntityId, int ClassId, string SectionIdColl, int ExamTypeId)
        {
            return db.DeleteById(_UserId, EntityId, ClassId, SectionIdColl, ExamTypeId);
        }

        public AcademicLib.RE.Exam.ExamScheduleCollections GetExamSchedule(int AcademicYearId, int? ClassId, string SectionIdColl, int? ExamTypeId)
        {
            return db.GetExamSchedule(_UserId,AcademicYearId, ClassId, SectionIdColl, ExamTypeId);
        }
        public AcademicLib.RE.Exam.AllClassExamScheduleCollections GetAllClassExamSchedule( int ExamTypeId, bool SectionWise, bool InDetails = false)
        {
            return db.GetAllClassExamSchedule(_UserId, ExamTypeId, SectionWise,InDetails);
        }
            public ResponeValues IsValidData(ref BE.Exam.Transaction.ExamSchedule beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ExamScheduleId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ExamScheduleId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (!beData.StartDate.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter Start Date";
                }
                else if (!beData.EndDate.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter End Date";
                }
                else if (!beData.StartTime.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter Start Time";
                }
                else if (!beData.EndTime.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter End Time";
                }
                else
                {
                    
                    foreach(var v in beData.ExamScheduleDetailsColl)
                    {
                        if (!v.SubjectId.HasValue || v.SubjectId == 0)
                        {
                            resVal.ResponseMSG = "Please ! Select Valid Subject Name";
                            return resVal;
                        }

                        if (!v.ExamDate.HasValue)
                        {
                            resVal.ResponseMSG = "Please ! Enter Subject Exam Date";
                            return resVal;
                        }

                        if(v.ExamDate.Value<beData.StartDate.Value || v.ExamDate.Value > beData.EndDate.Value)
                        {
                            resVal.ResponseMSG = "Invalid Subject ExamDate";
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
