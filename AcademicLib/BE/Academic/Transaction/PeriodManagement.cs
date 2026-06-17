using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class PeriodManagement : ResponeValues
    {
        public int? TranId { get; set; }
        public int? ClassShiftId { get; set; }

        public string ClassShiftName { get; set; }
        public int NoOfPeriod { get; set; }
        public int EachPeriodDuration { get; set; }
        public int B1BreakAfterPeriod { get; set; }
        public int B1TimeDuration { get; set; }
        public int B2BreakAfterPeriod { get; set; }
        public int B2TimeDuration { get; set; }

        public int B3BreakAfterPeriod { get; set; }
        public int B3TimeDuration { get; set; }
        public int B4BreakAfterPeriod { get; set; }
        public int B4TimeDuration { get; set; }

        private List<PeriodManagementDetails> _PeriodManagementDetailsColl = new List<PeriodManagementDetails>();

        public List<PeriodManagementDetails> PeriodManagementDetailsColl
        {
            get
            {
                return _PeriodManagementDetailsColl;
            }
            set
            {
                _PeriodManagementDetailsColl = value;
            }
        }
    }
    public class PeriodManagementCollections : List<PeriodManagement> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class PeriodManagementDetails
    {
        public int TranId { get; set; }
        public int PeriodManagementId { get; set; }
        public int Period { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime?  EndTime { get; set; }
        public int TimeDuration { get; set; }
      
    }
}
