using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class StudentType : Common
    {
        public int? StudentTypeId { get; set; }
        public int id
        {
            get
            {
                if (StudentTypeId.HasValue)
                    return StudentTypeId.Value;
                return 0;
            }
        }

        public STUDENTTYPES TypeId { get; set; }

    }

    public class StudentTypeCollections : System.Collections.Generic.List<StudentType>
    {
        public StudentTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public enum STUDENTTYPES
    {
        OTHER=0,
        DAY_SCHOLAR=1,
        HOSTEL=2,
        TRANSPORT=3
    }

}
