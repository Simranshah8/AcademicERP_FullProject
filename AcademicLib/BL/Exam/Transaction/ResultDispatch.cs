using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ResultDispatch
    {
        DA.Exam.Transaction.ResultDispatchDB db = null;
        int _UserId = 0;

        public ResultDispatch(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ResultDispatchDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.ResultDispatchCollections dataColl)
        {

            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, dataColl);
            else
                return isValid;
        }
        public AcademicLib.BE.Exam.Transaction.ResultDispatchCollections getResultDispatch(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.getResultDispatch(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId);
        }
       
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.ResultDispatchCollections dataColl)
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
                            resVal.ResponseMSG = "Please ! Select Valid ExamType Name";
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
