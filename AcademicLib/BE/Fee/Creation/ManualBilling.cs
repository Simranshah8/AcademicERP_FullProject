using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class ManualBilling : ResponeValues
    {
        public ManualBilling()
        {
            BillingType = BILLINGTYPES.MEMO;
            CancelRemarks = "";
            AdvanceFeeItemColl = new ManualBillingDetailsCollections();
        }
        public int? TranId { get; set; }
        public int AutoNumber { get; set; }
        public DateTime BillingDate { get; set; }
        public int? StudentId { get; set; }
        public double TotalAmount { get; set; }

        private ManualBillingDetailsCollections _ManualBillingDetailsCollections = new ManualBillingDetailsCollections();
        public ManualBillingDetailsCollections ManualBillingDetailsColl
        {
            get { return _ManualBillingDetailsCollections; }
            set { _ManualBillingDetailsCollections = value; }
        }

        public ManualBillingDetailsCollections AdvanceFeeItemColl { get; set; }
        public string Remarks { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string StudentName { get; set; }
        public string Address { get; set; }
        public int RollNo { get; set; }
        public string BillingDate_BS { get; set; }
        public string RegdNo { get; set; }
        public BILLINGTYPES BillingType { get; set; }
        public int ForMonthId { get; set; }
        public string ForMonthName { get; set; }
        public bool IsCancel { get; set; }
        public string CancelRemarks { get; set; }

        public bool IsLeft { get; set; }
        public string BillingTypeName { get; set; }
        public string RefBillNo { get; set; }
        public bool IsCash { get; set; }

        public int? ClassId { get; set; }
        public int? SectionId { get; set; }

        public int? AdmissionEnquiryId { get; set; }
        public int? LedgerId { get; set; }
        public int? RegistrationId { get; set; }
        public string RefNo { get; set; }
        public double Waiver { get; set; }
        public double PaidAmt { get; set; }
        public double DuesAmt
        {
            get
            {
                return TotalAmount - PaidAmt;
            }
        }
        public AcademicLib.BE.Fee.Transaction.FeePaymentModeCollections PaymentModeColl { get; set; }
        public string LedgerName { get; set; }

        public string CancelBy { get; set; } = "";        
        public DateTime? CancelDateTime { get; set; }
        public string CancelMiti { get; set; } = "";

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string InvoiceNo { get; set; }

        public string Batch { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Semester { get; set; } = "";
        public string BillingDataBS { get; set; } = "";
        public int? BatchId { get; set; }
        public int? CostClassId { get; set; }
        public bool IsRateAsFeeMapping { get; set; }
    }
    public class ManualBillingCollections : List<ManualBilling> {
        public ManualBillingCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ManualBillingDetails
    {
        public int TranId { get; set; }
        public int SNo { get; set; }
        public int? FeeItemId { get; set; }
        public string FeeItemName { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public double PayableAmt { get; set; }
        public string Remarks { get; set; }
        public double Waiver { get; set; }
        public double PaidAmt { get; set; }
        public double DuesAmt { 
            get
            {
                return PayableAmt - PaidAmt;
            }
        }
        public int? MonthId { get; set; }
        public bool IsAdvance { get; set; }
        public double? TaxRate { get; set; }
        public double? TaxAmt { get; set; }
    }
    public class ManualBillingDetailsCollections : List<ManualBillingDetails> { }

    public class RegAutoManualNoData : ResponeValues
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ClassName { get; set; }
        public int ClassId { get; set; }
        public int? RegistrationId { get; set; }
        public int? AdmissionEnquiryId { get; set; }
    }
    public enum BILLINGTYPES
    {
        MEMO=1,
        SALESINVOICE=2,
        DEBITNOTE=3,
        CREDITNOTE=4
    }
}
