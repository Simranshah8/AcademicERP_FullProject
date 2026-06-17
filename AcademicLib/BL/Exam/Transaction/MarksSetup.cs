using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class MarksSetup
    {
        DA.Exam.Transaction.MarksSetupDB db = null;
        int _UserId = 0;

        public MarksSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.MarksSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.MarksSetup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.MarksSetup GetMarksSetupByClassId(int ClassId, string SectionIdColl, int ExamTypeId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            return db.getMarksSetupByClassId(_UserId, ClassId,SectionIdColl,ExamTypeId,SemesterId,ClassYearId,BatchId);
        }
        public BE.Exam.Transaction.MarksSetup GetMarksSetupById(int EntityId, int MarksSetupId)
        {
            return db.getMarksSetupById(_UserId, EntityId, MarksSetupId);
        }
        public ResponeValues DeleteById(int EntityId, int MarksSetupId)
        {
            return db.DeleteById(_UserId, EntityId, MarksSetupId);
        }
        public AcademicLib.RE.Exam.MarkSetupStatusCollections GetMarkSetupStatus(int ExamTypeId,int? BranchId=null)
        {
            return db.GetMarkSetupStatus(_UserId, ExamTypeId,BranchId);
        }
        public ResponeValues Transfor(int FromExamTypeId, int ToExamTypeId, int? BranchId = null)
        {
            return db.Transfor(_UserId, FromExamTypeId, ToExamTypeId,BranchId);
        }
        public ResponeValues ImportMarkSetup( List<BE.Exam.Transaction.ImportMarkSetup> dataColl)
        {
            return db.ImportMarkSetup(_UserId, dataColl);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.MarksSetup beData, bool IsModify)
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
                }else if (beData.ClassId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
                }else if(beData.ExamTypeId==0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid ExamType Name";
                }else if(beData.MarksSetupDetailsColl==null || beData.MarksSetupDetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Suject Details";
                }
                else
                {
                    if (string.IsNullOrEmpty(beData.SectionIdColl))
                        beData.SectionIdColl = "";

                    if (beData.IsAutoSum)
                    {
                        beData.FullMark = beData.MarksSetupDetailsColl.Sum(p1 => p1.FMTH + p1.FMPR);
                        beData.PassMark = beData.MarksSetupDetailsColl.Sum(p1 => p1.PMTH + p1.PMPR);
                    }

                    List<int> subIdCOll = new List<int>();
                    int r = 1;
                    foreach(var q in beData.MarksSetupDetailsColl)
                    {
                        if (q.FMTH < q.PMTH)
                        {
                            resVal.ResponseMSG = "Please ! Enter Pass Mark Less Then Equal Full Mark";
                            return resVal;
                        }
                        else if (q.FMPR < q.PMPR)
                        {
                            resVal.ResponseMSG = "Please ! Enter Pass Mark Less Then Equal Full Mark";
                            return resVal;
                        }

                        if (q.OTH == 0)
                            q.OTH = 1;

                        if (q.OPR == 0)
                            q.OPR = 1;

                        if (!subIdCOll.Contains(q.SubjectId))
                            subIdCOll.Add(q.SubjectId);
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Duplicate Subject at row "+r.ToString();
                            return resVal;
                        }
                        r++;
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
