using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Setup
{
    public class BillConfiguration : ResponeValues
    {

        public BillConfiguration()
        {
            NumberingMethod = 1;
            NumericalPartWidth = 0;
            Prefix = "";
            Suffix = "";
            StartNumber = 1;
            DateStyle = 1;
            NoOfDecimal = 2;
            BillingHeading = "";
            BillingHeadingFont = "";
            BillingNotesFont = "";
            BillingNotes = "";
            ReminderFont = "";
            ReminderNotes = "";
        }
        public int NumberingMethod { get; set; }
        public int NumericalPartWidth { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartNumber { get; set; }
        public int DateStyle { get; set; }
        public int DateFormat { get; set; }
        public int NoOfDecimal { get; set; }
        public string BillingHeadingFont { get; set; }
        public string BillingHeading { get; set; }
        public string BillingNotesFont { get; set; }
        public string BillingNotes { get; set; }
        public string ReminderFont { get; set; }
        public string ReminderNotes { get; set; }
        public bool ShowPreDuesFeeHeading { get; set; }

        public int BillNoAs { get; set; } = 1;
        public bool ShowLeftStudentInOpening { get; set; }
        public bool ShowLeftStudentInFeeMapping { get; set; }
        public string OpeningDuesLabel { get; set; } = "Previous Dues";
        public int CalculateManualBillAs { get; set; } = 1;
        public bool IgnoreCashSalesReceiptInBillPrint { get; set; }
        public int DiscountEffectAs { get; set; } = 1;
        public bool ActiveTaxOnManualBilling { get; set; }
        public bool ActiveStudentType { get; set; }
        public bool ActiveRoundOff { get; set; }
        public bool ActiveForceDiscount { get; set; }
    }

}
