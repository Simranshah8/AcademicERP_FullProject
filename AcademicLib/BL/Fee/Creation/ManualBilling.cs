using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class ManualBilling
    {
        DA.Fee.Creation.ManualBillingDB db = null;
        int _UserId = 0;
        string _hostName, _dbName;

        public ManualBilling(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            this._hostName = hostName;
            this._dbName = dbName;
            db = new DA.Fee.Creation.ManualBillingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId,BE.Fee.Creation.ManualBilling beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
            {
                isValid= db.SaveUpdate(AcademicYearId, beData, isModify);

                if(isValid.IsSuccess && beData.IsCash && beData.BillingType==BE.Fee.Creation.BILLINGTYPES.SALESINVOICE)
                {
                    if(beData.AdvanceFeeItemColl!=null)
                    {
                        foreach (var af in beData.AdvanceFeeItemColl)
                        {
                            af.IsAdvance = true;
                            beData.ManualBillingDetailsColl.Add(af);
                        }                            
                    }

                    var monthColl = new AcademicLib.BL.Global(_UserId, _hostName, _dbName).getAcademicYearMonthList(AcademicYearId, beData.StudentId, null);

                    var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(_UserId,_hostName,_dbName ).GetFeeConfigurationById(0,AcademicYearId);
                    var costClassColl = new Dynamic.DataAccess.Account.CostClassDB(_hostName, _dbName).getAllCostClass(_UserId);
                    AcademicLib.BE.Fee.Transaction.FeeReceipt receipt = new BE.Fee.Transaction.FeeReceipt();
                    receipt.PaymentModeColl = beData.PaymentModeColl;
                    receipt.Waiver = beData.Waiver;
                    receipt.AdmissionEnquiryId = beData.AdmissionEnquiryId;
                    receipt.RegistrationId = beData.RegistrationId;
                    receipt.TranId = 0;
                    receipt.CUserId = this._UserId;
                    receipt.StudentId = beData.StudentId;
                    receipt.ManualBillingTranId = isValid.RId;
                    receipt.ReceivableAmt = beData.ManualBillingDetailsColl.Sum(p1=>p1.PayableAmt);
                    receipt.ReceivedAmt = beData.ManualBillingDetailsColl.Sum(p1 => p1.PaidAmt);
                    receipt.AfterReceivedDues = beData.TotalAmount - beData.PaidAmt;
                    receipt.Narration = beData.Remarks;
                    receipt.PaidUpToMonth = 0;                    
                    if (monthColl!=null && monthColl.Count > 0)
                    {
                        var findMN = monthColl.Find(p1 => p1.MSNo == beData.ForMonthId);
                        if (findMN != null)
                            receipt.PaidUpToMonth = findMN.NM;
                    }
                    if(receipt.PaidUpToMonth==0)
                        receipt.PaidUpToMonth = beData.ForMonthId;

                    receipt.VoucherDate = beData.BillingDate;
                    receipt.SemesterId = beData.SemesterId;
                    receipt.ClassYearId = beData.ClassYearId;

                    if(beData.LedgerId.HasValue && beData.LedgerId>0)
                        receipt.ReceiptAsLedgerId = beData.LedgerId.Value;
                    else
                        receipt.ReceiptAsLedgerId = feeConfig.FeeReceiptLedgerId.Value;

                    receipt.DetailsColl = new BE.Fee.Transaction.FeeReceiptDetailsCollections();
                    receipt.CostClassId = 0;// (costClassColl == null || costClassColl.Count == 0 ? 1 : costClassColl[0].CostClassId);

                    receipt.StudentName = beData.StudentName;
                    receipt.ClassName = beData.ClassName;
                    receipt.Address = beData.Address;
                    receipt.ClassId = beData.ClassId;
                    receipt.SemesterId = beData.SemesterId;
                    receipt.ClassYearId = beData.ClassYearId;

                    if (!string.IsNullOrEmpty(beData.RefNo))
                        receipt.RefNo = beData.RefNo;
                    else if (!string.IsNullOrEmpty(beData.RefBillNo))
                        receipt.RefNo = beData.RefBillNo;
                     
                    int sno = 1;
                    foreach(var det in beData.ManualBillingDetailsColl)
                    {
                        receipt.DetailsColl.Add(new BE.Fee.Transaction.FeeReceiptDetails()
                        {
                            FeeItemId = det.FeeItemId,
                            FeeItemName = det.FeeItemName,
                            ReceivableAmt = det.PayableAmt,
                            ReceivedAmt = det.PaidAmt,
                            DiscountAmt=det.DiscountAmt,
                            DiscountPer=det.DiscountPer,      
                            Waiver=det.Waiver,
                            Rate = det.Rate,
                            SNo = sno,
                            MonthId=det.MonthId,
                            IsAdvance=det.IsAdvance
                        }) ;
                        
                        sno++;
                    }

                    receipt.DiscountAmt = receipt.DetailsColl.Sum(p1 => p1.DiscountAmt);

                    bool nonStudent = false;

                    if ((!beData.StudentId.HasValue || beData.StudentId.Value == 0) && !string.IsNullOrEmpty(beData.StudentName))
                        nonStudent = true;

                    isValid = new AcademicLib.BL.Fee.Transaction.FeeReceipt(_UserId, _hostName, _dbName).SaveFormData(AcademicYearId, receipt,nonStudent);
                }

                return isValid;
            }                
            else
                return isValid;
        }
        public BE.Fee.Creation.ManualBillingCollections GetAllManualBilling(int AcademicYearId,int EntityId)
        {
            return db.getAllManualBilling(_UserId,AcademicYearId, EntityId);
        }
        public BE.Fee.Creation.ManualBilling GetManualBillingById(int EntityId, int TranId)
        {
            return db.getManualBillingById(_UserId, EntityId, TranId);
        }

        public AcademicLib.RE.Fee.ManualBillingCollections getManualBillingDetails(int AcademicYearId,DateTime? dateFrom, DateTime? dateTo,bool? IsCancel)
        {
            return db.getManualBillingDetails(_UserId,AcademicYearId, dateFrom, dateTo,IsCancel);
        }
            public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues getAutoNo(int AcademicYearId,int? CostClassId)
        {
            return db.getAutoNo(_UserId,AcademicYearId,CostClassId);
        }
        public ResponeValues Cancel(BE.Fee.Creation.ManualBilling beData)
        {
            return db.Cancel(beData);
        }
            public ResponeValues getFeeRate(int AcademicYearId, int StudentId, int FeeItemId)
        {
            return db.getFeeRate(_UserId,AcademicYearId, StudentId, FeeItemId);
        }
        public AcademicLib.BE.Fee.Creation.ManualBilling getManualBillingDetailsById( int TranId)
        {
            return db.getManualBillingDetailsById(_UserId, TranId);
        }
        public AcademicLib.RE.Fee.ManualBillingDetailsCollections getBillingDetails(DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getBillingDetails(_UserId, dateFrom, dateTo);
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
                //else if (beData.StudentId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select Valid Student";
                //}
                else if (beData.BillingDate.Year < 2000)
                {
                    resVal.ResponseMSG = "Please ! Enter Billing Date";
                }else if(beData.ManualBillingDetailsColl==null || beData.ManualBillingDetailsColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Billing Details";
                }else if(beData.BillingType==BE.Fee.Creation.BILLINGTYPES.MEMO && beData.ForMonthId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select For Month Name";
                }else if (beData.IsCash && IsModify)
                {
                    resVal.ResponseMSG = "Cash bill was not modify. pls cancel (bill and receipt both).";
                }                
                else
                {

                    if (beData.BillingType == BE.Fee.Creation.BILLINGTYPES.SALESINVOICE)
                    {
                        if (beData.LedgerId.HasValue && beData.LedgerId > 0)
                            beData.IsCash = true;
                        else
                            beData.IsCash = false;
                    }
                    
                    
                    var detailsColl = beData.ManualBillingDetailsColl;

                    beData.ManualBillingDetailsColl = new BE.Fee.Creation.ManualBillingDetailsCollections();
                    int sno = 1;
                    foreach(var v in detailsColl)
                    {
                        if(v.FeeItemId.HasValue && v.FeeItemId.Value > 0)
                        {
                            if(v.Qty!=0 || v.PayableAmt!=0 || v.Rate != 0)
                            {
                                double pAmt =Math.Round(v.PayableAmt,2);
                                double amt =Math.Round(v.Qty * v.Rate,2);
                                double disAmt =Math.Round(v.DiscountAmt,2);
                                double waiverAmt = Math.Round(v.Waiver, 2);
                                double taxAmt = v.TaxAmt.HasValue ? v.TaxAmt.Value : 0;
                                if (pAmt != (amt - disAmt-waiverAmt+taxAmt))
                                {
                                    resVal.ResponseMSG = "Qt X Rate = Amount - Discount-Waiver = Payable Amount does not match";
                                    return resVal;
                                }
                                else
                                {
                                    v.SNo = sno;

                                    if (v.PaidAmt == 0)
                                        v.PaidAmt = v.PayableAmt;
                                    
                                    beData.ManualBillingDetailsColl.Add(v);
                                    sno++;
                                }
                                    
                            }
                        }

                    }

                    if (string.IsNullOrEmpty(beData.StudentName))
                        beData.StudentName = "";

                    if (string.IsNullOrEmpty(beData.Address))
                        beData.Address = "";
                    
                    if (string.IsNullOrEmpty(beData.ClassName))
                        beData.ClassName = "";


                    if (beData.AdmissionEnquiryId.HasValue && beData.AdmissionEnquiryId == 0)
                        beData.AdmissionEnquiryId = null;

                    beData.TotalAmount = beData.ManualBillingDetailsColl.Sum(p1 => p1.PayableAmt);
                    beData.PaidAmt = beData.ManualBillingDetailsColl.Sum(p1 => p1.PaidAmt);                    
                    
                    if(beData.ManualBillingDetailsColl.Count==0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Details ";
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
        public BE.Fee.Creation.RegAutoManualNoData GetDataFromRegAutoManualNo(string RegNo, string AutoManualNo, int EntityId)
        {
            return db.getDataFromRegAutoManualNo(RegNo, AutoManualNo, _UserId, EntityId);
        }
    }
}
