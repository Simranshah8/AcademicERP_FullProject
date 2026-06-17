using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Transport.Creation
{

	public class TransportAtt : ResponeValues
	{

		public int? TranId { get; set; }
		public DateTime? ForDate { get; set; }
		
		public int VehicleId { get; set; }
		public int AttendanceForId { get; set; }
		public int StudentId { get; set; }
		public int? VehiclePointId { get; set; }
		public bool PickUp { get; set; }
		public bool DropOff { get; set; }
		public string Remarks { get; set; } = "";

		public int RouteId { get; set; }
		public string StudentName { get; set; } = "";
		public string Class { get; set; } = "";
		public string Section { get; set; } = "";
		public string RegNo { get; set; } = "";
		public string STransportPoint { get; set; } = "";
		public string SContactNo { get; set; } = "";
		public string SAddress { get; set; } = "";
		public string PointName { get; set; } = "";
		public string AttDate { get; set; } = "";

		public string ForMiti { get; set; } = "";
		public string VehicleName { get; set; } = "";
		public string VehicleNumber { get; set; } = "";
		public string Route { get; set; } = "";
		public int TotalPickup { get; set; }
		public int TotalDropOff { get; set; }

		//Add bY Prshant
		public string Batch { get; set; } = "";
		public string ClassYear { get; set; } = "";
		public string Semester { get; set; } = "";
		public int? PointId { get; set; } 
		public string DriverName { get; set; } = "";
		public string ConductorName { get; set; } = "";
		public string InchargeName { get; set; } = "";
		public string ContactNo { get; set; } = "";
		public string Address { get; set; } = "";
		public string ClassDetails { get; set; } = "";
		public string TransportPoint { get; set; } = "";
		public int? TotalStudents { get; set; }
		//Add by Prashant Faghun 27
		public bool Attendance { get; set; }


	}
	public class TransportAttCollections : System.Collections.Generic.List<TransportAtt>
	{
		public TransportAttCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

