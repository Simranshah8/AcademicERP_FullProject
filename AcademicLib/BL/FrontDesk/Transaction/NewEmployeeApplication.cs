using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class NewEmployeeApplication
    {
        DA.FrontDesk.Transaction.NewEmployeeApplicationDB db = null;
        int _UserId = 0;

        public NewEmployeeApplication(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.NewEmployeeApplicationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.NewEmployeeApplication beData)
        {
            bool isModify = false;// beData.NewEmployeeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.NewEmployeeApplicationCollections GetAllNewEmployeeApplication(int EntityId)
        {
            return db.getAllNewEmployeeApplication(_UserId, EntityId);
        }
        public BE.FrontDesk.Transaction.NewEmployeeApplication GetNewEmployeeApplicationById(int EntityId, int NewEmployeeId)
        {
            return db.getNewEmployeeApplicationById(_UserId, EntityId, NewEmployeeId);
        }
        public ResponeValues DeleteById(int EntityId, int NewEmployeeId)
        {
            return db.DeleteById(_UserId, EntityId, NewEmployeeId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.NewEmployeeApplication beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                //else if (IsModify && beData.NewEmployeeId == 0)
                //{
                //    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                //}
                //else if (!IsModify && beData.NewEmployeeId != 0)
                //{
                //    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                //}
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.FirstName))
                {
                    resVal.ResponseMSG = "Please ! Enter Name";
                }
                //else if (bedata.studentid == 0)
                //{
                //    resval.responsemsg = "please ! select employee";
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
