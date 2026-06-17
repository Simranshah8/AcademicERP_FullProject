using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class ClassLevel : Common
    {
        public int? LevelId { get; set; }
        public int id
        {
            get
            {
                if (LevelId.HasValue)
                    return LevelId.Value;
                return 0;
            }
        }

    }

    public class ClassLevelCollections : System.Collections.Generic.List<ClassLevel>
    {
        public ClassLevelCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
