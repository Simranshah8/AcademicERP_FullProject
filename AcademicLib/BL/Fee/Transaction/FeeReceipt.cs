using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Transaction
{
    public class FeeReceipt
    {
        DA.Fee.Transaction.FeeReceiptDB db = null;
        int _UserId = 0;
        string _hostName, _dbName;
        public FeeReceipt(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Transaction.FeeReceiptDB(hostName, dbName);

            _hostName = hostName;
            _dbName = dbName;
        }
        public ResponeValues SaveFormData(int AcademicYearId,AcademicLib.BE.Fee.Transaction.FeeReceipt beData,bool nonStudent)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify,nonStudent);
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

        public ResponeValues UpdateFeeReceiptRef(  double Amount, string RefNo)
        {
            return db.UpdateFeeReceiptRef(_UserId,Amount, RefNo);
        }
        public ResponeValues ImportFeeReceipt( List<BE.Fee.Transaction.ImportFeeReceipt> dataColl)
        {
            return db.ImportFeeReceipt(_UserId,dataColl);
        }
            public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesDetails(int AcademicYearId, int StudentId, int? PaidUpToMonth, string PaidUpMonthColl, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            return db.getDuesDetails(_UserId,AcademicYearId, StudentId, PaidUpToMonth,PaidUpMonthColl,SemesterId,ClassYearId,BatchId);
        }
        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesForAdmission(int AcademicYearId, int ClassId, int StudentId, int PaidUpToMonth, int? SemesterId, int? ClassYearId, int? RouteId, int? PointId, int? BedId)
        {
            return db.getDuesForAdmission(_UserId, AcademicYearId, ClassId, StudentId, PaidUpToMonth, SemesterId, ClassYearId, RouteId, PointId, BedId);
        }
            public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getSiblingDuesDetails(int AcademicYearId, int StudentId, int? PaidUpToMonth)
        {
            return db.getSiblingDuesDetails(_UserId,AcademicYearId, StudentId, PaidUpToMonth);
        }
        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesDetailsForWallet(int AcademicYearId,int? UptoMonthId)
        {
            return db.getDuesDetailsForWallet(_UserId,AcademicYearId,UptoMonthId);
        }
            public AcademicLib.BE.Fee.Transaction.FeeReceiptCollections GetAllFeeReceipt(int AcademicYearId, int EntityId)
        {
            return db.getAllFeeReceipt(_UserId,AcademicYearId, EntityId);
        }
        public AcademicLib.BE.Fee.Transaction.FeeReceipt GetFeeReceiptById(int EntityId, int TranId)
        {
            return db.getFeeReceiptById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public AcademicLib.RE.Fee.FeeReceiptCollections getFeeReceiptCollection(int AcademicYearId,DateTime? dateFrom, DateTime? dateTo, bool showCancel, int? fromReceipt, int? toReceipt, ref double openingAmt, ref double openingDisAmt)
        {
            return db.getFeeReceiptCollection(_UserId,AcademicYearId, dateFrom, dateTo, showCancel,fromReceipt,toReceipt,ref openingAmt,ref openingDisAmt);
        }
        public AcademicLib.RE.Fee.FeeSummaryCollections getFeeSummary(int AcademicYearId, int fromMonthId, int toMontherId, int forStudent,string feeItemIdColl,int? classId,int? sectionId, int? batchId, int? semesterId, int? classYearId, bool ForPaymentFollowup, int FollowupType,DateTime? dateFrom,DateTime? dateTo)
        {
            if (feeItemIdColl == "0")
                feeItemIdColl = "";

            if (classId.HasValue && classId.Value == 0)
                classId = null;

            return db.getFeeSummary(_UserId,AcademicYearId, fromMonthId, toMontherId, forStudent,feeItemIdColl,classId,sectionId,batchId,semesterId,classYearId,ForPaymentFollowup,FollowupType,dateFrom,dateTo);
        }
        public AcademicLib.RE.Fee.FeeSummaryCollections getFeeSummaryDateWise(int? AcademicYearId, string feeItemIdColl, DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getFeeSummaryDateWise(_UserId, AcademicYearId, feeItemIdColl, dateFrom, dateTo);
        }
            public AcademicLib.RE.Fee.FeeSummary_PCCollections getFeeSummary_PC(int AcademicYearId, int fromMonthId, int toMontherId, int forStudent, string feeItemIdColl, int? classId, int? sectionId)
        {
            if (feeItemIdColl == "0")
                feeItemIdColl = "";

            if (classId.HasValue && classId.Value == 0)
                classId = null;

            return db.getFeeSummary_PC(_UserId, AcademicYearId, fromMonthId, toMontherId, forStudent, feeItemIdColl, classId, sectionId);
        }

        public ResponeValues SendEmail(int TranId)
        {
            return db.SendEmail(_UserId, TranId);
        }
            public AcademicLib.RE.Fee.OnlinePaymentCollections getOnlinePaymentList(int AcademicYearId, DateTime dateFrom, DateTime dateTo)
        {
            return db.getOnlinePaymentList(_UserId,AcademicYearId, dateFrom, dateTo);
        }
            public AcademicLib.API.Student.Fee getStudentFee(int? AcademicYearId)
        {
            return db.getStudentFee(_UserId,AcademicYearId);
        }
        public AcademicLib.API.Student.Fee getStudentFeeForWallet(int AcademicYearId)
        {
            return db.getStudentFeeForWallet(_UserId,AcademicYearId);
        }
            public AcademicLib.RE.Fee.DateWiseIncomeCollections getDateWiseFeeIncome(int AcademicYearId, int? MonthId, DateTime? dateFrom, DateTime? dateTo,int? fromReceipt,int? toReceipt)
        {
            return db.getDateWiseFeeIncome(_UserId,AcademicYearId, MonthId, dateFrom, dateTo,fromReceipt,toReceipt);
        }

        public AcademicLib.RE.Fee.StudentVoucher getStudentVoucher(int AcademicYearId, int StudentId, int? SemesterId, int? ClassYearId)
        {
            return db.getStudentVoucher(_UserId,AcademicYearId, StudentId,SemesterId,ClassYearId);            
        }
        public AcademicLib.RE.Fee.StudentVoucher getStudentLedger(int AcademicYearId, int StudentId, int? SemesterId, int? ClassYearId)
        {
            return db.getStudentLedger(_UserId,AcademicYearId, StudentId,SemesterId,ClassYearId);
        }
        public AcademicLib.RE.Fee.ReminderSlipCollections getReminderSlip(int AcademicYearId, int UptoMonthId, int forStudent,int? classId,int? sectionId, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            if (classId.HasValue && classId == 0)
                classId = null;

            if (sectionId.HasValue && sectionId == 0)
                sectionId = null;

            return db.getReminderSlip(_UserId,AcademicYearId, UptoMonthId, forStudent,classId,sectionId,BatchId,ClassYearId,SemesterId);
        }
        public AcademicLib.RE.Fee.FeeSummaryClassWiseCollections getFeeSummaryClassWise(int AcademicYearId, int fromMonthId, int toMontherId, int forStudent)
        {
            return db.getFeeSummaryClassWise(_UserId,AcademicYearId, fromMonthId, toMontherId, forStudent);
        }
        public AcademicLib.RE.Fee.FeeSummaryStudentWiseCollections getFeeSummaryStudentWise(int AcademicYearId, int ClassId, int? SectionId, int fromMonthId, int toMontherId, int forStudent)
        {
            return db.getFeeSummaryStudentWise(_UserId,AcademicYearId, ClassId, SectionId, fromMonthId, toMontherId, forStudent);
        }
        public AcademicLib.RE.Fee.FeeIncomeClassWiseCollections getFeeIncomeClassWise(int AcademicYearId, DateTime dateFrom, DateTime dateTo, string feeItemIdColl)
        {
            if (feeItemIdColl == "0")
                feeItemIdColl = "";

            return db.getFeeIncomeClassWise(_UserId,AcademicYearId, dateFrom, dateTo,feeItemIdColl);
        }
        public AcademicLib.RE.Fee.FeeIncomeStudentWiseCollections getFeeIncomeStudentWise(int AcademicYearId, int ClassId, int? SectionId, DateTime dateFrom, DateTime dateTo, string feeItemIdColl)
        {
            if (feeItemIdColl == "0")
                feeItemIdColl = "";

            return db.getFeeIncomeStudentWise(_UserId,AcademicYearId, ClassId, SectionId, dateFrom, dateTo,feeItemIdColl);
        }
        public AcademicLib.API.Admin.DailyFeeReceipt admin_DailyCollection(int AcademicYearId, DateTime? dateFrom, DateTime? dateTo)
        {
            return db.admin_DailyCollection(_UserId,AcademicYearId, dateFrom, dateTo);
        }
        public AcademicLib.API.Admin.ClassWiseFeeSummaryCollections admin_ClassWiseFeeSummary(int? StudentId, int fromMonthId, int toMonthId,int forStudent, int? ClassId, int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.admin_ClassWiseFeeSummary(_UserId,StudentId, fromMonthId, toMonthId, forStudent,ClassId,SectionId,BatchId,SemesterId,ClassYearId);
        }
            public ResponeValues GenerateStudentCostCenter()
        {
            return db.GenerateStudentCostCenter(_UserId);
        }
        public ResponeValues GenerateFeeReceiptToJournal(int AcademicYearId,bool IsReGenerate)
        {
            return db.GenerateFeeReceiptToJournal(_UserId,AcademicYearId,IsReGenerate);
        }
        public ResponeValues GenerateEmployeeCostCenter()
        {
            return db.GenerateEmployeeCostCenter(_UserId);
        }
        public AcademicLib.BE.Fee.Transaction.CostCenterGenerateLogCollections getCostCenterGenerateLog()
        {
            return db.getCostCenterGenerateLog(_UserId);
        }

        public AcademicLib.API.Admin.FeeDuesCollections admin_DuesList(int AcademicYearId, int? classId,int? sectionId, int UpToMonthId = 0,int ForStudent=0, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.admin_DuesList(_UserId,AcademicYearId, classId,sectionId, UpToMonthId,ForStudent,BatchId,SemesterId,ClassYearId);
        }
        public AcademicLib.API.Admin.FeeHeadingWiseFeeSummaryCollections admin_FeeHeadingWiseFeeSummary(int? classId, int? sectionId, int UpToMonthId = 0,int? AcademicYearId=null)
        {
            return db.admin_FeeHeadingWiseFeeSummary(_UserId, UpToMonthId, classId, sectionId,AcademicYearId );
        }
        public AcademicLib.BE.Fee.Transaction.FeeReceipt_SENT getFeeForSMS(int AcademicYearId, int TranId)
        {
            return db.getFeeForSMS(_UserId,AcademicYearId, TranId);
        }
        public AcademicLib.RE.Fee.ClassAndFeeItemWiseCollections getFeeSummaryClassAndFeeItemWise( int AcademicYearId, int FromMonthId, int ToMonthId, int ForStudent)
        {
            return db.getFeeSummaryClassAndFeeItemWise(_UserId, AcademicYearId, FromMonthId, ToMonthId, ForStudent);
        }
            public ResponeValues IsValidData(ref AcademicLib.BE.Fee.Transaction.FeeReceipt beData, bool IsModify,bool nonStudent)
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
                else if (nonStudent==false && (!beData.StudentId.HasValue || beData.StudentId.Value==0))
                {
                    resVal.ResponseMSG = "Please ! Select Student Name";
                }
                else if (beData.CostClassId==0 && (!beData.ManualBillingTranId.HasValue || beData.ManualBillingTranId==0))
                {
                    resVal.ResponseMSG = "Please ! Select CostClass Name";
                }
                else if (beData.DetailsColl ==null || beData.DetailsColl.Count==0)
                {
                    resVal.ResponseMSG = "No Fee Details Found";
                }else if (beData.VoucherDate > DateTime.Today)
                {
                    resVal.ResponseMSG = "Please ! Enter Receipt Date less than equal today";
                }
                else
                {

                    if (beData.PaidUpToMonth == 0)
                        beData.PaidUpToMonth = 1;

                    var feeConfig = new AcademicLib.BL.Fee.Setup.FeeConfiguration(_UserId, _hostName, _dbName).GetFeeConfigurationById(0,beData.AcademicYearId);

                    beData.MonthWise = feeConfig.MonthWiseFeeHeading || feeConfig.ShowDuesFeeHeadingInReceipt;

                    if (beData.PaymentModeColl == null)
                        beData.PaymentModeColl = new BE.Fee.Transaction.FeePaymentModeCollections();

                    if (beData.AdmissionEnquiryId.HasValue && beData.AdmissionEnquiryId == 0)
                        beData.AdmissionEnquiryId = null;


                    if (beData.ReceiptAsLedgerId == 0)
                        beData.ReceiptAsLedgerId = 1;

                    double itemAmt = Math.Round(beData.DetailsColl.Sum(p1=>p1.ReceivedAmt),2), itemDiscount = Math.Round(beData.DetailsColl.Sum(p1=>p1.DiscountAmt),2), itemFine = Math.Round(beData.DetailsColl.Sum(p1=>p1.FineAmt),2);
                    double itemWaiver = Math.Round(beData.DetailsColl.Sum(p1 => p1.Waiver), 2);
                    double totalAmt =Math.Round(beData.ReceivedAmt,2), totalDiscount = Math.Round(beData.DiscountAmt,2), totalFine = Math.Round(beData.FineAmt,2);
                    double totalWaiver = Math.Round(beData.Waiver, 2);

                    if (totalAmt != itemAmt)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Fee Heading Wise Received And Total Received Does Not Match "+itemAmt.ToString()+"="+totalAmt.ToString();
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

                    double pAmt1 = Math.Round(beData.PaymentModeColl.Sum(p1 => p1.Amount),2);
                    double rAmt1 = Math.Round(beData.ReceivedAmt, 2);
                    if (pAmt1 != rAmt1)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Fee Received Amount Does Not Match With Payment Mode";
                        return resVal;
                    }

                    if(totalDiscount==0 && totalWaiver==0 && totalFine==0 && totalAmt == 0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Received Amount";
                        return resVal;
                    }

                    beData.Opening = beData.DetailsColl.Where(p1 => p1.IsOpening == true).Sum(p1 => p1.ReceivedAmt);

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
