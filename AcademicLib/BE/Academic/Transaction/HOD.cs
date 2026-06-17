using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
   public class HOD : ResponeValues
    {
        public int HODId { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public int ClassShiftId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int IsInclude { get; set; }
     
    }
    public class HODCollections : List<HOD> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
