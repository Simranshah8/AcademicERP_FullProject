using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Hostel
{
    public class HostelAttendance : ResponeValues
    {
        public int? StudentId { get; set; }
        public string RegNo { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string ClassSection { get; set; } = "";
        public string RoomDetail { get; set; } = "";
        public int? HostelId { get; set; }
        public int? BuildingId { get; set; }
        public int? FloorId { get; set; }
        public int? ShiftId { get; set; }
        public DateTime ForDate { get; set; }
        public string ObservationRemarks { get; set; } = "";
        public string ShiftName { get; set; } = "";
        public string StatusName { get; set; } = "";
        public int? StatusId { get; set; }
        public string Remarks { get; set; } = "";
    }
    public class HostelAttendanceCollections : List<HostelAttendance>
    {
        public HostelAttendanceCollections()
        {
            ResponseMSG = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}