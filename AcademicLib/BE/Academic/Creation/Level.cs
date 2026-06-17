using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Level : Common
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

    public class LevelCollections : System.Collections.Generic.List<Level>
    {
        public LevelCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
