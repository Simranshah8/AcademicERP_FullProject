using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Global
{
    public class StudentSMS
    {
        public int EntityId { get; set; }        
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ContactNo { get; set; }
        public string StudentName { get; set; }
        public string ContentPath { get; set; }
    }
}
