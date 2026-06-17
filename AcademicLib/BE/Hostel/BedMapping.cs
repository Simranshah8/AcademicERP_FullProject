using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Hostel
{
    public class BedMapping : ResponeValues
    {
        public BedMapping()
        {
            MonthIdColl = new List<Transport.Creation.TransportMappingMonth>();
        }
        public int? TranId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int? RoomId { get; set; }
        public int? BedId { get; set; }
        public int BedNo { get; set; }
        public DateTime? AllotDate { get; set; }
        public double Rate { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public double PayableAmt { get; set; }

        public List<Transport.Creation.TransportMappingMonth> MonthIdColl { get; set; }
    }

    public class BedMappingCollections : System.Collections.Generic.List<BedMapping>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    
}
