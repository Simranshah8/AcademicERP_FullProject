using Dynamic.BusinessEntity.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Transaction
{
    public class FeeReceipt : ResponeValues
    {
        public FeeReceipt()
        {
            ReceiptAsLedgerId = 1;
            PaymentModeColl = new FeePaymentModeCollections();
        }
        public int? TranId { get; set; }
        public int AutoVoucherNo { get; set; }
        public string AutoManualNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string Narration { get; set; }
        public string RefNo { get; set; }
        public int? StudentId { get; set; }
        public int? ClassId { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string Address { get; set; }
        public int PaidUpToMonth { get; set; }
        public double PreviousDues { get; set; }
        public double CurrentDues { get; set; }
        public double TotalDues { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public double FineAmt { get; set; }
        public double ReceivableAmt { get; set; }
        public double ReceivedAmt { get; set; }
        public double AfterReceivedDues { get; set; }
        public double AdvanceAmt { get; set; }
        public DateTime? CancelDateTime { get; set; }
        public int? CancelBy { get; set; }
        public string CancelRemarks { get; set; }
        public int CostClassId { get; set; }
        public int? AcademicYearId { get; set; }

        private FeeReceiptDetailsCollections _DetailsCollections = new FeeReceiptDetailsCollections();
        public FeeReceiptDetailsCollections DetailsColl
        {
            get { return _DetailsCollections; }
            set { _DetailsCollections = value; }
        }

        public int ReceiptAsLedgerId { get; set; }
        public int? ManualBillingTranId { get; set; }
        public int? AdmissionEnquiryId { get; set; }

        public int? RegistrationId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public double Waiver { get; set; }

        public FeePaymentModeCollections PaymentModeColl { get; set; }
        public bool MonthWise { get; set; }
        public double Opening { get; set; }
        public double? TenderAmount { get; set; }
    }
    public class FeeReceiptCollections : List<FeeReceipt> {

        public FeeReceiptCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class FeeReceiptDetails
    {
        public int StudentId { get; set; }
        public int TranId { get; set; }
        public int SNo { get; set; }
        public int? FeeItemId { get; set; }        
        public string FeeItemName { get; set; }
        public double PreviousDues { get; set; }
        public double CurrentDues { get; set; }
        public double TotalDues { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public double FineAmt { get; set; }
        public double ReceivableAmt { get; set; }
        public double ReceivedAmt { get; set; }
        public double AfterReceivedDues { get; set; }
        public double AdvanceAmt { get; set; }
        public bool IsManual { get; set; }
        public string Remarks { get; set; }
        public double DebitRate { get; set; }
        public double Rate { get; set; }
        public int PaidUpToMonthId { get; set; }

        public int? MonthId { get; set; }
        public string PrintName { get; set; }

        public string PeriodName { get; set; }
        public double Concession { get; set; }
        public double PartialPaidAmt { get; set; }
        public double Waiver { get; set; }
        public bool IsAdvance { get; set; }
        public bool IsOpening { get; set; }

    }
    public class FeeReceiptDetailsCollections : List<FeeReceiptDetails> { }

    public class FeePaymentMode
    {
        public int LedgerId { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }
    }
    public class FeePaymentModeCollections : List<FeePaymentMode>
    {

    }
    public class FeeReceiptNo : ResponeValues
    {
        public int CostClassId { get; set; }
        public int? AcademicYearId { get; set; }
        public int AutoNumber { get; set; }
        public string AutoManualNo { get; set; }
    }

    public class StudentFeeReceipt : ResponeValues
    {
        public StudentFeeReceipt()
        {
            ReceiptColl = new List<Receipt>();
            FeeItemWiseDuesColl = new List<FeeReceiptDetails>();
            SiblingStudentColl = new List<SiblingStudent>();
        }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string BillGenerateUpToMonth { get; set; }
        public double BillAmt { get; set; }
        public double TotalDebit { get; set; }
        public double TotalCredit { get; set; }
        public double PreviousDues { get; set; }
        public double CurrentDues { get; set; }
        public double TotalDues { get; set; }

        public List<Receipt> ReceiptColl { get; set; }
        public List<FeeReceiptDetails> FeeItemWiseDuesColl { get; set; }
        public List<SiblingStudent> SiblingStudentColl { get; set; }

        public string TransportRoute { get; set; }
        public string TransportPoint { get; set; }
        public string Hostel { get; set; }
        public int PaidUpToMonthId { get; set; }
        public string DisRemarks { get; set; } = "";
        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

        public string Remarks { get; set; }
        public int? StartMonthId { get; set; }
        public int? EndMonthId { get; set; }

        public int? BookLimit { get; set; }
        public int? BookCreditDays { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? ClassId { get; set; }
        public string LedgerPanaNo { get; set; } = "";

    }

    public class SiblingStudent
    {
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public bool IsParent { get; set; }
        public double DuesAmt { get; set; }
    }
    public class Receipt
    {
        public int TranId { get; set; }
        public string AutoManualNo { get; set; }
        public string RefNo { get; set; }
        public DateTime VoucherDate_AD { get; set; }
        public string VoucherDate_BS { get; set; }
        public double ReceivedAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double Total { get; set; }

        public int TranType { get; set; }
    }

    public class FeeReceipt_SENT 
    {
        public FeeReceipt_SENT()
        {
            ReceiptNo = "";
            ReceiptDate = "";
            ReceiptMiti = "";
            Narration = "";
            RefNo = "";
            StudentName = "";
            RegdNo = "";
            ClassName = "";
            SectionName = "";
            RollNo = "";
            FatherName = "";
            ContactNo = "";
            ReceiptAmt = "";
            DiscountAmt = "";
            AfterReceivedDues = "";
            FeeItemDetails = "";
            PaidUpToMonth = "";
            UserId = "";
            EmailId = "";
        }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string ReceiptMiti { get; set; }
        public string Narration { get; set; }
        public string RefNo { get; set; }
        public string StudentName { get; set; }
        public string RegdNo { get; set; } 
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string RollNo { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string ReceiptAmt { get; set; }
        public string DiscountAmt { get; set; }
        public string AfterReceivedDues { get; set; }
        public string FeeItemDetails { get; set; } 
        public string PaidUpToMonth { get; set; }
        public string UserId { get; set; }
        public string EmailId { get; set; }
    }

    public class ImportFeeReceipt
    {
        public string FiscalYear { get; set;  }
        public string Branch { get; set; }
        public int ReceiptNo { get; set; }
        public string AcademicYear { get; set; }
        public int MonthId { get; set; }
        public string RegdNo { get; set; }
        public DateTime? ReceiptDate_AD { get; set; }
        public string ReceiptDate_BS { get; set; }
        public string ReceiptAs { get; set; }
        public string FeeItem { get; set; }
        public double Amount { get; set; }
        public double DisPer { get; set; }
        public double DisAmt { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }
        public double TotalAmt { get; set; }
        public string Remarks { get; set; }

        public string StudentName { get; set; }
        public string Address { get; set; }
        public string RefNo { get; set; }
        public string ClassName { get; set; }
        public string ReceiptBy { get; set; }
    }
}
