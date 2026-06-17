using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class ServiceType : Common
    {
        public int? ServiceTypeId { get; set; }
        public int id
        {
            get
            {
                if (ServiceTypeId.HasValue)
                    return ServiceTypeId.Value;
                return 0;
            }
        }

    }

    public class ServiceTypeCollections : System.Collections.Generic.List<ServiceType>
    {
        public ServiceTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
