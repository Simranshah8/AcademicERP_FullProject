using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Transport.Creation
{
    public class TransportMapping : ResponeValues
    {
        public TransportMapping()
        {
            MonthIdColl = new List<TransportMappingMonth>();
        }
        public int? TranId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int? RouteId { get; set; }
        public int PointId { get; set; }
        public int TravelType { get; set; }
        public double Rate { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public double PayableAmt { get; set; }

        public List<TransportMappingMonth> MonthIdColl { get; set; }
    }

    public class TransportMappingCollections : System.Collections.Generic.List<TransportMapping>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }

    public class TransportMappingMonth
    {
        public int MonthId { get; set; }
        public double Rate { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public double PayableAmt { get; set; }

        public string Remarks { get; set; }
    }
}
