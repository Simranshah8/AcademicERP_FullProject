using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Creation
{
    public class ExamTypeGroup
    {
        DA.Exam.Creation.ExamTypeGroupDB db = null;
        int _UserId = 0;

        public ExamTypeGroup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Creation.ExamTypeGroupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Exam.Creation.ExamTypeGroup beData)
        {
            bool isModify = beData.ExamTypeGroupId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Creation.ExamTypeGroupCollections GetAllExamTypeGroup(int AcademicYearId, int EntityId)
        {
            return db.getAllExamTypeGroup(_UserId,AcademicYearId, EntityId);
        }
        public BE.Exam.Creation.ExamTypeGroup GetExamTypeGroupById(int EntityId, int ExamTypeGroupId)
        {
            return db.getExamTypeGroupById(_UserId, EntityId, ExamTypeGroupId);
        }
        public ResponeValues DeleteById(int EntityId, int ExamTypeGroupId)
        {
            return db.DeleteById(_UserId, EntityId, ExamTypeGroupId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Creation.ExamTypeGroup beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ExamTypeGroupId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ExamTypeGroupId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Name";
                }
                else if (string.IsNullOrEmpty(beData.DisplayName))
                {
                    resVal.ResponseMSG = "Please ! Enter Display Name ";
                }else if(beData.ExamTypeGroupDetailsColl==null || beData.ExamTypeGroupDetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Exam Details";
                }
                else
                {
                    if (beData.CurrentExamTypeId.HasValue && beData.CurrentExamTypeId.Value == 0)
                        beData.CurrentExamTypeId = null;

                    if (beData.ResultDate.HasValue && beData.ResultTime.HasValue)
                    {
                        var dt = beData.ResultDate.Value;
                        var t = beData.ResultTime.Value;
                        beData.ResultDate = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                        beData.ResultTime = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                    }

                    int sno = 1;
                    if (beData.ExamTypeGroupDetailsColl != null)
                    {
                        foreach(var v in beData.ExamTypeGroupDetailsColl)
                        {
                            if (v.ExamTypeId == 0)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please ! Select Valid ExamType Name";
                                return resVal;
                            }

                            v.SNO = sno;
                            sno++;
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
