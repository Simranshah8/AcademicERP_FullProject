using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class Department : AcademicLib.BE.Academic.Common
    {
        public int? DepartmentId { get; set; }

        public int id
        {
            get
            {
                if (DepartmentId.HasValue)
                    return DepartmentId.Value;
                return 0;
            }
        }
    }
    public class DepartmentColl : System.Collections.Generic.List<Department>
    {
        public DepartmentColl()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
