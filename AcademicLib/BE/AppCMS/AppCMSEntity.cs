using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.AppCMS.Creation
{
    public class AppCMSEntity 
    {
        public int BranchId { get; set; }
        public int TranId { get; set; }
        public int EntityId { get; set; }
        public int OrderNo { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Label { get; set; }
    }
    public class AppCMSEntityCollections : System.Collections.Generic.List<AppCMSEntity>
    {
        public string ResponseMSG { get; set; } = "";
        public bool IsSuccess { get; set; }
    }
}
