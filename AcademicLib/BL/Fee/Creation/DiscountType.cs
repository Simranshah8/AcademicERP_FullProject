using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class DiscountType
    {
        DA.Fee.Creation.DiscountTypeDB db = null;
        int _UserId = 0;
        public DiscountType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.DiscountTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Fee.Creation.DiscountType beData)
        {
            bool isModify = beData.DiscountTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Fee.Creation.DiscountTypeCollections GetAllDiscountType(int EntityId)
        {
            return db.getAllDiscountType(_UserId, EntityId);
        }
        public BE.Fee.Creation.DiscountType GetDiscountTypeById(int EntityId, int DiscountTypeId)
        {
            return db.getDiscountTypeById(_UserId, EntityId, DiscountTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int DiscountTypeId)
        {
            return db.DeleteById(_UserId, EntityId, DiscountTypeId);
        }
        public RE.Fee.DiscountStudentCollections getDiscountStudentList(int AcademicYearId,string ClassIdColl,string FeeItemIdColl)
        {
            return db.getDiscountStudentList(_UserId,AcademicYearId,ClassIdColl,FeeItemIdColl);
        }
            public ResponeValues IsValidData(ref BE.Fee.Creation.DiscountType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.DiscountTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.DiscountTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter DiscountType Name";
                }else if(beData.DetailsColl==null || beData.DetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Fee Details";
                }
                else
                {

                    List<int> feeItemColl = new List<int>();
                    foreach(var v in beData.DetailsColl)
                    {
                        if (v.FeeItemId.HasValue)
                        {
                            if (feeItemColl.Contains(v.FeeItemId.Value))
                            {
                                resVal.ResponseMSG = "Duplicate Fee ";
                                return resVal;
                            }
                            else
                                feeItemColl.Add(v.FeeItemId.Value);

                            if (v.DiscountAmt != 0 && v.DiscountPer != 0)
                            {
                                resVal.ResponseMSG = "Enter Any One Discount Amount Or Per(%)";
                                return resVal;
                            }

                            if (v.DiscountAmt == 0 && v.DiscountPer == 0)
                            {
                                resVal.ResponseMSG = "Enter Any One Discount Amount Or Per(%)";
                                return resVal;
                            }

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
