using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class VehicleDetail
    {
        public int SNo { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNo { get; set; }
        public DateTime? RenewalDate_AD { get; set; }
        public string RenewalDate_BS { get; set; }
        public string JachpassNo { get; set; }
        public DateTime? JPValidityTo_AD { get; set; }
        public string JPValidityTo_BS { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
        public string ConductorName { get; set; }
        public string ConductorContactNo { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ConductorId { get; set; }
    }

    public class VehicleDetailCollections : System.Collections.Generic.List<VehicleDetail>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
