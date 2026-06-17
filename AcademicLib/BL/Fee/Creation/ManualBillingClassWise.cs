using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class ManualBillingClassWise
    {
        DA.Fee.Creation.ManualBillingClassWiseDB db = null;
        int _UserId = 0;
        string _hostName, _dbName;

        public ManualBillingClassWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            this._hostName = hostName;
            this._dbName = dbName;
            db = new DA.Fee.Creation.ManualBillingClassWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.ManualBilling beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
            {
                isValid = db.SaveUpdate(AcademicYearId, beData, isModify);

                if (isValid.IsSuccess && beData.IsCash && beData.BillingType == BE.Fee.Creation.BILLINGTYPES.SALESINVOICE)
                {
                    var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(_UserId, _hostName, _dbName).GetFeeConfigurationById(0, AcademicYearId);
                    var costClassColl = new Dynamic.DataAccess.Account.CostClassDB(_hostName, _dbName).getAllCostClass(_UserId);
                    
                }

                return isValid;
            }
            else
                return isValid;
        }
        public BE.Fee.Creation.ManualBillingCollections GetAllManualBilling(int AcademicYearId, int EntityId)
        {
            return db.GetAllClassWiseManualBilling(_UserId, AcademicYearId, EntityId);
        }
        public BE.Fee.Creation.ManualBilling GetManualBillingById(int EntityId, int TranId)
        {
            return db.getManualBillingById(_UserId, EntityId, TranId);
        }

        public AcademicLib.RE.Fee.ManualBillingCollections getManualBillingDetails(int AcademicYearId, DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getManualBillingDetails(_UserId, AcademicYearId, dateFrom, dateTo);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues getAutoNo(int AcademicYearId, int? CostClassId)
        {
            return db.getAutoNo(_UserId, AcademicYearId, CostClassId);
        }
        public ResponeValues Cancel(BE.Fee.Creation.ManualBilling beData)
        {
            return db.Cancel(beData);
        }
        public ResponeValues getFeeRate(int AcademicYearId, int ClassId, int FeeItemId)
        {
            return db.getFeeRate(_UserId, AcademicYearId, ClassId, FeeItemId);
        }
        public AcademicLib.BE.Fee.Creation.ManualBilling getManualBillingDetailsById(int TranId)
        {
            return db.getManualBillingDetailsById(_UserId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.ManualBilling beData, bool IsModify)
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
                else if (!beData.ClassId.HasValue || beData.ClassId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Class Name";
                }
                else if (beData.BillingDate.Year < 2000)
                {
                    resVal.ResponseMSG = "Please ! Enter Billing Date";
                }
                else if (beData.ManualBillingDetailsColl == null || beData.ManualBillingDetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Billing Details";
                }
                else if (beData.BillingType == BE.Fee.Creation.BILLINGTYPES.MEMO && beData.ForMonthId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select For Month Name";
                }
                else if (beData.IsCash && IsModify)
                {
                    resVal.ResponseMSG = "Cash bill was not modify. pls cancel (bill and receipt both).";
                }
                else
                {
                    var detailsColl = beData.ManualBillingDetailsColl;

                    beData.ManualBillingDetailsColl = new BE.Fee.Creation.ManualBillingDetailsCollections();
                    int sno = 1;
                    foreach (var v in detailsColl)
                    {
                        if (v.FeeItemId.HasValue && v.FeeItemId.Value > 0)
                        {
                            if (v.Qty != 0 || v.PayableAmt != 0 || v.Rate != 0)
                            {
                                double pAmt = Math.Round(v.PayableAmt, 2);
                                double amt = Math.Round(v.Qty * v.Rate, 2);
                                double disAmt = Math.Round(v.DiscountAmt, 2);
                                if (pAmt != (amt - disAmt))
                                {
                                    resVal.ResponseMSG = "Qt X Rate = Amount - Discount = Payable Amount does not match";
                                    return resVal;
                                }
                                else
                                {
                                    v.SNo = sno;
                                    beData.ManualBillingDetailsColl.Add(v);
                                    sno++;
                                }

                            }
                        }

                    }

                    beData.TotalAmount = beData.ManualBillingDetailsColl.Sum(p1 => p1.PayableAmt);
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


        public ResponeValues SaveClassWiseManualBilling(int AcademicYearId, BE.Fee.Creation.ManualBilling beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
            {
                isValid = db.SaveManualBillingClassWise(AcademicYearId, beData, isModify);

                if (isValid.IsSuccess && beData.IsCash && beData.BillingType == BE.Fee.Creation.BILLINGTYPES.SALESINVOICE)
                {
                    var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(_UserId, _hostName, _dbName).GetFeeConfigurationById(0, AcademicYearId);
                    var costClassColl = new Dynamic.DataAccess.Account.CostClassDB(_hostName, _dbName).getAllCostClass(_UserId);
                }

                return isValid;
            }
            else
                return isValid;
        }

        public AcademicLib.BE.Fee.Creation.ManualBilling getClassWiseManualBillingDetailsById(int TranId)
        {
            return db.getClassWiseManualBillingDetailsById(_UserId, TranId);
        }

    }
}
