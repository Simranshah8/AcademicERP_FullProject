using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class AppFeatures : ResponeValues
    {
        public int ForUser { get; set; } //--1=Student,2=Employee,3=Admin
        public string ModuleName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool PActive { get; set; }
        public int? ForUserId { get; set; }
        public int? RoleId { get; set; }
        public int? ClassId { get; set; }
        public int? MOrderNo { get; set; }
        public int? EOrderNo { get; set; }

        public bool SubjectTeacher { get; set; }
        public bool ClassTeacher { get; set; }
        public bool CoOrdinator { get; set; }
        public bool HOD { get; set; }
        public string Role { get; set; } = "";
    }
    public class AppFeaturesCollections : System.Collections.Generic.List<AppFeatures> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


}
