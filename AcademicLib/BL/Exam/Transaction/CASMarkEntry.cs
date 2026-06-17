using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class CASMarkEntry
    {
        DA.Exam.Transaction.CASMarkEntryDB db = null;
        int _UserId = 0;

        public CASMarkEntry(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.CASMarkEntryDB(hostName, dbName);
        }

        public AcademicLib.RE.Exam.StudentForCASExamTypeMarkEntryCollections getStudentForExamTypeMarkEntry(int ClassId, int? SectionId, bool FilterSection, int SubjectId, int ExamTypeId,int? CASTypeId,int? AcademicYearId)
        {
            return db.getStudentForExamTypeMarkEntry(_UserId, ClassId, SectionId, FilterSection, SubjectId, ExamTypeId,CASTypeId,AcademicYearId);
        }
            public AcademicLib.RE.Exam.StudentForCASMarkEntryCollections getStudentForMarkEntry(int ClassId, int? SectionId, bool FilterSection, int SubjectId, int CASTypeId, DateTime ExamDate,int? AcademicYearId)
        {
            return db.getStudentForMarkEntry(_UserId, ClassId, SectionId, FilterSection, SubjectId, CASTypeId, ExamDate,AcademicYearId);
        }
        public ResponeValues SaveFormData( AcademicLib.RE.Exam.StudentForCASMarkEntryCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            
            foreach(var d in dataColl)
            {
                if(d.ExamDate>DateTime.Today)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Please ! Enter Exam Date less than equal today";
                    return resVal;
                }
            }

            resVal= db.SaveUpdate(_UserId,dataColl);

            return resVal;
        }
        public AcademicLib.RE.Exam.CASMarkEntryCollections getMarkEntrySummary(int ClassId, int? SectionId, bool FilterSection,int SubjectId, DateTime dateFrom, DateTime dateTo,int? AcademicYearId)
        {
            return db.getMarkEntrySummary(_UserId, ClassId, SectionId, FilterSection,SubjectId, dateFrom, dateTo,AcademicYearId);
        }

        public AcademicLib.RE.Exam.CASMarkEntrySubjectCollections getMarkEntrySubjectSummary(int? ClassId, int? SectionId, bool FilterSection, DateTime dateFrom, DateTime dateTo,int? AcademicYearId,int? CASTypeId,int? SubjectId)
        {
            return db.getMarkEntrySubjectSummary(_UserId, ClassId, SectionId, FilterSection, dateFrom, dateTo,AcademicYearId,CASTypeId,SubjectId);
        }
        public BE.Exam.Transaction.CASMarkEntryCollections GetAllCASMarkEntry(int EntityId)
        {
            return db.getAllCASMarkEntry(_UserId, EntityId);
        }
        public BE.Exam.Transaction.CASMarkEntry GetCASMarkEntryById(int EntityId, int TranId)
        {
            return db.getCASMarkEntryById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public AcademicLib.RE.Exam.CASTabulationCollections getTabulation( int ClassId, int? SectionId, bool FilterSection, int? SubjectId, int? ExamTypeId, int? CASTypeId,int? AcademicYearId)
        {
            return db.getTabulation(_UserId, ClassId, SectionId, FilterSection, SubjectId, ExamTypeId, CASTypeId,AcademicYearId);
        }
            public ResponeValues IsValidData(ref BE.Exam.Transaction.CASMarkEntry beData, bool IsModify)
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
    }
}
