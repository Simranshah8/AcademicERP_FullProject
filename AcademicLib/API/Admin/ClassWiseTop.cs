using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class ClassWiseTop
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int ExamTypeId { get; set; }
        public string StudentType { get; set; }
        public string ExamType { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public int RankInClass { get; set; }
        public int RankInSection { get; set; }
        public double GPA { get; set; }
        public string Grade { get; set; }
        public double ObtainMark { get; set; }
        public double ObtainPer { get; set; }
    }
    public class ClassWiseTopCollections : System.Collections.Generic.List<ClassWiseTop> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
