using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class CASType
    {
        DA.Exam.Transaction.CASTypeDB db = null;
        int _UserId = 0;

        public CASType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.CASTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Creation.CASType beData)
        {
            bool isModify = beData.CASTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Creation.CASTypeCollections GetAllCASType(int EntityId, int? ClassId = null, int? SectionId = null, int? SubjectId = null, int? ExamTypeId = null,bool ForEdit=false)
        {
            return db.getAllCASType(_UserId, EntityId,ClassId,SectionId,SubjectId,ExamTypeId,ForEdit);
        }
        public BE.Exam.Creation.CASType GetCASTypeById(int EntityId, int CASTypeId)
        {
            return db.getCASTypeById(_UserId, EntityId, CASTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int CASTypeId)
        {
            return db.DeleteById(_UserId, EntityId, CASTypeId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Creation.CASType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.CASTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.CASTypeId != 0)
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
