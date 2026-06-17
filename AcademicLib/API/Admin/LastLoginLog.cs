using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class LastLoginLog
    {
        public LastLoginLog()
        {
            UserName = "";
            GroupName = "";
            PublicIP = "";
            LogDateTimeBS = "";
            PhotoPath = "";
        }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string PublicIP { get; set; }
        public DateTime? LogDateTimeAD { get; set; }
        public string LogDateTimeBS { get; set; }
        public string PhotoPath { get; set; }
        public string PCName { get; set; }
    }
    public class LastLoginLogCollections : System.Collections.Generic.List<LastLoginLog> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
