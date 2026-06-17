using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class BillGenerate
    {
        DA.Fee.Creation.BillGenerateDB db = null;
        int _UserId = 0;

        public BillGenerate(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.BillGenerateDB(hostName, dbName);
        }
        public ResponeValues GenerateFine(int AcademicYearId, int ForMonthId, bool IsRegenerate)
        {
            return db.GenerateFine(_UserId, AcademicYearId, ForMonthId, IsRegenerate);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.BillGenerate beData)
        {
            return db.SaveUpdate(AcademicYearId, beData);
        }
        public ResponeValues SaveStudentWise(int AcademicYearId, BE.Fee.Creation.BillGenerate beData)
        {
            return db.SaveStudentWise(AcademicYearId, beData);
        }
        public RE.Fee.BillGenerateClassWiseCollections getClassWiseBillGenerateList(int AcademicYearId, int EntityId)
        {
            return db.getClassWiseBillGenerateList(_UserId, AcademicYearId, EntityId);
        }
        public RE.Fee.BillGenerateFeeCollections getClassWiseFeeItem(int AcademicYearId, int MonthId, int ClassId, int? SemesterId, int? ClassYearId)
        {
            return db.getClassWiseFeeItem(_UserId, AcademicYearId, MonthId, ClassId, SemesterId, ClassYearId);
        }
        public RE.Fee.StudentFeeItemDetailsCollections getStudentFeeDetails(int AcademicYearId, int ClassId, int MonthId, int? SemesterId, int? ClassYearId, int? BatchId)
        {
            return db.getStudentFeeDetails(_UserId, AcademicYearId, ClassId, MonthId, SemesterId, ClassYearId, BatchId);
        }
        public RE.Fee.BillGenerateStudentWiseCollections getStudentWiseBillGenerateList(int AcademicYearId, int EntityId)
        {
            return db.getStudentWiseBillGenerateList(_UserId, AcademicYearId, EntityId);
        }
        public RE.Fee.PendingBillGenerateCollections getPendingBillGenerateList(int AcademicYearId)
        {
            return db.getPendingBillGenerateList(_UserId, AcademicYearId);
        }
        public ResponeValues Delete(int AcademicYearId, int MonthId, int ClassId, int? SemesterId, int? ClassYearId)
        {
            return db.Delete(_UserId, AcademicYearId, MonthId, ClassId, SemesterId, ClassYearId);
        }
        public ResponeValues DeleteStudentWise(int AcademicYearId, int StudentId, int FromMonthId, int ToMonthId, int? SemesterId, int? ClassYearId, int? BatchId = null)
        {
            return db.DeleteStudentWise(_UserId, AcademicYearId, StudentId, FromMonthId, ToMonthId, SemesterId, ClassYearId, BatchId);
        }
        public AcademicLib.BE.Fee.Creation.BillGenerate_SENTCollections getBillForSMS(int AcademicYearId, int classId, int? sectionId, int fromMonthId, int toMonthId, int? batchId, int? facultyId, int? semesterId, int? classYearId)
        {
            return db.getBillForSMS(_UserId, AcademicYearId, classId, sectionId, fromMonthId, toMonthId, batchId, facultyId, semesterId, classYearId);
        }
        public AcademicLib.RE.Fee.BillMissingStudentCollections getBillMissingStudent(int AcademicYearId, int classId, int forMonth, int? SemesterId, int? ClassYearId)
        {
            return db.getBillMissingStudent(_UserId, AcademicYearId, classId, forMonth, SemesterId, ClassYearId);
        }
        public ResponeValues billGenerateMissingStudent(int AcademicYearId, int forMonth, string StudentIdColl)
        {
            return db.billGenerateMissingStudent(_UserId, AcademicYearId, forMonth, StudentIdColl);
        }
        public ResponeValue Upload_TP_StudentStatement(List<AcademicLib.Public_API.StudentStatement> dataColl)
        {
            return db.Upload_TP_StudentStatement(_UserId, dataColl);
        }
        public RE.Fee.BillGenerateFeeCollections GetStudentWiseFeeItem(int AcademicYearId, int? FromMonthId, int? ToMonthId, int? StudentId)
        {
            return db.GetStudentWiseFeeItem(_UserId, AcademicYearId, FromMonthId, ToMonthId, StudentId);
        }

        public ResponeValues ImportBillGenerate(List<BE.Fee.Creation.ImportBillGenerate> dataColl)
        {
            return db.ImportBillGenerate(_UserId, dataColl);
        }
    }
}
