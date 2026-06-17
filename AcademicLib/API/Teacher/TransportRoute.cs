using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class TransportRoute
    {
        public TransportRoute()
        {
            PickupPointColl = new List<PickupPoints>();
            RouteName = "";
            VehicleName = "";
            VehicleNo = "";
        }
        public int VehicleId { get; set; }
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public DateTime? ArrivalTime { get; set; } 
        public DateTime? DepartureTime { get; set; }
        public double StartPointLat { get; set; } 
        public double StartPointLng { get; set; }
        public double EndPointLat { get; set; }
        public double EndPointLng { get; set; }
        public double Radious { get; set; }

        public DateTime? D_ArrivalTime { get; set; }
        public DateTime? D_DepartureTime { get; set; }
        public double D_StartPointLat { get; set; }
        public double D_StartPointLng { get; set; }
        public double D_EndPointLat { get; set; }
        public double D_EndPointLng { get; set; }

        public int DriverId { get; set; }
        public int UserId { get; set; }

        public string VehicleName { get; set; }
        public string VehicleNo { get; set; }
        public List<PickupPoints> PickupPointColl { get; set; }
      
    }
    public class TransportRouteCollections : System.Collections.Generic.List<TransportRoute>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class PickupPoints
    {
        public PickupPoints()
        {
            PickupPointName = "";
            VehicleName = "";
            VehicleNo = "";
        }
        public string PickupPointName { get; set; }
        public DateTime? PickupTime { get; set; }
        public double PickAtLat { get; set; }
        public double PickAtLng { get; set; }
        public int PointId { get; set; }
        public bool IsPickPoint { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNo { get; set; }
        public double PickupRate { get; set; }
        public double DropRate { get; set; }
        public double BothRate { get; set; }
    }
}
