using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class HomeWorkChecked
    {
        public int StudentId { get; set; }
        public int HomeWorkId { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public int SUserId { get; set; }
    }
    public class HomeWorkCheckedCollections : System.Collections.Generic.List<HomeWorkChecked>
    {

    }

    public class AssignmentChecked
    {
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }

        public string ObtainGrade { get; set; }

        public double ObtainMark { get; set; }        
        public int UserId { get; set; }
        public int SUserId { get; set; }
    }

    public class AssignmentCheckedCollections : System.Collections.Generic.List<AssignmentChecked>
    {

    }
}
