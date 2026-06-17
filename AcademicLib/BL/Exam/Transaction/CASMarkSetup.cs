using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class CASMarkSetup
    {
        DA.Exam.Transaction.CASMarkSetupDB db = null;
        int _UserId = 0;

        public CASMarkSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.CASMarkSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.CASMarksSetup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
            {
                isValid = db.SaveUpdate(beData, isModify);

                if (isValid.IsSuccess)
                {
                    if(beData.ExamClassSubjectsColl!=null && beData.ExamClassSubjectsColl.Count > 0)
                    {
                        foreach(var be in beData.ExamClassSubjectsColl)
                        {
                            beData.ClassId = be.ClassId;
                            beData.ExamTypeId = be.ExamTypeId;
                            beData.SubjectId = be.SubjectId;
                            db.SaveUpdate(beData, isModify);
                        }
                    }
                }
            }                
            
            return isValid;
        }
        public BE.Exam.Transaction.CASMarksSetup GetMarksSetupByClassId(int ClassId, int? SectionId,int SubjectId, int ExamTypeId)
        {
            return db.getMarksSetupByClassId(_UserId, ClassId, SectionId,SubjectId, ExamTypeId);
        }       
        public AcademicLib.RE.Exam.CASMarkSetupStatusCollections GetMarkSetupStatus(int ExamTypeId,int ClassId)
        {
            return db.GetMarkSetupStatus(_UserId, ExamTypeId,ClassId);
        }
        public AcademicLib.BE.Exam.Transaction.ExamClassSubjectCollections getExamClassSubjectList(int? ClassId,int? AcademicYearId)
        {
            return db.getExamClassSubjectList(_UserId,ClassId,AcademicYearId);
        }
            public ResponeValues IsValidData(ref BE.Exam.Transaction.CASMarksSetup beData, bool IsModify)
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
                else if (beData.SubjectId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Subject Name";
                }
                else if (beData.MarksSetupDetailsColl == null || beData.MarksSetupDetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter CAS Details";
                }
                else
                {
                    double fm = beData.FullMark;
                    double cfm = beData.MarksSetupDetailsColl.Sum(p1 => p1.Mark);
                    if (fm != cfm)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Full mark does not match with sum of mark "+fm.ToString()+" <> "+cfm.ToString();
                        return resVal;
                    }

                    List<int> casIdColl = new List<int>();
                    foreach(var v in beData.MarksSetupDetailsColl)
                    {
                        if (casIdColl.Contains(v.CASTypeId))
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Duplicate CAS Type";
                            return resVal;
                        }else
                        {
                            casIdColl.Add(v.CASTypeId);
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
