using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
  public  class SeatAllotment : ResponeValues
    {
        public int? SeatAllotmentId { get; set; }
        public int? ShiftId { get; set; }
        public int? MediumId { get; set; }
        public int? ClassId { get; set; }
        public int? StudentId { get; set; }
        public bool IsAllotment { get; set; }
        public int? SectionId { get; set; }
        public string NewQuota { get; set; }
    }
    public class SeatAllotmentCollections : List<SeatAllotment> {
        public SeatAllotmentCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
 
 