using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class ClassTeacher
    {

        DA.Academic.Transaction.ClassTeacherDB db = null;
        int _UserId = 0;

        public ClassTeacher(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.ClassTeacherDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int? AcademicYearId,BE.Academic.Transaction.ClassTeacher beData)
        {
            bool isModify = beData.ClassTeacherId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Transaction.ClassTeacherCollections GetAllClassTeacher(int EntityId, int? AcademicYearId)
        {
            return db.getAllClassTeacher(_UserId, EntityId,AcademicYearId);
        }
        public BE.Academic.Transaction.ClassTeacher GetClassTeacherById(int EntityId, int ClassTeacherId)
        {
            return db.getClassTeacherById(_UserId, EntityId, ClassTeacherId);
        }
        public ResponeValues DeleteById(int EntityId, int ClassTeacherId)
        {
            return db.DeleteById(_UserId, EntityId, ClassTeacherId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.ClassTeacher beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ClassTeacherId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ClassTeacherId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if(beData.TeacherId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Teacher Name";
                }
                else if (beData.ClassId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
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
