using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeItem
    {
        DA.Fee.Creation.FeeItemDB db = null;
        int _UserId = 0;
        public FeeItem(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeItemDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Fee.Creation.FeeItem beData)
        {
            bool isModify = beData.FeeItemId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeItemCollections GetAllFeeItem(int EntityId)
        {
            return db.getAllFeeItem(_UserId, EntityId);
        }
        public BE.Fee.Creation.FeeItem GetFeeItemById(int EntityId, int FeeItemId)
        {
            return db.getFeeItemById(_UserId, EntityId, FeeItemId);
        }
        public ResponeValues DeleteById(int EntityId, int FeeItemId)
        {
            return db.DeleteById(_UserId, EntityId, FeeItemId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.FeeItem beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.FeeItemId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.FeeItemId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter FeeItem Name";
                }else if(!beData.ProductId.HasValue || beData.ProductId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Product Name";
                }
                else if (!beData.LedgerId.HasValue || beData.LedgerId.Value == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Ledger Name";
                }
                else
                {
                    if (beData.IsExtraFee)
                    {
                        if(beData.MonthIdColl!=null && beData.MonthIdColl.Count > 0)
                        {
                            resVal.ResponseMSG = "For Extra Fee Item Not Need To Mapping Month";
                            return resVal;
                        }
                    }

                    if (beData.LedgerId.HasValue && beData.LedgerId.Value == 0)
                        beData.LedgerId = null;
                    
                    if (beData.ProductId.HasValue && beData.ProductId.Value == 0)
                        beData.ProductId = null;

                    if (!beData.IsSecurityDeposit)
                        beData.RefundableFee = false;

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
