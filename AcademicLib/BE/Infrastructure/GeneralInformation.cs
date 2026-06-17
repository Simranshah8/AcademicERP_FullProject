using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Infrastructure
{

	public class GeneralInformation : ResponeValues 
	{ 

		public int? TranId { get; set; } 
		public int? BranchId { get; set; } 
		public int? CompanyId { get; set; } 
		public int? ProvinceId { get; set; } 
		public string Province { get; set; } ="" ; 
		public int? DistrictId { get; set; } 
		public string District { get; set; } ="" ; 
		public int? LocalLevelId { get; set; } 
		public string LocalLevel { get; set; } ="" ; 
		public int? WardNo { get; set; } 
		public string Tole { get; set; } ="" ; 
		public string MajorLandmarks { get; set; } ="" ; 
		public double? Latitude { get; set; } 
		public double? Longitude { get; set; } 
		public int? CampusChiefId { get; set; } 
		public DateTime? CCAppointmentDate { get; set; } 
		public int? CampusITId { get; set; } 
		public DateTime? CampusITAppointmentDate { get; set; } 
		public bool IsLandOwnershipCertificate { get; set; } 
		public int? LandOwnershipTypeId { get; set; } 
		public double? LandAreaSqm { get; set; } 
		public double? LandAreaRopani { get; set; } 
		public double? LandAreaBigha { get; set; } 
		public int? SiteOrientationId { get; set; } 
		public bool IsAllWeatherRoad { get; set; } 
		public bool IsVehicleAccessibility { get; set; } 
		public int? RoadTypeId { get; set; } 
		public double? RoadWidth { get; set; } 
		public double? WalkingDistanceMeter { get; set; } 
		public double? WalkingDistanceMin { get; set; } 
		public GeneralInformation()
		{
		GeneralInfoFacilitiesColl  =new GeneralInfoFacilitiesCollections();
		}
		public GeneralInfoFacilitiesCollections GeneralInfoFacilitiesColl  { get; set; } 


	}
		public class GeneralInfoFacilities
	{ 

		public int TranId { get; set; } 
		public int? SNo { get; set; } 
		public string Name { get; set; } ="" ; 
		public bool IsAvailable { get; set; } 
		public string SourceName { get; set; } ="" ;
		public int FacilitiesId { get; set; }
	} 

		public class GeneralInfoFacilitiesCollections  : System.Collections.Generic.List<GeneralInfoFacilities>
	{ 

		public string ResponseMSG { get; set; }= "" ; 

		public bool IsSuccess { get; set; }

		}

	public class GeneralInformationCollections : System.Collections.Generic.List<GeneralInformation>
	{
		public GeneralInformationCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EmpShortDet : ResponeValues
	{
		public int? EmployeeId { get; set; }
		public string OfficeContactNo { get; set; }
		public string OfficeEmailId { get; set; }
		public string Qualification { get; set; }
		public int Gender { get; set; }
		public string Caste { get; set; }
		public bool IsTeaching { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string FatherName { get; set; }
		public string MotherName { get; set; }
		public string Department { get; set; }
		public string Designation { get; set; }
		public DateTime DOB_AD { get; set; }

	}
	public class EmpShortDetCollections : List<EmpShortDet>
	{
		public EmpShortDetCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


}



