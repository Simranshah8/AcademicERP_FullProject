using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.HomeWork
{
    public class AssignmentType
    {

        DA.Homework.AssignmentTypeDB db = null;
        int _UserId = 0;

        public AssignmentType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Homework.AssignmentTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.HomeWork.AssignmentType beData)
        {
            bool isModify = beData.AssignmentTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.HomeWork.AssignmentTypeCollections GetAllAssignmentType(int EntityId)
        {
            return db.getAllAssignmentType(_UserId, EntityId);
        }
        public BE.HomeWork.AssignmentType GetAssignmentTypeById(int EntityId, int AssignmentTypeId)
        {
            return db.getAssignmentTypeById(_UserId, EntityId, AssignmentTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int AssignmentTypeId)
        {
            return db.DeleteById(_UserId, EntityId, AssignmentTypeId);
        }
        public ResponeValues IsValidData(ref BE.HomeWork.AssignmentType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.AssignmentTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.AssignmentTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.HeadLine))
                //{
                //    resVal.ResponseMSG = "Please ! Enter HeadLine ";
                //}
                else
                {
                    //if (beData.AssignmentTypeDocumentColl != null && beData.AssignmentTypeDocumentColl.Count > 0)
                    //{
                    //    var tmpAcademicColl = beData.AssignmentTypeDocumentColl;
                    //    beData.AssignmentTypeDocumentColl = new List<BE.Homework.AssignmentTypeDocument>();
                    //    foreach (var aDet in tmpAcademicColl)
                    //    {
                    //        if (!string.IsNullOrEmpty(aDet.AttachDocumentPath) && !string.IsNullOrEmpty(aDet.Description))
                    //        {
                    //            beData.AssignmentTypeDocumentColl.Add(aDet);
                    //        }
                    //    }
                    //}
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
