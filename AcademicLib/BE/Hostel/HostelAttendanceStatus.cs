using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Hostel
{

    public class HostelAttendanceStatus : ResponeValues
    {

        public int? AttendanceStatusId { get; set; }
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public string ColorCode { get; set; } = "";
        public int? OrderNo { get; set; }
        public int? AttendanceTypeId { get; set; }
        public string Description { get; set; } = "";
        public bool isDefault { get; set; }
    }
    public class HostelAttendanceStatusCollections : List<HostelAttendanceStatus>
    {
        public HostelAttendanceStatusCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}

