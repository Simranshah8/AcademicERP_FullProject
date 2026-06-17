using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Transport
{
    public class MSGForPickupPoint
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
    public class MSGForPickupPointCollections : System.Collections.Generic.List<MSGForPickupPoint>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
