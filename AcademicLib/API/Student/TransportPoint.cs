using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Student
{
    public class TransportPoint 
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public double StartPointLat { get; set; }
        public double StartPointLng { get; set; }
        public double EndPointLat { get; set; }
        public double EndPointLng { get; set; }

        public string PickupPointName { get; set; }
        public DateTime? PickupTime { get; set; }
        public double PickAtLat { get; set; }
        public double PickAtLng { get; set; }

        public int PointId { get; set; }
        public double Radious { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

        public DateTime? D_ArrivalTime { get; set; }
        public DateTime? D_DepartureTime { get; set; }
        public double D_StartPointLat { get; set; }
        public double D_StartPointLng { get; set; }
        public double D_EndPointLat { get; set; }
        public double D_EndPointLng { get; set; }
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public string GPSDevice { get; set; }

        public string Url { get; set; } = "";
        public string User { get; set; } = "";
        public string Pwd { get; set; } = "";
        public string Authentication { get; set; } = "";
        public string Token { get; set; } = "";
    }
}
