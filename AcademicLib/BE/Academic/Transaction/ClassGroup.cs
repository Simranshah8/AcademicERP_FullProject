using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ClassGroup : ResponeValues
    {
        public ClassGroup()
        {
            Name = "";
            SubjectName = "";
            EmployeeName = "";
            ForClass = "";
            DetailsColl = new List<ClassGroupDetails>();
        }
        public int? ClassGroupId { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public int EmployeeId { get; set; }
        public List<ClassGroupDetails> DetailsColl { get; set; }

        public string SubjectName { get; set; }
        public string EmployeeName { get; set; }
        public string ForClass { get; set; }
    }
    public class ClassGroupCollections : System.Collections.Generic.List<ClassGroup>
    {
        public ClassGroupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ClassGroupDetails
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
    }
}
