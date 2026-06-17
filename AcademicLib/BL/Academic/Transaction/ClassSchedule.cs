using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class ClassSchedule
    {
        DA.Academic.Transaction.ClassScheduleDB db = null;
        int _UserId = 0;

        public ClassSchedule(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.ClassScheduleDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.ClassScheduleCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, beData);
            else
                return isValid;
        }
        public BE.Academic.Transaction.ClassScheduleCollections GetClassScheduleByClassId(int ClassId,int? SectionId,int ClassShiftId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            return db.getClassScheduleByClassId(_UserId,ClassId,SectionId,ClassShiftId, SemesterId, ClassYearId, BatchId);
        }
        public AcademicLib.RE.Academic.ClassScheduleCollections getClassSchedule(int AcademicYearId, int? ClassId, int? SectionId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            return db.getClassSchedule(_UserId,AcademicYearId, ClassId, SectionId,SemesterId,ClassYearId,BatchId);
        }
        public ResponeValues DeleteByShiftId( int EntityId,int ClassId, int ClassShiftId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            return db.DeleteByShiftId(_UserId, EntityId,ClassId, ClassShiftId, SemesterId, ClassYearId, BatchId);
        }
            public ResponeValues IsValidData(ref BE.Academic.Transaction.ClassScheduleCollections beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                //else if (IsModify && beData.TranId == 0)
                //{
                //    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                //}
                //else if (!IsModify && beData.TranId != 0)
                //{
                //    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                //}
                //else if (beData.CUserId == 0)
                //{
                //    resVal.ResponseMSG = "Invalid User for CRUD";
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
