using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class SubjectWiseTop : ClassWiseTop
    {
        public string SubjectName { get; set; }
        public int SubjectSNo { get; set; }
    }
    public class SubjectWiseTopCollections : System.Collections.Generic.List<SubjectWiseTop>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
