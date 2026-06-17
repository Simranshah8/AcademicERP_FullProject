using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLogin.Models
{
    public class AppFeatures 
    {
        public int ForUser { get; set; } //--1=Student,2=Employee,3=Admin
        public string ModuleName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool PActive { get; set; }
        public int EntityId { get; set; }
    }
    public class AppFeaturesCollections : System.Collections.Generic.List<AppFeatures> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


}
