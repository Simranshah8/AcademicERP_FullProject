using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class SubjectMappingStudentWise : ResponeValues
    {
       public List<OptionalSubject> OptionalSubjectList { get; set; }
       public List<OptionalSubjectStudentWise> StudentList { get; set; }
    }

    public class OptionalSubject
    {
        public int TranId { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeTH { get; set; }
        public string CodePR { get; set; }
        public int NoOfOptionalSubject { get; set; }
    }

    public class OptionalSubjectStudentWise : StudentInfo
    {
        public List<int> TranIdColl { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }
        public int NoOfOptionalSubject { get; set; }
        public bool MatchOptSubject { get; set; }
    }
    
}
