using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class Department
    {
        DA.Library.Creation.DepartmentDB db = null;
        int _UserId = 0;
        public Department(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.DepartmentDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Department beData)
        {
            bool isModify = beData.DepartmentId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.DepartmentCollections GetAllDepartment(int EntityId)
        {
            return db.getAllDepartment(_UserId, EntityId);
        }
        public BE.Academic.Creation.Department GetDepartmentById(int EntityId, int DepartmentId)
        {
            return db.getDepartmentById(_UserId, EntityId, DepartmentId);
        }
        public ResponeValues DeleteById(int EntityId, int DepartmentId)
        {
            return db.DeleteById(_UserId, EntityId, DepartmentId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Department beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.DepartmentId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.DepartmentId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Department Name";
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
