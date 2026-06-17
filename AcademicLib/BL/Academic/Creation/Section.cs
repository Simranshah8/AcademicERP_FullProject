using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Section
    {
        DA.Academic.Creation.SectionDB db = null;
        int _UserId = 0;
        public Section(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.SectionDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Section beData)
        {
            bool isModify = beData.SectionId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.SectionCollections GetAllSection(int EntityId)
        {
            return db.getAllSection(_UserId, EntityId);
        }
        public BE.Academic.Creation.SectionCollections getAllSectionForTran(int EntityId, int AcademicYearId)
        {
            return db.getAllSectionForTran(_UserId, EntityId, AcademicYearId);
        }
            public BE.Academic.Creation.Section GetSectionById(int EntityId, int SectionId)
        {
            return db.getSectionById(_UserId, EntityId, SectionId);
        }
        public ResponeValues DeleteById(int EntityId, int SectionId)
        {
            return db.DeleteById(_UserId, EntityId, SectionId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Section beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {                
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.SectionId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.SectionId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Section Name";
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
