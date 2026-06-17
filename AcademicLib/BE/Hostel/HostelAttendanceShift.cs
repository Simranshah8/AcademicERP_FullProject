using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Hostel
{

    public class HostelAttendanceShift : ResponeValues
    {

        public int? AttendanceShiftId { get; set; }
        public string Name { get; set; } = "";
        public int? OrderNo { get; set; }
        public string Description { get; set; } = "";
    }
    public class HostelAttendanceShiftCollections : List<HostelAttendanceShift>
    {
        public HostelAttendanceShiftCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}

