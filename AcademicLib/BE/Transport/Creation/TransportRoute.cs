using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Transport.Creation
{
    public class TransportRoute :  ResponeValues
    {

        public int? RouteId { set; get; }
        public string Name { set; get; }
        public int VehicleId { set; get; }
        public string FuelConsume { set; get; }
        public DateTime? ArrivalTime { set; get; }
        public DateTime? DepartureTime { set; get; }
        public double Lat { set; get; }
        public double Lan { set; get; }

        public double EndLat { set; get; }
        public double EndLan { set; get; }


        public DateTime? D_ArrivalTime { set; get; }
        public DateTime? D_DepartureTime { set; get; }
        public double D_Lat { set; get; }
        public double D_Lan { set; get; }

        public double D_EndLat { set; get; }
        public double D_EndLan { set; get; }
        public string VehicleName { get; set; }
        public double Radious { get; set; }
        public string VehicleNumber { get; set; }


    }
    public class TransportRouteCollections : List<TransportRoute> {
        public TransportRouteCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
