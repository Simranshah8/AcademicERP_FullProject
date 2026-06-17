using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class CommunicationType
    {
        DA.FrontDesk.Transaction.CommunicationTypeDB db = null;
        int _UserId = 0;

        public CommunicationType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.CommunicationTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.CommunicationType beData)
        {
            bool isModify = beData.CommunicationTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.CommunicationTypeCollections GetAllCommunicationType(int EntityId,bool ForTran)
        {
            return db.getAllCommunicationType(_UserId, EntityId, ForTran);
        }
        public BE.FrontDesk.Transaction.CommunicationType GetCommunicationTypeById(int EntityId, int CommunicationTypeId)
        {
            return db.getCommunicationTypeById(_UserId, EntityId, CommunicationTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int CommunicationTypeId)
        {
            return db.DeleteById(_UserId, EntityId, CommunicationTypeId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.CommunicationType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.CommunicationTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.CommunicationTypeId != 0)
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
