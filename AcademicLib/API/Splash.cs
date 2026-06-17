using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API
{
    public class Splash : ResponeValues
    {
        public string Msg { get; set; }
        public string Heading { get; set; }
        public string Footer { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int PendingDay { get; set; }
    }
}
