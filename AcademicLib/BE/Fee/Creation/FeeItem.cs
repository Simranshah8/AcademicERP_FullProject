using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeItem : ResponeValues
    {
        public int? FeeItemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PrintName { get; set; }
        public int HeadFor { get; set; }
        public int? LedgerId { get; set; }
        public int? ProductId { get; set; }
        public bool ApplyTax { get; set; }
        public double TaxRate { get; set; }
        public int OrderNo { get; set; }
        public bool OneTimeApplicable { get; set; }
        public bool OnlyForNewStudent { get; set; }
        public bool OnlyForOldStudent { get; set; }
        public bool OnlyForHostel { get; set; }
        public bool OnlyForTransport { get; set; }
        public bool OnlyForFixedStudent { get; set; }
        public bool IsExtraFee { get; set; }
        public bool RefundableFee { get; set; }
        public bool ScholarshipApplicable { get; set; }
        public bool ApplyFine { get; set; }

        public bool IsSecurityDeposit { get; set; }
        public List<int> MonthIdColl { get; set; } 
        public bool NotApplicableForHostel { get; set; }
        public bool NotApplicableForTransport { get; set; }

        public string Ledger { get; set; } = "";
        public string LedgerGroup { get; set; } = "";
        public string Product { get; set; } = "";
        public string ProductType { get; set; } = "";
    }

    public class FeeItemCollections : System.Collections.Generic.List<FeeItem>
    {
        public FeeItemCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public enum HEADFOR
    {
        GeneralFees = 1,
        TransportFees = 2,
        HostelFees = 3,
        DayBoardersFees = 4,
        CanteenFees = 5,
        LibraryFees = 6,
        InventoryFeeS = 7,
        ExtraCurricularFees = 8
    }
}
