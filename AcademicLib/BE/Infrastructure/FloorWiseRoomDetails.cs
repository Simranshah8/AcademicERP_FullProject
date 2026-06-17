using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Infrastructure
{
    public class FloorWiseRoomDetails : ResponeValues
    {
        public int? FloorWiseRoomDetailsId { get; set; }
        public int? BuildingId { get; set; }
        public int? FloorId { get; set; }
        public string Name { get; set; } = "";
        public int? UtilitiesId { get; set; }
        public string SubUtility { get; set; } = "";
        public string Length { get; set; } = "";
        public string Breadth { get; set; } = "";
        public int? Capacity { get; set; }
        public string Resources { get; set; } = "";
        public int? RoomTypeId { get; set; }
        public string Utilities { get; set; } = "";
        public string BuildingName { get; set; } = "";
        public string FloorName { get; set; } = "";
        public FloorWiseRoomDetails()
        {
            subFloorWiseRoomDetailsColl = new SubFloorWiseRoomDetailsColl();
        }
        public SubFloorWiseRoomDetailsColl subFloorWiseRoomDetailsColl { get; set; }
    }
    public class FloorWiseRoomDetailsCollections : List<FloorWiseRoomDetails>
    {
        public string ResponseMSG { get; set; } = "";
        public bool IsSuccess { get; set; }
    }
    public class SubFloorWiseRoomDetails : ResponeValues
    {
        public int? FloorWiseRoomDetailsId { get; set; }
        public string Name { get; set; } = "";
        public int? UtilitiesId { get; set; }
        public string SubUtility { get; set; } = "";
        public string Length { get; set; } = "";
        public string Breadth { get; set; } = "";
        public int? Capacity { get; set; }
        public string Resources { get; set; } = "";
        public int? RoomTypeId { get; set; }
    }
    public class SubFloorWiseRoomDetailsColl : List<SubFloorWiseRoomDetails>
    {
        public string ResponseMSG { get; set; } = "";
        public bool IsSuccess { get; set; }
    }
}