using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeInstallmentType : AcademicLib.BE.Academic.Common
    {
        public int? FeeInstallmentTypeId { get; set; }
        public int id
        {
            get
            {
                if (FeeInstallmentTypeId.HasValue)
                    return FeeInstallmentTypeId.Value;
                return 0;
            }
        }

    }

    public class FeeInstallmentTypeCollections : System.Collections.Generic.List<FeeInstallmentType>
    {
        public FeeInstallmentTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
