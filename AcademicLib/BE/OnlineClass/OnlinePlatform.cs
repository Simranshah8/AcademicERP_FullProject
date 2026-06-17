using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.OnlineClass
{
    public class OnlinePlatform : ResponeValues
    {
        public int UserId { get; set; }
        public BE.Global.ONLINE_PLATFORMS PlatformType { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Link { get; set; }
    }

    public class OnlinePlatformCollections : System.Collections.Generic.List<OnlinePlatform>
    {
        public OnlinePlatformCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
