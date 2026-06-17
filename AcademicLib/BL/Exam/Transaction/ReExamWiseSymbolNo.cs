using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ReExamWiseSymbolNo
    {
        DA.Exam.Transaction.ReExamWiseSymbolNoDB db = null;
        int _UserId = 0;

        public ReExamWiseSymbolNo(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ReExamWiseSymbolNoDB(hostName, dbName);
        }
        public AcademicLib.BE.Exam.Transaction.ReExamWiseSymbolNoCollections getStudentList(bool ForClassWise, int ClassId, int? SectionId, int ExamTypeId, int ReExamTypeId,int? AcademicYearId)
        {
            return db.getStudentList(_UserId, ClassId, SectionId, ExamTypeId, ReExamTypeId, ForClassWise,AcademicYearId);
        }
            public ResponeValues SaveFormData(BE.Exam.Transaction.ExamWiseSymbolNoCollections dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, dataColl);
            else
                return isValid;
        }
        public AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections getSymbolNoForMarkImport()
        {
            return db.getSymbolNoForMarkImport(_UserId);
        }
        public BE.Exam.Transaction.ExamWiseSymbolNoCollections GetAllExamWiseSymbolNo(int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.getExamWiseSymbolNoClassWise(_UserId, ClassId, SectionId, ExamTypeId);
        }
        public BE.Exam.Transaction.ExamWiseSymbolNo GetExamWiseSymbolNoById(int EntityId, int TranId)
        {
            return db.getExamWiseSymbolNoById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues Transfor(int FromExamTypeId, int ToExamTypeId)
        {
            return db.Transfor(_UserId, FromExamTypeId, ToExamTypeId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.ExamWiseSymbolNoCollections dataColl)
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
