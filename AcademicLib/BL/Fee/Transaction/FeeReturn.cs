using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Transaction
{
    public class FeeReturn
    {
        DA.Fee.Transaction.FeeReturnDB db = null;
        int _UserId = 0;
        string _hostName, _dbName;
        public FeeReturn(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Transaction.FeeReturnDB(hostName, dbName);

            _hostName = hostName;
            _dbName = dbName;
        }
        public ResponeValues SaveFormData(int AcademicYearId, AcademicLib.BE.Fee.Transaction.FeeReceipt beData, bool nonStudent)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify, nonStudent);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData, isModify);
            else
                return isValid;
        }
        public BE.Fee.Transaction.FeeReceiptNo getAutoNo(BE.Fee.Transaction.FeeReceiptNo beData)
        {
            beData.CUserId = _UserId;

            if (beData.AcademicYearId.HasValue && beData.AcademicYearId.Value == 0)
                beData.AcademicYearId = null;

            return db.getAutoNo(beData);
        }
        public ResponeValues Cancel(BE.Fee.Transaction.FeeReceipt beData)
        {
            return db.Cancel(beData);
        }
        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesDetails(int AcademicYearId, int StudentId, int? PaidUpToMonth, string PaidUpMonthColl, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.getDuesDetails(_UserId, AcademicYearId, StudentId, PaidUpToMonth, PaidUpMonthColl, SemesterId, ClassYearId);
        }
        
        public AcademicLib.BE.Fee.Transaction.FeeReceiptCollections GetAllFeeReceipt(int AcademicYearId, int EntityId)
        {
            return db.getAllFeeReceipt(_UserId, AcademicYearId, EntityId);
        }
        public AcademicLib.BE.Fee.Transaction.FeeReceipt GetFeeReceiptById(int EntityId, int TranId)
        {
            return db.getFeeReceiptById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public AcademicLib.RE.Fee.FeeReceiptCollections getFeeReceiptCollection(int AcademicYearId, DateTime? dateFrom, DateTime? dateTo, bool showCancel, int? fromReceipt, int? toReceipt, ref double openingAmt, ref double openingDisAmt)
        {
            return db.getFeeReceiptCollection(_UserId, AcademicYearId, dateFrom, dateTo, showCancel, fromReceipt, toReceipt, ref openingAmt, ref openingDisAmt);
        }
            
        public ResponeValues GenerateStudentCostCenter()
        {
            return db.GenerateStudentCostCenter(_UserId);
        }
         
        public ResponeValues IsValidData(ref AcademicLib.BE.Fee.Transaction.FeeReceipt beData, bool IsModify, bool nonStudent)
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
                else if (nonStudent == false && (!beData.StudentId.HasValue || beData.StudentId.Value == 0))
                {
                    resVal.ResponseMSG = "Please ! Select Student Name";
                }
                else if (beData.CostClassId == 0 && (!beData.ManualBillingTranId.HasValue || beData.ManualBillingTranId == 0))
                {
                    resVal.ResponseMSG = "Please ! Select CostClass Name";
                }
                else if (beData.DetailsColl == null || beData.DetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "No Fee Details Found";
                }
                else if (beData.VoucherDate > DateTime.Today)
                {
                    resVal.ResponseMSG = "Please ! Enter Receipt Date less than equal today";
                }
                else if (string.IsNullOrEmpty(beData.Narration))
                {
                    resVal.ResponseMSG = "Please ! enter reason for return";
                }
                else
                {

                    var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(_UserId, _hostName, _dbName).GetFeeConfigurationById(0,beData.AcademicYearId);

                    beData.MonthWise = feeConfig.MonthWiseFeeHeading || feeConfig.ShowDuesFeeHeadingInReceipt;

                    if (beData.PaymentModeColl == null)
                        beData.PaymentModeColl = new BE.Fee.Transaction.FeePaymentModeCollections();

                    if (beData.AdmissionEnquiryId.HasValue && beData.AdmissionEnquiryId == 0)
                        beData.AdmissionEnquiryId = null;


                    if (beData.ReceiptAsLedgerId == 0)
                        beData.ReceiptAsLedgerId = 1;

                    double itemAmt = Math.Round(beData.DetailsColl.Sum(p1 => p1.ReceivedAmt), 2), itemDiscount = Math.Round(beData.DetailsColl.Sum(p1 => p1.DiscountAmt), 2), itemFine = Math.Round(beData.DetailsColl.Sum(p1 => p1.FineAmt), 2);
                    double itemWaiver = Math.Round(beData.DetailsColl.Sum(p1 => p1.Waiver), 2);
                    double totalAmt = Math.Round(beData.ReceivedAmt, 2), totalDiscount = Math.Round(beData.DiscountAmt, 2), totalFine = Math.Round(beData.FineAmt, 2);
                    double totalWaiver = Math.Round(beData.Waiver, 2);

                    if (totalAmt != itemAmt)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Fee Heading Wise Received And Total Received Does Not Match " + itemAmt.ToString() + "=" + totalAmt.ToString();
                        return resVal;
                    }

                    if (totalDiscount != itemDiscount)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Fee Heading Wise Discount And Total Discount Does Not Match " + totalDiscount.ToString() + "=" + itemDiscount.ToString();
                        return resVal;
                    }
                    if (totalWaiver != itemWaiver)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Fee Heading Wise Waiver And Total Waiver Does Not Match " + totalWaiver.ToString() + "=" + itemWaiver.ToString();
                        return resVal;
                    }

                    if (totalFine != itemFine)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Fee Heading Wise Fine And Total Fine Does Not Match " + totalFine.ToString() + "=" + itemFine.ToString();
                        return resVal;
                    }

                    if (beData.StudentId.HasValue && beData.StudentId.Value == 0)
                        beData.StudentId = null;

                    if (beData.PaymentModeColl.Count == 0)
                    {
                        beData.PaymentModeColl.Add(new BE.Fee.Transaction.FeePaymentMode() { Amount = beData.ReceivedAmt, LedgerId = beData.ReceiptAsLedgerId, Remarks = beData.Narration });
                    }

                    double pAmt1 = Math.Round(beData.PaymentModeColl.Sum(p1 => p1.Amount), 2);
                    double rAmt1 = Math.Round(beData.ReceivedAmt, 2);
                    if (pAmt1 != rAmt1)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Fee Received Amount Does Not Match With Payment Mode";
                        return resVal;
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
