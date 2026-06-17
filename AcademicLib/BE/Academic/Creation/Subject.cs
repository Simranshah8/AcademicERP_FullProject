using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AcademicLib.BE.Academic.Creation
{
    public class Subject : Common
    {
        public int? SubjectId { get; set; }
        public int id
        {
            get
            {
                if (SubjectId.HasValue)
                    return SubjectId.Value;
                return 0;
            }
        }

        public string CodeTH { get; set; }
        public string CodePR { get; set; }
        public bool IsECA { get; set; }
        public bool IsMath { get; set; }

        public int? ClassId { get; set; }
        public int? SectionId { get; set; }

        public double CRTH { get; set; }
        public double CRPR { get; set; }
        public double CR { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }


        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

        public bool SubjectTeacher { get; set; }
        public bool ClassTeacher { get; set; }
        public bool CoOrdinator { get; set; }
        public bool HOD { get; set; }
        public string Role { get; set; } = "";


    }
    public class SubjectCollections : System.Collections.Generic.List<Subject>
    {
        public SubjectCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ImportSubject
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeTH { get; set; }
        public string CodePR { get; set; }
        public bool IsECA { get; set; }
        public bool IsMath { get; set; }
        public double CRTH { get; set; }
        public double CRPR { get; set; }
        public double CR { get; set; }
    }
}
