using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class ExamCopyCheck
    {
        public int? OETranId { get; set; }
        public double FullMark { get; set; }
        public double ObtainMark { get; set; }
        public string Remarks { get; set; }

        public int UserId { get; set; }
    }
}
