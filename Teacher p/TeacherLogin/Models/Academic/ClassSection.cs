using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Academic.Creation
{
    public class ClassSectionList
    {
        public ClassSectionList()
        {
            ClassList = new ClassCollections();
            SectionList = new ClassSectionCollections();
            SectionListWithClass = new ClassSectionCollections();
            SectionListOnly = new ClassSectionCollections();
        }
        public ClassCollections ClassList { get; set; }
        public ClassSectionCollections SectionList { get; set; }
        public ClassSectionCollections SectionListWithClass { get; set; }
        public ClassSectionCollections SectionListOnly { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClassSections : ClassSection
    {

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
    public class ClassSectionCollections : List<ClassSections>
    {
        public ClassSectionCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}