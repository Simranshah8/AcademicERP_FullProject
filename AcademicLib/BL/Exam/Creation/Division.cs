using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Creation
{
    public class Division
    {
        DA.Exam.Creation.DivisionDB db = null;
        int _UserId = 0;

        public Division(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Creation.DivisionDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Creation.Division beData)
        {
            bool isModify = beData.DivisionId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Creation.DivisionCollections GetAllDivision(int EntityId)
        {
            return db.getAllDivision(_UserId, EntityId);
        }
        public BE.Exam.Creation.Division GetDivisionById(int EntityId, int DivisionId)
        {
            return db.getDivisionById(_UserId, EntityId, DivisionId);
        }
        public ResponeValues DeleteById(int EntityId, int DivisionId)
        {
            return db.DeleteById(_UserId, EntityId, DivisionId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Creation.Division beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.DivisionId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.DivisionId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Name";
                }              
                else
                {

                    if (beData.ClassId.HasValue && beData.ClassId.Value == 0)
                        beData.ClassId = null;

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
