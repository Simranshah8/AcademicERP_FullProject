using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Transaction
{
    public class CostCenterGenerateLog
    {
        public int ForCostCenter { get; set; }
        public string UserName { get; set; }
        public DateTime LogDateTime_AD { get; set; }
        public string LogDateTime_BS { get; set; }
    }
    public class CostCenterGenerateLogCollections : System.Collections.Generic.List<CostCenterGenerateLog>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
