using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Coordinator : ResponeValues
    {

        public int? TranId { get; set; }
        public int EmployeeId { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public bool IsInclude { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Teacher { get; set; }
        public int? BatchId { get; set; }
        public int? ClassYearId { get; set; }
        public int? SemesterId { get; set; }
        public string Batch { get; set; }
        public string ClassYear { get; set; }
        public string Semester { get; set; }
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
    public class CoordinatorCollections : System.Collections.Generic.List<Coordinator>
    {
        public CoordinatorCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

}
