using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Fee.Setup
{
    public class FineSetup
    {
        public FineSetup()
        {
            FineSetupColl = new List<FineSetupDetails>();
        }
        public int? TranId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int? SelectConditionAsPer { get; set; }
        public double ConditionAmount { get; set; }
        public int? FineOnBasisOfAmnt { get; set; }
        public int? FineOnBasisOfMonth { get; set; }
        public double FineAmount { get; set; }
        public int? DebateOnBasisOfAmnt { get; set; }
        public int? DebateOnBasisOfMonth { get; set; }
        public double ReBateAmount { get; set; }
        public List<FineSetupDetails> FineSetupColl { get; set; }
    }
    public class FineSetupCollections : System.Collections.Generic.List<FineSetup>
    {
        public FineSetupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class FineSetupDetails
    {
        public int TranId { get; set; }
        public int? DaysFrom { get; set; }
        public int? DaysTo { get; set; }
        public double? Amount { get; set; }
    }

}