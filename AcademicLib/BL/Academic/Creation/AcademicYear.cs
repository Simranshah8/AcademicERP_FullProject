using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class AcademicYear
    {
        DA.Academic.Creation.AcademicYearDB db = null;
        int _UserId = 0;
        public AcademicYear(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.AcademicYearDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.AcademicYear beData)
        {
            bool isModify = beData.AcademicYearId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.AcademicYearCollections GetAllAcademicYear(int EntityId)
        {
            return db.getAllAcademicYear(_UserId, EntityId);
        }
        public BE.Academic.Creation.AcademicYear GetAcademicYearById(int EntityId, int AcademicYearId)
        {
            return db.getAcademicYearById(_UserId, EntityId, AcademicYearId);
        }
        public ResponeValues DeleteById(int EntityId, int AcademicYearId)
        {
            return db.DeleteById(_UserId, EntityId, AcademicYearId);
        }
        public ResponeValues getDefaultAcademicYearId()
        {
            return db.getDefaultAcademicYearId(_UserId);
        }
        public BE.Academic.Creation.AcademicYear getPeriod(int AcademicYearId)
        {
            return db.getPeriod(_UserId, AcademicYearId);
        }

            public ResponeValues IsValidData(ref BE.Academic.Creation.AcademicYear beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.AcademicYearId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.AcademicYearId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter AcademicYear Name";
                }
                else if(!beData.StartDate.HasValue || !beData.EndDate.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter Academic Year Start/End Date";
                }
                else if (beData.StartMonth==0 || beData.EndMonth==0)
                {
                    resVal.ResponseMSG = "Please ! Select Academic Year Start/End Month";
                }
                else
                {
                    int yearid = 0;
                    int.TryParse(beData.Name, out yearid);
                    if (yearid == 0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Valid Academic Year Name";
                        return resVal;
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
