using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Faculty
    {
        DA.Academic.Creation.FacultyDB db = null;
        int _UserId = 0;
        public Faculty(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.FacultyDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Faculty beData)
        {
            bool isModify = beData.FacultyId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.FacultyCollections GetAllFaculty(int EntityId)
        {
            return db.getAllFaculty(_UserId, EntityId);
        }
        public BE.Academic.Creation.FacultyCollections getAllFacultyForTran(int EntityId, int AcademicYearId)
        {
            return db.getAllFacultyForTran(_UserId, EntityId, AcademicYearId);
        }
        public BE.Academic.Creation.Faculty GetFacultyById(int EntityId, int FacultyId)
        {
            return db.getFacultyById(_UserId, EntityId, FacultyId);
        }
        public ResponeValues DeleteById(int EntityId, int FacultyId)
        {
            return db.DeleteById(_UserId, EntityId, FacultyId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Faculty beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.FacultyId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.FacultyId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Faculty Name";
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
