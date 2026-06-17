using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Setup
{
    public class ClassWiseAcademicMonth:ResponeValues
    {        
        public int ClassId { get; set; }
        public int FromMonth { get; set; }
        public int ToMonth { get; set; }
        public int? BranchId { get; set; }

    }
    public class ClassWiseAcademicMonthCollections : List<ClassWiseAcademicMonth> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
