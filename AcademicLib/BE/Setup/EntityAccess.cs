using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class EntityAccess
    {
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int ModuleId { get; set; }
        public int EntityId { get; set; }
        public int EntityTranId { get; set; }
        public string ModuleName { get; set; }
        public string EntityName { get; set; }
        public bool Full { get; set; }        
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Modify { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool Export { get; set; }

        public string Icon { get; set; }
        public string WebUrl { get; set; }
        public string Description { get; set; }
    }
    public class EntityAccessCollections : System.Collections.Generic.List<EntityAccess> {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }

}
