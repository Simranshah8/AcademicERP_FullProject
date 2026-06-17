using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Hostel
{
    public class Building : ResponeValues
    {

        public int? BuildingId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }
        //For Add field by PRashant

        public string BuildingNo { get; set; } = "";
        public int? BuildingTypeId { get; set; }
        public string OtherBuildingType { get; set; } = "";
        public int? NoOfFloor { get; set; }
        public string OverallCondition { get; set; } = "";
        public int? NoOfClassRooms { get; set; }
        public int? NoOfOtherRooms { get; set; }
        public DateTime? ConstructionDate { get; set; }
        public string StructureType { get; set; } = "";
        public string OtherStructureType { get; set; } = "";
        public string RoofType { get; set; } = "";
        public string OtherRoofType { get; set; } = "";
        public string DamageGrade { get; set; } = "";
        public string InfrastructureType { get; set; } = "";
        public string FundingSources { get; set; } = "";
        public string InterventionType { get; set; } = "";
        public bool IsApprovedDesign { get; set; }
        public bool IsCompletionCertificate { get; set; }
        public string CompletionStatus { get; set; } = "";
        public DateTime? CompletionDate { get; set; }
        public string Remarks { get; set; } = "";
        public double Budget { get; set; }
        public int? BoysToiletNo { get; set; }
        public int? GirlsToiletNo { get; set; }
        public bool IsToiletFunctional { get; set; }
        public string FacilityNotFunctioning { get; set; } = "";

        public string ConstructionDate_BS { get; set; } = "";
        public string CompletionData_BS { get; set; } = "";
        public string BuildingType { get; set; } = "";

        public double? areaCoveredByBuilding { get; set; }
        public double? areaCoveredByAllRooms { get; set; }
        public bool ownershipOfBuilding { get; set; }
        public bool hasInternetConnection { get; set; }

        public Building()
        {
            BuildingFacilitiesColl = new BuildingFacilitiesCollections();
            BuildingColl = new BuildingCollections();
        }
        public BuildingFacilitiesCollections BuildingFacilitiesColl { get; set; }
        public BuildingCollections BuildingColl { get; set; }
    }
    public class BuildingCollections : List<Building> {
        public BuildingCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class BuildingFacilities
    {
        public int BuildingId { get; set; }
        public string Name { get; set; } = "";
    }

    public class BuildingFacilitiesCollections : System.Collections.Generic.List<BuildingFacilities>
    {
        public string ResponseMSG { get; set; } = "";
        public bool IsSuccess { get; set; }
    }
}
