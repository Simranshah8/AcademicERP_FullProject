using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class BillGenerate : ResponeValues
    {
        public int ClassId { get; set; }
        public int MonthId { get; set; }
        public DateTime BillDate { get; set; }
        public int? StudentId { get; set; }
        public int? FromMonthId { get; set; }
        public int? ToMonthId { get; set; }
        public int? StudentTypeId { get; set; }

        public int? BatchId { get; set; }
        public int? FacultyId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public List<BillGenerateDetails> DetailsColl { get; set; }
    }

    public class BillGenerateDetails
    {
        public int? FeeItemId { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        
    }

    public class BillGenerate_SENT
    {
        public BillGenerate_SENT()
        {
            UserId = "";
            RegNo = "";
            RollNo = "";
            Name = "";
            ClassName = "";
            SectionName = "";
            FatherName = "";
            ContactNo = "";
            Address = "";
            BillNo = "";
            BillDate = "";
            BillMiti = "";
            PreviousDues = "";
            FeeAmount = "";
            DiscountAmt = "";
            TaxAmt = "";
            FineAmt = "";
            CurrentFeeAmt = "";
            TotalDues = "";
            ForMonth = "";
        }
        public string UserId { get; set; }
        public string RegNo { get; set; }
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string BillNo { get; set; }
        public string BillDate { get; set; }
        public string BillMiti { get; set; }
        public string PreviousDues { get; set; }
        public string FeeAmount { get; set; }
        public string DiscountAmt { get; set; }
        public string TaxAmt { get; set; }
        public string FineAmt { get; set; }
        public string CurrentFeeAmt { get; set; }
        public string TotalDues { get; set; }
        public string ForMonth { get; set; }
        public string EmailId { get; set; }
        public int StudentId { get; set; }
    }

    public class BillGenerate_SENTCollections : System.Collections.Generic.List<BillGenerate_SENT>
    {

    }

    public class ImportBillGenerate
    {
        public string FiscalYear { get; set;  }
        public string Branch { get; set; }
        public int BillNo { get; set; }
        public string AcademicYear { get; set; }
        public int MonthId { get; set; }
        public string RegdNo { get; set; }
        public DateTime? BillDate_AD { get; set; }
        public string BillDate_BS { get; set; }
        public string FeeItem { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double DisPer { get; set; }
        public double DisAmt { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }
        public double PayableAmt { get; set; }
        public string Remarks { get; set; }
    }

}
