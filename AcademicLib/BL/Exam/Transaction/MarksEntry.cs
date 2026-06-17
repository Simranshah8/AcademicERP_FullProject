using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class MarksEntry
    {
        DA.Exam.Transaction.MarksEntryDB db = null;
        int _UserId = 0;

        public MarksEntry(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.MarksEntryDB(hostName, dbName);
        }
        public ResponeValues IsValidForMarkEntry(int ExamTypeId)
        {
            return db.IsValidForMarkEntry(_UserId, ExamTypeId);
        }
        public ResponeValues IsValidForReMarkEntry(int ExamTypeId,int ReExamTypeId)
        {
            return db.IsValidForReMarkEntry(_UserId, ExamTypeId,ReExamTypeId);
        }
        public ResponeValues SaveMarkEntry(AcademicLib.API.Teacher.MarkEntryCollections dataColl)
        {            
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveMarkEntry(_UserId,dataColl);
            else
                return isValid;
        }
        public ResponeValues SaveReMarkEntry(AcademicLib.API.Teacher.MarkEntryCollections dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveReMarkEntry(_UserId, dataColl);
            else
                return isValid;
        }
        public ResponeValues SaveExamWiseBlockMarkSheet(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId, BE.Exam.Transaction.ExamWiseBlockMarkSheetCollections dataColl)
        {
            return db.SaveExamWiseBlockMarkSheet(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId, dataColl);
        }
        public AcademicLib.BE.Exam.Transaction.ExamWiseBlockMarkSheetCollections getExamWiseBlockedMarksheet(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.getExamWiseBlockedMarksheet(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId);
        }
        public ResponeValues SaveExamGroupWiseBlockMarkSheet(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId, BE.Exam.Transaction.ExamWiseBlockMarkSheetCollections dataColl)
        {
            return db.SaveExamGroupWiseBlockMarkSheet(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId, dataColl);
        }
        public AcademicLib.BE.Exam.Transaction.ExamWiseBlockMarkSheetCollections getExamGroupWiseBlockedMarksheet(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.getExamGroupWiseBlockedMarksheet(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId);
        }
        public ResponeValues PublishedExamResult(int AcademicYearId, int ExamTypeId, string ClassIdColl, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            if (!string.IsNullOrEmpty(ClassIdColl) && ClassIdColl == "0")
                ClassIdColl = "";

            return db.PublishedExamResult(_UserId,AcademicYearId, ExamTypeId, ClassIdColl,BatchId,SemesterId,ClassYearId);
        }
        public ResponeValues PublishedGroupExamResult(int AcademicYearId, int ExamTypeGroupId,int? CurExamTypeId, string ClassIdColl)
        {
            if (!string.IsNullOrEmpty(ClassIdColl) && ClassIdColl == "0")
                ClassIdColl = "";

            if (CurExamTypeId.HasValue && CurExamTypeId.Value == 0)
                CurExamTypeId = null;

            return db.PublishedExamGroupResult(_UserId,AcademicYearId, ExamTypeGroupId,CurExamTypeId, ClassIdColl);
        }
        
        public AcademicLib.API.Teacher.StudentForMarkEntryCollections getStudentForMarkEntrySubWise(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId, int SubjectId, bool FilterSection, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null,int? BranchId=null)
        {
            return db.getStudentForMarkEntrySubWise(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId, SubjectId,FilterSection, SemesterId, ClassYearId, BatchId,BranchId);
        }
        public AcademicLib.API.Teacher.StudentForMarkEntryCollections getStudentForReMarkEntry(int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId, int SubjectId)
        {
            return db.getStudentForReMarkEntry(_UserId, ClassId, SectionId, ExamTypeId,ReExamTypeId, SubjectId);
        }
        public AcademicLib.API.Teacher.StudentForMarkEntryCollections getStudentForMarkEntry(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId,bool FilterSection, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null,int? BranchId=null)
        {            
            return db.getStudentForMarkEntry(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId,FilterSection,BatchId,ClassYearId,SemesterId,BranchId);
        }
        public ResponeValues SaveStudentWiseComment( AcademicLib.API.Teacher.StudentWiseCommentCollections dataColl)
        {
            return db.SaveStudentWiseComment(_UserId, dataColl);
        }
            public ResponeValues resetSubjectMapping(int AcademicYearId)
        {
            return db.resetSubjectMapping(_UserId, AcademicYearId);
        }
            public AcademicLib.API.Teacher.StudentForMarkEntryCollections getStudentForReMarkEntry(int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId)
        {
            return db.getStudentForReMarkEntry(_UserId, ClassId, SectionId, ExamTypeId,ReExamTypeId);
        }
        public AcademicLib.API.Teacher.StudentForMarkEntryCollections getStudentForStudentWiseMarkEntry(int AcademicYearId, int StudentId, int ExamTypeId)
        {
            return db.getStudentForStudentWiseMarkEntry(_UserId,AcademicYearId, StudentId, ExamTypeId);
        }
        public AcademicLib.API.Teacher.StudentForMarkEntryCollections getStudentForStudentWiseReMarkEntry(int StudentId, int ExamTypeId,int ReExamTypeId,int AcademicYearId)
        {
            return db.getStudentForStudentWiseReMarkEntry(_UserId, StudentId, ExamTypeId,ReExamTypeId,AcademicYearId);
        }
        public AcademicLib.RE.Exam.MarkSheetCollections getMarkSheetClassWise(int AcademicYearId, int? StudentId, int? ClassId, int? SectionId, int ExamTypeId,bool FilterSection,string classIdColl, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null,bool FromPublished=false,int? BranchId=null)
        {
            return db.getMarkSheetClassWise(_UserId,AcademicYearId, StudentId, ClassId, SectionId, ExamTypeId,FilterSection,classIdColl, BatchId, SemesterId ,ClassYearId,FromPublished,BranchId);
        }
        public AcademicLib.RE.Exam.MarkSheetCollections getReExamMarkSheetClassWise(int AcademicYearId, int? StudentId, int? ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId, bool FilterSection, string classIdColl,int? BranchId=null)
        {
            return db.getReExamMarkSheetClassWise(_UserId, AcademicYearId, StudentId, ClassId, SectionId, ExamTypeId,ReExamTypeId, FilterSection, classIdColl,BranchId);
        }
        public AcademicLib.RE.Exam.GroupMarkSheetCollections getGroupMarkSheetClassWise(int AcademicYearId,int? StudentId, int? ClassId, int? SectionId, int ExamTypeGroupId,bool FilterSection,int? CurExamTypeId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null, bool FromPublished = false,int? BranchId=null)
        {
            return db.getGroupMarkSheetClassWise(_UserId,AcademicYearId, StudentId, ClassId, SectionId, ExamTypeGroupId,FilterSection,CurExamTypeId,BatchId,SemesterId,ClassYearId,FromPublished,BranchId);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.MarksEntry beData)
        {
            bool isModify = beData.MarksEntryId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.MarksEntryCollections GetAllMarksEntry(int EntityId)
        {
            return db.getAllMarksEntry(_UserId, EntityId);
        }
        public BE.Exam.Transaction.MarksEntry GetMarksEntryById(int EntityId, int MarksEntryId)
        {
            return db.getMarksEntryById(_UserId, EntityId, MarksEntryId);
        }
        public ResponeValues DeleteById(int EntityId, int MarksEntryId)
        {
            return db.DeleteById(_UserId, EntityId, MarksEntryId);
        }
        public AcademicLib.RE.Exam.StudentForExamCollections getStudentListForReExam(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId,int? SubjectId)
        {
            return db.getStudentListForReExam(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId,ReExamTypeId,SubjectId);
        }
        public AcademicLib.RE.Exam.StudentForExamCollections getStudentListForExam(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId, int? SubjectId, bool FilterSection)
        {
            return db.getStudentListForExam(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId,SubjectId,FilterSection);
        }
        public AcademicLib.RE.Exam.MarkSheetCollections getExamResultSummary(int AcademicYearId, int ExamTypeId,int? BranchId=null)
        {
            return db.getExamResultSummary(_UserId,AcademicYearId, ExamTypeId,BranchId);
        }
        public AcademicLib.RE.Exam.TeacherWiseSubjectAnalysisCollections getTeacherWiseSubjectAnalysis(int AcademicYearId,int ExamTypeId,int ExamTypeGroupId,int? BranchId=null)
        {
            return db.getTeacherWiseSubjectAnalysis(_UserId,AcademicYearId, ExamTypeId,ExamTypeGroupId,BranchId);
        }
            public AcademicLib.RE.Exam.MarkSheetCollections getExamGroupResultSummary(int AcademicYearId, int ExamTypeGroupId,int? BranchId=null)
        {
            return db.getExamGroupResultSummary(_UserId,AcademicYearId, ExamTypeGroupId,BranchId);
        }
            public AcademicLib.RE.Exam.MarkSubmitCollections getMarkSubmit(int AcademicYearId, int ExamTypeId,int? BranchId=null)
        {
            return db.getMarkSubmit(_UserId,AcademicYearId, ExamTypeId,BranchId);
        }
        public AcademicLib.RE.Exam.StudentResultCollections getStudentResult(int StudentId,int AcademicYearId)
        {
            return db.getStudentResult(_UserId,AcademicYearId, StudentId);
        }
        public AcademicLib.API.Teacher.ExamWiseCommentCollections getStudentForTeacherComment(int UserId, int ClassId, int? SectionId, int? AcademicYearId, int ExamTypeId)
        {
            return db.getStudentForTeacherComment(_UserId, ClassId, SectionId, AcademicYearId, ExamTypeId);
        }
        public ResponeValue UpdateExamComment( AcademicLib.API.Teacher.ExamWiseCommentCollections dataColl)
        {
            return db.UpdateExamComment(_UserId, dataColl);
        }
            public AcademicLib.RE.Exam.StudentResultCollections getStudentGroupResult(int StudentId)
        {
            return db.getStudentGroupResult(_UserId, StudentId);
        }

        public ResponeValue IsValidForMarkSheetPrint( int? StudentId, int? ExamTypeId, int? ExamTypeGroupId, int? AcademicYearId)
        {
            return db.IsValidForMarkSheetPrint(_UserId, StudentId, ExamTypeId, ExamTypeGroupId, AcademicYearId);
        }
        public ResponeValue IsValidForAdmitCardPrint(int? StudentId, int? ExamTypeId, int? ExamTypeGroupId, int? AcademicYearId)
        {
            return db.IsValidForAdmitCardPrint(_UserId, StudentId, ExamTypeId, ExamTypeGroupId, AcademicYearId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.MarksEntry beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.MarksEntryId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.MarksEntryId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Name";
                //}
                //else if (beData.ShiftId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Shift ";
                //}

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
        public AcademicLib.API.Student.MarkSheetTemplate getMarkSheetTemplateTranId(int AcademicYearId)
        {
            return db.getMarkSheetTemplateTranId(_UserId,AcademicYearId);
        }

        public AcademicLib.API.Admin.ClassWiseTopCollections admin_ClassWiseTop(int ClassId, int ExamTypeId, int top)
        {
            return db.admin_ClassWiseTop(_UserId, ClassId, ExamTypeId, top);
        }
        public AcademicLib.API.Admin.SubjectWiseTopCollections admin_SubjectWiseTop(int ClassId, int? SectionId, int ExamTypeId, int top)
        {
            return db.admin_SubjectWiseTop(_UserId, ClassId, SectionId, ExamTypeId, top);
        }

        public AcademicLib.API.Admin.ClassWiseTopCollections admin_ExamWiseTop( int top,int? AcademicYearId)
        {
            return db.admin_ExamWiseTop(_UserId, top,AcademicYearId);
        }

        public AcademicLib.API.Admin.ExamWiseEvaluationCollections admin_ExamWiseEvaluation(int ExamTypeId)
        {
            return db.admin_ExamWiseEvaluation(_UserId, ExamTypeId);
        }
        public AcademicLib.API.Admin.ExamGradeWiseEvaluationCollections admin_ExamGradeWiseEvaluation(int ExamTypeId)
        {
            return db.admin_ExamGradeWiseEvaluation(_UserId, ExamTypeId);
        }
        public AcademicLib.API.Admin.ClassWiseEvaluationCollections admin_ClassWiseEvaluation( int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.admin_ClassWiseEvaluation(_UserId, ClassId, SectionId, ExamTypeId);
        }
        public AcademicLib.API.Admin.SubjectWiseEvaluationCollections admin_SubjectWiseEvaluation(  int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.admin_SubjectWiseEvaluation(_UserId, ClassId, SectionId, ExamTypeId);
        }
            public ResponeValues IsValidData(ref API.Teacher.MarkEntryCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (dataColl == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }               
                else
                {
                    foreach(var dc in dataColl)
                    {
                        if (string.IsNullOrEmpty(dc.ObtainMarkTH))
                            dc.ObtainMarkTH = "";

                        if (string.IsNullOrEmpty(dc.ObtainMarkPR))
                            dc.ObtainMarkPR = "";

                        //dc.ObtainMarkTH = dc.ObtainMarkTH.Trim().Replace("*", "").Replace("-","").Replace(".00","").Replace("#","").Trim();
                        //dc.ObtainMarkPR = dc.ObtainMarkPR.Trim().Replace("*", "").Replace("-", "").Replace(".00", "").Replace("#", "").Trim();

                        dc.ObtainMarkTH = dc.ObtainMarkTH.Trim().Replace("*", "").Replace("-","").Replace("#","").Trim();
                        dc.ObtainMarkPR = dc.ObtainMarkPR.Trim().Replace("*", "").Replace("-", "").Replace("#", "").Trim();
                    }

                    foreach (var dc in dataColl)
                    {
                        switch (dc.PaperType)
                        {
                            case 1:
                                {
                                    dc.ObtainMark = dc.ObtainMarkTH;
                                    dc.ObtainMarkPR = "";

                                    if (dc.ObtainMarkTH.ToLower().Trim() == "ab" || dc.ObtainMarkTH.ToLower() == "a")
                                    {
                                        dc.IsAbsentTH = true;
                                        dc.IsAbsent = true;
                                    }

                                    double om = 0;
                                    double.TryParse(dc.ObtainMarkTH, out om);
                                    om = Math.Round(om, 2);
                                    dc.OM_TH = om;
                                    dc.OM = om;    
                                }
                                break;
                            case 2:
                                {
                                    dc.ObtainMark = dc.ObtainMarkPR;
                                    dc.ObtainMarkTH = "";
                                    if (dc.ObtainMarkPR.ToLower().Trim() == "ab" || dc.ObtainMarkPR.ToLower() == "a")
                                    {
                                        dc.IsAbsentPR = true;
                                        dc.IsAbsent = true;
                                    }


                                    double om = 0;
                                    double.TryParse(dc.ObtainMarkPR, out om);
                                    om = Math.Round(om, 2);
                                    dc.OM_PR = om;
                                    dc.OM = om;
                                }
                                break;
                            case 3:
                                {
                                    dc.ObtainMark = dc.ObtainMarkTH + "#" + dc.ObtainMarkPR;

                                    if (dc.ObtainMarkTH.ToLower().Trim() == "ab" || dc.ObtainMarkTH.ToLower() == "a")
                                        dc.IsAbsentTH = true;

                                    if (dc.ObtainMarkPR.ToLower().Trim() == "ab" || dc.ObtainMarkPR.ToLower() == "a")
                                        dc.IsAbsentPR = true;

                                    if (dc.IsAbsentTH && dc.IsAbsentPR)
                                        dc.IsAbsent = true;

                                    double om = 0;
                                    double.TryParse(dc.ObtainMarkTH, out om);
                                    om = Math.Round(om, 2);
                                    dc.OM_TH = om;

                                    double om1 = 0;
                                    double.TryParse(dc.ObtainMarkPR, out om1);
                                    om1 = Math.Round(om1, 2);
                                    dc.OM_PR = om1;

                                    dc.OM = Math.Round(dc.OM_TH + dc.OM_PR,2);
                                }
                                break;
                            case 4:
                                {
                                    dc.ObtainMark = dc.ObtainMarkTH;
                                    dc.O_Grade = dc.ObtainMarkTH;

                                    if (dc.ObtainMarkTH.ToLower().Trim() == "ab")
                                    {
                                        dc.IsAbsentTH = true;
                                        dc.IsAbsent = true;
                                    }

                                }
                                break;
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
