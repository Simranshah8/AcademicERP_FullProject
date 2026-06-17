using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Creation
{
   public class ParentExamTypeGroup: ResponeValues
    {
        public int? TranId { get; set; }
        public int OrderNo { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTime? ResultDate { get; set; }
        public DateTime? ResultTime { get; set; }

        public string ResultDateBS { get; set; }
        public int SNo { get; set; }
        private List<ParentExamTypeGroupDetails>  _ParentExamTypeGroupDetailsCollections = new List<ParentExamTypeGroupDetails>();
        public List<ParentExamTypeGroupDetails> ParentExamTypeGroupDetailsColl
        {
            get
            {
                return _ParentExamTypeGroupDetailsCollections;
            }
            set
            {
                _ParentExamTypeGroupDetailsCollections = value;
            }
        }

        //Added field
        public int? DisplayYearBS { get; set; }
        public int? DisplayYearAD { get; set; }
    }
    public class ParentExamTypeGroupCollections : System.Collections.Generic.List<ParentExamTypeGroup> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ParentExamTypeGroupDetails
    {
        public int TranId { get; set; }
        public int ParentExamTypeGroupId { get; set; }
        public int SNO { get; set; }
        public int? ExamTypeGroupId { get; set; }
        public double Percent { get; set; }
        public string DisplayName { get; set; }
        public bool IsCalculateAttendance { get; set; }
    }
    public class ParentExamTypeGroupDetailsCollections : System.Collections.Generic.List<ParentExamTypeGroupDetails> { }
}

 