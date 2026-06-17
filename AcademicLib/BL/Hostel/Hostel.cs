using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Hostel
{
    public class Hostel
    {
        DA.Hostel.HostelDB db = null;
        int _UserId = 0;

        public Hostel(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Hostel.HostelDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Hostel.Hostel beData)
        {
            bool isModify = beData.HostelId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Hostel.HostelCollections GetAllHostel(int EntityId)
        {
            return db.getAllHostel(_UserId, EntityId);
        }
        public AcademicLib.BE.Hostel.Hostel GetHostelById(int EntityId, int HostelId)
        {
            return db.getHostelById(_UserId, EntityId, HostelId);
        }
        public ResponeValues DeleteById(int EntityId, int HostelId)
        {
            return db.DeleteById(_UserId, EntityId, HostelId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.Hostel.Hostel beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.HostelId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.HostelId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Logo))
                //{
                //    resVal.ResponseMSG = "Please ! Select Logo";
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
