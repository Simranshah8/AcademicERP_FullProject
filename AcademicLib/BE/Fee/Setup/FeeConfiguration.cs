using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Setup
{
   public class FeeConfiguration : ResponeValues
    {
        public FeeConfiguration()
        {
            FineHeading = "";
            TaxHeading = "";
            NumberingMethod = 1;
            NumericalPartWidth = 0;
            Prefix = "";
            Suffix = "";
            StartNumber = 1;
            DateStyle = 1;
            DateFormat = 1;
            NoOfDecimal = 2;
            Notes = "";
            ReceiptMonthAs = 1;
            DefaulterDuesColl = new List<FeeDefaulterMinDues>();
        }
        public int? TransportFeeItemId { get; set; }
        public int? HostelFeeItemId { get; set; }
        public int? LibraryFeeItemId { get; set; }
        public int? CanteenFeeItemId { get; set; }
        public int? FineFeeItemId { get; set; }
        public int? TaxFeeItemId { get; set; }
        public int? FixedStudentFeeItemId { get; set; }
        public int? FeeReceiptLedgerId { get; set; }
        public int? DiscountLedgerId { get; set; }
        public int? TaxLedgerId { get; set; }
        public int? FineLedgerId { get; set; }
        public int? FixedStudentLedgerId { get; set; }
        public int? SalesPartyLedgerId { get; set; }
        public string FineHeading { get; set; }
        public string TaxHeading { get; set; }
        public int NumberingMethod { get; set; }
        public int NumericalPartWidth { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartNumber { get; set; }
        public int DateStyle { get; set; }
        public int DateFormat { get; set; }
        public int NoOfDecimal { get; set; }
        public int FeeMapping { get; set; }
        public string Notes { get; set; }
        public bool SendAutoSMS { get; set; }
        public bool IRDEnabled { get; set; }
        public int ReceiptMonthAs { get; set; }
        public bool ReceiptDateValidateInBillPrint { get; set; }
        public bool ActiveMemoBilling { get; set; }
        public bool ShowRate { get; set; }
        public bool ShowOpeningHeadingWise { get; set; }
        public bool AllowDiscount { get; set; }
        public bool AllowDiscountInRegistration { get; set; }
        public bool AllowDiscountInEnquiry { get; set; }
        public bool AllowFine { get; set; }
        public bool AdmitDateEffectInBillGenerate { get; set; }

        public bool SiblingBillPrint { get; set; }
        public bool SiblingStudentLedger { get; set; }
        public bool SiblingStudentVoucher { get; set; }
        public bool SiblingFeeReminder { get; set; }

        public int? BillGenerate_VoucherId { get; set; }
        public int? FeeReceipt_VoucherId { get; set; }
        public int? DebitNote_VoucherId { get; set; }
        public int? CreditNote_VoucherId { get; set; }
        public int? AdvanceFeeItemId { get; set; }

        public bool MonthWiseFeeHeading { get; set; }
        public List<FeeDefaulterMinDues> DefaulterDuesColl { get; set; }
        public bool ShowLeftStudentInDiscountSetup { get; set; }

        public int JVPending { get; set; }
        public int SIPending { get; set; }
        public bool ShowOnlyGenerateBillInReceipt { get; set; }
        public bool ShowDuesFeeHeadingInReceipt { get; set; }        
        public bool AllowMultiplePaymentmode { get; set; }
        public List<Dynamic.BusinessEntity.Global.ReportTempletes> ReportTemplateList { get; set; }

        public string OpeningFeeMonth { get; set; }
        public bool AllowMonthWiseOnlinePayment { get; set; }
        public bool AllowValidateTotalDues { get; set; }
        public bool MonthNameAsBillDate_MB { get; set; }
        //Added
        public bool InclBillDisSL { get; set; } 
    }


    public class FeeDefaulterMinDues : ResponeValues
    {
        public int ClassId { get; set; }
        public double DuesAmt { get; set; }
        public string ClassName { get; set; }

        public int? ReceiptTemplateId { get; set; }
        public int? BillTemplateId { get; set; }
        public int CreditDays { get; set; }
        public DateTime? BillGenerateOn { get; set; }
        public int? BillGenerateDay { get; set; }
    }
} 