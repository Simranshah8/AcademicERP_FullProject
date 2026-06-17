using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FormEntity
{
    public class EntityFieldsAllow : ResponeValues
    {      
        public int ForUserId { get; set; }
        public int FieldId { get; set; }
        public bool IsAllow { get; set; }
    }

    public class EntityFieldsAllowCollections : System.Collections.Generic.List<EntityFieldsAllow>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
