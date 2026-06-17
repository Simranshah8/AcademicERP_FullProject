using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class DailyFeeReceipt : ResponeValues
    {
        public DailyFeeReceipt()
        {
            FeeHeadingWiseColl = new List<FeeItem>();
            ReceiptColl = new List<FeeReceipt>();
        }
        public List<FeeItem> FeeHeadingWiseColl { get; set; }
        public List<FeeReceipt> ReceiptColl { get; set; }
    }

    public class FeeItem
    {
        public int SNo { get; set; }
        public int FeeItemId { get; set; }
        public string FeeHeading { get; set; }
        public double ReceivedAmt { get; set; }
        public double DiscountAmt { get; set; }
    }
    public class FeeReceipt
    { 
        public FeeReceipt()
        {
            FeeHeadingWiseColl = new List<FeeItem>();
            Name = "";
            RefNo = "";
            ClassName = "";
            SectionName = "";
            FatherName = "";
            ContactNo = "";
            RegNo = "";
            FatherName = "";
            ContactNo = "";
            PhotoPath = "";
        }
        public int TranId { get; set; }
        public int StudentId { get; set; }
        public int AutoVoucherNo { get; set; }
        public string RefNo { get; set; }
        public DateTime VoucherDateAD { get; set; }
        public string VoucherDateBS { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public double DiscountAmt { get; set; }
        public double ReceivedAmt { get; set; }
        public double DuesAmt { get; set; }
        public string PhotoPath { get; set; }
        public List<FeeItem> FeeHeadingWiseColl { get; set; }
    }
}
