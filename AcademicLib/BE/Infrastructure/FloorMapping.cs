using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Infrastructure
{
    public class FloorMapping : ResponeValues
    {
        public int? TranId { get; set; }
        public int? BranchId { get; set; }
        public int? BuildingId { get; set; }
        public int? FloorId { get; set; }
        public int? NoOfClassRooms { get; set; }
        public int? NoOfOtherRooms { get; set; }
        public string SafetyMeasures { get; set; } = "";
        public string BuildingName { get; set; } = "";
        public string FloorName { get; set; } = "";
        //Add Field
        public string FloorType { get; set; } = "";
        public FloorMapping()
        {
            BuildingWiseFloorList = new BuildingWiseFloorMappingCollections();
        }
        public BuildingWiseFloorMappingCollections BuildingWiseFloorList { get;set;}

    }
    public class FloorMappingCollections : List<FloorMapping>
    {
        public FloorMappingCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class BuildingWiseFloorMapping : ResponeValues
    {
        public int? FloorId { get; set; }
        public int? NoOfClassRooms { get; set; }
        public int? NoOfOtherRooms { get; set; }
        public string SafetyMeasures { get; set; } = "";
        //Add Field
        public string FloorType { get; set; } = "";
    }
    public class BuildingWiseFloorMappingCollections : List<BuildingWiseFloorMapping>
    {
        public BuildingWiseFloorMappingCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}