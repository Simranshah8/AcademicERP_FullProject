using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class ClassShift
    {

        DA.Academic.Transaction.ClassShiftDB db = null;
        int _UserId = 0;

        public ClassShift(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.ClassShiftDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Academic.Transaction.ClassShift beData)
        {
            bool isModify = beData.ClassShiftId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Transaction.ClassShiftCollections GetAllClassShift(int EntityId,int AcademicYearId, bool ForTran)
        {
            return db.getAllClassShift(_UserId, EntityId,AcademicYearId,ForTran);
        }
        public BE.Academic.Transaction.ClassShift GetClassShiftById(int EntityId, int ClassShiftId)
        {
            return db.getClassShiftById(_UserId, EntityId, ClassShiftId);
        }
        public ResponeValues DeleteById(int EntityId, int ClassShiftId)
        {
            return db.DeleteById(_UserId, EntityId, ClassShiftId);
        }
        //Added by Simran
        public BE.Academic.Transaction.ClassShiftCollections GetClassWiseShift(int EntityId,int? AcademicYearId, int ClassId)
        {
            return db.GetClassWiseShift(_UserId, EntityId, AcademicYearId, ClassId);
        }
        //End
        public ResponeValues IsValidData(ref BE.Academic.Transaction.ClassShift beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ClassShiftId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ClassShiftId != 0)
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
