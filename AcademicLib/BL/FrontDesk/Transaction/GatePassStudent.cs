using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class GatePassStudent
    {
        DA.FrontDesk.Transaction.GatePassStudentDB db = null;
        int _UserId = 0;

        public GatePassStudent(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.GatePassStudentDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.GatePassStudent beData)
        {
            bool isModify = beData.GatePassStudentId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.GatePassStudentCollections GetAllGatePassStudent(int EntityId)
        {
            return db.getAllGatePassStudent(_UserId, EntityId);
        }
        public BE.FrontDesk.Transaction.GatePassStudent GetGatePassStudentById(int EntityId, int GatePassStudentId)
        {
            return db.getGatePassStudentById(_UserId, EntityId, GatePassStudentId);
        }
        public ResponeValues DeleteById(int EntityId, int GatePassStudentId)
        {
            return db.DeleteById(_UserId, EntityId, GatePassStudentId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.GatePassStudent beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.GatePassStudentId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.GatePassStudentId != 0)
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
                //else if (beData.StudentId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select Employee";
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
