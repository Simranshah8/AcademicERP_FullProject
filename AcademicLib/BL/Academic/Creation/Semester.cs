using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Semester
    {
        DA.Academic.Creation.SemesterDB db = null;
        int _UserId = 0;
        public Semester(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.SemesterDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Semester beData)
        {
            bool isModify = beData.SemesterId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.SemesterCollections GetAllSemester(int EntityId)
        {
            return db.getAllSemester(_UserId, EntityId);
        }
        public BE.Academic.Creation.SemesterCollections getAllSemesterForTran(int EntityId, int AcademicYearId)
        {
            return db.getAllSemesterForTran(_UserId, EntityId, AcademicYearId);
        }
        public BE.Academic.Creation.Semester GetSemesterById(int EntityId, int SemesterId)
        {
            return db.getSemesterById(_UserId, EntityId, SemesterId);
        }
        public ResponeValues DeleteById(int EntityId, int SemesterId)
        {
            return db.DeleteById(_UserId, EntityId, SemesterId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Semester beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.SemesterId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.SemesterId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Semester Name";
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
