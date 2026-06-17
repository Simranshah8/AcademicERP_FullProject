using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Infrastructure
{
    public class RoomDistribution
    {
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public string BuildingName { get; set; }
        public string FloorName { get; set; }
        public int NoOfClassRooms { get; set; }
        public int NoOfOtherRooms { get; set; }
    }

    public class RoomDistributionCollections : System.Collections.Generic.List<RoomDistribution>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
