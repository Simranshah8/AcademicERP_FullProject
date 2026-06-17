using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class UserLogNotification
    {
        public int EntityId { get; set; }
        public bool Add { get; set; }
        public bool Modify { get; set; }
        public bool Delete { get; set; }
        public bool Cancel { get; set; }
        public string UserIdColl { get; set; }
    }
    public class UserLogNotificationCollections : System.Collections.Generic.List<UserLogNotification>
    {

    }
}
