using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Transport.Creation
{
   public class TransportPoint: ResponeValues
    {

        public TransportPoint()
        {
            RouteIdColl = new List<int>();
        }
        public int? PointId { get; set; }
        public string Name { get; set; }        
        public string RouteName { get; set; }
        public DateTime? PickupTime { get; set; }
        public DateTime? DropTime { get; set; }
        public double InRate { get; set; }
        public double OutRate { get; set; }
        public double BothRate { get; set; }
        public double Lat { get; set; }
        public double Lan { get; set; }
        public string Description { get; set; }
        public bool UpdateInMapping { get; set; }
        public int OrderNo { get; set; }
        public List<int> RouteIdColl { get; set; }

    }
public class TransportPointCollections : List<TransportPoint> {
        public TransportPointCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}