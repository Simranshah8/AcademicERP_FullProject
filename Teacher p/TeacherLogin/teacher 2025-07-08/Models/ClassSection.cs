using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models
{
    public class Responsemsg
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        private List<Class> _ClassListColl = new List<Class>();
        public List<Class> ClassList
        {
            get
            {
                return _ClassListColl;
            }
            set
            {
                _ClassListColl = value;
            }

        }
        private List<Section> _SectionListColl = new List<Section>();
        public List<Section> SectionList
        {
            get
            {
                return _SectionListColl;
            }
            set
            {
                _SectionListColl = value;
            }

        }
    }
    public class Class
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class Section
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }

        public string text
        {
            get
            {
                return ClassName.Trim() + (SectionName.Trim().Length > 0 ? "-" + SectionName : "");
            }
        }
    }

}
