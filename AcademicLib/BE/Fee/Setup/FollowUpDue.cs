using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Fee.Setup
{
    public class Fine : ResponeValues
    {
        public List<FollowUpDue> DueDataDetColl { get; set; }
        public List<FineSetup> FineSetupDetColl { get; set; }
    }
    public class FollowUpDue
    {
        public FollowUpDue()
        {
            CurFineSetupColl = new List<FollowUpDueDetails>();
        }
        public int? TranId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int? FineOnBasisOfAmount { get; set; }
        public double FineAmount { get; set; }
        public int? DebateOnBasisOfAmount { get; set; }
        public double ReBateAmount { get; set; }
        public List<FollowUpDueDetails> CurFineSetupColl { get; set; } = new List<FollowUpDueDetails>();
    }
    public class FollowUpDueCollections : System.Collections.Generic.List<FollowUpDue>
    {
        public FollowUpDueCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class FollowUpDueDetails
    {
        public int TranId { get; set; }
        public int? DaysFrom { get; set; }
        public int? DaysTo { get; set; }
        public double? Amount { get; set; }
    }
}