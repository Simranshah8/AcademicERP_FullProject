using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Setup
{
    public class UpgradeStudentClass: ResponeValues
    {        
        public int FromClassId { get; set; }
        public int ToClassId { get; set; }
        public bool CanUpgarde { get; set; }
        public int? BranchId { get; set; }
    }
    public class UpgradeStudentClassCollections : List<UpgradeStudentClass> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
