using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class StudentGroup : ResponeValues
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<int> StudentIdColl { get; set; }
    }
    public class StudentGroupCollections : System.Collections.Generic.List<StudentGroup>
    {
        public StudentGroupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
