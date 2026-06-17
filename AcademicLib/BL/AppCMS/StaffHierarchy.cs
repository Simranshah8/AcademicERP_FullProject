using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class StaffHierarchy
    {
        DA.AppCMS.Creation.StaffHierarchyDB db = null;
        int _UserId = 0;

        public StaffHierarchy(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.StaffHierarchyDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.StaffHierarchy beData)
        {
            bool isModify = beData.StaffHierarchyId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.StaffHierarchyCollections GetAllStaffHierarchy(int EntityId, string BranchCode)
        {
            return db.getAllStaffHierarchy(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.StaffHierarchy GetStaffHierarchyById(int EntityId, int StaffHierarchyId)
        {
            return db.getStaffHierarchyById(_UserId, EntityId, StaffHierarchyId);
        }
        public ResponeValues DeleteById(int EntityId, int StaffHierarchyId)
        {
            return db.DeleteById(_UserId, EntityId, StaffHierarchyId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.StaffHierarchy beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.StaffHierarchyId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.StaffHierarchyId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.FullName))
                {
                    resVal.ResponseMSG = "Please ! Enter Full Name ";
                }
                else if (string.IsNullOrEmpty(beData.Designation))
                {
                    resVal.ResponseMSG = "Please ! Enter Designation ";
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
