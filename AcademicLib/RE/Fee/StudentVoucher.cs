using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class StudentVoucher : ResponeValues
    {
        public StudentVoucher()
        {
            VoucherColl = new List<Voucher>();
            LedgerDetailsColl = new List<LedgerDetails>();
            Name = "";
            RegNo = "";
            ClassName = "";
            SectionName = "";
            FatherName = "";
            MotherName = "";
            ContactNo = "";
            Address = "";
            PhotoPath = "";
            BillUpToMonth = "";
        }
        public int StudentId { get; set; }

        public string Name { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string BillUpToMonth { get; set; }
        public double OpeningAmt { get; set; }
        public double FeeAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double PaidAmt { get; set; }
        public double BalanceAmt { get; set; }
        public List<Voucher> VoucherColl { get; set; }
        public List<LedgerDetails> LedgerDetailsColl { get; set; }

        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public string AcademicYear { get; set; }
    }

    public class Voucher
    {
        public Voucher()
        {
            DetailsColl = new List<VoucherDetails>();
            VoucherDate_BS = "";
            VoucherType = "";
            VoucherNo = "";
            RefNo = "";
            Particulars = "";
            Narration = "";
            UserName = "";
        }
        public int TranType { get; set; }
        public int TranId { get; set; }
        public DateTime VoucherDate_AD { get; set; }
        public string VoucherDate_BS { get; set; }
        public string VoucherType { get; set; }
        public string VoucherNo { get; set; }
        public string RefNo { get; set; }
        public string Particulars { get; set; }
        public double Amount { get; set; }
        public double DisAmt { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string Narration { get; set; }
        public string UserName { get; set; }
        public DateTime LogDateTime { get; set; }

        public int? ATranId { get; set; }
        public string AVoucherNo { get; set; }
        public List<VoucherDetails> DetailsColl { get; set; }
        public double CurClosing { get; set; }

        public bool IsParent { get; set; } = true;
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }

    public class VoucherDetails
    {
        public VoucherDetails()
        {
            FeeItem = "";
            FeeSource = "";
            IsManual = "";
            Remarks = "";
        }

        public string FeeItem { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double DisAmt { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }
        public double PayableAmt { get; set; }
        public double ReceivedAmt { get; set; }
        public string FeeSource { get; set; }
        public string IsManual { get; set; }
        public string Remarks { get; set; }

    }
    public class LedgerDetails
    {
        public LedgerDetails()
        {
            Particular = "";
            FeeHeading = "";
            RefNo = "";
            VoucherDate = "";
            Details = "";
        }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public int TranType { get; set; }
        public string Particular { get; set; }
        public string FeeHeading { get; set; }
        public int FeeItemOrderNo { get; set; }
        public double PDues { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Paid { get; set; }
        public double Discount { get; set; }
        public string VoucherNo { get; set; }
        public string RefNo { get; set; }
        public string VoucherDate { get; set; }
        public string Details { get; set; }
        public double BalanceAmt { get; set; }
        public string Narration { get; set; }
    }
}
