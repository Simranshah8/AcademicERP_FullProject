using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class HeightAndWeight
    {
        DA.Exam.Transaction.HeightAndWeightDB db = null;
        int _UserId = 0;

        public HeightAndWeight(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.HeightAndWeightDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.HeightAndWeightCollections dataColl)
        {
          
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, dataColl);
            else
                return isValid;
        }
        public AcademicLib.BE.Exam.Transaction.HeightAndWeightCollections getHeightWeightClassWise(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.getHeightWeightClassWise(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId);
        }
            public BE.Exam.Transaction.HeightAndWeightCollections GetAllHeightAndWeight(int EntityId)
        {
            return db.getAllHeightAndWeight(_UserId, EntityId);
        }
        public BE.Exam.Transaction.HeightAndWeight GetHeightAndWeightById(int EntityId, int TranId)
        {
            return db.getHeightAndWeightById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.HeightAndWeightCollections dataColl)
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

                    foreach(var v in dataColl)
                    {
                        if (v.ExamTypeId == 0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Select Valid Exam Type Name";
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
