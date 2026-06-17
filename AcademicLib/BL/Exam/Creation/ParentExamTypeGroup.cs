using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Creation
{
    public class ParentExamTypeGroup
    {
        DA.Exam.Creation.ParentParentExamTypeGroupDB db = null;
        int _UserId = 0;

        public ParentExamTypeGroup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Creation.ParentParentExamTypeGroupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Creation.ParentExamTypeGroup beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Creation.ParentExamTypeGroupCollections GetAllParentExamTypeGroup(int EntityId)
        {
            return db.getAllParentExamTypeGroup(_UserId, EntityId);
        }
        public BE.Exam.Creation.ParentExamTypeGroup GetParentExamTypeGroupById(int EntityId, int TranId)
        {
            return db.getParentExamTypeGroupById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Creation.ParentExamTypeGroup beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
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
                else if (string.IsNullOrEmpty(beData.DisplayName))
                {
                    resVal.ResponseMSG = "Please ! Enter Display Name ";
                }

                else
                {
                    if (beData.ResultDate.HasValue && beData.ResultTime.HasValue)
                    {
                        var dt = beData.ResultDate.Value;
                        var t = beData.ResultTime.Value;
                        beData.ResultDate = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                        beData.ResultTime = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                    }

                    int sno = 1;
                    if (beData.ParentExamTypeGroupDetailsColl != null)
                    {
                        foreach (var v in beData.ParentExamTypeGroupDetailsColl)
                        {
                            v.SNO = sno;
                            sno++;
                        }
                    }

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
