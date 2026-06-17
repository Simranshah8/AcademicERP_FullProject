using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.HomeWork
{
    public class HomeworkType
    {
        DA.HomeWork.HomeworkTypeDB db = null;
        int _UserId = 0;

        public HomeworkType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.HomeWork.HomeworkTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.HomeWork.HomeworkType beData)
        {
            bool isModify = beData.HomeworkTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.HomeWork.HomeworkTypeCollections GetAllHomeworkType(int EntityId)
        {
            return db.getAllHomeworkType(_UserId, EntityId);
        }
        public BE.HomeWork.HomeworkType GetHomeworkTypeById(int EntityId, int HomeworkTypeId)
        {
            return db.getHomeworkTypeById(_UserId, EntityId, HomeworkTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int HomeworkTypeId)
        {
            return db.DeleteById(_UserId, EntityId, HomeworkTypeId);
        }
        public ResponeValues IsValidData(ref BE.HomeWork.HomeworkType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.HomeworkTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.HomeworkTypeId != 0)
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
                    //if (beData.HomeworkTypeDocumentColl != null && beData.HomeworkTypeDocumentColl.Count > 0)
                    //{
                    //    var tmpAcademicColl = beData.HomeworkTypeDocumentColl;
                    //    beData.HomeworkTypeDocumentColl = new List<BE.Homework.HomeworkTypeDocument>();
                    //    foreach (var aDet in tmpAcademicColl)
                    //    {
                    //        if (!string.IsNullOrEmpty(aDet.AttachDocumentPath) && !string.IsNullOrEmpty(aDet.Description))
                    //        {
                    //            beData.HomeworkTypeDocumentColl.Add(aDet);
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
